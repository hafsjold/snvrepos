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
<title>Person Grupper</title>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
</head>
<body bgcolor="#FFFFFF" text="#000000" onLoad="fonLoad(); MM_preloadImages('/images/vpil_f2.gif')">
<?
    setlocale (LC_TIME, "da_DK.ISO8859-1");

    if (isset($_REQUEST['tpgmid'])) 
      $tpgmid=$_REQUEST['tpgmid'];
    else
      $tpgmid=8;

    if (isset($_REQUEST['MaxRow'])) 
      $MaxRow=$_REQUEST['MaxRow'];
    else
      $MaxRow=50;

    if (isset($_REQUEST['DoWhat'])) 
      $DoWhat=$_REQUEST['DoWhat'];

    if (isset($_REQUEST['TopWin'])) 
      $TopWin=$_REQUEST['TopWin'];
    else
      $TopWin = $_SERVER['SCRIPT_NAME'];

    if (isset($_REQUEST['SGId'])) 
      $SGId=$_REQUEST['SGId'];

    if (isset($_REQUEST['SNavn'])) 
      $SNavn=$_REQUEST['SNavn'];

    if (isset($_REQUEST['SAdresse'])) 
      $SAdresse=$_REQUEST['SAdresse'];

    if (isset($_REQUEST['SBy'])) 
      $SBy=$_REQUEST['SBy'];

	$pPam = "";
	if ($_REQUEST['DFornavn']!="")
	  $pPam .= "&SNavn=".$_REQUEST['DFornavn'];
	if ($_REQUEST['DAdresse'])
	  $pPam .= "&SAdresse=".$_REQUEST['DAdresse'];
	if ($_REQUEST['DByNavn'])
	  $pPam .= "&SBy=".$_REQUEST['DByNavn'];
	if ($_REQUEST['DFornavn'])
	  $pPam .= "&DFornavn=".$_REQUEST['DFornavn'];
	if ($_REQUEST['DEfternavn'])
	  $pPam .= "&DEfternavn=".$_REQUEST['DEfternavn'];
	if ($_REQUEST['DAdresse'])
	  $pPam .= "&DAdresse=".$_REQUEST['DAdresse'];
	if ($_REQUEST['DPostnr'])
	  $pPam .= "&DPostnr=".$_REQUEST['DPostnr'];
	if ($_REQUEST['DByNavn'])
	  $pPam .= "&DByNavn=".$_REQUEST['DByNavn'];
	if ($_REQUEST['DTlfNr']."0")
	  $pPam .= "&DTlfNr0=".$_REQUEST['DTlfNr']."0";
	if ($_REQUEST['DTlfType']."0")
	  $pPam .= "&DTlfType0=".$_REQUEST['DTlfType']."0";
	if ($_REQUEST['DMailAdr']."0")
	  $pPam .= "&DMailAdr0=".$_REQUEST['DMailAdr']."0";
	if ($_REQUEST['DMailType']."0")
	  $pPam .= "&DMailType0=".$_REQUEST['DMailType']."0";
	if ($_REQUEST['DIndmeldt'])
	  $pPam .= "&DIndmeldt=".$_REQUEST['DIndmeldt'];
	if ($_REQUEST['DUdmeldt'])
	  $pPam .= "&DUdmeldt=".$_REQUEST['DUdmeldt'];
	if ($_REQUEST['DKon'])
	  $pPam .= "&DKon=".$_REQUEST['DKon'];
	if ($_REQUEST['DFodtDato'])
	  $pPam .= "&DFodtDato=".$_REQUEST['DFodtDato'];
	if ($_REQUEST['DFodtAar'])
	  $pPam .= "&DFodtAar=".$_REQUEST['DFodtAar'];

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

	if (($SGId)||($SNavn)||($SAdresse)||($SBy)){
	  $where = " WHERE ";
	  $and = 0;
	  if ($SGId) {
	    if ($and) $where .= " AND ";
	    if ($SGId == 1)
		  $where .= "tblPuls3060Medlem.PersonId IS NOT NULL AND tblPuls3060Medlem.Udmeldt IS NULL";
		else  
		  $where .= "tblPerson.Id = " . $SGId;
	    $and = 1;
	  }
	  if ($SNavn) {
	    if ($and) $where .= " AND ";
	    $where .= "(tblPerson.Fornavn ILIKE '%" . $SNavn . "%' ";
	    $where .= " OR ";
	    $where .= "tblPerson.Efternavn ILIKE '%" . $SNavn . "%' )";
	    $and = 1;
	  }
	  if ($SAdresse) {
	    if ($and) $where .= " AND ";
	    $where .= "tblPerson.Adresse ILIKE '%" . $SAdresse . "%' ";
	    $and = 1;
	  }
	  if ($SBy) {
	    if ($and) $where .= " AND ";
	    $where .= "tblPerson.ByNavn ILIKE '%" . $SBy . "%' ";
	    $and = 1;
	  }
	}

    include_once("conn.inc");  
    $dbLink = pg_connect($conn_www);
    
	$Query="
		SELECT 	v_1_person_i_gruppe.persongruppeid As GId, 
				(tblPerson.Fornavn || ' ' || tblPerson.Efternavn) AS Navn, 
				tblPerson.Adresse As Adresse, 
				tblPerson.ByNavn As ByNavn 
		FROM 	tblPerson
				INNER JOIN v_1_person_i_gruppe ON tblPerson.Id	= v_1_person_i_gruppe.grppersonid
				LEFT JOIN tblPuls3060Medlem ON tblPerson.Id	= tblPuls3060Medlem.PersonId
		$where";
	$Query .= " ORDER BY tblPerson.FnSound, tblPerson.EnSound, GId";
	$Query .= " LIMIT $MaxRow";

	$Query="select distinct
  			 l.persongruppeid  As GId,
  			 (p.fornavn || ' ' || p.efternavn) as navn,
  			 p.Adresse As Adresse,
  			 p.ByNavn As ByNavn,
			 p.FnSound,
             p.EnSound
  		from tbltlinie l
		inner join tblpersonref r on l.persongruppeid = r.persongruppeid
		left join tblperson p on r.personid = p.id
		where l.persongruppeid is not null and l.tpgmid = $tpgmid
		ORDER BY p.FnSound, p.EnSound, GId
		LIMIT $MaxRow";

	$dbResult = pg_query($dbLink, $Query);
