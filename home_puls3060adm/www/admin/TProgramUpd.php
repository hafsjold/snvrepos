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
<?
include_once("dbFunctions.inc");  

setlocale (LC_TIME, "da_DK.ISO8859-1");

if (isset($_REQUEST['TopWin'])) 
  $TopWin=$_REQUEST['TopWin'];
else
  $TopWin = $_SERVER['SCRIPT_NAME'];

if (isset($_REQUEST['TPgmId'])) 
  $TPgmId=$_REQUEST['TPgmId'];
else
  $TPgmId = 0;

if (isset($_REQUEST['DoWhat'])) 
  $DoWhat=$_REQUEST['DoWhat'];
else
  $DoWhat = 0;

if (isset($_REQUEST['line']))
  $line=$_REQUEST['line'];
else
  $line = 0;

if (isset($_REQUEST['TLinieID']))
  $TLinieID=$_REQUEST['TLinieID'];
else
  $TLinieID = 0;

if (isset($_REQUEST['TLinieDato'])) 
  $TLinieDato=$_REQUEST['TLinieDato'];
else
  $TLinieDato = "";

if (isset($_REQUEST['TLinieText'])) 
  $TLinieText=$_REQUEST['TLinieText'];
else
  $TLinieText = "";

if (isset($_REQUEST['TLinieDosis'])) 
  $TLinieDosis=$_REQUEST['TLinieDosis'];
else
  $TLinieDosis = "";

if (isset($_REQUEST['PersonGruppeId'])) 
  $PersonGruppeId=$_REQUEST['PersonGruppeId'];
else
  $PersonGruppeId = "";

if (isset($_REQUEST['TLinieDato_old']))
  $TLinieDato_old=$_REQUEST['TLinieDato_old'];
else
  $TLinieDato_old = "";

if (isset($_REQUEST['TLinieText_old']))
  $TLinieText_old=$_REQUEST['TLinieText_old'];
else
  $TLinieText_old = "";

if (isset($_REQUEST['TLinieDosis_old']))
  $TLinieDosis_old=$_REQUEST['TLinieDosis_old'];
else
  $TLinieDosis_old = "";

if (isset($_REQUEST['PersonGruppeId_old']))
  $PersonGruppeId_old=$_REQUEST['PersonGruppeId_old'];
else
  $PersonGruppeId_old = "";

include_once("conn.inc");  
$dbLink = pg_connect($conn_www);


$Fejl   = 0;

$onLoad = "fonLoad(); MM_preloadImages('/images/vpil.gif');MM_preloadImages('/images/vpil_f2.gif');";

