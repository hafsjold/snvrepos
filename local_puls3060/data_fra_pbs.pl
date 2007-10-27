#!/usr/bin/perl

## use strict;
use Fcntl ':mode';
use DBI;
use DBD::Pg;
use DBI qw(:sql_types);
use Net::SFTP;
use Net::SFTP::Constants qw( :flags );
use Time::localtime;
use Mail::Sender;

my $db_regnskab = "regnskab3060";

my $dbh_regnskab;
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

$path = "OUTBOUND";
my $files;
@files  = $sftp->ls($path);

$sftp->debug("DEBUG-2");

foreach my $item (@files){
  $sftp->debug("DEBUG-3");
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

exit(1);

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

my $sth_pbsnet_update = $dbh_regnskab->prepare( "SELECT pbs.pbsnet_update (?)" );

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
my ($pbsfilesid, $size);
$sth_select_file->bind_columns( undef, \$pbsfilesid, \$path, \$filename, \$size );
while( $sth_select_file->fetch() ) {
  my $flags = SSH2_FXF_READ;
  my $handle =  $sftp->do_open($path . "/" . $filename, $flags);
  my($buffer, $status) = $sftp->do_read($handle, 0, $size);
  $sftp->do_close($handle);
  SendMailAtt($filename, $buffer);
  my $data;
  my $seqnr = 1;
  my $crlf = "\r\n|\n";
  foreach $data (split(/$crlf/,$buffer)) {
    $sth_insert_data->bind_param(  1, $pbsfilesid, SQL_INTEGER );
    $sth_insert_data->bind_param(  2, $seqnr++, SQL_INTEGER );
    $sth_insert_data->bind_param(  3, $data, SQL_VARCHAR );
    $sth_insert_data->execute();
  };
  $sth_update->bind_param( 1, $pbsfilesid, SQL_INTEGER );
  $sth_update->execute();
  $sth_pbsnet_update->bind_param( 1, $pbsfilesid, SQL_INTEGER );
  $sth_pbsnet_update->execute();
};

$dbh_regnskab->commit();

$sth_select_sftp->finish();
$sth_insert_linie->finish();
$sth_select_file->finish();
$sth_insert_data->finish();
$sth_update->finish();
$sth_pbsnet_update->finish();
$dbh_regnskab->disconnect();

##################################################
# SendMail with attachment
##################################################
sub SendMailAtt {
   ##my $self = shift;
   my $filename = shift;
   my $senddata = shift;

   my $sender;
   eval {
      ##$daemon -> dolog("Notified by name: SendMail");
      
      $sender = new Mail::Sender {
         smtp => 'hd06.hafsjold.dk',
         from => 'mha@puls3060.dk',
         on_errors => undef,
      };

      $sender->OpenMultipart({
         to => 'mha@hafsjold.dk',
         bcc => 'arkiv@puls3060.dk',
         subject => 'Data modtaget fra PBC'
      });
      
      $sender->Body;
      $sender->SendEnc(<<'*END*');
Vedhæftede datafile er modtaget fra PBC.

Mvh, Mogens
*END*
      
      $filename =~ s/\.([a-zA-Z0-9]+)$/_\1\.txt/;
      $sender->Part(
       {description => 'File sent til PBS',
        ctype => 'text/plain',
        encoding => 'Base64',
        disposition => 'attachment; filename="'.$filename.'"; type="txt file"',
        msg => $senddata,
       });
      
      $sender->Close;
      
      ##select( undef, undef, undef, 60);
   };
   if ($@) {
      return 0;
   }
   else {
      return 1;
   }
}
