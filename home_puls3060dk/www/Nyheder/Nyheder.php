<? header ("Last-Modified: " . gmdate("D, d M Y H:i:s") . " GMT"); // always modified ?>
<? require ("Log.inc"); ?>
<html>
<head>
<!-- #BeginTemplate "/www/Udvikl/Templates/Puls3060_page.dwt" --> 
<head>
<!-- #BeginEditable "doctitle" --> 
<title>Nyheder</title>
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
	  if ($colorswitch==0){$colorswitch=1; return("#CFE3CC");}
	  else{$colorswitch=0; return("#CBE2E4");}
	}
    function DatoFormat($Sender)
	{
      return(strtolower(strftime("%A %d. %B", strtotime($Sender))));
	}
    function gettarget($Sender)
	{
	  if ($Sender=="1"){ return("_blank");}
	  else{return("_self");}
	}

    include_once("conn.inc");  
    $dbLink = pg_connect($conn_www);
	
	$Query = "SELECT Udgivet, 
					 Titel, 
					 Link, 
					 NewWindow 
			  FROM tblNyheder
			  WHERE  (Hidden IS NULL OR Hidden<>1) 
			  AND 	 Udgivet >= (now()- interval '90 days')

			  ORDER BY Udgivet DESC";
	$dbResult = pg_query($dbLink, $Query);
?>
      <p align=left><font color=#003399 size=4>Puls 3060 Nyheder</font></p>
      <table>
        <tr style=background-color:#FF0000><font color=#003399> 
          <th align=center>Dato</th>
          <th align=left>Nyhed</th>
        </font></tr>
<?
	while($row = pg_fetch_array($dbResult, NULL , PGSQL_ASSOC))
	{
?>
      <tr valign=top style=background-color:<?=getcolor();?>>
        <td align=center><?=DatoFormat($row["udgivet"]);?></td>
        <td><a href=<?=$row["link"];?> onClick="return top.rwlink(href);" target=<?=gettarget($row["newwindow"]);?>><?=nl2br($row["titel"]);?></a></td>
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
