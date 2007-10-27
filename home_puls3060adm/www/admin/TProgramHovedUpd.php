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

if (isset($_REQUEST['DoWhat'])) 
  $DoWhat=$_REQUEST['DoWhat'];
else
  $DoWhat = 0;

if (isset($_REQUEST['TPgmID']))
  $TPgmID=$_REQUEST['TPgmID'];

if (isset($_REQUEST['TPgmNavn'])) 
  $TPgmNavn=$_REQUEST['TPgmNavn'];

if (isset($_REQUEST['StartDato'])) 
  $StartDato=$_REQUEST['StartDato'];

if (isset($_REQUEST['SlutDato'])) 
  $SlutDato=$_REQUEST['SlutDato'];

if (isset($_REQUEST['Link'])) 
  $Link=$_REQUEST['Link'];

if (isset($_REQUEST['TPgmNavn_old']))
  $TPgmNavn_old=$_REQUEST['TPgmNavn_old'];

if (isset($_REQUEST['StartDato_old']))
  $StartDato_old=$_REQUEST['StartDato_old'];

if (isset($_REQUEST['SlutDato_old']))
  $SlutDato_old=$_REQUEST['SlutDato_old'];

if (isset($_REQUEST['Link_old']))
  $Link_old=$_REQUEST['Link_old'];

include_once("conn.inc");  
$dbLink = pg_connect($conn_www);


$Fejl   = 0;

$onLoad = "fonLoad(); MM_preloadImages('/images/vpil.gif');MM_preloadImages('/images/vpil_f2.gif');";

switch($DoWhat)
{
  case 2:  // Vis Tom Record
    break;

  case 3:  // Vis Record
    
    $Query="SELECT * FROM tbltpgm WHERE TPgmID=$TPgmID";
    if ($dbResult = pg_query($dbLink, $Query))
      if ($row = pg_fetch_array($dbResult, NULL , PGSQL_ASSOC)) {
        $TPgmNavn     = $row["tpgmnavn"];
        $StartDato     = $row["startdato"];
        $SlutDato    = $row["slutdato"];
        $Link = $row["link"];
      }
    break;
  
  case 5:  // Gem rettet Record
 	
    if (!$TPgmNavn) {
      $TPgmNavnFejl = "skal udfyldes";
      $Fejl = 1;
    }

    if (!$StartDato) {
      $StartDatoFejl = "skal udfyldes";
      $Fejl = 1;
    }

    if (!$SlutDato) {
      $SlutDatoFejl = "skal udfyldes";
      $Fejl = 1;
    }

    //if (!$Link) {
    //  $LinkFejl = "skal udfyldes";
    //  $Fejl = 1;
    //}

    if (!$Fejl){
      $set ="SET ";
      $fset =0;
      if ($TPgmNavn != $TPgmNavn_old){
        if ($fset) $set .= ", ";
        $set .="TPgmNavn = '$TPgmNavn'";
        $fset =1;
      }
      if ($StartDato != $StartDato_old){
        if ($fset) $set .= ", ";
        $set .="StartDato = '$StartDato'";
        $fset =1;
      }
      if ($SlutDato != $SlutDato_old){
        if ($fset) $set .= ", ";
        $set .="SlutDato = '$SlutDato'";
        $fset =1;
      }
      
      if ($Link != $Link_old){
        if ($fset) $set .= ", ";
        $set .="Link = '$Link'";
        $fset =1;
      }

      if ($fset) {
        $Query=" UPDATE tbltpgm " . $set . " WHERE TPgmID = $TPgmID";
        $dbResult = pg_query($dbLink, $Query);
      }
      
      $onLoad = "UpdateParentAndClose()";
    }
    break;

  case 6:  // Opret Record
 	
    if (!$TPgmNavn) {
      $TPgmNavnFejl = "skal udfyldes";
      $Fejl = 1;
    }

    if (!$StartDato) {
      $StartDatoFejl = "skal udfyldes";
      $Fejl = 1;
    }

    if (!$SlutDato) {
      $SlutDatoFejl = "skal udfyldes";
      $Fejl = 1;
    }

    //if (!$Link) {
    //  $LinkFejl = "skal udfyldes";
    //  $Fejl = 1;
    //}

    if (!$Fejl){
      $insert =" (";
      $value =" VALUES (";
      $fset =0;
      
      if ($fset) $insert .= ", ";
      if ($fset) $value .= ", ";
      $insert .="TPgmNavn";
      $value .="'$TPgmNavn'";
      $fset =1;

      if ($fset) $insert .= ", ";
      if ($fset) $value .= ", ";
      $insert .="StartDato";
      $value .="'$StartDato'";
      $fset =1;

      if ($fset) $insert .= ", ";
      if ($fset) $value .= ", ";
      $insert .="SlutDato";
      $value .="'$SlutDato'";
      $fset =1;
      
      if ($Link != $Link_old){
        if ($fset) $insert .= ", ";
        if ($fset) $value .= ", ";
        $insert .="Link";
        $value .="'$Link'";
        $fset =1;
      }

      if ($fset) {
        $insert .=") ";
        $value .=")";
        $Query=" INSERT INTO tbltpgm " . $insert . $value;
        $dbResult = pg_query($dbLink, $Query);
      }
      
      $onLoad = "UpdateParentAndClose()";
    }
    break;

  default:  // ingen case valgt

} // End Switch($DoWhat) 
?>

