<? header ("Last-Modified: " . gmdate("D, d M Y H:i:s") . " GMT"); // always modified ?>
<html>
<?
setlocale (LC_TIME, "da_DK.ISO8859-1");

include_once("conn.inc");  
$dbLink = pg_connect($conn_www);

$Query="SELECT OpgaveNavn FROM tblOpgave WHERE Id=$Id";
$dbResult = pg_query($dbLink, $Query);
$row = pg_fetch_array($dbResult, NULL , PGSQL_ASSOC);

$OpgaveNavn = $row["OpgaveNavn"];
$Kvitering = "Tak for dit tilsagn om hj&aelig;lp til at udf&oslash;re opgaven:" . $OpgaveNavn;
$Fejltekst = "";
$Fejl = 0;
$ok = 0;
if ($Fortryd)
{
  $Fornavn = "";
  $Efternavn = "";
  $Email = "";
  $Tlfnr = "";
  $Note = "";
}

if ($Send)
{
  $MailRef   = "Tilsagn $OpgaveNavn";

  if (!$OpgaveNavn) {
  	$Fejltekst .= "Opgave: skal angives";
    $Fejl = 1;
  } else {
    $MailBody  = "OpgaveId:\t$Id\n";
    $MailBody  .= "Opgave:\t$OpgaveNavn\n";
  }
  
  if (!$Fornavn) {
    $FornavnFejl = "skal udfyldes";
    $Fejl = 1;
  } else {
    $FornavnFejl = "";
    $MailBody .= "Fornavn:\t$Fornavn\n";
  }

  if (!$Efternavn) {
    $EfternavnFejl = "skal udfyldes";
    $Fejl = 1;
  } else {
    $EfternavnFejl = "";
    $MailBody .= "Efternavn:\t$Efternavn\n";
  }

  if (!$Email) {
    $EmailFejl = "skal udfyldes";
    $Fejl = 1;
  } else {
    $EmailFejl = "";
    $MailBody .= "Email:\t$Email\n";
  }

  if (!$Tlfnr) {
    $TlfnrFejl = "skal udfyldes";
    $Fejl = 1;
  } else {
    $TlfnrFejl = "";
    $MailBody .= "Tlfnr:\t$Tlfnr\n";
  }

  if ($Note) {
    $NoteFejl = "";
    $MailBody .= "Note:\n$Note";
  }

  if (!$Fejl){
    $MailExt  = "From: $Email\n";
    $MailExt .= "Reply-To: $Email\n";
    $MailExt .= "X-Mailer: PHP/" . phpversion();
    mail("mha@hafsjold.dk", $MailRef, $MailBody, $MailExt, "-f mha@puls3060.dk");
    $ok = 1;
  }
}
?>
<head>
<title>Tilsagn</title>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
</head>

<body bgcolor="#99CC00" text="#000000">

<? if (!$ok) { ?>
  <form name="Tilsagn" method="post" action="Tilsagn.php?Id=<?=$Id?>">
  <table width="100%" border="0" cellpadding="0" cellspacing="0">
    <tr bgcolor="#99CC00"> 
      <td width="20" height="6" bgcolor="#99CC00">&nbsp;</td>
      <td width="350" height="6">&nbsp;</td>
      <td height="6" bgcolor="#99CC00">&nbsp;</td>
    </tr>
    <tr>
      <td width="20" bgcolor="#99CC00">&nbsp;</td>
      <td width="350"> 
        <table width="89%" border="0" bgcolor="#FFFFFF">
          <tr> 
            <td width="300" colspan="2"><b><font color="#003399">Jeg vil gerne 
              hj&aelig;lpe med at udf&oslash;re opgaven: 
              <?=$OpgaveNavn?>
              </font></b></td>
          </tr>
          <tr> 
            <td width="50">&nbsp;</td>
            <td width="275">&nbsp;</td>
          </tr>
          <tr> 
            <td width="50">Fornavn</td>
            <td width="275"> 
              <input type="text" name="Fornavn" size="25" maxlength="25" value="<?=$Fornavn?>">
              <font color="#FF0000"> 
              <?=$FornavnFejl?>
              </font> </td>
          </tr>
          <tr> 
            <td width="50">Efternavn</td>
            <td width="275"> 
              <input type="text" name="Efternavn" size="25" maxlength="25" value="<?=$Efternavn?>">
              <font color="#FF0000"> 
              <?=$EfternavnFejl?>
              </font> </td>
          </tr>
          <tr> 
            <td width="50">E-mail</td>
            <td width="275"> 
              <input type="text" name="Email" size="25" maxlength="50" value="<?=$Email?>">
              <font color="#FF0000"> 
              <?=$EmailFejl?>
              </font> </td>
          </tr>
          <tr> 
            <td width="50">Telefonnr</td>
            <td width="275"> 
              <input type="text" name="Tlfnr" size="15" maxlength="15" value="<?=$Tlfnr?>">
              <font color="#FF0000"> 
              <?=$TlfnrFejl?>
              </font> </td>
          </tr>
          <tr> 
            <td width="50" valign="top">Note</td>
            <td width="275"> 
              <textarea name="Note" cols="30" rows="4"><?=$Note?></textarea>
              <font color="#FF0000"></font></td>
          </tr>
          <tr> 
            <td width="50">&nbsp; </td>
            <td width="275" bgcolor="#FFFFFF"> 
              <input type="submit" name="Send" value="Tilsagn">
              <input type="submit" name="Fortryd" value="Fortryd" onClick="FocusParentAndClose()">
            </td>
          </tr>
          <tr> 
            <td width="50">&nbsp;</td>
            <td width="275"> 
              <?=$Fejltekst?>
            </td>
          </tr>
        </table>
      </td>
      <td bgcolor="#99CC00">&nbsp;</td>
    </tr>
    <tr bgcolor="#99CC00"> 
      <td width="20" height="20" bgcolor="#99CC00">&nbsp;</td>
      <td width="350" height="20">&nbsp;</td>
      <td height="20" bgcolor="#99CC00">&nbsp;</td>
    </tr>
  </table>
  <p>&nbsp;</p>
  </form>
<? } else { ?>
  <table width="100%" border="0" cellpadding="0" cellspacing="0">
    <tr bgcolor="#99CC00"> 
      <td width="20" height="6" bgcolor="#99CC00">&nbsp;</td>
      <td width="350" height="6">&nbsp;</td>
      <td height="6" bgcolor="#99CC00">&nbsp;</td>
    </tr>
    <tr>
      <td width="20" bgcolor="#99CC00">&nbsp;</td>
      <td align=center valign="middle" width="350" height="250" bgcolor="#FFFFFF"> 
         <b><font color="#003399"><?=$Kvitering?></font></b>
         <br><br><input type="submit" name="OK" value="OK" onClick="FocusParentAndClose()">
      </td>
      <td bgcolor="#99CC00">&nbsp;</td>
    </tr>
    <tr bgcolor="#99CC00"> 
      <td width="20" height="20" bgcolor="#99CC00">&nbsp;</td>
      <td width="350" height="20">&nbsp;</td>
      <td height="20" bgcolor="#99CC00">&nbsp;</td>
    </tr>
  </table>
<? } ?>
</body>
</html>
<script language="javascript">
<!--
function FocusParentAndClose(){
    var ParentWindow;
	ParentWindow = top.opener;
	// ParentWindow.document.execCommand("Refresh");
	ParentWindow.focus();
	window.close();
}
// -->
</script>
