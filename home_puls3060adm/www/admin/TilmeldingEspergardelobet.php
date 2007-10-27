<?
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

    $pdf = pdf_new();
    pdf_set_parameter($pdf, "compatibility", "1.3");
    pdf_set_parameter($pdf, "resourcefile", "/usr/local/fonts/pdflib/pdflib.upr");
    pdf_open_file($pdf);

    pdf_set_info($pdf, "Subject", "Tilmelding til " . $row["navn"]);
    pdf_set_info($pdf, "Title", $row["navn"]);
    pdf_set_info($pdf, "Creator", "Puls 3060");
    pdf_set_info($pdf, "Author", "Mogens Hafsjold");

    $template = pdf_begin_template($pdf, 421, 595);
      pdf_set_value($pdf, "textrendering", 0);
//      pdf_set_font($pdf, "Times-Bold", 32, "host");
      pdf_findfont($pdf, "Arial", "host", 0);
      pdf_set_font($pdf, "Arial", 32, "host");
      pdf_show_boxed($pdf, $row["navn"], 10, 547, 401, 32, "center");
      pdf_set_font($pdf, "Times-Bold", 18, "host");
      pdf_show_boxed($pdf, DatoFormat($row["dato"]), 10, 505, 401, 18, "center");
      pdf_set_font($pdf, "Times-Bold", 12, "host");
      pdf_show_xy($pdf, "FORNAVN", 25, 450);
      pdf_show_xy($pdf, "EFTERNAVN", 25, 426);
      pdf_show_xy($pdf, "ADRESSE", 25, 402);
      pdf_show_xy($pdf, "POSTNR / BY", 25, 378);
      pdf_show_xy($pdf, "TELEFON", 25, 354);
      pdf_show_xy($pdf, "E-MAIL", 25, 330);
      pdf_show_xy($pdf, "FØDSELSÅR", 25, 306);
      pdf_show_xy($pdf, "AFDELING", 25, 282);
      pdf_show_xy($pdf, "LØBSAFGIFT", 25, 258);

      pdf_setlinewidth($pdf, 0.5);
      pdf_moveto($pdf, 10, 234);
      pdf_lineto($pdf, 401, 234);
      pdf_closepath_fill_stroke($pdf);
      
      pdf_set_font($pdf, "Times-Bold", 8, "host");
      pdf_show_boxed($pdf, "Herunder udfyldes ikke", 10, 224, 401, 8, "center");

      pdf_set_font($pdf, "Times-Bold", 12, "host");
      pdf_show_xy($pdf, "LØBSNR.", 25, 186);
      pdf_show_xy($pdf, "P3060-REF", 25, 162);
      pdf_show_xy($pdf, "AFD-ID", 25, 138);
      
      pdf_set_font($pdf, "Times-Roman", 8, "host");
      pdf_show_boxed($pdf, "Genereret af TilmeldingEspergardelobet.php - " . DatoFormat($row["nu"]), 10, 20, 401, 8, "center");
    pdf_end_template($pdf);

    if ($SortId == 'a')
      $Query="SELECT * FROM vA5tilmelding WHERE LobId = " . $LobId . " ORDER BY fornavn, efternavn;";
    else
      $Query="SELECT * FROM vA5tilmelding WHERE LobId = " . $LobId . " ORDER BY nummer desc;";

    $dbResult = pg_query($dbLink, $Query);

    while($row = pg_fetch_array($dbResult, NULL , PGSQL_ASSOC))
    {
      pdf_begin_page($pdf, 421, 595);
        pdf_place_image($pdf, $template, 0, 0, 1.0);
        pdf_set_font($pdf, "Times-Roman", 12, "host");
        pdf_set_value($pdf, "textrendering", 0);
        pdf_show_xy($pdf, $row["fornavn"], 110, 450);
        pdf_show_xy($pdf, $row["efternavn"], 110, 426);
        pdf_show_xy($pdf, $row["adresse"], 110, 402);
        pdf_show_xy($pdf, $row["postnr"], 110, 378);
        pdf_show_xy($pdf, $row["bynavn"], 170, 378);
        pdf_show_xy($pdf, $row["tlfnr"], 110, 354);
        pdf_show_xy($pdf, $row["mailadr"], 110, 330);
        pdf_show_xy($pdf, $row["fodtaar"], 110, 306);
        If ($row["kon"] == "m")
          pdf_show_xy($pdf, "Mand", 170, 306);
        else
          pdf_show_xy($pdf, "Kvinde", 170, 306);
        pdf_show_xy($pdf, $row["afdnavn"], 110, 282);
        pdf_show_xy($pdf, $row["lobsafgift"], 110, 258);
        If ($row["harbetaltdato"])
          pdf_show_xy($pdf, "Har betalt " . $row["harbetaltdato"], 170, 258);

        pdf_show_xy($pdf, $row["nummer"], 110, 186);
        pdf_show_xy($pdf, $row["personid"], 110, 162);
        pdf_show_xy($pdf, $row["afdid"], 110, 138);
      pdf_end_page($pdf);
    }
    pg_close($dbLink); 
    
    pdf_close_image($pdf, $template);
    pdf_close($pdf);

    $data = pdf_get_buffer($pdf);

    header("Content-type: application/pdf");
    header("Content-disposition: inline; filename=espglob.pdf");
    header("Content-length: " . strlen($data));

    echo $data;

?>