<head>
<title>TProgramHovedUpd</title>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
</head>
  <body bgcolor="#FFFFFF" text="#000000" onLoad="<?=$onLoad?>">

      <? if ($DoWhat == 2) { // Vis tom record ?>
        <form name="Tilsagn" method="post" action="TProgramHovedUpd.php?DoWhat=6">
        <table width="100%" border="0" bgcolor="#FFFFFF">
          <tr> 
            
      <td width="300" colspan="2"><b> <font color="#003399">Oprettelse af Trænings 
        Program</font></b> </td>
          </tr>
          <tr> 
            <td width="50">&nbsp;</td>
            <td width="350">&nbsp;</td>
          </tr>
          <tr> 
            
      <td width="50" valign="top">Navn</td>
            <td width="275"> 
              
        <input type="text" name="TPgmNavn" size="50" value="<?=$TPgmNavn?>" maxlength="50">
              <input type="hidden" name="TPgmNavn_old" value="<?=$TPgmNavn?>">
            </td>
          </tr>
             <tr> 
            
      <td width="50">Startdato</td>
            
      <td width="350"> 
        <input type="text" name="StartDato" size="25" value="<?=$StartDato?>">
        <input type="hidden" name="StartDato_old" value="<?=$StartDato?>">
            </td>
          </tr>

          <tr> 
            
      <td width="50" valign="top">Slutdato</td>
            <td width="275"> 
              
        <input type="text" name="SlutDato" size="25" value="<?=$SlutDato?>">
              <input type="hidden" name="SlutDato_old" value="<?=$SlutDato?>">
            </td>
          </tr>
          <tr> 
            
      <td width="50">Link</td>
            <td width="350"> 
              
        <input type="text" name="Link" size="50" maxlength="150" value="<?=$Link?>">
              <input type="hidden" name="Link_old" value="<?=$Link?>">
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
        <form name="Tilsagn" method="post" action="TProgramHovedUpd.php?DoWhat=5&TPgmID=<?=$TPgmID?>">
        <table width="100%" border="0" bgcolor="#FFFFFF">
          <tr> 
            
      <td width="300" colspan="2"><b> <font color="#003399">Ændring af </font></b><b><font color="#003399">Program</font></b></td>
          </tr>
          <tr> 
            <td width="50">&nbsp;</td>
            <td width="350">&nbsp;</td>
          </tr>
          <tr> 
            
      <td width="50" valign="top">Navn</td>
            <td width="275"> 
              
        <input type="text" name="TPgmNavn" size="50" value="<?=$TPgmNavn?>" maxlength="50">
              <input type="hidden" name="TPgmNavn_old" value="<?=$TPgmNavn?>">
            </td>
          </tr>
          <tr> 
            
      <td width="50">Startdato</td>
            <td width="350"> 
              
        <input type="text" name="StartDato" size="25" value="<?=$StartDato?>">
              <input type="hidden" name="StartDato_old" value="<?=$StartDato?>">
            </td>
          </tr>

          <tr> 
            
      <td width="50" valign="top">Slutdato</td>
            <td width="275"> 
              
        <input type="text" name="SlutDato" size="25" value="<?=$SlutDato?>">
              <input type="hidden" name="SlutDato_old" value="<?=$SlutDato?>">
            </td>
          </tr>
          <tr> 
            
      <td width="50">Link</td>
            <td width="350"> 
              
        <input type="text" name="Link" size="50" maxlength="150" value="<?=$Link?>">
              <input type="hidden" name="Link_old" value="<?=$Link?>">
            </td>
          </tr>
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
        <form name="Tilsagn" method="post" action="TProgramHovedUpd.php?DoWhat=5&TPgmID=<?=$TPgmID?>">
        <table width="100%" border="0" bgcolor="#FFFFFF">
          <tr> 
            
      <td width="300" colspan="2"><b> <font color="#003399">Ændring af </font></b><b><font color="#003399">Program</font></b></td>
          </tr>
          <tr> 
            <td width="50">&nbsp;</td>
            <td width="350">&nbsp;</td>
          </tr>
          <tr> 
            
      <td width="50" valign="top">Navn</td>
            <td width="275"> 
              
        <input type="text" name="TPgmNavn" size="50" value="<?=$_POST["TPgmNavn"]?>" maxlength="50">
        <font color="#FF0000"> 
        <?=$TPgmNavnFejl?>
        <input type="hidden" name="TPgmNavn_old3" value="<?=$_POST["TPgmNavn"]?>">
        </font> </td>
          </tr>
                    <tr> 
            
      <td width="50">Startdato</td>
            
      <td width="350"> 
        <input type="text" name="StartDato3" size="25" value="<?=$_POST["StartDato"]?>">
        <font color="#FF0000"> 
        <?=$StartDatoFejl?>
        </font> 
        <input type="hidden" name="StartDato_old" value="<?=$_POST["StartDato"]?>">
            </td>
          </tr>

		  <tr> 
            
      <td width="50" valign="top">Slutdato</td>
            <td width="275"> 
              
        <input type="text" name="SlutDato" size="25" value="<?=$_POST["SlutDato"]?>">
        <font color="#FF0000"> 
        <?=$SlutDatoFejl?>
        <input type="hidden" name="SlutDato_old3" value="<?=$_POST["SlutDato"]?>">
        </font> </td>
         </tr>
          <tr> 
            
      <td width="50">Link</td>
            <td width="350"> 
              
        <input type="text" name="Link" size="50" maxlength="150" value="<?=$_POST["Link"]?>">
              <?=GetPersonGruppeNavn($_POST["Link"])?>	
              <font color="#FF0000"> 
                <?=$LinkFejl?>
              </font> 
              <input type="hidden" name="Link_old" value="<?=$_POST["Link"]?>">
            </td>
          </tr>
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
        <form name="Tilsagn" method="post" action="TProgramHovedUpd.php?DoWhat=6">
        <table width="100%" border="0" bgcolor="#FFFFFF">
          <tr> 
            
      <td width="300" colspan="2"><b> <font color="#003399">Oprettelse af </font></b><b><font color="#003399">Program</font></b></td>
          </tr>
          <tr> 
            <td width="50">&nbsp;</td>
            <td width="350">&nbsp;</td>
          </tr>
          <tr> 
            
      <td width="50" valign="top">Navn</td>
            <td width="275"> 
              
        <input type="text" name="TPgmNavn" size="50" value="<?=$_POST["TPgmNavn"]?>" maxlength="50">
        <font color="#FF0000"> 
        <?=$TPgmNavnFejl?>
        <input type="hidden" name="TPgmNavn_old2" value="<?=$_POST["TPgmNavn"]?>">
        </font> </td>
          </tr>
                    <tr> 
            
      <td width="50">Startdato</td>
            <td width="350"> 
              
        <input type="text" name="StartDato" size="25" value="<?=$_POST["StartDato"]?>">
              <font color="#FF0000"> 
              <?=$StartDatoFejl?>
              </font> 
              <input type="hidden" name="StartDato_old" value="<?=$_POST["StartDato"]?>">
            </td>
          </tr>

		  <tr> 
            
      <td width="50" valign="top">Slutdato</td>
            <td width="275"> 
              
        <input type="text" name="SlutDato" size="25" value="<?=$_POST["SlutDato"]?>">
        <font color="#FF0000"> 
        <?=$SlutDatoFejl?>
        <input type="hidden" name="SlutDato_old2" value="<?=$_POST["SlutDato"]?>">
        </font> </td>
          </tr>
          <tr> 
            
      <td width="50">Link</td>
            <td width="350"> 
              
        <input type="text" name="Link" size="50" maxlength="150" value="<?=$_POST["Link"]?>">
              <?=GetPersonGruppeNavn($_POST["Link"])?>	
              <font color="#FF0000"> 
                <?=$LinkFejl?>
              </font> 
              <input type="hidden" name="Link_old" value="<?=$_POST["Link"]?>">
            </td>
          </tr>
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
	window.resizeTo(w, h);
}
// -->
</script>
