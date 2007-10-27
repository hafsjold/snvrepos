<? header ("Last-Modified: " . gmdate("D, d M Y H:i:s") . " GMT"); // always modified ?>
<html>
<script language="JavaScript">
<!--
function MM_findObj(n, d) { //v3.0
  var p,i,x;  if(!d) d=document; if((p=n.indexOf("?"))>0&&parent.frames.length) {
    d=parent.frames[n.substring(p+1)].document; n=n.substring(0,p);}
  if(!(x=d[n])&&d.all) x=d.all[n]; for (i=0;!x&&i<d.forms.length;i++) x=d.forms[i][n];
  for(i=0;!x&&d.layers&&i<d.layers.length;i++) x=MM_findObj(n,d.layers[i].document); return x;
}
function MM_swapImage() { //v3.0
  var i,j=0,x,a=MM_swapImage.arguments; document.MM_sr=new Array; for(i=0;i<(a.length-2);i+=3)
   if ((x=MM_findObj(a[i]))!=null){document.MM_sr[j++]=x; if(!x.oSrc) x.oSrc=x.src; x.src=a[i+2];}
}
function MM_swapImgRestore() { //v3.0
  var i,x,a=document.MM_sr; for(i=0;a&&i<a.length&&(x=a[i])&&x.oSrc;i++) x.src=x.oSrc;
}

function MM_preloadImages() { //v3.0
 var d=document; if(d.images){ if(!d.MM_p) d.MM_p=new Array();
   var i,j=d.MM_p.length,a=MM_preloadImages.arguments; for(i=0; i<a.length; i++)
   if (a[i].indexOf("#")!=0){ d.MM_p[j]=new Image; d.MM_p[j++].src=a[i];}}
}

//-->
</script>
<!-- #BeginTemplate "/www/Udvikl/Templates/Puls3060_page.dwt" --> 
<head>
<!-- #BeginEditable "doctitle" --> 
<title>Opgaver</title>
<!-- #EndEditable --> 
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
</head>
<body bgcolor="#FFFFFF" text="#000000" onLoad="MM_preloadImages('images/vpil_f2.gif');">
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
	$auth = isset($_SERVER['SSL_CLIENT_S_DN_CN']);

    function getcolor()
	{
	  global $colorswitch;
	  if ($colorswitch==0){$colorswitch=1; return("#CFE3CC");}
	  else{$colorswitch=0; return("#CBE2E4");}
	}
    function DatoFormat($Sender)
	{
      return(strtolower(strftime("%d. %b", strtotime($Sender))));
	}
    function TidFormat($Sender)
	{
      return(substr($Sender, 11, 5));
	}
    function gettarget($Sender)
	{
	  if ($Sender=="1"){ return("_blank");}
	  else{return("_self");}
	}

    include_once("conn.inc");  
    $dbLink = pg_connect($conn_www);
	
	$Query="SELECT OpgaveNavn 
			FROM tblOpgave 
			WHERE Id = $ID";
	$dbResult1 = pg_query($dbLink, $Query);
    $row1 = pg_fetch_array($dbResult1, NULL , PGSQL_ASSOC);
	
	$Query="
	  SELECT	tblOpgave.Id As OId,
	  			tblOpgave.OpgaveNavn As Opgave,
	  			tblOpgave.OpgaveInstruks As Instruks,
	  			tblOpgave.AntalPerson As Antal,
	  			tblOpgave.StartTid As StartTid,
	  			tblOpgave.SlutTid As SlutTid,
	  			(tblPerson.Fornavn || ' ' || tblPerson.Efternavn) As Navn,
        	 	('Opgaver.php?ID=' || tblHovedOpgave.Id || '&PersonId=' || tblPerson.Id) AS Link, 
	  			tblFunktion.Funktion,
				tblPerson.Id As PId
	  FROM 		tblOpgave 
				LEFT JOIN tblOpgaveRef ON tblOpgave.Id = tblOpgaveRef.BestaarafOpgaveId 
				LEFT JOIN tblOpgave AS tblHovedOpgave ON tblOpgaveRef.IndgaariOpgaveId = tblHovedOpgave.Id
	  			LEFT JOIN tblPersonGruppe ON tblOpgave.PersonGruppeId = tblPersonGruppe.Id
	  			LEFT JOIN tblPersonRef ON tblPersonGruppe.Id = tblPersonRef.PersonGruppeId
	  			LEFT JOIN tblPerson ON tblPersonRef.PersonId = tblPerson.Id
	  			LEFT JOIN tblFunktion ON tblPersonRef.FunktionId = tblFunktion.Id
	  WHERE tblHovedOpgave.Id = $ID
	";
	If ($PersonId <> ""){
	  $Query .= " AND tblPerson.Id = $PersonId ";
	}
	$Query .= "ORDER BY StartTid";
	$dbResult2 = pg_query($dbLink, $Query);
