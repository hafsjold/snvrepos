<?php
  require 'smarty_puls3060dk.php';
  $smarty = new Smarty_Puls3060dk;

  if ($_REQUEST['leftmenu']) $smarty->assign("leftmenu",$_REQUEST['leftmenu'] . ".tpl");
  if ($_REQUEST['mainpage']) $smarty->assign("mainpage","/data/home/puls3060ny/www" . $_REQUEST['mainpage']);

  $smarty->display('index_new.tpl');
?>
