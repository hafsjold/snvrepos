<?php
    setlocale (LC_TIME, "da_DK.ISO8859-1");

    include_once("conn.inc");  
    $dbLink = pg_connect($conn_www);
    $Query="SELECT xmlperson() as xmldata;";
    $dbResult = pg_query($dbLink, $Query);
    $row = pg_fetch_array($dbResult, NULL , PGSQL_ASSOC);
	$data = $row["xmldata"];
    
    header("Content-type: text/xml");
    header("Content-disposition: inline; filename=person.xml");
    header("Content-length: " . strlen($data));
    echo $data;

?>