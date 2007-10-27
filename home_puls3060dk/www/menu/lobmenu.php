<?php
include_once('menuclasses.php');
$menu = new clsMenu();
//$menu->addMenuItem('mnuEspergaerdeloebet', 'Espergærdeløbet', '/lobmnu/Lob/el2007.php', false, 1);
$menu->addMenuItem('mnuNatloeb', 'Halloweenløb', '/lobmnu/Lob/nat2007.htm', false, 1);
$menu->addMenuItem('mnuLoebstilmelding', 'Løbs tilmelding', '/lobmnu/Lob/LobUserTilmelding.php', false, 1);
$menu->addMenuItem('mnuResultatliste', 'Resultat liste', '/lobmnu/Lob/ResultaterIndex.php', false, 1);
$menu->addMenuItem('mnuIdraetsmaerke_5km', 'Idrætsmærke 5 km', '/Lob/Idraetsmaerke_5 km.pdf', true, 1);
$menu->addMenuItem('mnuIdraetsmaerke_10km', 'Idrætsmærke 10 km', '/Lob/Idraetsmaerke_10 km.pdf', true, 1);
//$menu->addMenuItem('mnuLobUserTilmelding_Test', 'PBS Test', '/lobmnu/pbstest/LobUserTilmelding.php', false, 1);
$menu->addMenuItem('mnuTestLobAdmTilmelding', 'Lob_Adm_Tilm.', '/tstmnu/Test/LobAdmTilmelding.php', false, 2);
//$menu->addMenuItem('mnuDetstroecenterlob', 'Det store center løb', '/lobmnu/Lob/scl2007_aflyst.htm', false, 1);
$menu->makeImages();
include_once('menu.tpl');
?> 
