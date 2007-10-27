<?php
    setlocale (LC_TIME, "da_DK.ISO8859-1");
    function DatoFormat($Sender)
	{
      return(strtolower(strftime("%A den %d. %B kl %H:%M", strtotime(substr($Sender,0,18)))));
	}

    if (isset($_REQUEST['LobId'])) 
      $LobId=$_REQUEST['LobId'];
    else
      $LobId = 42;

    if (isset($_REQUEST['SortId'])) 
      $SortId=$_REQUEST['SortId'];
    else
      $SortId = 'a';

    include_once("conn.inc");  
    $dbLink = pg_connect($conn_www);

    $Query="SELECT *, now() as nu FROM tbllob WHERE Id = $LobId;";
    $dbResult = pg_query($dbLink, $Query);
    $row = pg_fetch_array($dbResult, NULL , PGSQL_ASSOC);
try {

    $p = new PDFlib();

    $p->set_parameter("compatibility", "1.3");
    $p->set_parameter("resourcefile", "/usr/local/fonts/pdflib/pdflib.upr");

    if ($p->begin_document("", "") == 0) {
        die("Error: " . $p->get_errmsg());
    }

    $p->set_info("Subject", "Tilmelding til " . $row["navn"]);
    $p->set_info("Title", $row["navn"]);
    $p->set_info("Creator", "Puls 3060");
    $p->set_info("Author", "Mogens Hafsjold");

    $Arial=$p->load_font("Arial", "host", "");
    $ArialBold=$p->load_font("Arial-Bold", "host", "");
    $Times=$p->load_font("Times-Roman", "host", "");
    $TimesBold=$p->load_font("Times-Bold", "host", "");
 
    $template = $p->begin_template(612, 792);
      $p->setlinewidth(0.1);
      $p->set_value("textrendering", 0);
      
      $f=20;$x=20;$y=775;$w=555;$h=$f;
      $p->setfont($TimesBold, $f);
      $p->show_boxed($row["navn"], $x, $y, $w, $h, "center", "");

      $f=20;$x=20;$y=750;$w=555;$h=$f;
      $p->setfont($TimesBold, $f);
      $p->show_boxed(DatoFormat($row["dato"]), $x, $y, $w, $h, "center", "");
 
      $p->setlinewidth(0.5);
   
      $f=16;$ft=$ArialBold;
      $y=700;$h=27;
      
      $x=45;$w=540;
      $p->setColor("fill", "gray", 0.8, 0, 0, 0); 
      $p->rect($x, $y, $w, $h);
      $p->fill();
      $p->setColor("fill", "rgb",0, 0, 0, 0); 
      
      $t="NR";
      $w=50;
      $p->setfont($ft, $f);
      $p->show_boxed($t, $x, $y, $w, $h, "center", "");
      $p->rect($x, $y, $w, $h); $p->closepath_stroke();

      $t="";
      $x+=$w;$w=40;
      $p->setfont($ft, $f);
      $p->show_boxed($t, $x, $y, $w, $h, "center", "");
      $p->rect($x, $y, $w, $h); $p->closepath_stroke();

      $t="NAVN";
      $x+=$w;$w=225;
      $p->setfont($ft, $f);
      $p->show_boxed($t, $x+2, $y, $w-4, $h, "left", "");
      $p->rect($x, $y, $w, $h); $p->closepath_stroke();

      $t="AFD";
      $x+=$w;$w=150;
      $p->setfont($ft, $f);
      $p->show_boxed($t, $x+2, $y, $w-4, $h, "left", "");
      $p->rect($x, $y, $w, $h); $p->closepath_stroke();

      $t="AFGIFT";
      $x+=$w;$w=75;
      $p->setfont($ft, $f);
      $p->show_boxed($t, $x, $y, $w, $h, "center", "");
      $p->rect($x, $y, $w, $h); $p->closepath_stroke();
    
      $f=8;$x=45;$y=5;$w=550;$h=$f;
      $p->setfont($ft, $f);
      $p->show_boxed("Udskrevet " . date("Y-m-d H:i:s"), $x, $y, $w, $h, "left", "");
    $p->end_template();

    
    //$Query="SELECT * FROM vA5tilmelding WHERE nr_udleveret IS NULL AND LobId = " . $LobId . " ORDER BY fornavn, efternavn;";
    $Query="SELECT Upper(fornavn) as fn, Upper(efternavn) as en, * FROM vA5tilmelding WHERE LobId = " . $LobId . " ORDER BY fn, en;";

    $dbResult = pg_query($dbLink, $Query);
     
	$Line = 0; $Page = 0;
    while($row = pg_fetch_array($dbResult, NULL , PGSQL_ASSOC)) {
	  $Line++;
        
      if ($Line == 26) {
        $f=8;$x=45;$y=10;$w=540;$h=$f;
        $p->setfont($ft, $f);
        $p->show_boxed("Side " . ++$Page, $x, $y, $w, $h, "right", "");
        $p->end_page_ext("");
        $Line = 1;	  
      }
       
      if ($Line == 1) {
        $p->begin_page_ext(612, 792, "");
        $p->fit_image ($template, 0, 0, "");
        $p->set_value("textrendering", 0);
	    $p->setlinewidth(0.5);
        
        $f=16;$ft=$ArialBold;
        $y=700;$h=27;
	  }
      $y-=$h;

	  $t=$row["nummer"];
      $x=45;$w=50;
      $p->setfont($ft, $f);
      $p->show_boxed($t, $x, $y, $w, $h, "center", "");
      $p->rect($x, $y, $w, $h); $p->closepath_stroke();

      $t="";
      $x+=$w;$w=40;
      $p->setfont($ft, $f);
      $p->show_boxed($t, $x, $y, $w, $h, "center", "");
      $p->rect($x, $y, $w, $h); $p->closepath_stroke();

      $t=$row["fornavn"] . " " . $row["efternavn"];
      $x+=$w;$w=225;
      $p->setfont($ft, $f);
      $p->show_boxed($t, $x+2, $y, $w-4, $h, "left", "");
      $p->rect($x, $y, $w, $h); $p->closepath_stroke();

      $t=$row["afdnavn"];
      $x+=$w;$w=150;
      $p->setfont($ft, $f);
      $p->show_boxed($t, $x+2, $y, $w-4, $h, "left", "");
      $p->rect($x, $y, $w, $h); $p->closepath_stroke();

      if ($row["ordernum"] > 0) {
		$t="DK";	  
	  }
	  else {
		$t=$row["lobsafgift"];
	  }
      $x+=$w;$w=75;
      $p->setfont($ft, $f);
      $p->show_boxed($t, $x, $y, $w, $h, "center", "");
      $p->rect($x, $y, $w, $h); $p->closepath_stroke();
	
	}
    $p->end_page_ext("");
    $p->close_image($template);
    $p->end_document("");
  
    $data = $p->get_buffer();

    header("Content-type: application/pdf");
    header("Content-disposition: inline; filename=TilmeldingEspergaerdeloeb.pdf");
    header("Content-length: " . strlen($data));

    print $data;
}
catch (PDFlibException $e) {
   die("PDFlib exception occurred in TilmeldingEspergaerdelobet2.php:\n" .
   "[" . $e->get_errnum() . "] " . $e->get_apiname() . ": " .
   $e->get_errmsg() . "\n");
}
catch (Exception $e) {
   die($e);
}
?>