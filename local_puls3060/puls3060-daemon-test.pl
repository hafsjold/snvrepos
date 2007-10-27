#!/usr/bin/perl -w
use lib '/usr/local/puls3060';
use strict;
use DBI;
use DBD::Pg;
use Mail::Sender;
use Fcntl;
use POSIX ":sys_wait_h";
use Fcntl ':mode';
use Time::localtime;
use Net::SFTP::Foreign::Compat;
use Net::SFTP::Foreign::Constants qw( :flags );
use DBI qw(:sql_types);
use Mail::Sender;

##### make your settings here:
# databases
my $db_puls = "puls3060";
my $db_regnskab = "regnskab3060";
my $Profile_id = 2;	 ##  2=LKP, 3=TLKP, 1=lkp(old)
my $dbhost = "localhost"; ## "localhost" "hd08.hafsjold.dk";

data_til_pbc();


exit 0;


##################################################
# Data fra PBC
##################################################
sub data_fra_pbc {
  ##$daemon->dolog("Start new data_fra_pbc");

  ##
  ## Her skal udføres testget.pl
  ##

  my $dbh_regnskab;
  my $number_of_files;
  my $sql_insert_linie;
  my $sth_insert_linie;

  my $sql_insert_data;
  my $sth_insert_data;

  my $sql_select_file;
  my $sth_select_file;

  my $sql_select_sftp;
  my $sth_select_sftp;

  $dbh_regnskab = DBI->connect( "dbi:Pg:dbname=$db_regnskab;host=$dbhost", "pgsql", "", {RaiseError => 1, AutoCommit => 0} ) 
  				|| die "Database connection not made: $DBI::errstr";

  $number_of_files = 0;

  $dbh_regnskab->do( "DELETE FROM pbs.tblpbsnetdir;" );


  $sql_insert_linie = <<SQL;
  	INSERT INTO pbs.tblpbsnetdir 
   		(
  			id,
  			type,
  			path,
  			filename,
  			size,
  			atime,
  			mtime,
  			perm,
  			uid,
  			gid
  		) 
   		VALUES(
   			nextval('pbs.tblpbsnetdir_id_seq'::text), 
 			?, 
 			?, 
 			?, 
 			?, 
 			CAST(? AS timestamp), 
 			CAST(? AS timestamp), 
 			?, 
 			?, 
 			?
   		);
SQL
    
  $sth_insert_linie = $dbh_regnskab->prepare( $sql_insert_linie );

  my ($type, $path, $filename, $size, $atime, $mtime, $perm, $uid, $gid );

  $sql_select_sftp = <<SQL;
    SELECT 
      host,
      "user",
      outbound,
      inbound
    FROM
      pbs.tblsftp
    WHERE id = ?;
SQL
  $sth_select_sftp = $dbh_regnskab->prepare( $sql_select_sftp );
  $sth_select_sftp->bind_param( 1, $Profile_id, SQL_INTEGER );	## 1=PBS, 2=Test (HD03) 
  $sth_select_sftp->execute();

  my ($host, $user, $outbound, $inbound);

  $sth_select_sftp->bind_columns( undef, \$host, \$user, \$outbound, \$inbound );
  $sth_select_sftp->fetch();

  ##$daemon->dolog("data_fra_pbc Debug Net::SFTP::Foreign::Compat->new BEGIN");
  my $sftp = Net::SFTP::Foreign::Compat->new($host, user => $user);
  ##$daemon->dolog("data_fra_pbc Debug Net::SFTP::Foreign::Compat->new END");

  ##$daemon->dolog("data_fra_pbc DEBUG-1");

  $path = $outbound;

  my @outbound_files  = $sftp->ls($path);

  ##$daemon->dolog("data_fra_pbc DEBUG-2");

  foreach my $item (@outbound_files){
    ##$daemon->dolog("data_fra_pbc DEBUG-3");
    $filename	= $item->{'filename'};
    if (($filename ne ".") && ($filename ne "..")){
      my $attrs = $item->{'a'};
      $size = $attrs->{'size'};
      $atime = ctime($attrs->{'atime'});
      $mtime = ctime($attrs->{'mtime'});
      $perm = ($attrs->{'perm'} & 0777);
      $type = (S_IFMT($attrs->{'perm'})) >> 12;
      $uid =  $attrs->{'uid'};
      $gid =  $attrs->{'gid'};

      $sth_insert_linie->bind_param(  1, $type, SQL_INTEGER );
      $sth_insert_linie->bind_param(  2, $path, SQL_VARCHAR );
      $sth_insert_linie->bind_param(  3, $filename, SQL_VARCHAR );
      $sth_insert_linie->bind_param(  4, "$size", SQL_INTEGER ); 
      $sth_insert_linie->bind_param(  5, "$atime", SQL_VARCHAR );
      $sth_insert_linie->bind_param(  6, "$mtime", SQL_VARCHAR );
      $sth_insert_linie->bind_param(  7, $perm, SQL_INTEGER );
      $sth_insert_linie->bind_param(  8, $uid, SQL_INTEGER );
      $sth_insert_linie->bind_param(  9, $gid, SQL_INTEGER );
      $sth_insert_linie->execute();
    };
  };

  $dbh_regnskab->do( "SELECT pbs.mergenewfiles();" );

  $sql_insert_data = <<SQL;
  	INSERT INTO pbs.tblpbsfile 
   		(
  			pbsfilesid,
  			seqnr,     
  			data      
  		) 
   		VALUES(
   			?, 
   			?, 
   			?
   		);
SQL
  $sth_insert_data = $dbh_regnskab->prepare( $sql_insert_data );

  my $sql_update = <<SQL;
  	UPDATE pbs.tblpbsfiles set
  			transmittime = now()
  	WHERE id = ?;
SQL
  my $sth_update = $dbh_regnskab->prepare( $sql_update );

  my $sql_addpbsnet = <<SQL;
    SELECT pbs.addpbsnet(?);
SQL
  my $sth_addpbsnet = $dbh_regnskab->prepare( $sql_addpbsnet );

  $sql_select_file = <<SQL;
    SELECT 
      id,
      "path",
      filename,
      size
    FROM
      pbs.tblpbsfiles
    WHERE "path" = \'$outbound\'
      AND transmittime IS NULL;
SQL
  $sth_select_file = $dbh_regnskab->prepare( $sql_select_file );
  $sth_select_file->execute();
  my $pbsfilesid;
  $sth_select_file->bind_columns( undef, \$pbsfilesid, \$path, \$filename, \$size );

## ($data, $status) = $sftp->do_read($handle, $offset, $copy_size)
## 
## Sends the SSH_FXP_READ command to read from an open file handle $handle, starting at $offset, 
## and reading at most $copy_size bytes.
## 
## Returns a two-element list consisting of the data read from the SFTP server in the first slot,
## and the status code (if any) in the second. In the case of a successful read, the status code 
## will be undef, and the data will be defined and true. In the case of EOF, the status code will 
## be SSH2_FX_EOF, and the data will be undef. And in the case of an error in the read, a warning 
## will be emitted, the status code will contain the error code, and the data will be undef.

  while( $sth_select_file->fetch() ) {
    ##$daemon->dolog("data_fra_pbc DEBUG-4");
    my $flags = SSH2_FXF_READ;
    my $handle =  $sftp->do_open($path . "/" . $filename, $flags);

    my $buffer = "";
    my $fileoffset = 0;
    my $bufsize = 32000;
    if ($bufsize > $size) { 
      $bufsize = $size 
    }; 
    while (($size - $fileoffset) > 0) {
    	my($workbuffer, $status) = $sftp->do_read($handle, $fileoffset, $bufsize);
  		##eval {$daemon->dolog("data_fra_pbc Debug filename=>$filename<, fileoffset=>$fileoffset<, bufsize=>$bufsize<") };
		$buffer .= $workbuffer;
		$fileoffset += $bufsize;
    }
    $sftp->do_close($handle);
	
	eval { Log_data_fra_pbc( $filename, $buffer, $pbsfilesid ) };

    $number_of_files++;
    SendMail_data_fra_pbc($filename, $buffer);
    my $data;
    my $seqnr = 0;
    my $crlf = "\r\n|\n";
    foreach $data (split(/$crlf/,$buffer)) {
      if (($seqnr == 0) && !($data =~ /^PBCNET/)){
		$seqnr++;
      }
      $sth_insert_data->bind_param(  1, $pbsfilesid, SQL_INTEGER );
      $sth_insert_data->bind_param(  2, $seqnr++, SQL_INTEGER );
      $sth_insert_data->bind_param(  3, $data, SQL_VARCHAR );
      $sth_insert_data->execute();
    };
    $sth_update->bind_param( 1, $pbsfilesid, SQL_INTEGER );
    $sth_update->execute();

    $sth_addpbsnet->bind_param( 1, $pbsfilesid, SQL_INTEGER );
    $sth_addpbsnet->execute();	
  };

  $dbh_regnskab->commit();
  
  ##$daemon->dolog("data_fra_pbc DEBUG-5");

  eval { $sth_select_sftp->finish() };
  eval { $sth_insert_linie->finish() };
  eval { $sth_select_file->finish() };
  eval { $sth_insert_data->finish() };
  eval { $sth_update->finish() };
  eval { $sth_addpbsnet->finish() };
  
  if ($number_of_files > 0) {
    ##$daemon->dolog("Data modtaget fra pbc");
    $dbh_regnskab->do("SELECT pbs.betalinger_fra_pbs (0);");
    $dbh_regnskab->commit();
    $dbh_regnskab->do("SELECT pbs.bogfør_betalinger_fra_pbs (0);");
    $dbh_regnskab->commit();
    $dbh_regnskab->do("SELECT pbs.aftaleoplysninger_fra_pbs (0);");
    $dbh_regnskab->commit();
  };
  
  $dbh_regnskab->disconnect();
}

