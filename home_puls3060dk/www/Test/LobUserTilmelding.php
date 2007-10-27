<?php
setlocale (LC_TIME, "da_DK.ISO8859-1");

require('clsLobUserTilmelding.php');
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
	$tilmelding = new clsLobUserTilmelding();
	$tilmelding->setDB($db);
	$tilmelding->setLobId($LobId);
	$tilmelding->getLink($_REQUEST['p0']);
	$tilmelding->getLob();

	if (!$tilmelding->linked) {
		$smarty->assign('tilmelding', $tilmelding);
		$smarty->display('Test/LobUserTilmeldingForside.tpl');
	}
	else {
		$smarty->assign('tilmelding', $tilmelding);
		$smarty->display('Test/LobUserTilmelding.tpl');
	}
	break;


	case "1":   //Process LobUserTilmeldingForside.tpl
	if (isset($_SESSION['tilmelding']))	{
		$tilmelding = $_SESSION['tilmelding'];
		if (isset($_POST['cmdOk'])) {
			$smarty->assign('tilmelding', $tilmelding);
			$smarty->display('Test/LobUserTilmelding.tpl');
		}
	}
	break;


	case "2":   //Process LobUserTilmelding.tpl
	if (isset($_SESSION['tilmelding']))	{
		$tilmelding = $_SESSION['tilmelding'];
		if (isset($_POST['cmdBack'])) {
			$smarty->assign('tilmelding', $tilmelding);
			$smarty->display('Test/LobUserTilmeldingForside.tpl');
		}
		if (isset($_POST['cmdOk'])) {
			$tilmelding->setDB($db);
			$tilmelding->getFormDataLobUserTilmelding();

			if ($tilmelding->validateLobUserTilmelding()) {
				$smarty->assign('tilmelding', $tilmelding);
				$smarty->display('Test/LobUserTilmeldingBetaling.tpl');
			}
			else {
				$smarty->assign('tilmelding', $tilmelding);
				$smarty->display('Test/LobUserTilmelding.tpl');
			}
		}
	}
	break;


	case "3":   //Process LobUserTilmeldingBetaling.tpl
	if (isset($_SESSION['tilmelding']))	{
		$tilmelding = $_SESSION['tilmelding'];
		if (isset($_POST['cmdCancel'])) {
			$smarty->assign('tilmelding', $tilmelding);
			$smarty->display('Test/LobUserTilmeldingForside.tpl');
		}
		if (isset($_POST['cmdBack'])) {
			$smarty->assign('tilmelding', $tilmelding);
			$smarty->display('Test/LobUserTilmelding.tpl');
		}
		if (isset($_POST['cmdOk'])) {
			$tilmelding->setDB($db);
			$tilmelding->getFormDataLobUserTilmeldingBetaling();

			if ($tilmelding->validateLobUserTilmeldingBetaling()) {
				$smarty->assign('tilmelding', $tilmelding);
				$smarty->display('Test/LobUserTilmeldingBekraft.tpl');
			}
			else {
				$smarty->assign('tilmelding', $tilmelding);
				$smarty->display('Test/LobUserTilmeldingBetaling.tpl');
			}
		}
	}
	break;


	case "4":   //Process LobUserTilmeldingBekraft.tpl
	if (isset($_SESSION['tilmelding']))	{
		$tilmelding = $_SESSION['tilmelding'];
		if (isset($_POST['cmdCancel'])) {
			$smarty->assign('tilmelding', $tilmelding);
			$smarty->display('Test/LobUserTilmeldingForside.tpl');
		}
		if (isset($_POST['cmdBack'])) {
			$smarty->assign('tilmelding', $tilmelding);
			$smarty->display('Test/LobUserTilmeldingBetaling.tpl');
		}
		if (isset($_POST['cmdOk'])) {
			$tilmelding->setDB($db);
			if ($tilmelding->Authorize_Dankort()) {
				$tilmelding->lobtilmelding();
				if ($tilmelding->mail_sent) {
					$smarty->assign('tilmelding', $tilmelding);
					$smarty->display('Test/LobUserTilmeldingKvitering.tpl');
				}
				else {
					$tilmelding->ReverseAuthorize_Dankort();
					$smarty->assign('tilmelding', $tilmelding);
					$smarty->display('Test/LobUserTilmeldingBekraft.tpl');
				}
			}
			else {
				$smarty->assign('tilmelding', $tilmelding);
				$smarty->display('Test/LobUserTilmeldingBekraft.tpl');
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
