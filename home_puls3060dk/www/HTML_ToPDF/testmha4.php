<?php 
$psfile = dirname(__FILE__) . '/psfile.ps';
unlink($psfile);
$pdffile = dirname(__FILE__) . '/psfile.pdf';
unlink($pdffile);
$ps = ps_new();

$Nyhedsbrevnr = 2;
$MånedÅr = 'Juni 2006';

$h1 = 'Elektronisk nyhedsbrev';
$p11 = 'Dette er første gang, hvor vi udsender vores nyhedsbrev fra Puls 3060 elektronisk. Fra nu af kan du hver den 1/3 - 1/6 - 1/9 - 1/12 udskrive et nyhedsbrev fra vores hjemmeside www.puls3060.dk under nyheder. Alle medlemmer, der har oplyst deres e-mailadresse får nyhedsbrevet direkte som e-mail. Andre kan blot sende de nødvendige oplysninger om mailadresse til post@puls3060.dk, så vil de fremover modtage nyhedsbrevet automatisk.';
$p12 = 'Der er sikkert stadig nogle af vores medlemmer, der ikke har internetadgang. Så kender du en træningskammerat, der derfor ikke får nyhedsbrevet, så gør ham eller hende en lille tjeneste, og udskriv et ekstra eksemplar til vedkommende.';
$p1 = "$p11\r\n$p12"; 


$path = "/usr/local/fonts/pdflib";

$ok = ps_open_file($ps, $psfile);
ps_set_info($ps, "Creator", "PHP-PS");
ps_set_info($ps, "Author", "Mogens Hafsjold");
ps_set_info($ps, "Title", "Text placement example");
$nyhedsbrev_logo = ps_open_image_file ($ps, 'eps', "/data/home/puls3060dk/www/fireworks/Nyhedsbrev-hoved.eps");

$ok = ps_set_parameter($ps, "FontAFM", "Helvetica=/usr/local/fonts/pdflib/Helvetica.afm");
$ok = ps_set_parameter($ps, "FontAFM", "Helvetica-Bold=/usr/local/fonts/pdflib/Helvetica-Bold.afm");
$ok = ps_set_parameter($ps, "FontAFM", "Times-Roman=/usr/local/fonts/pdflib/Times-Roman.afm");
$ok = ps_set_parameter($ps, "FontAFM", "Courier=/usr/local/fonts/pdflib/Courier.afm");
$ok = ps_set_parameter($ps, "FontAFM", "Univers-BlackExt=/usr/local/fonts/pdflib/Univers-BlackExt.afm");
$ok = ps_set_parameter($ps, "FontOutline", "Univers-BlackExt=/usr/local/fonts/pdflib/Univers-BlackExt.pfb");

$ok = ps_set_parameter($ps, "hyphendict", "/usr/local/fonts/pdflib/hyph_da_DK.dic");
$ok = ps_set_parameter($ps, "hyphenation", "true");
ps_set_value ($ps, 'hyphenminchar', 2);
$ok = ps_set_parameter($ps, "linebreak", "true"); 
//$ok = ps_set_parameter($ps, "parbreak", "true");
//ps_set_value ($ps, 'parindentskip', 1);  
//ps_set_value ($ps, 'parindent', 15);
//ps_set_value ($ps, 'numindentlines', 1); 

$Helvetica = ps_findfont($ps, "Helvetica", NULL, 0);
$HelveticaBold = ps_findfont($ps, "Helvetica-Bold", NULL, 0);
$TimesRoman = ps_findfont($ps, "Times-Roman" ,NULL, 0);
$Courier = ps_findfont($ps, "Courier" ,NULL, 0);
$psfont = ps_findfont($ps, "Univers-BlackExt", NULL, 1);
$y = 842; // A4 højde
$yd_logo = 117;
$y_top_tekst = $y - $yd_logo -35;
$y_bot_tekst = 10;

// Logo Template
$Logo = ps_begin_template ($ps, 596, 32);
//Nummer og Måned/År
$ok = ps_setfont($ps, $psfont, 12.0);
$rest = ps_show_boxed ( $ps, "Nr. $Nyhedsbrevnr",  28, 0, 117, 12, 'left' );
$rest = ps_show_boxed ( $ps, "$MånedÅr",  312, 0, 256, 12, 'right' );
ps_end_template($ps);

// Column Template
$cw = 170;
$ch = $y_top_tekst - $y_bot_tekst;
$column1 = ps_begin_template ($ps, $cw, $ch);
$h = $ch + 25 ;
for ($i=0; $i<2; $i++ ){
	//overskrift
	$h -= 25;
	$ok = ps_setfont($ps, $HelveticaBold, 11.0);
	$rest = ps_show_boxed($ps, $h1, 0, 0, $cw, $h, 'left');
	$h = ps_get_value ($ps, 'texty');
	
	//Børdtekst
	$ok = ps_setfont($ps, $TimesRoman, 10.0);
	$h -=  (ps_get_value ($ps, 'leading') - 6);
	$rest = ps_show_boxed($ps, $p1,  0, 0, $cw, $h, 'justify');
	$h = ps_get_value ($ps, 'texty');
	
	//$h -= ps_get_value ($ps, 'leading');
	//$rest = ps_show_boxed($ps, $p11,  0, 0, $cw, $h, 'justify');
	//$h = ps_get_value ($ps, 'texty');
}
ps_end_template($ps);


$ok = ps_begin_page($ps, 596, $y);

$ok = ps_place_image ($ps, $nyhedsbrev_logo, 28, 725, 1.0);
$ok = ps_place_image ($ps, $Logo, 0, 725, 1.0);
$ok = ps_place_image($ps, $column1, 28,10, 1.0);
$ok = ps_place_image($ps, $column1, 213,10, 1.0);
$ok = ps_place_image($ps, $column1, 398,10, 1.0);

//$ok = ps_symbol($ps, ord(A));
$symbol_name = ps_symbol_name($ps, ord(A));
$symbol_width_A = ps_symbol_width($ps, ord(A));
$symbol_width_blank = ps_symbol_width($ps, 32);


//$ok = ps_rect ($ps, 28, 700, 170, -690);
//$ok = ps_rect ($ps, 213, 700, 170, -690);
//$ok = ps_rect ($ps, 398, 700, 170, -690);
ps_moveto($ps, 28, 5);
ps_lineto($ps, 568, 5);
ps_stroke($ps);

   


//$ok = ps_set_value($ps, "textx", 10);
//$ok = ps_set_value($ps, "texty", 120);

//$ok = ps_setfont($ps, $Helvetica, 12.0);
//$ok = ps_show($ps, "Mogens Hafsjold nr 3 at (10, 120)");


$ok = ps_end_page($ps);
ps_close_image ($ps, $nyhedsbrev_logo);
$ok = ps_close($ps);
$ok = ps_delete($ps);

$tmp_result = array();
$retCode = 0;
$cmd = 'PATH=/sbin:/usr/sbin:/bin:/usr/bin:/usr/local/sbin:/usr/local/bin; ps2pdf ' . $psfile . ' ' . $pdffile . ' 2>&1';
exec($cmd, $tmp_result, $retCode);

?> 