<?php
    setlocale (LC_TIME, "da_DK.ISO8859-1");
    function DatoFormat($Sender)
	{
      return(strtolower(strftime("%A den %d. %B kl %H:%M", strtotime(substr($Sender,0,18)))));
	}

    if (isset($_REQUEST['AntalSider'])) 
      $AntalSider=$_REQUEST['AntalSider'];
    else
      $AntalSider = 1;

    include_once("conn.inc");  
    $dbLink = pg_connect($conn_www);

    $Query="SELECT  
              tblvaremodel.varekatalogid as varekatalogid, 
              tblvaremodel.levvarenr as levvarenr, 
              tblvarekatalog.kortbeskrivelse as kortbeskrivelse, 
              tblvaremodel.puls3060logo as puls3060logo, 
              tblvaremodel.salgspris as salgspris
	 		FROM tblvaremodel INNER JOIN tblvarekatalog 
	   		  ON tblvaremodel.varekatalogid = tblvarekatalog.id;";
    
    $dbResult = pg_query($dbLink, $Query);

    $pdf = pdf_new();
    pdf_set_parameter($pdf, "compatibility", "1.3");
    pdf_set_parameter($pdf, "resourcefile", "/usr/local/fonts/pdflib/pdflib.upr");
    pdf_open_file($pdf);

    pdf_set_info($pdf, "Subject", "Klubtøj");
    pdf_set_info($pdf, "Title", "Bestilling af Klubtøj");
    pdf_set_info($pdf, "Creator", "Puls 3060");
    pdf_set_info($pdf, "Author", "Mogens Hafsjold");
      
    $Arial=pdf_findfont($pdf, "Arial", "host", 0);
    $ArialBold=pdf_findfont($pdf, "Arial-Bold", "host", 0);
    $Times=pdf_findfont($pdf, "Times-Roman", "host", 0);
    $TimesBold=pdf_findfont($pdf, "Times-Bold", "host", 0);

    $template = pdf_begin_template($pdf, 612, 792);
      pdf_setlinewidth($pdf, 0.1);
      pdf_set_value($pdf, "textrendering", 0);
      
      $f=18;$x=20;$y=769;$w=555;$h=$f;
      pdf_setfont($pdf, $TimesBold, $f);
      pdf_show_boxed($pdf, "BESTILLING", $x, $y, $w, $h, "center");

      $f=12;$y-=($f+2);$h=$f;
      pdf_setfont($pdf, $TimesBold, $f);
      pdf_show_boxed($pdf, "AF", $x, $y, $w, $h, "center");
      
      $f=18;$y-=($f+2);$h=$f;
      pdf_setfont($pdf, $TimesBold, $f);
      pdf_show_boxed($pdf, "PULS 3060 KLUBTØJ", $x, $y, $w, $h, "center");
      
      $f=12;$y-=($f+2);$h=$f;
      pdf_setfont($pdf, $TimesBold, $f);
      pdf_show_boxed($pdf, "Efterår 2004", $x, $y, $w, $h, "center");

      $f=12;$y-=($f+8);$w=555;$h=$f;
	  $Ordernrx1=$x;$Ordernry1=$y;

      pdf_setlinewidth($pdf, 0.5);
      
      $f=10;$ft=$ArialBold;

      $t="Varenr";
      $x=45;$y=675;$w=40;$h=2*$f+4;
      pdf_setfont($pdf, $ft, $f);
      pdf_show_boxed($pdf, $t, $x, $y, $w, $h, "center");
      pdf_rect($pdf, $x, $y, $w, $h); pdf_closepath_stroke($pdf);

      $t="Navn";
      $x+=$w;$w=200;
      pdf_setfont($pdf, $ft, $f);
      pdf_show_boxed($pdf, $t, $x, $y, $w, $h, "center");
      pdf_rect($pdf, $x, $y, $w, $h); pdf_closepath_stroke($pdf);

      $t="Størrelser";
      $x+=$w;$w=56;
      pdf_setfont($pdf, $ft, $f);
      pdf_show_boxed($pdf, $t, $x, $y, $w, $h, "center");
      pdf_rect($pdf, $x, $y, $w, $h); pdf_closepath_stroke($pdf);

      $t="Med logo";
      $x+=$w;$w=28;
      pdf_setfont($pdf, $ft, $f);
      pdf_show_boxed($pdf, $t, $x, $y, $w, $h, "center");
      pdf_rect($pdf, $x, $y, $w, $h); pdf_closepath_stroke($pdf);

      $t="Pris";
      $x+=$w;$w=50;
      pdf_setfont($pdf, $ft, $f);
      pdf_show_boxed($pdf, $t, $x, $y, $w, $h, "center");
      $w+=2;
      pdf_rect($pdf, $x, $y, $w, $h); pdf_closepath_stroke($pdf);

      $x1=$x;$y1=$y;$w1=$w;$h1=$h;

      $t="Bestilling";
      $x+=$w;$w=130;$h=$f+2;$y+=$h;
      pdf_setfont($pdf, $ft, $f);
      pdf_show_boxed($pdf, $t, $x, $y, $w, $h, "center");
      pdf_rect($pdf, $x, $y, $w, $h); pdf_closepath_stroke($pdf);

      $x=$x1;$y=$y1;$w=$w1;$h=$h1;

      $t="Størrelse";
      $x+=$w;$w=50;$h=$f+2;
      pdf_setfont($pdf, $ft, $f);
      pdf_show_boxed($pdf, $t, $x, $y, $w, $h, "center");
      pdf_rect($pdf, $x, $y, $w, $h); pdf_closepath_stroke($pdf);

      $t="Antal";
      $x+=$w;$w=30;
      pdf_setfont($pdf, $ft, $f);
      pdf_show_boxed($pdf, $t, $x, $y, $w, $h, "center");
      pdf_rect($pdf, $x, $y, $w, $h); pdf_closepath_stroke($pdf);

      $t="Beløb";
      $x+=$w;$w=50;
      pdf_setfont($pdf, $ft, $f);
      pdf_show_boxed($pdf, $t, $x, $y, $w, $h, "center");
      pdf_rect($pdf, $x, $y, $w, $h); pdf_closepath_stroke($pdf);

      $f=9;$ft=$Arial;

      $t3="S - XLL";

      while($row = pg_fetch_array($dbResult, NULL , PGSQL_ASSOC))
	  {
        $x=45;$w=40;$h=$f+4;$y-=$h;
        pdf_setfont($pdf, $ft, $f);
        pdf_show_boxed($pdf, $row["levvarenr"], $x, $y, $w, $h, "center");
        pdf_rect($pdf, $x, $y, $w, $h); pdf_closepath_stroke($pdf);

        $x+=$w;$w=200;
        pdf_setfont($pdf, $ft, $f);
        pdf_show_boxed($pdf, $row["kortbeskrivelse"], $x+2, $y, $w, $h, "left");
        pdf_rect($pdf, $x, $y, $w, $h); pdf_closepath_stroke($pdf);

        $x+=$w;$w=56;
        pdf_setfont($pdf, $ft, $f);
        pdf_show_boxed($pdf, $t3, $x, $y, $w, $h, "center");
        pdf_rect($pdf, $x, $y, $w, $h); pdf_closepath_stroke($pdf);

        if ($row["puls3060logo"]=="J")
		  $t4="Ja";
		else
		  $t4="";
        $x+=$w;$w=28;
        pdf_setfont($pdf, $ft, $f);
        pdf_show_boxed($pdf, $t4, $x, $y, $w, $h, "center");
        pdf_rect($pdf, $x, $y, $w, $h); pdf_closepath_stroke($pdf);

        $x+=$w;$w=50;
        pdf_setfont($pdf, $ft, $f);
        pdf_show_boxed($pdf, $row["salgspris"], $x, $y, $w, $h, "right");
        $w+=2;
        pdf_rect($pdf, $x, $y, $w, $h); pdf_closepath_stroke($pdf);

        $x+=$w;$w=50;
        pdf_rect($pdf, $x, $y, $w, $h); pdf_closepath_stroke($pdf);

        $x+=$w;$w=30;
        pdf_rect($pdf, $x, $y, $w, $h); pdf_closepath_stroke($pdf);

        $x+=$w;$w=50;
        pdf_rect($pdf, $x, $y, $w, $h); pdf_closepath_stroke($pdf);
	  }

      $f=10;$ft=$ArialBold;
      $t="I alt ";
      $x=45;$w=456;$h=$f+4;$y-=$h;
      pdf_setfont($pdf, $ft, $f);
      pdf_show_boxed($pdf, $t, $x, $y, $w, $h, "right");

      $x+=$w;$w=50;
      pdf_rect($pdf, $x, $y, $w, $h); pdf_closepath_stroke($pdf);

	  $f=10;$ft=$Arial;
      $t="Kan ikke anvendes til bestilling efter 23. oktober 2004.";
      $x=45;$w=350;$h=$f+4;$y-=$h;
      pdf_setfont($pdf, $ft, $f);
      pdf_show_boxed($pdf, $t, $x, $y, $w, $h, "left");
      
	  $y-=$h;
      
	  $f=10;$ft=$Arial;
      $t="Bestildt af:";
      $x=45;$w=75;$h=$f+4;$y-=$h;
      pdf_setfont($pdf, $ft, $f);
      pdf_show_boxed($pdf, $t, $x, $y, $w, $h, "left");
      
      $t="Navn ";
      $x=45;$h=$f+4;$y-=$h;
      pdf_setfont($pdf, $ft, $f);
      pdf_show_boxed($pdf, $t, $x, $y, $w, $h, "right");
      pdf_rect($pdf, $x, $y, $w, $h); pdf_closepath_stroke($pdf);
      pdf_rect($pdf, $x+$w, $y, 200, $h); pdf_closepath_stroke($pdf);

      $t="Adresse ";
      $x=45;$h=2*$f+4;$y-=$h;
      pdf_setfont($pdf, $ft, $f);
      pdf_show_boxed($pdf, $t, $x, $y, $w, $h, "right");
      pdf_rect($pdf, $x, $y, $w, $h); pdf_closepath_stroke($pdf);
      pdf_rect($pdf, $x+$w, $y+$f+2, 200, $f+2); pdf_closepath_stroke($pdf);
      pdf_rect($pdf, $x+$w, $y, 200, $f+2); pdf_closepath_stroke($pdf);

      $t="Postnr/By ";
      $x=45;$h=$f+4;$y-=$h;
      pdf_setfont($pdf, $ft, $f);
      pdf_show_boxed($pdf, $t, $x, $y, $w, $h, "right");
      pdf_rect($pdf, $x, $y, $w, $h); pdf_closepath_stroke($pdf);
      pdf_rect($pdf, $x+$w, $y, 200, $h); pdf_closepath_stroke($pdf);

      $t="Telefon ";
      $x=45;$h=$f+4;$y-=$h;
      pdf_setfont($pdf, $ft, $f);
      pdf_show_boxed($pdf, $t, $x, $y, $w, $h, "right");
      pdf_rect($pdf, $x, $y, $w, $h); pdf_closepath_stroke($pdf);
      pdf_rect($pdf, $x+$w, $y, 200, $h); pdf_closepath_stroke($pdf);

      $t="e-mail adresse ";
      $x=45;$h=$f+4;$y-=$h;
      pdf_setfont($pdf, $ft, $f);
      pdf_show_boxed($pdf, $t, $x, $y, $w, $h, "right");
      pdf_rect($pdf, $x, $y, $w, $h); pdf_closepath_stroke($pdf);
      pdf_rect($pdf, $x+$w, $y, 200, $h); pdf_closepath_stroke($pdf);

      $y-=25;
      
      $x=45;$h=$f+4;$y-=$h;
      pdf_moveto($pdf, $x, $y);pdf_lineto($pdf, $x+60, $y);pdf_closepath_fill_stroke($pdf);
      pdf_moveto($pdf, $x+75, $y);pdf_lineto($pdf, $x+275, $y);pdf_closepath_fill_stroke($pdf);

      $x=45;$h=$f;$y-=$h;
      pdf_setfont($pdf, $ft, $f);
      pdf_show_boxed($pdf, "Dato", $x, $y, 60, $h, "left");
      pdf_show_boxed($pdf, "Købers underskrift", $x+75, $y, 200, $h, "left");

      $y-=25;
      $t="I alt beløbet skal indbetales på Puls 3060's bankkonto i Danske Bank - Reg.nr. ";
      $x=45;$h=$f+4;$y-=$h;
      pdf_setfont($pdf, $ft, $f);
      pdf_show_xy($pdf, $t, $x, $y);
      $textx = pdf_get_value($pdf, "textx"); $texty = pdf_get_value($pdf, "texty");
      
      $f=12;$ft=$ArialBold;
      $t="3183";
      pdf_setfont($pdf, $ft, $f);
      pdf_show_xy($pdf, $t, $textx, $y);
      $textx = pdf_get_value($pdf, "textx"); $texty = pdf_get_value($pdf, "texty");

      $f=10;$ft=$Arial;
      $t=" kontonr. ";
      pdf_setfont($pdf, $ft, $f);
      pdf_show_xy($pdf, $t, $textx+1, $y);
      $textx = pdf_get_value($pdf, "textx"); $texty = pdf_get_value($pdf, "texty");
      
      $f=12;$ft=$ArialBold;
      $t="141 79 67";
      pdf_setfont($pdf, $ft, $f);
      pdf_show_xy($pdf, $t, $textx, $y);
      $textx = pdf_get_value($pdf, "textx"); $texty = pdf_get_value($pdf, "texty");

      $f=10;$ft=$Arial;
      $t="mærket med ";
      $x=45;$h=$f+4;$y-=$h;
      pdf_setfont($pdf, $ft, $f);
      pdf_show_xy($pdf, $t, $x, $y);
      $Ordernrx2 = pdf_get_value($pdf, "textx"); $Ordernry2 = pdf_get_value($pdf, "texty");

      $y-=25;
      $f=10;$ft=$Arial;
      $t="Når betalingen er foretaget sendes/afleveres bestillingen til klubbens kasserer";
      $x=45;$h=$f+4;$y-=$h;$w=522;
      pdf_setfont($pdf, $ft, $f);
      pdf_show_boxed($pdf, $t, $x, $y, $w, $h, "left");

      $t="Eva Risum Mortensen";
      $x=45;$h=$f+4;$y-=$h;$w=522;
      pdf_setfont($pdf, $ft, $f);
      pdf_show_boxed($pdf, $t, $x+50, $y, $w, $h, "left");

      $t="Jettevej 3";
      $x=45;$h=$f+4;$y-=$h;$w=522;
      pdf_setfont($pdf, $ft, $f);
      pdf_show_boxed($pdf, $t, $x+50, $y, $w, $h, "left");

      $t="3060 Espergærde";
      $x=45;$h=$f+4;$y-=$h;$w=522;
      pdf_setfont($pdf, $ft, $f);
      pdf_show_boxed($pdf, $t, $x+50, $y, $w, $h, "left");

      $y-=25;
      $t="Når kassereren har registreret betalingen vil bestillingen indgå i den næste samlede pulje af bestillinger til Puls 3060's leverandør";
      $x=45;$h=2*$f+4;$y-=$h;$w=522;
      pdf_setfont($pdf, $ft, $f);
      pdf_show_boxed($pdf, $t, $x, $y, $w, $h, "left");

      $y-=25;
      
      $x=45;$h=$f+4;$y-=$h;
      pdf_moveto($pdf, $x, $y);pdf_lineto($pdf, $x+60, $y);pdf_closepath_fill_stroke($pdf);
      pdf_moveto($pdf, $x+75, $y);pdf_lineto($pdf, $x+275, $y);pdf_closepath_fill_stroke($pdf);

      $f=8;$ft=$Arial;
      $x=45;$h=2*$f;$y-=$h;
      pdf_setfont($pdf, $ft, $f);
      pdf_show_boxed($pdf, "Dato", $x, $y, 60, $h, "left");
      pdf_show_boxed($pdf, "Kassererens underskrift for registrering af betaling", $x+75, $y, 200, $h, "left");

      $f=8;$ft=$TimesBold;
      pdf_setfont($pdf, $ft, 8);
      pdf_show_boxed($pdf, "Udgave 7 - 10. juni 2004", 20, 20, 555, 8, "right");
    pdf_end_template($pdf);


    for ($i=0; $i<$AntalSider; $i++)
    {
      $Query="INSERT INTO tblbestilling (udskriftdato) values(now());
              SELECT currval('tblbestilling_id_seq') as ordrenr;";
      $dbResult = pg_query($dbLink, $Query);
      $row = pg_fetch_array($dbResult, NULL , PGSQL_ASSOC);
      $Ordernr=$row["ordrenr"];
      
      pdf_begin_page($pdf, 612, 792);
        pdf_place_image($pdf, $template, 0, 0, 1.0);
        pdf_set_value($pdf, "textrendering", 0);

        $f=12;$ft=$TimesBold;
        $w=555;$h=$f;
        pdf_setfont($pdf, $TimesBold, $f);
        pdf_show_boxed($pdf, "Ordernr: $Ordernr", $Ordernrx1, $Ordernry1, $w, $h, "center");

        $f=12;$ft=$ArialBold;
        $t="Klubtøj-$Ordernr";
        $Ordernr++;
        pdf_setfont($pdf, $ft, $f);
        pdf_show_xy($pdf, $t, $Ordernrx2, $Ordernry2);

      pdf_end_page($pdf);
    }
    pdf_close_image($pdf, $template);
    pdf_close($pdf);

    $data = pdf_get_buffer($pdf);

    header("Content-type: application/pdf");
    header("Content-disposition: inline; filename=klubtoj.pdf");
    header("Content-length: " . strlen($data));

    echo $data;

?>