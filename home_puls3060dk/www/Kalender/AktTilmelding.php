<? header ("Last-Modified: " . gmdate("D, d M Y H:i:s") . " GMT"); // always modified ?>
<html>
<!-- #BeginTemplate "/Templates/Puls3060_page.dwt" -->
<head>
<!-- #BeginEditable "doctitle" --> 
<script language="javascript"    
  src="../Scripts/WebUIValidation.js">
</script>

<?
setlocale (LC_TIME, "da_DK.ISO8859-1");
include_once("mailFunctions.inc");  

function DatoFormat($Sender)
{
	return(strtolower(strftime("%A den %e. %B %Y kl %H.%M", strtotime($Sender))));
}
if (isset($_REQUEST['AktId'])) 
  $AktId=$_REQUEST['AktId'];
else
  $AktId=1;

if (isset($_REQUEST['Fornavn'])) 
  $Fornavn=$_REQUEST['Fornavn'];
else
  $Fornavn="";

if (isset($_REQUEST['Efternavn'])) 
  $Efternavn=$_REQUEST['Efternavn'];
else
  $Efternavn="";

if (isset($_REQUEST['Adresse'])) 
  $Adresse=$_REQUEST['Adresse'];
else
  $Adresse="";

if (isset($_REQUEST['Postnr'])) 
  $Postnr=$_REQUEST['Postnr'];
else
  $Postnr="";

if (isset($_REQUEST['Bynavn'])) 
  $Bynavn=$_REQUEST['Bynavn'];
else
  $Bynavn="";

if (isset($_REQUEST['Telefonnr'])) 
  $Telefonnr=$_REQUEST['Telefonnr'];
else
  $Telefonnr="";

if (isset($_REQUEST['Email'])) 
  $Email=$_REQUEST['Email'];
else
  $Email="";

if (isset($_REQUEST['DoWhat'])) 
  $DoWhat=$_REQUEST['DoWhat'];
else
  $DoWhat=1;

if ($DoWhat != 2)
    $DoWhat = 1; 

if ($DoWhat == 2){
  if (MailVal($Email, 3))
    $DoWhat = 3; 
}

$bTilmeldingSlut = 1;

include_once("conn.inc");  
$dbLink = pg_connect($conn_www);



if ($DoWhat == 1){
    if (isset($_REQUEST['p0'])) 
      $p0=$_REQUEST['p0'];
    else
      $p0="0";
    $DId        = 0;			    
    $DFornavn   = "";   
    $DEfternavn = ""; 
    $DAdresse   = ""; 
    $DPostnr    = ""; 
    $DBynavn    = ""; 
    $DTelefonnr = ""; 
    $DEmail     = ""; 

    $Query="SELECT P3060_ref, Akt_ref FROM tblLink WHERE LinkId='" .$p0. "'";
    if ($dbResult = pg_query($dbLink, $Query))
      if($row = pg_fetch_array($dbResult, NULL , PGSQL_ASSOC)){
        $DId 	= $row["p3060_ref"];
        $AktId  = $row["akt_ref"];
      }

    $Query="SELECT * FROM tblPerson WHERE Id=$DId";
    if ($dbResult = pg_query($dbLink, $Query))
      if ($row = pg_fetch_array($dbResult, NULL , PGSQL_ASSOC)) {
        $DFornavn   = $row["fornavn"];
        $DEfternavn = $row["efternavn"];
        $DAdresse   = $row["adresse"];
        $DPostnr    = $row["postnr"];
        $DBynavn    = $row["bynavn"];

        $Query="SELECT * FROM tblTelefon WHERE PersonId=$DId ORDER BY Id";
        if ($dbResult = pg_query($dbLink, $Query))
          if($row = pg_fetch_array($dbResult, NULL , PGSQL_ASSOC)){
            $DTelefonnr = $row["tlfnr"];
    	  }

        $Query="SELECT * FROM tblMailadresse WHERE PersonId=$DId ORDER BY Id";
        if ($dbResult = pg_query($dbLink, $Query))
          if($row = pg_fetch_array($dbResult, NULL , PGSQL_ASSOC)){
            $DEmail = $row["mailadr"];
    	  }
      }
}

$Query="SELECT KLinieText, KLinieDato, tilmeldingslut, klinieoverskrift, (now()- interval '1 day')::date AS nu
		FROM   tblKLinie
		WHERE KLinieID = $AktId";
$dbResult = pg_query($dbLink, $Query);
$row = pg_fetch_array($dbResult, NULL , PGSQL_ASSOC);
$Navn  = $row["klinieoverskrift"];
$Dato  = $row["kliniedato"];
$tilmeldingslut = $row["tilmeldingslut"];
$nu = $row["nu"];
if ($tilmeldingslut > $nu){ 
  $bTilmeldingSlut = 0;
}

