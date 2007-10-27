<?php
include_once('menuclasses.php');
$menu = new clsMenu();
$menu->addMenuItem('mnuKlubtoj', 'Klubtøj', '/medmnu/Medlem/Klubtoj.htm', false, 1);
$menu->addMenuItem('mnuIndmeldelse', 'Indmeldelse', '/medmnu/Medlem/NytMedlem.php', false, 1);
//$menu->addMenuItem('mnuBestyrelse', 'Bestyrelse', '/medmnu/Medlem/Bestyrelse.htm', false, 1);
$menu->addMenuItem('mnuBestyrelse', 'Bestyrelse', '/medmnu/getartikel.php?artikel=1', false, 1);
//$menu->addMenuItem('mnuKontakter', 'Kontakter', '/medmnu/Medlem/Kontakter.htm', false, 1);
$menu->addMenuItem('mnuKontakter', 'Kontakter', '/medmnu/getartikel.php?artikel=2', false, 1);
$menu->addMenuItem('mnuVedtagter', 'Vedtægter', 'https://www.puls3060.dk/medmnu/Medlem/Vedtagter.htm', false, 1);
$menu->addMenuItem('mnuMoeder', 'Møder', '/medmnu/Medlem/Moeder.php', false, 1);
//$menu->addMenuItem('mnuTest', 'Test', '/tstmnu/Test/Test.htm', false, 1);
$menu->addMenuItem('mnuxIndmeldelse', 'xIndmeldelse', '/medmnu/Medlem/NytxMedlem.php', false, 2);
$menu->makeImages();
include_once('menu.tpl');
?> 
