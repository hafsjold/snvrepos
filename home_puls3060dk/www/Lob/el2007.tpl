<html>
<head>
<link href="/css/puls3060.css" rel="stylesheet" type="text/css">
<title>Esperg&aelig;rdel&oslash;b</title>

<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">

</head>


<body bgcolor="#FFFFFF" text="#000000">
<div class="pagepuls3060" id="pagepuls3060">
  <h3>Esperg&aelig;rdel&oslash;bet  {$smarty.now|date_format:"%Y"}</h3>
  <p><b>L&oslash;bsdatoer</b> 
  <table width="100%" border="0" cellspacing="0" cellpadding="0">
    {while $lobliste->nextLobList()}
      <tr> 
        <td width="2%">&nbsp;</td>
        <td width="30%">{$lobliste->dato|date_format:"%A den %e. %B"}</td>
        <td>{$lobliste->dato|date_format:"kl %H.%M"}</td>
      </tr>
    {/while}

  </table>
  <p><b>Distancer</b> 
  <table width="100%" border="0" cellspacing="0" cellpadding="0">
    <tr> 
      <td width="2%">&nbsp;</td>
      <td>5 km</td>
    </tr>
    <tr> 
      <td width="2%">&nbsp;</td>
      <td>10 km</td>
    </tr>
    <tr> 
      <td width="2%">&nbsp;</td>
      <td>5 km gang</td>
    </tr>
  </table>
{if $lobliste2->nextLobList()}
  <p><b>Priser</b> 
  <table width="100%" border="0" cellspacing="0" cellpadding="0">
    <tr> 
      <td width="2%">&nbsp;</td>
      <td width="20%">Voksne</td>
      <td>{$lobliste2->lobsafgift|string_format:"%d"},- kr</td>
    </tr>
    <tr> 
      <td width="2%">&nbsp;</td>
      <td width="20%">Unge indtil 15 &aring;r</td>
      <td>{$lobliste2->lobsafgiftunge|string_format:"%d"},- kr</td>
    </tr>
  </table>
  <p><b>Tilmelding p&aring; l&oslash;bsdagen</b>
  <table width="100%" border="0" cellspacing="0" cellpadding="0">
    <tr> 
      <td width="2%">&nbsp;</td>
      <td>Kl. 9.15 - 9.45 ved start-stedet i Egeb&aelig;ks Vang skov</td>
    </tr>
  </table>
  <p><b></b><b>Elektronisk tilmelding</b> 
  <table width="100%" border="0" cellspacing="0" cellpadding="0">
    <tr> 
      <td width="2%">&nbsp;</td>
      <td>Du kan tilmelde dig elektronisk ved at tryk p&aring; &quot;<a href="/lobmnu/Lob/LobUserTilmelding.php" onClick="return top.rwlink(href);">L&oslash;bs 
        tilmelding</a>&quot; ude til venstre og der udfyld de oplysninger 
        vi skal bruge i forbindelse med tilmeldingen. </td>
    </tr>
  </table>
  <p><b>Afhentning af l&oslash;bsnumre</b>
  <table width="100%" border="0" cellspacing="0" cellpadding="0">
    <tr> 
      <td width="2%">&nbsp;</td>
      <td>
        <p>Hvis du har tilmeldt dig elektronisk kan du betale og afhente dit 
          l&oslash;bsnummer ved Start-stedet i Egeb&aelig;ks Vang skov mellem 
          kl 9.15 og 9.45</p>
        </td>
    </tr>
  </table>
  <p><b>Start</b> 
  <table width="100%" border="0" cellspacing="0" cellpadding="0">
    <tr> 
      <td width="2%">&nbsp;</td>
      <td>Kl. 10.00 ved indgangen til Egeb&aelig;ks Vang skov p&aring; M&oslash;rdrupvej 
        over for Gammel Skovvej.</td>
    </tr>
  </table>
  <p><b>Arrang&oslash;r</b> 
  <table width="100%" border="0" cellspacing="0" cellpadding="0">
    <tr> 
      <td width="2%">&nbsp;</td>
      <td>Motionsl&oslash;beklubben Puls 3060.</td>
    </tr>
  </table>
  <p><b>Forplejning</b> 
  <table width="100%" border="0" cellspacing="0" cellpadding="0">
    <tr> 
      <td width="2%">&nbsp;</td>
      <td>
        <p>Efter l&oslash;bet er der vand. <br>
          Der vil normalt ogs&aring; v&aelig;re mulighed for at k&oslash;be 
          en forfriskning.</p>
        </td>
    </tr>
  </table>
  <p><b>Ruten</b> 
  <table width="100%" border="0" cellspacing="0" cellpadding="0">
    <tr> 
      <td width="2%">&nbsp;</td>
      <td>En tur p&aring; 5 km rundt i den dejlige Egeb&aelig;ks Vang skov.</td>
    </tr>
  </table>
  <p><b>L&oslash;bsklasser</b>
  <table width="100%" border="0" cellspacing="0" cellpadding="0">
    <tr> 
      <td width="2%">&nbsp;</td>
      <td colspan="2">Der l&oslash;bes i 5 aldersklasser p&aring; distancen 
        5 og 10km for m&aelig;nd / kvinder</td>
    </tr>
    <tr> 
      <td width="2%">&nbsp;</td>
      <td width="2%">&nbsp;</td>
      <td>Op til 11 &aring;r</td>
    </tr>
    <tr> 
      <td width="2%">&nbsp;</td>
      <td width="2%">&nbsp;</td>
      <td>12 - 15 &aring;r</td>
    </tr>
    <tr> 
      <td width="2%">&nbsp;</td>
      <td width="2%">&nbsp;</td>
      <td>16 - 39 &aring;r</td>
    </tr>
    <tr> 
      <td width="2%">&nbsp;</td>
      <td width="2%">&nbsp;</td>
      <td>40 - 49 &aring;r</td>
    </tr>
    <tr> 
      <td width="2%">&nbsp;</td>
      <td width="2%">&nbsp;</td>
      <td>50 &aring;r og op</td>
    </tr>
  </table>
  <p><b>Resultatliste</b> 
  <table width="100%" border="0" cellspacing="0" cellpadding="0">
    <tr> 
      <td width="2%">&nbsp;</td>
      <td colspan="2">Resultatet af l&oslash;bet offentlig&oslash;res p&aring; 
        f&oslash;lgende m&aring;de </td>
    </tr>
    <tr> 
      <td width="2%">&nbsp;</td>
      <td width="2%">&nbsp;</td>
      <td>- Her p&aring; Puls 3060's hjemmeside www.puls3060.dk</td>
    </tr>
    <tr> 
      <td width="2%">&nbsp;</td>
      <td width="2%">&nbsp;</td>
      <td>- P&aring; opslagstavlen i Esperg&aelig;rde Hallen</td>
    </tr>
    <tr> 
      <td width="2%">&nbsp;</td>
      <td width="2%">&nbsp;</td>
      <td>- Resultatet vil ogs&aring; blive tilsendt Helsing&oslash;r Dagblad 
      </td>
    </tr>
  </table>
  <table width="100%" border="0" cellspacing="0" cellpadding="0">
  </table>
{/if}
</div>
</body>
</html>

