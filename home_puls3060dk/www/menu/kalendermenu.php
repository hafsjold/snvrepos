<?php
include_once('menuclasses.php');
$menu = new clsMenu();
$menu->addMenuItem('mnuKalender', 'Kalender', '/kalmnu/Kalender/Aktivitetskalender.php');
$menu->makeImages();
include_once('menu.tpl');
?> 
