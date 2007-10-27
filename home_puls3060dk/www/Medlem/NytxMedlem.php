<?php
setlocale (LC_TIME, "da_DK.ISO8859-1");

require 'smarty_puls3060dk.php';
require('clsNytMedlem.php');

$smarty = new Smarty_Puls3060dk;
$smarty->caching = false;
$db = new clsPuls3060SQL();

if (!clsNytMedlem::validate_browser()) {
	$smarty->display('Medlem/NytMedlemBrowserFejl.tpl');
	return;
}

if (isset($_REQUEST['DoWhat'])) $DoWhat=$_REQUEST['DoWhat'];
else $DoWhat = 1;

session_start();

switch ($DoWhat) {
	case "1":   //display screen1
	if (session_is_registered ('medlem')) {
		session_unregister ('medlem');
	}
	$medlem = new clsNytMedlem();
	$medlem->setDB($db);


	$fields = $medlem->fields;
	$smarty->assign('fields', $fields);
	$smarty->assign('medlem', $medlem);	
	$smarty->display('Medlem/Screen.tpl');
	break;

	case "2":   //validate screen1
	if (isset($_SESSION['medlem']))	{
		$medlem = $_SESSION['medlem'];
		$medlem->setDB($db);
		$medlem->getFormData();


		if ($medlem->validate()) {
			$smarty->assign('medlem', $medlem);
			$smarty->display('Medlem/NytMedlemBekraft.tpl');
		}
		else {
			$smarty->assign('medlem', $medlem);
			$smarty->display('Medlem/NytMedlem.tpl');
		}
	}
	break;

	case "3":   //update database
	if (isset($_SESSION['medlem']))	{
		$medlem = $_SESSION['medlem'];
		$medlem->setDB($db);

		$medlem->tblnytmedlem();
		$smarty->assign('medlem', $medlem);
		$smarty->display('Medlem/NytMedlemKvitering.tpl');
	}
	break;

	default:
		throw new Exception("Dowhat: $DoWhat is not valid action");
		break;
}

$_SESSION['medlem'] = $medlem;

?>