<?php
// http://www.puls3060.dk/kalmnu/Kalender/AktUserTilmelding.php?AktId=388

setlocale (LC_TIME, "da_DK.ISO8859-1");

require('clsAktUserTilmelding.php');
require 'smarty_puls3060dk.php';
$smarty = new Smarty_Puls3060dk;
$dbPuls3060 = new clsPuls3060SQL();
$dbRegnskab3060 = new clsRegnskab3060SQL();

if (isset($_REQUEST['DoWhat'])) $DoWhat=$_REQUEST['DoWhat'];
else $DoWhat = 0;

if (isset($_REQUEST['AktId'])) $AktId=$_REQUEST['AktId'];
else $AktId = 0;

session_start();


switch ($DoWhat) {
	case "0":   //Process Init
	if (session_is_registered ('tilmelding')) {
		session_unregister ('tilmelding');
	}
	$tilmelding = new clsAktUserTilmelding();
	$tilmelding->setDB($dbPuls3060, $dbRegnskab3060);
	$tilmelding->setAktId($AktId);
	$tilmelding->getLink($_REQUEST['p0']);
	$tilmelding->getAkt();

	if ($tilmelding->tilmelding_er_slut == "f") {
		if ($tilmelding->aktafgift > 0) {
			if (!$tilmelding->linked) {
				$smarty->assign('tilmelding', $tilmelding);
				$smarty->display('Kalender/AktUserTilmeldingForside1.tpl');
			}
			else {
				$smarty->assign('tilmelding', $tilmelding);
				$smarty->display('Kalender/AktUserTilmelding1.tpl');
			}
		}
		else {
			if (!$tilmelding->linked) {
				$smarty->assign('tilmelding', $tilmelding);
				$smarty->display('Kalender/AktUserTilmeldingForside2.tpl');
			}
			else {
				$smarty->assign('tilmelding', $tilmelding);
				$smarty->display('Kalender/AktUserTilmelding2.tpl');
			}
		}
	}
	break;


	case "11":   //Process AktUserTilmeldingForside1.tpl
	if (isset($_SESSION['tilmelding']))	{
		$tilmelding = $_SESSION['tilmelding'];
		if (isset($_POST['cmdOk'])) {
			$smarty->assign('tilmelding', $tilmelding);
			$smarty->display('Kalender/AktUserTilmelding1.tpl');
		}
	}
	break;


	case "12":   //Process AktUserTilmelding1.tpl
	if (isset($_SESSION['tilmelding']))	{
		$tilmelding = $_SESSION['tilmelding'];
		if (isset($_POST['cmdBack'])) {
			$smarty->assign('tilmelding', $tilmelding);
			$smarty->display('Kalender/AktUserTilmeldingForside1.tpl');
		}
		if (isset($_POST['cmdOk'])) {
			$tilmelding->setDB($dbPuls3060, $dbRegnskab3060);
			$tilmelding->getFormDataAktUserTilmelding();

			if ($tilmelding->validateAktUserTilmelding()) {
				$tilmelding->setQuickpay();
				$tilmelding->getNextOrdernum();
				$smarty->assign('tilmelding', $tilmelding);
				$smarty->display('Kalender/AktUserTilmeldingGodkend1.tpl');
			}
			else {
				$smarty->assign('tilmelding', $tilmelding);
				$smarty->display('Kalender/AktUserTilmelding1.tpl');
			}
		}
	}
	break;


	case "13":   //Process AktUserTilmeldingGodkend1.tpl
	if (isset($_SESSION['tilmelding']))	{
		$tilmelding = $_SESSION['tilmelding'];
		if (isset($_POST['cmdCancel'])) {
			$smarty->assign('tilmelding', $tilmelding);
			$smarty->display('Kalender/AktUserTilmeldingForside1.tpl');
		}
		elseif (isset($_POST['cmdBack'])) {
			$smarty->assign('tilmelding', $tilmelding);
			$smarty->display('Kalender/AktUserTilmelding1.tpl');
		}
		else {
			$tilmelding->setDB($dbPuls3060, $dbRegnskab3060);
			$smarty->assign('tilmelding', $tilmelding);
			$smarty->display('Kalender/AktUserTilmeldingBetaling1.tpl');
		}
	}
	break;


	case "14":   //Process AktUserTilmeldingBetaling1.tpl
	if (isset($_SESSION['tilmelding']))	{
		$tilmelding = $_SESSION['tilmelding'];
		if (isset($_POST['cmdCancel'])) {
			$smarty->assign('tilmelding', $tilmelding);
			$smarty->display('Kalender/AktUserTilmeldingForside1.tpl');
		}
		if (isset($_POST['cmdBack'])) {
			$smarty->assign('tilmelding', $tilmelding);
			$smarty->display('Kalender/AktUserTilmeldingGodkend1.tpl');
		}

		if (isset($_POST['cmdOk'])) { // Dankort
			$tilmelding->setDB($dbPuls3060, $dbRegnskab3060);
			$tilmelding->getFormDataAktUserTilmeldingBetaling();

			if ($tilmelding->validateAktUserTilmeldingBetaling()) {
				$tilmelding->akttilmelding(1);
				if ($tilmelding->mail_sent) {
					$smarty->assign('tilmelding', $tilmelding);
					$smarty->display('Kalender/AktUserTilmeldingKvitering1.tpl');
				}
				else {
					$tilmelding->validateAktUserTilmeldingBekraft('Dankort pageerror mail_sent');
					$tilmelding->ReverseAuthorize_Dankort();
					$smarty->assign('tilmelding', $tilmelding);
					$smarty->display('Kalender/AktUserTilmeldingGodkend1.tpl');
				}
			}
			else {
				$smarty->assign('tilmelding', $tilmelding);
				$smarty->display('Kalender/AktUserTilmeldingBetaling1.tpl');
			}
		}

		if (isset($_GET['Card'])) { // eDankort
			$tilmelding->setDB($dbPuls3060, $dbRegnskab3060);
			if (isset($_GET['Err'])) { // error
				$tilmelding->validateAktUserTilmeldingBekraft('eDankort pageerror');
				$smarty->assign('tilmelding', $tilmelding);
				$smarty->display('Kalender/AktUserTilmeldingGodkend1.tpl');
			}
			else {
				$tilmelding->StatusTrans_Dankort();
				$tilmelding->akttilmelding(1);
				if ($tilmelding->mail_sent) {
					$smarty->assign('tilmelding', $tilmelding);
					$smarty->display('Kalender/AktUserTilmeldingKvitering1.tpl');
				}
				else {
					$tilmelding->validateAktUserTilmeldingBekraft('eDankort pageerror mail_sent');
					$tilmelding->ReverseAuthorize_Dankort();
					$smarty->assign('tilmelding', $tilmelding);
					$smarty->display('Kalender/AktUserTilmeldingGodkend1.tpl');
				}
			}
		}

	}
	break;


	case "21":   //Process AktUserTilmeldingForside2.tpl
	if (isset($_SESSION['tilmelding']))	{
		$tilmelding = $_SESSION['tilmelding'];
		if (isset($_POST['cmdOk'])) {
			$smarty->assign('tilmelding', $tilmelding);
			$smarty->display('Kalender/AktUserTilmelding2.tpl');
		}
	}
	break;


	case "22":   //Process AktUserTilmelding2.tpl
	if (isset($_SESSION['tilmelding']))	{
		$tilmelding = $_SESSION['tilmelding'];
		if (isset($_POST['cmdBack'])) {
			$smarty->assign('tilmelding', $tilmelding);
			$smarty->display('Kalender/AktUserTilmeldingForside2.tpl');
		}
		if (isset($_POST['cmdOk'])) {
			$tilmelding->setDB($dbPuls3060, $dbRegnskab3060);
			$tilmelding->getFormDataAktUserTilmelding();

			if ($tilmelding->validateAktUserTilmelding()) {
				$smarty->assign('tilmelding', $tilmelding);
				$smarty->display('Kalender/AktUserTilmeldingGodkend2.tpl');
			}
			else {
				$smarty->assign('tilmelding', $tilmelding);
				$smarty->display('Kalender/AktUserTilmelding2.tpl');
			}
		}
	}
	break;


	case "23":   //Process AktUserTilmeldingGodkend2.tpl
	if (isset($_SESSION['tilmelding']))	{
		$tilmelding = $_SESSION['tilmelding'];
		if (isset($_POST['cmdCancel'])) {
			$smarty->assign('tilmelding', $tilmelding);
			$smarty->display('Kalender/AktUserTilmeldingForside2.tpl');
		}
		elseif (isset($_POST['cmdBack'])) {
			$smarty->assign('tilmelding', $tilmelding);
			$smarty->display('Kalender/AktUserTilmelding2.tpl');
		}
		else {
			$tilmelding->setDB($dbPuls3060, $dbRegnskab3060);
			$tilmelding->akttilmelding(2);
			$smarty->assign('tilmelding', $tilmelding);
			$smarty->display('Kalender/AktUserTilmeldingKvitering2.tpl');
		}
	}
	break;


	default:
		throw new Exception("Dowhat: $DoWhat is not valid action");
		break;
}

$_SESSION['tilmelding'] = $tilmelding;
?>
