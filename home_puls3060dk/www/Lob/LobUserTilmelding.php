<?php
setlocale (LC_TIME, "da_DK.ISO8859-1");

require('clsLobUserTilmelding.php');
require 'smarty_puls3060dk.php';
$smarty = new Smarty_Puls3060dk;
$dbPuls3060 = new clsPuls3060SQL();
$dbRegnskab3060 = new clsRegnskab3060SQL();

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
	$tilmelding->setDB($dbPuls3060, $dbRegnskab3060);
	$tilmelding->setLobId($LobId);
	$tilmelding->getLink($_REQUEST['p0']);
	$tilmelding->getLob();

	if (!$tilmelding->linked) {
		$smarty->assign('tilmelding', $tilmelding);
		$smarty->display('Lob/LobUserTilmeldingForside.tpl');
	}
	else {
		$smarty->assign('tilmelding', $tilmelding);
		$smarty->display('Lob/LobUserTilmelding.tpl');
	}
	break;


	case "1":   //Process LobUserTilmeldingForside.tpl
	if (isset($_SESSION['tilmelding']))	{
		$tilmelding = $_SESSION['tilmelding'];
		if (isset($_POST['cmdOk'])) {
			$smarty->assign('tilmelding', $tilmelding);
			$smarty->display('Lob/LobUserTilmelding.tpl');
		}
	}
	break;


	case "2":   //Process LobUserTilmelding.tpl
	if (isset($_SESSION['tilmelding']))	{
		$tilmelding = $_SESSION['tilmelding'];
		if (isset($_POST['cmdBack'])) {
			$smarty->assign('tilmelding', $tilmelding);
			$smarty->display('Lob/LobUserTilmeldingForside.tpl');
		}
		if (isset($_POST['cmdOk'])) {
			$tilmelding->setDB($dbPuls3060, $dbRegnskab3060);
			$tilmelding->getFormDataLobUserTilmelding();

			if ($tilmelding->validateLobUserTilmelding()) {
				$tilmelding->setQuickpay();
				$tilmelding->getNextOrdernum();
				$smarty->assign('tilmelding', $tilmelding);
				$smarty->display('Lob/LobUserTilmeldingGodkend.tpl');
			}
			else {
				$smarty->assign('tilmelding', $tilmelding);
				$smarty->display('Lob/LobUserTilmelding.tpl');
			}
		}
	}
	break;


	case "3":   //Process LobUserTilmeldingGodkend.tpl
	if (isset($_SESSION['tilmelding']))	{
		$tilmelding = $_SESSION['tilmelding'];
		if (isset($_POST['cmdCancel'])) {
			$smarty->assign('tilmelding', $tilmelding);
			$smarty->display('Lob/LobUserTilmeldingForside.tpl');
		}
		elseif (isset($_POST['cmdBack'])) {
			$smarty->assign('tilmelding', $tilmelding);
			$smarty->display('Lob/LobUserTilmelding.tpl');
		}
		else {
			$tilmelding->setDB($dbPuls3060, $dbRegnskab3060);
			$smarty->assign('tilmelding', $tilmelding);
			$smarty->display('Lob/LobUserTilmeldingBetaling.tpl');
		}
	}
	break;


	case "4":   //Process LobUserTilmeldingBetaling.tpl
	if (isset($_SESSION['tilmelding']))	{
		$tilmelding = $_SESSION['tilmelding'];
		if (isset($_POST['cmdCancel'])) {
			$smarty->assign('tilmelding', $tilmelding);
			$smarty->display('Lob/LobUserTilmeldingForside.tpl');
		}
		if (isset($_POST['cmdBack'])) {
			$smarty->assign('tilmelding', $tilmelding);
			$smarty->display('Lob/LobUserTilmeldingGodkend.tpl');
		}

		if (isset($_POST['cmdOk'])) { // Dankort
			$tilmelding->setDB($dbPuls3060, $dbRegnskab3060);
			$tilmelding->getFormDataLobUserTilmeldingBetaling();

			if ($tilmelding->validateLobUserTilmeldingBetaling()) {
				$tilmelding->lobtilmelding();
				if ($tilmelding->mail_sent) {
					$smarty->assign('tilmelding', $tilmelding);
					$smarty->display('Lob/LobUserTilmeldingKvitering.tpl');
				}
				else {
					$tilmelding->validateLobUserTilmeldingBekraft('Dankort pageerror mail_sent');
					$tilmelding->ReverseAuthorize_Dankort();
					$smarty->assign('tilmelding', $tilmelding);
					$smarty->display('Lob/LobUserTilmeldingGodkend.tpl');
				}
			}
			else {
				$smarty->assign('tilmelding', $tilmelding);
				$smarty->display('Lob/LobUserTilmeldingBetaling.tpl');
			}
		}

		if (isset($_GET['Card'])) { // eDankort
			$tilmelding->setDB($dbPuls3060, $dbRegnskab3060);
			if (isset($_GET['Err'])) { // error
				$tilmelding->validateLobUserTilmeldingBekraft('eDankort pageerror');
				$smarty->assign('tilmelding', $tilmelding);
				$smarty->display('Lob/LobUserTilmeldingGodkend.tpl');
			}
			else {
				$tilmelding->StatusTrans_Dankort();
				$tilmelding->lobtilmelding();
				if ($tilmelding->mail_sent) {
					$smarty->assign('tilmelding', $tilmelding);
					$smarty->display('Lob/LobUserTilmeldingKvitering.tpl');
				}
				else {
					$tilmelding->validateLobUserTilmeldingBekraft('eDankort pageerror mail_sent');
					$tilmelding->ReverseAuthorize_Dankort();
					$smarty->assign('tilmelding', $tilmelding);
					$smarty->display('Lob/LobUserTilmeldingGodkend.tpl');
				}
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
