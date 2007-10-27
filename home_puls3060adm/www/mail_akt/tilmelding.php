<!-- Clear msie buffer <?=str_repeat(":",500);?> -->
<?
//adm.puls3060.dk/mail_akt/tilmelding.php?AktId=376&MaillistId=999
ini_set("max_execution_time",3000000);
setlocale (LC_TIME, "da_DK.ISO8859-1");
define('CRLF', "\r\n", TRUE);

require_once('clsPuls3060SQL.php');
require_once('email_message.php');
require_once 'smarty_puls3060adm.php';
require_once('dbFunctions.inc');

//**************************************************************
//**************************************************************
class clsAktivitetMail {
	protected $_DBPuls3060;
	protected $_DBPuls3060_2;
	protected $_Smarty;
	protected $_maillistid;
	protected $_aktid;
	protected $_kliniedato;
	protected $_klinietext;
	protected $_klines;
	protected $_link;
	protected $_info;
	protected $_newwindow;
	protected $_etilmelding;
	protected $_hidden;
	protected $_kliniested;
	protected $_tilmeldingslut;
	protected $_personid;
	protected $_mailadr;
	protected $_fornavn;
	protected $_navn;
	protected $_betalttildato;
	protected $_sidstetilmeldingsdato;
	protected $_klinieoverskrift;
	protected $_p0;
	protected $_image_content_id;

	public function __construct($MaillistId, $AktId){
		$this->_maillistid = $MaillistId;
		$this->_aktid = $AktId;
		$this->_DBPuls3060 = new clsPuls3060SQL();
		$this->_DBPuls3060_2 = new clsPuls3060SQL();
		$this->_Smarty = new Smarty_Puls3060adm();
		$this->_Smarty->caching = false;
		return ;
	} // ctor

	public function __destruct() {
		echo("Alle mails sendt<br>");
	}

	private function __get($property) {
		switch ($property) {
			case "maillistid":
			case "aktid":
			case "kliniedato":
			case "klinietext":
			case "klines":
			case "link":
			case "info":
			case "newwindow":
			case "etilmelding":
			case "hidden":
			case "kliniested":
			case "tilmeldingslut":
			case "personid":
			case "mailadr":
			case "fornavn":
			case "navn":
			case "betalttildato":
			case "sidstetilmeldingsdato":
			case "klinieoverskrift":
			case "p0":
			case "image_content_id":
				$_prop = '_' . $property;
				return $this->$_prop;
				break;

			default:
				throw new Exception("$property is not a valid property");
				break;
		}
		return null;
	}

	public function createMailList(){
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
    		tilmeldingslut,
    		klinieoverskrift
    	  FROM tblkLinie
    	  WHERE klinieid = ?";

		if ($this->_DBPuls3060->prepare($Query)) {
			$AktDataArr = array($this->_aktid);
			if ($this->_DBPuls3060->execute($AktDataArr)) {
				if ($this->_DBPuls3060->next(SQL_ASSOC)) {
					$this->_kliniedato = $this->_DBPuls3060->record['kliniedato'];
					$this->_klinietext = $this->_DBPuls3060->record['klinietext'];
					$this->_klines = $this->_DBPuls3060->record['klines'];
					$this->_link = $this->_DBPuls3060->record['link'];
					$this->_info = $this->_DBPuls3060->record['info'];
					$this->_newwindow = $this->_DBPuls3060->record['newwindow'];
					$this->_etilmelding = $this->_DBPuls3060->record['etilmelding'];
					$this->_hidden = $this->_DBPuls3060->record['hidden'];
					$this->_kliniested = $this->_DBPuls3060->record['kliniested'];
					$this->_tilmeldingslut = $this->_DBPuls3060->record['tilmeldingslut'];
					$this->_klinieoverskrift = $this->_DBPuls3060->record['klinieoverskrift'];
				}
			}
		}

		if ($this->_maillistid == 998) {
			$this->_DBPuls3060->autoCommit(false);
			$this->_DBPuls3060->prepare("SELECT opretmailliste(?, 2, 0) AS maillistid;");
			$MailListeArr = array($this->_klinieoverskrift);
			if ($this->_DBPuls3060->execute($MailListeArr)) {
				if ($this->_DBPuls3060->next(SQL_ASSOC)) {
					$this->_maillistid = $this->_DBPuls3060->record['maillistid'];
					$this->_DBPuls3060->commit();
				}
			}
		}
	}

