<?php
function DatoFormat($Sender) {
	return (strftime("%Y-%m-%dT%H:%M:%S", strtotime(strtok($Sender, "."))));
}

setlocale (LC_TIME, "da_DK.ISO8859-1");
include_once("conn_regnskab.inc");  
require_once('domit/xml_domit_include.php'); 

$doc =& new DOMIT_Document(); 
$success= $doc->loadXML("regnskab.xml");

$dbLink = pg_connect($conn_regnskab);
$Query="SELECT ds, k, konto, dato, bilag, tekst, beløb FROM fvpertrans_excel(1) order by dato, bilag;";
$AntalCol = 7;
$dbResult = pg_query($dbLink, $Query);

$WorksheetNode =& $doc->selectNodes("/Workbook/Worksheet",2);   
$TableNode =& $WorksheetNode->selectNodes("Table",1);   
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
   $TableNode->appendChild($clonedCDNode);
}

$ExpandedRowCountNode =& $TableNode->getAttributeNode("ss:ExpandedRowCount");
$ExpandedRowCountNodeValue = $ExpandedRowCountNode->getValue();
$TableNode->setAttribute("ss:ExpandedRowCount", $lin+$ExpandedRowCountNodeValue-1);

$ReferenceNode =& $doc->selectNodes("/Workbook/Worksheet/PivotTable/PTSource/ConsolidationReference/Reference",1);   
$ReferenceNode->setText("R1C1:R" . ($lin+$ExpandedRowCountNodeValue-1) . "C7");
$FileNameNode =& $doc->selectNodes("/Workbook/Worksheet/PivotTable/PTSource/ConsolidationReference/FileName",1);   
$FileNameNode->setText("[regnskab.xls]Transer");

$HeaderNode =& $doc->selectNodes("/Workbook/Worksheet/WorksheetOptions/PageSetup/Header",1);   
$HeaderDataNode =& $HeaderNode->getAttributeNode("x:Data");
$HeaderDataNodeValue = $HeaderDataNode->getValue();
$HeaderNode->setAttribute("x:Data", $HeaderDataNodeValue . " - 2006 ");

//echo $doc->toNormalizedString(true);

$data = $doc->toString(false,true);
header("Content-type: application/x-msexcel");
header("Content-disposition: inline; filename=regnskab.xls");
header("Content-length: " . strlen($data));
echo $data;

?>
