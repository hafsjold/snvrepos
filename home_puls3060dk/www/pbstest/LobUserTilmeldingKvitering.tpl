<html>
<head>
<link href="/css/puls3060.css" rel="stylesheet" type="text/css">
<title>L&oslash;bsTilmelding</title>
  <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
</head>
<body bgcolor="#FFFFFF" text="#000000">
{include file="pbstest/LobUserTilmeldingHeading.tpl" step="5"}
<div class="pagepuls3060">
  <h3>{$tilmelding->lobnavn} {$tilmelding->lobdato|date_format:"%A den %e. %B kl %H.%M"}</h3>
  <p>
    {$tilmelding->fornavn} tak for din tilmelding til 
    {$tilmelding->lobnavn} {$tilmelding->lobdato|date_format:"%A den %e. %B"}.
    Vi har registreret følgende oplysninger i forbindelse med din tilmelding:
  </p>
  <div class="clslvl1">
    <div class="clslvl2L"> L&oslash;b: </div>
    <div class="clslvl2M">
      {$tilmelding->lobnavn}
    </div>
  </div>
  <div class="clslvl1">
    <div class="clslvl2L">
      Afdeling:
    </div>
    <div class="clslvl2M">
      {$tilmelding->afdnavn}
    </div>
  </div>
  <div class="clslvl1">
    <div class="clslvl2L"> L&oslash;bsdag: </div>
    <div class="clslvl2M">
      {$tilmelding->lobdato|date_format:"%A den %e. %B %G kl %H.%M"}
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
    <div class="clslvl2L"> K&oslash;n: </div>
    <div class="clslvl2M">
      {$tilmelding->kon}
    </div>
  </div>
  <div class="clslvl1">
    <div class="clslvl2L"> L&oslash;bsnr.: </div>
    <div class="clslvl2M">
      {$tilmelding->nummer}
    </div>
  </div>
  <div class="clslvl1">
    <div class="clslvl2L"> L&oslash;bsafgift: </div>
    <div class="clslvl2M">
      <p>{$tilmelding->lobsafgift|string_format:"%d DKK"}</p>
    </div>
  </div>
  <div class="clslvl1">
    <div class="clslvl2L">
      Ordrenummer:
    </div>
    <div class="clslvl2M">
      {$tilmelding->ordernum}
    </div>
  </div>
  <p> {$tilmelding->afhente_dit_loebsnummer} L&oslash;bsafgiften p&aring; 
    {$tilmelding->lobsafgift|string_format:"%d DKK"} vil blive h&aelig;vet p&aring; dit Dankort dagen 
    efter l&oslash;bet. Hvis du mod forventning skulle blive forhindret i at deltage 
    i l&oslash;bet vil vi ikke h&aelig;ve l&oslash;bsafgiften fra dit Dankort, og 
    du har derfor ikke haft nogen udgift i forbindelse med denne tilmelding. </p>
  <p> Der er ogs&aring; sendt en e-mail til {$tilmelding->mailadr} med en kopi 
    af ovenst&aring;ende oplysning. </p>
</div>
</body>
</html>