?>
<table>
      <tr style=background-color:#FF0000><font color=#003399> 
        <th align=center>GId</th>
        <th align=center>Navn</th>
        <th align=center>Adresse</th>
        <th align=center>By</th>
        
    <th align=left> <a href="javascript:SelectHeading()" onMouseOut="MM_swapImgRestore();" onMouseOver="MM_swapImage('vpil','','/images/vpil_f2.gif',1)" ><img name="vpil" src="/images/vpil.gif" width="15" height="15" border="0"></a> 
    </th>
      </font></tr>
      
      <form name="Search" method="post" action="<?=$_SERVER['SCRIPT_NAME']?>?TopWin=<?=$TopWin?><?=$pPam?>">
	    <tr valign=top style=background-color:<?=getcolor();?>>
          <td align=left><input type="text" name="SGId" size="3" maxlength="5" value="<?=$SGId;?>"></td>
          <td align=left><input type="text" name="SNavn" size="18" maxlength="35" value="<?=$SNavn;?>"></td>
          <td align=left><input type="text" name="SAdresse" size="18" maxlength="35" value="<?=$SAdresse;?>"></td>
          <td align=left><input type="text" name="SBy" size="12" maxlength="25" value="<?=$SBy;?>"></td>
          <td align=left valign="middle"> 
            <input type="submit" name="Send" value="S">
          </td>
        </tr>
      </form>