##################################################
# Log_data_fra_pbc in /home/lkp/OUTBOUND
##################################################
sub Log_data_fra_pbc {
   my $filename = shift;
   my $buffer = shift;
   my $pbsfilesid = shift;
   
   my $path = "/home/lkp/OUTBOUND";
   my $sinkpath = $path . "/" . $pbsfilesid . "-" . $filename; 
   
   sysopen( SINK, $sinkpath, O_WRONLY|O_TRUNC|O_CREAT, 0400) or die "Couldn´t open $sinkpath for writing: $!\n";
   syswrite( SINK, $buffer, length($buffer) ) or die "Couldn´t write to $sinkpath: $!\n";
   close SINK;
}


##################################################
# SendMail_data_fra_pbc with attachment
##################################################
sub SendMail_data_fra_pbc {
   my $filename = shift;
   my $senddata = shift;

   my $sender;
   my $subjecttext;

   my $data;
   my $seqnr = 0;
   my $crlf = "\r\n|\n";
   my $nettyp = 0;
   my $pbssys;
   my $pbsdat;
   foreach $data (split(/$crlf/,$senddata)) {
     if (($seqnr == 0) && ($data =~ /^PBCNET(..)(...)/)) {
       $nettyp = $1;
       $pbssys = $2;
     };
   
     if (($seqnr == 1) && ($nettyp == 30) && ($data =~ /^BS002........BS1.(...)/)) {
       $pbsdat = $1;
     };
   
     $seqnr++;
   };
   
   if ($nettyp == 30) {
     if ($pbsdat == 602) {
       $subjecttext = "PBS: Betalinger modtaget fra";
   	 }
   	 else {
       $subjecttext = "PBS: Data ($pbsdat) modtaget fra";
   	 };
   }
   else {
     $subjecttext = "PBS: Kvitering modtaget fra";
   };
   
   if ($pbssys eq 'OS1') {
     $subjecttext .= " OverførselsService";
   }
   else {
     $subjecttext .= " BetalingsService";
   };

   eval {
      ##$daemon->dolog("Notified by name: SendMail");
      
      $sender = new Mail::Sender {
         smtp => 'localhost',
         from => 'mha@puls3060.dk',
         on_errors => undef,
      };

      $sender->OpenMultipart({
         to => 'mha@hafsjold.dk',
         bcc => 'arkiv@puls3060.dk',
         subject => $subjecttext
      });
      
      $sender->Body;
      $sender->SendEnc(<<'*END*');
Vedhæftede datafile er modtaget fra PBC.

Mvh, puls3060-daemon
*END*
      
      $filename =~ s/\.([a-zA-Z0-9]+)$/_$1\.txt/;
      $sender->Part(
       {description => 'File sent til PBS',
        ctype => 'text/plain',
        encoding => 'Base64',
        disposition => 'attachment; filename="'.$filename.'"; type="txt file"',
        msg => $senddata,
       });
      
      $sender->Close;
   };
   if ($@) {
      return 0;
   }
   else {
      return 1;
   }
}


