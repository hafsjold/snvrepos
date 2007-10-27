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
<title>L&oslash;bsdeltagere</title>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
</head>
<body bgcolor="#FFFFFF" text="#000000" onLoad="fonLoad(); MM_preloadImages('/images/vpil_f2.gif')">
<?
    setlocale (LC_TIME, "da_DK.ISO8859-1");

    if (isset($_REQUEST['TopWin'])) 
      $TopWin=$_REQUEST['TopWin'];
    else
      $TopWin = $_SERVER['SCRIPT_NAME'];

    if (isset($_REQUEST['SLobId'])) 
      $SLobId=$_REQUEST['SLobId'];
    else
      $SLobId = 37;

    if (isset($_REQUEST['SAfdeling'])) 
      $SAfdeling=$_REQUEST['SAfdeling'];

    if (isset($_REQUEST['SNummer'])) 
      $SNummer=$_REQUEST['SNummer'];

    if (isset($_REQUEST['SPId'])) 
      $SPId=$_REQUEST['SPId'];

    if (isset($_REQUEST['SNavn'])) 
      $SNavn=$_REQUEST['SNavn'];

    if (isset($_REQUEST['SAdresse'])) 
      $SAdresse=$_REQUEST['SAdresse'];

    if (isset($_REQUEST['SBy'])) 
      $SBy=$_REQUEST['SBy'];

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

    $ip = getenv ("REMOTE_ADDR"); 
	$auth = isset($_SERVER['SSL_CLIENT_S_DN_CN']); 

	$Medlem = 0;
	$Fornavn = $SNavn;
	$Efternavn = "";
    $where = "";
	$line = 0;

    include_once("conn.inc");  
    $dbLink = pg_connect($conn_www);
    

    if ($SLobId == 40){
  	  if (($SNummer)&&(!$SPId)){
        $Query="SELECT PersonId FROM tblGrandPrix2002Deltagere WHERE Nummer = $SNummer";
  	    $dbResult = pg_query($dbLink, $Query);
        if (($dbResult) && (pg_numrows($dbResult) == 1)){
          $row = pg_fetch_array($dbResult, NULL , PGSQL_ASSOC);
      	  $SPId = $row["personid"];
  	    }
  	  }
	}
	else{
  	  if (($SNummer)&&(!$SPId)){
        $Query="SELECT PersonId, Afdeling FROM vLobTilmelding WHERE LobId = $SLobId AND Nummer = $SNummer";
  	    $dbResult = pg_query($dbLink, $Query);
        if (($dbResult) && (pg_numrows($dbResult) == 1)){
          $row = pg_fetch_array($dbResult, NULL , PGSQL_ASSOC);
      	  $SPId = $row["personid"];
          $SAfdeling=$row["afdeling"];
  	    }
  	  }
	}

	if (($SNummer)&&($SPId)){
      $Query="SELECT Id FROM tblPerson WHERE Id = $SPId";
	  $dbResult = pg_query($dbLink, $Query);
      if (pg_numrows($dbResult) == 1){
        if ($SAfdeling == 0)
		  $SAfdeling = "null";		
        $Query="INSERT INTO tblLobDeltager (LobId, Afdeling, Nummer, PersonId) values($SLobId, $SAfdeling, $SNummer, $SPId)";
	    $dbResult = pg_query($dbLink, $Query);
		$SNummer = ""; 
		$SPId = "";
		$SNavn = "";
		$SAdresse ="";
		$SBy ="";
        $SAfdeling="";
	  }
	}

	$Query="SELECT Navn, Dato, getafdelinger(Id, 0) AS afdeling_dropdown_html
	        FROM tblLob WHERE Id = $SLobId";
	$dbResult = pg_query($dbLink, $Query);
    $row = pg_fetch_array($dbResult, NULL , PGSQL_ASSOC);
    $LobNavn = $row["navn"];
    $LobDato = $row["dato"];
    $afdeling_dropdown_html = $row["afdeling_dropdown_html"];

	$Query="
		SELECT 	tblLobDeltager.Nummer As Nummer, 
				tblAfdeling.Afdnavn As Afdeling, 
				tblLobDeltager.PersonId As PId, 
				(tblPerson.Fornavn || ' ' || tblPerson.Efternavn) AS Navn, 
				tblPerson.Adresse As Adresse, 
				tblPerson.ByNavn As ByNavn 
		FROM 	tblLobDeltager 
				LEFT JOIN tblPerson ON tblLobDeltager.PersonId = tblPerson.Id
				LEFT JOIN tblAfdeling ON tblLobDeltager.Afdeling = tblAfdeling.Id
		WHERE	tblLobDeltager.LobId = $SLobId
	    ORDER BY tblLobDeltager.Nummer";
	$dbResult = pg_query($dbLink, $Query);
?>
<form name="Search" method="post" action="<?=$_SERVER['SCRIPT_NAME']?>?TopWin=<?=$TopWin?>&SLobId=<?=$SLobId;?>" language="javascript" onsubmit="return(ValidatorOnSubmit());">
 <p align=left>
   <font color=#003399 size=4>
     L&oslash;bs Deltagere 
     <?=$LobNavn;?> <?=$LobDato;?>
   </font>
 </p>
