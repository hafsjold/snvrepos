<html>
<head>
<link href="/css/puls3060.css" rel="stylesheet" type="text/css">
<title>L&oslash;bsTilmelding</title>
	<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
</head>
<body bgcolor="#FFFFFF" text="#000000">	
{include file="Kalender/AktUserTilmeldingHeading2.tpl" step="2"}
<div class="pagepuls3060">
  <h3>{$tilmelding->aktnavn} {$tilmelding->aktdato|date_format:"%A den %e. %B kl %H.%M"}</h3>
  <p> Udfyld nedenst&aring;ende og trykke p&aring; &quot;N&aelig;ste side&quot;.</p>

  <form method="post" action="{$SCRIPT_NAME}?DoWhat=22">
  <div class="clslvl1">
    <div class="clslvl2L">
      Fornavn:
    </div>
    <div class="clslvl2M">
      <input name="fornavn" type="text" maxlength="25" id="fornavn" size="25" value="{$tilmelding->fornavn}"/>
    </div>
    {if $tilmelding->fornavn_error_display}
      <div class="clslvl2E">
        {$tilmelding->fornavn_error}
      </div>
    {/if}
  </div>

  <div class="clslvl1">
    <div class="clslvl2L">
      Efternavn:
    </div>
    <div class="clslvl2M">
      <input name="efternavn" type="text" maxlength="25" id="efternavn" size="25" value="{$tilmelding->efternavn}"/>
    </div>
    {if $tilmelding->efternavn_error_display}
      <div class="clslvl2E">
        {$tilmelding->efternavn_error}
      </div>
    {/if}
  </div>

  <div class="clslvl1">
    <div class="clslvl2L">
      Adresse:
    </div>
    <div class="clslvl2M">
      <input name="adresse" type="text" maxlength="35" id="adresse" size="35" value="{$tilmelding->adresse}"/>
    </div>
    {if $tilmelding->adresse_error_display}
      <div class="clslvl2E">
        {$tilmelding->adresse_error}
      </div>
    {/if}
  </div>

  <div class="clslvl1">
    <div class="clslvl2L">
      Postnummer:
    </div>
    <div class="clslvl2M">
      <input name="postnr" type="text" maxlength="4" id="postnr" size="4" value="{$tilmelding->postnr}"/>
    </div>
    {if $tilmelding->postnr_error_display}
      <div class="clslvl2E">
        {$tilmelding->postnr_error}
      </div>
    {/if}
  </div>

  <div class="clslvl1">
    <div class="clslvl2L">
      By:
    </div>
    <div class="clslvl2M">
      <input name="bynavn" type="text" maxlength="25" id="bynavn" size="25"  value="{$tilmelding->bynavn}"/>
    </div>
    {if $tilmelding->bynavn_error_display}
      <div class="clslvl2E">
        {$tilmelding->bynavn_error}
      </div>
    {/if}
  </div>

  <div class="clslvl1">
    <div class="clslvl2L">
      Telefon:
    </div>
    <div class="clslvl2M">
      <input name="tlfnr" type="text" maxlength="8" id="tlfnr" size="8"  value="{$tilmelding->tlfnr}"/>
    </div>
    {if $tilmelding->tlfnr_error_display}
      <div class="clslvl2E">
        {$tilmelding->tlfnr_error}
      </div>
    {/if}
  </div>

  <div class="clslvl1">
    <div class="clslvl2L">
      E-mail:
    </div>
    <div class="clslvl2M">
      <input name="mailadr" type="text" id="mailadr" size="30"  value="{$tilmelding->mailadr}"/>
    </div>
    <div class="clslvl2R">
      (navn@abc.dk)
    </div>
    {if $tilmelding->mailadr_error_display}
      <div class="clslvl2E">
        {$tilmelding->mailadr_error}
      </div>
    {/if}
  </div>
  <br/>
  <div class="clslvl1">
    <div class="clslvl2L">
    </div>
    <div class="clslvl2M">
      <input type="submit" Name="cmdOk" value="Næste side" />
    </div>
    <div class="clslvl2R">
      <input type="submit" Name="cmdBack" value="Tilbage" />
    </div>
  </div>
  </form>
</div>
</body>
</html>
