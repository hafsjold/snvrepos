<? header ("Last-Modified: " . gmdate("D, d M Y H:i:s") . " GMT"); // always modified ?>
<html>
<script language="JavaScript">
<!--
function MM_findObj(n, d) { //v3.0
  var p,i,x;  if(!d) d=document; if((p=n.indexOf("?"))>0&&parent.frames.length) {
    d=parent.frames[n.substring(p+1)].document; n=n.substring(0,p);}
  if(!(x=d[n])&&d.all) x=d.all[n]; for (i=0;!x&&i<d.forms.length;i++) x=d.forms[i][n];
  for(i=0;!x&&d.layers&&i<d.layers.length;i++) x=MM_findObj(n,d.layers[i].document); return x;
}
function MM_swapImage() { //v3.0
  var i,j=0,x,a=MM_swapImage.arguments; document.MM_sr=new Array; for(i=0;i<(a.length-2);i+=3)
   if ((x=MM_findObj(a[i]))!=null){document.MM_sr[j++]=x; if(!x.oSrc) x.oSrc=x.src; x.src=a[i+2];}
}
function MM_swapImgRestore() { //v3.0
  var i,x,a=document.MM_sr; for(i=0;a&&i<a.length&&(x=a[i])&&x.oSrc;i++) x.src=x.oSrc;
}

function MM_preloadImages() { //v3.0
 var d=document; if(d.images){ if(!d.MM_p) d.MM_p=new Array();
   var i,j=d.MM_p.length,a=MM_preloadImages.arguments; for(i=0; i<a.length; i++)
   if (a[i].indexOf("#")!=0){ d.MM_p[j]=new Image; d.MM_p[j++].src=a[i];}}
}

//-->
</script>
<?
include_once("dbFunctions.inc");
include_once("crypt64.inc");    

setlocale (LC_TIME, "da_DK.ISO8859-1");

$TlfNr = NULL;
$TlfType = NULL;
$MailAdr = NULL;
$MailType = NULL;

$DTlfNr = NULL;
$DTlfType = NULL;
$DMailAdr = NULL;
$DMailType = NULL;
$AdresseFejl = NULL;
$PostnrFejl = NULL;
$ByNavnFejl = NULL;
$IndmeldtFejl = NULL;
$BetaltTilDatoFejl = NULL;
$UdmeldtFejl = NULL;
$KonFejl = NULL;
$FodtDatoFejl = NULL;
$FodtAarFejl = NULL;
$nomailFejl = NULL;
$brugeridFejl = NULL;
$passwdFejl = NULL;



if (isset($_REQUEST['DoWhat'])) 
  $DoWhat=$_REQUEST['DoWhat'];
else
  $DoWhat = 0;

if (isset($_REQUEST['TopWin'])) 
  $TopWin=$_REQUEST['TopWin'];
else
  $TopWin = $_SERVER['SCRIPT_NAME'];

if (isset($_REQUEST['Id'])) $Id=$_REQUEST['Id'];
else $Id = NULL;

if (isset($_REQUEST['telefonlinnr'])) $telefonlinnr=$_REQUEST['telefonlinnr'];
else $telefonlinnr = NULL;

if (isset($_REQUEST['emaillinnr'])) $emaillinnr=$_REQUEST['emaillinnr'];
else $emaillinnr = NULL;

if (isset($_REQUEST['Puls3060MedlemId'])) $Puls3060MedlemId=$_REQUEST['Puls3060MedlemId'];
else $Puls3060MedlemId = NULL;

if (isset($_REQUEST['PersonligId'])) $PersonligId=$_REQUEST['PersonligId'];
else $PersonligId = NULL;

if (isset($_REQUEST['Fornavn'])) $Fornavn=$_REQUEST['Fornavn'];
else $Fornavn = NULL;

if (isset($_REQUEST['Efternavn'])) $Efternavn=$_REQUEST['Efternavn'];
else $Efternavn = NULL;

if (isset($_REQUEST['Adresse'])) $Adresse=$_REQUEST['Adresse'];
else $Adresse = NULL;

if (isset($_REQUEST['Postnr'])) $Postnr=$_REQUEST['Postnr'];
else $Postnr = NULL;

if (isset($_REQUEST['ByNavn'])) $ByNavn=$_REQUEST['ByNavn'];
else $ByNavn = NULL;

if (isset($_REQUEST['Indmeldt'])) $Indmeldt=$_REQUEST['Indmeldt'];
else $Indmeldt = NULL;

if (isset($_REQUEST['BetaltTilDato'])) $BetaltTilDato=$_REQUEST['BetaltTilDato'];
else $xxx = NULL;

if (isset($_REQUEST['Udmeldt'])) $Udmeldt=$_REQUEST['Udmeldt'];
else $Udmeldt = NULL;

if (isset($_REQUEST['Kon'])) $Kon=$_REQUEST['Kon'];
else $Kon = NULL;

if (isset($_REQUEST['FodtDato'])) $FodtDato=$_REQUEST['FodtDato'];
else $FodtDato = NULL;

if (isset($_REQUEST['FodtAar'])) $FodtAar=$_REQUEST['FodtAar'];
else $FodtAar = NULL;

if (isset($_REQUEST['nomail'])) $nomail=$_REQUEST['nomail'];
else $nomail = NULL;

if (isset($_REQUEST['brugerid'])) $brugerid=$_REQUEST['brugerid'];
else $brugerid = NULL;

if (isset($_REQUEST['passwd'])) $passwd=$_REQUEST['passwd'];
else $passwd = NULL;

if (isset($_REQUEST['Fornavn_old'])) $Fornavn_old=$_REQUEST['Fornavn_old'];
else $Fornavn_old = NULL;

if (isset($_REQUEST['Efternavn_old'])) $Efternavn_old=$_REQUEST['Efternavn_old'];
else $Efternavn_old = NULL;

if (isset($_REQUEST['Adresse_old'])) $Adresse_old=$_REQUEST['Adresse_old'];
else $Adresse_old = NULL;

if (isset($_REQUEST['Postnr_old'])) $Postnr_old=$_REQUEST['Postnr_old'];
else $Postnr_old = NULL;

if (isset($_REQUEST['ByNavn_old'])) $ByNavn_old=$_REQUEST['ByNavn_old'];
else $ByNavn_old = NULL;

if (isset($_REQUEST['Indmeldt_old'])) $Indmeldt_old=$_REQUEST['Indmeldt_old'];
else $Indmeldt_old = NULL;

if (isset($_REQUEST['BetaltTilDato_old'])) $BetaltTilDato_old=$_REQUEST['BetaltTilDato_old'];
else $BetaltTilDato_old = NULL;

if (isset($_REQUEST['Udmeldt_old'])) $Udmeldt_old=$_REQUEST['Udmeldt_old'];
else $Udmeldt_old = NULL;

if (isset($_REQUEST['Kon_old'])) $Kon_old=$_REQUEST['Kon_old'];
else $Kon_old = NULL;

if (isset($_REQUEST['FodtDato_old'])) $FodtDato_old=$_REQUEST['FodtDato_old'];
else $FodtDato_old = NULL;

if (isset($_REQUEST['FodtAar_old'])) $FodtAar_old=$_REQUEST['FodtAar_old'];
else $FodtAar_old = NULL;

if (isset($_REQUEST['nomail_old'])) $nomail_old=$_REQUEST['nomail_old'];
else $nomail_old = NULL;

if (isset($_REQUEST['brugerid_old'])) $brugerid_old=$_REQUEST['brugerid_old'];
else $brugerid_old = NULL;

if (isset($_REQUEST['passwd_old'])) $passwd_old=$_REQUEST['passwd_old'];
else $passwd_old = NULL;

if (isset($_REQUEST['DFornavn'])) $DFornavn=$_REQUEST['DFornavn'];
else $DFornavn = NULL;

if (isset($_REQUEST['DEfternavn'])) $DEfternavn=$_REQUEST['DEfternavn'];
else $DEfternavn = NULL;

if (isset($_REQUEST['DAdresse'])) $DAdresse=$_REQUEST['DAdresse'];
else $DAdresse = NULL;

if (isset($_REQUEST['DPostnr'])) $DPostnr=$_REQUEST['DPostnr'];
else $DPostnr = NULL;

if (isset($_REQUEST['DByNavn'])) $DByNavn=$_REQUEST['DByNavn'];
else $DByNavn = NULL;

if (isset($_REQUEST['DTlfNr0'])) $DTlfNr0=$_REQUEST['DTlfNr0'];
else $DTlfNr0 = NULL;

if (isset($_REQUEST['DTlfType0'])) $DTlfType0=$_REQUEST['DTlfType0'];
else $DTlfType0 = NULL;

if (isset($_REQUEST['DMailAdr0'])) $DMailAdr0=$_REQUEST['DMailAdr0'];
else $DMailAdr0 = NULL;

if (isset($_REQUEST['DMailType0'])) $DMailType0=$_REQUEST['DMailType0'];
else $DMailType0 = NULL;

if (isset($_REQUEST['DIndmeldt'])) $DIndmeldt=$_REQUEST['DIndmeldt'];
else $DIndmeldt = NULL;

if (isset($_REQUEST['DBetaltTilDato'])) $DBetaltTilDato=$_REQUEST['DBetaltTilDato'];
else $DBetaltTilDato = NULL;

if (isset($_REQUEST['DUdmeldt'])) $DUdmeldt=$_REQUEST['DUdmeldt'];
else $DUdmeldt = NULL;

