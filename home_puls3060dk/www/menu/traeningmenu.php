<?php
include_once('menuclasses.php');
$menu = new clsMenu();
$menu->addMenuItem('mnuProgrammer', 'Programmer', '/tramnu/Traening/TProgramIndex.php');
$menu->addMenuItem('mnuBegynder', 'Begynder', '/tramnu/Traening/Begyndertraening.htm');
$menu->addMenuItem('mnuLoebsttraening', 'Løbstræning', '/tramnu/Traening/lobtraningsholdet.htm');
$menu->addMenuItem('mnuMellemholdet', 'Mellemholdet', '/tramnu/Traening/Mellemholdet.htm');
$menu->addMenuItem('mnuHalvtimesholdet', 'Halvtimesholdet', '/tramnu/Traening/Halvtimesholdet.htm');
$menu->addMenuItem('mnuGruppeloeb', 'Gruppeløb', '/tramnu/Traening/gruppelob.htm');
$menu->addMenuItem('mnuStavgang', 'Stavgang', '/tramnu/Traening/Stavgang.htm');
$menu->addMenuItem('mnuPowerwalk', 'Power Walk', '/tramnu/Traening/powerwalk.htm');
$menu->addMenuItem('mnuRuteforslag', 'Ruteforslag', '/Traening/PULS3060ruteforslag.pdf', true);
$menu->addMenuItem('mnuTraenervejledning', 'Trænervejledning', 'http://www.puls3060.dk/Traening/Tranervejledning.pdf', true);
$menu->makeImages();
include_once('menu.tpl');
?> 
