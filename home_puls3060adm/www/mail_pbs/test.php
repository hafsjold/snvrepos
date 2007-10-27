<!-- Clear msie buffer <?=str_repeat(":",500);?> -->
<?
//adm.puls3060.dk/mail_pbs/test.php

ini_set("max_execution_time",3000000);
setlocale (LC_TIME, "da_DK.ISO8859-1");
define('CRLF', "\r\n", TRUE);

require_once('clsPuls3060SQL.php');
require_once('clsRegnskab3060SQL.php');
require_once('email_message.php');
require_once 'smarty_puls3060adm.php';
require_once('dbFunctions.inc');

class clsPbsMail {
	protected $_DBPuls3060;
	protected $_DBPuls3060_2;
	protected $_DBRegnskab3060;
	protected $_Smarty;
	protected $_maillistid;
	protected $_personid;
	protected $_mailadr;
	protected $_fornavn;
	protected $_navn;
	protected $_betalttildato;
	protected $_sidstetilmeldingsdato;
	protected $_p0;
	protected $_image_content_id;

	public function __construct(){
		$this->_DBPuls3060 = new clsPuls3060SQL();
		$this->_DBPuls3060_2 = new clsPuls3060SQL();
		$this->_DBRegnskab3060 = new clsRegnskab3060SQL();
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
			case "personid":
			case "mailadr":
			case "fornavn":
			case "navn":
			case "betalttildato":
			case "sidstetilmeldingsdato":
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

	public function createMailList($fradato, $tildato){
		$MailListeAntal = 0;
		$this->_DBPuls3060->autoCommit(false);
		$this->_DBPuls3060->prepare("INSERT INTO public.tblmaillist (maillistnavn, mailtekst) VALUES (?,?)");
		$MailListeArr = array('PBSTilmelding',"PBS Tilmelding $fradato - $tildato");
		$this->_DBPuls3060->execute($MailListeArr);
		$this->_DBPuls3060->query("select currval('tblmaillist_id_seq') as insert_id", SQL_INIT, SQL_ASSOC);
		$this->_maillistid = $this->_DBPuls3060->record['insert_id'];
		$this->_DBPuls3060->prepare("INSERT INTO public.tblmaillistlinie (maillistid, personid) VALUES (?, ?)");

		if ($this->_DBRegnskab3060->prepare("SELECT debitorkonto FROM public.vmedlemmer_med_mailadresse_uden_pbsaftale WHERE betalttildato > ? AND betalttildato <= ?")) {
			$FraTilArr = array($fradato, $tildato);
			if ($this->_DBRegnskab3060->execute($FraTilArr)) {
				while ($this->_DBRegnskab3060->next(SQL_ASSOC)) {
					$konto = $this->_DBRegnskab3060->record['debitorkonto'];
					$MailListeLinieArr = array($this->_maillistid, $konto - 10000);
					$this->_DBPuls3060->execute($MailListeLinieArr);
					$MailListeAntal++;
				}
			}
		}

		if ($MailListeAntal){
			$this->_DBPuls3060->commit();
			//$this->_maillistid = 999; // TEST TEST
		}
		else {
			$this->_DBPuls3060->rollback();
			$this->_maillistid = 999;
		}
	}

	public function visMailList(){

	}
	
	
	
	public function sendMailList(){
		$this->_DBPuls3060_2->autoCommit(false);
		$Query="
	  		SELECT
	    	  tblmaillistlinie.personid AS personid, 
			  tblmailadresse.mailadr AS mailadr, 
			  tblperson.fornavn AS fornavn, 
			  (tblperson.fornavn || ' ' || tblperson.efternavn) AS navn,
			  tblpuls3060medlem.betalttildato AS betalttildato,
			  ((tblpuls3060medlem.betalttildato - interval '36 days')) AS sidstetilmeldingsdato  
	  		FROM tblmaillistlinie
			  INNER JOIN tblperson ON tblmaillistlinie.personid = tblperson.id
			  INNER JOIN tblpuls3060medlem ON tblmaillistlinie.personid = tblpuls3060medlem.personid
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
			  P3060_ref)
		   	VALUES
			 (?, 
			  '/menu/Medlem.php', 
			  '/Medlem/PbsUserTilmelding.php', 
			  0, 
			  ?)"; 
		$this->_DBPuls3060_2->prepare($Insert);

		if ($this->_DBPuls3060->prepare($Query)) {
			if ($this->_DBPuls3060->execute($QueryDataArr)) {
				while ($this->_DBPuls3060->next(SQL_ASSOC)) {
					$this->_personid = $this->_DBPuls3060->record['personid'];
					$this->_mailadr = $this->_DBPuls3060->record['mailadr'];
					$this->_fornavn = $this->_DBPuls3060->record['fornavn'];
					$this->_navn = $this->_DBPuls3060->record['navn'];
					$this->_betalttildato = $this->_DBPuls3060->record['betalttildato'];
					$this->_sidstetilmeldingsdato = $this->_DBPuls3060->record['sidstetilmeldingsdato'];
					$this->_p0 = getGUID();

					$InsertDataArr = array($this->_p0, $this->_personid);
					$this->_DBPuls3060_2->execute($InsertDataArr);
					$this->_DBPuls3060_2->commit();

					$email_message=new email_message_class;
					$email_message->SetEncodedEmailHeader("To",$this->_mailadr,$this->_navn);
					$email_message->SetEncodedEmailHeader("From","mha@puls3060.dk","Puls 3060");
					$email_message->SetEncodedEmailHeader("Reply-To","mha@puls3060.dk","Puls 3060");
					$email_message->SetHeader("Sender","mha@puls3060.dk");
					$email_message->SetHeader("Return-Path","mha@puls3060.dk");
					$email_message->SetEncodedHeader("Subject","Puls 3060 tilmelding til Betalingsservice");

					$image=array(
					"FileName"=>"/data/home/puls3060dk/www/mailimages/puls3060logo.gif",
					"Content-Type"=>"automatic/name",
					"Disposition"=>"inline"
					);
					$email_message->CreateFilePart($image,$image_part);
					$this->_image_content_id=$email_message->GetPartContentID($image_part);

					$this->_Smarty->assign("tilmelding",$this);
					$html_message = $this->_Smarty->fetch("mail_pbs/html_tilmelding.tpl");
					$text_message = $this->_Smarty->fetch("mail_pbs/txt_tilmelding.tpl");

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

class clsMailListe {
	protected $_DBPuls3060;
	protected $_maillistid;
	protected $_personid;
	protected $_mailadr;
	protected $_fornavn;
	protected $_navn;
	protected $_betalttildato;
	protected $_sidstetilmeldingsdato;

	public function __construct($maillistid){
		$this->_DBPuls3060 = new clsPuls3060SQL();
		$this->_maillistid = $maillistid;
		$this->initMailList();
		return ;
	} // ctor

	public function __destruct() {
	}

	private function __get($property) {
		switch ($property) {
			case "maillistid":
			case "personid":
			case "mailadr":
			case "fornavn":
			case "navn":
			case "betalttildato":
			case "sidstetilmeldingsdato":
				$_prop = '_' . $property;
				return $this->$_prop;
				break;

			case "rows":
				return $this->_DBPuls3060->result->numRows();
				break;				 

				
			default:
				throw new Exception("$property is not a valid property");
				break;
		}
		return null;
	}

	public function initMailList(){
		$Query="
	  		SELECT
	    	  tblmaillistlinie.personid AS personid, 
			  tblmailadresse.mailadr AS mailadr, 
			  tblperson.fornavn AS fornavn, 
			  (tblperson.fornavn || ' ' || tblperson.efternavn) AS navn,
			  tblpuls3060medlem.betalttildato AS betalttildato,
			  ((tblpuls3060medlem.betalttildato - interval '36 days')) AS sidstetilmeldingsdato  
	  		FROM tblmaillistlinie
			  INNER JOIN tblperson ON tblmaillistlinie.personid = tblperson.id
			  INNER JOIN tblpuls3060medlem ON tblmaillistlinie.personid = tblpuls3060medlem.personid
			  INNER JOIN tblmailadresse ON tblmaillistlinie.personid = tblmailadresse.personid
	  		WHERE tblmaillistlinie.maillistid=?
	  		AND tblperson.nomail <> 1
	  		ORDER BY tblmaillistlinie.id
			";
		$QueryDataArr = array($this->_maillistid);
		$this->_DBPuls3060->prepare($Query);
		$this->_DBPuls3060->execute($QueryDataArr);
   }

	public function nextMailList(){	
		if ($this->_DBPuls3060->next(SQL_ASSOC)) {
			$this->_personid = $this->_DBPuls3060->record['personid'];
			$this->_mailadr = $this->_DBPuls3060->record['mailadr'];
			$this->_fornavn = $this->_DBPuls3060->record['fornavn'];
			$this->_navn = $this->_DBPuls3060->record['navn'];
			$this->_betalttildato = $this->_DBPuls3060->record['betalttildato'];
			$this->_sidstetilmeldingsdato = $this->_DBPuls3060->record['sidstetilmeldingsdato'];
			return true;
		}
		else {
			return false;
		}
	}

}



//******************************************
//MAIN FUNCTION
//******************************************
$days_left_this_month = (mktime(0, 0, 0, date("m") +1 , 1, date("Y")) - mktime(0, 0, 0, date("m") , date("d")+1, date("Y")))/86400;
if ($days_left_this_month > 10) {
	$end_this_month  = date("Y-m-d", mktime(0, 0, 0, date("m") +1 , 1, date("Y")) - 86400);
	$end_next_month  = date("Y-m-d", mktime(0, 0, 0, date("m") +2 , 1, date("Y")) - 86400);
}
else {
	$end_this_month  = date("Y-m-d", mktime(0, 0, 0, date("m") +2 , 1, date("Y")) - 86400);
	$end_next_month  = date("Y-m-d", mktime(0, 0, 0, date("m") +3 , 1, date("Y")) - 86400);	
}
$objPbsMail = new clsPbsMail();
$objPbsMail->createMailList($end_this_month, $end_next_month );
$objMailListe = new clsMailListe($objPbsMail->maillistid);
$smarty = new Smarty_Puls3060adm();
$smarty->caching = false;
$smarty->assign("mailliste",$objMailListe);
//$smarty->register_object("mailliste",$objMailListe);
$smarty->display('mail_pbs/test.tpl');

//****$objPbsMail->sendMailList();

?>
