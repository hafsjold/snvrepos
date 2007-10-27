<html>
<head>
<link href="/css/puls3060.css" rel="stylesheet" type="text/css">
<title>L&oslash;bsTilmelding</title>
  <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
</head>
<body bgcolor="#FFFFFF" text="#000000">
{include file="Lob/LobAdmTilmeldingHeading.tpl" step="3"}
<div class="pagepuls3060" id="pagepuls3060">
  <h3>Tilmelding til {$tilmelding->lobnavn}</h3>
  <p><font size="4" color="#FF0000">Der er opstået en fejl.</font> 
     {$tilmelding->fornavn} {$tilmelding->efternavn} er ikke tilmeldt {$tilmelding->lobnavn}.</p>
  <form method="post" action="{$SCRIPT_NAME}?DoWhat=0">
    <div class="clslvl1">
      <div class="clslvl2L">
      </div>
      <div class="clslvl2M">
        <input type="submit" Name="cmdOk" value="Næste Tilmelding" />
      </div>
    </div>
  </form>
  </div>
</body>
</html>
