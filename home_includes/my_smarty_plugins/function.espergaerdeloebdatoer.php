<?php
/*
* Smarty plugin
* -------------------------------------------------------------
* File: function.espergaerdeloebdatoer.php
* Type: function
* Name: espergaerdeloebdatoer
* Purpose: outputs Datoer for Espergærdeløb
* -------------------------------------------------------------
*/
function smarty_function_espergaerdeloebdatoer($params, &$smarty)
{
    include_once("conn.inc");  
    $dbLink = pg_connect($conn_www);
	$Query="select id, navn, dato from public.tbllob where year(dato) = year(now()) and substring(navn, 1, 15) = 'Espergærdeløbet' order by dato";
	$dbResult = pg_query($dbLink, $Query);
	$row = pg_fetch_array($dbResult, NULL , PGSQL_ASSOC);
	$datoer[1] = $row["dato"];
	return $datoer;
}
?>



