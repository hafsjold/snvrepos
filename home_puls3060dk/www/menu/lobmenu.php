<?php
include_once('menuclasses.php');
$menu = new clsMenu();
//$menu->addMenuItem('mnuEspergaerdeloebet', 'Esperg�rdel�bet', '/lobmnu/Lob/el2007.php', false, 1);
$menu->addMenuItem('mnuNatloeb', 'Halloweenl�b', '/lobmnu/Lob/nat2007.htm', false, 1);
$menu->addMenuItem('mnuLoebstilmelding', 'L�bs tilmelding', '/lobmnu/Lob/LobUserTilmelding.php', false, 1);
$menu->addMenuItem('mnuResultatliste', 'Resultat liste', '/lobmnu/Lob/ResultaterIndex.php', false, 1);
$menu->addMenuItem('mnuIdraetsmaerke_5km', 'Idr�tsm�rke 5 km', '/Lob/Idraetsmaerke_5 km.pdf', true, 1);
$menu->addMenuItem('mnuIdraetsmaerke_10km', 'Idr�tsm�rke 10 km', '/Lob/Idraetsmaerke_10 km.pdf', true, 1);
//$menu->addMenuItem('mnuLobUserTilmelding_Test', 'PBS Test', '/lobmnu/pbstest/LobUserTilmelding.php', false, 1);
$menu->addMenuItem('mnuTestLobAdmTilmelding', 'Lob_Adm_Tilm.', '/tstmnu/Test/LobAdmTilmelding.php', false, 2);
//$menu->addMenuItem('mnuDetstroecenterlob', 'Det store center l�b', '/lobmnu/Lob/scl2007_aflyst.htm', false, 1);
$menu->makeImages();
include_once('menu.tpl');
?> 
