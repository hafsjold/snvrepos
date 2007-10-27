#!/usr/bin/perl -w
use strict;
use Mail::Sender;
use MIME::QuotedPrint;

##SELECT pbs.mail_overforsel('test message', 'wnavn_medlem', 'mha@hafsjold.dk', '2007-10-11'::date);

my $msg = 'test message';
my $navn_medlem = 'wnavn_medlem';
my $navn_mail_adresse = 'mha@hafsjold.dk';
my $betalingsdato = '2007-10-11';

my $subject = 'Bankoverførel ' . $betalingsdato . ' fra Puls 3060';
##my $subject = encode_qp($subject1, "\015\012");

##my $subject = 'Bankoverfoersel ' . $betalingsdato . ' fra Puls 3060 ';
my $from_mail_adresse = 'mha@hafsjold.dk';
my $til_navn = $navn_medlem . '\n\n';
my $underskrift_navn = 'Mogens Hafsjold';
my $underskrift = '\n\nMed venlig hilsen\nPuls 3060\n\n' . $underskrift_navn . '\nKasserer\n\nTelefon\: 40 13 35 40\nE-mail\: kasserer\@puls3060\.dk';


my $sender;
my $mail;

eval '$mail = "' . $til_navn . $msg . $underskrift . '"';

eval {

   $sender = new Mail::Sender {
      smtp => 'localhost',
      from => $from_mail_adresse,
	  subject => $subject,
      charset => 'UTF-8',
	  encoding => 'Quoted-print',	
      on_errors => undef,
   };

   ##$sender->{'subject'} = "Nyt subject";
   my $mh1 = $sender->{'subject'};
   
   $sender->Open({
      to => $navn_mail_adresse,
      bcc => 'arkiv@puls3060.dk',
 ##     subject => $subject,
      charset => 'UTF-8',
	  encoding => 'Quoted-print'	
   });
   

   
   $sender->SendEnc($mail);
   $sender->Close;
};
