<html>
<head>
<title>For&aring;rsfest 2006</title>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
</head>
<body text="#000000" leftmargin="0" topmargin="0" marginwidth="0" marginheight="0">
<table bgcolor="#FFFFFF" width="100%" border="0" cellspacing="0" cellpadding="0" dwcopytype="CopyTableRow">
  <tr> 
    <td width="30">&nbsp;</td>
    <td>&nbsp;</td>
    <td width="30">&nbsp;</td>
  </tr>
  <tr> 
    <td width="30">&nbsp;</td>
    <td> 
      <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr> 
          <td> 
            <p><font size="6" color="#003399"><b>{$tilmelding->klinieoverskrift}</b></font></p>
            </td>
          <td>
            <div align="right"><font face="Arial, Helvetica, sans-serif" size="2" color="#000000"><b><b><font face="Arial, Helvetica, sans-serif" size="5" color="#003399"><img src="cid:{$tilmelding->image_content_id}" width="229" height="80"></font></b></b></font></div>
          </td>
        </tr>
      </table>
      <p><font face="Arial, Helvetica, sans-serif" size="3" color="#000000">Hej 
        {$tilmelding->fornavn}</font></p>
      <p><font face="Arial, Helvetica, sans-serif" size="2" color="#000000">Tilmeld 
        dig til {$tilmelding->klinieoverskrift}<b><b></b></b></font></p>
      <p><font size="5"><b><font size="6" color="#003399">
        {$tilmelding->kliniedato|date_format:"%A den %e. %B %G kl %H.%M"}
      </font></b></font></p>
      <p>Bindende tilmelding elektronisk ved tryk p&aring; dit navn <a href=https://www.puls3060.dk?p0={$tilmelding->p0}>{$tilmelding->navn}</a></p>
      <p>Efter tilmelding vil du f&aring; tilsendt en e-mail som kvittering.</p>
      <p>Hvis du vil vide mere om <font face="Arial, Helvetica, sans-serif" size="2" color="#000000">{$tilmelding->klinieoverskrift}</font> 
        s&aring; tryk <a href="http://www.puls3060.dk{$tilmelding->link}?p3=no">her</a></p>
      <p><font face="Arial, Helvetica, sans-serif" size="2" color="#000000">Mvh.<br>
        Puls 3060<br>
        </font></p>
      <p><font face="Arial, Helvetica, sans-serif" size="2" color="#000000"><font size="3" color="#FF0000"><b>Sidste 
        frist for tilmelding er {$tilmelding->tilmeldingslut|date_format:"%A den %e. %B"}.</b></font></font></p>
    </td>
    <td width="30">&nbsp;</td>
  </tr>
  <tr> 
    <td width="30">&nbsp;</td>
    <td align="right"><br>
      <font color=#a03952 face="Arial, Helvetica, sans-serif" 
size=2><i><b></b></i></font> <br>
      <br>
    </td>
    <td width="30">&nbsp;</td>
  </tr>
</table>
</body>
</html>
