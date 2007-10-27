<? header ("Last-Modified: " . gmdate("D, d M Y H:i:s") . " GMT"); // always modified ?>
<html>
<script language=Javascript src='/Editor/scripts/language/danish/editor_lang.js'></script>
<script language=JavaScript src='/Editor/scripts/innovaeditor.js'></script>
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

setlocale (LC_TIME, "da_DK.ISO8859-1");

include_once("conn.inc");
$dbLink = pg_connect($conn_www);

function encodeHTML($sHTML)
{
	$sHTML=ereg_replace("&","&amp;",$sHTML);
	$sHTML=ereg_replace("<","&lt;",$sHTML);
	$sHTML=ereg_replace(">","&gt;",$sHTML);
	return $sHTML;
}

if (isset($_REQUEST['TopWin']))
$TopWin=$_REQUEST['TopWin'];
else
$TopWin = $_SERVER['SCRIPT_NAME'];

if (isset($_REQUEST['DoWhat']))
$DoWhat=$_REQUEST['DoWhat'];
else
$DoWhat = 0;

if (isset($_REQUEST['KLinieID']))
$KLinieID=$_REQUEST['KLinieID'];
else
$KLinieID = 0;

if (isset($_REQUEST['line']))
$line=$_REQUEST['line'];
else
$line = 0;

if (isset($_REQUEST['KLinieDato']))
$KLinieDato=$_REQUEST['KLinieDato'];
else
$KLinieDato = "";

if (isset($_REQUEST['KLinieText']))
$KLinieText=$_REQUEST['KLinieText'];
else
$KLinieText = "";

if (isset($_REQUEST['KLinieSted']))
$KLinieSted=$_REQUEST['KLinieSted'];
else
$KLinieSted = "";

if (isset($_REQUEST['Info']))
$Info=$_REQUEST['Info'];
else
$Info = 0;

if (isset($_REQUEST['Link']))
$Link=$_REQUEST['Link'];
else
$Link = "";

if (isset($_REQUEST['PersonGruppeId']))
$PersonGruppeId=$_REQUEST['PersonGruppeId'];
else
$PersonGruppeId = "";

if (isset($_REQUEST['klines']))
$klines=$_REQUEST['klines'];
else
$klines = "";

if (isset($_REQUEST['newwindow']))
$newwindow=$_REQUEST['newwindow'];
else
$newwindow = 0;

if (isset($_REQUEST['etilmelding']))
$etilmelding=$_REQUEST['etilmelding'];
else
$etilmelding = 0;

if (isset($_REQUEST['hidden_']))
$hidden_=$_REQUEST['hidden_'];
else
$hidden_ = 0;

if (isset($_REQUEST['tilmeldingslut']))
$tilmeldingslut=$_REQUEST['tilmeldingslut'];
else
$tilmeldingslut = 0;

if (isset($_REQUEST['klinieoverskrift']))
$klinieoverskrift=$_REQUEST['klinieoverskrift'];
else
$klinieoverskrift = "";

if (isset($_REQUEST['aktafgift']))
$aktafgift=$_REQUEST['aktafgift'];
else
$aktafgift = 0;

if (isset($_REQUEST['KLinieDato_old']))
$KLinieDato_old=$_REQUEST['KLinieDato_old'];
else
$KLinieDato_old = "";

if (isset($_REQUEST['KLinieText_old']))
$KLinieText_old=$_REQUEST['KLinieText_old'];
else
$KLinieText_old = "";

if (isset($_REQUEST['KLinieSted_old']))
$KLinieSted_old=$_REQUEST['KLinieSted_old'];
else
$KLinieSted_old = "";

if (isset($_REQUEST['Info_old']))
$Info_old=$_REQUEST['Info_old'];
else
$Info_old = "";

if (isset($_REQUEST['Link_old']))
$Link_old=$_REQUEST['Link_old'];
else
$Link_old = "";

if (isset($_REQUEST['PersonGruppeId_old']))
$PersonGruppeId_old=$_REQUEST['PersonGruppeId_old'];
else
$PersonGruppeId_old = "";

if (isset($_REQUEST['klines_old']))
$klines_old=$_REQUEST['klines_old'];
else
$klines_old = "";

if (isset($_REQUEST['newwindow_old']))
$newwindow_old=$_REQUEST['newwindow_old'];
else
$newwindow_old = "";

if (isset($_REQUEST['etilmelding_old']))
$etilmelding_old=$_REQUEST['etilmelding_old'];
else
$etilmelding_old = "";

if (isset($_REQUEST['hidden__old']))
$hidden__old=$_REQUEST['hidden__old'];
else
$hidden__old = "";

if (isset($_REQUEST['tilmeldingslut_old']))
$tilmeldingslut_old=$_REQUEST['tilmeldingslut_old'];
else
$tilmeldingslut_old = "";

if (isset($_REQUEST['klinieoverskrift_old']))
$klinieoverskrift_old=$_REQUEST['klinieoverskrift_old'];
else
$klinieoverskrift_old = "";

if (isset($_REQUEST['aktafgift_old']))
$aktafgift_old=$_REQUEST['aktafgift_old'];
else
$aktafgift_old = "";

$Fejl   = 0;
$onLoad = "fonLoad(); MM_preloadImages('/images/vpil_f2.gif');";

