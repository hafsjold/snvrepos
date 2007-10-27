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
      return(strtolower(strftime("%A %d. %B", strtotime($Sender))));
	}
    function gettarget($Sender)
	{
	  if ($Sender=="1"){ return("_blank");}
	  else{return("_self");}
	}

    include_once("conn.inc");  
    $dbLink = pg_connect($conn_www);
	
	$Query="SELECT TPgmId, 
				   TPgmNavn, 
				   StartDato, 
				   SlutDato, 
				   ('/tramnu/Traening/TProgram.php?ID=' || TPgmId) AS link1,
				   link,
				   newwindow
			FROM vtpgm 
			WHERE SlutDato >= now()   
			AND StartDato <=  now()  
			ORDER BY StartDato DESC, SlutDato, TPgmNavn";
	$dbResult = pg_query($dbLink, $Query);
?>
    <p align=left><font color=#003399 size=4>Oversigt for Puls 3060 Træning</font></p>

    <table>
      <tr style=background-color:#FF0000><font color=#003399> 
        <th align=left>Program</th>
      </font></tr>
<?
	while($row = pg_fetch_array($dbResult, NULL , PGSQL_ASSOC))
	{
	  if ($row["link"]){$link = $row["link"]; }
	  else{$link = $row["link1"]; } 
?>
      <tr valign=top style=background-color:<?=getcolor();?>>
        <td><a href=<?=$link;?>	onClick="return top.rwlink(href);" target=<?=gettarget($row["newwindow"]);?>><?=nl2br($row["tpgmnavn"]);?></a></td>
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
