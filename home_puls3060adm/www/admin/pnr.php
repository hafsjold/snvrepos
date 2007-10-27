<?php
  include_once("conn.inc");  
  $dbLink = pg_connect($conn_www);
  if (isset($_REQUEST['postnr'])) {
	if(ob_get_length()) ob_clean();
    header('Expires: Mon, 26 Jul 1997 05:00:00 GMT' ); 
    header('Last-Modified: ' . gmdate('D, d M Y H:i:s') . 'GMT'); 
    header('Cache-Control: no-cache, must-revalidate'); 
    header('Pragma: no-cache');
    header('Content-Type: text/html');
	$postnr = $_REQUEST['postnr'];
    $Query="SELECT postnavn FROM kmspostnr WHERE postnr='" . $postnr . "'";
    if ($dbResult = pg_query($dbLink, $Query)) {
      if($row = pg_fetch_array($dbResult, NULL , PGSQL_ASSOC)){
        echo utf8_encode($row["postnavn"]);
      }
	  else echo "";
	}
  }
?>