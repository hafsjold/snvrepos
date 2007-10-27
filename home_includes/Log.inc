<?
  include_once("conn.inc");  
  $Subnet = "192.168";
  if ($Subnet != substr($_SERVER['REMOTE_ADDR'], 0, strlen($Subnet))){
    $dbLogLink = pg_connect($conn_www);
    pg_query($dbLogLink, "INSERT INTO tblLog (LogName, LogIP, browser) VALUES ('" . $_SERVER['SCRIPT_NAME'] . "', '" . $_SERVER['REMOTE_ADDR'] . "', '" . $_SERVER['HTTP_USER_AGENT'] ."')");
  }
?>
