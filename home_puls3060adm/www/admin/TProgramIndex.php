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
<head>
<title>ProgramIndex</title>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
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
				   ('TProgram.php?ID=' || TPgmId || ' target=_blank') AS link1,
				   link,
				   newwindow
			FROM tblTPgm 
			WHERE SlutDato >= (now() - interval '90 days')   
			AND StartDato <=  (now() + interval '90 days')  
			ORDER BY StartDato DESC, SlutDato, TPgmNavn";
	$dbResult = pg_query($dbLink, $Query);
?>
</head>
<body bgcolor="#FFFFFF" text="#000000" onLoad="fonLoad();; MM_preloadImages('/images/vpil.gif');MM_preloadImages('/images/vpil_f2.gif');">
    <p align=left><font color=#003399 size=4>Oversigt for Puls 3060 Træning</font></p>

    <table>
      <tr style=background-color:#FF0000><font color=#003399> 
        <th align=left>Program</th>
        <th align=center>Start</th>
        <th align=center>Slut</th>
        <th align=left> <a href="javascript:SelectHeading()" onMouseOut="MM_swapImgRestore();" onMouseOver="MM_swapImage('vpil','','/images/vpil_f2.gif',1)" ><img name="vpil" src="/images/vpil.gif" width="15" height="15" border="0"></a> 
      </font></tr>
<?
	while($row = pg_fetch_array($dbResult, NULL , PGSQL_ASSOC))
	{
	  if ($row["link"]){$link = $row["link"]; }
	  else{$link = $row["link1"]; } 
      $buttom  = "<a href=\"javascript:MaintLink(" . $row["tpgmid"] . ")\" onMouseOut=\"MM_swapImgRestore();\" onMouseOver=\"MM_swapImage('vpil" . $row["tpgmid"] . "','','/images/vpil_f2.gif',1);\" ><img name=\"vpil" . $row["tpgmid"] . "\" src=\"/images/vpil.gif\" width=\"15\" height=\"15\" border=\"0\"></a>";
?>
      <tr valign=top style=background-color:<?=getcolor();?>>
        <td><a href=<?=$link;?> target=<?=gettarget($row["newwindow"]);?>><?=nl2br($row["tpgmnavn"]);?></a></td>
        <td align=center><?=DatoFormat($row["startdato"]);?></td>
        <td align=center><?=DatoFormat($row["slutdato"]);?></td>
        <td><?=$buttom;?></td>
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
	var w = 700;
	var l = 3;
	if (l>23){
      w += 5;
	  l = 23;
	}
	window.resizeTo(w, h+(l*25));
}
var newwindow;
// Update Record
function MaintLink(id){
	newwindow = window.open("/admin/TProgramHovedUpd.php?DoWhat=3&TPgmID="+id,"TProgramHoved","status")
	newwindow.focus();
}

// Add Record
function SelectHeading(){
  newwindow = window.open("/admin/TProgramHovedUpd.php?DoWhat=2&TPgmId=<?=$ID?>","TProgramHoved","status");
  newwindow.focus();
}
// -->
</script>
</html>
