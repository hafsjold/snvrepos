<?php
require('clsPuls3060SQL.php');
require("mailFunctions.inc");

/* = = = = = = = = = = = = = = = = = = = = */
class clsNytMedlem {

	protected $_DB;
	protected $_id;
	protected $_personid;
	protected $_fornavn;
	protected $_efternavn;
	protected $_adresse;
	protected $_postnr;
	protected $_bynavn;
	protected $_tlfnr;
	protected $_mailadr;
	protected $_fodtdato;
	protected $_kon;
	protected $_ip;
	protected $_indmeldtdato;
	protected $_kontingenttildato;
	protected $_kontingentkr;
	protected $_action;
	protected $_mail_sent = false;
	protected $_fornavn_error;
	protected $_efternavn_error;
	protected $_adresse_error;
	protected $_postnr_error;
	protected $_bynavn_error;
	protected $_tlfnr_error;
	protected $_mailadr_error;
	protected $_fodtdato_error;
	protected $_kon_error;

	public function __construct(){
		return ;
	} // ctor

	public function setDB($inDB){
		$this->_DB = $inDB;
		return ;
	} // ctor

	private function __get($property) {
		switch ($property) {
			case "id":
			case "personid":
			case "fornavn":
			case "efternavn":
			case "adresse":
			case "postnr":
			case "bynavn":
			case "tlfnr":
			case "mailadr":
			case "fodtdato":
			case "kon":
			case "ip":
			case "indmeldtdato":
			case "kontingenttildato":
			case "kontingentkr":
			case "action":
			case "mail_sent":
			case "fornavn_error":
			case "efternavn_error":
			case "adresse_error":
			case "postnr_error":
			case "bynavn_error":
			case "tlfnr_error":
			case "mailadr_error":
			case "fodtdato_error":
			case "kon_error":
				$_prop = '_' . $property;
				return $this->$_prop;
				break;

			case "postnr_validationexpression":
				return "[1-9]{1}[0-9]{3}";
				break;

			case "tlfnr_validationexpression":
				return "[1-9]{1}[0-9]{7}";
				break;

			case "mailadr_validationexpression":
				return "[a-zA-Z_0-9\\.\\-]{1,40}@([a-zA-Z0-9\\-]{1,40}\\.){1,5}[a-zA-Z]{2,3}";
				break;

			case "kon_validationexpression":
				return "[mk]{1,1}";
				break;

			case "fodtdato_validationexpression":
				return "[^-]+-[^-]+-[^-]+";
				break;

			default:
				throw new Exception("$property is not a valid property");
				break;
		}
		return null;
	}

	private function __set($property, $value) {
		switch ($property) {
			case "id":
			case "personid":
			case "fornavn":
			case "efternavn":
			case "adresse":
			case "postnr":
			case "bynavn":
			case "tlfnr":
			case "mailadr":
			case "fodtdato":
			case "kon":
				$_prop = '_' . $property;
				$this->$_prop = $value;
				break;

			default:
				throw new Exception("$property is not a valid property");
				break;
		}
	}

	public function getFormData(){
		if (isset($_POST['fornavn'])) $this->_fornavn=$_POST['fornavn'];
		if (isset($_POST['efternavn'])) $this->_efternavn=$_POST['efternavn'];
		if (isset($_POST['adresse'])) $this->_adresse=$_POST['adresse'];
		if (isset($_POST['postnr'])) $this->_postnr=$_POST['postnr'];
		if (isset($_POST['bynavn'])) $this->_bynavn=$_POST['bynavn'];
		if (isset($_POST['tlfnr'])) $this->_tlfnr=$_POST['tlfnr'];
		if (isset($_POST['mailadr'])) $this->_mailadr=$_POST['mailadr'];
		if (isset($_POST['fodtdato'])) $this->_fodtdato=$_POST['fodtdato'];
		if (isset($_POST['kon'])) $this->_kon=$_POST['kon'];
		if (isset($_SERVER['REMOTE_ADDR'])) $this->_ip=$_SERVER['REMOTE_ADDR'];

		if ($this->_DB->query("SELECT ((now())::date) as indmeldtdato, ((date_trunc('month', now()) + interval '1 year'  - interval '1 day')::date) as kontingenttildato", SQL_INIT, SQL_ASSOC)) {
			$this->_indmeldtdato = $this->_DB->record['indmeldtdato'];
			$this->_kontingenttildato = $this->_DB->record['kontingenttildato'];
			$this->_kontingentkr = 150;
		}
	}

	static public function validate_browser() {
		$browser = get_browser();
		if ($browser->browser == "IE") {
			if (($browser->majorver == 5)
			&& ($browser->minorver >= 5)
			|| ($browser->majorver > 5)) {
				return true;
			}
			else {
				return false;
			}
		}
		else {
			return false;
		}
	}
	
	public function validate() {
		if (MailVal($this->_mailadr, 3)) {
			$this->_mailadr_error = "Denne e-mail adresse findes ikke";
			return false;
		}
		else {
			return true;
		}
	}

	public function tblnytmedlem(){

		$this->_DB->autoCommit(false);
		if ($this->_DB->prepare("INSERT INTO tblnytmedlem (fornavn, efternavn, adresse, postnr, bynavn, tlfnr, mailadr, fodtdato, kon, indmeldtdato, kontingenttildato, kontingentkr, action, ip) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)")) {
			$data = array($this->_fornavn, $this->_efternavn, $this->_adresse, $this->_postnr, $this->_bynavn, $this->_tlfnr, $this->_mailadr, $this->_fodtdato, $this->_kon, $this->_indmeldtdato, $this->_kontingenttildato, $this->_kontingentkr, $this->_action, $this->_ip);
			if ($this->_DB->execute($data)) {
				if ($this->_DB->query("select currval('tblnytmedlem_id_seq') as insert_id", SQL_INIT, SQL_ASSOC)) {
					$this->_id = $this->_DB->record['insert_id'];
					$this->_DB->commit();
					//send mail
					$subj = "Medlemskab af Puls 3060";
					$text_smarty = new Smarty_Puls3060dk;
					$text_smarty->caching = false;
					$text_smarty->assign("medlem",$this);
					$body = $text_smarty->fetch("Medlem/NytMedlemMail.tpl");
					mail($this->_mailadr, $subj, $body, "From: mha@puls3060.dk\nBcc: mha@hafsjold.dk\nReply-To: mha@puls3060.dk\nX-Mailer: PHP", "-f mha@puls3060.dk");
					$this->_mail_sent = true;
				}
			}
		}
	}

} // class clsNytMedlem

/* = = = = = = = = = = = = = = = = = = = = */
?>