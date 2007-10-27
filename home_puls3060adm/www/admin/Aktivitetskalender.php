<? header ("Last-Modified: " . gmdate("D, d M Y H:i:s") . " GMT"); // always modified ?>
<? require ("Log.inc"); ?>
<html>
<script language="JavaScript">
<!--
function MM_findObj(n, d) { //v3.0
  var p,i,x;  if(!d) d=document; if((p=n.indexOf("?"))>0&&parent.frames.length) {
    d=parent.frames[n.substring(p+1)].document; n=n.substring(0,p);}
  if(!(x=d[n])&&d.all) x=d.all[n]; for (i=0;!x&&i<d.forms.length;i++) x=d.forms[i][n];
  for(i=0;!x&&d.layers&&i<d.layers.length;i++) x=MM_findObj(n,d.layers[i].document); return x;
}
function MM_displayStatusMsg(msgStr) { //v1.0
  status=msgStr;
  document.MM_returnValue = true;
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
<head>
<title>Aktivitetskalender</title>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
</head>
<body bgcolor="#FFFFFF" text="#000000" onLoad="fonLoad();MM_preloadImages('/images/vpil_f2.gif');">
<table width="100%" border="0" cellspacing="0" cellpadding="0" dwcopytype="CopyTableRow">
  <tr> 
    <td width="30">&nbsp;</td>
    <td>&nbsp;</td>
    <td width="30">&nbsp;</td>
  </tr>
  <tr> 
    <td width="30">&nbsp;</td>
    <td> 
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
      if (strftime("%H%M", strtotime($Sender)) != "0000")
        return(strtolower(strftime("%a %d. %b %Y kl %H:%M", strtotime($Sender))));
	  else
        return(strtolower(strftime("%a %d. %b %Y", strtotime($Sender))));
	}
    function gettarget($Sender)
	{
	  if ($Sender=="1"){ return("_blank");}
	  else{return("_self");}
	}

    if (isset($_REQUEST['PersonId'])) 
      $PersonId=$_REQUEST['PersonId'];
    else
      $PersonId="";

    include_once("conn.inc");  
    $dbLink = pg_connect($conn_www);
	
	$Query="SELECT 	tblKLinie.KLinieID AS KLinieID,
					tblKLinie.KLinieDato AS KLinieDato,
					tblKLinie.KLinieText AS KLinieText, 
					tblKLinie.KLinieSted AS KLinieSted, 
					tblKLinie.Info AS Info, 
					tblKLinie.NewWindow AS NewWindow, 
				    tblKLinie.PersonGruppeId As PersonGruppeId,
					(tblPerson.Fornavn || ' ' || tblPerson.Efternavn) AS Ansvarlig, 
        	 		('Aktivitetskalender.php?PersonId=' || tblPerson.Id) AS Link, 
        			tblKLinie.eTilmelding  AS eTilmelding,
        			tblKLinie.Link  AS URLLink
			FROM 	tblKLinie 
	  				LEFT JOIN tblPersonGruppe ON tblKLinie.PersonGruppeId = tblPersonGruppe.Id
	  				LEFT JOIN tblPersonRef ON tblPersonGruppe.Id = tblPersonRef.PersonGruppeId
	  				LEFT JOIN tblPerson ON tblPersonRef.PersonId = tblPerson.Id
	  				LEFT JOIN tblFunktion ON tblPersonRef.FunktionId = tblFunktion.Id
			WHERE 	tblKLinie.KLinieDato >= (now()- interval '360 days')";
	If ($PersonId <> ""){
	  $Query .= " AND tblPerson.Id = $PersonId ";
	}
	$Query .= "ORDER BY tblKLinie.KLinieDato, tblKLinie.KLinieID";
	$dbResult = pg_query($dbLink, $Query);
?>
    <p align=left><font color=#003399 size=4>Puls 3060 Aktivitetskalender</font></p>

    <table>
      <tr style=background-color:#FF0000><font color=#003399> 
        <th align=center>Dato</th>
        <th align=left>Aktivitet</th>
        <th align=left>Kontakt</th>
        <th align=center>Info</th>
        <th align=center>Grp</th>
        <th align=left> <a href="javascript:SelectHeading()" onMouseOut="MM_swapImgRestore();" onMouseOver="MM_swapImage('vpil','','/images/vpil_f2.gif',1)" ><img name="vpil" src="/images/vpil.gif" width="15" height="15" border="0"></a> 
      </font></tr>
