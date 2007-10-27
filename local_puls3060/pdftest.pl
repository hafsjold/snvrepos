#!/usr/bin/perl -w
use strict;

use DBI;
use DBD::Pg;
use DBI qw(:sql_types);
use PDF::API2;

bilagpdf();

##################################################
# bilagpdf
##################################################
sub bilagpdf {
  my $db_regnskab = "regnskab3060";

  my $dbh_regnskab;
  my $sql_select_bilag;
  my $sth_select_bilag;

  $dbh_regnskab = DBI->connect( "dbi:Pg:dbname=$db_regnskab", "pgsql", "", {RaiseError => 1, AutoCommit => 0} ) 
              || die "Database connection not made: $DBI::errstr";


  $sql_select_bilag = <<SQL;
    SELECT bilagid,
           bilagaar,
           bilagnr,
           bilagdato,
           konto,
           tekst,
           beloeb
    FROM vudskriv_bilag
    WHERE bilagaar = ?
      AND bilagnr >= ?
      AND bilagnr < 10000
	ORDER BY bilagaar, bilagnr, bilagid, transid
SQL
  my ($bilagid, $bilagaar, $bilagnr, $bilagdato, $konto, $tekst, $beloeb);

  $sth_select_bilag = $dbh_regnskab->prepare( $sql_select_bilag );
  $sth_select_bilag->bind_param( 1, 2005, SQL_INTEGER ); 
  $sth_select_bilag->bind_param( 2, 100, SQL_INTEGER ); 
  $sth_select_bilag->execute();
  $sth_select_bilag->bind_columns( undef, \$bilagid, \$bilagaar, \$bilagnr, \$bilagdato, \$konto, \$tekst, \$beloeb );


  my $overflow = 0;
  my $overflowline = 100;
  my $page;
  my $arial;
  my $arialbold;
  my $txt;
  my $g;

  my $pdf  = PDF::API2->new(-file => "/usr/local/puls3060/HelloWorld.pdf");
  $pdf->mediabox(612,792);

  my $bilagid_prev = -1;
  my $x; 
  my $y = 792; 
  my $Numbeloeb;
  my $saldo;
  my $dkonto;
  my $kkonto;
  my $TestText = "DETTE ER EN TILFÆLDIG MEGET LANG TEKST SOM JEG TRO";

  while( $sth_select_bilag->fetch() ) {
    print  "$bilagnr\n";
	
	if ($beloeb < 0) {
      $Numbeloeb = sprintf "%5.2f", -$beloeb;
      $dkonto = "";
      $kkonto = $konto;
	}
	else{
      $Numbeloeb = sprintf "%5.2f", $beloeb;
      $dkonto = $konto;
      $kkonto = "";
	}
	$Numbeloeb =~ s/\./,/g;

    if ($y > $overflowline) {
      $overflow = 0;
	}
	else {
      $overflow = 1;
	}

	
	if (($bilagid != $bilagid_prev)  || ($overflow)){
	  if ($bilagid_prev != -1){
		 ## End Page
	     if (($saldo != 0) && (!$overflow)){
	       $y -=30;
	       $txt->font($arialbold, 14);
           $Numbeloeb = sprintf "%5.2f", $saldo;
	       $txt->translate($x+355,$y);
	       $txt->text_right("Dif: $Numbeloeb");
	  	   
	  	   $saldo = 0;
	     }
	     $g->stroke();
      }
	  
	  ## New Page	
	  $page = $pdf->page;
	  $arialbold = $pdf->corefont('Arial-Bold',-encoding => 'latin1');
	  $arial = $pdf->corefont('Arial',-encoding => 'latin1');
	  $txt = $page->text;
	  $g = $page->gfx();

	  #Overskrift
	  $txt->font($arialbold, 20);
	  $x=70; $y = 730; $txt->translate($x,$y);
	  $txt->text("Bogføringsbilag Puls3060");
      
      ##my ($mx,$my) = $txt->textpos;
	  ##print "X,Y: $mx,$my\n";
	  
	  $y -=65;
	  #Bilagsdato
	  $txt->font($arialbold, 14);
	  $txt->translate($x,$y);
	  $txt->text("Dato");
	  $txt->translate($x+120,$y);
	  $txt->text_right("$bilagdato");
      $g->rect($x-2, $y-4, 40, 20);
      $g->rect($x-2+40, $y-4, 84, 20);

 	  #Bilagsnr
	  $txt->translate($x+345,$y);
	  $txt->text("Bilagsnr.");
	  $txt->translate($x+345+110,$y);
	  $txt->text_right("$bilagnr");
      $g->rect($x-2+345, $y-4, 64, 20);
      $g->rect($x-2+345+64, $y-4, 50, 20);

	  $y -=45;
	  #Overskrift
	  $txt->translate($x,$y);
	  $txt->text("Bilagstekst");
	  $txt->translate($x+355,$y);
	  $txt->text_right("Beløb");
	  $txt->translate($x+355+50,$y);
	  $txt->text_right("Debet");
	  $txt->translate($x+355+50+50,$y);
	  $txt->text_right("Kredit");

      $g->rect($x-2, $y-4, 305, 20);
      $g->rect($x-2+305, $y-4, 54, 20);
      $g->rect($x-2+305+54, $y-4, 50, 20);
      $g->rect($x-2+305+54+50, $y-4, 50, 20);
	  
	  $txt->font($arial, 9);
    }
	$y -=20;
	$txt->translate($x,$y);
	$txt->text("$tekst");
	$txt->translate($x+355,$y);
	$txt->text_right("$Numbeloeb");
	$txt->translate($x+355+50,$y);
	$txt->text_right("$dkonto");
	$txt->translate($x+355+50+50,$y);
	$txt->text_right("$kkonto");

    $g->rect($x-2, $y-4, 305, 20);
    $g->rect($x-2+305, $y-4, 54, 20);
    $g->rect($x-2+305+54, $y-4, 50, 20);
    $g->rect($x-2+305+54+50, $y-4, 50, 20);

    $saldo += $beloeb;
	$bilagid_prev = $bilagid;
  }
  if ($bilagid_prev != -1){
	 ## End Page
	 if ($saldo != 0) {
	   $y -=30;
	   $txt->font($arialbold, 14);
       $Numbeloeb = sprintf "%5.2f", $saldo;
	   $txt->translate($x+355,$y);
       $txt->text_right("Dif: $Numbeloeb");
	 }
     $g->stroke();
  }
  
  $pdf->save;
  $pdf->end( );

  $dbh_regnskab->disconnect();
}