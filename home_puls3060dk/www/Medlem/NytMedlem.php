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
include_once("dbFunctions.inc");  
require_once('email_message.php');

function DatoFormat($Sender)
{
	return(strtolower(strftime("%e. %B %Y", strtotime($Sender))));
}

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

include_once("conn.inc");  
$dbLink = pg_connect($conn_www);

$Query="SELECT ((now())::date) as indmeldtdato, ((date_trunc('month', now()) + interval '1 year'  - interval '1 day')::date) as kontingenttildato;";
$dbResult = pg_query($dbLink, $Query);
$row = pg_fetch_array($dbResult, NULL , PGSQL_ASSOC);
$indmeldtdato      = $row["indmeldtdato"];
$kontingenttildato = $row["kontingenttildato"];
$kontingentkr      = 150;


if ($DoWhat != 2)
    $DoWhat = 1; 

if ($DoWhat == 2){
  if (MailVal($Email, 3))
    $DoWhat = 3; 
}



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
    $set1 .="indmeldtdato";
    $set2 .="'$indmeldtdato'";
    $fset =1;
  }

  if ($kontingenttildato){
    if ($fset){
      $set1 .= ", ";
      $set2 .= ", ";
    }
    $set1 .="kontingenttildato";
    $set2 .="'$kontingenttildato'";
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
  $Body  = 'Denne e-mail er genereret af siden "Indmeldelse" på www.puls3060.dk';
  $Body .= "\r\n";
  $Body .= "\r\n  Ref-nr......: " . $P3060_REF;
  $Body .= "\r\n  Fornavn.....: " . $Fornavn;
  $Body .= "\r\n  Efternavn...: " . $Efternavn;
  $Body .= "\r\n  Adresse.....: " . $Adresse;
  $Body .= "\r\n  Postnummer..: " . $Postnr;
  $Body .= "\r\n  By..........: " . $Bynavn;
  $Body .= "\r\n  Telefon.....: " . $Telefonnr;
  $Body .= "\r\n  E-mail......: " . $Email;
  $Body .= "\r\n  Fødselsdato.: " . $Fodelsdato;
  $Body .= "\r\n  Køn.........: " . $Køn;
  $Body .= "\r\n  Kontingent..: " . $kontingentkr . ",00 kr.";
  $Body .= "\r\n  Fra.........: " . DatoFormat($indmeldtdato);
  $Body .= "\r\n  Til.........: " . DatoFormat($kontingenttildato);

//  mail("mha@hafsjold.dk", $Subj, $Body, "From: " . $Email . "\r\nReply-To: " . $Email . "\r\nBcc: arkiv@puls3060.dk" . "\r\nX-Mailer: PHP", "-f mha@puls3060.dk");
  $email_message=new email_message_class;
  $email_message->SetEncodedEmailHeader("To","mha@hafsjold.dk","Mogens Hafsjold");
  $email_message->SetEncodedEmailHeader("Bcc","arkiv@puls3060.dk","Arkiv");
  $email_message->SetEncodedEmailHeader("From",$Email,"$Fornavn $Efternavn");
  $email_message->SetEncodedEmailHeader("Reply-To",$Email,"$Fornavn $Efternavn");
  $email_message->SetHeader("Sender","mha@puls3060.dk");
  $email_message->SetHeader("Return-Path","mha@puls3060.dk");
  
  $email_message->SetEncodedHeader("Subject",$Subj);
  $email_message->AddQuotedPrintableTextPart($email_message->WrapText($Body));
  
  $error=$email_message->Send();





  $Subj = "Medlemskab af Puls 3060";
  $Body  = "Hej $Fornavn,";
  $Body .= "\r\n";
  $Body .= "\r\nTak for din indmeldelse i Puls 3060.";
  $Body .= "\r\nDu har givet os følgende oplysninger i forbindelse med din indmeldelse:";
  $Body .= "\r\n";
  $Body .= "\r\n  Ref-nr......: " . $P3060_REF;
  $Body .= "\r\n  Fornavn.....: " . $Fornavn;
  $Body .= "\r\n  Efternavn...: " . $Efternavn;
  $Body .= "\r\n  Adresse.....: " . $Adresse;
  $Body .= "\r\n  Postnummer..: " . $Postnr;
  $Body .= "\r\n  By..........: " . $Bynavn;
  $Body .= "\r\n  Telefon.....: " . $Telefonnr;
  $Body .= "\r\n  E-mail......: " . $Email;
  $Body .= "\r\n  Fødselsdato.: " . $Fodelsdato;
  $Body .= "\r\n  Køn.........: " . $Køn;
  $Body .= "\r\n";
  $Body .= "\r\nInden medlemskabet kan træde i kraft, skal du betale et medlemskontingent, for perioden fra " . DatoFormat($indmeldtdato) . " og frem til " . DatoFormat($kontingenttildato) . ", på kr. " . $kontingentkr . ",00.";
  $Body .= "\r\n";
  $Body .= "\r\nDu vil inden for kort tid modtage et indbetalingskort, som skal anvendes til betaling af kontingentet.";
  $Body .= "\r\n";
  $Body .= "\r\nVi ser frem til at løbe eller power walke sammen med dig.";
  $Body .= "\r\n";
  $Body .= "\r\nMed venlig hilsen";
  $Body .= "\r\nPuls 3060";
  $Body .= "\r\n";
  $Body .= "\r\nBesøg vores hjemmeside www.puls3060.dk ";

//  mail($Email, $Subj, $Body, "From: mha@hafsjold.dk\r\nReply-To: mha@hafsjold.dk\r\nBcc: arkiv@puls3060.dk\r\nX-Mailer: PHP", "-f mha@puls3060.dk");
  $email_message=new email_message_class;
  $email_message->SetEncodedEmailHeader("To",$Email,"$Fornavn $Efternavn");
  $email_message->SetEncodedEmailHeader("Bcc","arkiv@puls3060.dk","Arkiv");
  $email_message->SetEncodedEmailHeader("From","mha@puls3060.dk","Mogens Hafsjold");
  $email_message->SetEncodedEmailHeader("Reply-To","mha@puls3060.dk","Mogens Hafsjold");
  $email_message->SetHeader("Sender","mha@puls3060.dk");
  $email_message->SetHeader("Return-Path","mha@puls3060.dk");
  
  $email_message->SetEncodedHeader("Subject",$Subj);
  $email_message->AddQuotedPrintableTextPart($email_message->WrapText($Body));
  
  $error=$email_message->Send();


}
?>

