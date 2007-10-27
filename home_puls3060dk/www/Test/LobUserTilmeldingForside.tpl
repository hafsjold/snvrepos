<html>
<head>
<link href="/css/puls3060.css" rel="stylesheet" type="text/css">
<title>L&oslash;bsTilmelding</title>
	<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
</head>
<body bgcolor="#FFFFFF" text="#000000">	
{include file="Test/LobUserTilmeldingHeading.tpl" step="1"}
<div class="pagepuls3060">
  <h3>{$tilmelding->lobnavn} {$tilmelding->lobdato|date_format:"%A den %e. %B kl %H.%M"}</h3>
  <p> Du kan tilmelde dig {$tilmelding->lobnavn} ved at udfylde sk&aelig;rmbillederne 
    p&aring; de n&aelig;ste sider.</p>
  <p>Du skal have f&oslash;lgende oplysninger parat:<br>
    - navn og adresse<br>
    - telefon nummer<br>
    - e-mail adresse<br>
    - f&oslash;dsels&aring;r og k&oslash;n </p>
  <p>Du skal ogs&aring; bruge et Dankort til betaling af l&oslash;bsafgiften.</p>
  <p>L&oslash;bsafgifter er 25 kr. <br>
    Hvis du er under 15 &aring;r (f&oslash;dt i 1992 eller senere) er l&oslash;bsafgifte 
    10 kr.<br>
  </p>
  <form method="post" action="{$SCRIPT_NAME}?DoWhat=1">
    <div class="clslvl1">
      <div class="clslvl2L">
      </div>
      <div class="clslvl2M">
        <input type="submit" Name="cmdOk" value="Næste side" />
      </div>
    </div>
  </form>
</div>
</body>
</html>
