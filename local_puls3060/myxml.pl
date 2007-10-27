#!/usr/bin/perl -w

use XML::DOM;

my $parser = new XML::DOM::Parser;
my $doc = $parser->parsefile ("candy.xml");

my $root = $doc->getDocumentElement();
print "\nThe root element is ", $root->getNodeName(), ".\n";

my @children = $root->getChildNodes();
print "There are ", scalar(@children), " child elements.\n";
  
print "They are: \n";

foreach my $child ($root->getChildNodes()) {
     if ($child->getNodeType == TEXT_NODE){
         print "Text: ", $child->getData();
     } elsif ($child->getNodeType == ELEMENT_NODE) {
         print $child->getNodeName(), " = ", $child->getFirstChild()->getData(), "\n";	
     }
}


my @products = $root->getElementsByTagName("product");
$productNum = 1;
foreach my $product (@products) {
      my $productElement = $product;

      $productElement->setAttributeNode($doc->createAttribute("productNumber"));
      $productElement->setAttribute("productNumber", ("Product " + $productNum));

      $productName = $productElement->getFirstChild()->getData();
      $productElement->getFirstChild()->setNodeValue(uc($productName));

      $updateElement = $doc->createElement("updated");
      $rightNow = time();
      $updateText = $doc->createTextNode($rightNow);

      $updateElement->appendChild($updateText);
      $productElement->appendChild($updateElement);

      $productNum = $productNum + 1;
}