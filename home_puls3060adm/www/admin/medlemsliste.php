<?php
function DatoFormat($Sender) {
	return (strftime("%Y-%m-%dT%H:%M:%S", strtotime(strtok($Sender, "."))));
}


setlocale (LC_TIME, "da_DK.ISO8859-1");
include_once("conn.inc");  
require_once('domit/xml_domit_include.php'); 

$doc =& new DOMIT_Document(); 
$success= $doc->loadXML("medlemsliste.xml");
$dbLink = pg_connect($conn_www);
$Query="
	SELECT
		m.personid as personid,
		m.fornavn as fornavn,
		m.efternavn as efternavn,
		p.adresse as adresse,
		m.postnr as postnr,
		p.bynavn as bynavn,
		m.indmeldt as indmeldt,
		m.betalttildato as betalttildato,
		m.udmeldt as udmeldt,
		(SELECT tlfnr FROM tbltelefon w WHERE w.personid = m.personid ORDER BY tlftype DESC LIMIT 1) as tlfnr,
		(SELECT mailadr FROM tblmailadresse x WHERE x.personid = m.personid ORDER BY mailtype DESC LIMIT 1) as mailadr
	FROM vmedlemsliste_alle m
	LEFT JOIN tblperson p ON m.personid = p.id
	ORDER BY fornavn, efternavn;";
$AntalCol = 11;
$dbResult = pg_query($dbLink, $Query);

$TableNode =& $doc->selectNodes("/Workbook/Worksheet/Table",1);   
$TemplateNodeList =& $TableNode->selectNodes("Row");
$lastRow = $TemplateNodeList->getLength();   
$TemplateNode =& $TableNode->selectNodes("Row",$lastRow);
$clonedTemplateNode =& $TemplateNode->cloneNode(true);
$TableNode->removeChild($TemplateNode);

$lin = 0;
while($row = pg_fetch_array($dbResult, NULL , PGSQL_NUM)){
   $lin++;
   $clonedCDNode =& $clonedTemplateNode->cloneNode(true);
   for ($col=1;$col<=$AntalCol;$col++){
		$CellNode =& $clonedCDNode->selectNodes("Cell", $col);		
		$DataNode =& $CellNode->selectNodes("Data", 1);
        $TypeNode =& $DataNode->getAttributeNode("ss:Type");
		$TypeNodeValue = $TypeNode->getValue();
		if ($row[$col -1]){
			switch ($TypeNodeValue) {
				case "String":
					$DataNode->setText(utf8_encode($row[$col -1]));
					break;
				case "Number":
					$DataNode->setText($row[$col -1]);
					break;
				case "DateTime":
					$DataNode->setText(DatoFormat($row[$col -1]));
					break;
				default:
					$DataNode->setText(utf8_encode($row[$col -1]));
			}		
		}else{
			$CellNode->removeChild($DataNode);
		}
	}
   $TableNode =& $doc->selectNodes("/Workbook/Worksheet/Table",1);
   $TableNode->appendChild($clonedCDNode);
}

$ExpandedRowCountNode =& $TableNode->getAttributeNode("ss:ExpandedRowCount");
$ExpandedRowCountNodeValue = $ExpandedRowCountNode->getValue();
$TableNode->setAttribute("ss:ExpandedRowCount", $lin+$ExpandedRowCountNodeValue-1);

//echo $doc->toNormalizedString(true);

$data = $doc->toString(false,false);
header("Content-type: application/application/x-msexcel");
header("Content-disposition: inline; filename=medlemsliste.xls");
header("Content-length: " . strlen($data));
echo $data;
?>