switch($DoWhat)
{
  case 2:  // Vis Tom Record
    $Query="SELECT tliniedato as dato, 
                   EXTRACT(dow FROM tliniedato) as ugedag 
                   FROM tblTLinie 
                   WHERE tpgmid=$TPgmId
            UNION
            SELECT startdato as dato, 
                   EXTRACT(dow FROM startdato) as ugedag 
                   FROM tblTPgm 
                   WHERE tpgmid=$TPgmId
            ORDER BY 1 DESC
            LIMIT 1;";
    $dbResult = pg_query($dbLink, $Query);
    if ($row = pg_fetch_array($dbResult, NULL , PGSQL_ASSOC)) {
      $Dato   = $row["dato"];
      $UgeDag = $row["ugedag"];
      
      switch($UgeDag)	{
        case 1:  // mandag -> onsdag
		    $days = 2;
          break;
        case 2:  // tirsdag -> onsdag
		    $days = 1;
          break;
        case 3:  // onsdag -> fredag
		    $days = 2;
          break;
        case 4:  // torsdag -> fredag
		    $days = 1;
          break;
        case 5:  // fredag -> mandag
		    $days = 3;
          break;
        case 6:  // lørdag -> mandag
		    $days = 2;
          break;
        case 0:  // Søndag -> mandag
		    $days = 1;
          break;
      }
      
      $Query="SELECT TIMESTAMP '" . $Dato . "' + interval '" . $days . "days' as dato";
      $dbResult = pg_query($dbLink, $Query);
      if ($row = pg_fetch_array($dbResult, NULL , PGSQL_ASSOC)) {
        $TLinieDato   = $row["dato"];
 	  }
    }
 
    $line = 0;
    break;

  case 3:  // Vis Record
    
    $Query="SELECT * FROM tblTLinie WHERE TLinieID=$TLinieID";
    if ($dbResult = pg_query($dbLink, $Query))
      if ($row = pg_fetch_array($dbResult, NULL , PGSQL_ASSOC)) {
        $TLinieDato     = $row["tliniedato"];
        $TLinieText     = $row["tlinietext"];
        $TLinieDosis    = $row["tliniedosis"];
        $PersonGruppeId = $row["persongruppeid"];
      }

    $line = 0;
    if (isset($PersonGruppeId)){
      $Query="SELECT PersonId FROM tblPersonRef WHERE PersonGruppeId=$PersonGruppeId";
      if ($dbResult = pg_query($dbLink, $Query))
        while($row = pg_fetch_array($dbResult, NULL , PGSQL_ASSOC))
          $PersonId[$line++] = $row["personid"];
    }
    $PersonId[$line++] = 0;
    break;
  
  case 5:  // Gem rettet Record
 	
    if (!$TLinieDato) {
      $TLinieDatoFejl = "skal udfyldes";
      $Fejl = 1;
    }

    if (!$TLinieText) {
      $TLinieTextFejl = "skal udfyldes";
      $Fejl = 1;
    }

    //if (!$TLinieDosis) {
    //  $TLinieDosisFejl = "skal udfyldes";
    //  $Fejl = 1;
    //}

    //if (!$PersonGruppeId) {
    //  $PersonGruppeIdFejl = "skal udfyldes";
    //  $Fejl = 1;
    //}

    if (!$Fejl){
      $set ="SET ";
      $fset =0;
      if ($TLinieDato != $TLinieDato_old){
        if ($fset) $set .= ", ";
        $set .="TLinieDato = '$TLinieDato'";
        $fset =1;
      }
      if ($TLinieText != $TLinieText_old){
        if ($fset) $set .= ", ";
        $set .="TLinieText = '$TLinieText'";
        $fset =1;
      }
      if ($TLinieDosis != $TLinieDosis_old){
        if ($fset) $set .= ", ";
        $set .="TLinieDosis = '$TLinieDosis'";
        $fset =1;
      }
      
      // Test om et PersonId er ændret
      $chg =0;
      for($i=0; (($i<$_POST["line"]) && (!$chg)) ; $i++)
        if ($_POST["PersonId" . $i] != $_POST["PersonId" . $i . "_old"])
          $chg =1;

      // Opret en privat gruppe hvis PersonId er ændret
	  if ($chg)
	  {
        //$PersonGruppeId = ClearPrivatPersonGruppe($PersonGruppeId_old);
        //for($i=0; $i<$_POST["line"] ; $i++)
        //  InsertPersonIntoPersonGruppe($PersonGruppeId, $_POST["PersonId" . $i], 0);		
        
        $get_persongruppe_id_parm = "null";
        for($i=0; ($i<7) ; $i++)
        {
		  if ($i<$_POST["line"])
		    if ($_POST["PersonId" . $i] > 0)
		      $get_persongruppe_id_parm .= ", " . $_POST["PersonId" . $i]; 
		    else
		      $get_persongruppe_id_parm .= ", null"; 
	      else
		    $get_persongruppe_id_parm .= ", null"; 
	    }
	    $PersonGruppeId = get_persongruppe_id($get_persongruppe_id_parm);
	  }

      if ($PersonGruppeId != $PersonGruppeId_old){
        if ($fset) $set .= ", ";
        $set .="PersonGruppeId = '$PersonGruppeId'";
        $fset =1;
      }

      if ($fset) {
        $Query=" UPDATE tblTLinie " . $set . " WHERE TLinieID = $TLinieID";
        $dbResult = pg_query($dbLink, $Query);
      }
      
      $onLoad = "UpdateParentAndClose()";
    }
    break;

  case 6:  // Opret Record
 	
    if (!$TLinieDato) {
      $TLinieDatoFejl = "skal udfyldes";
      $Fejl = 1;
    }

    if (!$TLinieText) {
      $TLinieTextFejl = "skal udfyldes";
      $Fejl = 1;
    }

    //if (!$TLinieDosis) {
    //  $TLinieDosisFejl = "skal udfyldes";
    //  $Fejl = 1;
    //}

    if (!$Fejl){
      $insert =" (tpgmid";
      $value =" VALUES ( $TPgmId ";
      $fset =1;
      
      if ($fset) $insert .= ", ";
      if ($fset) $value .= ", ";
      $insert .="TLinieDato";
      $value .="'$TLinieDato'";
      $fset =1;

      if ($fset) $insert .= ", ";
      if ($fset) $value .= ", ";
      $insert .="TLinieText";
      $value .="'$TLinieText'";
      $fset =1;

      if ($fset) $insert .= ", ";
      if ($fset) $value .= ", ";
      $insert .="TLinieDosis";
      $value .="'$TLinieDosis'";
      $fset =1;
      
      if ($fset) {
        $insert .=") ";
        $value .=")";
        $Query=" INSERT INTO tblTLinie " . $insert . $value;
        $dbResult = pg_query($dbLink, $Query);
      }
      
      $onLoad = "UpdateParentAndClose()";
    }
    break;

  default:  // ingen case valgt

} // End Switch($DoWhat) 
?>