	public function sendMailList(){
		$this->_DBPuls3060_2->autoCommit(false);
		$Query="
	  		SELECT
	    	  tblmaillistlinie.personid AS personid, 
			  tblmailadresse.mailadr AS mailadr, 
			  tblperson.fornavn AS fornavn, 
			  (tblperson.fornavn || ' ' || tblperson.efternavn) AS navn
	  		FROM tblmaillistlinie
			  INNER JOIN tblperson ON tblmaillistlinie.personid = tblperson.id
			  INNER JOIN tblmailadresse ON tblmaillistlinie.personid = tblmailadresse.personid
	  		WHERE tblmaillistlinie.maillistid=?
	  		AND tblperson.nomail <> 1
	  		ORDER BY tblmaillistlinie.id
			";
		$QueryDataArr = array($this->_maillistid);

		$Insert = "
		   	INSERT INTO tblLink 
			  (LinkId, 
			  Left_url, 
			  Right_url, 
			  Info, 
			  P3060_ref,
			  Akt_ref)
		   	VALUES
			 (?, 
			  '/menu/kalendermenu.php', 
			  '/Kalender/AktUserTilmelding.php', 
			  0, 
			  ?,
			  ?)"; 
		$this->_DBPuls3060_2->prepare($Insert);

		if ($this->_DBPuls3060->prepare($Query)) {
			if ($this->_DBPuls3060->execute($QueryDataArr)) {
				while ($this->_DBPuls3060->next(SQL_ASSOC)) {
					$this->_personid = $this->_DBPuls3060->record['personid'];
					$this->_mailadr = $this->_DBPuls3060->record['mailadr'];
					$this->_fornavn = $this->_DBPuls3060->record['fornavn'];
					$this->_navn = $this->_DBPuls3060->record['navn'];
					$this->_p0 = getGUID();

					$InsertDataArr = array($this->_p0, $this->_personid, $this->_aktid);
					$this->_DBPuls3060_2->execute($InsertDataArr);
					$this->_DBPuls3060_2->commit();

					$email_message=new email_message_class;
					$email_message->SetEncodedEmailHeader("To",$this->_mailadr,$this->_navn);
					$email_message->SetEncodedEmailHeader("From","mha@puls3060.dk","Puls 3060");
					$email_message->SetEncodedEmailHeader("Reply-To","mha@puls3060.dk","Puls 3060");
					$email_message->SetHeader("Sender","mha@puls3060.dk");
					$email_message->SetHeader("Return-Path","mha@puls3060.dk");
					$email_message->SetEncodedHeader("Subject","Elektronisk tilmelding til $this->_klinieoverskrift");

					$image=array(
					"FileName"=>"/data/home/puls3060dk/www/mailimages/puls3060logo.gif",
					"Content-Type"=>"automatic/name",
					"Disposition"=>"inline"
					);
					$email_message->CreateFilePart($image,$image_part);
					$this->_image_content_id=$email_message->GetPartContentID($image_part);

					$this->_Smarty->assign("tilmelding",$this);
					$html_message = $this->_Smarty->fetch("mail_akt/html_tilmelding.tpl");
					$text_message = $this->_Smarty->fetch("mail_akt/txt_tilmelding.tpl");

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
					echo("$this->_navn  ($this->_mailadr)<br>");
				}
			}
		}
	}

}

//******************************************
//MAIN FUNCTION
//******************************************
if (isset($_REQUEST['MaillistId']))
$MaillistId=$_REQUEST['MaillistId'];
else
$MaillistId = 999;

if (isset($_REQUEST['AktId']))
$AktId=$_REQUEST['AktId'];
else
$AktId = 86;

$objAktivitetMail = new clsAktivitetMail($MaillistId, $AktId);
$objAktivitetMail->createMailList();
$objAktivitetMail->sendMailList();
?>