<table>
      <tr style=background-color:#FF0000><font color=#003399> 
        <th align=center>Nr</th>
        <th align=center>PId</th>
        <th align=center>Afd</th>
        <th align=center>Navn</th>
        <th align=center>Adresse</th>
        <th align=center>By</th>
        <th align=left>&nbsp;</th>
      </font></tr>
      
	  <tr valign=top style=background-color:<?=getcolor();?>>
        <td align=left><input type="text" name="SNummer" value="<?=$SNummer;?>" size="3" maxlength="5"></td>
        <td align=left><input type="text" name="SPId" value="<?=$SPId;?>" size="3" maxlength="5"></td>
        <td align=left><select name="SAfdeling"><?=$afdeling_dropdown_html?></select></td>
        <td align=left><input type="text" name="SNavn" value="<?=$SNavn;?>" size="18" maxlength="35"></td>
        <td align=left><input type="text" name="SAdresse" value="<?=$SAdresse;?>" size="18" maxlength="35"></td>
        <td align=left><input type="text" name="SBy" value="<?=$SBy;?>" size="12" maxlength="25"></td>
        <td align=left valign="middle"> 
          <input type="submit" name="Send" value="S">
        </td>
      </tr>

<?
    $buf = 0;
	$break = 0;
	for (;;)
	{
	  if (!$dbResult) break;
	  $row = pg_fetch_array($dbResult, NULL , PGSQL_ASSOC);
	  $break = ($r["pid"] != $row["pid"]);
	  if ((($buf)&&($break))||($buf)&&(!$row))
	  {
        $line++;
?>
        <tr valign=top style=background-color:<?=getcolor();?>>
          <td align=left><?=$r["nummer"];?></td>
          <td align=left><?=$r["pid"];?></td>
          <td align=left><?=$r["afdeling"];?></td>
          <td align=left><?=$r["navn"];?></td>
          <td align=left><?=$r["adresse"];?></td>
          <td align=left><?=$r["bynavn"];?></td>
          <td align=left valign="middle"><?=$r["buttom"];?></td>
        </tr>
<?
	  }
	  if (!$row) break;
	  if (($buf)&&(!$break))
	  {
	  }
	  if ((!$buf)||(($buf)&&($break)))
	  {
	    $r["nummer"]   = $row["nummer"];
	    $r["pid"]	   = $row["pid"];
	    $r["afdeling"] = $row["afdeling"];
        $r["navn"]     = $row["navn"];
        $r["adresse"]  = $row["adresse"];
        $r["bynavn"]   = $row["bynavn"];
        $r["buttom"]   = "<a href=\"javascript:SelectLine(" . $row["pid"] . ")\" onMouseOut=\"MM_swapImgRestore();\" onMouseOver=\"MM_swapImage('vpil" . $row["pid"] . "','','/images/vpil_f2.gif',1);\" ><img name=\"vpil" . $row["pid"] . "\" src=\"/images/vpil.gif\" width=\"15\" height=\"15\" border=\"0\"></a>";
        $buf = 1;
	  }
	}
?>
</table>
</form>
</body>
<script language="javascript">
<!--
function fonLoad(){
    var SNummer;
	var h = 300;
	var w = 675;
	var l = <?=$line?>;
	if (l>23){
      w += 15;
	  l = 23;
	}
	window.resizeTo(w, h+(l*25));
    SNummer = document.all['SNummer'];
	SNummer.focus();
}

function SelectLine(id){
	var newwindow;
	newwindow = window.open("/admin/PersonUpd.php?DoWhat=3&Id="+id+"&TopWin=<?=$TopWin?>","PersonUpd","status");
	newwindow.focus();
}

function ValidatorOnSubmit(){
    var SNummer;
    var SPId;
	var SNavn;
    SNummer = document.all['SNummer'];
    SPId = document.all['SPId'];
    SNavn = document.all['SNavn'];
	if ((SNummer.value>0) && (SPId.value>0)){
	  return true;
	}

	if ((SNummer.value>0) && (SNavn.value!="")){
	  var newwindow;
      var SNavn;
	  var SAdresse;
	  var SBy;
      var url;
	  url = "/admin/PListe2.php?TopWin=<?=$TopWin?>";
      SNavn = document.all['SNavn'];
	  if (SNavn.value!="")
	    url += "&SNavn=" + SNavn.value;
      SAdresse = document.all['SAdresse'];
	  if (SAdresse.value!="")
	    url += "&SAdresse=" + SAdresse.value;
      SBy = document.all['SBy'];
	  if (SBy.value!="")
	    url += "&SBy=" + SBy.value;
	  newwindow = window.open(url,"TestPersonUpd","status, scrollbars, resizeable");
	  newwindow.focus();
      return false;
	}

	if (SNummer.value>0){
	  return true;
	}

    return false;
}

// -->
</script>
</html>
