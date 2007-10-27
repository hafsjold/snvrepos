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

my $filename = 'abcdefghj';
my $senddata = 'datadatadata';
SendMail_data_til_pbc($filename, $senddata);



##################################################
# SendMail_data_til_pbc with attachment
##################################################
sub SendMail_data_til_pbc {
   my $filename = shift;
   my $senddata = shift;

   my $sender;
   eval {
      #$daemon -> dolog("Notified by name: SendMail");
      
      $sender = new Mail::Sender {
         smtp => 'hd06.hafsjold.dk',
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

