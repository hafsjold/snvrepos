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
use Net::SFTP;
use Net::SFTP::Constants qw( :flags );
use DBI qw(:sql_types);
use Mail::Sender;
use daemon3060;

##### make your settings here:
# databases
my $db_puls = "puls3060";
my $db_regnskab = "regnskab3060";

data_fra_pbc();
exit 0;


##################################################
# Data fra PBC
##################################################
sub data_fra_pbc {
  $daemon -> dolog("Start new data_fra_pbc");

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

  $dbh_regnskab = DBI->connect( "dbi:Pg:dbname=$db_regnskab", "pgsql", "", {RaiseError => 1, AutoCommit => 0} ) 
  				|| die "Database connection not made: $DBI::errstr";

  $daemon -> dolog("data_fra_pbc Debug 0");
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
   			?, 
   			?, 
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
      "password"
    FROM
      pbs.tblsftp
    WHERE id = ?;
SQL
  $sth_select_sftp = $dbh_regnskab->prepare( $sql_select_sftp );
  $sth_select_sftp->bind_param( 1, 1, SQL_INTEGER );	## 1=PBS, 2=Test (HD03) 
  $sth_select_sftp->execute();

  my ($host, $user, $password);

  $sth_select_sftp->bind_columns( undef, \$host, \$user, \$password );
  $sth_select_sftp->fetch();

  my $sftp = Net::SFTP->new($host, user => $user, password => $password, debug => 1, privileged => 0 );

  $sftp->debug("DEBUG-1");
  $daemon -> dolog("data_fra_pbc Debug 1");

  $path = "OUTBOUND";

  my @outbound_files  = $sftp->ls($path);

  $sftp->debug("DEBUG-2");
  $daemon -> dolog("data_fra_pbc Debug 2");

  foreach my $item (@outbound_files){
    $sftp->debug("DEBUG-3");
    $daemon -> dolog("data_fra_pbc Debug 3");
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

  $sql_select_file = <<SQL;
    SELECT 
      id,
      "path",
      filename,
      size
    FROM
      pbs.tblpbsfiles
    WHERE "path" = 'OUTBOUND'
      AND transmittime IS NULL;
SQL
  $sth_select_file = $dbh_regnskab->prepare( $sql_select_file );
  $sth_select_file->execute();
  my $pbsfilesid;
  $sth_select_file->bind_columns( undef, \$pbsfilesid, \$path, \$filename, \$size );
  while( $sth_select_file->fetch() ) {
    my $flags = SSH2_FXF_READ;
    my $handle =  $sftp->do_open($path . "/" . $filename, $flags);
    my($buffer, $status) = $sftp->do_read($handle, 0, $size);
    $sftp->do_close($handle);
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
  };

  $dbh_regnskab->commit();

  $sth_select_sftp->finish();
  $sth_insert_linie->finish();
  $sth_select_file->finish();
  $sth_insert_data->finish();
  $sth_update->finish();
  
  if ($number_of_files > 0) {
    $dbh_regnskab->do("SELECT pbs.betalinger_fra_pbs (0);");
    $dbh_regnskab->commit();
    $dbh_regnskab->do("SELECT pbs.bogfør_betalinger_fra_pbs (0);");
    $dbh_regnskab->commit();
    $dbh_regnskab->do("SELECT pbs.aftaleoplysninger_fra_pbs (0);");
    $dbh_regnskab->commit();
  };
  
  $dbh_regnskab->disconnect();
}