?>
    <p align=left><font color=#003399 size=4><?=$row1["OpgaveNavn"];?></font></p>

    <table>
      <tr style=background-color:#FF0000><font color=#003399> 
        <th align=center>Opgave</th>
        <th align=center>Dato</th>
        <th align=center>Start</th>
        <th align=center>Slut</th>
        <th align=center>Ant<br>per</th>
        <th align=left>Udføres af</th>
        <? if($auth) { ?>
          <th align=left>
             <a href="javascript:OpgaverAdd(0)" onMouseOut="MM_swapImgRestore();" onMouseOver="MM_swapImage('vpil','','images/vpil_f2.gif',1);" ><img name="vpil" src="images/vpil.gif" width="15" height="15" border="0"></a>
          </th>
        <? } else { ?>
          <th align=left>&nbsp;</th>
        <? } ?>
      </font></tr>
<?
    $buf = 0;
	$break = 0;
	for (;;)
	{
	  if (!$dbResult2) break;
	  $row2 = pg_fetch_array($dbResult2, NULL , PGSQL_ASSOC);
	  $break = ($r["OId"] != $row2["OId"]);
	  if ((($buf)&&($break))||($buf)&&(!$row2))
	  {
        if (($r["PIdCount"] >= $r["Antal"])	&& (!$auth))
	      $r["Buttom"] = "";
?>
        <tr valign=top style=background-color:<?=getcolor();?>>
          <td align=left><?=nl2br($r["Opgave"]);?></td>
          <td align=center><?=DatoFormat($r["StartTid"]);?></td>
          <td align=center><?=TidFormat($r["StartTid"]);?></td>
          <td align=center><?=TidFormat($r["SlutTid"]);?></td>
          <td align=center><?=nl2br($r["Antal"]);?></td>
          <td align=left><?=$r["LinkNavn"];?></td>
          <td align=left valign="middle"><?=$r["Buttom"];?></td>
        </tr>
<?
	  }
	  if (!$row2) break;
	  if (($buf)&&(!$break))
	  {
        $r["LinkNavn"] .= "<br><a href=". $row2["Link"] . ">" . $row2["Navn"] . "</a>";
	    if ($row2["PId"])
	      $r["PIdCount"] += 1;
	  }
	  if ((!$buf)||(($buf)&&($break)))
	  {
	    $r["OId"]	   = $row2["OId"];
        $r["Opgave"]   = $row2["Opgave"];
        $r["Instruks"] = $row2["Instruks"];
        $r["Antal"]    = $row2["Antal"];
        $r["StartTid"] = $row2["StartTid"];
        $r["SlutTid"]  = $row2["SlutTid"];
		$r["LinkNavn"] = "<a href=". $row2["Link"] . ">" . $row2["Navn"] . "</a>";
        if($auth)
          $r["Buttom"]   = "<a href=\"javascript:OpgaverUpd(" . $row2["OId"] . ")\" onMouseOut=\"MM_swapImgRestore();\" onMouseOver=\"MM_swapImage('vpil" . $row2["OId"] . "','','images/vpil_f2.gif',1);\" ><img name=\"vpil" . $row2["OId"] . "\" src=\"images/vpil.gif\" width=\"15\" height=\"15\" border=\"0\"></a>";
        else
          $r["Buttom"]   = "<a href=\"javascript:Tilsagn(" . $row2["OId"] . ")\" onMouseOut=\"MM_swapImgRestore();\" onMouseOver=\"MM_swapImage('vpil" . $row2["OId"] . "','','images/vpil_f2.gif',1);\" ><img name=\"vpil" . $row2["OId"] . "\" src=\"images/vpil.gif\" width=\"15\" height=\"15\" border=\"0\"></a>";
	    if ($row2["PId"])
	      $r["PIdCount"] = 1;
        else
	      $r["PIdCount"] = 0;

        $buf = 1;
	  }
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
<script language="javascript">
<!--
var newwindow1;
function Tilsagn(id){
	newwindow1 = window.open("/Opgaver/Tilsagn.php?Id="+id,"Tilsagn","status,height=400,width=410")
	newwindow1.focus();
}
var newwindow2;
function OpgaverUpd(id){
	newwindow2 = window.open("/Opgaver/OpgaverUpd.php?DoWhat=3&Id="+id,"OpgaverUpd","status")
	newwindow2.focus();
}
var newwindow3;
function OpgaverAdd(id){
	newwindow3 = window.open("/Opgaver/OpgaverUpd.php?DoWhat=2&Id="+id,"OpgaverUpd","status")
	newwindow3.focus();
}
// -->
</script>
<!-- #EndTemplate -->
</html>
