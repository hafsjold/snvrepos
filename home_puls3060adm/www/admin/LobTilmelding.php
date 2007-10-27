<? header ("Last-Modified: " . gmdate("D, d M Y H:i:s") . " GMT"); // always modified ?>
<html>
<head>
<script language="javascript"    
  src="../Scripts/WebUIValidation.js">
</script>

<?
setlocale (LC_TIME, "da_DK.ISO8859-1");
include_once("mailFunctions.inc");  
include_once("dbFunctions.inc");  

function DatoFormat($Sender)
{
	return(strtolower(strftime("%A den %e. %B %Y kl %H.%M", strtotime($Sender))));
}

if (isset($_REQUEST['DoWhat'])) 
  $DoWhat=$_REQUEST['DoWhat'];

if (isset($_REQUEST['p0'])) 
  $p0=$_REQUEST['p0'];

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

if (isset($_REQUEST['FodtAar'])) 
  $FodtAar=$_REQUEST['FodtAar'];

if (isset($_REQUEST['Køn'])) 
  $Køn=$_REQUEST['Køn'];

if (isset($_REQUEST['Afdeling'])) 
  $Afdeling=$_REQUEST['Afdeling'];

if ($DoWhat != 2)
    $DoWhat = 1; 


include_once("conn.inc");  
$dbLink = pg_connect($conn_www);


$Query="SELECT ID, Navn, Dato
		FROM   tblLob
		WHERE Dato > NOW()
		ORDER BY Dato ASC";
$dbResult = pg_query($dbLink, $Query);
$row = pg_fetch_array($dbResult, NULL , PGSQL_ASSOC);
$LobId = $row["id"];
$Navn = $row["navn"];
$Dato = $row["dato"];
$Alder = strftime("%Y", strtotime($Dato)) - $FodtAar;

if ($DoWhat == 1){
    if (!$p0) $p0 = "0"; 
    $DId        = 0;			    
    $DFornavn   = "";   
    $DEfternavn = ""; 
    $DAdresse   = ""; 
    $DPostnr    = ""; 
    $DBynavn    = ""; 
    $DTelefonnr = ""; 
    $DEmail     = ""; 
    $DFodtAar   = ""; 
    $DKon       = ""; 
    $DAfdeling  = 0; 

    $Query="SELECT P3060_ref FROM tblLink WHERE LinkId='" .$p0. "'";
    if ($dbResult = pg_query($dbLink, $Query))
      if($row = pg_fetch_array($dbResult, NULL , PGSQL_ASSOC)){
        $DId = $row["p3060_ref"];
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

        $Query="SELECT (year(now()) - fodtaar) as alder, * FROM tblPersonlig WHERE PersonId=$DId";
        if ($dbResult = pg_query($dbLink, $Query))
          if ($row = pg_fetch_array($dbResult, NULL , PGSQL_ASSOC)) {
            $DKon     = $row["kon"];
            $DFodtAar = $row["fodtaar"];
            $DAlder   = $row["alder"];
          	if ($DAlder < 15)
			  $DAfdeling = 2;
          }
      }
      
      $Query="SELECT getafdelinger($LobId, $DAfdeling) AS afdeling_dropdown_html;";
      if ($dbResult = pg_query($dbLink, $Query))
        if ($row = pg_fetch_array($dbResult, NULL , PGSQL_ASSOC)) {
          $afdeling_dropdown_html     = $row["afdeling_dropdown_html"];
        }
}

if ($DoWhat == 2){
  $Query= "SELECT * from gettilmelding(" . $LobId . ", " . $FodtAar . ", '" . $Køn . "', " . $Afdeling . ");";

  $dbResult = pg_query($dbLink, $Query);
  $row = pg_fetch_array($dbResult, NULL , PGSQL_ASSOC);
  
  $DistId     = $row["distid"];
  $GrupId     = $row["grupid"];
  $Lobsafgift = $row["lobsafgift"];
  $Afdnavn    = $row["afdnavn"];
  $Dist       = $row["dist"];
  $Grup       = $row["grup"];
}

