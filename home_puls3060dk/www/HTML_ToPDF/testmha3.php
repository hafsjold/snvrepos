<?php 
$indent = "";

$showfile = dirname(__FILE__) . '/Nyhedsbrev-2_konv.htm';
$dom = new DOMDocument();

$dom->load($showfile);

$allnodes = $dom->getElementsByTagName('*');
for ($i=0; $i<50; $i++) {
  $node = $allnodes->item($i);
  echo "node: {$node->nodeName} <br/>";
}
$savefile = dirname(__FILE__) . '/Nyhedsbrev-2_konv.xml';
unlink($savefile);
$dom->save($savefile);


?> 