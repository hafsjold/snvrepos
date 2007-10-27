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
<title>Opgaver</title>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
</head>
<body bgcolor="#FFFFFF" text="#000000" onLoad="fonLoad(); MM_preloadImages('images/vpil_f2.gif');">
<?
    setlocale (LC_TIME, "da_DK.ISO8859-1");
    if (isset($_REQUEST['LinieId'])) 
      $LinieId=$_REQUEST['LinieId'];

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
	$auth = ($ip=='192.168.1.21');

	$Medlem = 1;
	$Fornavn = "";
	$Efternavn = "";
    $where = "";
	$line = 0;

	if (($Fornavn)||($Efternavn)||($Medlem)){
	  $where = " WHERE ";
	  $and = 0;
	  if ($Fornavn) {
	    if ($and) $where .= " AND ";
	    $where .= "tblPerson.Fornavn ILIKE '%" . $Fornavn . "%' ";
	    $and = 1;
	  }
	  if ($Efternavn) {
	    if ($and) $where .= " AND ";
	    $where .= "tblPerson.Efternavn ILIKE '%" . $Efternavn . "%' ";
	    $and = 1;
	  }
	  if ($Medlem) {
	    if ($and) $where .= " AND ";
	    $where .= "tblPuls3060Medlem.PersonId Is Not Null";
	    $and = 1;
	  }
	}

    include_once("conn.inc");  
    $dbLink = pg_connect($conn_www);
    
	$Query="
		SELECT 	tblPerson.Id As PId, 
				(tblPerson.Fornavn || ' ' || tblPerson.Efternavn) AS Navn, 
				tblPerson.Adresse As Adresse, 
				tblPerson.ByNavn As ByNavn 
		FROM 	tblPerson
				LEFT JOIN tblPuls3060Medlem ON tblPerson.Id	= tblPuls3060Medlem.PersonId
		$where";
	$Query .= " ORDER BY tblPerson.FnSound, tblPerson.EnSound, PId";
	$dbResult = pg_query($dbLink, $Query);
?>
    <table>
      <tr style=background-color:#FF0000><font color=#003399> 
        <th align=center>Navn</th>
        <th align=center>Adresse</th>
        <th align=center>By</th>
        <th align=left>&nbsp;</th>
      </font></tr>
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
	    $r["pid"]	   = $row["pid"];
        $r["navn"]     = $row["navn"];
        $r["adresse"]  = $row["adresse"];
        $r["bynavn"]   = $row["bynavn"];
        $r["buttom"]   = "<a href=\"javascript:SelectPerson(" . $row["pid"] . ", " . $LinieId . ", '" . $row["navn"] . "')\" onMouseOut=\"MM_swapImgRestore();\" onMouseOver=\"MM_swapImage('vpil" . $row["pid"] . "','','images/vpil_f2.gif',1);\" ><img name=\"vpil" . $row["pid"] . "\" src=\"images/vpil.gif\" width=\"15\" height=\"15\" border=\"0\"></a>";
        $buf = 1;
	  }
	}
?>
    </table>
</body>
<script language="javascript">
<!--
var newwindow;
function SelectPerson(id,lineid,navn){
    var ParentWindow;
	ParentWindow = top.opener;
	ParentWindow.document.all.item('PersonId'+lineid).innerText=id;
	ParentWindow.document.all.item('PersonNavn'+lineid).innerText=navn;
	ParentWindow.focus();
	window.close();
}
function fonLoad(){
	var h = 70;
	var w = 475;
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
