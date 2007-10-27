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

include_once("conn.inc");  
$dbLink = pg_connect($conn_www);


if (!isset($DoWhat)) 
  $DoWhat = 0;

$Fejl   = 0;
$onLoad = "fonLoad(); MM_preloadImages('/Opgaver/images/vpil_f2.gif');";

switch($DoWhat)
{
  case 2:  // Vis tom Record
    
    $StartTid       = "";
    $SlutTid        = "";
    $OpgaveNavn     = "";
    $OpgaveInstruks = "";
    $AntalPerson    = "";
    $PersonGruppeId = "";

    $line = 3;
    break;

  case 3:  // Vis Record
    
    $Query="SELECT * FROM tblOpgave WHERE Id=$Id";
    if ($dbResult = pg_query($dbLink, $Query))
      if ($row = pg_fetch_array($dbResult, NULL , PGSQL_ASSOC)) {
        $StartTid       = $row["StartTid"];
        $SlutTid        = $row["SlutTid"];
        $OpgaveNavn     = $row["OpgaveNavn"];
        $OpgaveInstruks = $row["OpgaveInstruks"];
        $AntalPerson    = $row["AntalPerson"];
        $PersonGruppeId = $row["PersonGruppeId"];
      }

    $line = 0;
    $Query="SELECT PersonId FROM tblPersonRef WHERE PersonGruppeId=$PersonGruppeId";
    if ($dbResult = pg_query($dbLink, $Query))
      while($row = pg_fetch_array($dbResult, NULL , PGSQL_ASSOC))
        $PersonId[$line++] = $row["PersonId"];
    $PersonId[$line++] = 0;
    break;
  
  case 5:  // Gem rettet Record
 	
    if (!$StartTid) {
      $StartTidFejl = "skal udfyldes";
      $Fejl = 1;
    }
 	
    if (!$SlutTid) {
      $SlutTidFejl = "skal udfyldes";
      $Fejl = 1;
    }

    if (!$OpgaveNavn) {
      $OpgaveNavnFejl = "skal udfyldes";
      $Fejl = 1;
    }

    //if (!$OpgaveInstruks) {
    //  $OpgaveInstruksFejl = "skal udfyldes";
    //  $Fejl = 1;
    //}

    //if (!$AntalPerson) {
    //  $AntalPersonFejl = "skal udfyldes";
    //  $Fejl = 1;
    //}

    //if (!$PersonGruppeId) {
    //  $PersonGruppeIdFejl = "skal udfyldes";
    //  $Fejl = 1;
    //}

    if (!$Fejl){
      $set ="SET ";
      $fset =0;
      if ($StartTid != $StartTid_old){
        if ($fset) $set .= ", ";
        $set .="StartTid = '$StartTid'";
        $fset =1;
      }
      if ($SlutTid != $SlutTid_old){
        if ($fset) $set .= ", ";
        $set .="SlutTid = '$SlutTid'";
        $fset =1;
      }
      if ($OpgaveNavn != $OpgaveNavn_old){
        if ($fset) $set .= ", ";
        $set .="OpgaveNavn = '$OpgaveNavn'";
        $fset =1;
      }
      if ($OpgaveInstruks != $OpgaveInstruks_old){
        if ($fset) $set .= ", ";
        $set .="OpgaveInstruks = '$OpgaveInstruks'";
        $fset =1;
      }
      if ($AntalPerson != $AntalPerson_old){
        if ($fset) $set .= ", ";
        $set .="AntalPerson = $AntalPerson";
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
        $PersonGruppeId = ClearPrivatPersonGruppe($PersonGruppeId_old);
        for($i=0; $i<$_POST["line"] ; $i++)
          InsertPersonIntoPersonGruppe($PersonGruppeId, $_POST["PersonId" . $i], 0);		
	  }

      if ($PersonGruppeId != $PersonGruppeId_old){
        if ($fset) $set .= ", ";
        $set .="PersonGruppeId = '$PersonGruppeId'";
        $fset =1;
      }

      if ($fset) {
        $Query=" UPDATE tblOpgave " . $set . " WHERE Id = $Id";
        $dbResult = pg_query($dbLink, $Query);
      }
      $onLoad = "UpdateParentAndClose()";
    }
    break;

  case 6:  // Gem ny Record
 	
    if (!$StartTid) {
      $StartTidFejl = "skal udfyldes";
      $Fejl = 1;
    }
 	
    if (!$SlutTid) {
      $SlutTidFejl = "skal udfyldes";
      $Fejl = 1;
    }

    if (!$OpgaveNavn) {
      $OpgaveNavnFejl = "skal udfyldes";
      $Fejl = 1;
    }

    //if (!$OpgaveInstruks) {
    //  $OpgaveInstruksFejl = "skal udfyldes";
    //  $Fejl = 1;
    //}

    //if (!$AntalPerson) {
    //  $AntalPersonFejl = "skal udfyldes";
    //  $Fejl = 1;
    //}

    //if (!$PersonGruppeId) {
    //  $PersonGruppeIdFejl = "skal udfyldes";
    //  $Fejl = 1;
    //}

    if (!$Fejl){
      $set ="SET ";
      $fset =0;
      if ($StartTid != $StartTid_old){
        if ($fset) $set .= ", ";
        $set .="StartTid = '$StartTid'";
        $fset =1;
      }
      if ($SlutTid != $SlutTid_old){
        if ($fset) $set .= ", ";
        $set .="SlutTid = '$SlutTid'";
        $fset =1;
      }
      if ($OpgaveNavn != $OpgaveNavn_old){
        if ($fset) $set .= ", ";
        $set .="OpgaveNavn = '$OpgaveNavn'";
        $fset =1;
      }
      if ($OpgaveInstruks != $OpgaveInstruks_old){
        if ($fset) $set .= ", ";
        $set .="OpgaveInstruks = '$OpgaveInstruks'";
        $fset =1;
      }
      if ($AntalPerson != $AntalPerson_old){
        if ($fset) $set .= ", ";
        $set .="AntalPerson = $AntalPerson";
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
        $PersonGruppeId = ClearPrivatPersonGruppe($PersonGruppeId_old);
        for($i=0; $i<$_POST["line"] ; $i++)
          InsertPersonIntoPersonGruppe($PersonGruppeId, $_POST["PersonId" . $i], 0);		
	  }

      if ($PersonGruppeId != $PersonGruppeId_old){
        if ($fset) $set .= ", ";
        $set .="PersonGruppeId = '$PersonGruppeId'";
        $fset =1;
      }

      if ($fset) {
        $Query=" INSERT INTO tblOpgave " . $set;
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
  <body bgcolor="#99CC00" text="#000000" onLoad="<?=$onLoad?>">
  <table width="100%" border="0" cellpadding="0" cellspacing="0">
    <tr bgcolor="#99CC00"> 
      <td width="20" height="6" bgcolor="#99CC00">&nbsp;</td>
      <td width="350" height="6">&nbsp;</td>
      <td height="6" bgcolor="#99CC00">&nbsp;</td>
    </tr>
    <tr>
      <td width="20" bgcolor="#99CC00">&nbsp;</td>
      <td width="350" bgcolor="#FFFFFF"> 
      
      <? if ($DoWhat == 2) { // Vis tom record ?>
        <form name="Tilsagn" method="post" action="OpgaverUpd.php?DoWhat=6&Id=<?=$Id?>">
        <table width="100%" border="0" bgcolor="#FFFFFF">
          <tr> 
            <td width="300" colspan="2"><b>
              <font color="#003399">Opret ny Opgave</font></b>
            </td>
          </tr>
          <tr> 
            <td width="50">&nbsp;</td>
            <td width="350">&nbsp;</td>
          </tr>
          <tr> 
            <td width="50">Start</td>
            <td width="350"> 
              <input type="text" name="StartTid" size="20" maxlength="25" value="<?=$StartTid?>">
              <input type="hidden" name="StartTid_old" value="<?=$StartTid?>">
            </td>
          </tr>
          <tr> 
            <td width="50">Slut</td>
            <td width="350"> 
              <input type="text" name="SlutTid" size="20" maxlength="25" value="<?=$SlutTid?>">
              <input type="hidden" name="SlutTid_old" value="<?=$SlutTid?>">
            </td>
          </tr>
          <tr> 
            <td width="50" valign="top">Opgave</td>
            <td width="275"> 
              <textarea name="OpgaveNavn" cols="25" rows="4"><?=$OpgaveNavn?></textarea>
              <input type="hidden" name="OpgaveNavn_old" value="<?=$OpgaveNavn?>">
            </td>
          </tr>
          <tr> 
            <td width="50" valign="top">Instruks</td>
            <td width="275"> 
              <textarea name="OpgaveInstruks" cols="25" rows="4"><?=$OpgaveInstruks?></textarea>
              <input type="hidden" name="OpgaveInstruks_old" value="<?=$OpgaveInstruks?>">
            </td>
          </tr>
          <tr> 
            <td width="50" valign="top">Antal</td>
            <td width="275"> 
              <input type="text" name="AntalPerson" size="4" maxlength="8" value="<?=$AntalPerson?>">
              <input type="hidden" name="AntalPerson_old" value="<?=$AntalPerson?>">
            </td>
          </tr>
          <tr> 
            <td width="50">Grp</td>
            <td width="350"> 
              <input type="text" name="PersonGruppeId" size="4" maxlength="8" value="<?=$PersonGruppeId?>">
              <?=GetPersonGruppeNavn($PersonGruppeId)?>	
              <input type="hidden" name="PersonGruppeId_old" value="<?=$PersonGruppeId?>">
              <input type="hidden" name="line" value="<?=$line?>">
            </td>
          </tr>
          <? for($i=0; $i<$line; $i++) { 
               $Buttom = "<a href=\"javascript:PListLink(" . $i . ")\" onMouseOut=\"MM_swapImgRestore();\" onMouseOver=\"MM_swapImage('vpil" . $i . "','','/Opgaver/images/vpil_f2.gif',1);\" ><img name=\"vpil" . $i . "\" src=\"/Opgaver/images/vpil.gif\" width=\"15\" height=\"15\" border=\"0\"></a>";
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
      <? } // End If $DoWhat 2 ?>
      
      <? if ($DoWhat == 3) { // Vis record ?>
        <form name="Tilsagn" method="post" action="OpgaverUpd.php?DoWhat=5&Id=<?=$Id?>">
        <table width="100%" border="0" bgcolor="#FFFFFF">
          <tr> 
            <td width="300" colspan="2"><b>
              <font color="#003399">Ændring af Opgave</font></b>
            </td>
          </tr>
          <tr> 
            <td width="50">&nbsp;</td>
            <td width="350">&nbsp;</td>
          </tr>
          <tr> 
            <td width="50">Start</td>
            <td width="350"> 
              <input type="text" name="StartTid" size="20" maxlength="25" value="<?=$StartTid?>">
              <input type="hidden" name="StartTid_old" value="<?=$StartTid?>">
            </td>
          </tr>
          <tr> 
            <td width="50">Slut</td>
            <td width="350"> 
              <input type="text" name="SlutTid" size="20" maxlength="25" value="<?=$SlutTid?>">
              <input type="hidden" name="SlutTid_old" value="<?=$SlutTid?>">
            </td>
          </tr>
          <tr> 
            <td width="50" valign="top">Opgave</td>
            <td width="275"> 
              <textarea name="OpgaveNavn" cols="25" rows="4"><?=$OpgaveNavn?></textarea>
              <input type="hidden" name="OpgaveNavn_old" value="<?=$OpgaveNavn?>">
            </td>
          </tr>
          <tr> 
            <td width="50" valign="top">Instruks</td>
            <td width="275"> 
              <textarea name="OpgaveInstruks" cols="25" rows="4"><?=$OpgaveInstruks?></textarea>
              <input type="hidden" name="OpgaveInstruks_old" value="<?=$OpgaveInstruks?>">
            </td>
          </tr>
          <tr> 
            <td width="50" valign="top">Antal</td>
            <td width="275"> 
              <input type="text" name="AntalPerson" size="4" maxlength="8" value="<?=$AntalPerson?>">
              <input type="hidden" name="AntalPerson_old" value="<?=$AntalPerson?>">
            </td>
          </tr>
          <tr> 
            <td width="50">Grp</td>
            <td width="350"> 
              <input type="text" name="PersonGruppeId" size="4" maxlength="8" value="<?=$PersonGruppeId?>">
              <?=GetPersonGruppeNavn($PersonGruppeId)?>	
              <input type="hidden" name="PersonGruppeId_old" value="<?=$PersonGruppeId?>">
              <input type="hidden" name="line" value="<?=$line?>">
            </td>
          </tr>
          <? for($i=0; $i<$line; $i++) { 
               $Buttom = "<a href=\"javascript:PListLink(" . $i . ")\" onMouseOut=\"MM_swapImgRestore();\" onMouseOver=\"MM_swapImage('vpil" . $i . "','','/Opgaver/images/vpil_f2.gif',1);\" ><img name=\"vpil" . $i . "\" src=\"/Opgaver/images/vpil.gif\" width=\"15\" height=\"15\" border=\"0\"></a>";
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
        <form name="Tilsagn" method="post" action="OpgaverUpd.php?DoWhat=5&Id=<?=$Id?>">
        <table width="100%" border="0" bgcolor="#FFFFFF">
          <tr> 
            <td width="300" colspan="2"><b>
              <font color="#003399">Ændring af Opgave</font></b>
            </td>
          </tr>
          <tr> 
            <td width="50">&nbsp;</td>
            <td width="350">&nbsp;</td>
          </tr>
          <tr> 
            <td width="50">Start</td>
            <td width="350"> 
              <input type="text" name="StartTid" size="20" maxlength="25" value="<?=$_POST["StartTid"]?>">
              <font color="#FF0000"> 
              <?=$StartTidFejl?>
              </font> 
              <input type="hidden" name="StartTid_old" value="<?=$_POST["StartTid"]?>">
            </td>
          </tr>
          <tr> 
            <td width="50">Slut</td>
            <td width="350"> 
              <input type="text" name="SlutTid" size="20" maxlength="25" value="<?=$_POST["SlutTid"]?>">
              <font color="#FF0000"> 
              <?=$SlutTidFejl?>
              </font> 
              <input type="hidden" name="SlutTid_old" value="<?=$_POST["SlutTid"]?>">
            </td>
          </tr>
          <tr> 
            <td width="50" valign="top">Opgave</td>
            <td width="275"> 
              <textarea name="OpgaveNavn" cols="25" rows="4"><?=$_POST["OpgaveNavn"]?></textarea>
            </td>
            <td width="75" valign="top"> 
              <font color="#FF0000"> 
                <?=$OpgaveNavnFejl?>
              </font> 
              <input type="hidden" name="OpgaveNavn_old" value="<?=$_POST["OpgaveNavn"]?>">
            </td>
          </tr>
          <tr> 
            <td width="50" valign="top">Instruks</td>
            <td width="275"> 
              <textarea name="OpgaveInstruks" cols="25" rows="4"><?=$_POST["OpgaveInstruks"]?></textarea>
            </td>
            <td width="75" valign="top"> 
              <font color="#FF0000"> 
                <?=$OpgaveInstruksFejl?>
              </font> 
              <input type="hidden" name="OpgaveInstruks_old" value="<?=$_POST["OpgaveInstruks"]?>">
            </td>
          </tr>
          <tr> 
            <td width="50">Antal</td>
            <td width="350"> 
              <input type="text" name="AntalPerson" size="4" maxlength="4" value="<?=$_POST["AntalPerson"]?>">
              <font color="#FF0000"> 
                <?=$AntalPersonFejl?>
              </font> 
              <input type="hidden" name="AntalPerson_old" value="<?=$_POST["AntalPerson"]?>">
            </td>
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


      <? if ($DoWhat == 6) { // Rettet record gemt eller Vis record med fejl ?>
      <? if (!$Fejl) { // Ny record gemt ?>
         <b><font color="#003399">Kviterings tekst her</font></b>
         <a href="javascript:UpdateParentAndClose()"> Retur</a> 
      <? } else { // Vis ny record med fejl ?>
        <form name="Tilsagn" method="post" action="OpgaverUpd.php?DoWhat=6&Id=<?=$Id?>">
        <table width="100%" border="0" bgcolor="#FFFFFF">
          <tr> 
            <td width="300" colspan="2"><b>
              <font color="#003399">Porte ny Opgave</font></b>
            </td>
          </tr>
          <tr> 
            <td width="50">&nbsp;</td>
            <td width="350">&nbsp;</td>
          </tr>
          <tr> 
            <td width="50">Start</td>
            <td width="350"> 
              <input type="text" name="StartTid" size="20" maxlength="25" value="<?=$_POST["StartTid"]?>">
              <font color="#FF0000"> 
              <?=$StartTidFejl?>
              </font> 
              <input type="hidden" name="StartTid_old" value="<?=$_POST["StartTid"]?>">
            </td>
          </tr>
          <tr> 
            <td width="50">Slut</td>
            <td width="350"> 
              <input type="text" name="SlutTid" size="20" maxlength="25" value="<?=$_POST["SlutTid"]?>">
              <font color="#FF0000"> 
              <?=$SlutTidFejl?>
              </font> 
              <input type="hidden" name="SlutTid_old" value="<?=$_POST["SlutTid"]?>">
            </td>
          </tr>
          <tr> 
            <td width="50" valign="top">Opgave</td>
            <td width="275"> 
              <textarea name="OpgaveNavn" cols="25" rows="4"><?=$_POST["OpgaveNavn"]?></textarea>
            </td>
            <td width="75" valign="top"> 
              <font color="#FF0000"> 
                <?=$OpgaveNavnFejl?>
              </font> 
              <input type="hidden" name="OpgaveNavn_old" value="<?=$_POST["OpgaveNavn"]?>">
            </td>
          </tr>
          <tr> 
            <td width="50" valign="top">Instruks</td>
            <td width="275"> 
              <textarea name="OpgaveInstruks" cols="25" rows="4"><?=$_POST["OpgaveInstruks"]?></textarea>
            </td>
            <td width="75" valign="top"> 
              <font color="#FF0000"> 
                <?=$OpgaveInstruksFejl?>
              </font> 
              <input type="hidden" name="OpgaveInstruks_old" value="<?=$_POST["OpgaveInstruks"]?>">
            </td>
          </tr>
          <tr> 
            <td width="50">Antal</td>
            <td width="350"> 
              <input type="text" name="AntalPerson" size="4" maxlength="4" value="<?=$_POST["AntalPerson"]?>">
              <font color="#FF0000"> 
                <?=$AntalPersonFejl?>
              </font> 
              <input type="hidden" name="AntalPerson_old" value="<?=$_POST["AntalPerson"]?>">
            </td>
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
      <? } } // End If $DoWhat 6 ?>

      </td>
      <td bgcolor="#99CC00">&nbsp;</td>
    </tr>
    <tr bgcolor="#99CC00"> 
      <td width="20" height="20" bgcolor="#99CC00">&nbsp;</td>
      <td width="350" height="20">&nbsp;</td>
      <td height="20" bgcolor="#99CC00">&nbsp;</td>
    </tr>
  </table>
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
	var h = 520;
	var w = 415;
	window.resizeTo(w, h+(<?=$line?>*27));
}
var newwindow;
function PListLink(id){
	newwindow = window.open("/Opgaver/PListe.php?LinieId="+id,"PListe","status, scrollbars, resizeable, width=475, heigth=100")
	newwindow.moveBy(300,0);
	newwindow.focus();
}
// -->
</script>
