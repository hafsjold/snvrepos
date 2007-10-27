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
<title>Person opdatering</title>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
</head>
<body bgcolor="#FFFFFF" text="#000000" onLoad="fonLoad(); MM_preloadImages('/images/vpil_f2.gif')">
<?
    setlocale (LC_TIME, "da_DK.ISO8859-1");

    if (isset($_REQUEST['TopWin'])) 
      $TopWin=$_REQUEST['TopWin'];
    else
      $TopWin = $_SERVER['SCRIPT_NAME'];

    if (isset($_REQUEST['DoWhat'])) 
      $DoWhat=$_REQUEST['DoWhat'];
    else 
      $DoWhat = 0;

    if (isset($_REQUEST['TableId'])) 
      $TableId=$_REQUEST['TableId'];
    else
      $TableId = 4;

    if (isset($_REQUEST['StartId'])) 
      $StartId=$_REQUEST['StartId'];
    else
      $StartId = 0;

    if (isset($_REQUEST['AktId'])) 
      $AktId=$_REQUEST['AktId'];
    else
      $AktId = 0;

    if (isset($_REQUEST['LobId'])) 
      $LobId=$_REQUEST['LobId'];
    else
      $LobId = 0;

    if (isset($_REQUEST['SId'])) 
      $SId=$_REQUEST['SId'];

    if (isset($_REQUEST['SPId'])) 
      $SPId=$_REQUEST['SPId'];

    if (isset($_REQUEST['SId_Get'])) 
      $SId_Get=$_REQUEST['SId_Get'];

    if (isset($_REQUEST['SPId_Get'])) 		   
      $SPId_Get=$_REQUEST['SPId_Get'];

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

	$line = 0;

	switch ($TableId){
	  case 1:
	    $TABLE = "tblPersonDataExchange_1";
		break;
	  case 2:
	    $TABLE = "tblPersonDataExchange_2";
		break;
	  case 3:
	    $TABLE = "tblPersonDataExchange_3";
		break;
	  case 4:
	    $TABLE = "tblLobTilmelding";
		break;
	  case 5:
	    $TABLE = "tblNytMedlem";
	    $VIEW  = "vNytMedlem";
		break;
	  case 6:
	    $TABLE = "tblPersonDataExchange_4";
		break;
	  case 7:
	    $TABLE = "tblGrandPrix2002Deltagere";
		break;
	  case 8:
	    $TABLE = "tblakttilmelding";
		break;
	  case 9:
	    $TABLE = "tblGrandPrix2003Deltagere";	
		break;
	  case 10:
	    $TABLE = "tblPersonDataExchange_6";
		break;
	
	}
    
    include_once("conn.inc");  
    $dbLink = pg_connect($conn_www);
    

	if ($DoWhat == 2){
	  if (($SId)&&($SPId)){
        $Query="SELECT Id FROM tblPerson WHERE Id = $SPId";
	    $dbResult = pg_query($dbLink, $Query);
        if (pg_numrows($dbResult) == 1){
          $Query="UPDATE $TABLE SET PersonId = $SPId WHERE Id = $SId";
	      $dbResult = pg_query($dbLink, $Query);
	      
	      $StartId = $SId; 
	      $SId = ""; 
	      $SPId = "";
	      $SNavn = "";
	      $SFornavn = "";
	      $SEfternavn = "";
	      $SAdresse ="";
	      $SBy ="";
	    }
	  }
	}

    $Overskrift = "Synkronisering af personer fra " . $TABLE;
	
	switch ($TableId){
	  case 1:
	  case 2:
	  case 3:
	  case 6:
	  case 10:
	    $Query="
	      SELECT Id 		As Id, 
			     PersonId 	As PId, 
			     (Fornavn || ' ' || Efternavn) AS Navn, 
			     Fornavn 	    AS Fornavn, 
			     Efternavn 	    AS Efternavn, 
			     Adresse 	    As Adresse, 
                 Postnr  	    As Postnr,   
			     ByNavn 	    As ByNavn, 
                 TlfNr 		    As TlfNr,
                 ''             As MailAdr,    
                 Indmeldt 	    As Indmeldt, 
                 ''      	    As BetaltTilDato, 
                 Udmeldt 	    As Udmeldt,  
                 Kon 		    As Kon,      
                 FodtDato 	    As FodtDato, 
                 year(FodtDato)	As FodtAar"; 
		break;
	  case 4:
	    $Query = "SELECT Navn, Dato FROM tblLob WHERE Id = $LobId";
        $dbResult = pg_query($dbLink, $Query);
        $row = pg_fetch_array($dbResult, NULL , PGSQL_ASSOC);
        $Overskrift = "Symkronisering af tilmeldinger til " . $row["navn"];

	    $Query="
	      SELECT Id 		As Id, 
			     PersonId 	As PId, 
			     (Fornavn || ' ' || Efternavn) AS Navn, 
			     Fornavn 	AS Fornavn, 
			     Efternavn 	AS Efternavn, 
			     Adresse 	As Adresse, 
                 Postnr  	As Postnr,   
			     ByNavn 	As ByNavn, 
                 TlfNr 		As TlfNr,
                 MailAdr    As MailAdr,    
                 ''     	As Indmeldt, 
                 ''      	As BetaltTilDato, 
                 ''     	As Udmeldt,  
                 Kon 		As Kon,      
                 ''     	As FodtDato, 
                 FodtAar   	As FodtAar"; 
		break;
	  case 5:
        $Overskrift = "Synkronisering af nye medlemmer";
	    $Query="
	      SELECT Id 		As Id, 
			     PersonId 	As PId, 
			     (Fornavn || ' ' || Efternavn) AS Navn, 
			     Fornavn 	       AS Fornavn, 
			     Efternavn 	       AS Efternavn, 
			     Adresse 	       As Adresse, 
                 Postnr  	       As Postnr,   
			     ByNavn 	       As ByNavn, 
                 TlfNr 		       As TlfNr,
                 MailAdr           As MailAdr,    
                 indmeldtdato      As Indmeldt, 
                 kontingenttildato As BetaltTilDato, 
                 ''     	       As Udmeldt,  
                 Kon 		       As Kon,      
                 FodtDato  	       As FodtDato, 
                 year(FodtDato)    As FodtAar"; 
		break;
	  case 7:
	    $Query="
	      SELECT Id 		As Id, 
			     PersonId 	As PId, 
			     (Fornavn || ' ' || Efternavn) AS Navn, 
			     Fornavn 	AS Fornavn, 
			     Efternavn 	AS Efternavn, 
			     Adresse 	As Adresse, 
                 Postnr  	As Postnr,   
			     ByNavn 	As ByNavn, 
                 '' 		As TlfNr,
                 ''         As MailAdr,    
                 ''     	As Indmeldt, 
                 ''      	As BetaltTilDato, 
                 ''     	As Udmeldt,  
                 Kon 		As Kon,      
                 ''     	As FodtDato, 
                 FodtAar   	As FodtAar"; 
		break;
	  case 8:
	    $Query = "SELECT klinietext As Navn, kliniedato As Dato FROM tblklinie WHERE klinieid = $AktId";
        $dbResult = pg_query($dbLink, $Query);
        $row = pg_fetch_array($dbResult, NULL , PGSQL_ASSOC);
        $Overskrift = "Symkronisering af aktivitetstilmeldinger til " . $row["navn"] . " " . $row["dato"];

	    $Query="
	      SELECT Id 		As Id, 
			     PersonId 	As PId, 
			     (Fornavn || ' ' || Efternavn) AS Navn, 
			     Fornavn 	AS Fornavn, 
			     Efternavn 	AS Efternavn, 
			     Adresse 	As Adresse, 
                 Postnr  	As Postnr,   
			     ByNavn 	As ByNavn, 
                 TlfNr 		As TlfNr,
                 MailAdr    As MailAdr,    
                 ''     	As Indmeldt, 
                 ''      	As BetaltTilDato, 
                 ''     	As Udmeldt,  
                 '' 		As Kon,      
                 ''     	As FodtDato, 
                 ''	    	As FodtAar"; 
		break;
	}

	if ($DoWhat == 1)
	  $Query .=" FROM $TABLE WHERE Id = $SId_Get";
	else{
	  switch ($TableId){
	    case 4:
	      if ($LobId >0)
            $Query .=" FROM vlobtilmelding WHERE LobId = $LobId AND Id >= $StartId ORDER BY Id";
          else
	        $Query .=" FROM $TABLE WHERE Id >= $StartId ORDER BY Id";
		  break;
	    
	    case 5:
	      $Query .=" FROM $VIEW WHERE (((Id >= $StartId) AND (betalttildato is null) AND (action is not null) AND ((Indmeldt is null) OR (Indmeldt > (now()- interval '120 days')))) OR ((personid = 0) OR (personid is null))) ORDER BY Id DESC";
		  break;

	    case 8:
	      if ($AktId >0)
            $Query .=" FROM $TABLE WHERE AktId = $AktId AND Id >= $StartId ORDER BY Id";
          else
	        $Query .=" FROM $TABLE WHERE Id >= $StartId ORDER BY Id";
		  break;

	    default:
	      $Query .=" FROM $TABLE WHERE Id >= $StartId ORDER BY Id";
		  break;
	  }
    }
	$dbResult = pg_query($dbLink, $Query);