switch($DoWhat)
{
	case 2:  // Vis Tom Record

	$line = 0;
	break;

	case 3:  // Vis Record

	$Query="SELECT * FROM tblKLinie WHERE KLinieID=$KLinieID";
	if ($dbResult = pg_query($dbLink, $Query))
	if ($row = pg_fetch_array($dbResult, NULL , PGSQL_ASSOC)) {
		$KLinieDato       = $row["kliniedato"];
		$KLinieText       = $row["klinietext"];
		$KLinieSted       = $row["kliniested"];
		$Info             = $row["info"];
		$Link             = $row["link"];
		$PersonGruppeId   = $row["persongruppeid"];
		$klines 		  = $row["klines"];
		$newwindow 		  = $row["newwindow"];
		$etilmelding 	  = $row["etilmelding"];
		$hidden_ 		  = $row["hidden"];
		$tilmeldingslut   = $row["tilmeldingslut"];
		$klinieoverskrift = $row["klinieoverskrift"];
		$aktafgift 		  = $row["aktafgift"];
	}

	$line = 0;
	if (isset($PersonGruppeId)){
		$Query="SELECT PersonId FROM tblPersonRef WHERE PersonGruppeId=$PersonGruppeId";
		if ($dbResult = pg_query($dbLink, $Query))
		while($row = pg_fetch_array($dbResult, NULL , PGSQL_ASSOC))
		$PersonId[$line++] = $row["personid"];
	}
	$PersonId[$line++] = 0;
	break;

	case 5:  // Gem rettet Record

	if (!$KLinieDato) {
		$KLinieDatoFejl = "skal udfyldes";
		$Fejl = 1;
	}

	if (!$klinieoverskrift) {
	  $klinieoverskriftFejl = "skal udfyldes";
	  $Fejl = 1;
	}

	if (!$KLinieText) {
		$KLinieTextFejl = "skal udfyldes";
		$Fejl = 1;
	}

	//if (!$KLinieSted) {
	//  $KLinieStedFejl = "skal udfyldes";
	//  $Fejl = 1;
	//}

	//if (!$Info) {
	//  $InfoFejl = "skal udfyldes";
	//  $Fejl = 1;
	//}

	//if (!$Link) {
	//  $LinkFejl = "skal udfyldes";
	//  $Fejl = 1;
	//}

	//if (!$PersonGruppeId) {
	//  $PersonGruppeIdFejl = "skal udfyldes";
	//  $Fejl = 1;
	//}

	if (!$Fejl){
		$set ="SET ";
		$fset =0;
		if ($KLinieDato != $KLinieDato_old){
			if ($fset) $set .= ", ";
			$set .="kliniedato = '$KLinieDato'";
			$fset =1;
		}
		if ($KLinieText != $KLinieText_old){
			if ($fset) $set .= ", ";
			$set .="klinietext = '$KLinieText'";
			$fset =1;
		}

		if ($KLinieSted != $KLinieSted_old){
			if ($fset) $set .= ", ";
			$set .="kliniested = '$KLinieSted'";
			$fset =1;
		}

		if ($Info != $Info_old){
			if ($fset) $set .= ", ";
			$set .="Info = '$Info'";
			$fset =1;
		}

		if ($Link != $Link_old){
			if ($fset) $set .= ", ";
			$set .="Link = '$Link'";
			$fset =1;
		}

		if ($klines != $klines_old){
			if ($fset) $set .= ", ";
			$set .="klines = '$klines'";
			$fset =1;
		}

		if ($newwindow != $newwindow_old){
			if ($fset) $set .= ", ";
			$set .="newwindow = '$newwindow'";
			$fset =1;
		}

		if ($etilmelding != $etilmelding_old){
			if ($fset) $set .= ", ";
			$set .="etilmelding = '$etilmelding'";
			$fset =1;
		}

		if ($hidden_ != $hidden__old){
			if ($fset) $set .= ", ";
			$set .="hidden = '$hidden_'";
			$fset =1;
		}

		if ($tilmeldingslut != $tilmeldingslut_old){
			if ($fset) $set .= ", ";
			$set .="tilmeldingslut = '$tilmeldingslut'";
			$fset =1;
		}

		if ($klinieoverskrift != $klinieoverskrift_old){
			if ($fset) $set .= ", ";
			$set .="klinieoverskrift = '$klinieoverskrift'";
			$fset =1;
		}

		if ($aktafgift != $aktafgift_old){
			if ($fset) $set .= ", ";
			$set .="aktafgift = $aktafgift";
			$fset =1;
		}

		// Test om et PersonId er ændret
		$chg =0;
		for($i=0; (($i<$_POST["line"]) && (!$chg)) ; $i++)
		if ($_POST["PersonId" . $i] != $_POST["PersonId" . $i . "_old"])
		$chg =1;

		// Opret en privat gruppe hvis PersonId er ændret
		if ($chg)
		{
			//$PersonGruppeId = ClearPrivatPersonGruppe($PersonGruppeId_old);
			//for($i=0; $i<$_POST["line"] ; $i++)
			//  InsertPersonIntoPersonGruppe($PersonGruppeId, $_POST["PersonId" . $i], 0);
			$get_persongruppe_id_parm = "null";
			for($i=0; ($i<7) ; $i++)
			{
				if ($i<$_POST["line"])
				if ($_POST["PersonId" . $i] > 0)
				$get_persongruppe_id_parm .= ", " . $_POST["PersonId" . $i];
				else
				$get_persongruppe_id_parm .= ", null";
				else
				$get_persongruppe_id_parm .= ", null";
			}
			$PersonGruppeId = get_persongruppe_id($get_persongruppe_id_parm);
		}

		if ($PersonGruppeId != $PersonGruppeId_old){
			if ($fset) $set .= ", ";
			$set .="PersonGruppeId = '$PersonGruppeId'";
			$fset =1;
		}

		if ($fset) {
			$Query=" UPDATE tblKLinie " . $set . " WHERE KLinieID = $KLinieID";
			$dbResult = pg_query($dbLink, $Query);
		}
		////print($Query);
		$onLoad = "UpdateParentAndClose()";
	}
	break;

	case 6:  // Opret Record

	if (!$KLinieDato) {
		$KLinieDatoFejl = "skal udfyldes";
		$Fejl = 1;
	}

	if (!$klinieoverskrift) {
	  $klinieoverskriftFejl = "skal udfyldes";
	  $Fejl = 1;
	}

	if (!$KLinieText) {
		$KLinieTextFejl = "skal udfyldes";
		$Fejl = 1;
	}

	//if (!$KLinieSted) {
	//  $KLinieStedFejl = "skal udfyldes";
	//  $Fejl = 1;
	//}

	//if (!$Info) {
	//  $InfoFejl = "skal udfyldes";
	//  $Fejl = 1;
	//}

	//if (!$Link) {
	//  $LinkFejl = "skal udfyldes";
	//  $Fejl = 1;
	//}

	//if (!$PersonGruppeId) {
	//  $PersonGruppeIdFejl = "skal udfyldes";
	//  $Fejl = 1;
	//}

	if (!$Fejl){
		$insert =" (kliniedato";
		$value =" VALUES ('$KLinieDato'";
		$fset =1;

		if ($fset) $insert .= ", ";
		if ($fset) $value .= ", ";
		$insert .="klinietext";
		$value .="'$KLinieText'";
		$fset =1;

		if ($KLinieSted){
			if ($fset) $insert .= ", ";
			if ($fset) $value .= ", ";
			$insert .="kliniested";
			$value .="'$KLinieSted'";
			$fset =1;
		}

		if ($Info){
			if ($fset) $insert .= ", ";
			if ($fset) $value .= ", ";
			$insert .="info";
			$value .="'$Info'";
			$fset =1;
		}

		if ($Link){
			if ($fset) $insert .= ", ";
			if ($fset) $value .= ", ";
			$insert .="link";
			$value .="'$Link'";
			$fset =1;
		}

		if ($klines){
			if ($fset) $insert .= ", ";
			if ($fset) $value .= ", ";
			$insert .="klines";
			$value .="'$klines'";
			$fset =1;
		}

		if ($newwindow){
			if ($fset) $insert .= ", ";
			if ($fset) $value .= ", ";
			$insert .="newwindow";
			$value .="'$newwindow'";
			$fset =1;
		}

		if ($etilmelding){
			if ($fset) $insert .= ", ";
			if ($fset) $value .= ", ";
			$insert .="etilmelding";
			$value .="'$etilmelding'";
			$fset =1;
		}

		if ($hidden_){
			if ($fset) $insert .= ", ";
			if ($fset) $value .= ", ";
			$insert .="hidden";
			$value .="'$hidden_'";
			$fset =1;
		}

		if ($tilmeldingslut){
			if ($fset) $insert .= ", ";
			if ($fset) $value .= ", ";
			$insert .="tilmeldingslut";
			$value .="'$tilmeldingslut'";
			$fset =1;
		}

		if ($klinieoverskrift){
			if ($fset) $insert .= ", ";
			if ($fset) $value .= ", ";
			$insert .="klinieoverskrift";
			$value .="'$klinieoverskrift'";
			$fset =1;
		}

		if ($aktafgift){
			if ($fset) $insert .= ", ";
			if ($fset) $value .= ", ";
			$insert .="aktafgift";
			$value .="$aktafgift";
			$fset =1;
		}
		
		if ($fset) {
			$insert .=") ";
			$value .=")";
			$Query=" INSERT INTO tblklinie " . $insert . $value;
			$dbResult = pg_query($dbLink, $Query);
		}

		//////print($Query);
		$onLoad = "UpdateParentAndClose()";
	}
	break;

	default:  // ingen case valgt

} // End Switch($DoWhat)
?>