<title>Indmeldelse</title>
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
      <p align=left><font color=#003399 size=4>Indmeldelse</font></p>
      <p> Du kan melde dig ind i L&oslash;beklubben Puls 3060 ved at udfylde formularen 
        p&aring; denne side og trykke p&aring; knappen &quot;Indmeldelse&quot;. 
        Kontingentet er kr. 150 pr. &aring;r.</p>
      <form method="post" action="NytMedlem.php?DoWhat=2" language="javascript" onsubmit="ValidatorOnSubmit();">
        <table width="100%" border="0">
          <tr> 
            <td>Fornavn:</td>
            <td> 
              <input name="Fornavn" type="text" maxlength="25" id="Fornavn" size="25" />
              <span id="Fornavnval" controltovalidate="Fornavn" errormessage="Fornavn skal udfyldes" display="Dynamic" evaluationfunction="RequiredFieldValidatorEvaluateIsValid" initialvalue="" style="color:Red;display:none;"> 
                skal udfyldes 
              </span> </td>
            <td>&nbsp;</td>
          </tr>
          <tr> 
            <td>Efternavn:</td>
            <td> 
              <input name="Efternavn" type="text" maxlength="25" id="Efternavn" size="25" />
              <span id="Efternavnval" controltovalidate="Efternavn" errormessage="Efternavn skal udfyldes" display="Dynamic" evaluationfunction="RequiredFieldValidatorEvaluateIsValid" initialvalue="" style="color:Red;display:none;"> 
                skal udfyldes 
              </span> </td>
            <td>&nbsp;</td>
          </tr>
          <tr> 
            <td>Adresse:</td>
            <td> 
              <input name="Adresse" type="text" maxlength="35" id="Adresse" size="35" />
              <span id="Adresseval" controltovalidate="Adresse" errormessage="Adresse skal udfyldes" display="Dynamic" evaluationfunction="RequiredFieldValidatorEvaluateIsValid" initialvalue="" style="color:Red;display:none;"> 
                skal udfyldes 
              </span> </td>
            <td>&nbsp;</td>
          </tr>
          <tr> 
            <td>Postnummer:</td>
            <td> 
              <input name="Postnr" type="text" maxlength="4" id="Postnr" size="4" />
              <span id="Postnrval1" controltovalidate="Postnr" errormessage="Postnr skal udfyldes" display="Dynamic" evaluationfunction="RequiredFieldValidatorEvaluateIsValid" initialvalue="" style="color:Red;display:none;"> 
                skal udfyldes 
              </span> 
              <span id="Postnrval2" controltovalidate="Postnr" errormessage="Postnr skal være mellem 1000 og 9900" display="Dynamic" evaluationfunction="RegularExpressionValidatorEvaluateIsValid" validationexpression="[1-9]{1}[0-9]{3}" style="color:Red;display:none;"> 
                skal være mellem 1000 og 9900 
              </span> </td>
            <td>&nbsp;</td>
          </tr>
          <tr> 
            <td>By:</td>
            <td> 
              <input name="Bynavn" type="text" maxlength="25" id="Bynavn" size="25" />
              <span id="Bynavnval" controltovalidate="Bynavn" errormessage="By skal udfyldes" display="Dynamic" evaluationfunction="RequiredFieldValidatorEvaluateIsValid" initialvalue="" style="color:Red;display:none;"> 
                skal udfyldes 
              </span> </td>
            <td>&nbsp;</td>
          </tr>
          <tr> 
            <td>Telefon</td>
            <td> 
              <input name="Telefonnr" type="text" maxlength="8" id="Telefonnr" size="8" />
              <span id="Telefonnrval1" controltovalidate="Telefonnr" errormessage="Telefonnr skal udfyldes" display="Dynamic" evaluationfunction="RequiredFieldValidatorEvaluateIsValid" initialvalue="" style="color:Red;display:none;"> 
                skal udfyldes 
              </span> 
              <span id="Telefonnrval" controltovalidate="Telefonnr" errormessage="Telefonnr skal være mellem 10000000 og 99999999" display="Dynamic" evaluationfunction="RegularExpressionValidatorEvaluateIsValid" validationexpression="[1-9]{1}[0-9]{7}" style="color:Red;display:none;"> 
                skal være mellem 10000000 og 99999999 
              </span> </td>
            <td>&nbsp;</td>
          </tr>
          <tr> 
            <td>E-mail:</td>
            <td> 
              <input name="Email" type="text" id="Email" size="30" />
              <span id="Emailval1" controltovalidate="Email" errormessage="Email skal udfyldes" display="Dynamic" evaluationfunction="RequiredFieldValidatorEvaluateIsValid" initialvalue="" style="color:Red;display:none;"> 
                skal udfyldes 
              </span> 
              <span id="Emailval" controltovalidate="Email" errormessage="Email skal udfyldes korrekt" display="Dynamic" evaluationfunction="RegularExpressionValidatorEvaluateIsValid" validationexpression="[a-zA-Z_0-9\.\-]{1,40}@([a-zA-Z0-9\-]{1,40}\.){1,5}[a-zA-Z]{2,3}" style="color:Red;display:none;"> 
                skal udfyldes korrekt 
              </span> </td>
            <td> <font size="3">(navn@abc.dk)</font></td>
          </tr>
          <tr> 
            <td>F&oslash;dselsdato:</td>
            <td> 
              <input name="Fodelsdato" type="text" maxlength="10" id="Fodelsdato" size="10" />
              <span id="Fodelsdatoval1" controltovalidate="Fodelsdato" errormessage="Fødelsdato skal udfyldes" display="Dynamic" evaluationfunction="RequiredFieldValidatorEvaluateIsValid" initialvalue="" style="color:Red;display:none;"> 
                skal udfyldes 
              </span> 
              <span id="Fodelsdatoval3" controltovalidate="Fodelsdato" errormessage="Fødelsdato skilletegn skal være -" display="Dynamic" evaluationfunction="RegularExpressionValidatorEvaluateIsValid" validationexpression="[^-]+-[^-]+-[^-]+" style="color:Red;display:none;"> 
                skilletegn skal være - 
              </span> 
              <span id="Fodelsdatoval2" controltovalidate="Fodelsdato" errormessage="Fødelsdato skal udfyldes med en dato" display="Dynamic" evaluationfunction="CompareValidatorEvaluateIsValid" operator="DataTypeCheck" type="Date" dateorder="dmy" style="color:Red;display:none;"> 
                skal udfyldes med en dato 
              </span> 
              <span id="Fodelsdatoval4" controltovalidate="Fodelsdato" errormessage="Alder skal være mellem 2 og 99 år" display="Dynamic" evaluationfunction="CustomValidatorEvaluateIsValid" clientvalidationfunction="CheckAlder" style="color:Red;display:none;"> 
                Alder skal være mellem 2 og 99 år 
              </span> 
            </td>
            <td><font size="3">(dd-mm-&aring;&aring;&aring;&aring;)</font></td>
          </tr>
          <tr> 
            <td>K&oslash;n:</td>
            <td> 
              <input name="Køn" type="text" maxlength="1" id="Køn" size="1" />
              <span id="Kønval1" controltovalidate="Køn" errormessage="Køn skal udfyldes" display="Dynamic" evaluationfunction="RequiredFieldValidatorEvaluateIsValid" initialvalue="" style="color:Red;display:none;"> 
                skal udfyldes 
              </span> 
              <span id="Kønval2" controltovalidate="Køn" errormessage="Køn skal udfyldes med m eller k" display="Dynamic" evaluationfunction="RegularExpressionValidatorEvaluateIsValid" validationexpression="[mk]{1,1}" style="color:Red;display:none;"> 
                skal udfyldes med m eller k 
              </span> </td>
            <td><font size="3">(m: mand, k: kvinde)</font></td>
          </tr>
          <tr valign="bottom"> 
            <td height="50">&nbsp;</td>
            <td colspan="2" height="50"> 
              <? if ($IEbrowser == 1) { ?>
                <input type="submit" value="Indmeldelse" />
              <?} else {?>
              <font color="#FF3300">Kan kun anvendes med Microsoft IE Browser 
              nyere end version 5.5.<br>
              Du kan alternativt sende en e-mail med ønske om indmeldelse i Puls 
              3060 til <a href="mailto:mha@hafsjold.dk">mha@hafsjold.dk</a><br>
              Husk ogs&aring; at skrive dine personlige oplysninger s&aring; vi 
              kan registrerer dig som medlem.</font> 
              <? } ?>
          </tr>
        </table>
        <span id="Message"></span> 
        <div id="valsum1" headertext="Følgende fejl skal rettes i formularen ovenfor:" displaymode="List" style="color:Red;display:none;"> 
        </div>
        <p>Efter du har udfyldt ovenst&aring;ende formular og trykket p&aring; 
          &quot;<b>Indmeldelse</b>&quot; vil du f&aring; tilsendt en e-mail med 
          oplysning om betaling af kontingent.</p>
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
  	document.all["Telefonnrval1"], 
  	document.all["Emailval"], 
  	document.all["Emailval1"], 
  	document.all["Fodelsdatoval1"], 
  	document.all["Fodelsdatoval3"], 
  	document.all["Fodelsdatoval2"], 
  	document.all["Fodelsdatoval4"], 
  	document.all["Kønval1"], 
  	document.all["Kønval2"]);

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
      day = m[1];
      month = m[3];
      year = m[4];
      var now = new Date();
      var date = new Date(year, (month - 1), day);
	  var alder = now.getFullYear() - date.getFullYear() + 1;
      return (alder > 1) && (alder < 100);
  }
  // -->
  </script>
      </form>
<? } // End If $DoWhat == 1 ?>

<? if ($DoWhat == 2) { // Kvitering ?>
      <p align=left><font color=#003399 size=4>Indmeldelse</font></p>
      <p> 
        <?= $Fornavn;?>
        tak for din indmeldelse i Puls 3060. Der er nu sendt en e-mail til 
        <?= $Email;?>
        med oplysning om betaling af kontingent.<br>
      </p>
<? } // End If $DoWhat == 2 ?>

<? if ($DoWhat == 3) { // Forkert e-mail addresse ?>
      <p align=left><font color=#003399 size=4>Indmeldelse</font></p>
      <p> 
        <?= $Fornavn;?>
        , e-mail adressen <b>
        <?= $Email;?>
        </b> som du har oplyst, kan vi ikke bruge til at sende oplysning om betaling 
        af kontingent.<br>
        <br>
        Hvis e-mail adressen er forkert, så tryk på "Indmeldelse" ude til venstre 
        og udfyld og send formularen igen.<br>
        <br>
		Hvis e-mail adressen er korrekt, så tryk på "Bestyrelse" ude til venstre og ring eller skriv til et medlem af bestyrelsen for at blive indmeldt. 
      </p>
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