if ($DoWhat == 2){
  // Opret tblAktTilmelding
  $set1 ="(";
  $set2 ="VALUES(";
  $fset =0;
  if ($AktId){
    if ($fset){
      $set1 .= ", ";
      $set2 .= ", ";
    }
    $set1 .="AktId";
    $set2 .="$AktId";
    $fset =1;
  }

  if ($Fornavn){
    if ($fset){
      $set1 .= ", ";
      $set2 .= ", ";
    }
    $set1 .="Fornavn";
    $set2 .="'$Fornavn'";
    $fset =1;
  }

  if ($Efternavn){
    if ($fset){
      $set1 .= ", ";
      $set2 .= ", ";
    }
    $set1 .="Efternavn";
    $set2 .="'$Efternavn'";
    $fset =1;
  }

  if ($Adresse){
    if ($fset){
      $set1 .= ", ";
      $set2 .= ", ";
    }
    $set1 .="Adresse";
    $set2 .="'$Adresse'";
    $fset =1;
  }
  
  if ($Postnr){
    if ($fset){
      $set1 .= ", ";
      $set2 .= ", ";
    }
    $set1 .="Postnr";
    $set2 .="'$Postnr'";
    $fset =1;
  }

  if ($Bynavn){
    if ($fset){
      $set1 .= ", ";
      $set2 .= ", ";
    }
    $set1 .="ByNavn";
    $set2 .="'$Bynavn'";
    $fset =1;
  }

  if ($Telefonnr){
    if ($fset){
      $set1 .= ", ";
      $set2 .= ", ";
    }
    $set1 .="TlfNr";
    $set2 .="'$Telefonnr'";
    $fset =1;
  }

  if ($Email){
    if ($fset){
      $set1 .= ", ";
      $set2 .= ", ";
    }
    $set1 .="MailAdr";
    $set2 .="'$Email'";
    $fset =1;
  }

  if ($fset) {
    $set1 .=", IP";
    $set2 .=", '" . $_SERVER['REMOTE_ADDR'] . "'";
    $Query=" INSERT INTO tblAktTilmelding " . $set1 . ") " . $set2 . ");";
    $dbResult = pg_query($dbLink, $Query);
  }

  $Subj = "Tilmelding til $Navn";
  $Body  = 'Denne e-mail er genereret af siden "Aktivitets tilmelding" på www.puls3060.dk';
  $Body .= "\n  Aktivitet...: " . $Navn;
  $Body .= "\n  Dato........: " . DatoFormat($Dato);
  $Body .= "\n  Fornavn.....: " . $Fornavn;
  $Body .= "\n  Efternavn...: " . $Efternavn;
  $Body .= "\n  Adresse.....: " . $Adresse;
  $Body .= "\n  Postnummer..: " . $Postnr;
  $Body .= "\n  By..........: " . $Bynavn;
  $Body .= "\n  Telefon.....: " . $Telefonnr;
  $Body .= "\n  E-mail......: " . $Email;
  mail("mha@hafsjold.dk", $Subj, $Body, "From: " . $Email . "\nReply-To: " . $Email . "\nX-Mailer: PHP", "-f mha@puls3060.dk");

  $Subj = "Tilmelding til $Navn";
  $Body  = "Hej $Fornavn,";
  $Body .= "\n";
  $Body .= "\nTak for din tilmelding til " . $Navn . " " . DatoFormat($Dato) . ".";
  $Body .= "\nVi har registreret følgende oplysninger i forbindelse med din tilmelding:";
  $Body .= "\n";
  $Body .= "\n  Aktivitet...: " . $Navn;
  $Body .= "\n  Dato........: " . DatoFormat($Dato);
  $Body .= "\n  Fornavn.....: " . $Fornavn;
  $Body .= "\n  Efternavn...: " . $Efternavn;
  $Body .= "\n  Adresse.....: " . $Adresse;
  $Body .= "\n  Postnummer..: " . $Postnr;
  $Body .= "\n  By..........: " . $Bynavn;
  $Body .= "\n  Telefon.....: " . $Telefonnr;
  $Body .= "\n  E-mail......: " . $Email;
  $Body .= "\n";
  $Body .= "\nMed venlig hilsen";
  $Body .= "\nPuls 3060";
  $Body .= "\n";
  $Body .= "\nBesøg vores hjemmeside www.puls3060.dk ";
  mail($Email, $Subj, $Body, "From: mha@hafsjold.dk\nReply-To: mha@hafsjold.dk\nX-Mailer: PHP", "-f mha@puls3060.dk");
}
?>

<title>Aktivitets Tilmelding</title>
<!-- #EndEditable -->
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
</head>

