<? header ("Last-Modified: " . gmdate("D, d M Y H:i:s") . " GMT"); // always modified ?>
<html>
<!-- #BeginTemplate "/www/Udvikl/Templates/Puls3060_page.dwt" --> 
<head>
<!-- #BeginEditable "doctitle" --> 
<title>ResultaterDeltager</title>
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
    if (isset($_REQUEST['PersonId'])) 
      $PersonId=$_REQUEST['PersonId'];

    setlocale (LC_TIME, "da_DK.ISO8859-1");

    function getcolor()
	{
	  global $colorswitch;
	  if ($colorswitch==0){$colorswitch=1; return("#CFE3CC");}
	  else{$colorswitch=0; return("#CBE2E4");}
	}
    function DatoFormat($Sender)
	{
      return(strtolower(strftime("%d. %b %Y", strtotime($Sender))));
	}
    function TidFormat($Sender)
	{
      return(substr($Sender, 1));
	}

    include_once("conn.inc");  
    $dbLink = pg_connect($conn_www);
	
	
	$Query=
	"SELECT (tblPerson.Fornavn || ' ' || tblPerson.Efternavn) AS Navn 
	FROM tblPerson
	WHERE tblPerson.Id = $PersonId";
	$dbResult1 = pg_query($dbLink, $Query);
    $row1 = pg_fetch_array($dbResult1, NULL , PGSQL_ASSOC);	
	
	$Query=
	"SELECT tblResultat.PersonId AS PersonId, 
			tblLob.Dato AS Dato, 
			tblLob.Navn AS Navn, 
			tblDist.Dist AS Dist, 
        	tblResultat.Tid AS Tid
	FROM tblResultat 
		INNER JOIN tblLob ON tblLob.Id = tblResultat.LobId
		INNER JOIN tblDist ON tblDist.Id = tblResultat.Distance
	WHERE tblResultat.PersonId = $PersonId
	ORDER BY tblResultat.PersonId, 
		 	 tblLob.Dato DESC";
	$dbResult2 = pg_query($dbLink, $Query);
?>
      <p align=left><font color=#003399 size=4>Resultat for <?=nl2br($row1["navn"]);?></font></p>

      <table>
        <tr style=background-color:#FF0000><font color=#003399> 
          <th align=center>Dato</th>
          <th align=left>Løb</th>
          <th align=center>Distance</th>
          <th align=center>Tid</th>
        </font></tr>
<?
	while($row2 = pg_fetch_array($dbResult2, NULL , PGSQL_ASSOC))
	{
?>
      <tr valign=top style=background-color:<?=getcolor();?>>
        <td align=right><?=DatoFormat($row2["dato"]);?></td>
        <td><?=nl2br($row2["navn"]);?></td>
        <td align=center><?=nl2br($row2["dist"]);?></td>
        <td align=center><?=TidFormat($row2["tid"]);?></td>
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
