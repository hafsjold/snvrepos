<?php
/*
* Smarty plugin
* -------------------------------------------------------------
* File: function.antalpuls3060medlemmer.php
* Type: function
* Name: antalpuls3060medlemmer
* Purpose: outputs antal puls3060 medlemmer
* -------------------------------------------------------------
*/
function smarty_function_antalpuls3060medlemmer($params, &$smarty)
{
    include_once("conn.inc");  
    $dbLink = pg_connect($conn_www);
	$Query="select count(*) as antal from vmedlemsliste";
	$dbResult = pg_query($dbLink, $Query);
	$row = pg_fetch_array($dbResult, NULL , PGSQL_ASSOC);
	return $row["antal"];
}
?>