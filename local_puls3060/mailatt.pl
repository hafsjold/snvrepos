#!/usr/bin/perl

## Send data til PBS med tftp

use strict;
use Fcntl ':mode';
use Time::localtime;
use Mail::Sender;


my $senddata = "Dette er en tekst";
SendMailAtt($senddata);


##################################################
# SendMail with attachment
##################################################
sub SendMailAtt {
   ##my $self = shift;
   my $senddata = shift;

   ##print $senddata;
      
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
         subject => 'TEST AF Puls 3060 Daemon'
      });
      
      $sender->Body;
      $sender->SendEnc(<<'*END*');
Dette er en test

Mvh, Mogens
*END*
      
      $sender->Part(
       {description => 'Perl module Mail::Sender.pm',
        ctype => 'text/plain',
        encoding => 'Base64',
        disposition => 'attachment; filename="senddata.txt"; type="txt file"',
        msg => 'senddata',
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
