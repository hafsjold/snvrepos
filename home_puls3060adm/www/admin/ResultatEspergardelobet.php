<?php
setlocale (LC_TIME, "da_DK.ISO8859-1");
function DatoFormat($Sender)
{
	return(strtolower(strftime("%A den %d. %B kl %H:%M", strtotime(substr($Sender,0,18)))));
}
function TimeFormat($Sender)
{
	return(strtolower(strftime("%H:%M:%S", strtotime(substr($Sender,0,8)))));
}

if (isset($_REQUEST['LobId']))
$LobId=$_REQUEST['LobId'];
	else
$LobId = 69;

if (isset($_REQUEST['SortId']))
$SortId=$_REQUEST['SortId'];
	else
$SortId = 'a';

include_once("conn.inc");
$dbLink = pg_connect($conn_www);

$Query="SELECT *, now() as nu FROM tbllob WHERE Id = $LobId;";
$dbResult = pg_query($dbLink, $Query);
$row = pg_fetch_array($dbResult, NULL , PGSQL_ASSOC);
$nu = $row["nu"];
$lobnavn = $row["navn"];
$dato = $row["dato"];

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


	$Query="
        SELECT tblafdeling.afdnavn, 
               tblgrup.grup, 
               (tblperson.fornavn || ' ' || tblperson.efternavn) As navn, 
               vresultat.tid
        FROM ((vresultat LEFT 
          JOIN tblafdeling ON vresultat.afdeling = tblafdeling.id) 
          LEFT JOIN tblgrup ON vresultat.gruppe = tblgrup.id) 
          LEFT JOIN tblperson ON vresultat.personid = tblperson.id
        WHERE (((vresultat.lobid)=$LobId))
        ORDER BY vresultat.sortkode, vresultat.gruppe, vresultat.tid
        ;";

	$dbResult = pg_query($dbLink, $Query);

	$Line = 0; $Page = 0; $col = 0; $afdnavn = ""; $grup = "";
	while($row = pg_fetch_array($dbResult, NULL , PGSQL_ASSOC)) {
		$Line++;

		if ($afdnavn <> $row["afdnavn"]) {
			if (($col == 1)
			&& ($Line > 40))
			$Line = 46;

			if (($col == 2)
			&& ($Line > 85))
			$Line = 91;
		}
		if ($grup <> $row["grup"]) {
			if (($col == 1)
			&& ($Line > 40))
			$Line = 46;

			if (($col == 2)
			&& ($Line > 85))
			$Line = 91;
		}

		if ($Line == 91) {
			$p->end_page_ext("");
			$Line = 1;
		}

		if ($Line == 1) {
			$p->begin_page_ext(612, 792, "");
			$p->set_value("textrendering", 0);
			$p->setlinewidth(0.5);

			$f=20;$x=20;$y=775;$w=555;$h=$f;
			$p->setfont($TimesBold, $f);
			$p->show_boxed($lobnavn, $x, $y, $w, $h, "center", "");

			$f=20;$x=20;$y=750;$w=555;$h=$f;
			$p->setfont($TimesBold, $f);
			$p->show_boxed(DatoFormat($dato), $x, $y, $w, $h, "center", "");

			$f=20;$x=20;$y=710;$w=555;$h=$f;
			$p->setfont($TimesBold, $f);
			$p->show_boxed("Resultatliste", $x, $y, $w, $h, "center", "");

			$f=10;
			$ft=$ArialBold;

			$col = 1;
			$startx=45;
			$y=675;
			$h=15;
		}

		if ($Line == 46) {
			$col = 2;
			$startx=320;
			$y=675;
			$h=15;
		}

		if ($afdnavn <> $row["afdnavn"]){
			if (($col == 1) && ($Line > 1)) $y-=2*$h;
			if (($col == 2) && ($Line > 46)) $y-=2*$h;
			$t=$row["afdnavn"];
			$x=$startx;$w=175;
			$p->setfont($TimesBold, 16);
			$p->show_boxed($t, $x, $y, $w, 17, "left", "");
			$afdnavn = $row["afdnavn"];

			$y-=2*$h;
			$t=$row["grup"];
			$x=$startx;$w=175;
			$p->setfont($TimesBold, 12);
			$p->show_boxed($t, $x, $y, $w, $h, "left", "");
			$grup=$row["grup"];
			$Line += 4;
		}

		if ($grup <> $row["grup"]) {
			if (($col == 1) && ($Line > 1)) $y-=2*$h;
			if (($col == 2) && ($Line > 46)) $y-=2*$h;
			$t=$row["grup"];
			$x=$startx;$w=175;
			$p->setfont($TimesBold, 12);
			$p->show_boxed($t, $x, $y, $w, $h, "left", "");
			$grup=$row["grup"];
			$Line += 2;
		}

		$y-=$h;

		$t=$row["navn"];
		$x=$startx;$w=175;
		$p->setfont($ft, $f);
		$p->show_boxed($t, $x+2, $y, $w, $h, "left", "");
		$p->rect($x, $y, $w, $h); $p->closepath_stroke();

		$t=TimeFormat($row["tid"]);
		$x+=$w;$w=60;
		$p->setfont($ft, $f);
		$p->show_boxed($t, $x, $y, $w, $h, "center", "");
		$p->rect($x, $y, $w, $h); $p->closepath_stroke();
	}

	$p->end_page_ext("");
    $p->end_document("");

	$data = $p->get_buffer();

	header("Content-type: application/pdf");
	header("Content-disposition: inline; filename=ResultatEspergaerdelobet.pdf");
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