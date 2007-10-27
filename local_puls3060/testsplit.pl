#!/usr/bin/perl -w
use strict;

my $navn_medlem = "Ole";
my $underskrift_navn = "Mogens Hafsjold";
my $mail;
 
eval '$mail = "Kære $navn_medlem

Så vidt jeg kan se, har du ikke betalt seneste kontingentopkrævning for medlemskabet af PULS 3060.

Hvis det skyldes en    forglemmelse, bedes du foretage indbetalingen snarest muligt.

Har du ikke modtaget opkrævningen, eller er den bortkommet, så kontakte mig venligst.


På foreningens vegne
$underskrift_navn"';

print formattext($mail, 55);


sub formattext {
  my $inputtext = shift;
  my $maxlinewith = shift;
  
  my $currentlinelength;
  my $outputtext;
  my $currentword;
  my $currentline;
  my $workline;
  my $linecount = 0;
  my $wordcount = 0;
  my $crlf = "\r\n|\n";
  my $bltp = "\t| ";

  foreach $currentline (split(/$crlf/,$inputtext)) {
    if (length($currentline) <= $maxlinewith) {
      $outputtext .= $currentline . "\n";
  	$linecount++;
    } 
    else {
      $currentlinelength = 0;
  	$wordcount = 0;
      foreach $currentword (split(/$bltp/,$currentline)) {
  	  if ((length($currentword)) > 0) { 
  	    if ($currentlinelength == 0) {
  	      $workline .= $currentword;
  	      $currentlinelength = length($currentword);
  	      $wordcount++;
          }
  	    else {
  	      if (($currentlinelength + 1 + length($currentword)) <= $maxlinewith) {
  	        $workline .= " " . $currentword;
  	        $currentlinelength += 1 + length($currentword);
  	        $wordcount++;
  	  	  }
  		  else {
  	        $outputtext .= expandline($workline, $wordcount, $maxlinewith) . "\n";
  	        $linecount++;
  	        $workline = $currentword;
              $currentlinelength = length($currentword);
  	        $wordcount = 1;
  		  }
  	    }
        }
      }
      if ($currentlinelength > 0) {
  	  $outputtext .= $workline . "\n";
        $workline = "";
  	  $linecount++;
      }
    }
  }
  return $outputtext;
}

sub expandline {
  my $inputline = shift;
  my $wordsinline = shift;
  my $outputlinewith = shift;
  
  my $inputword;
  my $outputline;
  my $blanksmissingcount;
  my $blankscountmodulo;
  my $blankscount;
  my $firstword = 1;
  my $splitchar = ' ';

  $blanksmissingcount = $outputlinewith - length($inputline);
  $blankscount = abs($blanksmissingcount/($wordsinline-1));
  $blankscountmodulo = $blanksmissingcount%($wordsinline-1);

  foreach $inputword (split(/$splitchar/,$inputline)) {
  	if ($firstword) {
  	  $outputline = $inputword;
	}
	else {
	  if ($blankscountmodulo > 0) {
	    $outputline .=  (' ' x ($blankscount+2)) . $inputword;
		$blankscountmodulo--;
	  }
	  else {
	    $outputline .= (' ' x ($blankscount+1)) . $inputword;
	  }
    }
    $firstword = 0;
  }
  return $outputline;
}




