<?php
include_once('menuclasses.php');
$menu = new clsMenu();
$menu->addMenuItem('mnuTestLobAdmTilmelding', 'Lob_Adm_Tilm.', '/tstmnu/Test/LobAdmTilmelding.php');
$menu->addMenuItem('mnuTestIndmeldelse', 'Indmeldelse', '/tstmnu/Test/NytMedlem.php');
$menu->addMenuItem('mnuTestLobUserTilmelding', 'Lob_User_Tilm.', '/tstmnu/Test/LobUserTilmelding.php');
$menu->makeImages();
include_once('menu.tpl');
?> 
