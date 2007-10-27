<?php
  $ret = NULL;
  if (isset($_REQUEST['postnr']))
    $postnr=$_REQUEST['postnr'];
	if ($postnr == 3060)
	  echo 'Espergaerde';
	else
	  echo $postnr;
?>