<? header ("Last-Modified: " . gmdate("D, d M Y H:i:s") . " GMT"); // always modified ?>
<html>
<!-- #BeginTemplate "/www/Udvikl/Templates/Puls3060_page.dwt" --> 
<head>
<!-- #BeginEditable "doctitle" --> 
<title>ResultaterIndex</title>
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

    if (isset($_REQUEST['AktId'])) 
      $AktId=$_REQUEST['AktId'];
	else
      $AktId=86;

    include_once("conn.inc");  
    $dbLink = pg_connect($conn_www);
	
	
	$Query="
	  SELECT 
		KLinieDato, 
		KLinieText
	  FROM tblKLinie 
	  WHERE KLinieID = $AktId";
	
	$dbResult = pg_query($dbLink, $Query);
	if ($row = pg_fetch_array($dbResult, NULL , PGSQL_ASSOC))
	{
      $KLinieDato = $row["kliniedato"];
      $KLinieText = $row["klinietext"];
	}	
	
	$Query="SELECT Fornavn, Efternavn 
			FROM tblAktTilmelding 
			WHERE AktId=$AktId
			ORDER BY Id";  
	$dbResult = pg_query($dbLink, $Query);
?>
    <p align=left><font color=#003399 size=4>Tilmeldinger til <?=$KLinieText?></font></p>

    <table>
      <tr style="background-color: #FF0000"><font color="#003399">
        <th align=left>Fornavn</th>
        <th align=left>Efternavn</th>
      </font></tr>
<?
	while($row = pg_fetch_array($dbResult, NULL , PGSQL_ASSOC))
	{
?>
      <tr valign=top style=background-color:<?=getcolor();?>>
        <td><?=nl2br($row["fornavn"]);?></td>
        <td><?=nl2br($row["efternavn"]);?></td>
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
