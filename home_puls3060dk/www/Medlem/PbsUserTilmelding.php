<?php
setlocale (LC_TIME, "da_DK.ISO8859-1");

require('clsPbsUserTilmelding.php');
require 'smarty_puls3060dk.php';
$smarty = new Smarty_Puls3060dk;
$db = new clsPuls3060SQL();

$tilmelding = new clsPbsUserTilmelding();
$tilmelding->setDB($db);
$tilmelding->getLink($_REQUEST['p0']);

if ($tilmelding->linked) {
	$smarty->assign('tilmelding', $tilmelding);
	$smarty->display('Medlem/PbsUserTilmelding.tpl');
}
else {
	$smarty->assign('tilmelding', $tilmelding);
	$smarty->display('Medlem/PbsUserTilmeldingFejl.tpl');
}
?>