if (isset($_REQUEST['DKon'])) $DKon=$_REQUEST['DKon'];
else $DKon = NULL;

if (isset($_REQUEST['DFodtDato'])) $DFodtDato=$_REQUEST['DFodtDato'];
else $DFodtDato = NULL;

if (isset($_REQUEST['DFodtAar'])) $DFodtAar=$_REQUEST['DFodtAar'];
else $DFodtAar = NULL;

if (isset($_REQUEST['Dnomail'])) $Dnomail=$_REQUEST['Dnomail'];
else $Dnomail = NULL;

if (isset($_REQUEST['Dbrugerid'])) $Dbrugerid=$_REQUEST['Dbrugerid'];
else $Dbrugerid = NULL;

if (isset($_REQUEST['Dpasswd'])) $Dpasswd=$_REQUEST['Dpasswd'];
else $Dpasswd = NULL;

if (isset($_REQUEST['Fortryd'])) $Fortryd=$_REQUEST['Fortryd'];
else $Fortryd = NULL;

if (isset($_REQUEST['Send'])) $Send=$_REQUEST['Send'];
else $Send = NULL;



function setcolor($Data1, $Data2)
{
  if ($Data1==$Data2)
    return(" class=color2");
  else
    return(" class=color1");
}

include_once("conn.inc");  
$dbLink = pg_connect($conn_www);


$Fejl   = 0;
$onLoad = "fonLoad(); MM_preloadImages('/images/vpil_f2.gif');";

