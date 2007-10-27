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

<title>Oversigt for Puls 3060 Træning</title>

<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
</head>
<body bgcolor="#FFFFFF" text="#000000" onLoad="fonLoad();MM_preloadImages('/images/vpil.gif');MM_preloadImages('/images/vpil_f2.gif');">

<?
    setlocale (LC_TIME, "da_DK.ISO8859-1");

    if (isset($_REQUEST['ID'])) 
      $ID=$_REQUEST['ID'];

    if (isset($_REQUEST['TPTranerID'])) 
      $TPTranerID=$_REQUEST['TPTranerID'];

    
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
      if (strftime("%H%M", strtotime($Sender)) != "0000")
        return(strtolower(strftime("%a %d. %b kl %H:%M", strtotime($Sender))));
	  else
        return(strtolower(strftime("%a %d. %b", strtotime($Sender))));
	}
    
    include_once("conn.inc");  
    $dbLink = pg_connect($conn_www);
	
	$Query="SELECT TPgmNavn 
			FROM tblTPgm 
			WHERE TPgmId = $ID";
	$dbResult1 = pg_query($dbLink, $Query);
    $row1 = pg_fetch_array($dbResult1, NULL , PGSQL_ASSOC);
	
	$Query="
	  SELECT 	tblTLinie.TLinieID AS TLinieID, 
	    	 	tblTLinie.TPgmID AS TPgmID, 
	    	 	tblTLinie.TLinieDato AS TLinieDato, 
	    	 	tblTLinie.TLinieText AS TLinieText, 
	    	 	tblTLinie.TLinieDosis AS TLinieDosis, 
				tblTLinie.PersonGruppeId As PersonGruppeId,
        	 	('TProgram.php?ID=' || tblTLinie.TPgmID || '&TPTranerID=' || tblPerson.Id) AS Link, 
        	 	tblPerson.Id AS TPTranerID, 
        	 	(tblPerson.Fornavn || ' ' || tblPerson.Efternavn) AS TPTranerKortNavn
      FROM		tblTLinie 
	  			LEFT JOIN tblPersonGruppe ON tblTLinie.PersonGruppeId = tblPersonGruppe.Id
	  			LEFT JOIN tblPersonRef ON tblPersonGruppe.Id = tblPersonRef.PersonGruppeId
	  			LEFT JOIN tblPerson ON tblPersonRef.PersonId = tblPerson.Id
	  			LEFT JOIN tblFunktion ON tblPersonRef.FunktionId = tblFunktion.Id
	  WHERE  	tblTLinie.TPgmID = $ID
	  AND 	 	tblTLinie.TLinieDato >= (now()- interval '60 days')
	";
	If ($TPTranerID <> ""){
	  $Query .= " AND tblPerson.Id = $TPTranerID ";
	}
	
	$Query .= " ORDER BY tblTLinie.TLinieDato, tblTLinie.TLinieID";
	
	$dbResult2 = pg_query($dbLink, $Query);

?>
    <p align=left><font color=#003399 size=4><?=$row1["tpgmnavn"];?></font></p>

    <table>
      <tr style=background-color:#FF0000><font color=#003399> 
        <th align=center>Dato</th>
        <th align=left>Aktivitet</th>
        <th align=left>Intensitet</th>
        <th align=left>Træner</th>
        <th align=center>Grp</th>
        <th align=left> <a href="javascript:SelectHeading()" onMouseOut="MM_swapImgRestore();" onMouseOver="MM_swapImage('vpil','','/images/vpil_f2.gif',1)" ><img name="vpil" src="/images/vpil.gif" width="15" height="15" border="0"></a> 
	  </font></tr>
<?
    $buf = 0;
	$break = 0;
	for (;;)
	{
	  if (!$dbResult2) break;
	  $row2 = pg_fetch_array($dbResult2, NULL , PGSQL_ASSOC);
	  $break = ($r["tlinieid"] != $row2["tlinieid"]);
	  if ((($buf)&&($break))||($buf)&&(!$row2))
	  {
?>
      <tr valign=top style=background-color:<?=getcolor();?>>
        <td align=center><?=DatoFormat($r["tliniedato"]);?></td>
        <td><?=nl2br($r["tlinietext"]);?></td>
        <td><?=nl2br($r["tliniedosis"]);?></td>
        <td><?=$r["linknavn"];?></td>
        <td align=right><?=$r["persongruppeid"];?></td>
        <td><?=$r["buttom"];?></td>
      </tr>
<?
	  }
	  if (!$row2) break;
	  if (($buf)&&(!$break))
	  {
        $r["linknavn"] .= "<br><a href=". $row2["link"] . ">" . $row2["tptranerkortnavn"] . "</a>";
	  }
	  if ((!$buf)||(($buf)&&($break)))
	  {
	    $r["tlinieid"]       = $row2["tlinieid"];
        $r["tliniedato"]     = $row2["tliniedato"];
        $r["tlinietext"]     = $row2["tlinietext"];
        $r["tliniedosis"]    = $row2["tliniedosis"];
        $r["persongruppeid"] = $row2["persongruppeid"];
		$r["linknavn"]    = "<a href=". $row2["link"] . ">" . $row2["tptranerkortnavn"] . "</a>";
        $r["buttom"]      = "<a href=\"javascript:MaintLink(" . $row2["tlinieid"] . ")\" onMouseOut=\"MM_swapImgRestore();\" onMouseOver=\"MM_swapImage('vpil" . $row2["tlinieid"] . "','','/images/vpil_f2.gif',1);\" ><img name=\"vpil" . $row2["tlinieid"] . "\" src=\"/images/vpil.gif\" width=\"15\" height=\"15\" border=\"0\"></a>";
        $buf = 1;
	  }
	}
?>
    </table>
</body>
</html>
<script language="javascript">
<!--
var newwindow;
function MaintLink(id){
	newwindow = window.open("/admin/TProgramUpd.php?DoWhat=3&TPgmId=<?=$ID?>&TLinieID="+id,"TProgramUpd","status");
	newwindow.focus();
}

// Add Record
function SelectHeading(){
  newwindow = window.open("/admin/TProgramUpd.php?DoWhat=2&TPgmId=<?=$ID?>","TProgramUpd","status");
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
