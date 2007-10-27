<?
include_once("vcard.php");  

$v = new vCard();

$v->setName("Hafsjold","Mogens", "", "");
$v->setBirthday("1946-06-02");
$v->setAddress("", "", "Nørremarken 31", "Espergærde", "", "3060", "Denmark");

$v->setPhoneNumber("+45 49 13 35 40", "HOME;VOICE");
$v->setPhoneNumber("+45 40 13 35 40", "PREF;CELL;VOICE");
$v->setPhoneNumber("+45 49 17 05 44", "WORK;VOICE");
$v->setPhoneNumber("+45 49 17 05 77", "WORK;FAX");

$v->setEmail("mha@hafsjold.dk");

$v->setURL("http://www.puls3060.dk", "WORK");
//$v->setNote("You can take some notes here.\r\nMultiple lines are supported via \\r\\n.");

$output = $v->getVCard();
$filename = $v->getFileName();

Header("Content-Disposition: attachment; filename=$filename");
Header("Content-Length: ".strlen($output));
Header("Connection: close");
Header("Content-Type: text/x-vCard; name=$filename");

echo $output;
?>