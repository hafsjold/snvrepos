<?php
    setlocale (LC_TIME, "da_DK.ISO8859-1");
    function DatoFormat($Sender)
	{
      return(strtolower(strftime("%A den %d. %B", strtotime(substr($Sender,0,18)))));
	}
    function TimeFormat($Sender)
	{
      return(strtolower(strftime("%H:%M:%S", strtotime(substr($Sender,0,8)))));
	}

    if (isset($_REQUEST['LobId'])) 
      $LobId=$_REQUEST['LobId'];
    else
      $LobId = 51;

    if (isset($_REQUEST['SortId'])) 
      $SortId=$_REQUEST['SortId'];
    else
      $SortId = 'a';

    include_once("conn.inc");  
    $dbLink = pg_connect($conn_www);

    $Query="SELECT *, now() as nu FROM tbllob WHERE Id = $LobId;";
    $dbResult = pg_query($dbLink, $Query);
    $row = pg_fetch_array($dbResult, NULL , PGSQL_ASSOC);
    $dato = $row["dato"];
    $lobnavn = $row["navn"];
    
    $Query = "SELECT count(*) as ant FROM vresultat WHERE lobid=$LobId;";
    $dbResult = pg_query($dbLink, $Query);
    $row = pg_fetch_array($dbResult, NULL , PGSQL_ASSOC);
    $ant = $row["ant"];

    $Query= "SELECT * FROM tbllob WHERE dato > '" . $dato . "' ORDER BY dato LIMIT 1;";
    $dbResult = pg_query($dbLink, $Query);
    $row = pg_fetch_array($dbResult, NULL , PGSQL_ASSOC);
    $dato2 = $row["dato"];

    $data =	"";
    $data .= "HUSK HUSK HUSK at sende mail som 'Almindelig Tekst'\n\n";
    $data .= "Til:\tRedaktionen Helsingør Dagblad(redaktionen@helsingordagblad.dk)\n";
    $data .= "Bcc:\t3060Bestyr\n";
    $data .= "Emne:\tResultatliste for " . $lobnavn . " " . DatoFormat($dato) . "\n\n\n";

    $data .= "Til Redaktionen Helsingør Dagblad\n\n";
    $data .= $lobnavn . " " . DatoFormat($dato) . "\n\n";
    $data .= "Der var " . $ant . " deltager til start på 1, 5 og 10 km.\n\n";
    $data .= "Næste Espergærdeløb er " . DatoFormat($dato2) . ", og som sædvanlig med start kl. 10.00.\n\n";
    $data .= "På PULS 3060 hjemmeside: www.puls3060.dk kan du altid hente oplysninger om kommende aktiviteter.\n\n\n";
    $data .= "RESULTATLISTE:\n\n";

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
    
	$afdnavn = ""; $grup = "";
    while($row = pg_fetch_array($dbResult, NULL , PGSQL_ASSOC)) {

      if ($afdnavn <> $row["afdnavn"]){
	    $data .= "\n" . $row["afdnavn"] . "\n";
		$afdnavn = $row["afdnavn"];

	    $data .= "\n" . $row["grup"] . "\n";
	    $grup=$row["grup"];
      }

	  if ($grup <> $row["grup"]) {
	    $data .= "\n" . $row["grup"] . "\n";
	    $grup=$row["grup"];
      }
      
	  $data .= $row["navn"] . "\t" . TimeFormat($row["tid"]) . "\n";
	}

    header("Content-type: application/pdf");
    header("Content-disposition: inline; filename=klubtoj.pdf");
    header("Content-length: " . strlen($data));

    echo $data;

?>