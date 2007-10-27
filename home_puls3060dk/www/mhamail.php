<?php
    require_once('email_message.php');
    
	$Subj = "Nyt Medlem æøåÆØÅ af Puls 3060";
	$Body  = 'Denne e-mail er genereret af siden "Indmeldelse" på www.puls3060.dk';
	$Body .= "\r\n";
	$Body .= "\r\n  Ref-nr......: ";
	$Body .= "\r\n  Fornavn.....: ";
	$Body .= "\r\n  Efternavn...: ";
	$Body .= "\r\n  Adresse.....: ";
	$Body .= "\r\n  Postnummer..: ";
	$Body .= "\r\n  By..........: ";
	$Body .= "\r\n  Telefon.....: ";
	$Body .= "\r\n  E-mail......: ";
	$Body .= "\r\n  Fødselsdato.: ";
	$Body .= "\r\n  Køn.........: ";
	$Body .= "\r\n  Kontingent..: ";
	$Body .= "\r\n  Fra.........: ";
	$Body .= "\r\n  Til.........: ";


    $email_message=new email_message_class;
    $email_message->SetEncodedEmailHeader("To","mha@hafsjold.dk","Mogens Hafsjold");
    $email_message->SetEncodedEmailHeader("From","mha@puls3060.dk","Puls 3060");
    $email_message->SetEncodedEmailHeader("Reply-To","mha@puls3060.dk","Puls 3060");
    $email_message->SetHeader("Sender","mha@puls3060.dk");
    $email_message->SetHeader("Return-Path","mha@puls3060.dk");
    
    $email_message->SetEncodedHeader("Subject",$Subj);
    $email_message->AddQuotedPrintableTextPart($email_message->WrapText($Body));
    
    $error=$email_message->Send();

?>