<body bgcolor="#FFFFFF" text="#000000">

  <table width="100%" border="0" cellspacing="0" cellpadding="0" dwcopytype="CopyTableRow">
    <tr> 
      <td width="30">&nbsp;</td>
      <td>&nbsp;</td>
      <td width="30">&nbsp;</td>
    </tr>
    <tr> 
      <td width="30">&nbsp;</td>
      
    <td valign="top"> <!-- #BeginEditable "docbody" --> 
      <? if ($DoWhat == 1) { // Udfyld formular og send ?>
      <p align=left><font color=#003399 size=4>Tilmelding til <?=$Navn;?></font></p>
      <p> Du kan tilmelde dig <?=$Navn;?> <?=DatoFormat($Dato);?> ved at udfylde formularen 
        p&aring; denne side og trykke p&aring; knappen &quot;Tilmelding&quot;.  </p>
      <form method="post" action="AktTilmelding.php?AktId=<?=$AktId?>&DoWhat=2" language="javascript" onsubmit="ValidatorOnSubmit();">
        <table width="100%" border="0">
          <tr> 
            <td>Fornavn:</td>
            <td> 
              <input name="Fornavn" type="text" maxlength="25" id="Fornavn" size="25" value="<?=$DFornavn?>"/>
              <span id="Fornavnval" controltovalidate="Fornavn" errormessage="Fornavn skal udfyldes" display="Dynamic" evaluationfunction="RequiredFieldValidatorEvaluateIsValid" initialvalue="" style="color:Red;display:none;"> 
              skal udfyldes </span> </td>
            <td>&nbsp;</td>
          </tr>
          <tr> 
            <td>Efternavn:</td>
            <td> 
              <input name="Efternavn" type="text" maxlength="25" id="Efternavn" size="25" value="<?=$DEfternavn?>"/>
              <span id="Efternavnval" controltovalidate="Efternavn" errormessage="Efternavn skal udfyldes" display="Dynamic" evaluationfunction="RequiredFieldValidatorEvaluateIsValid" initialvalue="" style="color:Red;display:none;"> 
              skal udfyldes </span> </td>
            <td>&nbsp;</td>
          </tr>
          <tr> 
            <td>Adresse:</td>
            <td> 
              <input name="Adresse" type="text" maxlength="35" id="Adresse" size="35" value="<?=$DAdresse?>"/>
              <span id="Adresseval" controltovalidate="Adresse" errormessage="Adresse skal udfyldes" display="Dynamic" evaluationfunction="RequiredFieldValidatorEvaluateIsValid" initialvalue="" style="color:Red;display:none;"> 
              skal udfyldes </span> </td>
            <td>&nbsp;</td>
          </tr>
          <tr> 
            <td>Postnummer:</td>
            <td> 
              <input name="Postnr" type="text" maxlength="4" id="Postnr" size="4" value="<?=$DPostnr?>"/>
              <span id="Postnrval1" controltovalidate="Postnr" errormessage="Postnr skal udfyldes" display="Dynamic" evaluationfunction="RequiredFieldValidatorEvaluateIsValid" initialvalue="" style="color:Red;display:none;"> 
              skal udfyldes </span> <span id="Postnrval2" controltovalidate="Postnr" errormessage="Postnr skal være mellem 1000 og 9900" display="Dynamic" evaluationfunction="RegularExpressionValidatorEvaluateIsValid" validationexpression="[1-9]{1}[0-9]{3}" style="color:Red;display:none;"> 
              skal være mellem 1000 og 9900 </span> </td>
            <td>&nbsp;</td>
          </tr>
          <tr> 
            <td>By:</td>
            <td> 
              <input name="Bynavn" type="text" maxlength="25" id="Bynavn" size="25"  value="<?=$DBynavn?>"/>
              <span id="Bynavnval" controltovalidate="Bynavn" errormessage="By skal udfyldes" display="Dynamic" evaluationfunction="RequiredFieldValidatorEvaluateIsValid" initialvalue="" style="color:Red;display:none;"> 
              skal udfyldes </span> </td>
            <td>&nbsp;</td>
          </tr>
          <tr> 
            <td>Telefon</td>
            <td> 
              <input name="Telefonnr" type="text" maxlength="8" id="Telefonnr" size="8"  value="<?=$DTelefonnr?>"/>
              <span id="Telefonnrval1" controltovalidate="Telefonnr" errormessage="Telefonnr skal udfyldes" display="Dynamic" evaluationfunction="RequiredFieldValidatorEvaluateIsValid" initialvalue="" style="color:Red;display:none;"> 
              skal udfyldes </span> <span id="Telefonnrval" controltovalidate="Telefonnr" errormessage="Telefonnr skal være mellem 10000000 og 99999999" display="Dynamic" evaluationfunction="RegularExpressionValidatorEvaluateIsValid" validationexpression="[1-9]{1}[0-9]{7}" style="color:Red;display:none;"> 
              skal være mellem 10000000 og 99999999 </span> </td>
            <td>&nbsp;</td>
          </tr>
          <tr> 
            <td>E-mail:</td>
            <td> 
              <input name="Email" type="text" id="Email" size="30"  value="<?=$DEmail?>"/>
              <span id="Emailval1" controltovalidate="Email" errormessage="Email skal udfyldes" display="Dynamic" evaluationfunction="RequiredFieldValidatorEvaluateIsValid" initialvalue="" style="color:Red;display:none;"> 
              skal udfyldes </span> <span id="Emailval" controltovalidate="Email" errormessage="Email skal udfyldes korrekt" display="Dynamic" evaluationfunction="RegularExpressionValidatorEvaluateIsValid" validationexpression="[a-zA-Z_0-9\.\-]{1,40}@([a-zA-Z0-9\-]{1,40}\.){1,5}[a-zA-Z]{2,3}" style="color:Red;display:none;"> 
              skal udfyldes korrekt </span> </td>
            <td> <font size="3">(navn@abc.dk)</font></td>
          </tr>
          <tr valign="bottom"> 
            <td height="50">&nbsp;</td>
            <td colspan="2" height="50"> 
              <?if ($bTilmeldingSlut==1){ ?>
              <font size="4" color="#FF0000"> Tilmeldingen er afsluttet <?=$tilmeldingslut?>.</font> 
              <?} else {?>
              <input type="submit" value="Tilmelding" />
			  <?}?>			  
          </tr>
        </table>
        <span id="Message"></span> 
        <div id="valsum1" headertext="Følgende fejl skal rettes i formularen ovenfor:" displaymode="List" style="color:Red;display:none;"> 
        </div>
        <p>Efter du har udfyldt ovenst&aring;ende formular og trykket p&aring; 
          &quot;<b>Tilmelding</b>&quot; vil du f&aring; tilsendt en e-mail som 
          kvittering.</p>
        <script language="javascript">
  <!--
  var Page_ValidationSummaries =  new Array(document.all["valsum1"]);
  var Page_Validators =  new Array(
  	document.all["Fornavnval"], 
  	document.all["Efternavnval"], 
  	document.all["Adresseval"], 
  	document.all["Postnrval1"], 
  	document.all["Postnrval2"], 
  	document.all["Bynavnval"], 
  	document.all["Telefonnrval"], 
  	document.all["Telefonnrval1"], 
  	document.all["Emailval"], 
  	document.all["Emailval1"] 
  	);

  var Page_ValidationErrorPrefix = "Validation script error: ";
  var Page_ValidationBadID = "Client ID is not unique: ";
  var Page_ValidationBadFunction = "Invalid ClientValidationFunction: ";
  var Page_ValidationActive = false;
  if (typeof(Page_ValidationVer) == "undefined")
      alert("Warning! Unable to find script library 'WebUIValidation.js'.");
  else if (Page_ValidationVer != "112")
      alert("Warning! This page is using the wrong version of 'WebUIValidation.js'. Page expects version '112'. Script library is '" + Page_ValidationVer + "'.");
  else
      ValidatorOnLoad();

  function ValidatorOnSubmit() {
      if (Page_ValidationActive) {
          ValidatorCommonOnSubmit();
      }
  }
  
  // -->
  </script>
      </form>
      <? } // End If $DoWhat == 1 ?>
      <? if ($DoWhat == 2) { // Kvittering ?>
      <p align=left><font color=#003399 size=4>Tilmelding til <?=$Navn;?></font></p>
      <p> 
        <?= $Fornavn;?>
        tak for din tilmelding til 
        <?=$Navn;?>
        <?=DatoFormat($Dato);?>
        . Der er nu sendt en e-mail til 
        <?= $Email;?>
        som kvittering.<br>
      </p>
      <? } // End If $DoWhat == 2 ?>
      <? if ($DoWhat == 3) { // Forkert e-mail addresse ?>
      <p align=left><font color=#003399 size=4>Tilmelding til <?=$Navn;?></font></p>
      <p> 
        <?= $Fornavn;?>
        , e-mail adressen <b> 
        <?= $Email;?>
        </b> som du har oplyst, kan vi ikke bruge til at sende en kvittering.</p>
      <? } // End If $DoWhat == 3 ?>
      <!-- #EndEditable --> </td>
    <td width="30">&nbsp;</td>
  </tr>
  <tr> 
    <td width="30">&nbsp;</td>
    <td align="right"><br>
      <font color=#a03952 face="Arial, Helvetica, sans-serif" 
size=2><i><b></b></i></font> <br>
      <br>
    </td>
    <td width="30">&nbsp;</td>
  </tr>
</table>
</body>
<!-- #EndTemplate --></html>
