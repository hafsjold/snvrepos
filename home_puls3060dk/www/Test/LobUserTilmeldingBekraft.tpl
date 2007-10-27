<html>
<head>
<link href="/css/puls3060.css" rel="stylesheet" type="text/css">
<title>L&oslash;bsTilmelding</title>
	<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
</head>
<body bgcolor="#FFFFFF" text="#000000">	
{include file="Test/LobUserTilmeldingHeading.tpl" step="4"}
<div class="pagepuls3060" id="pagepuls3060">
  <h3>{$tilmelding->lobnavn} {$tilmelding->lobdato|date_format:"%A den %e. %B kl %H.%M"}</h3>
  <p>Kontroller at nedenst&aring;ende oplysninger og tryk p&aring; knappen &quot;Godkend&quot; 
    for at tilmelde dig.</p>
  <form method="post" action="{$SCRIPT_NAME}?DoWhat=4">
    <div class="clslvl1">
      <div class="clslvl2L">
        Afdeling:
      </div>
      <div class="clslvl2M">
        {$tilmelding->afdnavn}
      </div>
    </div>
    <div class="clslvl1">
      <div class="clslvl2L">
        Fornavn:
      </div>
      <div class="clslvl2M">
        {$tilmelding->fornavn}
      </div>
    </div>
    <div class="clslvl1">
      <div class="clslvl2L">
        Efternavn:
      </div>
      <div class="clslvl2M">
        {$tilmelding->efternavn} 
      </div>
    </div>
    <div class="clslvl1">
      <div class="clslvl2L">
        Adresse:
      </div>
      <div class="clslvl2M">
        {$tilmelding->adresse}
      </div>
    </div>
    <div class="clslvl1">
      <div class="clslvl2L">
        Postnummer:
      </div>
      <div class="clslvl2M">
        {$tilmelding->postnr}
      </div>
    </div>
    <div class="clslvl1">
      <div class="clslvl2L">
        By:
      </div>
      <div class="clslvl2M">
        {$tilmelding->bynavn}
      </div>
    </div>
    <div class="clslvl1">
      <div class="clslvl2L">
        Telefon
      </div>
      <div class="clslvl2M">
        {$tilmelding->tlfnr}
      </div>
    </div>
    <div class="clslvl1">
      <div class="clslvl2L">
        E-mail:
      </div>
      <div class="clslvl2M">
        {$tilmelding->mailadr}
      </div>
    </div>
    <div class="clslvl1">
      <div class="clslvl2L">
        F&oslash;dsels &Aring;r:
      </div>
      <div class="clslvl2M">
        {$tilmelding->fodtaar}
      </div>
    </div>
    <div class="clslvl1">
      <div class="clslvl2L">
        K&oslash;n:
      </div>
      <div class="clslvl2M">
        {$tilmelding->kon}
      </div>
    </div>
    <br/>
    <div class="clslvl1">
      <div class="clslvl2L">
        L&oslash;bsafgift:
      </div>
      <div class="clslvl2M">
        {$tilmelding->lobsafgift}
      </div>
    </div>
    <div class="clslvl1">
      <div class="clslvl2L">
        Kortnummer:
      </div>
      <div class="clslvl2M">
        {$tilmelding->cardnumber}
      </div>
    </div>
    <div class="clslvl1">
      <div class="clslvl2L">
        Udl&oslash;bsdato:
      </div>
      <div class="clslvl2M">
        {$tilmelding->expirationdate}
      </div>
    </div>
    <div class="clslvl1">
      <div class="clslvl2L">
        Kontrolcifre:
      </div>
      <div class="clslvl2M">
        {$tilmelding->cvd}
      </div>
    </div>

	<br/>

    <div class="clslvl1">
      <div class="clslvl2L">
      </div>
      <div class="clslvl2M">
        <input type="submit" Name="cmdOk" value="Godkend" />
      </div>
      <div class="clslvl2R">
        <input type="submit" Name="cmdBack" value="Tilbage" />
        <input type="submit" Name="cmdCancel" value="Fortryd" />
      </div>
    </div>
  </form>

  {if $tilmelding->page_error_display}
  <p><font color="#FF0000">
    {$tilmelding->page_error}
  </font></p>
  {/if}



</div>
</body>
</html>
