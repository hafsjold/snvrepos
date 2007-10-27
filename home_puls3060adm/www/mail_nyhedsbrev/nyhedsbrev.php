<!-- Clear msie buffer <?=str_repeat(":",500);?> -->
<?
//adm.puls3060.dk/mail_nyhedsbrev/nyhedsbrev.php?BrevId=5&MaillistId=999
ini_set("max_execution_time",3000000);
setlocale (LC_TIME, "da_DK.ISO8859-1");
define('CRLF', "\r\n", TRUE);

require_once('clsPuls3060SQL.php');
require_once('email_message.php');
require_once 'smarty_puls3060adm.php';
require_once('dbFunctions.inc');

//**************************************************************
//**************************************************************
class clsNyhedsbrevMail {
	protected $_DBPuls3060;
	protected $_Smarty;
	protected $_maillistid;
	protected $_brevid;
	protected $_udgivet;
	protected $_titel;
	protected $_link;
	protected $_personid;
	protected $_mailadr;
	protected $_fornavn;
	protected $_navn;
	protected $_image_content_id;

	public function __construct($MaillistId, $BrevId){
		$this->_maillistid = $MaillistId;
		$this->_brevid = $BrevId;
		$this->_DBPuls3060 = new clsPuls3060SQL();
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
			case "brevid":
			case "udgivet":
			case "titel":
			case "link":
			case "personid":
			case "mailadr":
			case "fornavn":
			case "navn":
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
    		udgivet,
    		titel,
    		link
    	  FROM tblnyhedsbrev
    	  WHERE id = ?";

		if ($this->_DBPuls3060->prepare($Query)) {
			$AktDataArr = array($this->_brevid);
			if ($this->_DBPuls3060->execute($AktDataArr)) {
				if ($this->_DBPuls3060->next(SQL_ASSOC)) {
					$this->_udgivet = $this->_DBPuls3060->record['udgivet'];
					$this->_titel = $this->_DBPuls3060->record['titel'];
					$this->_link = $this->_DBPuls3060->record['link'];
				}
			}
		}

		if ($this->_maillistid == 998) {
			$this->_DBPuls3060->autoCommit(false);
			$this->_DBPuls3060->prepare("SELECT opretmailliste(?, 2, 0) AS maillistid;");
			$MailListeArr = array("Puls 3060 Nyhedsbrev $this->_titel");
			if ($this->_DBPuls3060->execute($MailListeArr)) {
				if ($this->_DBPuls3060->next(SQL_ASSOC)) {
					$this->_maillistid = $this->_DBPuls3060->record['maillistid'];
					$this->_DBPuls3060->commit();
				}
			}
		}
	}

	public function sendMailList(){
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

		if ($this->_DBPuls3060->prepare($Query)) {
			if ($this->_DBPuls3060->execute($QueryDataArr)) {
				while ($this->_DBPuls3060->next(SQL_ASSOC)) {
					$this->_personid = $this->_DBPuls3060->record['personid'];
					$this->_mailadr = $this->_DBPuls3060->record['mailadr'];
					$this->_fornavn = $this->_DBPuls3060->record['fornavn'];
					$this->_navn = $this->_DBPuls3060->record['navn'];

					$email_message=new email_message_class;
					$email_message->SetEncodedEmailHeader("To",$this->_mailadr,$this->_navn);
					$email_message->SetEncodedEmailHeader("From","mha@puls3060.dk","Puls 3060");
					$email_message->SetEncodedEmailHeader("Reply-To","mha@puls3060.dk","Puls 3060");
					$email_message->SetHeader("Sender","mha@puls3060.dk");
					$email_message->SetHeader("Return-Path","mha@puls3060.dk");
					$email_message->SetEncodedHeader("Subject","Puls 3060 Nyhedsbrev");

					$image=array(
					"FileName"=>"/data/home/puls3060dk/www/mailimages/puls3060logo.gif",
					"Content-Type"=>"automatic/name",
					"Disposition"=>"inline"
					);
					$email_message->CreateFilePart($image,$image_part);
					$this->_image_content_id=$email_message->GetPartContentID($image_part);

					$this->_Smarty->assign("nyhedsbrev",$this);
					$html_message = $this->_Smarty->fetch("mail_nyhedsbrev/html_nyhedsbrev.tpl");
					$text_message = $this->_Smarty->fetch("mail_nyhedsbrev/txt_nyhedsbrev.tpl");

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

					//string file_get_contents ( string filename );
					$attachment=array(
						"Name"=>"Nyhedsbrev-" . $this->_brevid . ".pdf",
						"Content-Type"=>"automatic/name",
						"Disposition"=>"attachment"
					);
					$attachment['Data'] = file_get_contents ('/data/home/puls3060dk/www/Medlem/Nyhedsbrev/Nyhedsbrev-' . $this->_brevid . '.pdf' );
					$email_message->AddFilePart($attachment);

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

if (isset($_REQUEST['BrevId']))
$BrevId=$_REQUEST['BrevId'];
else
$BrevId = 1;

$objNyhedsbrevMail = new clsNyhedsbrevMail($MaillistId, $BrevId);
$objNyhedsbrevMail->createMailList();
$objNyhedsbrevMail->sendMailList();
?>