switch($DoWhat)
{
  case 2:  // Vis Tom Record
    
    $Fornavn   = "";
    $Efternavn = "";
    $Adresse   = "";
    $Postnr    = "";
    $ByNavn    = "";
    $nomail = "";

    $telefonlinnr = 0;
    $TelefonId[$telefonlinnr] = 0;
    $TlfNr[$telefonlinnr]     = "";
    $TlfType[$telefonlinnr++] = "";
 
    $emaillinnr = 0;
    $MailadresseId[$emaillinnr] = 0;
    $MailAdr[$emaillinnr]       = "";
    $MailType[$emaillinnr++]    = "";

    $Puls3060MedlemId = "";
    $Indmeldt         = "";
    $BetaltTilDato    = "";
    $Udmeldt          = "";

    $PersonligId = "";
    $Kon         = "";
    $FodtDato    = "";
    $FodtAar     = "";
    $brugerid     = "";
    $passwd     = "";
    
    if ($TopWin=="/admin/PListe4.php"){ 
      $Fornavn    = "$DFornavn";
      $Efternavn  = "$DEfternavn";
      $Adresse    = "$DAdresse";
      $Postnr     = "$DPostnr";
      $ByNavn     = "$DByNavn";

      $TlfNr[0]   = "$DTlfNr0";
      $TlfType[0] = "$DTlfType0";
   
      $MailAdr[0] = "$DMailAdr0";
      $MailType[0]= "$DMailType0";

      $Indmeldt      = "$DIndmeldt";
      $BetaltTilDato = "$DBetaltTilDato";
      $Udmeldt       = "$DUdmeldt";

      $Kon        = "$DKon";
      $FodtDato   = "$DFodtDato";
      $FodtAar    = "$DFodtAar";
      $brugerid   = "$Dbrugerid";
	  $passwd     = "$Dpasswd";
    }
    break;

  case 3:  // Vis Record
    
    $Query="SELECT * FROM tblPerson WHERE Id=$Id";
    if ($dbResult = pg_query($dbLink, $Query))
      if ($row = pg_fetch_array($dbResult, NULL , PGSQL_ASSOC)) {
        $Fornavn   = $row["fornavn"];
        $Efternavn = $row["efternavn"];
        $Adresse   = $row["adresse"];
        $Postnr    = $row["postnr"];
        $ByNavn    = $row["bynavn"];
        $nomail    = $row["nomail"];
      }

    $telefonlinnr = 0;
    $Query="SELECT * FROM tblTelefon WHERE PersonId=$Id";
    if ($dbResult = pg_query($dbLink, $Query))
      while($row = pg_fetch_array($dbResult, NULL , PGSQL_ASSOC)){
        $TelefonId[$telefonlinnr] = $row["id"];
        $TlfNr[$telefonlinnr]     = $row["tlfnr"];
        $TlfType[$telefonlinnr++] = $row["tlftype"];
	  }
    $TelefonId[$telefonlinnr] = 0;
    $TlfNr[$telefonlinnr]     = "";
    $TlfType[$telefonlinnr++] = "";

    if ($TopWin=="/admin/PListe4.php"){ 
      $DTlfNr[0]   = "$DTlfNr0";
      $DTlfType[0] = "$DTlfType0";
    }
 
    $emaillinnr = 0;
    $Query="SELECT * FROM tblMailadresse WHERE PersonId=$Id";
    if ($dbResult = pg_query($dbLink, $Query))
      while($row = pg_fetch_array($dbResult, NULL , PGSQL_ASSOC)){
        $MailadresseId[$emaillinnr] = $row["id"];
        $MailAdr[$emaillinnr]       = $row["mailadr"];
        $MailType[$emaillinnr++]    = $row["mailtype"];
	  }
    $MailadresseId[$emaillinnr] = 0;
    $MailAdr[$emaillinnr]       = "";
    $MailType[$emaillinnr++]    = "";

    if ($TopWin=="/admin/PListe4.php"){ 
      $DMailAdr[0] = "$DMailAdr0";
      $DMailType[0]= "$DMailType0";
    }

    $Query="SELECT * FROM tblPuls3060Medlem WHERE PersonId=$Id";
    if ($dbResult = pg_query($dbLink, $Query))
      if ($row = pg_fetch_array($dbResult, NULL , PGSQL_ASSOC)) {
        $Puls3060MedlemId = $row["id"];
        $Indmeldt         = $row["indmeldt"];
        $BetaltTilDato    = $row["betalttildato"];
        $Udmeldt          = $row["udmeldt"];
      }

    $Query="SELECT * FROM tblPersonlig WHERE PersonId=$Id";
    if ($dbResult = pg_query($dbLink, $Query))
      if ($row = pg_fetch_array($dbResult, NULL , PGSQL_ASSOC)) {
        $PersonligId = $row["id"];
        $Kon         = $row["kon"];
        $FodtDato    = $row["fodtdato"];
        $FodtAar     = $row["fodtaar"];
        $brugerid    = $row["brugerid"];
        $passwd      = $row["passwd"];
	 }
    break;
  

  case 5:  // Gem rettet Record
 	
    if (!$Fornavn) {
      $FornavnFejl = "skal udfyldes";
      $Fejl = 1;
    }

    if (!$Efternavn) {
      $EfternavnFejl = "skal udfyldes";
      $Fejl = 1;
    }

    //if (!$Adresse) {
    //  $AdresseFejl = "skal udfyldes";
    //  $Fejl = 1;
    //}

    //if (!$Postnr) {
    //  $PostnrFejl = "skal udfyldes";
    //  $Fejl = 1;
    //}

    //if (!$ByNavn) {
    //  $ByNavnFejl = "skal udfyldes";
    //  $Fejl = 1;
    //}

    if (!$Fejl){
      // Opdater tblPerson
      $set ="SET ";
      $fset =0;
      if ($Fornavn != $Fornavn_old){
        if ($fset) $set .= ", ";
        $set .="Fornavn = '$Fornavn'";
        $set .=", FnSound = soundex('" . $Fornavn . "')";
        $fset =1;
      }
      if ($Efternavn != $Efternavn_old){
        if ($fset) $set .= ", ";
        $set .="Efternavn = '$Efternavn'";
        $set .=", EnSound = soundex('" . $Efternavn . "')";
        $fset =1;
      }
      if ($Adresse != $Adresse_old){
        if ($fset) $set .= ", ";
        $set .="Adresse = '$Adresse'";
        $fset =1;
      }
      
      if ($Postnr != $Postnr_old){
        if ($fset) $set .= ", ";
        $set .="Postnr = '$Postnr'";
        $fset =1;
      }

      if ($ByNavn != $ByNavn_old){
        if ($fset) $set .= ", ";
        $set .="ByNavn = '$ByNavn'";
        $fset =1;
      }
      
      if ($nomail != $nomail_old){
        if ($fset) $set .= ", ";
        $set .="nomail = '$nomail'";
        $fset =1;
      }


	  $Query="";
      if ($fset) {
        $Query .=" UPDATE tblPerson " . $set . " WHERE Id = $Id;";
      }

      // Opdater tblTelefon
      for ($i=0; $i<$telefonlinnr; $i++) {
        if ($_POST["TlfNr" . $i] == "")
		{
	      if ($_POST["TelefonId" . $i] > 0){
            $Query .="DELETE FROM tblTelefon WHERE Id = " . $_POST["TelefonId" . $i] . ";";
		  }
		}
		else
		{
          if ($_POST["TelefonId" . $i] > 0){
            $set ="SET ";
            $fset =0;
            if ($_POST["TlfNr" . $i] != $_POST["TlfNr" . $i . "_old"]){
              if ($fset) $set .= ", ";
              $set .="TlfNr = '" . $_POST["TlfNr" . $i] . "'";
              $fset =1;
            }
            if ($_POST["TlfType" . $i] != $_POST["TlfType" . $i . "_old"]){
              if ($fset) $set .= ", ";
              $set .="TlfType = '" . $_POST["TlfType" . $i] . "'";
              $fset =1;
            }
          }
          else{
            $set1 ="(";
            $set2 ="values(";
            $fset =0;
            if ($_POST["TlfNr" . $i] != $_POST["TlfNr" . $i . "_old"]){
              if ($fset){
                $set1 .= ", ";
                $set2 .= ", ";
              }
              $set1 .="TlfNr";
              $set2 .="'" . $_POST["TlfNr" . $i] . "'";
              $fset =1;
            }
            if ($_POST["TlfType" . $i] != $_POST["TlfType" . $i . "_old"]){
              if ($fset){
                $set1 .= ", ";
                $set2 .= ", ";
              }
              $set1 .="TlfType";
              $set2 .="'" . $_POST["TlfType" . $i] . "'";
              $fset =1;
            }
            if ($fset){
              $set1 .=", PersonId";
              $set2 .=", " . $Id;
            }
          }
          if ($fset) {
	        if ($_POST["TelefonId" . $i] > 0)
              $Query .=" UPDATE tblTelefon " . $set . " WHERE Id = " . $_POST["TelefonId" . $i] .";";
		    else
              $Query .=" INSERT INTO tblTelefon " . $set1 . ") " . $set2 . ");";
          }
        }
	  }

      // Opdater tblMailadresse
      for ($i=0; $i<$emaillinnr; $i++) {
        if ($_POST["MailAdr" . $i] == "")
		{
	      if ($_POST["MailadresseId" . $i] > 0){
            $Query .="DELETE FROM tblMailadresse WHERE Id = " . $_POST["MailadresseId" . $i] . ";";
		  }
		}
		else
		{
  	      if ($_POST["MailadresseId" . $i] > 0){
            $set ="SET ";
            $fset =0;
            if ($_POST["MailAdr" .$i] != $_POST["MailAdr" . $i . "_old"]){
              if ($fset) $set .= ", ";
              $set .="MailAdr = '" . $_POST["MailAdr" . $i] . "'";
              $fset =1;
            }
            if ($_POST["MailType" .$i] != $_POST["MailType" . $i . "_old"]){
              if ($fset) $set .= ", ";
              $set .="MailType = '" . $_POST["MailType" . $i] . "'";
              $fset =1;
            }
          }
		  else{
            $set1 ="(";
            $set2 ="values(";
            $fset =0;
            if ($_POST["MailAdr" .$i] != $_POST["MailAdr" . $i . "_old"]){
              if ($fset){
                $set1 .= ", ";
                $set2 .= ", ";
              }
              $set1 .="MailAdr";
              $set2 .="'" . $_POST["MailAdr" . $i] . "'";
              $fset =1;
            }
            if ($_POST["MailType" .$i] != $_POST["MailType" . $i . "_old"]){
              if ($fset){
                $set1 .= ", ";
                $set2 .= ", ";
              }
              $set1 .="MailType";
              $set2 .="'" . $_POST["MailType" . $i] . "'";
              $fset =1;
            }
            if ($fset){
              $set1 .=", PersonId";
              $set2 .=", " . $Id;
            }
		  }
          if ($fset) {
  	      
  	      if ($_POST["MailadresseId" . $i] > 0)
              $Query .=" UPDATE tblMailadresse " . $set . " WHERE Id = " . $_POST["MailadresseId" . $i] . ";";
  		  else
              $Query .=" INSERT INTO tblMailadresse " . $set1 . ") " . $set2 . ");";
          }
        }
	  }

      // Opdater tblPuls3060Medlem
	  if ($Puls3060MedlemId > 0){
        $set ="SET ";
        $fset =0;
        if ($Indmeldt != $Indmeldt_old){
          if ($fset) $set .= ", ";
          $set .="Indmeldt = ".(($Indmeldt == '') ? "null" : "'$Indmeldt'");
          $fset =1;
        }
        if ($BetaltTilDato != $BetaltTilDato_old){
          if ($fset) $set .= ", ";
          $set .="BetaltTilDato = ".(($BetaltTilDato == '') ? "null" : "'$BetaltTilDato'");
          $fset =1;
        }
        if ($Udmeldt != $Udmeldt_old){
          if ($fset) $set .= ", ";
          $set .="Udmeldt = ".(($Udmeldt == '') ? "null" : "'$Udmeldt'");
          $fset =1;
        }
	  }
	  else{
        $set1 ="(";
        $set2 ="values(";
        $fset =0;
        if ($Indmeldt != $Indmeldt_old){
          if ($fset){
            $set1 .= ", ";
            $set2 .= ", ";
          }
          $set1 .="Indmeldt";
          $set2 .="'$Indmeldt'";
          $fset =1;
        }
        if ($BetaltTilDato != $BetaltTilDato_old){
          if ($fset){
            $set1 .= ", ";
            $set2 .= ", ";
          }
          $set1 .="BetaltTilDato";
          $set2 .="'$BetaltTilDato'";
          $fset =1;
        }
        if ($Udmeldt != $Udmeldt_old){
          if ($fset){
            $set1 .= ", ";
            $set2 .= ", ";
          }
          $set1 .="Udmeldt";
          $set2 .="$Udmeldt'";
          $fset =1;
        }
        if ($fset){
          $set1 .=", PersonId";
          $set2 .=", " . $Id;
        }
	  }

      if ($fset) {
	    if ($Puls3060MedlemId > 0)
          $Query .=" UPDATE tblPuls3060Medlem " . $set . " WHERE Id = $Puls3060MedlemId;";
		else
          $Query .=" INSERT INTO tblPuls3060Medlem " . $set1 . ") " . $set2 . ");";
      }

      // Opdater tblPersonlig
	  if ($PersonligId > 0){
        $set ="SET ";
        $fset =0;
        if ($Kon != $Kon_old){
          if ($fset) $set .= ", ";
          $set .="Kon = '$Kon'";
          $fset =1;
        }
        if ($FodtDato != $FodtDato_old){
          if ($fset) $set .= ", ";
          $set .="FodtDato = ".(($FodtDato == '') ? "null" : "'$FodtDato'");
          $fset =1;
        }
        if ($FodtAar != $FodtAar_old){
          if ($fset) $set .= ", ";
          $set .="FodtAar = ".(($FodtAar == '') ? "null" : "'$FodtAar'");
          $fset =1;
        }
        if ($brugerid != $brugerid_old){
          if ($fset) $set .= ", ";
          $set .="brugerid = ".(($brugerid == '') ? "null" : "'$brugerid'");
          $fset =1;
        }
        if ($passwd != $passwd_old){
          if ($fset) $set .= ", ";
		  $password = crypt($passwd, base64_encode($passwd));		  
          $set .="passwd = ".(($passwd == '') ? "null" : "'$password'");
          $fset =1;
          
		  if ($fset) $set .= ", ";
		  $passwdcrypt = encrypt64($passwd);		  
          $set .="passwdcrypt = ".(($passwd == '') ? "null" : "'$passwdcrypt'");
          $fset =1;
		}
      }
	  else{
        $set1 ="(";
        $set2 ="values(";
        $fset =0;
        if ($Kon != $Kon_old){
          if ($fset){
            $set1 .= ", ";
            $set2 .= ", ";
          }
          $set1 .="Kon";
          $set2 .="'$Kon'";
          $fset =1;
        }
        if ($FodtDato != $FodtDato_old){
          if ($fset){
            $set1 .= ", ";
            $set2 .= ", ";
          }
          $set1 .="FodtDato";
          $set2 .="'$FodtDato'";
          $fset =1;
        }
        if ($FodtAar != $FodtAar_old){
          if ($fset){
            $set1 .= ", ";
            $set2 .= ", ";
          }
          $set1 .="FodtAar";
          $set2 .="'$FodtAar'";
          $fset =1;
        }
        if ($brugerid != $brugerid_old){
          if ($fset){
            $set1 .= ", ";
            $set2 .= ", ";
          }
          $set1 .="brugerid";
          $set2 .="'$brugerid'";
          $fset =1;
        }
        if ($passwd != $passwd_old){
          if ($fset){
            $set1 .= ", ";
            $set2 .= ", ";
          }
          $set1 .="passwd";
		  $password = crypt($passwd, base64_encode($passwd));
          $set2 .="'$password'";
          $fset =1;

          if ($fset){
            $set1 .= ", ";
            $set2 .= ", ";
          }
          $set1 .="passwdcrypt";
		  $passwdcrypt = encrypt64($passwd);	
          $set2 .="'$passwdcrypt'";
          $fset =1;		        
		}
 
      if ($fset){
          $set1 .=", PersonId";
          $set2 .=", " . $Id;
        }
	  }
      
      if ($fset) {
	    if ($PersonligId > 0)
          $Query .=" UPDATE tblPersonlig " . $set . " WHERE Id = $PersonligId;";
		else
          $Query .=" INSERT INTO tblPersonlig " . $set1 . ") " . $set2 . ");";
      }
	  
	  if ($Query){
	    $Queries .="BEGIN; " . $Query . "COMMIT;";
        if (!($dbResult = pg_query($dbLink, $Queries))){
           echo "$Queries";
           print("<br>ROLLBACK: " . pg_errormessage($dbLink)) . "<br>";
           $dbResult = pg_query($dbLink, "ROLLBACK;");
		   $Fejl = 1;
		}
		else
          $onLoad = "UpdateParentAndClose()";
	  }
	  else
        $onLoad = "UpdateParentAndClose()";


    }
    break;

  case 6:  // Gem ny Record
 	
    if (!$Fornavn) {
      $FornavnFejl = "skal udfyldes";
      $Fejl = 1;
    }

    if (!$Efternavn) {
      $EfternavnFejl = "skal udfyldes";
      $Fejl = 1;
    }

    //if (!$Adresse) {
    //  $AdresseFejl = "skal udfyldes";
    //  $Fejl = 1;
    //}

    //if (!$Postnr) {
    //  $PostnrFejl = "skal udfyldes";
    //  $Fejl = 1;
    //}

    //if (!$ByNavn) {
    //  $ByNavnFejl = "skal udfyldes";
    //  $Fejl = 1;
    //}

    if (!$Fejl){
      // Opret tblPerson
      $set1 ="(";
      $set2 ="values(";
      $fset =0;
      if ($Fornavn != $Fornavn_old){
        if ($fset){
          $set1 .= ", ";
          $set2 .= ", ";
        }
        $set1 .="Fornavn";
        $set2 .="'$Fornavn'";
        $set1 .=", FnSound";
        $set2 .=", soundex('" . $Fornavn . "')";
        $fset =1;
      }
      if ($Efternavn != $Efternavn_old){
        if ($fset){
          $set1 .= ", ";
          $set2 .= ", ";
        }
        $set1 .="Efternavn";
        $set2 .="'$Efternavn'";
        $set1 .=", EnSound";
        $set2 .=", soundex('" . $Efternavn . "')";
        $fset =1;
      }
      if ($Adresse != $Adresse_old){
        if ($fset){
          $set1 .= ", ";
          $set2 .= ", ";
        }
        $set1 .="Adresse";
        $set2 .="'$Adresse'";
        $fset =1;
      }
      
      if ($Postnr != $Postnr_old){
        if ($fset){
          $set1 .= ", ";
          $set2 .= ", ";
        }
        $set1 .="Postnr";
        $set2 .="'$Postnr'";
        $fset =1;
      }

      if ($ByNavn != $ByNavn_old){
        if ($fset){
          $set1 .= ", ";
          $set2 .= ", ";
        }
        $set1 .="ByNavn";
        $set2 .="'$ByNavn'";
        $fset =1;
      }

      if ($nomail != $nomail_old){
        if ($fset){
          $set1 .= ", ";
          $set2 .= ", ";
        }
        $set1 .="nomail";
        $set2 .="'$nomail'";
        $fset =1;
      }

	  $Query="";
      if ($fset) {
        $Query .=" INSERT INTO tblPerson " . $set1 . ") " . $set2 . ");";
      }

      // Opret tblTelefon
      for ($i=0; $i<$telefonlinnr; $i++) {
        $set1 ="(";
        $set2 ="values(";
        $fset =0;
        if ($_POST["TlfNr" . $i] != $_POST["TlfNr" . $i . "_old"]){
          if ($fset){
            $set1 .= ", ";
            $set2 .= ", ";
          }
          $set1 .="TlfNr";
          $set2 .="'" . $_POST["TlfNr" . $i] . "'";
          $fset =1;
        }
        if ($_POST["TlfType" . $i] != $_POST["TlfType" . $i . "_old"]){
          if ($fset){
            $set1 .= ", ";
            $set2 .= ", ";
          }
          $set1 .="TlfType";
          $set2 .="'" . $_POST["TlfType" . $i] . "'";
          $fset =1;
        }
        if ($fset){
          $set1 .=", PersonId";
          $set2 .=", currval('tblperson_id_seq')";
        }
        if ($fset) {
          $Query .=" INSERT INTO tblTelefon " . $set1 . ") " . $set2 . ");";
        }
	  }

      // Opret tblMailadresse
      for ($i=0; $i<$emaillinnr; $i++) {
        $set1 ="(";
        $set2 ="values(";
        $fset =0;
        if ($_POST["MailAdr" .$i] != $_POST["MailAdr" . $i . "_old"]){
          if ($fset){
            $set1 .= ", ";
            $set2 .= ", ";
          }
          $set1 .="MailAdr";
          $set2 .="'" . $_POST["MailAdr" . $i] . "'";
          $fset =1;
        }
        if ($_POST["MailType" .$i] != $_POST["MailType" . $i . "_old"]){
          if ($fset){
            $set1 .= ", ";
            $set2 .= ", ";
          }
          $set1 .="MailType";
          $set2 .="'" . $_POST["MailType" . $i] . "'";
          $fset =1;
        }
        if ($fset){
          $set1 .=", PersonId";
          $set2 .=", currval('tblperson_id_seq')";
        }
        if ($fset) {
          $Query .=" INSERT INTO tblMailadresse " . $set1 . ") " . $set2 . ");";
        }
	  }

      // Opret tblPuls3060Medlem
      $set1 ="(";
      $set2 ="values(";
      $fset =0;
      if ($Indmeldt != $Indmeldt_old){
        if ($fset){
          $set1 .= ", ";
          $set2 .= ", ";
        }
        $set1 .="Indmeldt";
        $set2 .="'$Indmeldt'";
        $fset =1;
      }
      if ($BetaltTilDato != $BetaltTilDato_old){
        if ($fset){
          $set1 .= ", ";
          $set2 .= ", ";
        }
        $set1 .="BetaltTilDato";
        $set2 .="'$BetaltTilDato'";
        $fset =1;
      }
      if ($Udmeldt != $Udmeldt_old){
        if ($fset){
          $set1 .= ", ";
          $set2 .= ", ";
        }
        $set1 .="Udmeldt";
        $set2 .="'$Udmeldt'";
        $fset =1;
      }
      if ($fset){
        $set1 .=", PersonId";
          $set2 .=", currval('tblperson_id_seq')";
      }
      if ($fset) {
        $Query .=" INSERT INTO tblPuls3060Medlem " . $set1 . ") " . $set2 . ");";
      }

      // Opret tblPersonlig
      $set1 ="(";
      $set2 ="values(";
      $fset =0;
      if ($Kon != $Kon_old){
        if ($fset){
          $set1 .= ", ";
          $set2 .= ", ";
        }
        $set1 .="Kon";
        $set2 .="'$Kon'";
        $fset =1;
      }
      if ($FodtDato != $FodtDato_old){
        if ($fset){
          $set1 .= ", ";
          $set2 .= ", ";
        }
        $set1 .="FodtDato";
        $set2 .="'$FodtDato'";
        $fset =1;
      }
      if ($FodtAar != $FodtAar_old){
        if ($fset){
          $set1 .= ", ";
          $set2 .= ", ";
        }
        $set1 .="FodtAar";
        $set2 .="'$FodtAar'";
        $fset =1;
      }
      if ($brugerid != $brugerid_old){
        if ($fset){
          $set1 .= ", ";
          $set2 .= ", ";
        }
        $set1 .="brugerid";
        $set2 .="'$brugerid'";
        $fset =1;
      }
      if ($passwd != $passwd_old){
        if ($fset){
          $set1 .= ", ";
          $set2 .= ", ";
        }
        $set1 .="passwd";
	    $password = crypt($passwd, base64_encode($passwd));
		$set2 .="'$password'";
        $fset =1;

       if ($fset){
          $set1 .= ", ";
          $set2 .= ", ";
        }
        $set1 .="passwdcrypt";
		$passwdcrypt = encrypt64($passwd);	
		$set2 .="'$passwdcrypt'";
        $fset =1;
	  }
      if ($fset){
        $set1 .=", PersonId";
        $set2 .=", currval('tblperson_id_seq')";
      }
      if ($fset) {
        $Query .=" INSERT INTO tblPersonlig " . $set1 . ") " . $set2 . ");";
      }

	  if ($Query){
	    $Queries .="BEGIN; " . $Query . "COMMIT;";
        if (!($dbResult = pg_query($dbLink, $Queries))){
           echo "$Queries";
           print("<br>ROLLBACK: " . pg_errormessage($dbLink)) . "<br>";
           $dbResult = pg_query($dbLink, "ROLLBACK;");
		}
		else{
          $Id = pg_insert_id($dbLink, "tblperson_id_seq");
          $onLoad = "UpdateParentAndClose()";
		}
	  }
	  else
        $onLoad = "UpdateParentAndClose()";
    }
    break;


  default:  // ingen case valgt

} // End Switch($DoWhat) 
?>

