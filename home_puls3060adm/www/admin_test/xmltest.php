<?php 

// Set error reporting to ignore notices 
error_reporting(E_ALL ^ E_NOTICE); 

// Include XML_Unserializer 
require_once 'XML/Unserializer.php'; 

// The XML document 
$doc='
<?mxl version="1.0" encoding="ISO-8859-1"?>
  <palette>
    <red>45</red>
    <green>240</green>
    <blue>120</blue>
  </palette>
'; 

// Instantiate the serializer 
$Unserializer = &new XML_Unserializer(); 

// Serialize the data structure 
$status = $Unserializer->unserialize($doc); 

// Check whether serialization worked 
if (PEAR::isError($status)) { 
   die($status->getMessage()); 
} 

// Display the PHP data structure 
echo '<pre>'; 
print_r($Unserializer->getUnserializedData()); 
echo '</pre>'; 
?>
