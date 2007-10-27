<?php
    setlocale (LC_TIME, "da_DK.ISO8859-1");
    function DatoFormat($Sender)
	{
      return(strtolower(strftime("%A den %d. %B kl %H:%M", strtotime(substr($Sender,0,18)))));
	}

    include_once("conn.inc");  
    $dbLink = pg_connect($conn_www);

try {

	$p = new PDFlib();

    $p->set_parameter("compatibility", "1.3");
    $p->set_parameter("resourcefile", "/usr/local/fonts/pdflib/pdflib.upr");

    if ($p->begin_document("", "") == 0) {
        die("Error: " . $p->get_errmsg());
    }

    $p->set_info("Subject", "Har deltaget i Espergærdeløb");
    $p->set_info("Title", "Har deltaget i Espergærdeløb");
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
      $p->show_boxed("Har deltaget i Espergærdeløb", $x, $y, $w, $h, "center", "");

      $p->setlinewidth(0.5);
      
      $f=12;$ft=$ArialBold;
      $y=725;$h=27;
      
      $x=45;$w=540;
      $p->setColor("fill", "gray",0.8, 0, 0, 0); 
      $p->rect($x, $y, $w, $h);
      $p->fill();
      $p->setColor("fill", "rgb",0, 0, 0, 0); 
      
      $t="p3060 ref";
      $w=40;
      $p->setfont($ft, $f);
      $p->show_boxed($t, $x, $y, $w, $h, "center", "");
      $p->rect($x, $y, $w, $h); $p->closepath_stroke();

      $t="Navn";
      $x+=$w;$w=225;
      $p->setfont($ft, $f);
      $p->show_boxed($t, $x+2, $y, $w-4, $h, "left", "");
      $p->rect($x, $y, $w, $h); $p->closepath_stroke();

      $t="Adresse";
      $x+=$w;$w=200;
      $p->setfont($ft, $f);
      $p->show_boxed($t, $x+2, $y, $w-4, $h, "left", "");
      $p->rect($x, $y, $w, $h); $p->closepath_stroke();

      $t="Post nr";
      $x+=$w;$w=30;
      $p->setfont($ft, $f);
      $p->show_boxed($t, $x, $y, $w, $h, "center", "");
      $p->rect($x, $y, $w, $h); $p->closepath_stroke();

      $t="Fodt år";
      $x+=$w;$w=30;
      $p->setfont($ft, $f);
      $p->show_boxed($t, $x, $y, $w, $h, "center", "");
      $p->rect($x, $y, $w, $h); $p->closepath_stroke();

      $t="M K";
      $x+=$w;$w=15;
      $p->setfont($ft, $f);
      $p->show_boxed($t, $x, $y, $w, $h, "center", "");
      $p->rect($x, $y, $w, $h); $p->closepath_stroke();
    
      $f=8;$x=45;$y=10;$w=550;$h=$f;
      $p->setfont($ft, $f);
      $p->show_boxed("Udskrevet " . date("Y-m-d H:i:s"), $x, $y, $w, $h, "left", "");

    $p->end_template();

    
    $Query="SELECT * FROM vhardeltagetiespergaerdelob;";
    $dbResult = pg_query($dbLink, $Query);
    
	$Line = 0; $Page = 0;
    while($row = pg_fetch_array($dbResult, NULL , PGSQL_ASSOC)) {
	  $Line++;
      if ($Line == 36) {
        $f=8;$x=45;$y=10;$w=540;$h=$f;
        $p->setfont($ft, $f);
        $p->show_boxed("Side " . ++$Page, $x, $y, $w, $h, "right", "");
        $p->end_page_ext("");
        $Line = 1;	  
      }
      
      if ($Line == 1) {
        $p->begin_page_ext(612, 792, "");
        $p->fit_image($template, 0, 0, "");
        $p->set_value("textrendering", 0);
	    $p->setlinewidth(0.5);
        
        $f=12;$ft=$ArialBold;
        $y=725;$h=20;
	  }
      $y-=$h;
      
	  $t=$row["p3060_ref"];
      $x=45;$w=40;
      $p->setfont($ft, $f);
      $p->show_boxed($t, $x, $y, $w, $h, "center", "");
      $p->rect($x, $y, $w, $h); $p->closepath_stroke();

	  $t=$row["navn"];
      $x+=$w;$w=225;
      $p->setfont($ft, $f);
      $p->show_boxed($t, $x+2, $y, $w-4, $h, "left", "");
      $p->rect($x, $y, $w, $h); $p->closepath_stroke();

      $t=$row["adresse"];
      $x+=$w;$w=200;
      $p->setfont($ft, $f);
      $p->show_boxed($t, $x+2, $y, $w-4, $h, "left", "");
      $p->rect($x, $y, $w, $h); $p->closepath_stroke();

      $t=$row["postnr"];
      $x+=$w;$w=30;
      $p->setfont($ft, $f);
      $p->show_boxed($t, $x, $y, $w, $h, "center", "");
      $p->rect($x, $y, $w, $h); $p->closepath_stroke();

      $t=$row["fodtaar"];
      $x+=$w;$w=30;
      $p->setfont($ft, $f);
      $p->show_boxed($t, $x, $y, $w, $h, "center", "");
      $p->rect($x, $y, $w, $h); $p->closepath_stroke();
	
      $t=$row["mk"];
      $x+=$w;$w=15;
      $p->setfont($ft, $f);
      $p->show_boxed($t, $x, $y, $w, $h, "center", "");
      $p->rect($x, $y, $w, $h); $p->closepath_stroke();
	}
    $p->end_page_ext("");
    $p->close_image($template);
    $p->end_document("");
  
    $data = $p->get_buffer();

    header("Content-type: application/pdf");
    header("Content-disposition: inline; filename=HarDeltagetIEspergardelobet.pdf");
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