<head>
<title>PersonUpd</title>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
<STYLE>
  .color1 { color:red }
  .color2 { color: }
</STYLE>
</head>
  <body bgcolor="#99CC00" text="#000000" onLoad="<?=$onLoad?>">
  <table width="100%" border="0" cellpadding="0" cellspacing="0">
    <tr bgcolor="#99CC00"> 
      <td width="20" height="6" bgcolor="#99CC00">&nbsp;</td>
      <td width="850" height="6">&nbsp;</td>
      <td height="6" bgcolor="#99CC00">&nbsp;</td>
    </tr>
    <tr>
      <td width="20" bgcolor="#99CC00">&nbsp;</td>
      <td width="850" bgcolor="#FFFFFF"> 

      <? if ($DoWhat == 2) { // Vis Tom record ?>
        <form name="Tilsagn" method="post" action="<?=$_SERVER['SCRIPT_NAME']?>?DoWhat=6&Id=<?=$Id?>&TopWin=<?=$TopWin?>">
        <table width="100%" border="0" bgcolor="#FFFFFF">
          <tr> 
            <td width="300" colspan="2"><b>
              <font color="#003399">Opret Person</font></b>
            </td>
          </tr>
          <tr> 
            <td width="50">&nbsp;</td>
            <td width="350">&nbsp;</td>
          </tr>
          <tr> 
            <td width="50">Fornavn</td>
            <td width="350"> 
              <input type="text" name="Fornavn" size="15" maxlength="25" value="<?=$Fornavn?>">
              <input type="hidden" name="Fornavn_old" value="">
            </td>
          </tr>
          <tr> 
            <td width="50" valign="top">Efternavn</td>
            <td width="275"> 
              <input type="text" name="Efternavn" size="15" maxlength="25" value="<?=$Efternavn?>">
              <input type="hidden" name="Efternavn_old" value="">
            </td>
          </tr>
          <tr> 
            <td width="50" valign="top">Adresse</td>
            <td width="275"> 
              <input type="text" name="Adresse" size="25" maxlength="35" value="<?=$Adresse?>">
              <input type="hidden" name="Adresse_old" value="">
            </td>
          </tr>
          <tr> 
            <td width="50">Postnr</td>
            <td width="350"> 
              <input type="text" name="Postnr" size="4" maxlength="8" value="<?=$Postnr?>">
              <input type="hidden" name="Postnr_old" value="">
            </td>
          </tr>
          <tr> 
            <td width="50">By</td>
            <td width="350"> 
              <input type="text" name="ByNavn" size="20" maxlength="25" value="<?=$ByNavn?>">
              <input type="hidden" name="ByNavn_old" value="">
              <input type="hidden" name="telefonlinnr" value="<?=$telefonlinnr?>">
              <input type="hidden" name="emaillinnr" value="<?=$emaillinnr?>">
              <input type="hidden" name="Puls3060MedlemId" value="<?=$Puls3060MedlemId?>">
              <input type="hidden" name="PersonligId" value="<?=$PersonligId?>">
            </td>
          </tr>

          <? for($i=0; $i<$telefonlinnr; $i++) { ?>
            <tr> 
              <td width="50">Telefon</td>
              <td width="350"> 
                <input type="text" name="TlfNr<?=$i?>" size="8" maxlength="8" value="<?=$TlfNr[$i]?>">
                <input type="text" name="TlfType<?=$i?>" size="8" maxlength="8" value="<?=$TlfType[$i]?>">
                <input type="hidden" name="TelefonId<?=$i?>" value="<?=$TelefonId[$i]?>">
                <input type="hidden" name="TlfNr<?=$i?>_old" value="">
                <input type="hidden" name="TlfType<?=$i?>_old" value="">
              </td>
            </tr>
          <? } ?>

          <? for($i=0; $i<$emaillinnr; $i++) { ?>
            <tr> 
              <td width="50">E-mail</td>
              <td width="350"> 
                <input type="text" name="MailAdr<?=$i?>" size="25" maxlength="50" value="<?=$MailAdr[$i]?>">
                <input type="text" name="MailType<?=$i?>" size="8" maxlength="8" value="<?=$MailType[$i]?>">
                <input type="hidden" name="MailadresseId<?=$i?>" value="<?=$MailadresseId[$i]?>">
                <input type="hidden" name="MailAdr<?=$i?>_old" value="">
                <input type="hidden" name="MailType<?=$i?>_old" value="">
              </td>
            </tr>
          <? } ?>
          <tr> 
            <td width="50">Indmeldt</td>
            <td width="350"> 
              <input type="text" name="Indmeldt" size="20" maxlength="25" value="<?=$Indmeldt?>">
              <input type="hidden" name="Indmeldt_old" value="">
            </td>
          </tr>
          <tr> 
            <td width="50">Betalt til</td>
            <td width="350"> 
              <input type="text" name="BetaltTilDato" size="20" maxlength="25" value="<?=$BetaltTilDato?>">
              <input type="hidden" name="BetaltTilDato_old" value="">
            </td>
          </tr>
          <tr> 
            <td width="50">Udmeldt</td>
            <td width="350"> 
              <input type="text" name="Udmeldt" size="20" maxlength="25" value="<?=$Udmeldt?>">
              <input type="hidden" name="Udmeldt_old" value="">
            </td>
          </tr>
          <tr> 
            <td width="50">Køn</td>
            <td width="350"> 
              <input type="text" name="Kon" size="1" maxlength="1" value="<?=$Kon?>">
              <input type="hidden" name="Kon_old" value="">
            </td>
          </tr>
          <tr> 
            <td width="50">Født</td>
            <td width="350"> 
              <input type="text" name="FodtDato" size="20" maxlength="25" value="<?=$FodtDato?>">
              <input type="hidden" name="FodtDato_old" value="">
            </td>
          </tr>
          <tr> 
            <td width="50">Født År</td>
            <td width="350"> 
              <input type="text" name="FodtAar" size="4" maxlength="4" value="<?=$FodtAar?>">
              <input type="hidden" name="FodtAar_old" value="">
            </td>
          </tr>
          <tr> 
            <td width="50">Ingen Mail</td>
            <td width="350"> 
              <input type="text" name="nomail" size="1" maxlength="1" value="<?=$nomail?>">
              <input type="hidden" name="nomail_old" value="">
            </td>
          </tr>
          <tr> 
            <td width="50">UserId</td>
            <td width="350"> 
              <input type="text" name="brugerid" size="20" maxlength="25" value="<?=$brugerid?>">
              <input type="hidden" name="brugerid_old" value="">
            </td>
          </tr>
          <tr> 
            <td width="50">Password</td>
            <td width="350"> 
              <input type="text" name="passwd" size="20" maxlength="25" value="<?=$passwd?>">
              <input type="hidden" name="passwd_old" value="">
            </td>
          </tr>
		  
          <tr> 
            <td width="50">&nbsp;</td>
            <td width="350">&nbsp;</td>
          </tr>
          <tr> 
            <td width="50">&nbsp;</td>
            <td width="350" bgcolor="#FFFFFF"> 
              <input type="submit" name="Send" value="Opdater">
              <input type="submit" name="Fortryd" value="Fortryd" onClick="FocusParentAndClose()">
          </tr>
          <tr> 
            <td width="50">&nbsp;</td>
            <td width="350">&nbsp;</td>
          </tr>
        </table>
        </form>
      <? } // End If $DoWhat 2 ?>

      <? if ($DoWhat == 3) { // Vis record ?>
        <form name="Tilsagn" method="post" action="<?=$_SERVER['SCRIPT_NAME']?>?DoWhat=5&Id=<?=$Id?>&TopWin=<?=$TopWin?>">
        <table width="100%" border="0" bgcolor="#FFFFFF">
          <tr> 
            <td width="300" colspan="3"><b>
              <font color="#003399">Ændring af Person <?=$Id?></font></b>
            </td>
          </tr>
          <tr> 
            <td width="50">&nbsp;</td>
            <td width="350">&nbsp;</td>
            <td width="350">&nbsp;</td>
          </tr>
          <tr> 
            <td width="50">Fornavn</td>
            <td width="350"> 
              <input type="text" name="Fornavn" size="15" maxlength="25" value="<?=$Fornavn?>">
              <input type="hidden" name="Fornavn_old" value="<?=$Fornavn?>">
            </td>
            <td width="350"> 
              <input readonly type="text" name="DFornavn" size="15" maxlength="25" value="<?=$DFornavn?>"
              	 <?=setcolor($Fornavn, $DFornavn);?> onclick="updatefield('Fornavn', 'DFornavn');" >
            </td>
          </tr>
          <tr> 
            <td width="50" valign="top">Efternavn</td>
            <td width="275"> 
              <input type="text" name="Efternavn" size="15" maxlength="25" value="<?=$Efternavn?>">
              <input type="hidden" name="Efternavn_old" value="<?=$Efternavn?>">
            </td>
            <td width="275"> 
              <input readonly type="text" name="DEfternavn" size="15" maxlength="25" value="<?=$DEfternavn?>"
              	 <?=setcolor($Efternavn, $DEfternavn);?> onclick="updatefield('Efternavn', 'DEfternavn');">
            </td>
          </tr>
          <tr> 
            <td width="50" valign="top">Adresse</td>
            <td width="275"> 
              <input type="text" name="Adresse" size="25" maxlength="35" value="<?=$Adresse?>">
              <input type="hidden" name="Adresse_old" value="<?=$Adresse?>">
            </td>
            <td width="275"> 
              <input readonly type="text" name="DAdresse" size="25" maxlength="35" value="<?=$DAdresse?>"
              	 <?=setcolor($Adresse, $DAdresse);?> onclick="updatefield('Adresse', 'DAdresse');">
            </td>
          </tr>
          <tr> 
            <td width="50">Postnr</td>
            <td width="350"> 
              <input type="text" name="Postnr" size="4" maxlength="8" value="<?=$Postnr?>">
              <input type="hidden" name="Postnr_old" value="<?=$Postnr?>">
            </td>
            <td width="350"> 
              <input readonly type="text" name="DPostnr" size="4" maxlength="8" value="<?=$DPostnr?>"
              	 <?=setcolor($Postnr, $DPostnr);?> onclick="updatefield('Postnr', 'DPostnr');">
            </td>
          </tr>
          <tr> 
            <td width="50">By</td>
            <td width="350"> 
              <input type="text" name="ByNavn" size="20" maxlength="25" value="<?=$ByNavn?>">
              <input type="hidden" name="ByNavn_old" value="<?=$ByNavn?>">
              <input type="hidden" name="telefonlinnr" value="<?=$telefonlinnr?>">
              <input type="hidden" name="emaillinnr" value="<?=$emaillinnr?>">
              <input type="hidden" name="Puls3060MedlemId" value="<?=$Puls3060MedlemId?>">
              <input type="hidden" name="PersonligId" value="<?=$PersonligId?>">
            </td>
            <td width="350"> 
              <input readonly type="text" name="DByNavn" size="20" maxlength="25" value="<?=$DByNavn?>"
              	 <?=setcolor($ByNavn, $DByNavn);?> onclick="updatefield('ByNavn', 'DByNavn');">
            </td>
          </tr>

          <? for($i=0; $i<$telefonlinnr; $i++) { ?>
            <tr> 
              <td width="50">Telefon</td>
              <td width="350"> 
                <input type="text" name="TlfNr<?=$i?>" size="8" maxlength="8" value="<?=$TlfNr[$i]?>">
                <input type="text" name="TlfType<?=$i?>" size="8" maxlength="8" value="<?=$TlfType[$i]?>">
                <input type="hidden" name="TelefonId<?=$i?>" value="<?=$TelefonId[$i]?>">
                <input type="hidden" name="TlfNr<?=$i?>_old" value="<?=$TlfNr[$i]?>">
                <input type="hidden" name="TlfType<?=$i?>_old" value="<?=$TlfType[$i]?>">
              </td>
              <td width="350"> 
                <input readonly type="text" name="DTlfNr<?=$i?>" size="8" maxlength="8" value="<?=$DTlfNr[$i]?>"
              	  <?=setcolor($TlfNr[$i], $DTlfNr[$i]);?> onclick="updatefield('TlfNr<?=$i?>', 'DTlfNr<?=$i?>');">
                <input readonly type="text" name="DTlfType<?=$i?>" size="8" maxlength="8" value="<?=$DTlfType[$i]?>"
              	  <?=setcolor($TlfType[$i], $DTlfType[$i]);?> onclick="updatefield('TlfType<?=$i?>', 'DTlfType<?=$i?>');">
              </td>
            </tr>
          <? } ?>

          <? for($i=0; $i<$emaillinnr; $i++) { ?>
            <tr> 
              <td width="50">E-mail</td>
              <td width="350"> 
                <input type="text" name="MailAdr<?=$i?>" size="25" maxlength="50" value="<?=$MailAdr[$i]?>">
                <input type="text" name="MailType<?=$i?>" size="8" maxlength="8" value="<?=$MailType[$i]?>">
                <input type="hidden" name="MailadresseId<?=$i?>" value="<?=$MailadresseId[$i]?>">
                <input type="hidden" name="MailAdr<?=$i?>_old" value="<?=$MailAdr[$i]?>">
                <input type="hidden" name="MailType<?=$i?>_old" value="<?=$MailType[$i]?>">
              </td>
              <td width="350"> 
                <input readonly type="text" name="DMailAdr<?=$i?>" size="25" maxlength="50" value="<?=$DMailAdr[$i]?>"
              	  <?=setcolor($MailAdr[$i], $DMailAdr[$i]);?> onclick="updatefield('MailAdr<?=$i?>', 'DMailAdr<?=$i?>');">
                <input readonly type="text" name="DMailType<?=$i?>" size="8" maxlength="8" value="<?=$DMailType[$i]?>"
              	  <?=setcolor($MailType[$i], $DMailType[$i]);?> onclick="updatefield('MailType<?=$i?>', 'DMailType<?=$i?>');">
              </td>
            </tr>
          <? } ?>
          <tr> 
            <td width="50">Indmeldt</td>
            <td width="350"> 
              <input type="text" name="Indmeldt" size="20" maxlength="25" value="<?=$Indmeldt?>">
              <input type="hidden" name="Indmeldt_old" value="<?=$Indmeldt?>">
            </td>
            <td width="350"> 
              <input readonly type="text" name="DIndmeldt" size="20" maxlength="25" value="<?=$DIndmeldt?>"
              	 <?=setcolor($Indmeldt, $DIndmeldt);?> onclick="updatefield('Indmeldt', 'DIndmeldt');">
            </td>
          </tr>
          <tr> 
            <td width="50">Betalt til</td>
            <td width="350"> 
              <input type="text" name="BetaltTilDato" size="20" maxlength="25" value="<?=$BetaltTilDato?>">
              <input type="hidden" name="BetaltTilDato_old" value="<?=$BetaltTilDato?>">
            </td>
            <td width="350"> 
              <input readonly type="text" name="DBetaltTilDato" size="20" maxlength="25" value="<?=$DBetaltTilDato?>"
              	 <?=setcolor($BetaltTilDato, $DBetaltTilDato);?> onclick="updatefield('BetaltTilDato', 'DBetaltTilDato');">
            </td>
          </tr>
         <tr> 
            <td width="50">Udmeldt</td>
            <td width="350"> 
              <input type="text" name="Udmeldt" size="20" maxlength="25" value="<?=$Udmeldt?>">
              <input type="hidden" name="Udmeldt_old" value="<?=$Udmeldt?>">
            </td>
            <td width="350"> 
              <input readonly type="text" name="DUdmeldt" size="20" maxlength="25" value="<?=$DUdmeldt?>"
              	 <?=setcolor($Udmeldt, $DUdmeldt);?> onclick="updatefield('Udmeldt', 'DUdmeldt');">
            </td>
          </tr>
          <tr> 
            <td width="50">Køn</td>
            <td width="350"> 
              <input type="text" name="Kon" size="1" maxlength="1" value="<?=$Kon?>">
              <input type="hidden" name="Kon_old" value="<?=$Kon?>">
            </td>
            <td width="350"> 
              <input readonly type="text" name="DKon" size="1" maxlength="1" value="<?=$DKon?>"
              	 <?=setcolor($Kon, $DKon);?> onclick="updatefield('Kon', 'DKon');">
            </td>
          </tr>
          <tr> 
            <td width="50">Født</td>
            <td width="350"> 
              <input type="text" name="FodtDato" size="20" maxlength="25" value="<?=$FodtDato?>">
              <input type="hidden" name="FodtDato_old" value="<?=$FodtDato?>">
            </td>
            <td width="350"> 
              <input readonly type="text" name="DFodtDato" size="20" maxlength="25" value="<?=$DFodtDato?>"
              	 <?=setcolor($FodtDato, $DFodtDato);?> onclick="updatefield('FodtDato', 'DFodtDato');">
            </td>
          </tr>
          <tr> 
            <td width="50">Født År</td>
            <td width="350"> 
              <input type="text" name="FodtAar" size="4" maxlength="4" value="<?=$FodtAar?>">
              <input type="hidden" name="FodtAar_old" value="<?=$FodtAar?>">
            </td>
            <td width="350"> 
              <input readonly type="text" name="DFodtAar" size="4" maxlength="4" value="<?=$DFodtAar?>"
              	 <?=setcolor($FodtAar, $DFodtAar);?> onclick="updatefield('FodtAar', 'DFodtAar');">
            </td>
          </tr>
          <tr> 
            <td width="50">Ingen Mail</td>
            <td width="350"> 
              <input type="text" name="nomail" size="1" maxlength="1" value="<?=$nomail?>">
              <input type="hidden" name="nomail_old" value="<?=$nomail?>">
            </td>
            <td width="350"> 
              <input readonly type="text" name="Dnomail" size="1" maxlength="1" value="<?=$Dnomail?>"
              	 <?=setcolor($nomail, $Dnomail);?> onclick="updatefield('nomail', 'Dnomail');">
            </td>
          </tr>
          <tr> 
            <td width="50">UserID</td>
            <td width="350"> 
              <input type="text" name="brugerid" size="20" maxlength="25" value="<?=$brugerid?>">
              <input type="hidden" name="brugerid_old" value="<?=$brugerid?>">
            </td>
            <td width="350"> 
              <input readonly type="text" name="Dbrugerid" size="20" maxlength="25" value="<?=$Dbrugerid?>"
              	 <?=setcolor($brugerid, $Dbrugerid);?> onclick="updatefield('brugerid', 'Dbrugerid');">
            </td>
          </tr>
          <tr> 
            <td width="50">Password</td>
            <td width="350"> 
              <input type="text" name="passwd" size="20" maxlength="25" value="<?=$passwd?>">
              <input type="hidden" name="passwd_old" value="<?=$passwd?>">
            </td>
            <td width="350"> 
              <input readonly type="text" name="Dpasswd" size="20" maxlength="25" value="<?=$Dpasswd?>"
              	 <?=setcolor($passwd, $Dpasswd);?> onclick="updatefield('passwd', 'Dpasswd');">
            </td>
          </tr>
		  
          <tr> 
            <td width="50">&nbsp;</td>
            <td width="350">&nbsp;</td>
            <td width="350">&nbsp;</td>
          </tr>
          <tr> 
            <td width="50">&nbsp;</td>
            <td width="350" bgcolor="#FFFFFF"> 
              <input type="submit" name="Send" value="Opdater">
              <input type="submit" name="Fortryd" value="Fortryd" onClick="FocusParentAndClose()">
            <td width="350">&nbsp;</td>
          </tr>
          <tr> 
            <td width="50">&nbsp;</td>
            <td width="350">&nbsp;</td>
            <td width="350">&nbsp;</td>
          </tr>
        </table>
        </form>
      <? } // End If $DoWhat 3 ?>

      <? if ($DoWhat == 5) { // Rettet record gemt eller Vis record med fejl ?>
      <? if (!$Fejl) { // Rettet record gemt ?>
         <b><font color="#003399">Kviterings tekst 5 her</font></b>
         <a href="javascript:UpdateParentAndClose()"> Retur</a> 
      <? } else { // Vis record med fejl ?>
        <form name="Tilsagn" method="post" action="<?=$_SERVER['SCRIPT_NAME']?>?DoWhat=5&Id=<?=$Id?>&TopWin=<?=$TopWin?>">
        <table width="100%" border="0" bgcolor="#FFFFFF">
          <tr> 
            <td width="300" colspan="2"><b>
              <font color="#003399">Ændring af Person</font></b>
            </td>
          </tr>
          <tr> 
            <td width="50">&nbsp;</td>
            <td width="350">&nbsp;</td>
          </tr>
          <tr> 
            <td width="50">Fornavn</td>
            <td width="350"> 
              <input type="text" name="Fornavn" size="15" maxlength="25" value="<?=$_POST["Fornavn"]?>">
              <font color="#FF0000"> 
              <?=$FornavnFejl?>
              </font> 
              <input type="hidden" name="Fornavn_old" value="<?=$_POST["Fornavn"]?>">
            </td>
          </tr>
          <tr> 
            <td width="50" valign="top">Efternavn</td>
            <td width="275"> 
              <input type="text" name="Efternavn" size="15" maxlength="25" value="<?=$_POST["Efternavn"]?>">
            </td>
            <td width="75" valign="top"> 
              <font color="#FF0000"> 
                <?=$EfternavnFejl?>
              </font> 
              <input type="hidden" name="Efternavn_old" value="<?=$_POST["Efternavn"]?>">
            </td>
          </tr>
          <tr> 
            <td width="50" valign="top">Adresse</td>
            <td width="275"> 
              <input type="text" name="Adresse" size="25" maxlength="35" value="<?=$_POST["Adresse"]?>">
            </td>
            <td width="75" valign="top"> 
              <font color="#FF0000"> 
                <?=$AdresseFejl?>
              </font> 
              <input type="hidden" name="Adresse_old" value="<?=$_POST["Adresse"]?>">
            </td>
          </tr>
          <tr> 
            <td width="50">Postnr</td>
            <td width="350"> 
              <input type="text" name="Postnr" size="4" maxlength="8" value="<?=$_POST["Postnr"]?>">
              <font color="#FF0000"> 
                <?=$PostnrFejl?>
              </font> 
              <input type="hidden" name="Postnr_old" value="<?=$_POST["Postnr"]?>">
            </td>
          </tr>
          <tr> 
            <td width="50">By</td>
            <td width="350"> 
              <input type="text" name="ByNavn" size="4" maxlength="8" value="<?=$_POST["ByNavn"]?>">
              <font color="#FF0000"> 
                <?=$ByNavnFejl?>
              </font> 
              <input type="hidden" name="ByNavn_old" value="<?=$_POST["ByNavn"]?>">
              <input type="hidden" name="telefonlinnr" value="<?=$_POST["telefonlinnr"]?>">
              <input type="hidden" name="emaillinnr" value="<?=$_POST["emaillinnr"]?>">
              <input type="hidden" name="Puls3060MedlemId" value="<?=$_POST["Puls3060MedlemId"]?>">
              <input type="hidden" name="PersonligId" value="<?=$_POST["PersonligId"]?>">
            </td>
          </tr>
          <? for($i=0; $i<$_POST["telefonlinnr"]; $i++) { ?>
            <tr> 
              <td width="50">Telefon</td>
              <td width="350"> 														  
                <input type="text" name="TlfNr<?=$i?>" size="4" maxlength="8" value="<?=$_POST["TlfNr" . $i]?>">
                <input type="text" name="TlfType<?=$i?>" size="4" maxlength="8" value="<?=$_POST["TlfType" . $i]?>">
                <input type="hidden" name="TelefonId<?=$i?>" value="<?=$_POST["TelefonId" . $i]?>">
                <input type="hidden" name="TlfNr<?=$i?>_old" value="<?=$_POST["TlfNr" . $i]?>">
                <input type="hidden" name="TlfType<?=$i?>_old" value="<?=$_POST["TlfType" . $i]?>">
              </td>
            </tr>
          <? } ?>

          <? for($i=0; $i<$_POST["emaillinnr"]; $i++) { ?>
            <tr> 
              <td width="50">E-mail</td>
              <td width="350"> 														  
                <input type="text" name="MailAdr<?=$i?>" size="4" maxlength="8" value="<?=$_POST["MailAdr" . $i]?>">
                <input type="text" name="MailType<?=$i?>" size="4" maxlength="8" value="<?=$_POST["MailType" . $i]?>">
                <input type="hidden" name="MailadresseId<?=$i?>" value="<?=$_POST["MailadresseId" . $i]?>">
                <input type="hidden" name="MailAdr<?=$i?>_old" value="<?=$_POST["MailAdr" . $i]?>">
                <input type="hidden" name="MailType<?=$i?>_old" value="<?=$_POST["MailType" . $i]?>">
              </td>
            </tr>
          <? } ?>
          <tr> 
            <td width="50">Indmeldt</td>
            <td width="350"> 
              <input type="text" name="Indmeldt" size="20" maxlength="25" value="<?=$_POST["Indmeldt"]?>">
              <font color="#FF0000"> 
                <?=$IndmeldtFejl?>
              </font> 
              <input type="hidden" name="Indmeldt_old" value="<?=$_POST["Indmeldt"]?>">
            </td>
          </tr>      
          <tr> 
            <td width="50">Betalt til</td>
            <td width="350"> 
              <input type="text" name="BetaltTilDato" size="20" maxlength="25" value="<?=$_POST["BetaltTilDato"]?>">
              <font color="#FF0000"> 
                <?=$BetaltTilDatoFejl?>
              </font> 
              <input type="hidden" name="BetaltTilDato_old" value="<?=$_POST["BetaltTilDato"]?>">
            </td>
          </tr>
          <tr> 
            <td width="50">Udmeldt</td>
            <td width="350"> 
              <input type="text" name="Udmeldt" size="20" maxlength="25" value="<?=$_POST["Udmeldt"]?>">
              <font color="#FF0000"> 
                <?=$UdmeldtFejl?>
              </font> 
              <input type="hidden" name="Udmeldt_old" value="<?=$_POST["Udmeldt"]?>">
            </td>
          </tr>
          <tr> 
            <td width="50">Køn</td>
            <td width="350"> 
              <input type="text" name="Kon" size="1" maxlength="1" value="<?=$_POST["Kon"]?>">
              <font color="#FF0000"> 
                <?=$KonFejl?>
              </font> 
              <input type="hidden" name="Kon_old" value="<?=$_POST["Kon"]?>">
            </td>
          </tr>
          <tr> 
            <td width="50">Født</td>
            <td width="350"> 
              <input type="text" name="FodtDato" size="20" maxlength="25" value="<?=$_POST["FodtDato"]?>">
              <font color="#FF0000"> 
                <?=$FodtDatoFejl?>
              </font> 
              <input type="hidden" name="FodtDato_old" value="<?=$_POST["FodtDato"]?>">
            </td>
          </tr>
          <tr> 
            <td width="50">Født År</td>
            <td width="350"> 
              <input type="text" name="FodtAar" size="4" maxlength="4" value="<?=$_POST["FodtAar"]?>">
              <font color="#FF0000"> 
                <?=$FodtAarFejl?>
              </font> 
              <input type="hidden" name="FodtAar_old" value="<?=$_POST["FodtAar"]?>">
            </td>
          </tr>
          <tr> 
            <td width="50">Ingen Mail</td>
            <td width="350"> 
              <input type="text" name="nomail" size="1" maxlength="1" value="<?=$_POST["nomail"]?>">
              <font color="#FF0000"> 
                <?=$nomailFejl?>
              </font> 
              <input type="hidden" name="nomail_old" value="<?=$_POST["nomail"]?>">
            </td>
          </tr>
          <tr> 
            <td width="50">UserID</td>
            <td width="350"> 
              <input type="text" name="brugerid" size="20" maxlength="25" value="<?=$_POST["brugerid"]?>">
              <font color="#FF0000"> 
                <?=$brugeridFejl?>
              </font> 
              <input type="hidden" name="brugerid_old" value="<?=$_POST["brugerid"]?>">
            </td>
          </tr>
          <tr> 
            <td width="50">Password</td>
            <td width="350"> 
              <input type="text" name="passwd" size="20" maxlength="25" value="<?=$_POST["passwd"]?>">
              <font color="#FF0000"> 
                <?=$passwdFejl?>
              </font> 
              <input type="hidden" name="passwd_old" value="<?=$_POST["passwd"]?>">
            </td>
          </tr>
		  
	      <tr> 
            <td width="50">&nbsp;</td>
            <td width="350">&nbsp;</td>
          </tr>
          <tr> 
            <td width="50">&nbsp; </td>
            <td width="350" bgcolor="#FFFFFF"> 
              <input type="submit" name="Send" value="Opdater">
              <input type="submit" name="Fortryd" value="Fortryd" onClick="FocusParentAndClose()">
          </tr>
          <tr> 
            <td width="50">&nbsp;</td>
            <td width="350">&nbsp;</td>
          </tr>
        </table>
        </form>
      <? } } // End If $DoWhat 5 ?>


      <? if ($DoWhat == 6) { // Ny record gemt eller Vis ny record med fejl ?>
      <? if (!$Fejl) { // Ny record gemt ?>
         <b><font color="#003399">Kviterings tekst 6 her</font></b>
         <a href="javascript:UpdateParentAndClose()"> Retur</a> 
      <? } else { // Vis ny record med fejl ?>
        <form name="Tilsagn" method="post" action="<?=$_SERVER['SCRIPT_NAME']?>?DoWhat=6&Id=<?=$Id?>&TopWin=<?=$TopWin?>">
        <table width="100%" border="0" bgcolor="#FFFFFF">
          <tr> 
            <td width="300" colspan="2"><b>
              <font color="#003399">Opret Person</font></b>
            </td>
          </tr>
          <tr> 
            <td width="50">&nbsp;</td>
            <td width="350">&nbsp;</td>
          </tr>
          <tr> 
            <td width="50">Fornavn</td>
            <td width="350"> 
              <input type="text" name="Fornavn" size="15" maxlength="25" value="<?=$_POST["Fornavn"]?>">
              <font color="#FF0000"> 
              <?=$FornavnFejl?>
              </font> 
              <input type="hidden" name="Fornavn_old" value="<?=$_POST["Fornavn"]?>">
            </td>
          </tr>
          <tr> 
            <td width="50" valign="top">Efternavn</td>
            <td width="275"> 
              <input type="text" name="Efternavn" size="15" maxlength="25" value="<?=$_POST["Efternavn"]?>">
            </td>
            <td width="75" valign="top"> 
              <font color="#FF0000"> 
                <?=$EfternavnFejl?>
              </font> 
              <input type="hidden" name="Efternavn_old" value="<?=$_POST["Efternavn"]?>">
            </td>
          </tr>
          <tr> 
            <td width="50" valign="top">Adresse</td>
            <td width="275"> 
              <input type="text" name="Adresse" size="25" maxlength="35" value="<?=$_POST["Adresse"]?>">
            </td>
            <td width="75" valign="top"> 
              <font color="#FF0000"> 
                <?=$AdresseFejl?>
              </font> 
              <input type="hidden" name="Adresse_old" value="<?=$_POST["Adresse"]?>">
            </td>
          </tr>
          <tr> 
            <td width="50">Postnr</td>
            <td width="350"> 
              <input type="text" name="Postnr" size="4" maxlength="8" value="<?=$_POST["Postnr"]?>">
              <font color="#FF0000"> 
                <?=$PostnrFejl?>
              </font> 
              <input type="hidden" name="Postnr_old" value="<?=$_POST["Postnr"]?>">
            </td>
          </tr>
          <tr> 
            <td width="50">By</td>
            <td width="350"> 
              <input type="text" name="ByNavn" size="4" maxlength="8" value="<?=$_POST["ByNavn"]?>">
              <font color="#FF0000"> 
                <?=$ByNavnFejl?>
              </font> 
              <input type="hidden" name="ByNavn_old" value="<?=$_POST["ByNavn"]?>">
              <input type="hidden" name="telefonlinnr" value="<?=$_POST["telefonlinnr"]?>">
              <input type="hidden" name="emaillinnr" value="<?=$_POST["emaillinnr"]?>">
              <input type="hidden" name="Puls3060MedlemId" value="<?=$_POST["Puls3060MedlemId"]?>">
              <input type="hidden" name="PersonligId" value="<?=$_POST["PersonligId"]?>">
            </td>
          </tr>
          <? for($i=0; $i<$_POST["telefonlinnr"]; $i++) { ?>
            <tr> 
              <td width="50">Telefon</td>
              <td width="350"> 														  
                <input type="text" name="TlfNr<?=$i?>" size="4" maxlength="8" value="<?=$_POST["TlfNr" . $i]?>">
                <input type="text" name="TlfType<?=$i?>" size="4" maxlength="8" value="<?=$_POST["TlfType" . $i]?>">
                <input type="hidden" name="TelefonId<?=$i?>" value="<?=$_POST["TelefonId" . $i]?>">
                <input type="hidden" name="TlfNr<?=$i?>_old" value="<?=$_POST["TlfNr" . $i]?>">
                <input type="hidden" name="TlfType<?=$i?>_old" value="<?=$_POST["TlfType" . $i]?>">
              </td>
            </tr>
          <? } ?>

          <? for($i=0; $i<$_POST["emaillinnr"]; $i++) { ?>
            <tr> 
              <td width="50">E-mail</td>
              <td width="350"> 														  
                <input type="text" name="MailAdr<?=$i?>" size="4" maxlength="8" value="<?=$_POST["MailAdr" . $i]?>">
                <input type="text" name="MailType<?=$i?>" size="4" maxlength="8" value="<?=$_POST["MailType" . $i]?>">
                <input type="hidden" name="MailadresseId<?=$i?>" value="<?=$_POST["MailadresseId" . $i]?>">
                <input type="hidden" name="MailAdr<?=$i?>_old" value="<?=$_POST["MailAdr" . $i]?>">
                <input type="hidden" name="MailType<?=$i?>_old" value="<?=$_POST["MailType" . $i]?>">
              </td>
            </tr>
          <? } ?>
      
          <tr> 
            <td width="50">&nbsp;</td>
            <td width="350">&nbsp;</td>
          </tr>
          <tr> 
            <td width="50">&nbsp; </td>
            <td width="350" bgcolor="#FFFFFF"> 
              <input type="submit" name="Send" value="Opdater">
              <input type="submit" name="Fortryd" value="Fortryd" onClick="FocusParentAndClose()">
          </tr>
          <tr> 
            <td width="50">&nbsp;</td>
            <td width="350">&nbsp;</td>
          </tr>
        </table>
        </form>
      <? } } // End If $DoWhat 6 ?>

      </td>
      <td bgcolor="#99CC00">&nbsp;</td>
    </tr>
    <tr bgcolor="#99CC00"> 
      <td width="20" height="20" bgcolor="#99CC00">&nbsp;</td>
      <td width="850" height="20">&nbsp;</td>
      <td height="20" bgcolor="#99CC00">&nbsp;</td>
    </tr>
  </table>