if ($DoWhat == 2){
  // Opret tblLobTilmelding
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
  if ($FodtAar){
    if ($fset){
      $set1 .= ", ";
      $set2 .= ", ";
    }
    $set1 .="FodtAar";
    $set2 .="'$FodtAar'";
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
  if ($DistId){
    if ($fset){
      $set1 .= ", ";
      $set2 .= ", ";
    }
    $set1 .="Distance";
    $set2 .="$DistId";
    $fset =1;
  }
  if ($GrupId){
    if ($fset){
      $set1 .= ", ";
      $set2 .= ", ";
    }
    $set1 .="Gruppe";
    $set2 .="$GrupId";
    $fset =1;
  }
  if ($Lobsafgift){
    if ($fset){
      $set1 .= ", ";
      $set2 .= ", ";
    }
    $set1 .="Lobsafgift";
    $set2 .="$Lobsafgift";
    $fset =1;
  }
  if ($Afdeling){
    if ($fset){
      $set1 .= ", ";
      $set2 .= ", ";
    }
    $set1 .="Afdeling";
    $set2 .="$Afdeling";
    $fset =1;
  }

  if ($fset) {
    $set1 .=", IP";
    $set2 .=", '" . $_SERVER['REMOTE_ADDR'] . "'";
    $Query=" INSERT INTO tblLobTilmelding " . $set1 . ") " . $set2 . ");";
    $dbResult = pg_query($dbLink, $Query);
    $P3060_REF = pg_insert_id($dbLink, 'tbllobtilmelding_id_seq');
  }

  $Query = "SELECT tblNrPulje.Id AS MyId FROM tblNrPulje
            WHERE tblNrPulje.NrType='online'
            AND tblNrPulje.Id  NOT IN (SELECT Nummer
   									   FROM tblNrTildeling
   									   WHERE LobId = " . $LobId . ")
            ORDER BY tblNrPulje.Id;";

  $dbResult = pg_query($dbLink, $Query);
  $row = pg_fetch_array($dbResult, NULL , PGSQL_ASSOC);
  $Nummer = $row["myid"];
  $Query = "INSERT INTO tblNrTildeling (LobId, Nummer, LobTilmeldingId) VALUES($LobId, $Nummer, $P3060_REF)";   
  $dbResult = pg_query($dbLink, $Query);

  $Subj = "Tilmelding til Espergærdeløbet";
  $Body  = 'Denne e-mail er genereret af siden "Tilmelding til Espergærdeløbet" på www.puls3060.dk';
  $Body .= "\n  Løb.........: " . $Navn;
  $Body .= "\n  Afdeling....: " . $Afdnavn;
  $Body .= "\n  Løbsdag.....: " . DatoFormat($Dato);
  $Body .= "\n  Fornavn.....: " . $Fornavn;
  $Body .= "\n  Efternavn...: " . $Efternavn;
  $Body .= "\n  Adresse.....: " . $Adresse;
  $Body .= "\n  Postnummer..: " . $Postnr;
  $Body .= "\n  By..........: " . $Bynavn;
  $Body .= "\n  Telefon.....: " . $Telefonnr;
  $Body .= "\n  E-mail......: " . $Email;
  $Body .= "\n  FødselsÅr...: " . $FodtAar;
  $Body .= "\n  Køn.........: " . $Køn;
  $Body .= "\n  Løbsnummer..: " . $Nummer;
  $Body .= "\n  Distance....: " . $DistId . " " .$Dist;
  $Body .= "\n  Gruppe......: " . $GrupId . " " . $Grup;
  $Body .= "\n  Løbsafgift..: " . $Lobsafgift;
  mail("mha@hafsjold.dk", $Subj, $Body, "From: " . $Email . "\nReply-To: " . $Email . "\nX-Mailer: PHP", "-f mha@puls3060.dk");

}
?>

<title>L&oslash;bsTilmelding</title>
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
      <p align=left><font color=#003399 size=4>Tilmelding til <?=$Navn;?> <?=DatoFormat($Dato);?></font></p>
      <form method="post" action="LobTilmelding.php?DoWhat=2" language="javascript" onsubmit="ValidatorOnSubmit();">
        <table width="100%" border="0">
          <tr> 
            <td>Afdeling:</td>
            <td> 
              <select name="Afdeling">
                <?=$afdeling_dropdown_html?>
              </select>
              <span id="Afdelingval" controltovalidate="Afdeling" errormessage="Der skal vælges en afdeling" display="Dynamic" evaluationfunction="RegularExpressionValidatorEvaluateIsValid" validationexpression="[1-9]{1,1}" style="color:Red;display:none;"> 
                der skal vælges en afdeling 
              </span>
            </td>
            <td><font size="3">&nbsp;
              </font>
			</td>
          </tr>
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
            </td>
            <td>&nbsp;</td>
          </tr>
          <tr> 
            <td>Postnummer:</td>
            <td> 
              <input name="Postnr" type="text" maxlength="4" id="Postnr" size="4" value="<?=$DPostnr?>"/>
            </td>
            <td>&nbsp;</td>
          </tr>
          <tr> 
            <td>By:</td>
            <td> 
              <input name="Bynavn" type="text" maxlength="25" id="Bynavn" size="25"  value="<?=$DBynavn?>"/>
            </td>
            <td>&nbsp;</td>
          </tr>
          <tr> 
            <td>Telefon</td>
            <td> 
              <input name="Telefonnr" type="text" maxlength="8" id="Telefonnr" size="8"  value="<?=$DTelefonnr?>"/>
            </td>
            <td>&nbsp;</td>
          </tr>
          <tr> 
            <td>E-mail:</td>
            <td> 
              <input name="Email" type="text" id="Email" size="30"  value="<?=$DEmail?>"/>
            </td>
            <td> <font size="3">(navn@abc.dk)</font></td>
          </tr>
          <tr> 
            <td>F&oslash;dsels &Aring;r:</td>
            <td> 
              <input name="FodtAar" type="text" maxlength="4" id="FodtAar" size="4"  value="<?=$DFodtAar?>"/>
              <span id="FodtAarval1" controltovalidate="FodtAar" errormessage="FødelsÅr skal udfyldes" display="Dynamic" evaluationfunction="RequiredFieldValidatorEvaluateIsValid" initialvalue="" style="color:Red;display:none;"> 
              skal udfyldes </span> <span id="FodtAarval2" controltovalidate="FodtAar" errormessage="FødelsÅr skal være mellem 1000 og 9900" display="Dynamic" evaluationfunction="RegularExpressionValidatorEvaluateIsValid" validationexpression="[1-2]{1}[09]{1}[0-9]{2}" style="color:Red;display:none;"> 
              skal være et &aring;rstal med 4 cifre </span> <span id="FodtAarval4" controltovalidate="FodtAar" errormessage="Alder skal være mellem 2 og 99 år" display="Dynamic" evaluationfunction="CustomValidatorEvaluateIsValid" clientvalidationfunction="CheckAlder" style="color:Red;display:none;"> 
              Alder skal være mellem 2 og 99 &aring;r </span> </td>
            <td><font size="3">(&aring;&aring;&aring;&aring;)</font></td>
          </tr>
          <tr> 
            <td>K&oslash;n:</td>
            <td> 
              <input name="Køn" type="text" maxlength="1" id="Køn" size="1"  value="<?=$DKon?>"/>
              <span id="Kønval1" controltovalidate="Køn" errormessage="Køn skal udfyldes" display="Dynamic" evaluationfunction="RequiredFieldValidatorEvaluateIsValid" initialvalue="" style="color:Red;display:none;"> 
              skal udfyldes </span> <span id="Kønval2" controltovalidate="Køn" errormessage="Køn skal udfyldes med m eller k" display="Dynamic" evaluationfunction="RegularExpressionValidatorEvaluateIsValid" validationexpression="[mk]{1,1}" style="color:Red;display:none;"> 
              skal udfyldes med m eller k </span> </td>
            <td><font size="3">(m: mand, k: kvinde)</font></td>
          </tr>
          <tr valign="bottom"> 
            <td height="50">&nbsp;</td>
            <td colspan="2" height="50"> 
              <input type="submit" value="Tilmelding" />
          </tr>
        </table>
        <span id="Message"></span> 
        <div id="valsum1" headertext="Følgende fejl skal rettes i formularen ovenfor:" displaymode="List" style="color:Red;display:none;"> 
        </div>
        <script language="javascript">
  <!--
  var Page_ValidationSummaries =  new Array(document.all["valsum1"]);
  var Page_Validators =  new Array(
  	document.all["Afdelingval"],
  	document.all["Fornavnval"], 
  	document.all["Efternavnval"], 
  	document.all["FodtAarval1"], 
  	document.all["FodtAarval2"], 
  	document.all["FodtAarval4"], 
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
      day = 1;
      month = 1;
      year = value;
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
      <p align=left><font color=#003399 size=4>Tilmelding til Esperg&aelig;rdel&oslash;bet</font></p>
      <p> 
        <?= $Fornavn;?>	er nu tilmeldt <?=$Navn;?> <?=DatoFormat($Dato);?>.
      </p>
      <? } // End If $DoWhat == 2 ?>
      </td>
    <td width="30">&nbsp;</td>
  </tr>
  <tr> 
    <td width="30">&nbsp;</td>
    <td align="right"><br>
      <font color=#a03952 face="Arial, Helvetica, sans-serif" size=2><i><b></b></i></font> <br>
      <br>
    </td>
    <td width="30">&nbsp;</td>
  </tr>
</table>
</body>
</html>
