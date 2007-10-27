<?php
    setlocale (LC_TIME, "da_DK.ISO8859-1");
    function DatoFormat($Sender)
	{
      return(strtolower(strftime("%A den %d. %B", strtotime(substr($Sender,0,18)))));
	}
    function TimeFormat($Sender)
	{
      return(strtolower(strftime("%M:%S", strtotime(substr($Sender,0,8)))));
	}

    if (isset($_REQUEST['LobId'])) 
      $LobId=$_REQUEST['LobId'];
    else
      $LobId = 51;


    include_once("conn.inc");  
    $dbLink = pg_connect($conn_www);

    $Query="SELECT publicerresultat($LobId) AS retur;";
    $dbResult = pg_query($dbLink, $Query);
    $row = pg_fetch_array($dbResult, NULL , PGSQL_ASSOC);
    $retur = $row["retur"];

    if ($retur == 0)
      Echo "Resultat publicer p&aring; WWW Gennemf&oslash;rt";
    else
      Echo "Resultat publicer p&aring; WWW m&aring; kun k&oslash;res 1 gang";

?>
