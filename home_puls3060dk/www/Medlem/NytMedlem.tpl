<html>
<head>
<link href="/css/puls3060.css" rel="stylesheet" type="text/css">
<title>Indmeldelse</title>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
</head>
<body bgcolor="#FFFFFF" text="#000000">
<div class="pagepuls3060" id="pagepuls3060">
  <h3>Indmeldelse</h3>
  <p> Du kan melde dig ind i L&oslash;beklubben Puls 3060 ved at udfylde formularen 
    p&aring; denne side og trykke p&aring; knappen &quot;Indmeldelse&quot;. 
    Kontingentet er kr. 150 pr. &aring;r.</p>
  <form method="post" action="{$SCRIPT_NAME}?DoWhat=2" onsubmit="ValidatorOnSubmit();">
    <table width="100%" border="0">
      <tr> 
        <td>Fornavn:</td>
        <td> 
          <input name="fornavn" type="text" maxlength="25" id="fornavn" size="25" value="{$medlem->fornavn}"/>
          <span id="fornavn_val_0" style="color:Red;">{$medlem->fornavn_error}</span> 
          <span id="fornavn_val_1" controltovalidate="fornavn" errormessage="Fornavn skal udfyldes" display="Dynamic" evaluationfunction="RequiredFieldValidatorEvaluateIsValid" initialvalue="" style="color:Red;display:none;"> 
            skal udfyldes 
          </span> 
        </td>
        <td>&nbsp;</td>
      </tr>
      <tr> 
        <td>Efternavn:</td>
        <td> 
          <input name="efternavn" type="text" maxlength="25" id="efternavn" size="25" value="{$medlem->efternavn}"/>
          <span id="efternavn_val_0" style="color:Red;">{$medlem->efternavn_error}</span> 
          <span id="efternavn_val_1" controltovalidate="efternavn" errormessage="Efternavn skal udfyldes" display="Dynamic" evaluationfunction="RequiredFieldValidatorEvaluateIsValid" initialvalue="" style="color:Red;display:none;"> 
            skal udfyldes 
          </span> 
        </td>
        <td>&nbsp;</td>
      </tr>
      <tr> 
        <td>Adresse:</td>
        <td> 
          <input name="adresse" type="text" maxlength="35" id="adresse" size="35" value="{$medlem->adresse}"/>
          <span id="adresse_val_0" style="color:Red;">{$medlem->adresse_error}</span> 
          <span id="adresse_val_1" controltovalidate="adresse" errormessage="Adresse skal udfyldes" display="Dynamic" evaluationfunction="RequiredFieldValidatorEvaluateIsValid" initialvalue="" style="color:Red;display:none;"> 
            skal udfyldes 
          </span> 
        </td>
        <td>&nbsp;</td>
      </tr>
      <tr> 
        <td>Postnummer:</td>
        <td> 
          <input name="postnr" type="text" maxlength="4" id="postnr" size="4" value="{$medlem->postnr}"/>
          <span id="postnr_val_0" style="color:Red;">{$medlem->postnr_error}</span> 
          <span id="postnr_val_1" controltovalidate="postnr" errormessage="Postnr skal udfyldes" display="Dynamic" evaluationfunction="RequiredFieldValidatorEvaluateIsValid" initialvalue="" style="color:Red;display:none;"> 
            skal udfyldes 
          </span> 
          <span id="postnr_val_2" controltovalidate="postnr" errormessage="Postnr skal være mellem 1000 og 9900" display="Dynamic" evaluationfunction="RegularExpressionValidatorEvaluateIsValid" validationexpression="{$medlem->postnr_validationexpression}" style="color:Red;display:none;"> 
            skal være mellem 1000 og 9900 
          </span> 
        </td>
        <td>&nbsp;</td>
      </tr>
      <tr> 
        <td>By:</td>
        <td> 
          <input name="bynavn" type="text" maxlength="25" id="bynavn" size="25" value="{$medlem->bynavn}"/>
          <span id="bynavn_val_0" style="color:Red;">{$medlem->bynavn_error}</span> 
          <span id="bynavn_val_1" controltovalidate="bynavn" errormessage="By skal udfyldes" display="Dynamic" evaluationfunction="RequiredFieldValidatorEvaluateIsValid" initialvalue="" style="color:Red;display:none;"> 
            skal udfyldes 
          </span> 
        </td>
        <td>&nbsp;</td>
      </tr>
      <tr> 
        <td>Telefon</td>
        <td> 
          <input name="tlfnr" type="text" maxlength="8" id="tlfnr" size="8" value="{$medlem->tlfnr}"/>
          <span id="tlfnr_val_0" style="color:Red;">{$medlem->tlfnr_error}</span> 
          <span id="tlfnr_val_1" controltovalidate="tlfnr" errormessage="Telefonnr skal udfyldes" display="Dynamic" evaluationfunction="RequiredFieldValidatorEvaluateIsValid" initialvalue="" style="color:Red;display:none;"> 
            skal udfyldes 
          </span> 
          <span id="tlfnr_val_2" controltovalidate="tlfnr" errormessage="Telefonnr skal være mellem 10000000 og 99999999" display="Dynamic" evaluationfunction="RegularExpressionValidatorEvaluateIsValid" validationexpression="{$medlem->tlfnr_validationexpression}" style="color:Red;display:none;"> 
            skal være mellem 10000000 og 99999999 
          </span> 
        </td>
        <td>&nbsp;</td>
      </tr>
      <tr> 
        <td>E-mail:</td>
        <td> 
          <input name="mailadr" type="text" id="mailadr" size="30" value="{$medlem->mailadr}"/>
          <span id="mailadr_val_0" style="color:Red;">{$medlem->mailadr_error}</span> 
          <span id="mailadr_val_1" controltovalidate="mailadr" errormessage="Email skal udfyldes" display="Dynamic" evaluationfunction="RequiredFieldValidatorEvaluateIsValid" initialvalue="" style="color:Red;display:none;"> 
            skal udfyldes 
          </span> 
          <span id="mailadr_val_2" controltovalidate="mailadr" errormessage="Email skal udfyldes korrekt" display="Dynamic" evaluationfunction="RegularExpressionValidatorEvaluateIsValid" validationexpression="{$medlem->mailadr_validationexpression}" style="color:Red;display:none;"> 
            skal udfyldes korrekt 
          </span> 
        </td>
        <td>(navn@abc.dk)</td>
      </tr>
      <tr> 
        <td>F&oslash;dselsdato:</td>
        <td> 
          <input name="fodtdato" type="text" maxlength="10" id="fodtdato" size="10" value="{$medlem->fodtdato}"/>
          <span id="fodtdato_val_0" style="color:Red;">{$medlem->fodtdato_error}</span> 
          <span id="fodtdato_val_1" controltovalidate="fodtdato" errormessage="Fødelsdato skal udfyldes" display="Dynamic" evaluationfunction="RequiredFieldValidatorEvaluateIsValid" initialvalue="" style="color:Red;display:none;"> 
            skal udfyldes 
          </span> 
          <span id="fodtdato_val_2" controltovalidate="fodtdato" errormessage="Fødelsdato skilletegn skal være -" display="Dynamic" evaluationfunction="RegularExpressionValidatorEvaluateIsValid" validationexpression="{$medlem->fodtdato_validationexpression}" style="color:Red;display:none;"> 
            skilletegn skal være - 
          </span> 
          <span id="fodtdato_val_3" controltovalidate="fodtdato" errormessage="Fødelsdato skal udfyldes med en dato" display="Dynamic" evaluationfunction="CompareValidatorEvaluateIsValid" operator="DataTypeCheck" type="Date" dateorder="dmy" style="color:Red;display:none;"> 
            skal udfyldes med en dato 
          </span> 
          <span id="fodtdato_val_4" controltovalidate="fodtdato" errormessage="Alder skal være mellem 2 og 99 år" display="Dynamic" evaluationfunction="CustomValidatorEvaluateIsValid" clientvalidationfunction="CheckAlder" style="color:Red;display:none;"> 
            Alder skal være mellem 2 og 99 år 
          </span> 
        </td>
        <td>(dd-mm-&aring;&aring;&aring;&aring;)</td>
      </tr>
      <tr> 
        <td>K&oslash;n:</td>
        <td> 
          <input name="kon" type="text" maxlength="1" id="kon" size="1" value="{$medlem->kon}"/>
          <span id="kon_val_0" style="color:Red;">{$medlem->kon_error}</span> 
          <span id="kon_val_1" controltovalidate="kon" errormessage="Køn skal udfyldes" display="Dynamic" evaluationfunction="RequiredFieldValidatorEvaluateIsValid" initialvalue="" style="color:Red;display:none;"> 
            skal udfyldes 
          </span> 
          <span id="kon_val_2" controltovalidate="kon" errormessage="Køn skal udfyldes med m eller k" display="Dynamic" evaluationfunction="RegularExpressionValidatorEvaluateIsValid" validationexpression="{$medlem->kon_validationexpression}" style="color:Red;display:none;"> 
            skal udfyldes med m eller k 
          </span> 
        </td>
        <td>(m: mand, k: kvinde)</td>
      </tr>
      <tr valign="bottom"> 
        <td height="50">&nbsp;</td>
        <td colspan="2" height="50"> 
            <input type="submit" value="Indmeldelse" />
      	</td>
      </tr>
    </table>
    <span id="Message"></span> 
    <div id="valsum1" headertext="Følgende fejl skal rettes i formularen ovenfor:" displaymode="List" style="color:Red;display:none;"> 
    </div>
    <p>Efter du har udfyldt ovenst&aring;ende formular og trykket p&aring; 
      &quot;<b>Indmeldelse</b>&quot; vil du f&aring; tilsendt en e-mail med 
      oplysning om betaling af kontingent.</p>
  </form>
</div>
</body>
</html>


<script language="javascript"    
  src="../Scripts/WebUIValidation.js">
</script>
<script language="javascript"
  src="NytMedlem.js">
</script>
