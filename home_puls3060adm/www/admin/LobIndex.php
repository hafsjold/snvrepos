<? header ("Last-Modified: " . gmdate("D, d M Y H:i:s") . " GMT"); // always modified ?>
<html>
<head>
<title>LobIndex</title>

<?
    setlocale (LC_TIME, "da_DK.ISO8859-1");

    if (isset($_REQUEST['DoWhat'])) 
      $DoWhat=$_REQUEST['DoWhat'];
    else 
      $DoWhat = 0;


    function getcolor()
	{
	  global $colorswitch;
	  if ($colorswitch==0){
		$colorswitch=1;
	    return("#CFE3CC");
	  }
	  else{
		$colorswitch=0;
	    return("#CBE2E4");
	  }
	}
    function DatoFormat($Sender)
	{
      return(strtolower(strftime("%A %d. %B %Y", strtotime($Sender))));
	}

    include_once("conn.inc");  
    $dbLink = pg_connect($conn_www);
	
	switch ($DoWhat){
	  case 1:
	    $Overskrift = "Vælg l&oslash;b: Synkroniser Tilmeldinger";
	    $Query="SELECT ID, Navn, Dato, ('PListe4.php?TableId=4&LobId=' || ID) AS Link
			FROM   tblLob
			WHERE   Dato >= (NOW() - interval '5 days')
			ORDER BY Dato ASC, Navn
			LIMIT 3";
	    break;

	  case 2:
	    $Overskrift = "Vælg l&oslash;b: Indtast Løbs deltagere";
	    $Query="SELECT ID, Navn, Dato, ('PListe3.php?SLobId=' || ID) AS Link
			FROM   tblLob
			WHERE   Dato >= (NOW() - interval '5 days')
			ORDER BY Dato ASC, Navn
			LIMIT 3";
	    break;

	  case 3:
	    $Overskrift = "Vælg l&oslash;b: Skriv Tilmelding sorteret på Navn";
	    $Query="SELECT ID, Navn, Dato, ('TilmeldingEspergardelobet.php?SortId=a&LobId=' || ID || ' target = _blank') AS Link
			FROM   tblLob
			WHERE   Dato >= (NOW() - interval '5 days')
			ORDER BY Dato ASC, Navn
			LIMIT 3";
	    break;

	  case 4:
	    $Overskrift = "Vælg l&oslash;b: Skriv Tilmelding sorteret på Nr";
	    $Query="SELECT ID, Navn, Dato, ('TilmeldingEspergardelobet.php?SortId=b&LobId=' || ID || ' target = _blank') AS Link
			FROM   tblLob
			WHERE   Dato >= (NOW() - interval '5 days')
			ORDER BY Dato ASC, Navn
			LIMIT 3";
	    break;

	  case 5:
	    $Overskrift = "Vælg l&oslash;b: Indtast Resultat Tider";
	    $Query="SELECT ID, Navn, Dato, ('Resultat.php?lobid=' || ID) AS Link
			FROM   tblLob
			WHERE   Dato >= (NOW() - interval '5 days')
			ORDER BY Dato ASC, Navn
			LIMIT 3";
	    break;

	  case 6:
	    $Overskrift = "Vælg l&oslash;b: Skriv Tilmelding Liste";
	    $Query="SELECT ID, Navn, Dato, ('TilmeldingEspergardelobet2.php?LobId=' || ID || ' target = _blank') AS Link
			FROM   tblLob
			WHERE   Dato >= (NOW() - interval '5 days')
			ORDER BY Dato ASC, Navn
			LIMIT 3";
	    break;

	  case 7:
	    $Overskrift = "Vælg l&oslash;b: Skriv Resultat Liste";
	    $Query="SELECT ID, Navn, Dato, ('ResultatEspergardelobet.php?LobId=' || ID || ' target = _blank') AS Link
			FROM   tblLob
			WHERE   Dato >= (NOW() - interval '5 days')
			ORDER BY Dato ASC, Navn
			LIMIT 3";
	    break;

	  case 8:
	    $Overskrift = "Vælg l&oslash;b: Resultat Helsingør Dagblad";
	    $Query="SELECT ID, Navn, Dato, ('ResultatEspergardelobet2.php?LobId=' || ID || ' target = _blank') AS Link
			FROM   tblLob
			WHERE   Dato >= (NOW() - interval '5 days')
			ORDER BY Dato ASC, Navn
			LIMIT 3";
	    break;

	  case 9:
	    $Overskrift = "Vælg overførsel af Tilmeldinger -> Deltagere";
	    $Query="SELECT ID, Navn, Dato, ('Tilmeldinger2Deltagere.php?LobId=' || ID || ' target = _blank') AS Link
			FROM   tblLob
			WHERE   Dato >= (NOW() - interval '5 days')
			ORDER BY Dato ASC, Navn
			LIMIT 3";
	    break;

	  case 10:
	    $Overskrift = "Publicer resultat på WWW";
	    $Query="SELECT ID, Navn, Dato, ('PublicerResultat2www.php?LobId=' || ID || ' target = _blank') AS Link
			FROM   tblLob
			WHERE   Dato >= (NOW() - interval '5 days')
			ORDER BY Dato ASC, Navn
			LIMIT 3";
	    break;


	  default:
	    $Overskrift = "Vælg l&oslash;b";
	    $Query="SELECT ID, Navn, Dato, (' ') AS Link
			FROM   tblLob
			WHERE   Dato >= (NOW() - interval '5 days')
			ORDER BY Dato ASC, Navn
			LIMIT 3";
	    break;
	}

	$dbResult = pg_query($dbLink, $Query);
?>
</head>
<body bgcolor="#FFFFFF" text="#000000" onLoad="fonLoad();">
    <p align=left><font color=#003399 size=4><?=$Overskrift?></font></p>
    <table>
      <tr style="background-color: #FF0000"><font color="#003399">
        <th align=left>Navn</th>
        <th align=left>Dato</th>
      </font></tr>
<?
	while($row = pg_fetch_array($dbResult, NULL , PGSQL_ASSOC))
	{
?>
      <tr valign=top style=background-color:<?=getcolor();?>>
        <td><a href=<?=$row["link"];?>><?=nl2br($row["navn"]);?></a></td>
        <td><?=DatoFormat($row["dato"]);?></td>
	  </tr>
<?
	}
?>
    </table>
  </body>
<script language="javascript">
<!--
function fonLoad(){
	var h = 270;
	var w = 400;
	var l = 1;
	if (l>23){
      w += 15;
	  l = 23;
	}
	window.resizeTo(w, h+(l*25));
}
// -->
</script>

</html>
