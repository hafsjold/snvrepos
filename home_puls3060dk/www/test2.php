<? header ("Last-Modified: " . gmdate("D, d M Y H:i:s") . " GMT"); // always modified ?>
<?php
  $conn_www = "host=hd11.hafsjold.dk port=5432 sslmode=require dbname=puls3060 user=www";
  $dbLink = pg_connect($conn_www);

  $Query="SELECT navn FROM tbllob";
  $dbResult = pg_query($dbLink, $Query);
  $row = pg_fetch_array($dbResult, NULL , PGSQL_ASSOC);
  $navn = $row["navn"];
?>

Test=<?=$navn;?>