</body>
</html>

<script language="javascript">
<!--
<?if ($TopWin=="/admin/PListe2.php"){ ?>
  <?if ($DoWhat==5){ ?>
    // For 5=Update Person
    function UpdateParentAndClose(){
      var ParentWindow;
	  ParentWindow = top.opener;
	  ParentWindow.focus();
	  window.close();
    }
  <? } // endif 5 ?>
  <?if ($DoWhat==6){ ?>
    // For 6=Add Person
    function UpdateParentAndClose(){
      var ParentWindow;
	  ParentWindow = top.opener;
	  ParentWindow.focus();
	  window.close();
    }
  <? } // endif 6 ?>
  function FocusParentAndClose(){
    var ParentWindow;
	ParentWindow = top.opener;
	ParentWindow.focus();
	window.close();
  }
<? } // endif /admin/PListe3.php ?>
<?if ($TopWin=="/admin/PListe3.php"){ ?>
  <?if ($DoWhat==5){ ?>
    // For 5=Update Person
    function UpdateParentAndClose(){
      var ParentWindow;
	  ParentWindow = top.opener;
	  ParentWindow.focus();
	  window.close();
    }
  <? } // endif 5 ?>
  <?if ($DoWhat==6){ ?>
    // For 6=Add Person
    function UpdateParentAndClose(){
      var ParentWindow;
      var ParentParentWindow;
      var SPId;
      var Search;
	  ParentWindow = top.opener;
	  ParentParentWindow = ParentWindow.top.opener;
      SPId = ParentParentWindow.document.all['SPId'];
	  SPId.value=<?=$Id?>;
      Search = ParentParentWindow.document.all['Search'];
	  Search.submit();
	  ParentWindow.close();
	  window.close();
    }
  <? } // endif 6 ?>
  function FocusParentAndClose(){
    var ParentWindow;
	ParentWindow = top.opener;
	ParentWindow.focus();
	window.close();
  }
<? } // endif /admin/PListe3.php ?>

