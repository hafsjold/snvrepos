<html>
<head>
<link href="/css/puls3060.css" rel="stylesheet" type="text/css">
<title>Kontakter</title>
<meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
</head>
<body bgcolor="#FFFFFF" text="#000000">
<?php
// /getartikel.php?artikel=3
include_once("conn.inc");
$dbLink = pg_connect($conn_www);

if ($dbLink) {
	if (isSet($_REQUEST['artikel'])){
		$Query="SELECT artikel, changedate FROM tblartikel WHERE artikelid='" . $_REQUEST['artikel'] . "'" . "ORDER BY version DESC";
		if ($dbResult = pg_query($dbLink, $Query))
		if($row = pg_fetch_array($dbResult, NULL , PGSQL_ASSOC)){

			if (eregi ("<a([^>]+)>", $row["artikel"])){
				$artikel = eregi_replace("<a([^>]+)>",
				"<a\\1 onClick=\"return top.rwlink(href);\">",
				$row["artikel"]);
			}
			else{
				$artikel = $row["artikel"];
			}

			if (($timestamp = strtotime($row["changedate"])) === false) {
				$hdrdate = date("r");
			} else {
				$hdrdate = date("r", $timestamp);
			}

			header("Date: " . $hdrdate);
			echo $artikel;
		}
		else {
			echo 'artikel ikke fundet';
		}
	}
	else {
		echo 'artikel ikke fundet';
	}
}
?>
</body>
</html>