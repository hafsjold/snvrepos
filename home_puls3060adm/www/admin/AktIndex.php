<? header ("Last-Modified: " . gmdate("D, d M Y H:i:s") . " GMT"); // always modified ?>
<html>
<head>
<title>AktIndex</title>

<?
    setlocale (LC_TIME, "da_DK.ISO8859-1");

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
	
	$Query="SELECT klinieid As ID, 
	               coalesce(klinieoverskrift, cast(klinieid AS text)) as navn, 
	               kliniedato As Dato, 
	               ('PListe4.php?TableId=8&AktId=' || klinieid) AS Link,
	               ('aktivitetstilmeldinger.php?AktId=' || klinieid) AS Download
			FROM   tblklinie
			WHERE etilmelding = 1  
			AND   kliniedato >= (NOW() - interval '90 days')
			ORDER BY kliniedato DESC, klinietext";
	$dbResult = pg_query($dbLink, $Query);
?>
</head>
<body bgcolor="#FFFFFF" text="#000000" onLoad="fonLoad();">
    <p align=left><font color=#003399 size=4>Aktivitetsoversigt</font></p>
    <table>
      <tr style="background-color: #FF0000"><font color="#003399">
        <th align=left>Navn</th>
        <th align=left>Dato</th>
        <th align=left>Download</th>
      </font></tr>
<?
	while($row = pg_fetch_array($dbResult, NULL , PGSQL_ASSOC))
	{
?>
      <tr valign=top style=background-color:<?=getcolor();?>>
        <td><a href=<?=$row["link"];?>><?=nl2br($row["navn"]);?></a></td>
        <td><?=DatoFormat($row["dato"]);?></td>
        <td><a href=<?=$row["download"];?> target="new" >Download</a></td>
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
	var w = 500;
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
