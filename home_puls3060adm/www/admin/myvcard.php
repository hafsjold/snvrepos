<?
setlocale (LC_TIME, "da_DK.ISO8859-1");
include_once("conn.inc");
include_once("vcard.php");

$dbLink = pg_connect($conn_www);
$Query="
	SELECT
		m.personid as personid,
		m.fornavn as fornavn,
		m.efternavn as efternavn,
		p.adresse as adresse,
		m.postnr as postnr,
		p.bynavn as bynavn,
		(SELECT mailadr FROM tblmailadresse x WHERE x.personid = m.personid ORDER BY mailtype DESC LIMIT 1) as mailadr
	FROM vmedlemsliste m
	LEFT JOIN tblperson p ON m.personid = p.id
	ORDER BY fornavn, efternavn;";
$dbResult = pg_query($dbLink, $Query);
$output = "";

while($row = pg_fetch_array($dbResult, NULL , PGSQL_ASSOC)){
	$v = new vCard();

	$v->setName($row['efternavn'], $row['fornavn'], "", "");
	$v->setCategories("Puls");
	$v->setAddress("", "", $row['adresse'], $row['bynavn'], "", $row['postnr'], "Denmark","HOME;POSTAL");


	$dbTlfnrResult = pg_query($dbLink, "SELECT tlfnr,tlftype FROM tbltelefon WHERE personid = " . $row['personid'] . ";");
	while($Tlfnrrow = pg_fetch_array($dbTlfnrResult, NULL , PGSQL_ASSOC)){
		if ($Tlfnrrow['tlftype'] == "Hjem")
		$v->setPhoneNumber("+45 ". $Tlfnrrow['tlfnr'], "HOME;VOICE");
		elseif ($Tlfnrrow['tlftype'] == "Arbejde")
		$v->setPhoneNumber("+45 ". $Tlfnrrow['tlfnr'], "WORK;VOICE");
		elseif ($Tlfnrrow['tlftype'] == "Mobil")
		$v->setPhoneNumber("+45 ". $Tlfnrrow['tlfnr'], "CELL;VOICE");
		else
		$v->setPhoneNumber("+45 ". $Tlfnrrow['tlfnr'], "HOME;VOICE");
	}

	$dbMailadrResult = pg_query($dbLink, "SELECT mailadr FROM tblmailadresse WHERE personid = " . $row['personid'] . " ORDER BY id;");
	while($Mailadrrow = pg_fetch_array($dbMailadrResult, NULL , PGSQL_ASSOC)){
			$v->setEmail($Mailadrrow['mailadr']);
	}


	//$v->setNote("You can take some notes here.\r\nMultiple lines are supported via \\r\\n.");
	//$v->setURL("http://www.puls3060.dk", "WORK");

	$output .= $v->getVCard();
}

//$filename = $v->getFileName();
$filename = "puls3060.vcf";

Header("Content-Disposition: attachment; filename=$filename");
Header("Content-Length: ".strlen($output));
Header("Connection: close");
Header("Content-Type: text/x-vCard; name=$filename");

echo $output;
?>