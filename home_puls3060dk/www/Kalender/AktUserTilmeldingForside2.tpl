<html>
<head>
<link href="/css/puls3060.css" rel="stylesheet" type="text/css">
<title>L&oslash;bsTilmelding</title>
	<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
</head>
<body bgcolor="#FFFFFF" text="#000000">	
{include file="Kalender/AktUserTilmeldingHeading2.tpl" step="1"}
<div class="pagepuls3060">
  <h3>{$tilmelding->aktnavn} {$tilmelding->aktdato|date_format:"%A den %e. %B kl %H.%M"}</h3>
  <p> Du kan tilmelde dig {$tilmelding->aktnavn} ved at udfylde sk&aelig;rmbillederne 
    p&aring; de n&aelig;ste sider.</p>
  <p>Du skal have f&oslash;lgende oplysninger parat:<br>
    - navn og adresse<br>
    - telefon nummer<br>
    - e-mail adresse<br>
  </p>
  <form method="post" action="{$SCRIPT_NAME}?DoWhat=21">
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
