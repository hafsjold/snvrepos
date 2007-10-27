<html>
<head>
<link href="/css/puls3060.css" rel="stylesheet" type="text/css">
<title>L&oslash;bsTilmelding</title>
  <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
</head>
<body bgcolor="#FFFFFF" text="#000000">
{include file="Kalender/AktUserTilmeldingHeading2.tpl" step="4"}
<div class="pagepuls3060">
  <h3>{$tilmelding->aktnavn} {$tilmelding->aktdato|date_format:"%A den %e. %B kl %H.%M"}</h3>
  <p>
    {$tilmelding->fornavn} tak for din tilmelding til 
    {$tilmelding->aktnavn} {$tilmelding->aktdato|date_format:"%A den %e. %B"}.
    Vi har registreret følgende oplysninger i forbindelse med din tilmelding:
  </p>
  <div class="clslvl1">
    <div class="clslvl2L"> L&oslash;b: </div>
    <div class="clslvl2M">
      {$tilmelding->aktnavn}
    </div>
  </div>
  <div class="clslvl1">
    <div class="clslvl2L"> L&oslash;bsdag: </div>
    <div class="clslvl2M">
      {$tilmelding->aktdato|date_format:"%A den %e. %B %G kl %H.%M"}
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
  <p> Der er ogs&aring; sendt en e-mail til {$tilmelding->mailadr} med en kopi 
    af ovenst&aring;ende oplysning. </p>
</div>
</body>
</html>