<?
    $buf = 0;
	$break = 0;
	for (;;)
	{
	  if (!$dbResult) break;
	  $row = pg_fetch_array($dbResult, NULL , PGSQL_ASSOC);
	  $break = ($r["gid"] != $row["gid"]);
	  if ((($buf)&&($break))||($buf)&&(!$row))
	  {
        $line++;
?>
        <tr valign=top style=background-color:<?=getcolor();?>>
          <td align=left><?=$r["gid"];?></td>
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
	    $r["gid"]	   = $row["gid"];
        $r["navn"]     = $row["navn"];
        $r["adresse"]  = $row["adresse"];
        $r["bynavn"]   = $row["bynavn"];
        $r["buttom"]   = "<a href=\"javascript:SelectLine(" . $row["gid"] . ", '" . $row["navn"] . "')\" onMouseOut=\"MM_swapImgRestore();\" onMouseOver=\"MM_swapImage('vpil" . $row["gid"] . "','','/images/vpil_f2.gif',1);\" ><img name=\"vpil" . $row["gid"] . "\" src=\"/images/vpil.gif\" width=\"15\" height=\"15\" border=\"0\"></a>";
        $buf = 1;
	  }
	}
?>
    </table>
</body>
<script language="javascript">
<!--
<?if ($TopWin=="/admin/PListe2.php"){ ?>
  // Add Person
  function SelectHeading(){
    var newwindow;
	newwindow = window.open("/admin/PersonUpd.php?DoWhat=2&TopWin=<?=$TopWin?><?=$pPam?>","PersonUpd","status")
	newwindow.focus();
  }
  
  // Update Person
  function SelectLine(id,lineid,navn){
    var newwindow;
	newwindow = window.open("/admin/PersonUpd.php?DoWhat=3&Id="+id+"&TopWin=<?=$TopWin?><?=$pPam?>","PersonUpd","status")
	newwindow.focus();
  }
<? } // endif /admin/PListe2.php ?>

<?if ($TopWin=="/admin/PListe3.php"){ ?>
  // Add Person
  function SelectHeading(){
    var newwindow;
	newwindow = window.open("/admin/PersonUpd.php?DoWhat=2&TopWin=<?=$TopWin?><?=$pPam?>","PersonUpd","status")
	newwindow.focus();
  }

  // Select Person
  function SelectLine(id,lineid,navn){
    var ParentWindow;
    var SGId;
    var Search;
	ParentWindow = top.opener;
    SGId = ParentWindow.document.all['SGId'];
	SGId.value=id;
    Search = ParentWindow.document.all['Search'];
	Search.submit();
	window.close();
  }
<? } // endif /admin/PListe3.php ?>

<?if ($TopWin=="/admin/PListe4.php"){ ?>
  // Add Person
  function SelectHeading(){
    var newwindow;
	newwindow = window.open("/admin/PersonUpd.php?DoWhat=2&TopWin=<?=$TopWin?><?=$pPam?>","PersonUpd","status")
	newwindow.focus();
  }

  // Select Person
  function SelectLine(id,lineid,navn){
    var ParentWindow;
    var SGId;
    var UpdatePersonId;
	ParentWindow = top.opener;
    SGId = ParentWindow.document.all['SGId'];
	SGId.value=id;
    UpdatePersonId = ParentWindow.document.all['UpdatePersonId'];
	UpdatePersonId.submit();
	window.close();
  }
<? } // endif /admin/PListe4.php ?>

<?if ($TopWin=="/admin/TProgramUpd.php"){ ?>
  var newwindow;
  function SelectLine(id,navn){
    var ParentWindow;
	ParentWindow = top.opener;
	ParentWindow.document.all.item('PersonGruppeId').innerText=id;
	ParentWindow.document.all.item('PersonGruppeNavn').innerText=navn;
	ParentWindow.focus();
	window.close();
  }
<? } // endif /admin/TProgramUpd.php ?>

  function fonLoad(){
	var h = 270;
	var w = 525;
	var l = <?=$line?>;
	if (l>25){
      w += 15;
	  l = 25;
	}
	window.resizeTo(w, h+(l*25));
  }
// -->
</script>
</html>
