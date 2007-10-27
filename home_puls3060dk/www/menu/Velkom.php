<?php
include_once('menuclasses.php');
$menu = new clsMenu();
$menu->addMenuItem('mnuMaalsaetning', 'M�ls�tning', '/velmnu/Velkom/maalsaetning.htm', false, 1);
$menu->addMenuItem('mnuTilbud', 'Tilbud', '/velmnu/Velkom/traening.htm', false, 1);
$menu->addMenuItem('mnuEgneLoeb', 'Egne l�b', '/velmnu/Velkom/egneloeb.htm', false, 1);
if (isset($_SESSION['login']))	
{
	$login = $_SESSION['login'];
	if ($login->login) $menu->addMenuItem('mnuLogaf', 'Log af', '/velmnu/logout.php', false, 1);
	else $menu->addMenuItem('mnuLogin', 'Log p�', '/velmnu/login/login.php', false, 1);	
}	
else $menu->addMenuItem('mnuLogin', 'Log p�', '/velmnu/login/login.php', false, 1);	
$menu->makeImages();
include_once('menu.tpl');
?> 