<head>
<title>AktivitetskalenderUpd</title>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
</head>
  <body bgcolor="#FFFFFF" text="#000000" onLoad="<?=$onLoad?>">

      <? if ($DoWhat == 2) { // Vis tom record ?>
        <form name="Tilsagn" method="post" action="AktivitetskalenderUpd.php?DoWhat=6">
        <table width="100%" border="0" bgcolor="#FFFFFF">
          <tr> 
            <td width="300" colspan="2"><b>
              <font color="#003399">Oprettelse af Aktivitetskalender aktivitet</font></b>
            </td>
          </tr>
          <tr> 
            <td width="50">&nbsp;</td>
            <td width="350">&nbsp;</td>
          </tr>

          <tr> 
            <td width="50">Dato</td>
            <td width="350"> 
              <input type="text" name="KLinieDato" size="15" maxlength="25" value="<?=$KLinieDato?>">
              <input type="hidden" name="KLinieDato_old" value="<?=$KLinieDato?>">
            </td>
          </tr>
          
          <tr> 
            <td width="75" valign="top">Overskrift</td>
            <td width="275"> 
              <input type="text" name="klinieoverskrift" size="55" value="<?=$klinieoverskrift?>" maxlength="250">
              <input type="hidden" name="klinieoverskrift_old" value="<?=$klinieoverskrift?>">
            </td>
          </tr>
          
          <tr> 
            <td width="75" valign="top">Aktivitet</td>
            <td width="275"> 
              <textarea name="KLinieText" cols="25" rows="4">
                <?=$KLinieText?>
              </textarea>
			  <script> //STEP 2: Replace the textarea (KLinieText)
				var oEdit1 = new InnovaEditor("oEdit1");
				oEdit1.width=250;
				oEdit1.height=250;
				oEdit1.features=["FullScreen","Preview","Print","Search",
					"Cut","Copy","Paste","PasteWord","PasteText","|","Undo","Redo","|",
					"ForeColor","BackColor","|","Bookmark","Hyperlink","XHTMLSource","BRK",
					"Numbering","Bullets","|","Indent","Outdent","LTR","RTL","|",
					"Image","Flash","Media","|","Table","Guidelines","Absolute","|",
					"Characters","Line","Form","RemoveFormat","ClearAll","BRK",
					"StyleAndFormatting","TextFormatting","ListFormatting","BoxFormatting",
					"ParagraphFormatting","CssText","Styles","|",
					"Paragraph","FontName","FontSize","|",
					"Bold","Italic","Underline","Strikethrough","|",
					"JustifyLeft","JustifyCenter","JustifyRight","JustifyFull"];
				oEdit1.REPLACE("KLinieText");//Specify the id of the textarea here
			  </script>
              <input type="hidden" name="KLinieText_old" value="<?=$KLinieText?>">
            </td>
          </tr>
          <tr> 
            <td width="75" valign="top">Sted</td>
            <td width="275"> 
              <textarea name="KLinieSted" cols="50" rows="4"><?=$KLinieSted?></textarea>
              <input type="hidden" name="KLinieSted_old" value="<?=$KLinieSted?>">
            </td>
          </tr>
          <tr> 
            <td width="75" valign="top">Info</td>
            <td width="275"> 
              <input type="checkbox" name="Info" value="1" <? if ($Info == 1) { ?>CHECKED<? } ?>>
              <input type="hidden" name="Info_old" value="<?=$Info?>">
            </td>
          </tr>
          <tr> 
            <td width="75" valign="top">Link</td>
            <td width="275"> 
              <input type="text" name="Link" size="55" value="<?=$Link?>" maxlength="250">
              <input type="hidden" name="Link_old" value="<?=$Link?>">
            </td>
          </tr>
          <tr> 
            <td width="75" valign="top">Linier</td>
            <td width="275"> 
              <input type="text" name="klines"  size="1" value="<?=$klines?>" maxlength="2">
              <input type="hidden" name="klines_old" value="<?=$klines?>">
            </td>
          </tr>

          <tr> 
            <td width="75" valign="top">Nyt vindue</td>
            <td width="275"> 
              <input type="checkbox" name="newwindow" value="1" <? if ($newwindow == 1) { ?>CHECKED<? } ?>>
              <input type="hidden" name="newwindow_old" value="<?=$newwindow?>">
            </td>
          </tr>

          <tr> 
            <td width="75" valign="top">e-tilmelding</td>
            <td width="275"> 
              <input type="checkbox" name="etilmelding" value="1" <? if ($etilmelding == 1) { ?>CHECKED<? } ?>>
              <input type="hidden" name="etilmelding_old" value="<?=$etilmelding?>">
            </td>
          </tr>

          <tr> 
            <td width="50">Afgift</td>
            <td width="350"> 
              <input type="text" name="aktafgift" size="10" maxlength="10" value="<?=$aktafgift?>">
              <input type="hidden" name="aktafgift_old" value="<?=$aktafgift?>">
            </td>
          </tr>

          <tr> 
            <td width="50">SlutDato</td>
            <td width="350"> 
              <input type="text" name="tilmeldingslut" size="15" maxlength="25" value="<?=$tilmeldingslut?>">
              <input type="hidden" name="tilmeldingslut_old" value="<?=$tilmeldingslut?>">
            </td>
          </tr>

          <tr> 
            <td width="75" valign="top">Skjult</td>
            <td width="275"> 
              <input type="checkbox" name="hidden_" value="1" <? if ($hidden_ == 1) { ?>CHECKED<? } ?>>
              <input type="hidden" name="hidden__old" value="<?=$hidden_?>">
            </td>
          </tr>
           <tr> 
            <td width="75">&nbsp;</td>
            <td width="350">&nbsp;</td>
          </tr>
          <tr> 
            <td width="75">&nbsp;</td>
            <td width="350" bgcolor="#FFFFFF"> 
              <input type="submit" name="Send" value="Opret">
              <input type="submit" name="Fortryd" value="Fortryd" onClick="FocusParentAndClose()">
          </tr>
          <tr> 
            <td width="75">&nbsp;</td>
            <td width="350">&nbsp;</td>
          </tr>
        </table>
        </form>
      <? } // End If $DoWhat 2 ?>
      
      <? if ($DoWhat == 3) { // Vis record ?>
        <form name="Tilsagn" method="post" action="AktivitetskalenderUpd.php?DoWhat=5&KLinieID=<?=$KLinieID?>">
        <table width="100%" border="0" bgcolor="#FFFFFF">
          <tr> 
            <td width="300" colspan="2"><b> <font color="#003399">Ændring af Aktivitetskalender 
              aktivitet</font></b> </td>
          </tr>
          <tr> 
            <td width="50">&nbsp;</td>
            <td width="350">&nbsp;</td>
          </tr>

          <tr> 
            <td width="50">Aktnr</td>
            <td width="350"> 
              <?=$KLinieID?>
            </td>
          </tr>

          <tr> 
            <td width="75">Dato</td>
            <td width="350"> 
              <input type="text" name="KLinieDato" size="15" maxlength="25" value="<?=$KLinieDato?>">
              <input type="hidden" name="KLinieDato_old" value="<?=$KLinieDato?>">
            </td>
          </tr>
          <tr> 
            <td width="75" valign="top">Overskrift</td>
            <td width="275"> 
              <input type="text" name="klinieoverskrift" size="55" value="<?=$klinieoverskrift?>" maxlength="250">
              <input type="hidden" name="klinieoverskrift_old" value="<?=$klinieoverskrift?>">
            </td>
          </tr>
          <tr> 
            <td width="75" valign="top">Aktivitet</td>
            <td width="275"> 
              <textarea id="KLinieText" name="KLinieText" cols="10" rows="4">
				<?
				if(isset($KLinieText)) {
					$sContent=stripslashes($KLinieText); /*** remove (/) slashes ***/
					echo encodeHTML($sContent);
				}
				?>              	
              </textarea>
			  <script> //STEP 2: Replace the textarea (KLinieText)
				var oEdit1 = new InnovaEditor("oEdit1");
				oEdit1.width=250;
				oEdit1.height=250;
				oEdit1.features=["FullScreen","Preview","Print","Search",
					"Cut","Copy","Paste","PasteWord","PasteText","|","Undo","Redo","|",
					"ForeColor","BackColor","|","Bookmark","Hyperlink","XHTMLSource","BRK",
					"Numbering","Bullets","|","Indent","Outdent","LTR","RTL","|",
					"Image","Flash","Media","|","Table","Guidelines","Absolute","|",
					"Characters","Line","Form","RemoveFormat","ClearAll","BRK",
					"StyleAndFormatting","TextFormatting","ListFormatting","BoxFormatting",
					"ParagraphFormatting","CssText","Styles","|",
					"Paragraph","FontName","FontSize","|",
					"Bold","Italic","Underline","Strikethrough","|",
					"JustifyLeft","JustifyCenter","JustifyRight","JustifyFull"];
				oEdit1.REPLACE("KLinieText");//Specify the id of the textarea here
			  </script>
			  <input type="hidden" name="KLinieText_old" value='<?=encodeHTML($sContent);?>'>
            </td>
          </tr>
          <tr> 
            <td width="75" valign="top">Sted</td>
            <td width="275"> 
              <textarea name="KLinieSted" cols="50" rows="4"><?=$KLinieSted?></textarea>
              <input type="hidden" name="KLinieSted_old" value="<?=$KLinieSted?>">
            </td>
          </tr>
          <tr> 
            <td width="75" valign="top">Info</td>
            <td width="275"> 
              <input type="checkbox" name="Info" value="1" <? if ($Info == 1) { ?>CHECKED<? } ?>>
              <input type="hidden" name="Info_old" value="<?=$Info?>">
            </td>
          </tr>
          <tr> 
            <td width="75" valign="top">Link</td>
            <td width="275"> 
              <input type="text" name="Link" size="55" value="<?=$Link?>" maxlength="250">
              <input type="hidden" name="Link_old" value="<?=$Link?>">
            </td>
          </tr>
          <tr> 
            <td width="75" valign="top">Linier</td>
            <td width="275"> 
              <input type="text" name="klines" size="1" value="<?=$klines?>" maxlength="1">
              <input type="hidden" name="klines_old" value="<?=$klines?>">
            </td>
          </tr>

          <tr> 
            <td width="75" valign="top">Nyt vindue</td>
            <td width="275"> 
              <input type="checkbox" name="newwindow" value="1" <? if ($newwindow == 1) { ?>CHECKED<? } ?>>
              <input type="hidden" name="newwindow_old" value="<?=$newwindow?>">
            </td>
          </tr>

          <tr> 
            <td width="75" valign="top">e-tilmelding</td>
            <td width="275"> 
              <input type="checkbox" name="etilmelding" value="1" <? if ($etilmelding == 1) { ?>CHECKED<? } ?>>
              <input type="hidden" name="etilmelding_old" value="<?=$etilmelding?>">
            </td>
          </tr>
          <tr> 
            <td width="50">Afgift</td>
            <td width="350"> 
              <input type="text" name="aktafgift" size="10" maxlength="10" value="<?=$aktafgift?>">
              <input type="hidden" name="aktafgift_old" value="<?=$aktafgift?>">
            </td>
          </tr>

          <tr> 
            <td width="50">SlutDato</td>
            <td width="350"> 
              <input type="text" name="tilmeldingslut" size="15" maxlength="25" value="<?=$tilmeldingslut?>">
              <input type="hidden" name="tilmeldingslut_old" value="<?=$tilmeldingslut?>">
            </td>
          </tr>

          <tr> 
            <td width="75" valign="top">Skjult</td>
            <td width="275"> 
              <input type="checkbox" name="hidden_" value="1" <? if ($hidden_ == 1) { ?>CHECKED<? } ?>>
              <input type="hidden" name="hidden__old" value="<?=$hidden_?>">
            </td>
          </tr>

          <tr> 
            <td width="75">Grp</td>
            <td width="350"> 
              <input type="text" name="PersonGruppeId" size="4" maxlength="8" value="<?=$PersonGruppeId?>">
              <?=GetPersonGruppeNavn($PersonGruppeId)?>	
              <input type="hidden" name="PersonGruppeId_old" value="<?=$PersonGruppeId?>">
              <input type="hidden" name="line" value="<?=$line?>">
            </td>
          </tr>
          <? for($i=0; $i<$line; $i++) { 
          	$Buttom = "<a href=\"javascript:PListLink(" . $i . ")\" onMouseOut=\"MM_swapImgRestore();\" onMouseOver=\"MM_swapImage('vpil" . $i . "','','/images/vpil_f2.gif',1);\" ><img name=\"vpil" . $i . "\" src=\"/images/vpil.gif\" width=\"15\" height=\"15\" border=\"0\"></a>";
          ?>
            <tr> 
              <td width="75">&nbsp;</td>
              <td width="350"> 
                <input type="text" name="PersonId<?=$i?>" size="4" maxlength="8" value="<?=$PersonId[$i]?>">
                <?=$Buttom?>&nbsp;<a name="PersonNavn<?=$i?>"><?=GetPersonNavn($PersonId[$i])?></a>	
                <input type="hidden" name="PersonId<?=$i?>_old" size="4" maxlength="8" value="<?=$PersonId[$i]?>">
              </td>
            </tr>
          <? } ?>
          <tr> 
            <td width="75">&nbsp;</td>
            <td width="350">&nbsp;</td>
          </tr>
          <tr> 
            <td width="75">&nbsp;</td>
            <td width="350" bgcolor="#FFFFFF"> 
              <input type="submit" name="Send" value="Opdater">
              <input type="submit" name="Fortryd" value="Fortryd" onClick="FocusParentAndClose()">
          </tr>
          <tr> 
            <td width="75">&nbsp;</td>
            <td width="350">&nbsp;</td>
          </tr>
        </table>
        </form>
      <? } // End If $DoWhat 3 ?>

      <? if ($DoWhat == 5) { // Rettet record gemt eller Vis record med fejl ?>
      <? if (!$Fejl) { // Rettet record gemt ?>
         <b><font color="#003399">Kviterings tekst her</font></b>
         <a href="javascript:UpdateParentAndClose()"> Retur</a> 
      <? } else { // Vis record med fejl ?>
        <form name="Tilsagn" method="post" action="AktivitetskalenderUpd.php?DoWhat=5&KLinieID=<?=$KLinieID?>">
        <table width="100%" border="0" bgcolor="#FFFFFF">
          <tr> 
            <td width="300" colspan="2"><b> <font color="#003399">Ændring af Aktivitetskalender aktivitet</font></b> </td>
          </tr>
          <tr> 
            <td width="50">&nbsp;</td>
            <td width="350">&nbsp;</td>
          </tr>

          <tr> 
            <td width="50">Aktnr</td>
            <td width="350"> 
              <?=$KLinieID?>
            </td>
          </tr>

          <tr> 
            <td width="75">Dato</td>
            <td width="350"> 
              <input type="text" name="KLinieDato" size="15" maxlength="25" value="<?=$_POST["KLinieDato"]?>">
              <font color="#FF0000"> 
              <?=$KLinieDatoFejl?>
              </font> 
              <input type="hidden" name="KLinieDato_old" value="<?=$_POST["KLinieDato"]?>">
            </td>
          </tr>

          <tr> 
            <td width="75" valign="top">Overskrift</td>
            <td width="275"> 
              <input type="text" name="klinieoverskrift" size="55" value="<?=$_POST["klinieoverskrift"]?>" maxlength="250">
            </td>
            <td width="75" valign="top"> 
              <font color="#FF0000"> 
                <?=$klinieoverskriftFejl?>
              </font> 
              <input type="hidden" name="klinieoverskrift_old" value="<?=$_POST["klinieoverskrift"]?>">
            </td>
          </tr>

          <tr> 
            <td width="75" valign="top">Aktivitet</td>
            <td width="275"> 
              <textarea id="KLinieText" name="KLinieText" cols="50" rows="4">
		        <?
		          if(isset($_POST["KLinieText"])) {
			        $sContent=stripslashes($_POST["KLinieText"]); /*** remove (/) slashes ***/
			        echo encodeHTML($sContent);
		          }
		        ?>              	
              </textarea>
			  <script> //STEP 2: Replace the textarea (KLinieText)
				var oEdit1 = new InnovaEditor("oEdit1");
				oEdit1.width=250;
				oEdit1.height=250;
				oEdit1.features=["FullScreen","Preview","Print","Search",
					"Cut","Copy","Paste","PasteWord","PasteText","|","Undo","Redo","|",
					"ForeColor","BackColor","|","Bookmark","Hyperlink","XHTMLSource","BRK",
					"Numbering","Bullets","|","Indent","Outdent","LTR","RTL","|",
					"Image","Flash","Media","|","Table","Guidelines","Absolute","|",
					"Characters","Line","Form","RemoveFormat","ClearAll","BRK",
					"StyleAndFormatting","TextFormatting","ListFormatting","BoxFormatting",
					"ParagraphFormatting","CssText","Styles","|",
					"Paragraph","FontName","FontSize","|",
					"Bold","Italic","Underline","Strikethrough","|",
					"JustifyLeft","JustifyCenter","JustifyRight","JustifyFull"];
				oEdit1.REPLACE("KLinieText");//Specify the id of the textarea here
			  </script>
            </td>
            <td width="75" valign="top"> 
              <font color="#FF0000"> 
                <?=$KLinieTextFejl?>
              </font> 
			  <input type="hidden" name="KLinieText_old" value="<?=$_POST["KLinieText"]?>">
            </td>
          </tr>

          <tr> 
            <td width="75" valign="top">Sted</td>
            <td width="275"> 
              <textarea name="KLinieSted" cols="50" rows="4"><?=$_POST["KLinieSted"]?></textarea>
            </td>
            <td width="75" valign="top"> 
              <font color="#FF0000"> 
                <?=$KLinieStedFejl?>
              </font> 
              <input type="hidden" name="KLinieSted_old" value="<?=$_POST["KLinieSted"]?>">
            </td>
          </tr>

          <tr> 
            <td width="75" valign="top">Info</td>
            <td width="275"> 
              <input type="checkbox" name="Info" value="1" <? if ($_POST["Info"] == 1) { ?>CHECKED<? } ?>>
            </td>
            <td width="75" valign="top"> 
              <font color="#FF0000"> 
                <?=$InfoFejl?>
              </font> 
              <input type="hidden" name="Info_old" value="<?=$_POST["Info"]?>">
            </td>
          </tr>

          <tr> 
            <td width="75" valign="top">Link</td>
            <td width="275"> 
              <input type="text" name="textarea" size="55" value="<?=$_POST["Link"]?>" maxlength="250">
            </td>
            <td width="75" valign="top"> 
              <font color="#FF0000"> 
                <?=$LinkFejl?>
              </font> 
              <input type="hidden" name="Link_old" value="<?=$_POST["Link"]?>">
            </td>
          </tr>

          <tr> 
            <td width="75" valign="top">Linier</td>
            <td width="275"> 
              <input type="text" name="klines" size="1" value="<?=$_POST["klines"]?>" maxlength="1">
            </td>
            <td width="75" valign="top"> 
              <font color="#FF0000"> 
                <?=$klinesFejl?>
              </font> 
              <input type="hidden" name="klines_old" value="<?=$_POST["klines"]?>">
            </td>
          </tr>

          <tr> 
            <td width="75" valign="top">Nyt vindue</td>
            <td width="275"> 
              <input type="checkbox" name="newwindow" value="1" <? if ($_POST["newwindow"] == 1) { ?>CHECKED<? } ?>>
            </td>
            <td width="75" valign="top"> 
              <font color="#FF0000"> 
                <?=$newwindowFejl?>
              </font> 
              <input type="hidden" name="newwindow_old" value="<?=$_POST["newwindow"]?>">
            </td>
          </tr>

          <tr> 
            <td width="75" valign="top">e-tilmelding</td>
            <td width="275"> 
              <input type="checkbox" name="etilmelding" value="1" <? if ($_POST["etilmelding"] == 1) { ?>CHECKED<? } ?>>
            </td>
            <td width="75" valign="top"> 
              <font color="#FF0000"> 
                <?=$etilmeldingFejl?>
              </font> 
              <input type="hidden" name="etilmelding_old" value="<?=$_POST["etilmelding"]?>">
            </td>
          </tr>

          <tr> 
            <td width="75">SlutDato</td>
            <td width="350"> 
              <input type="text" name="tilmeldingslut" size="15" maxlength="25" value="<?=$_POST["tilmeldingslut"]?>">
              <font color="#FF0000"> 
                <?=$tilmeldingslutFejl?>
              </font> 
              <input type="hidden" name="KLinieDato_old" value="<?=$_POST["tilmeldingslut"]?>">
            </td>
          </tr>
          
          <tr> 
            <td width="75" valign="top">Skjult</td>
            <td width="275"> 
              <input type="checkbox" name="hidden_" value="1" <? if ($_POST["hidden_"] == 1) { ?>CHECKED<? } ?>>
            </td>
            <td width="75" valign="top"> 
              <font color="#FF0000"> 
                <?=$hidden_Fejl?>
              </font> 
              <input type="hidden" name="hidden__old" value="<?=$_POST["hidden_"]?>">
            </td>
          </tr>

          <tr> 
            <td width="75">Grp</td>
            <td width="350"> 
              <input type="text" name="PersonGruppeId" size="4" maxlength="8" value="<?=$_POST["PersonGruppeId"]?>">
              <?=GetPersonGruppeNavn($_POST["PersonGruppeId"])?>	
              <font color="#FF0000"> 
                <?=$PersonGruppeIdFejl?>
              </font> 
              <input type="hidden" name="PersonGruppeId_old" value="<?=$_POST["PersonGruppeId"]?>">
              <input type="hidden" name="line" value="<?=$_POST["line"]?>">
            </td>
          </tr>
          <? for($i=0; $i<$_POST["line"]; $i++) { ?>
            <tr> 
              <td width="75">&nbsp;</td>
              
            <td width="350"> 														  
              <input type="text" name="PersonId<?=$i?>" size="4" maxlength="8" value="<?=$_POST["PersonId" . $i]?>">
                <?=GetPersonNavn($_POST["PersonId" . $i])?>	
                <input type="hidden" name="PersonId<?=$i?>_old" size="4" maxlength="8" value="<?=$_POST["PersonId" . $i]?>">
              </td>
            </tr>
          <? } ?>
          <tr> 
            <td width="75">&nbsp;</td>
            <td width="350">&nbsp;</td>
          </tr>
          <tr> 
            <td width="75">&nbsp; </td>
            <td width="350" bgcolor="#FFFFFF"> 
              <input type="submit" name="Send" value="Opdater">
              <input type="submit" name="Fortryd" value="Fortryd" onClick="FocusParentAndClose()">
          </tr>
          <tr> 
            <td width="75">&nbsp;</td>
            <td width="350">&nbsp;</td>
          </tr>
        </table>
        </form>
      <? } } // End If $DoWhat 5 ?>

      <? if ($DoWhat == 6) { // Oprettet record gemt eller Vis record med fejl ?>
      <? if (!$Fejl) { // Oprettet record gemt ?>
         <b><font color="#003399">Kviterings tekst her</font></b>
         <a href="javascript:UpdateParentAndClose()"> Retur</a> 
      <? } else { // Vis record med fejl ?>
        <form name="Tilsagn" method="post" action="AktivitetskalenderUpd.php?DoWhat=6">
        <table width="100%" border="0" bgcolor="#FFFFFF">
          <tr> 
            <td width="300" colspan="2"><b>
              <font color="#003399">Oprettelse af Aktivitetskalender aktivitet</font></b>
            </td>
          </tr>
          <tr> 
            <td width="75">&nbsp;</td>
            <td width="350">&nbsp;</td>
          </tr>
          
          <tr> 
            <td width="75">Dato</td>
            <td width="350"> 
              <input type="text" name="KLinieDato" size="15" maxlength="25" value="<?=$_POST["KLinieDato"]?>">
              <font color="#FF0000"> 
              <?=$KLinieDatoFejl?>
              </font> 
              <input type="hidden" name="KLinieDato_old" value="<?=$_POST["KLinieDato"]?>">
            </td>
          </tr>

          <tr> 
            <td width="75" valign="top">Overskrift</td>
            <td width="275"> 
              <input type="text" name="klinieoverskrift" size="55" value="<?=$_POST["klinieoverskrift"]?>" maxlength="250">
              <font color="#FF0000"> 
                <?=$klinieoverskriftFejl?>
              </font> 
              <input type="hidden" name="klinieoverskrift_old" value="<?=$_POST["klinieoverskrift"]?>">
            </td>
          </tr>
          
          <tr> 
            <td width="75" valign="top">Aktivitet</td>
            <td width="275"> 
              <textarea id="KLinieText" name="KLinieText" cols="50" rows="4">
		        <?
		          if(isset($_POST["KLinieText"])) {
			        $sContent=stripslashes($_POST["KLinieText"]); /*** remove (/) slashes ***/
			        echo encodeHTML($sContent);
		          }
		        ?>              	
              </textarea>
			  <script> //STEP 2: Replace the textarea (KLinieText)
				var oEdit1 = new InnovaEditor("oEdit1");
				oEdit1.width=250;
				oEdit1.height=250;
				oEdit1.features=["FullScreen","Preview","Print","Search",
					"Cut","Copy","Paste","PasteWord","PasteText","|","Undo","Redo","|",
					"ForeColor","BackColor","|","Bookmark","Hyperlink","XHTMLSource","BRK",
					"Numbering","Bullets","|","Indent","Outdent","LTR","RTL","|",
					"Image","Flash","Media","|","Table","Guidelines","Absolute","|",
					"Characters","Line","Form","RemoveFormat","ClearAll","BRK",
					"StyleAndFormatting","TextFormatting","ListFormatting","BoxFormatting",
					"ParagraphFormatting","CssText","Styles","|",
					"Paragraph","FontName","FontSize","|",
					"Bold","Italic","Underline","Strikethrough","|",
					"JustifyLeft","JustifyCenter","JustifyRight","JustifyFull"];
				oEdit1.REPLACE("KLinieText");//Specify the id of the textarea here
			  </script>
              <font color="#FF0000"> 
                <?=$KLinieTextFejl?>
              </font>
              <input type="hidden" name="KLinieText_old" value="<?=$_POST["KLinieText"]?>">
            </td>
          </tr>
          
          <tr> 
            <td width="75" valign="top">Sted</td>
            <td width="275"> 
              <textarea name="KLinieSted" cols="50" rows="4"><?=$_POST["KLinieSted"]?></textarea>
              <font color="#FF0000"> 
                <?=$KLinieStedFejl?>
              </font>
              <input type="hidden" name="KLinieSted_old" value="<?=$_POST["KLinieSted"]?>">
            </td>
          </tr>
          
          <tr> 
            <td width="75" valign="top">Info</td>
            <td width="275"> 
              <input type="checkbox" name="Info" value="1" <? if ($_POST["Info"] == 1) { ?>CHECKED<? } ?>>
              <font color="#FF0000"> 
                <?=$InfoFejl?>
              </font> 
              <input type="hidden" name="Info_old" value="<?=$_POST["Info"]?>">
            </td>
          </tr>
          
          <tr> 
            <td width="75" valign="top">Link</td>
            <td width="275"> 
              <input type="text" name="Link" size="55" value="<?=$_POST["Link"]?>" maxlength="250">
              <font color="#FF0000"> 
                <?=$LinkFejl?>
              </font> 
              <input type="hidden" name="Link_old" value="<?=$_POST["Link"]?>">
            </td>
          </tr>

          <tr> 
            <td width="75" valign="top">Linier</td>
            <td width="275"> 
              <input type="text" name="klines" size="1" value="<?=$_POST["klines"]?>" maxlength="1">
            </td>
            <td width="75" valign="top"> 
              <font color="#FF0000"> 
                <?=$klinesFejl?>
              </font> 
              <input type="hidden" name="klines_old" value="<?=$_POST["klines"]?>">
            </td>
          </tr>

          <tr> 
            <td width="75" valign="top">Nyt vindue</td>
            <td width="275"> 
              <input type="checkbox" name="newwindow" value="1" <? if ($_POST["newwindow"] == 1) { ?>CHECKED<? } ?>>
            </td>
            <td width="75" valign="top"> 
              <font color="#FF0000"> 
                <?=$newwindowFejl?>
              </font> 
              <input type="hidden" name="newwindow_old" value="<?=$_POST["newwindow"]?>">
            </td>
          </tr>

          <tr> 
            <td width="75" valign="top">e-tilmelding</td>
            <td width="275"> 
              <input type="text" name="etilmelding" size="1" value="<?=$_POST["etilmelding"]?>" maxlength="1">
            </td>
            <td width="75" valign="top"> 
              <font color="#FF0000"> 
                <?=$etilmeldingFejl?>
              </font> 
              <input type="hidden" name="etilmelding_old" value="<?=$_POST["etilmelding"]?>">
            </td>
          </tr>

          <tr> 
            <td width="75">Afgift</td>
            <td width="350"> 
              <input type="text" name="aktafgift" size="10" maxlength="10" value="<?=$_POST["aktafgift"]?>">
              <font color="#FF0000"> 
              <?=$aktafgiftFejl?>
              </font> 
              <input type="hidden" name="aktafgift_old" value="<?=$_POST["aktafgift"]?>">
            </td>
          </tr>
          
          <tr> 
            <td width="75">SlutDato</td>
            <td width="350"> 
              <input type="text" name="tilmeldingslut" size="15" maxlength="25" value="<?=$_POST["tilmeldingslut"]?>">
              <font color="#FF0000"> 
                <?=$tilmeldingslutFejl?>
              </font> 
              <input type="hidden" name="KLinieDato_old" value="<?=$_POST["tilmeldingslut"]?>">
            </td>
          </tr>
          
          <tr> 
            <td width="75" valign="top">Skjult</td>
            <td width="275"> 
              <input type="checkbox" name="hidden_" value="1" <? if ($_POST["hidden_"] == 1) { ?>CHECKED<? } ?>>
            </td>
            <td width="75" valign="top"> 
              <font color="#FF0000"> 
                <?=$hidden_Fejl?>
              </font> 
              <input type="hidden" name="hidden__old" value="<?=$_POST["hidden_"]?>">
            </td>
          </tr>

          <tr> 
            <td width="75">&nbsp;</td>
            <td width="350">&nbsp;</td>
          </tr>
          <tr> 
            <td width="75">&nbsp; </td>
            <td width="350" bgcolor="#FFFFFF"> 
              <input type="submit" name="Send" value="Opret">
              <input type="submit" name="Fortryd" value="Fortryd" onClick="FocusParentAndClose()">
          </tr>
          <tr> 
            <td width="75">&nbsp;</td>
            <td width="350">&nbsp;</td>
          </tr>
        </table>
        </form>
      <? } } // End If $DoWhat 6 ?>

</body>
</html>

<script language="javascript">
<!--
function UpdateParentAndClose(){
	var ParentWindow;
	ParentWindow = top.opener;
	ParentWindow.document.execCommand("Refresh");
	ParentWindow.focus();
	window.close();
}
function FocusParentAndClose(){
	var ParentWindow;
	ParentWindow = top.opener;
	ParentWindow.focus();
	window.close();
}
function fonLoad(){
	var h = 545;
	var w = 615;
	window.resizeTo(w, h+(<?=$line?>*27));
}
var newwindow;
function PListLink(id){
	var url;
	url = "/admin/PListe2.php?TopWin=<?=$TopWin?>&MaxRow=20&LinieId="+id;
	newwindow = window.open(url,"PListe","status, scrollbars, resizeable, width=475, heigth=100")
	newwindow.moveBy(300,0);
	newwindow.focus();
}
// -->
</script>
