<?php
setlocale (LC_TIME, "da_DK.ISO8859-1");

require('clsLobAdmTilmelding.php');
require 'smarty_puls3060dk.php';
$smarty = new Smarty_Puls3060dk;
$db = new clsPuls3060SQL();

if (isset($_REQUEST['DoWhat'])) $DoWhat=$_REQUEST['DoWhat'];
else $DoWhat = 0;

if (isset($_REQUEST['LobId'])) $LobId=$_REQUEST['LobId'];
else $LobId = 0;

session_start();


switch ($DoWhat) {
	case "0":   //Process Init
	if (session_is_registered ('tilmelding')) {
		session_unregister ('tilmelding');
	}
	$tilmelding = new clsLobAdmTilmelding();
	$tilmelding->setDB($db);
	$tilmelding->setLobId($LobId);
	$tilmelding->getLob();

	$smarty->assign('tilmelding', $tilmelding);
	$smarty->display('Lob/LobAdmTilmelding.tpl');

	break;

	case "2":   //Process LobAdmTilmelding.tpl
	if (isset($_SESSION['tilmelding']))	{
		$tilmelding = $_SESSION['tilmelding'];
		if (isset($_POST['cmdOk'])) {
			$tilmelding->setDB($db);
			$tilmelding->getFormData();

			if ($tilmelding->validate()) {
				$smarty->assign('tilmelding', $tilmelding);
				$smarty->display('Lob/LobAdmTilmeldingBekraft.tpl');
			}
			else {
				$smarty->assign('tilmelding', $tilmelding);
				$smarty->display('Lob/LobAdmTilmelding.tpl');
			}
		}
	}
	break;


	case "3":   //Process LobAdmTilmeldingBekraft.tpl
	if (isset($_SESSION['tilmelding']))	{
		$tilmelding = $_SESSION['tilmelding'];
		if (isset($_POST['cmdCancel'])) {
			session_unregister ('tilmelding');
			$tilmelding = new clsLobAdmTilmelding();
			$tilmelding->setDB($db);
			$tilmelding->getLob();
			$smarty->assign('tilmelding', $tilmelding);
			$smarty->display('Lob/LobAdmTilmelding.tpl');
		}
		if (isset($_POST['cmdBack'])) {
			$smarty->assign('tilmelding', $tilmelding);
			$smarty->display('Lob/LobAdmTilmelding.tpl');
		}
		if (isset($_POST['cmdOk'])) {
			$tilmelding->setDB($db);
			$tilmelding->LobAdmTilmelding();
			if ($tilmelding->mail_sent) {
				$smarty->assign('tilmelding', $tilmelding);
				$smarty->display('Lob/LobAdmTilmeldingKvitering.tpl');
			}
			else {
				$smarty->assign('tilmelding', $tilmelding);
				$smarty->display('Lob/LobAdmTilmeldingFejl.tpl');
			}
		}
	}
	break;

	default:
		throw new Exception("Dowhat: $DoWhat is not valid action");
		break;
}

$_SESSION['tilmelding'] = $tilmelding;
?>
