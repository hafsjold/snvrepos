<? header ("Last-Modified: " . gmdate("D, d M Y H:i:s") . " GMT"); // always modified ?>
<html>
<head>
<script language="javascript"
  src="../Scripts/WebUIValidation.js">
</script>

<?
setlocale (LC_TIME, "da_DK.ISO8859-1");
include_once("dbFunctions.inc");  
include_once("mailFunctions.inc");  
require_once('email_message.php');

function DatoFormat($Sender)
{
	return(strtolower(strftime("%e. %B %Y", strtotime($Sender))));
}

function DatoFormat2($SenderDato)
{
	return(strftime("%d-%m-%Y", strtotime($SenderDato)));
}


include_once("conn.inc");  
$dbLink = pg_connect($conn_www);

$Query="SELECT ((now())::date) as indmeldtdato, ((date_trunc('month', now()) + interval '1 year'  - interval '1 day')::date) as kontingenttildato;";
$dbResult = pg_query($dbLink, $Query);
$row = pg_fetch_array($dbResult, NULL , PGSQL_ASSOC);

$mha_kontingenttildato = $row["kontingenttildato"];
$mha_kontingenttildatoStr1 = DatoFormat($mha_kontingenttildato);
$mha_kontingenttildatoStr2 = DatoFormat2($mha_kontingenttildato);
  
$browser = get_browser();
if ($browser->browser == "IE") 
  if (($browser->majorver == 5) 
  && ($browser->minorver >= 5) 
  || ($browser->majorver > 5)) 
  $IEbrowser = 1;
else
  $IEbrowser = 0;

if (isset($_REQUEST['DoWhat'])) 
  $DoWhat=$_REQUEST['DoWhat'];

if (isset($_REQUEST['Fornavn'])) 
  $Fornavn=$_REQUEST['Fornavn'];

if (isset($_REQUEST['Efternavn'])) 
  $Efternavn=$_REQUEST['Efternavn'];

if (isset($_REQUEST['Adresse'])) 
  $Adresse=$_REQUEST['Adresse'];

if (isset($_REQUEST['Postnr'])) 
  $Postnr=$_REQUEST['Postnr'];

if (isset($_REQUEST['Bynavn'])) 
  $Bynavn=$_REQUEST['Bynavn'];

if (isset($_REQUEST['Telefonnr'])) 
  $Telefonnr=$_REQUEST['Telefonnr'];

if (isset($_REQUEST['Email'])) 
  $Email=$_REQUEST['Email'];

if (isset($_REQUEST['Fodelsdato'])) 
  $Fodelsdato=$_REQUEST['Fodelsdato'];

if (isset($_REQUEST['Køn'])) 
  $Køn=$_REQUEST['Køn'];

if (isset($_REQUEST['indmeldtdato'])) 
  $indmeldtdato=$_REQUEST['indmeldtdato'];
else
  $indmeldtdato = $row["indmeldtdato"];

if (isset($_REQUEST['kontingenttildato'])) 
  $kontingenttildato=$_REQUEST['kontingenttildato'];
else
  $kontingenttildato = $row["kontingenttildato"];

if (isset($_REQUEST['kontingentkr'])) 
  $kontingentkr=$_REQUEST['kontingentkr'];
else
  $kontingentkr = 150;

if (isset($_REQUEST['action'])) 
  $action=$_REQUEST['action'];
else
  $action = "p";



if ($DoWhat != 2)
    $DoWhat = 1; 


