#!/usr/bin/perl -w

use strict;
use DBI;
use DBD::Pg;
use Mail::Sender;
use Fcntl;
use POSIX ":sys_wait_h";
use daemon3060;

##### make your settings here:
# databases
my $db_puls = "puls3060";
my $db_regnskab = "regnskab3060";

# start daemon
my $daemon = new daemon3060; 
# or if we want to change the daemon name
# my $daemon = new daemon "timeserver";

# the following lines are the defaults, so we don't need them
# $daemon -> pidfile ("/var/run/puls3060-test-daemon.pid");
# $daemon -> logto ('//syslog);

if ($daemon -> check_running()) {
   $daemon -> kill; # kill daemon if running
} else {
   # otherwise start it (if any arguments are given, run in debug mode)
   unless ($ARGV[0]) {
      $daemon -> detach;
   } else {
      $daemon -> debug;
   }
   $daemon -> dolog("started puls3060-test-daemon");

   my $kid;
   
   ##$daemon->forkchild(\&Timeout_Test);
   $daemon->forkchild(\&Listen_Test);
   $daemon->forkchild(\&SendMailAtt);
   
   
   while(1) { 
     my $retval =  select( undef, undef, undef, 30);
     $daemon -> dolog("Main loop signal: $retval, err: $!");
     
     foreach my $pid ( keys %{$daemon -> {'child_properties'}} ) {
       ##next if defined $daemon->{'child_properties'}->{$pid}->{'EXIT_STATUS'};
       my $status = $daemon->{'child_properties'}->{$pid}->{'EXIT_STATUS'};
       ##$daemon -> dolog("EXIT_STATUS for pid $pid: $status ");
       my $func = $daemon->{'child_properties'}->{$pid}->{'func'};
       ##$daemon -> dolog("func for pid $pid: $func ");

       if (defined $daemon->{'child_properties'}->{$pid}->{'EXIT_STATUS'}){
          $daemon->forkchild($func);
          delete $daemon->{'child_properties'}->{$pid};
       }

     }
   };
   
   $daemon -> dolog("puls3060-test-daemon done its work");
};

# end daemon (should not be reached)
exit 0;

##################################################
# Timeout_Test
##################################################
sub Timeout_Test {
  $daemon -> dolog("Start new Timeout_Test");
   select( undef, undef, undef, 60);
}

##################################################
# Listen_TestListen_Test
##################################################
sub Listen_Test {
  my $dbh;
  my $fd;
  my $rin;
  my $rout;
  my $retval;
  my $notify_r;

  $daemon -> dolog("Start new Listen_Test");

  $dbh = DBI->connect("dbi:Pg:dbname=$db_regnskab", "pgsql", "",
   	     {RaiseError => 1, AutoCommit => 1});
  $fd=$dbh->func('getfd');
  vec($rin,$fd,1) = 1;
  $dbh->do('LISTEN test');

  $retval = select( $rout=$rin, undef, undef, undef);
  
  if ($retval > 0) {
    $notify_r = $dbh->func('pg_notifies'); 
    $daemon -> dolog("Listen_Test: Notified by name: ".$notify_r->[0]." from PID: ".$notify_r->[1]);
  }elsif($retval == 0){
    $daemon -> dolog("Listen_Test: Notified by name: Timeout");
  }else{
    $daemon -> dolog("Listen_Test: Notified by signal: $retval, err: $!");
  };

  eval {
    $dbh->disconnect();
  };
}

##################################################
# PBS Opkrævning
##################################################
sub PbsFakturaForslag {
   # connection
   my $dbh_puls;
   my $dbh_regnskab;

   $dbh_puls = DBI->connect("dbi:Pg:dbname=$db_puls", "pgsql", "",
      	       {RaiseError => 1, AutoCommit => 1}
            );

   $dbh_regnskab = DBI->connect("dbi:Pg:dbname=$db_regnskab", "pgsql", "",
      	       {RaiseError => 1, AutoCommit => 1}
            );

   $daemon -> dolog("Notified by name: PbsFakturaForslag");


   $dbh_regnskab->do( "INSERT INTO queue.tblprocessnytmedlem 
       (processtablename, nytmedlemid) 
       VALUES('queue.tblprocessnytmedlem', 1999);");

   $dbh_puls->disconnect();
   $dbh_regnskab->disconnect();
}

##################################################
# SendMail
##################################################
sub SendMail {
   my $sender;
   eval {
      $daemon -> dolog("Notified by name: SendMail");
      $sender = new Mail::Sender {
         smtp => 'hd11.hafsjold.dk',
         from => 'mha@puls3060.dk',
         on_errors => undef,
      };

      $sender->Open({
         to => 'mha@hafsjold.dk',
         bcc => 'arkiv@puls3060.dk',
        subject => 'TEST AF Puls 3060 Daemon'
      });

      $sender->SendLineEnc("Dette er en test af Puls 3060 Daemon.");

      $sender->SendLineEnc("\nMvh\nPuls 3060 Daemon");

      $sender->Close();
      select( undef, undef, undef, 15);
   };
   if ($@) {
      return 0;
   }
   else {
      return 1;
   }
}

##################################################
# SendMail with attachment
##################################################
sub SendMailAtt {
   my $sender;
   eval {
      $daemon -> dolog("Notified by name: SendMail");
      
      $sender = new Mail::Sender {
         smtp => 'hd11.hafsjold.dk',
         from => 'mha@puls3060.dk',
         on_errors => undef,
      };

      $sender->OpenMultipart({
         to => 'mha@hafsjold.dk',
         bcc => 'arkiv@puls3060.dk',
         subject => 'TEST AF Puls 3060 Daemon'
      });
      
      $sender->Body;
      $sender->SendEnc(<<'*END*');
Here is a new module Mail::Sender.
It provides an object based interface to sending SMTP mails.
It uses a direct socket connection, so it doesn't need any
additional program.

Mvh, Mogens
*END*
      
      $sender->Attach(
       {description => 'Perl module Mail::Sender.pm',
        ctype => 'application/x-zip-encoded',
        encoding => 'Base64',
        disposition => 'attachment; filename="puls3060-test-daemon.zip"; type="ZIP archive"',
        file => 'puls3060-test-daemon.zip'
       });
      
      $sender->Close;
      
      select( undef, undef, undef, 60);
   };
   if ($@) {
      return 0;
   }
   else {
      return 1;
   }
}