<?if ($TopWin=="/admin/PListe4.php"){ ?>
  <?if ($DoWhat==5){ ?>
    // For 5=Update Person
    function UpdateParentAndClose(){
      var ParentWindow;
      var UpdatePersonId;
	  ParentWindow = top.opener;
      UpdatePersonId = ParentWindow.document.all['UpdatePersonId'];
	  UpdatePersonId.submit();
	  window.close();
    }
    function FocusParentAndClose(){
      var ParentWindow;
      var UpdatePersonId;
	  ParentWindow = top.opener;
      UpdatePersonId = ParentWindow.document.all['UpdatePersonId'];
	  UpdatePersonId.submit();
	  window.close();
  }
  <? } // endif 5 ?>
  <?if ($DoWhat==6){ ?>
    // For 6=Add Person
    function UpdateParentAndClose(){
      var ParentWindow;
      var ParentParentWindow;
      var SPId;
      var UpdatePersonId;
	  ParentWindow = top.opener;
	  ParentParentWindow = ParentWindow.top.opener;
      SPId = ParentParentWindow.document.all['SPId'];
	  SPId.value=<?=$Id?>;
      UpdatePersonId = ParentParentWindow.document.all['UpdatePersonId'];
	  UpdatePersonId.submit();
	  ParentWindow.close();
	  window.close();
    }
    function FocusParentAndClose(){
      var ParentWindow;
	  ParentWindow = top.opener;
	  ParentWindow.focus();
	  window.close();
    }
  <? } // endif 6 ?>
<? } // endif /admin/PListe4.php ?>

  function fonLoad(){
	var h = 645;
	var w = 650;
	window.resizeTo(w, h+((<?=$telefonlinnr?>+<?=$emaillinnr?>)*27));
  }
  
  function updatefield(toName, fromName){
      if (document.all[fromName].value=="")
        return;
      document.all[toName].value = document.all[fromName].value;
      document.all[fromName].className='color2';
  }
// -->
</script>