?>
<p align=left><font color=#003399 size=4><?=$Overskrift?></font></p>

<table>
      <tr style=background-color:#FF0000><font color=#003399> 
        <th align=center>Id</th>
        <th align=center>PId</th>
        <th align=center>Navn</th>
        <th align=center>Adresse</th>
        <th align=center>By</th>
	  <? if ($TableId == 5){ ?>
        <th align=center>Indmeldt</th>
	  <? } ?>
	  <? if ($DoWhat != 1){ ?>
          <th align=left>&nbsp;</th>
	  <? } ?>
      </font></tr>
<?
    $buf = 0;
	$break = 0;
	for (;;)
	{
	  if (!$dbResult) break;
	  $row = pg_fetch_array($dbResult, NULL , PGSQL_ASSOC);
	  $break = ($r["id"] != $row["id"]);
	  if ((($buf)&&($break))||($buf)&&(!$row))
	  {
        $line++;
     	if ($DoWhat == 1){
//echo("<br>Debug Line: $line");
		  $DFornavn   = $r["fornavn"];
		  $DEfternavn = $r["efternavn"];
		  $DAdresse   = $r["adresse"];
		  $DPostnr	  = $r["postnr"];
		  $DByNavn	  = $r["bynavn"];
		  $DTlfNr0	  = $r["tlfnr"];
		  if ($DTlfNr0)
		    $DTlfType0  = "Hjem";
		  else
		    $DTlfType0 = "";
		  $DMailAdr0   = $r["mailadr"];
		  if ($DMailAdr0)
		    $DMailType0  = "Hjem";
		  else
		    $DMailType0 = "";
		  $DIndmeldt      = $r["indmeldt"];
		  $DBetaltTilDato = $r["betalttildato"];
		  $DUdmeldt       = $r["udmeldt"];
		  $DKon	          = $r["kon"];
		  $DFodtDato      = $r["fodtdato"];
		  $DFodtAar       = $r["fodtaar"];

		  $pPam = "";
		  if ($DFornavn!="")
		    $pPam .= "&SNavn=$DFornavn";
		  if ($DAdresse)
		    $pPam .= "&SAdresse=$DAdresse";
		  if ($DByNavn)
		    $pPam .= "&SBy=$DByNavn";
		  
		  if ($DFornavn)
		    $pPam .= "&DFornavn=$DFornavn";
		  if ($DEfternavn)
		    $pPam .= "&DEfternavn=$DEfternavn";
		  if ($DAdresse)
		    $pPam .= "&DAdresse=$DAdresse";
		  if ($DPostnr)
		    $pPam .= "&DPostnr=$DPostnr";
		  if ($DByNavn)
		    $pPam .= "&DByNavn=$DByNavn";
		  if ($DTlfNr0)
		    $pPam .= "&DTlfNr0=$DTlfNr0";
		  if ($DTlfType0)
		    $pPam .= "&DTlfType0=$DTlfType0";
		  if ($DMailAdr0)
		    $pPam .= "&DMailAdr0=$DMailAdr0";
		  if ($DMailType0)
		    $pPam .= "&DMailType0=$DMailType0";
		  if ($DIndmeldt)
		    $pPam .= "&DIndmeldt=$DIndmeldt";
		  if ($DBetaltTilDato)
		    $pPam .= "&DBetaltTilDato=$DBetaltTilDato";
		  if ($DUdmeldt)
		    $pPam .= "&DUdmeldt=$DUdmeldt";
		  if ($DKon)
		    $pPam .= "&DKon=$DKon";
		  if ($DFodtDato)
		    $pPam .= "&DFodtDato=$DFodtDato";
		  if ($DFodtAar)
		    $pPam .= "&DFodtAar=$DFodtAar";
		}
?>
        <tr valign=top style=background-color:<?=getcolor();?>>
          <td align=left><?=$r["id"];?></td>
          <td align=left><?=$r["pid"];?></td>
          <td align=left><?=$r["navn"];?></td>
          <td align=left><?=$r["adresse"];?></td>
          <td align=left><?=$r["bynavn"];?></td>
	  	<? if ($TableId == 5){ ?>
          <td align=left><?=$r["indmeldt"];?></td>
	  	<? } ?>
	    <? if ($DoWhat != 1){ ?>
          <td align=left valign="middle"><?=$r["buttom"];?></td>
	    <? } ?>
        </tr>
<?
	  }
	  if (!$row) break;
	  if (($buf)&&(!$break))
	  {
	  }
	  if ((!$buf)||(($buf)&&($break)))
	  {
	    $r["id"]            = $row["id"];
	    $r["pid"]	        = $row["pid"];
        $r["navn"]          = $row["navn"];
        $r["fornavn"]       = $row["fornavn"];
        $r["efternavn"]     = $row["efternavn"];
        $r["adresse"]       = $row["adresse"];
		$r["postnr"]	    = $row["postnr"];	 
        $r["bynavn"]        = $row["bynavn"];
		$r["tlfnr"]	        = $row["tlfnr"];	 
		$r["mailadr"]       = $row["mailadr"];	 
		$r["indmeldt"]      = $row["indmeldt"]; 
		$r["betalttildato"] = $row["betalttildato"]; 
		$r["udmeldt"]	    = $row["udmeldt"];	 
		$r["kon"]		    = $row["kon"];		 
		$r["fodtdato"]      = $row["fodtdato"]; 
		$r["fodtaar"]       = $row["fodtaar"]; 
        $r["buttom"]        = "<a href=\"javascript:SelectLine(" . $row["id"] . ", " . (($row["pid"])?$row["pid"]:0) .")\" onMouseOut=\"MM_swapImgRestore();\" onMouseOver=\"MM_swapImage('vpil" . $row["id"] . "','','/images/vpil_f2.gif',1);\" ><img name=\"vpil" . $row["id"] . "\" src=\"/images/vpil.gif\" width=\"15\" height=\"15\" border=\"0\"></a>";
        $buf = 1;
	  }
	}
