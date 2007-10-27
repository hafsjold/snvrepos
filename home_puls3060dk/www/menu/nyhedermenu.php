<?php
include_once('menuclasses.php');
$menu = new clsMenu();
$menu->addMenuItem('mnuNyheder', 'Nyheder', '/nyhmnu/Nyheder/Nyheder.php');
$menu->addMenuItem('mnuNyhedsbreve', 'Nyhedsbreve', '/nyhmnu/Medlem/Nyhedsbrev.php');
$menu->makeImages();
include_once('menu.tpl');
?> 
