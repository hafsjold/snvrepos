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
  <form method="post" action="{$SCRIPT_NAME}?DoWhat=3">
    <table width="100%" border="0">
      <tr> 
        <td width="20%">Fornavn:</td>
        <td>{$medlem->fornavn}</td>
      </tr>
      <tr> 
        <td>Efternavn:</td>
        <td>{$medlem->efternavn}</td>
      </tr>
      <tr> 
        <td>Adresse:</td>
        <td>{$medlem->adresse}</td>
      </tr>
      <tr> 
        <td>Postnummer:</td>
        <td>{$medlem->postnr}</td>
      </tr>
      <tr> 
        <td>By:</td>
        <td>{$medlem->bynavn}</td>
      </tr>
      <tr> 
        <td>Telefon</td>
        <td>{$medlem->tlfnr}</td>
      </tr>
      <tr> 
        <td>E-mail:</td>
        <td>{$medlem->mailadr}</td>
      </tr>
      <tr> 
        <td>F&oslash;dselsdato:</td>
        <td>{$medlem->fodtdato}</td>
      </tr>
      <tr> 
        <td>K&oslash;n:</td>
        <td>{$medlem->kon}</td>
      </tr>
      <tr valign="bottom"> 
        <td height="50">&nbsp;</td>
        <td height="50"> 
            <input type="submit" value="Indmeldelse" />
      	</td>
      </tr>
    </table>
    <p>Efter du har udfyldt ovenst&aring;ende formular og trykket p&aring; 
      &quot;<b>Indmeldelse</b>&quot; vil du f&aring; tilsendt en e-mail med 
      oplysning om betaling af kontingent.</p>
  </form>
</div>
</body>
</html>