<head>
<title>TProgramUpd</title>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
</head>
  <body bgcolor="#FFFFFF" text="#000000" onLoad="<?=$onLoad?>">

      <? if ($DoWhat == 2) { // Vis tom record ?>
        <form name="Tilsagn" method="post" action="TProgramUpd.php?DoWhat=6&TPgmId=<?=$TPgmId?>">
        <table width="100%" border="0" bgcolor="#FFFFFF">
          <tr> 
            <td width="300" colspan="2"><b>
              <font color="#003399">Oprettelse af Træningsaktivitet</font></b>
            </td>
          </tr>
          <tr> 
            <td width="50">&nbsp;</td>
            <td width="350">&nbsp;</td>
          </tr>
          <tr> 
            <td width="50">Dato</td>
            
      <td width="350"> 
        <input type="text" name="TLinieDato" size="20" maxlength="25" value="<?=$TLinieDato?>">
        <input type="hidden" name="TLinieDato_old" value="<?=$TLinieDato?>">
            </td>
          </tr>
          <tr> 
            <td width="50" valign="top">Aktivitet</td>
            <td width="275"> 
              <textarea name="TLinieText" cols="25" rows="4"><?=$TLinieText?></textarea>
              <input type="hidden" name="TLinieText_old" value="<?=$TLinieText?>">
            </td>
          </tr>
          <tr> 
            <td width="50" valign="top">Intensitet</td>
            <td width="275"> 
              <textarea name="TLinieDosis" cols="25" rows="4"><?=$TLinieDosis?></textarea>
              <input type="hidden" name="TLinieDosis_old" value="<?=$TLinieDosis?>">
            </td>
          </tr>
           <tr> 
            <td width="50">&nbsp;</td>
            <td width="350">&nbsp;</td>
          </tr>
          <tr> 
            <td width="50">&nbsp;</td>
            <td width="350" bgcolor="#FFFFFF"> 
              <input type="submit" name="Send" value="Opret">
              <input type="submit" name="Fortryd" value="Fortryd" onClick="FocusParentAndClose()">
          </tr>
          <tr> 
            <td width="50">&nbsp;</td>
            <td width="350">&nbsp;</td>
          </tr>
        </table>
        </form>
      <? } // End If $DoWhat 2 ?>

      <? if ($DoWhat == 3) { // Vis record ?>
        <form name="Tilsagn" method="post" action="TProgramUpd.php?DoWhat=5&TLinieID=<?=$TLinieID?>">
        <table width="100%" border="0" bgcolor="#FFFFFF">
          <tr> 
            <td width="300" colspan="2"><b>
              <font color="#003399">Ændring af Træningsaktivitet</font></b>
            </td>
          </tr>
          <tr> 
            <td width="50">&nbsp;</td>
            <td width="350">&nbsp;</td>
          </tr>
          <tr> 
            <td width="50">Dato</td>
            <td width="350"> 
              
        <input type="text" name="TLinieDato" size="20" maxlength="25" value="<?=$TLinieDato?>">
              <input type="hidden" name="TLinieDato_old" value="<?=$TLinieDato?>">
            </td>
          </tr>
          <tr> 
            <td width="50" valign="top">Aktivitet</td>
            <td width="275"> 
              <textarea name="TLinieText" cols="25" rows="4"><?=$TLinieText?></textarea>
              <input type="hidden" name="TLinieText_old" value="<?=$TLinieText?>">
            </td>
          </tr>
          <tr> 
            <td width="50" valign="top">Intensitet</td>
            <td width="275"> 
              <textarea name="TLinieDosis" cols="25" rows="4"><?=$TLinieDosis?></textarea>
              <input type="hidden" name="TLinieDosis_old" value="<?=$TLinieDosis?>">
            </td>
          </tr>
          <?
               $ButtomGrp = "<a href=\"javascript:PListGrpLink()\" onMouseOut=\"MM_swapImgRestore();\" onMouseOver=\"MM_swapImage('vpilGrp','','/images/vpil_f2.gif',1);\" ><img name=\"vpilGrp\" src=\"/images/vpil.gif\" width=\"15\" height=\"15\" border=\"0\"></a>";
          ?>
          <tr> 
            <td width="50">Grp</td>
            <td width="350"> 
              <input type="text" name="PersonGruppeId" size="4" maxlength="8" value="<?=$PersonGruppeId?>">
              <?=$ButtomGrp?>&nbsp;<a name="PersonGruppeNavn<?=$i?>"><?=GetPersonGruppeNavn($PersonGruppeId)?></a>	
              <input type="hidden" name="PersonGruppeId_old" value="<?=$PersonGruppeId?>">
              <input type="hidden" name="line" value="<?=$line?>">
            </td>
          </tr>
          <? for($i=0; $i<$line; $i++) { 
               $Buttom = "<a href=\"javascript:PListLink(" . $i . ")\" onMouseOut=\"MM_swapImgRestore();\" onMouseOver=\"MM_swapImage('vpil" . $i . "','','/images/vpil_f2.gif',1);\" ><img name=\"vpil" . $i . "\" src=\"/images/vpil.gif\" width=\"15\" height=\"15\" border=\"0\"></a>";
          ?>
            <tr> 
              <td width="50">&nbsp;</td>
              <td width="350"> 
                <input type="text" name="PersonId<?=$i?>" size="4" maxlength="8" value="<?=$PersonId[$i]?>">
                <?=$Buttom?>&nbsp;<a name="PersonNavn<?=$i?>"><?=GetPersonNavn($PersonId[$i])?></a>	
                <input type="hidden" name="PersonId<?=$i?>_old" size="4" maxlength="8" value="<?=$PersonId[$i]?>">
              </td>
            </tr>
          <? } ?>
          <tr> 
            <td width="50">&nbsp;</td>
            <td width="350">&nbsp;</td>
          </tr>
          <tr> 
            <td width="50">&nbsp;</td>
            <td width="350" bgcolor="#FFFFFF"> 
              <input type="submit" name="Send" value="Opdater">
              <input type="submit" name="Fortryd" value="Fortryd" onClick="FocusParentAndClose()">
          </tr>
          <tr> 
            <td width="50">&nbsp;</td>
            <td width="350">&nbsp;</td>
          </tr>
        </table>
        </form>
      <? } // End If $DoWhat 3 ?>

      <? if ($DoWhat == 5) { // Rettet record gemt eller Vis record med fejl ?>
      <? if (!$Fejl) { // Rettet record gemt ?>
         <b><font color="#003399">Kviterings tekst her</font></b>
         <a href="javascript:UpdateParentAndClose()"> Retur</a> 
      <? } else { // Vis record med fejl ?>
        <form name="Tilsagn" method="post" action="TProgramUpd.php?DoWhat=5&TLinieID=<?=$TLinieID?>">
        <table width="100%" border="0" bgcolor="#FFFFFF">
          <tr> 
            <td width="300" colspan="2"><b>
              <font color="#003399">Ændring af Træningsaktivitet</font></b>
            </td>
          </tr>
          <tr> 
            <td width="50">&nbsp;</td>
            <td width="350">&nbsp;</td>
          </tr>
          <tr> 
            <td width="50">Dato</td>
            
      <td width="350"> 
        <input type="text" name="TLinieDato" size="20" maxlength="25" value="<?=$_POST["TLinieDato"]?>">
        <font color="#FF0000"> 
        <?=$TLinieDatoFejl?>
        </font> 
        <input type="hidden" name="TLinieDato_old" value="<?=$_POST["TLinieDato"]?>">
            </td>
          </tr>
          <tr> 
            <td width="50" valign="top">Aktivitet</td>
            <td width="275"> 
              <textarea name="TLinieText" cols="25" rows="4"><?=$_POST["TLinieText"]?></textarea>
        <font color="#FF0000"> 
        <?=$TLinieTextFejl?>
        <input type="hidden" name="TLinieText_old" value="<?=$_POST["TLinieText"]?>">
        </font> </td>
          </tr>
          <tr> 
            <td width="50" valign="top">Intensitet</td>
            <td width="275"> 
              <textarea name="TLinieDosis" cols="25" rows="4"><?=$_POST["TLinieDosis"]?></textarea>
        <font color="#FF0000"> 
        <?=$TLinieDosisFejl?>
        <input type="hidden" name="TLinieDosis_old" value="<?=$_POST["TLinieDosis"]?>">
        </font> </td>
          </tr>
          <tr> 
            <td width="50">Grp</td>
            <td width="350"> 
              <input type="text" name="PersonGruppeId" size="4" maxlength="8" value="<?=$_POST["PersonGruppeId"]?>">
              <?=GetPersonGruppeNavn($_POST["PersonGruppeId"])?>	
              <font color="#FF0000"> 
                <?=$PersonGruppeIdFejl?>
              </font> 
              <input type="hidden" name="PersonGruppeId_old" value="<?=$_POST["PersonGruppeId"]?>">
              <input type="hidden" name="line" value="<?=$_POST["line"]?>">
            </td>
          </tr>
          <? for($i=0; $i<$_POST["line"]; $i++) { ?>
            <tr> 
              <td width="50">&nbsp;</td>
              
            <td width="350"> 														  
              <input type="text" name="PersonId<?=$i?>" size="4" maxlength="8" value="<?=$_POST["PersonId" . $i]?>">
                <?=GetPersonNavn($_POST["PersonId" . $i])?>	
                <input type="hidden" name="PersonId<?=$i?>_old" size="4" maxlength="8" value="<?=$_POST["PersonId" . $i]?>">
              </td>
            </tr>
          <? } ?>
          <tr> 
            <td width="50">&nbsp;</td>
            <td width="350">&nbsp;</td>
          </tr>
          <tr> 
            <td width="50">&nbsp; </td>
            <td width="350" bgcolor="#FFFFFF"> 
              <input type="submit" name="Send" value="Opdater">
              <input type="submit" name="Fortryd" value="Fortryd" onClick="FocusParentAndClose()">
          </tr>
          <tr> 
            <td width="50">&nbsp;</td>
            <td width="350">&nbsp;</td>
          </tr>
        </table>
        </form>
      <? } } // End If $DoWhat 5 ?>

      <? if ($DoWhat == 6) { // Oprettet record gemt eller Vis record med fejl ?>
      <? if (!$Fejl) { // Oprettet record gemt ?>
         <b><font color="#003399">Kviterings tekst her</font></b>
         <a href="javascript:UpdateParentAndClose()"> Retur</a> 
      <? } else { // Vis record med fejl ?>
        <form name="Tilsagn" method="post" action="TProgramUpd.php?DoWhat=6&TPgmId=<?=$TPgmId?>">
        <table width="100%" border="0" bgcolor="#FFFFFF">
          <tr> 
            <td width="300" colspan="2"><b>
              <font color="#003399">Oprettelse af Træningsaktivitet</font></b>
            </td>
          </tr>
          <tr> 
            <td width="50">&nbsp;</td>
            <td width="350">&nbsp;</td>
          </tr>
          <tr> 
            <td width="50">Dato</td>
            <td width="350"> 
              
        <input type="text" name="TLinieDato" size="20" maxlength="25" value="<?=$_POST["TLinieDato"]?>">
              <font color="#FF0000"> 
              <?=$TLinieDatoFejl?>
              </font> 
              <input type="hidden" name="TLinieDato_old" value="<?=$_POST["TLinieDato"]?>">
            </td>
          </tr>
          <tr> 
            <td width="50" valign="top">Aktivitet</td>
            <td width="275"> 
              <textarea name="TLinieText" cols="25" rows="4"><?=$_POST["TLinieText"]?></textarea>
        <font color="#FF0000"> 
        <?=$TLinieTextFejl?>
        <input type="hidden" name="TLinieText_old" value="<?=$_POST["TLinieText"]?>">
        </font> </td>
          </tr>
          <tr> 
            <td width="50" valign="top">Intensitet</td>
            <td width="275"> 
              <textarea name="TLinieDosis" cols="25" rows="4"><?=$_POST["TLinieDosis"]?></textarea>
        <font color="#FF0000"> 
        <?=$TLinieDosisFejl?>
        <input type="hidden" name="TLinieDosis_old" value="<?=$_POST["TLinieDosis"]?>">
        </font> </td>
           </tr>
          <tr> 
            <td width="50">&nbsp;</td>
            <td width="350">&nbsp;</td>
          </tr>
          <tr> 
            <td width="50">&nbsp; </td>
            <td width="350" bgcolor="#FFFFFF"> 
              <input type="submit" name="Send" value="Opret">
              <input type="submit" name="Fortryd" value="Fortryd" onClick="FocusParentAndClose()">
          </tr>
          <tr> 
            <td width="50">&nbsp;</td>
            <td width="350">&nbsp;</td>
          </tr>
        </table>
        </form>
      <? } } // End If $DoWhat 6 ?>

</body>
</html>

<script language="javascript">
<!--
function UpdateParentAndClose(){
    var ParentWindow;
	ParentWindow = top.opener;
	ParentWindow.document.execCommand("Refresh");
	ParentWindow.focus();
	window.close();
}
function FocusParentAndClose(){
    var ParentWindow;
	ParentWindow = top.opener;
	ParentWindow.focus();
	window.close();
}
function fonLoad(){
	var h = 445;
	var w = 415;
	window.resizeTo(w, h+(<?=$line?>*27));
}
var newwindow;
function PListLink(id){
    var url;
	url = "/admin/PListe2.php?TopWin=<?=$TopWin?>&MaxRow=20&LinieId="+id;
	newwindow = window.open(url,"PListe","status, scrollbars, resizeable, width=475, heigth=100")
	newwindow.moveBy(300,0);
	newwindow.focus();
}
var newwindow2;
function PListGrpLink(){
    var url;
	url = "/admin/PListe5.php?TopWin=<?=$TopWin?>&MaxRow=20&tpgmid=<?=$TPgmId?>";
	newwindow2 = window.open(url,"PListe","status, scrollbars, resizeable, width=475, heigth=100")
	newwindow2.moveBy(300,0);
	newwindow2.focus();
}
// -->
</script>
