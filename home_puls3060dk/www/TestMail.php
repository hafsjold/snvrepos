<?
  $Subj = "Test e-mail";
  $Body  = 'Denne e-mail er genereret af siden "TestMail" på www.puls3060.dk';
  mail("administrator@buusjensen.dk", $Subj, $Body, "From: mha@puls3060.dk\nBcc: mha@hafsjold.dk\nReply-To: mha@puls3060.dk\nX-Mailer: PHP", "-f mha@puls3060.dk");
?>