if ($DoWhat == 2){
  // Opret tblNytMedlem
  $set1 ="(";
  $set2 ="VALUES(";
  $fset =0;
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

  if ($Fodelsdato){
    if ($fset){
      $set1 .= ", ";
      $set2 .= ", ";
    }
	$Fodelsdatoymd = substr($Fodelsdato, 6, 4) . "-" . substr($Fodelsdato, 3, 2)  . "-" . substr($Fodelsdato, 0, 2);
    $set1 .="FodtDato";
    $set2 .="'$Fodelsdatoymd'";
    $fset =1;
  }
  
  if ($Køn){
    if ($fset){
      $set1 .= ", ";
      $set2 .= ", ";
    }
    $set1 .="Kon";
    $set2 .="'$Køn'";
    $fset =1;
  }

  if ($indmeldtdato){
    if ($fset){
      $set1 .= ", ";
      $set2 .= ", ";
    }
	$indmeldtdatoymd = substr($indmeldtdato, 6, 4) . "-" . substr($indmeldtdato, 3, 2)  . "-" . substr($indmeldtdato, 0, 2);
    $set1 .="indmeldtdato";
    $set2 .="'$indmeldtdatoymd'::date";
    $fset =1;
  }

  if ($kontingenttildato){
    if ($fset){
      $set1 .= ", ";
      $set2 .= ", ";
    }
	$kontingenttildatoymd = substr($kontingenttildato, 6, 4) . "-" . substr($kontingenttildato, 3, 2)  . "-" . substr($kontingenttildato, 0, 2);
    $set1 .="kontingenttildato";
    $set2 .="'$kontingenttildatoymd'::date";
    $fset =1;
  }

  if ($kontingentkr){
    if ($fset){
      $set1 .= ", ";
      $set2 .= ", ";
    }
    $set1 .="kontingentkr";
    $set2 .=$kontingentkr;
    $fset =1;
  }
  
  if ($fset) {
    $set1 .=", IP";
    $set2 .=", '" . $_SERVER['REMOTE_ADDR'] . "'";
    
    $Query=" INSERT INTO tblNytMedlem " . $set1 . ") " . $set2 . ");";
    $debug = $Query;
    $dbResult = pg_query($dbLink, $Query);
    $P3060_REF = pg_insert_id($dbLink, 'tblnytmedlem_id_seq');
  }
  else
    $P3060_REF = 9999;
  
  $Subj = "Nyt Medlem af Puls 3060";
  $Body  = 'Denne e-mail er genereret af siden "NytMedlem" på adm.puls3060.dk';
  $Body .= "\n";
  $Body .= "\n  Ref-nr......: " . $P3060_REF;
  $Body .= "\n  Fornavn.....: " . $Fornavn;
  $Body .= "\n  Efternavn...: " . $Efternavn;
  $Body .= "\n  Adresse.....: " . $Adresse;
  $Body .= "\n  Postnummer..: " . $Postnr;
  $Body .= "\n  By..........: " . $Bynavn;
  $Body .= "\n  Telefon.....: " . $Telefonnr;
  $Body .= "\n  E-mail......: " . $Email;
  $Body .= "\n  Fødselsdato.: " . $Fodelsdato;
  $Body .= "\n  Køn.........: " . $Køn;
  $Body .= "\n  Kontingent..: " . $kontingentkr . ",00 kr.";
  $Body .= "\n  Fra.........: " . DatoFormat($indmeldtdatoymd);
  $Body .= "\n  Til.........: " . DatoFormat($kontingenttildatoymd);
  //mail("mha@hafsjold.dk", $Subj, $Body, "From: " . $Email . "\nReply-To: " . $Email . "\nBcc: arkiv@puls3060.dk" . "\nX-Mailer: PHP", "-f mha@puls3060.dk");
  
  $email_message=new email_message_class;
  $email_message->SetEncodedEmailHeader("To","mha@hafsjold.dk","Mogens Hafsjold");
  $email_message->SetEncodedEmailHeader("Bcc","arkiv@puls3060.dk","Arkiv");
  $email_message->SetHeader("From",$Email);
  $email_message->SetHeader("Reply-To",$Email);
  $email_message->SetHeader("Sender","mha@puls3060.dk");
  $email_message->SetHeader("Return-Path","mha@puls3060.dk");
  
  $email_message->SetEncodedHeader("Subject",$Subj);
  $email_message->AddQuotedPrintableTextPart($email_message->WrapText($Body));
  
  $error=$email_message->Send();
  
}
?>

