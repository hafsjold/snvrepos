<?php
setlocale (LC_TIME, "da_DK.ISO8859-1");
include_once("conn.inc");  
$dbLink = pg_connect($conn_www);

require_once('domit/xml_domit_include.php'); 
$doc =& new DOMIT_Document(); 
$success= $doc->loadXML("aktivitetstilmeldinger.xml");

$aktid = 327;
$Query="SELECT id, aktid, personid, fornavn, efternavn, adresse, postnr, bynavn, tlfnr, mailadr FROM tblakttilmelding WHERE aktid = " . $aktid . " ORDER BY fornavn, efternavn;";
$dbResult = pg_query($dbLink, $Query);

$lin = 0;
while($row = pg_fetch_array($dbResult, NULL , PGSQL_ASSOC)){
   $lin++;
   $CopyNode =& $doc->selectNodes("/Workbook/Worksheet/Table/Row",2);
   $clonedCDNode =& $CopyNode->cloneNode(true);
   for ($col=1;$col<=10;$col++){
		$ColNode =& $clonedCDNode->selectNodes("Cell/Data", $col);
		switch ($col) {
		case 1:
		   $ColNode->setText($row["id"]);
		   break;
		case 2:
		   $ColNode->setText($row["aktid"]);
		   break;
		case 3:
		   $ColNode->setText($row["personid"]);
		   break;
		case 4:
		   $ColNode->setText(utf8_encode($row["fornavn"]));
		   break;
		case 5:
		   $ColNode->setText(utf8_encode($row["efternavn"]));
		   break;
		case 6:
		   $ColNode->setText(utf8_encode($row["adresse"]));
		   break;
		case 7:
		   $ColNode->setText($row["postnr"]);
		   break;
		case 8:
		   $ColNode->setText(utf8_encode($row["bynavn"]));
		   break;
		case 9:
		   $ColNode->setText($row["tlfnr"]);
		   break;
		case 10:
		   $ColNode->setText($row["mailadr"]);
		   break;
		}
	}
   $TableNode =& $doc->selectNodes("/Workbook/Worksheet/Table",1);
   $TableNode->appendChild($clonedCDNode);
}
$DeleteNode =& $doc->selectNodes("/Workbook/Worksheet/Table/Row",2);
$TableNode =& $doc->selectNodes("/Workbook/Worksheet/Table",1);
$TableNode->removeChild($DeleteNode);

$TableNode =& $doc->selectNodes("/Workbook/Worksheet/Table",1);
$TableNode->setAttribute("ss:ExpandedRowCount", $lin+1);

//echo $doc->toNormalizedString(true);

$data = $doc->toString(false,false);
header("Content-type: application/application/x-msexcel");
header("Content-disposition: inline; filename=aktivitetstilmeldinger.xls");
header("Content-length: " . strlen($data));
echo $data;

?>