##################################################
# Data til PBC
##################################################
sub data_til_pbc {
  ##$daemon->dolog("Start new data_til_pbc");

  my $dbh_regnskab;
  my $sql_select;
  my $sth_select;
  my $sth_senddata;
  my $sql_sendqueue;
  my $sth_sendqueue;
  my $sql_sendqueue_update;
  my $sth_sendqueue_update;

  my $sql_update;
  my $sth_update;
  my $sql_select_sftp;
  my $sth_select_sftp;
  
  my $filename;
  my $path;
  my $transmittime = ctime();
  my $pbsfilesid;

  $dbh_regnskab = DBI->connect( "dbi:Pg:dbname=$db_regnskab;host=$dbhost", "pgsql", "", {RaiseError => 1, AutoCommit => 0} ) 
  				|| die "Database connection not made: $DBI::errstr";

  
  $sql_sendqueue = <<SQL;
  	SELECT pbsfilesid
  	FROM  pbs.tblsendqueue
  	WHERE send_to_pbc = false
  	  AND onhold = false;
SQL
  $sth_sendqueue = $dbh_regnskab->prepare( $sql_sendqueue );

  $sql_sendqueue_update = <<SQL;
  	UPDATE pbs.tblsendqueue SET
  	  send_to_pbc = true
  	WHERE pbsfilesid = ?;
SQL
  $sth_sendqueue_update = $dbh_regnskab->prepare( $sql_sendqueue_update );

  $sql_select = <<SQL;
  	SELECT data, length(data) AS datalen
   	FROM  pbs.tblpbsfile
   	WHERE pbsfilesid = ?
   	ORDER BY seqnr;
SQL
  $sth_select = $dbh_regnskab->prepare( $sql_select );

  $sql_select_sftp = <<SQL;
    SELECT 
      host,
      "user",
      outbound,
      inbound
    FROM
      pbs.tblsftp
    WHERE id = ?;
SQL
  $sth_select_sftp = $dbh_regnskab->prepare( $sql_select_sftp );

  $sql_update = <<SQL;
  	UPDATE pbs.tblpbsfiles set
  			type = ?,
  			path = ?,
  			filename = ?,
  			size = ?,
  			atime = CAST(? AS timestamp),
  			mtime = CAST(? AS timestamp),
  			perm = ?,
  			uid = ?,
  			gid = ?,
  			transmittime = CAST(? AS timestamp)
  	WHERE id = ?;
SQL
  $sth_update = $dbh_regnskab->prepare( $sql_update );

##$daemon->dolog("data_til_pbc Debug 0");

  $sth_sendqueue->execute();
  $sth_sendqueue->bind_columns( undef, \$pbsfilesid );
  while( $sth_sendqueue->fetch() ) {

    ## Add PBCNET Header og Trailer, samt generer filename
    $sth_senddata = $dbh_regnskab->prepare( "SELECT pbs.senddata ( ? )");
    $sth_senddata->bind_param( 1, $pbsfilesid, SQL_INTEGER );
    $sth_senddata->execute();
    $sth_senddata->bind_columns( undef, \$filename );
    $sth_senddata->fetch();

    ## Send data
    $sth_select->bind_param( 1, $pbsfilesid, SQL_INTEGER );
    $sth_select->execute();

    my( $data, $datalen );
    $sth_select->bind_columns( undef, \$data, \$datalen );
    ## skriv DATA
    my $senddata = "";;
    my $crlf = "\r\n";

    while( $sth_select->fetch() ) {
      $senddata .= $data . $crlf
    };

	my ($type, $size, $atime, $mtime, $perm, $uid, $gid );
    my @st = stat();
    $st[2] = ((8 << 12) + 0640); ## mode 
    $st[4] = 0;					 ## uid
    $st[5] = 1001;				 ## gid
    $st[7] = length($senddata);	 ## size
    $st[8] = time;  	 		 ## atime
    $st[9] = time;  	 		 ## mtime

    my $attrs = Net::SFTP::Foreign::Attributes->new_from_stat(@st);
    
	$size = $attrs->{'size'};
    $atime = ctime($attrs->{'atime'});
    $mtime = ctime($attrs->{'mtime'});
    $perm = ($attrs->{'perm'} & 0777);
    $type = (S_IFMT($attrs->{'perm'})) >> 12;
    $uid =  $attrs->{'uid'};
    $gid =  $attrs->{'gid'};
    
    $sth_select_sftp->bind_param( 1, $Profile_id, SQL_INTEGER );	## 1=PBS, 2=Test (HD03) 
    $sth_select_sftp->execute();

  	my ($host, $user, $outbound, $inbound);
    $sth_select_sftp->bind_columns( undef, \$host, \$user, \$outbound, \$inbound );
    $sth_select_sftp->fetch();

    ##$daemon->dolog("data_til_pbc Debug Net::SFTP::Foreign::Compat->new BEGIN");
    my $sftp = Net::SFTP::Foreign::Compat->new($host, user => $user);
    ##$daemon->dolog("data_til_pbc Debug Net::SFTP::Foreign::Compat->new END");

    my $flags = SSH2_FXF_CREAT | SSH2_FXF_WRITE;
    $path = $inbound;

    my $handle =  $sftp->do_open($path . "/" . $filename, $flags, $attrs);
    $sftp->do_write($handle, 0, $senddata);

	eval { Log_data_til_pbc( $filename, $senddata, $pbsfilesid ) };

    ## Opdater filestatus

    $size = $attrs->{'size'};
    $atime = ctime($attrs->{'atime'});
    $mtime = ctime($attrs->{'mtime'});
    $perm = ($attrs->{'perm'} & 0777);
    $type = (S_IFMT($attrs->{'perm'})) >> 12;
    $uid =  $attrs->{'uid'};
    $gid =  $attrs->{'gid'};

    $sth_update->bind_param(  1, $type, SQL_INTEGER );
    $sth_update->bind_param(  2, $path, SQL_VARCHAR );
    $sth_update->bind_param(  3, $filename, SQL_VARCHAR );
    $sth_update->bind_param(  4, "$size", SQL_INTEGER ); 
    $sth_update->bind_param(  5, "$atime", SQL_VARCHAR );
    $sth_update->bind_param(  6, "$mtime", SQL_VARCHAR );
    $sth_update->bind_param(  7, $perm, SQL_INTEGER );
    $sth_update->bind_param(  8, $uid, SQL_INTEGER );
    $sth_update->bind_param(  9, $gid, SQL_INTEGER );
    $sth_update->bind_param( 10, "$transmittime", SQL_VARCHAR );
    $sth_update->bind_param( 11, $pbsfilesid, SQL_INTEGER );
    $sth_update->execute();

    $sth_sendqueue_update->bind_param( 1, $pbsfilesid, SQL_INTEGER );
    $sth_sendqueue_update->execute();

    $dbh_regnskab->commit();

    SendMail_data_til_pbc($filename, $senddata);
    $sftp->do_close($handle);
  };

  eval { $sth_select_sftp->finish() };
  eval { $sth_senddata->finish() };
  eval { $sth_select->finish() };
  eval { $sth_update->finish() };
  eval { $sth_sendqueue->finish() };
  eval { $sth_sendqueue_update->finish() };
  
  $dbh_regnskab->disconnect();

}