<title>Indmeldelse</title>
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
      
    <td valign="top"> 
      <? if ($DoWhat == 1) { // Udfyld formular og send ?>
      <font color=#003399 size=4>Opret nyt medlem</font>
       <form method="post" action="NytMedlem.php?DoWhat=2" language="javascript" onsubmit="ValidatorOnSubmit();">
        <table width="100%" border="0">
          <tr> 
            <td>Fornavn:</td>
            <td> 
              <input name="Fornavn" type="text" maxlength="25" id="Fornavn" size="25" />
              <span id="Fornavnval" controltovalidate="Fornavn" errormessage="Fornavn skal udfyldes" display="Dynamic" evaluationfunction="RequiredFieldValidatorEvaluateIsValid" initialvalue="" style="color:Red;display:none;"> 
              skal udfyldes </span> </td>
            <td>&nbsp;</td>
          </tr>
          <tr> 
            <td>Efternavn:</td>
            <td> 
              <input name="Efternavn" type="text" maxlength="25" id="Efternavn" size="25" />
              <span id="Efternavnval" controltovalidate="Efternavn" errormessage="Efternavn skal udfyldes" display="Dynamic" evaluationfunction="RequiredFieldValidatorEvaluateIsValid" initialvalue="" style="color:Red;display:none;"> 
              skal udfyldes </span> </td>
            <td>&nbsp;</td>
          </tr>
          <tr> 
            <td>Adresse:</td>
            <td> 
              <input name="Adresse" type="text" maxlength="35" id="Adresse" size="35" />
              <span id="Adresseval" controltovalidate="Adresse" errormessage="Adresse skal udfyldes" display="Dynamic" evaluationfunction="RequiredFieldValidatorEvaluateIsValid" initialvalue="" style="color:Red;display:none;"> 
              skal udfyldes </span> </td>
            <td>&nbsp;</td>
          </tr>
          <tr> 
            <td>Postnummer:</td>
            <td> 
              <input name="Postnr" type="text" maxlength="4" id="Postnr" size="4" onchange="getBynavn_xmlhttp();"/>
              <span id="Postnrval1" controltovalidate="Postnr" errormessage="Postnr skal udfyldes" display="Dynamic" evaluationfunction="RequiredFieldValidatorEvaluateIsValid" initialvalue="" style="color:Red;display:none;"> 
              skal udfyldes </span> <span id="Postnrval2" controltovalidate="Postnr" errormessage="Postnr skal være mellem 1000 og 9900" display="Dynamic" evaluationfunction="RegularExpressionValidatorEvaluateIsValid" validationexpression="[1-9]{1}[0-9]{3}" style="color:Red;display:none;"> 
              skal være mellem 1000 og 9900 </span> </td>
            <td>&nbsp;</td>
          </tr>
          <tr> 
            <td>By:</td>
            <td> 
              <input name="Bynavn" type="text" maxlength="25" id="Bynavn" size="25" />
              <span id="Bynavnval" controltovalidate="Bynavn" errormessage="By skal udfyldes" display="Dynamic" evaluationfunction="RequiredFieldValidatorEvaluateIsValid" initialvalue="" style="color:Red;display:none;"> 
              skal udfyldes </span> </td>
            <td>&nbsp;</td>
          </tr>
          <tr> 
            <td>Telefon</td>
            <td> 
              <input name="Telefonnr" type="text" maxlength="8" id="Telefonnr" size="8" />
              <span id="Telefonnrval" controltovalidate="Telefonnr" errormessage="Telefonnr skal være mellem 10000000 og 99999999" display="Dynamic" evaluationfunction="RegularExpressionValidatorEvaluateIsValid" validationexpression="[1-9]{1}[0-9]{7}" style="color:Red;display:none;"> 
              skal være mellem 10000000 og 99999999 </span> </td>
            <td>&nbsp;</td>
          </tr>
          <tr> 
            <td>E-mail:</td>
            <td> 
              <input name="Email" type="text" id="Email" size="30" />
              <span id="Emailval" controltovalidate="Email" errormessage="Email skal udfyldes korrekt" display="Dynamic" evaluationfunction="RegularExpressionValidatorEvaluateIsValid" validationexpression="[a-zA-Z_0-9\.\-]{1,40}@([a-zA-Z0-9\-]{1,40}\.){1,5}[a-zA-Z]{2,3}" style="color:Red;display:none;"> 
              skal udfyldes korrekt </span> </td>
            <td> <font size="3">(navn@abc.dk)</font></td>
          </tr>
          <tr> 
            <td>F&oslash;dselsdato:</td>
            <td> 
              <input name="Fodelsdato" type="text" maxlength="10" id="Fodelsdato" size="10" />
              <span id="Fodelsdatoval3" controltovalidate="Fodelsdato" errormessage="Fødelsdato skilletegn skal være -" display="Dynamic" evaluationfunction="RegularExpressionValidatorEvaluateIsValid" validationexpression="[^-]+-[^-]+-[^-]+" style="color:Red;display:none;"> 
              skilletegn skal være - </span> <span id="Fodelsdatoval2" controltovalidate="Fodelsdato" errormessage="Fødelsdato skal udfyldes med en dato" display="Dynamic" evaluationfunction="CompareValidatorEvaluateIsValid" operator="DataTypeCheck" type="Date" dateorder="dmy" style="color:Red;display:none;"> 
              skal udfyldes med en dato </span> <span id="Fodelsdatoval4" controltovalidate="Fodelsdato" errormessage="Alder skal være mellem 2 og 99 år" display="Dynamic" evaluationfunction="CustomValidatorEvaluateIsValid" clientvalidationfunction="CheckAlder" style="color:Red;display:none;"> 
              Alder skal være mellem 2 og 99 år </span> </td>
            <td><font size="3">(dd-mm-&aring;&aring;&aring;&aring;)</font></td>
          </tr>
          <tr> 
            <td>K&oslash;n:</td>
            <td> 
              <input name="Køn" type="text" maxlength="1" id="Køn" size="1" />
              <span id="Kønval1" controltovalidate="Køn" errormessage="Køn skal udfyldes" display="Dynamic" evaluationfunction="RequiredFieldValidatorEvaluateIsValid" initialvalue="" style="color:Red;display:none;"> 
              skal udfyldes </span> <span id="Kønval2" controltovalidate="Køn" errormessage="Køn skal udfyldes med m eller k" display="Dynamic" evaluationfunction="RegularExpressionValidatorEvaluateIsValid" validationexpression="[mk]{1,1}" style="color:Red;display:none;"> 
              skal udfyldes med m eller k </span> </td>
            <td><font size="3">(m: mand, k: kvinde)</font></td>
          </tr>
          <tr> 
            <td>Indmeldt dato:</td>
            <td> 
              <input name="indmeldtdato" type="text" maxlength="10" id="indmeldtdato" size="10" value="<?=DatoFormat2($indmeldtdato)?>" />
              <span id="indmeldtdatoval1" controltovalidate="indmeldtdato" errormessage="Indmeldt dato skal udfyldes" display="Dynamic" evaluationfunction="RequiredFieldValidatorEvaluateIsValid" initialvalue="" style="color:Red;display:none;"> 
              skal udfyldes </span> <span id="indmeldtdatoval3" controltovalidate="indmeldtdato" errormessage="Indmeldt dato skilletegn skal være -" display="Dynamic" evaluationfunction="RegularExpressionValidatorEvaluateIsValid" validationexpression="[^-]+-[^-]+-[^-]+" style="color:Red;display:none;"> 
              skilletegn skal være - </span> <span id="indmeldtdatoval2" controltovalidate="indmeldtdato" errormessage="Indmeldt dato skal udfyldes med en dato" display="Dynamic" evaluationfunction="CompareValidatorEvaluateIsValid" operator="DataTypeCheck" type="Date" dateorder="dmy" style="color:Red;display:none;"> 
              skal udfyldes med en dato </span> <span id="indmeldtdatoval4" controltovalidate="indmeldtdato" errormessage="Indmeldt dato skal være i intervallet +- 30 dage" display="Dynamic" evaluationfunction="CustomValidatorEvaluateIsValid" clientvalidationfunction="CheckIndmeldtDato" style="color:Red;display:none;"> 
              Indmeldt dato skal være i intervallet +- 30 dage </span> </td>
            <td><font size="3">(dd-mm-&aring;&aring;&aring;&aring;)</font></td>
          </tr>
          <tr> 
          <tr> 
            <td>Kontingent til dato:</td>
            <td> 
              <input name="kontingenttildato" type="text" maxlength="10" id="kontingenttildato" size="10" value="<?=DatoFormat2($kontingenttildato)?>" />
              <span id="kontingenttildatoval1" controltovalidate="kontingenttildato" errormessage="Kontingent til dato skal udfyldes" display="Dynamic" evaluationfunction="RequiredFieldValidatorEvaluateIsValid" initialvalue="" style="color:Red;display:none;"> 
              skal udfyldes </span> <span id="kontingenttildatoval3" controltovalidate="kontingenttildato" errormessage="Kontingent til dato skilletegn skal være -" display="Dynamic" evaluationfunction="RegularExpressionValidatorEvaluateIsValid" validationexpression="[^-]+-[^-]+-[^-]+" style="color:Red;display:none;"> 
              skilletegn skal være - </span> <span id="kontingenttildatoval2" controltovalidate="kontingenttildato" errormessage="Kontingent til dato skal udfyldes med en dato" display="Dynamic" evaluationfunction="CompareValidatorEvaluateIsValid" operator="DataTypeCheck" type="Date" dateorder="dmy" style="color:Red;display:none;"> 
              skal udfyldes med en dato </span> <span id="kontingenttildatoval4" controltovalidate="kontingenttildato" errormessage="Kontingent til dato skal være i intervallet 10 til 365 dage" display="Dynamic" evaluationfunction="CustomValidatorEvaluateIsValid" clientvalidationfunction="CheckBetaltTilDato" style="color:Red;display:none;"> 
              Kontingent til dato skal være i intervallet 10 til 365 dage </span> 
            </td>
            <td><font size="3">(dd-mm-&aring;&aring;&aring;&aring;)</font></td>
          </tr>
          <tr> 
          <tr> 
            <td>Kontingent</td>
            <td> 
              <input name="kontingentkr" type="text" maxlength="3" id="kontingentkr" size="3" value="<?=$kontingentkr?>" />
              <span id="kontingentkrval1" controltovalidate="kontingentkr" errormessage="Kontingent skal udfyldes" display="Dynamic" evaluationfunction="RequiredFieldValidatorEvaluateIsValid" initialvalue="" style="color:Red;display:none;"> 
              skal udfyldes </span> <span id="kontingentkrval2" controltovalidate="kontingentkr" errormessage="Kontingent skal være mellem 100 og 399 kr" display="Dynamic" evaluationfunction="RegularExpressionValidatorEvaluateIsValid" validationexpression="[1-3]{1}[0-9]{2}" style="color:Red;display:none;"> 
              skal være mellem 100 og 399 kr </span> </td>
            <td>(bel&oslash;b i kr.)</td>
          </tr>
          <tr> 
            <td>Opkr&aelig;vning:</td>
            <td> 
              <input name="action" type="text" maxlength="1" id="action" size="1" value="<?=$action?>" />
              <span id="actionval1" controltovalidate="action" errormessage="Opkr&aelig;vning skal udfyldes" display="Dynamic" evaluationfunction="RequiredFieldValidatorEvaluateIsValid" initialvalue="" style="color:Red;display:none;"> 
              skal udfyldes </span> <span id="actionval2" controltovalidate="action" errormessage="Opkr&aelig;vning skal udfyldes med g eller p" display="Dynamic" evaluationfunction="RegularExpressionValidatorEvaluateIsValid" validationexpression="[gp]{1,1}" style="color:Red;display:none;"> 
              skal udfyldes med g eller p </span> </td>
            <td> <font size="3"> (g: Giro, p: PBS) </font> </td>
          </tr>
          <tr valign="bottom"> 
            <td height="50">&nbsp;</td>
            <td colspan="2" height="50"> 
              <? if ($IEbrowser == 1) { ?>
              <input type="submit" value="Opret" />
              <?} else {?>
              <font color="#FF3300">Kan kun anvendes med Microsoft IE Browser 
              nyere end version 5.5.<br>
              </font> 
              <? } ?>
          </tr>
        </table>
        <span id="Message"></span> 
        <div id="valsum1" headertext="Følgende fejl skal rettes i formularen ovenfor:" displaymode="List" style="color:Red;display:none;"> 
        </div>
        <p>&nbsp;</p>
        <script language="javascript">
  <!--
  var Page_CompareValidators =  new Array(document.all["Fodelsdatoval2"]);
  var Page_ValidationSummaries =  new Array(document.all["valsum1"]);
  var Page_Validators =  new Array(
  	document.all["Fornavnval"], 
  	document.all["Efternavnval"], 
  	document.all["Adresseval"], 
  	document.all["Postnrval1"], 
  	document.all["Postnrval2"], 
  	document.all["Bynavnval"], 
  	document.all["Telefonnrval"], 
  	document.all["Emailval"], 
  	document.all["Fodelsdatoval3"], 
  	document.all["Fodelsdatoval2"], 
  	document.all["Fodelsdatoval4"], 
  	document.all["Kønval1"], 
  	document.all["Kønval2"],
  	document.all["indmeldtdatoval1"], 
  	document.all["indmeldtdatoval3"], 
  	document.all["indmeldtdatoval2"], 
  	document.all["indmeldtdatoval4"], 
  	document.all["kontingenttildatoval1"], 
  	document.all["kontingenttildatoval3"], 
  	document.all["kontingenttildatoval2"], 
  	document.all["kontingenttildatoval4"], 
  	document.all["kontingentkrval1"], 
  	document.all["kontingentkrval2"], 
 	document.all["actionval1"], 
  	document.all["actionval2"]);
  //debugger
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
  
  function CheckAlder(val, value) {
      var day, month, year, m, yearLastExp;
      yearLastExp = new RegExp("^\\s*(\\d{1,2})([-./])(\\d{1,2})\\2(\\d{4})\\s*$");
      m = value.match(yearLastExp);
      if (m == null) 
         return null;
      day = m[1];
      month = m[3];
      year = m[4];
      var now = new Date();
      var date = new Date(year, (month - 1), day);
	  var alder = now.getFullYear() - date.getFullYear() + 1;
      return (alder > 1) && (alder < 100);
  }
  function CheckIndmeldtDato(val, value) {
      var day, month, year, m, yearLastExp;
      yearLastExp = new RegExp("^\\s*(\\d{1,2})([-./])(\\d{1,2})\\2(\\d{4})\\s*$");
      m = value.match(yearLastExp);
      if (m == null) 
         return null;
      day = m[1];
      month = m[3];
      year = m[4];
      var now = new Date();
      var date = new Date(year, (month - 1), day);
	  var alder = date.valueOf() - now.valueOf();
	  var ms2dag = 24*60*60*1000;
      return (alder >= -30*ms2dag) && (alder <= 30*ms2dag);
  }
  function CheckBetaltTilDato(val, value) {
      var day, month, year, m, yearLastExp;
      yearLastExp = new RegExp("^\\s*(\\d{1,2})([-./])(\\d{1,2})\\2(\\d{4})\\s*$");
      m = value.match(yearLastExp);
      if (m == null) 
         return null;
      day = m[1];
      month = m[3];
      year = m[4];
      var now = new Date();
      var date = new Date(year, (month - 1), day);
	  var alder = date.valueOf() - now.valueOf();
	  var ms2dag = 24*60*60*1000;
      return (alder >= 10*ms2dag) && (alder <= 365*ms2dag);
  }

  function init_xmlhttp() {
	  var A;
	  var msxmlhttp = new Array(
	   	'Msxml2.XMLHTTP.5.0',
		'Msxml2.XMLHTTP.4.0',
		'Msxml2.XMLHTTP.3.0',
		'Msxml2.XMLHTTP',
		'Microsoft.XMLHTTP');
	  for (var i = 0; i < msxmlhttp.length; i++) {
		  try {
			  A = new ActiveXObject(msxmlhttp[i]);
		  } catch (e) {
		     A = null;
		  }
	  }
	  if(!A && typeof XMLHttpRequest != "undefined")
		  A = new XMLHttpRequest();
	  return A;
  }
  
  function getBynavn_xmlhttp() {
  	  var xmlhttp;
	  //debugger;
	  xmlhttp = init_xmlhttp();
	  xmlhttp.open("GET", "pnr.php?postnr="+document.getElementById("Postnr").value, true);
	  xmlhttp.onreadystatechange=function() {
	      if (xmlhttp.readyState==4) {
	          //alert (xmlhttp.responseText);
			  document.getElementById("Bynavn").value = xmlhttp.responseText;
          }
      }
	  xmlhttp.send(null);
  }

  // -->
  </script>
      </form>
      <? } // End If $DoWhat == 1 ?>
      <? if ($DoWhat == 2) { // Kvitering ?>
      <p align=left><font color=#003399 size=4>Opret nyt medlem</font></p>
      <p> 
        <?= $Fornavn;?>
        <?= $Efternavn;?>
        er oprettet som nyt medlem. </p>
      <form method="post" action="NytMedlem.php";">
        <input type="submit" value="OK" />
      </form>
      <? } // End If $DoWhat == 2 ?>
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
</html>