?>
</table>

<? if ($DoWhat == 1){ ?>
  <form name="UpdatePersonId" method="post" action="<?=$_SERVER['SCRIPT_NAME']?>?DoWhat=2&StartId=<?=$StartId?>&TableId=<?=$TableId?>&AktId=<?=$AktId?>&LobId=<?=$LobId?>&TopWin=<?=$TopWin?>">
    <input type="hidden" name="SId" value="<?=$SId_Get?>">
  <? if ($SPId_Get > 0){ ?>
    <input type="hidden" name="SPId" value="<?=$SPId_Get?>" >
  <? }else{ ?>
    <input type="hidden" name="SPId" >
  <? } ?>
  </form>
<? }else{ ?>
  <form name="GetSelectedRecord" method="post" action="<?=$_SERVER['SCRIPT_NAME']?>?DoWhat=1&StartId=<?=$StartId?>&TableId=<?=$TableId?>&AktId=<?=$AktId?>&LobId=<?=$LobId?>&TopWin=<?=$TopWin?>">
    <input type="hidden" name="SId_Get">
    <input type="hidden" name="SPId_Get">
  </form>
<? } ?>

</body>
<script language="javascript">
<!--
function fonLoad(){
    var SId;
	var h = 270;
	var w = 575;
	var l = <?=$line?>;
  	<? if ($TableId == 5){ ?>
      w += 100;
  	<? } ?>
	if (l>23){
      w += 15;
	  l = 23;
	}
	window.resizeTo(w, h+(l*25));
<? if ($DoWhat == 1){ ?>
    SelectLine();
<? } ?>
}

<? if ($DoWhat == 1){ ?>
  function SelectLine(){
	var newwindow;
    var url;
  <? if ($SPId_Get > 0){ ?>
	url = "/admin/PersonUpd.php?DoWhat=3&TopWin=<?=$TopWin?>&Id=<?=$SPId_Get?><?=$pPam?>";
  <? }else{ ?>
	url = "/admin/PListe2.php?TopWin=<?=$TopWin?><?=$pPam?>";
  <? } ?>
	newwindow = window.open(url,"TestPersonUpd","status, scrollbars, resizeable");
	newwindow.focus();
  }
<? }else{ ?>
  function SelectLine(SId, SPId){
	var GetSelectedRecord;
	var SId_Get;
	var SPId_Get;
    SId_Get = document.all['SId_Get'];
    SId_Get.value=SId;
    SPId_Get = document.all['SPId_Get'];
    SPId_Get.value=SPId;
    GetSelectedRecord = document.all['GetSelectedRecord'];
    GetSelectedRecord.submit();
  }
<? } ?>
// -->
</script>
</html>
