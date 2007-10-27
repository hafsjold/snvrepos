<!-- Clear msie buffer <?=str_repeat(":",500);?> -->
<?
    ini_set("max_execution_time",3000000);

    setlocale (LC_TIME, "da_DK.ISO8859-1");

	require 'smarty_puls3060adm.php';

	$smarty = new Smarty_Puls3060adm;
	$smarty->caching = false;

    if (isset($_REQUEST['MaillistId'])) 
      $MaillistId=$_REQUEST['MaillistId'];
    else
      $MaillistId = 999;

    if (isset($_REQUEST['AktId'])) 
      $AktId=$_REQUEST['AktId'];
    else
      $AktId = 86;

    //error_reporting(E_ALL);
    
    require_once('email_message.php');
    include('dbFunctions.inc');  
	define('CRLF', "\r\n", TRUE);

    include_once("conn.inc");  
    $dbLink = pg_connect($conn_www);
	
	
	$Query="
	  SELECT
		kliniedato,
		klinietext,
		klines,
		link,
		info,
		newwindow,
		etilmelding,
		hidden,
		kliniested,
		tilmeldingslut
	  FROM tblkLinie
	  WHERE klinieid = $AktId";
	
	$dbResult = pg_query($dbLink, $Query);
	if ($row = pg_fetch_array($dbResult, NULL , PGSQL_ASSOC))
	{
      $KLinieDato = $row["kliniedato"];
      $KLinieText = $row["klinietext"];
      $klines = $row["klines"];
      $link = $row["link"];
      $info = $row["info"];
      $newwindow = $row["newwindow"];
      $etilmelding = $row["etilmelding"];
      $hidden = $row["hidden"];
      $kliniested = $row["kliniested"];
      $tilmeldingslut = $row["tilmeldingslut"];
	}	
	
	if ($MaillistId == 998) {
	  $Query="SELECT opretmailliste('" . $KLinieText . "', 2, 0) AS MaillistId;";
	  $dbResult = pg_query($dbLink, $Query);
      if ($row = pg_fetch_array($dbResult, NULL , PGSQL_ASSOC)) {
        $MaillistId = $row["maillistid"];
	  }
	}

	$Query="
	  SELECT
	    tblMaillistLinie.PersonId As PersonId, 
		tblMailadresse.MailAdr As MailAdr, 
		tblPerson.Fornavn As Fornavn, 
		(tblPerson.Fornavn || ' ' || tblPerson.Efternavn) AS Navn 
	  FROM tblMaillistLinie
		INNER JOIN tblPerson ON tblMaillistLinie.PersonId = tblPerson.Id
		INNER JOIN tblMailadresse ON tblMaillistLinie.PersonId = tblMailadresse.PersonId
	  WHERE tblMaillistLinie.MaillistId=$MaillistId
	  AND tblPerson.nomail <> 1
	  ORDER BY tblMaillistLinie.Id
		";
	$dbResult = pg_query($dbLink, $Query);

	while($row = pg_fetch_array($dbResult, NULL , PGSQL_ASSOC))
	{
		$PersonId = $row["personid"];
		$MailAdr = $row["mailadr"];
		$Fornavn = $row["fornavn"];
		$Navn = $row["navn"];
		$p0 = getGUID();
		$GUID = "'" . $p0 . "'";
		$Insert = "
		   INSERT INTO tblLink 
			 (LinkId, 
			  Left_url, 
			  Right_url, 
			  Info, 
			  P3060_ref,
			  Akt_ref)
		   VALUES
			 ($GUID, 
			  '/menu/kalendermenu.php', 
			  '/Kalender/AktTilmelding.php', 
			  0, 
			  $PersonId,
			  $AktId)"; 
	    
	    $dbResult2 = pg_query($dbLink, $Insert);
        flush();

    	$from_address="mha@puls3060.dk";
    	$from_name="Puls 3060";

    	$reply_name=$from_name;
    	$reply_address=$from_address;
    	$error_delivery_name=$from_name;
    	$error_delivery_address=$from_address;

    	$to_name=$Navn;
    	$to_address=$MailAdr;

    	$subject="Subject: Elektronisk tilmelding til $KLinieText";
    	
    	$email_message=new email_message_class;

    	$email_message->SetEncodedEmailHeader("To",$to_address,$to_name);
    	$email_message->SetEncodedEmailHeader("From",$from_address,$from_name);
    	$email_message->SetEncodedEmailHeader("Reply-To",$reply_address,$reply_name);
    	$email_message->SetHeader("Sender",$from_address);
    	$email_message->SetHeader("Return-Path",$error_delivery_address);
    	$email_message->SetEncodedHeader("Subject",$subject);

    	$image=array(
    		"FileName"=>"/data/home/puls3060dk/www/mailimages/puls3060logo.gif",
    		"Content-Type"=>"automatic/name",
    		"Disposition"=>"inline"
    	);
    	$email_message->CreateFilePart($image,$image_part);
     	$image_content_id=$email_message->GetPartContentID($image_part);

        $smarty->assign("KLinieText",$KLinieText);
        $smarty->assign("KLinieDato",$KLinieDato);
		$smarty->assign("image_content_id",$image_content_id);
		$smarty->assign("Fornavn",$Fornavn);
		$smarty->assign("p0",$p0);
		$smarty->assign("Navn",$Navn);
		$smarty->assign("link",$link);
		$smarty->assign("tilmeldingslut",$tilmeldingslut);
		$html_message = $smarty->fetch("mail_akt/html_tilmelding.tpl");
		$text_message = $smarty->fetch("mail_akt/txt_tilmelding.tpl");
   	
    	$email_message->CreateQuotedPrintableHTMLPart($html_message,"",$html_part);

    	$related_parts=array(
    		$html_part,
    		$image_part,
    	);
    	$email_message->CreateRelatedMultipart($related_parts,$html_parts);

    	$email_message->CreateQuotedPrintableTextPart($email_message->WrapText($text_message),"",$text_part);

    	$alternative_parts=array(
    		$text_part,
    		$html_parts
    	);
    	$email_message->AddAlternativeMultipart($alternative_parts);

    	$error=$email_message->Send();
    	if(strcmp($error,""))
    		echo "Error: $error\n";
    	else
		    echo("$to_name  ($to_address)<br>");

	}
   echo("Alle mails sendt<br>");

?>