<?
    $buf = 0;
	$break = 0;
	for (;;)
	{
	  if (!$dbResult) break;
	  $row = pg_fetch_array($dbResult, NULL , PGSQL_ASSOC);
	  $break = ($r["klinieid"] != $row["klinieid"]);
	  if ((($buf)&&($break))||($buf)&&(!$row))
	  {
?>
      <tr valign=top style=background-color:<?=getcolor();?>>
        <td align=center><?=DatoFormat($r["kliniedato"]);?></td>
        <? if (eregi ("<a([^>]+)>", $r["klinietext"])){ 
           $klinietext_html = eregi_replace("href=\"(/(nyhmnu|lobmnu|kalmnu|medmnu|tramnu|velmnu|lnkmnu)/.*)\"", 
                   			"href=\"http://www.puls3060.dk\\1\" target=\"_blank\"", 
                  			$r["klinietext"]);                
        ?>
          <td>
                <?=$klinietext_html;?>
				<? if ($r["kliniested"]!=""){ ?>
                  <br><br>Sted:<br><?=nl2br($r["kliniested"]);?>
                <? } ?>
          </td>
        <? }elseif ($r["urllink"]==""){ ?>
          <td>
                <?=nl2br($r["klinietext"]);?>
				<? if ($r["kliniested"]!=""){ ?>
                  <br><br>Sted:<br><?=nl2br($r["kliniested"]);?>
                <? } ?>
          </td>
        <? }else{ 
              $url = ereg_replace("javascript:top\\.leftmain\\('([^\\']+)','([^\\']+)'\\);", 
                                  "http://www.puls3060.dk?p1=\\1&p2=\\2&p3=no", 
                                  $r["urllink"]);
              
              $url2 = ereg_replace("^(/(nyhmnu|lobmnu|kalmnu|medmnu|tramnu|velmnu|lnkmnu)/.*)\$", 
                                   "http://www.puls3060.dk\\1&", 
                                   $url);
        ?>
          <td>
            <a href=<?=$url2;?> target=<?=gettarget(1);?>><?=nl2br($r["klinietext"]);?></a>
			<? if ($r["kliniested"]!=""){ ?>
              <br><br>Sted:<br><?=nl2br($r["kliniested"]);?>
            <? } ?>
          </td>
        <? } ?>
        <td><?=$r["linknavn"];?></td>
        <td align=center><?=$r["info"];?></td>
        <td align=right><?=$r["persongruppeid"];?></td>
        <td><?=$r["buttom"];?></td>
      </tr>
<?
	  }
	  if (!$row) break;
	  if (($buf)&&(!$break))
	  {
        $r["linknavn"] .= "<br><a href=". $row["link"] . ">" . $row["ansvarlig"] . "</a>";
	  }
	  if ((!$buf)||(($buf)&&($break)))
	  {
	    $r["klinieid"]       = $row["klinieid"];
        $r["kliniedato"]     = $row["kliniedato"];
        $r["klinietext"]     = $row["klinietext"];
        $r["kliniested"]     = $row["kliniested"];
        $r["info"]           = $row["info"];
        $r["newwindow"]      = $row["newwindow"];
		$r["etilmelding"]    = $row["etilmelding"];
        $r["urllink"]        = $row["urllink"];
        $r["persongruppeid"] = $row["persongruppeid"];
		$r["linknavn"]       = "<a href=". $row["link"] . ">" . $row["ansvarlig"] . "</a>";
        $r["buttom"]         = "<a href=\"javascript:MaintLink(" . $row["klinieid"] . ")\" onMouseOut=\"MM_swapImgRestore();\" onMouseOver=\"MM_swapImage('vpil" . $row["klinieid"] . "','','/images/vpil_f2.gif',1);\" ><img name=\"vpil" . $row["klinieid"] . "\" src=\"/images/vpil.gif\" width=\"15\" height=\"15\" border=\"0\"></a>";
        $buf = 1;
	  }
	}
?>
         
    </table>
      </td>
    <td width="30">&nbsp;</td>
  </tr>
  <tr> 
    <td width="30">&nbsp;</td>
    <td align="right"><br>
      <font color=#a03952 face="Arial, Helvetica, sans-serif" size=2><i><b></b></i></font> <br>
      <br>
    </td>
    <td width="30">&nbsp;</td>
  </tr>
</table>
</body>
</html>
<script language="javascript">
<!--
var newwindow;
function MaintLink(id){
	newwindow = window.open("/admin/AktivitetskalenderUpd.php?DoWhat=3&KLinieID="+id,"AktivitetskalenderUpd","status")
	newwindow.focus();
}

// Add Record
function SelectHeading(){
  newwindow = window.open("/admin/AktivitetskalenderUpd.php?DoWhat=2","AktivitetskalenderUpd","status");
  newwindow.focus();
}

function fonLoad(){
	var h = 600;
	var w = 700;
	var l = 1;
	if (l>23){
      w += 15;
	  l = 23;
	}
	window.resizeTo(w, h+(l*25));
}
// -->
</script>