##################################################
# Log_data_til_pbc in /home/lkp/SendPrev
##################################################
sub Log_data_til_pbc {
   my $filename = shift;
   my $buffer = shift;
   my $pbsfilesid = shift;
                 
   my $path = "/home/lkp/SendPrev";
   my $sinkpath = $path . "/" . $pbsfilesid . "-" . $filename; 
   
   sysopen( SINK, $sinkpath, O_WRONLY|O_TRUNC|O_CREAT, 0400) or die "Couldn´t open $sinkpath for writing: $!\n";
   syswrite( SINK, $buffer, length($buffer) ) or die "Couldn´t write to $sinkpath: $!\n";
   close SINK;
}



##################################################
# SendMail_data_til_pbc with attachment
##################################################
sub SendMail_data_til_pbc {
   my $filename = shift;
   my $senddata = shift;

   my $sender;
   eval {
      ##$daemon->dolog("Notified by name: SendMail");
      
      $sender = new Mail::Sender {
         smtp => 'localhost',
         from => 'mha@puls3060.dk',
         on_errors => undef,
      };

      $sender->OpenMultipart({
         to => 'mha@hafsjold.dk',
         bcc => 'arkiv@puls3060.dk',
         subject => 'Data sendt til PBC'
      });
      
      $sender->Body;
      $sender->SendEnc(<<'*END*');
Vedhæftede datafile er sendt til PBC.

Mvh, Mogens
*END*
      
      $filename =~ s/\.([a-zA-Z0-9]+)$/_$1\.txt/;
      $sender->Part(
       {description => 'File sent til PBS',
        ctype => 'text/plain',
        encoding => 'Base64',
        disposition => 'attachment; filename="'.$filename.'"; type="txt file"',
        msg => $senddata,
       });
      
      $sender->Close;
   };
   if ($@) {
      return 0;
   }
   else {
      return 1;
   }
}
