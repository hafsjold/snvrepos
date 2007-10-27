<?php
$htmFileOut = dirname(__FILE__) . '/Nyhedsbrev-2_konv.htm';
$htmlOut = implode('', file($htmFileOut));

$indent = 5; /* Number of spaces to indent per level */

$xml = new XMLReader();
$xml->open($htmFileOut);
$xml->setParserProperty(XMLReader::LOADDTD,  FALSE); 
$xml->setParserProperty(XMLReader::VALIDATE, TRUE);
while($xml->read()) {
   /* Print node name indenting it based on depth and $indent var */
   print str_repeat("&nbsp;", $xml->depth * $indent).$xml->name."<br/>";
   if ($xml->hasAttributes) {
       $attCount = $xml->attributeCount;
       print str_repeat("&nbsp;", $xml->depth * $indent)." Number of Attributes: ".$xml->attributeCount."<br/>";
   }
}
print "<br/><br/>Valid:<br/>n";
var_dump($xml->isValid());


?>