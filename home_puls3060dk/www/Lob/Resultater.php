<? header ("Last-Modified: " . gmdate("D, d M Y H:i:s") . " GMT"); // always modified ?>
<? require ("Log.inc"); ?>
<html>
<!-- #BeginTemplate "/www/Udvikl/Templates/Puls3060_page.dwt" --> 
<head>
<!-- #BeginEditable "doctitle" --> 
<title>Resultater</title>
<!-- #EndEditable --> 
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
</head>
<body bgcolor="#FFFFFF" text="#000000">
<table width="100%" border="0" cellspacing="0" cellpadding="0" dwcopytype="CopyTableRow">
  <tr> 
    <td width="30">&nbsp;</td>
    <td>&nbsp;</td>
    <td width="30">&nbsp;</td>
  </tr>
  <tr> 
    <td width="30">&nbsp;</td>
    <td> <!-- #BeginEditable "docbody" --> 
<?
    if (isset($_REQUEST['ID'])) 
      $ID=$_REQUEST['ID'];

    setlocale (LC_TIME, "da_DK.ISO8859-1");
    function getcolor()
	{
	  global $colorswitch;
	  if ($colorswitch==0){$colorswitch=1; return("#CFE3CC");}
	  else{$colorswitch=0; return("#CBE2E4");}
	}
    function DatoFormat($Sender)
	{
      return(strtolower(strftime("%A %d. %B %Y", strtotime($Sender))));
	}
    function TidFormat($Sender)
	{
      return(substr($Sender, 1));
	}
    
	function Idraetsmaerke($Sender)
	{
      if ($Sender=='t')
	    return ' *';
	  else
	    return ' ';	    
	}

    include_once("conn.inc");  
    $dbLink = pg_connect($conn_www);
	
	$Query="SELECT Navn, Dato 
			FROM tblLob 
			WHERE Id = $ID";
	$dbResult1 = pg_query($dbLink, $Query);
    $row1 = pg_fetch_array($dbResult1, NULL , PGSQL_ASSOC);
	
	$Query=
	"SELECT tblResultat.LobId AS LobId, 
        	tblResultat.Tid AS Tid, 
        	('/lobmnu/Lob/ResultaterDeltager.php?PersonId=' || tblResultat.PersonId) AS Link,
			tblDist.Dist AS Dist, 
			tblGrup.Grup AS Grup, 
			(tblPerson.Fornavn || ' ' || tblPerson.Efternavn) AS Navn,
			getidraetsmaerkext(lobid, personid, distance, tid) as idm			
	FROM tblResultat 
		INNER JOIN tblDist ON tblResultat.Distance = tblDist.Id
		INNER JOIN tblGrup ON tblResultat.Gruppe = tblGrup.Id
		INNER JOIN tblPerson ON tblPerson.Id = tblResultat.PersonId
    WHERE tblResultat.LobId = $ID
	ORDER BY tblResultat.LobId, 
			 tblDist.Id, 
		 	 tblGrup.Id, 
		 	 tblResultat.Tid";
	$dbResult2 = pg_query($dbLink, $Query);

?>
      <p align=left><font color=#003399 size=4>Resultat for <?=$row1["navn"];?><br>
      <?=DatoFormat($row1["dato"]);?></font></p>
	  <p align=left><font color="#003399" size="3">Idr&aelig;tsm&aelig;rketider markeret med *</font></p>
      <table>
        <tr style=background-color:#FF0000><font color=#003399> 
          <th align=left>Navn</th>
          <th align=center>Distance</th>
          <th align=left>Gruppe</th>
          <th align=center>Tid</th>
        </font></tr>
<?
	while($row2 = pg_fetch_array($dbResult2, NULL , PGSQL_ASSOC))
	{
?>
      <tr valign=top style=background-color:<?=getcolor();?>>
        <td align=left><a href=<?=$row2["link"];?> onClick="return top.rwlink(href);"><?=nl2br($row2["navn"] . Idraetsmaerke($row2["idm"]));?></a></td>
        <td align=center><?=$row2["dist"];?></td>
        <td align=left><?=$row2["grup"];?></td>
        <td align=left><?=TidFormat($row2["tid"]);?></td>
      </tr>
<?
	}
?>
      </table>
      <!-- #EndEditable --> </td>
    <td width="30">&nbsp;</td>
  </tr>
  <tr> 
    <td width="30">&nbsp;</td>
    <td align="right"><br>
      <font color=#a03952 face="Arial, Helvetica, sans-serif" 
size=2><i><b></b></i></font> <br>
      <br>
    </td>
    <td width="30">&nbsp;</td>
  </tr>
</table>
</body>
<!-- #EndTemplate -->
</html>
