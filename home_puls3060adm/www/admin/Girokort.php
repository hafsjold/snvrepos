<?php
    setlocale (LC_TIME, "da_DK.ISO8859-1");

    include_once("conn.inc");  
    $dbLink = pg_connect($conn_www);

    include_once("conn_regnskab.inc");  
    $dbLink = pg_connect($conn_regnskab);
    $Query="SELECT pbs.nytmedlem_fakturer_bsh(0);";
    $dbResult = pg_query($dbLink, $Query);
    $row = pg_fetch_array($dbResult, NULL , PGSQL_ASSOC);
    echo "Der er sendt " . $row["nytmedlem_fakturer_bsh"] . " FI-kort til PBS";
?>