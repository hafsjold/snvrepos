#!/usr/bin/perl

## Send data til PBS med tftp

use strict;
use Fcntl ':mode';
use Time::localtime;
use Net::SFTP;
use DBI;
use DBD::Pg;
use DBI qw(:sql_types);
use Net::SFTP::Constants qw( :flags );
use Mail::Sender;


my $db_regnskab = "regnskab3060";
data_til_pbc();
##################################################
# Data til PBC
##################################################
sub data_til_pbc {
  my $dbh_regnskab;
  my $sql_select;
  my $sth_select;
  my $sth_senddata;
  my $sql_sendqueue;
  my $sth_sendqueue;
  my $sql_update;
  my $sth_update;
  my $sql_select_sftp;
  my $sth_select_sftp;
  
  my $filename;
  my $path;
  my $transmittime = ctime();
  my $pbsfilesid;

  $dbh_regnskab = DBI->connect( "dbi:Pg:dbname=$db_regnskab", "pgsql", "", {RaiseError => 1, AutoCommit => 0} ) 
  				|| die "Database connection not made: $DBI::errstr";

  
  $sql_sendqueue = <<SQL;
  	SELECT pbsfilesid
  	FROM  pbs.tblsendqueue
  	WHERE send_to_pbc = false;
SQL
  $sth_sendqueue = $dbh_regnskab->prepare( $sql_sendqueue );

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
      "password"
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
  			atime = ?,
  			mtime = ?,
  			perm = ?,
  			uid = ?,
  			gid = ?,
  			transmittime = ?
  	WHERE id = ?;
SQL
  $sth_update = $dbh_regnskab->prepare( $sql_update );


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

    $sth_select_sftp->bind_param( 1, 2, SQL_INTEGER );	## 1=PBS, 2=Test (HD03) 
    $sth_select_sftp->execute();

    my ($host, $user, $password);

    $sth_select_sftp->bind_columns( undef, \$host, \$user, \$password );
    $sth_select_sftp->fetch();

    my $sftp = Net::SFTP->new($host, user => $user, password => $password, debug => 1, privileged => 0 );

    my $flags = SSH2_FXF_CREAT | SSH2_FXF_WRITE;
    $path = "INBOUND";
    my $handle =  $sftp->do_open($path . "/" . $filename, $flags);
    $sftp->do_write($handle, 0, $senddata);

    ## Opdater filestatus
    my ($type, $size, $atime, $mtime, $perm, $uid, $gid );

    my $attrs = $sftp->do_fstat($handle);
     
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

    $dbh_regnskab->commit();

    SendMail_data_til_pbc($filename, $senddata);
    $sftp->do_close($handle);
  };

  $sth_select_sftp->finish();
  $sth_senddata->finish();
  $sth_select->finish();
  $sth_update->finish();
  $sth_sendqueue->finish();
  
  $dbh_regnskab->disconnect();
}

##################################################
# SendMail_data_til_pbc with attachment
##################################################
sub SendMail_data_til_pbc {
   my $filename = shift;
   my $senddata = shift;

   my $sender;
   eval {
      ##$daemon -> dolog("Notified by name: SendMail");
      
      $sender = new Mail::Sender {
         smtp => 'hd11.hafsjold.dk',
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
