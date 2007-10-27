<?php
require_once('clsPuls3060SQL.php');
require_once('clsRegnskab3060SQL.php');
require("mailFunctions.inc");
require ("quickpay_puls3060.php");
require_once('email_message.php');   

/* = = = = = = = = = = = = = = = = = = = = */
class clsAktUserTilmelding {

	protected $_DBPuls3060;
	protected $_DBRegnskab3060;
	protected $_qp;
	protected $_id;
	protected $_personid;
	protected $_fornavn;
	protected $_efternavn;
	protected $_adresse;
	protected $_postnr;
	protected $_bynavn;
	protected $_tlfnr;
	protected $_mailadr;
	protected $_aktafgift;
	protected $_aktid;
	protected $_aktnavn;
	protected $_aktdato;
	protected $_tilmelding_er_slut;
	protected $_ip;
	protected $_cardnumber;
	protected $_expirationdate;
	protected $_cvd;
	protected $_mail_sent = false;
	protected $_fornavn_error = null;
	protected $_efternavn_error = null;
	protected $_adresse_error = null;
	protected $_postnr_error = null;
	protected $_bynavn_error = null;
	protected $_tlfnr_error = null;
	protected $_mailadr_error = null;
	protected $_cardnumber_error = null;
	protected $_expirationdate_error = null;
	protected $_page_error = null;
	protected $_cvd_error = null;
	protected $_linked = false;
	protected $_ordernum = 0;
	protected $_transaction;

	public function __construct(){
		return ;
	} // ctor

	public function setDB($inDBPuls3060, $inDBRegnskab3060){
		$this->_DBPuls3060 = $inDBPuls3060;
		$this->_DBRegnskab3060 = $inDBRegnskab3060;
		return ;
	} // ctor

	public function setAktId($inAktId){
		if ((isset($inAktId)) &&  ($inAktId > 0)) {
			$this->_aktid = $inAktId;
		}
		return ;
	} // ctor

	public function setQuickpay(){
		$this->_qp = new quickpay_puls3060();
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
			case "aktafgift":
			case "aktid":
			case "aktnavn":
			case "aktdato":
			case "tilmelding_er_slut":
			case "ip":
			case "cardnumber":
			case "expirationdate":
			case "cvd":
			case "mail_sent":
			case "fornavn_error":
			case "efternavn_error":
			case "adresse_error":
			case "postnr_error":
			case "bynavn_error":
			case "tlfnr_error":
			case "mailadr_error":
			case "cardnumber_error":
			case "expirationdate_error":
			case "cvd_error":
			case "page_error":
			case "linked":
			case "ordernum":
			case "transaction":
				$_prop = '_' . $property;
				return $this->$_prop;
				break;

			case "fornavn_error_display":
				if ($this->_fornavn_error != null) return true;
				break;

			case "efternavn_error_display":
				if ($this->_efternavn_error != null) return true;
				break;

			case "adresse_error_display":
				if ($this->_adresse_error != null) return true;
				break;

			case "postnr_error_display":
				if ($this->_postnr_error != null) return true;
				break;

			case "bynavn_error_display":
				if ($this->_bynavn_error != null) return true;
				break;

			case "tlfnr_error_display":
				if ($this->_tlfnr_error != null) return true;
				break;

			case "mailadr_error_display":
				if ($this->_mailadr_error != null) return true;
				break;

			case "cardnumber_error_display":
				if ($this->_cardnumber_error != null) return true;
				break;

			case "expirationdate_error_display":
				if ($this->_expirationdate_error != null) return true;
				break;

			case "cvd_error_display":
				if ($this->_cvd_error != null) return true;
				break;

			case "page_error_display":
				if ($this->_page_error != null) return true;
				break;

			case "autocapture":
				return "0";
				break;

			case "merchantid":
				return $this->_qp->get_merchant();
				break;

			case "amount":
				return $this->_aktafgift*100;
				break;

			case "currency":
				return $this->_qp->get_currency();
				break;

			case "okpage":
				return "https://www.puls3060.dk/kalmnu/Kalender/AktUserTilmelding.php?DoWhat=14&Card=eDank";
				break;

			case "errorpage":
				return "https://www.puls3060.dk/kalmnu/Kalender/AktUserTilmelding.php?DoWhat=14&Card=eDank&Err=1";
				break;

			case "resultpage":
				return "https://www.puls3060.dk/pbsresult/AktUserTilmeldingResult.php";
				break;

			case "md5word":
				return $this->_qp->get_md5checkword();
				break;

			case "md5check":
				return md5($this->autocapture.$this->ordernum.$this->amount.$this->currency.$this->merchantid.$this->okpage.$this->errorpage.$this->resultpage.$this->md5word);
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
			case "aktafgift":
			case "aktid":
			case "aktnavn":
			case "aktdato":
			case "tilmelding_er_slut":
			case "ordernum":
				$_prop = '_' . $property;
				$this->$_prop = $value;
				break;

			default:
				throw new Exception("$property is not a valid property");
				break;
		}
	}

	private function pbsdt2sqldt($dt){
		//$dt format 'ååmmddhhmmss'
		$sqldt =  mktime(substr($dt,6,2), substr($dt,8,2), substr($dt,10,2), substr($dt,2,2), substr($dt,4,2), substr($dt,0,2)); //mktime(hh,mm,ss,mm,dd,yy)
		return date("Y-m-d H:i:s", $sqldt);
	}

	public function getAkt(){
		if (isset($this->_aktid)){
			if ($this->_DBPuls3060->query("SELECT (date_part('days',(tilmeldingslut-now())) < 0)  AS tilmelding_er_slut, * FROM tblklinie WHERE klinieid = $this->_aktid", SQL_INIT, SQL_ASSOC)) {
				if (isset($this->_DBPuls3060->record['klinieid'])){
					$this->_aktnavn = $this->_DBPuls3060->record['klinieoverskrift'];
					$this->_aktdato = $this->_DBPuls3060->record['kliniedato'];
					$this->_aktafgift = $this->_DBPuls3060->record['aktafgift'];
					$this->_tilmelding_er_slut = $this->_DBPuls3060->record['tilmelding_er_slut'];
				}
				else {
					unset($this->_aktid);
				}
			}
		}
	}

	public function getLink($linkid){
		if (isset($linkid)) {
			if ($this->_DBPuls3060->query("SELECT * FROM tbllink WHERE linkid = '" . $linkid . "'", SQL_INIT, SQL_ASSOC)) {
				$this->_personid = $this->_DBPuls3060->record['p3060_ref'];
				$this->_aktid = $this->_DBPuls3060->record['akt_ref'];
				$this->_linked = true;

			}

			if ($this->_DBPuls3060->query("SELECT * FROM tblperson WHERE id = $this->_personid ", SQL_INIT, SQL_ASSOC)) {
				$this->_fornavn = $this->_DBPuls3060->record['fornavn'];
				$this->_efternavn = $this->_DBPuls3060->record['efternavn'];
				$this->_adresse = $this->_DBPuls3060->record['adresse'];
				$this->_postnr = $this->_DBPuls3060->record['postnr'];
				$this->_bynavn = $this->_DBPuls3060->record['bynavn'];
			}

			if ($this->_DBPuls3060->query("SELECT * FROM tbltelefon WHERE personid = $this->_personid ", SQL_INIT, SQL_ASSOC)) {
				$this->_tlfnr = $this->_DBPuls3060->record['tlfnr'];
			}

			if ($this->_DBPuls3060->query("SELECT * FROM tblmailadresse WHERE personid = $this->_personid ", SQL_INIT, SQL_ASSOC)) {
				$this->_mailadr = $this->_DBPuls3060->record['mailadr'];
			}
		}
	}

	public function getFormDataAktUserTilmelding(){
		if (isset($_POST['fornavn'])) $this->_fornavn=$_POST['fornavn'];
		if (isset($_POST['efternavn'])) $this->_efternavn=$_POST['efternavn'];
		if (isset($_POST['adresse'])) $this->_adresse=$_POST['adresse'];
		if (isset($_POST['postnr'])) $this->_postnr=$_POST['postnr'];
		if (isset($_POST['bynavn'])) $this->_bynavn=$_POST['bynavn'];
		if (isset($_POST['tlfnr'])) $this->_tlfnr=$_POST['tlfnr'];
		if (isset($_POST['mailadr'])) $this->_mailadr=$_POST['mailadr'];
		if (isset($_SERVER['REMOTE_ADDR'])) $this->_ip=$_SERVER['REMOTE_ADDR'];
	}

	public function validateAktUserTilmelding() {
		$this->_page_error = null;
		$this->_fornavn_error = null;
		$this->_efternavn_error = null;
		$this->_adresse_error = null;
		$this->_postnr_error = null;
		$this->_bynavn_error = null;
		$this->_tlfnr_error = null;
		$this->_mailadr_error = null;

		if (strlen(trim($this->_fornavn)) == 0) {
			$this->_fornavn_error = "Fornavn skal udfyldes";
		}

		if (strlen(trim($this->_efternavn)) == 0) {
			$this->_efternavn_error = "Efternavn skal udfyldes";
		}

		if (strlen(trim($this->_adresse)) == 0) {
			$this->_adresse_error = "Adresse skal udfyldes";
		}

		if (strlen(trim($this->_postnr)) == 0) {
			$this->_postnr_error = "Postnummer skal udfyldes";
		}
		elseif (!ereg("^[1-9]{1}[0-9]{3}$", $this->_postnr)) {
			$this->_postnr_error = "Postnummer skal være mellem 1000 og 9900";
		}

		if (strlen(trim($this->_bynavn)) == 0) {
			$this->_bynavn_error = "By skal udfyldes";
		}

		if (strlen(trim($this->_tlfnr)) == 0) {
			$this->_tlfnr_error = "Telefon skal udfyldes";
		}
		elseif (!ereg("^[1-9]{1}[0-9]{7}$", $this->_tlfnr)) {
			$this->_tlfnr_error = "Telefon skal være mellem 10000000 og 99999999";
		}

		if (strlen(trim($this->_mailadr)) == 0) {
			$this->_mailadr_error = "E-mail skal udfyldes";
		}
		elseif (!ereg("^[a-zA-Z_0-9\\.\\-]{1,40}@([a-zA-Z0-9\\-]{1,40}\\.){1,5}[a-zA-Z]{2,3}$", $this->_mailadr)) {
			$this->_mailadr_error = "E-mail skal udfyldes korrekt";
		}
		elseif (MailVal($this->_mailadr, 3)) {
			$this->_mailadr_error = "Denne e-mail findes ikke";
		}

		if (($this->_fornavn_error == null)
		&& ($this->_efternavn_error == null)
		&& ($this->_adresse_error == null)
		&& ($this->_postnr_error == null)
		&& ($this->_bynavn_error == null)
		&& ($this->_tlfnr_error == null)
		&& ($this->_mailadr_error == null)
		&& ($this->_page_error == null)) {
			return true;
		}
		else {
			return false;
		}
	}

	public function getFormDataAktUserTilmeldingBetaling(){
		if (isset($_POST['cardnumber'])) $this->_cardnumber=$_POST['cardnumber'];
		if ((isset($_POST['expire_year'])) &&  (isset($_POST['expire_month']))) {
			$this->_expirationdate = $_POST['expire_year'] . $_POST['expire_month'];
		}
		if (isset($_POST['cvd'])) $this->_cvd=$_POST['cvd'];
	}

	public function validateAktUserTilmeldingBetaling() {
		$this->_page_error = null;
		$this->_cardnumber_error = null;
		$this->_expirationdate_error = null;
		$this->_cvd_error = null;

		if (strlen(trim($this->_cardnumber)) == 0) {
			$this->_cardnumber_error = "Kortnummer skal udfyldes";
		}elseif (strlen(trim($this->_cardnumber)) != 16) {
			$this->_cardnumber_error = "Kortnummer skal udfyldes med alle 16 cifre uden mellemrum";
        }elseif ((substr($this->_cardnumber,0,4) != '4571' ) and (substr($this->_cardnumber,0,4) != '5019' ) ){ 
			$this->_cardnumber_error = "Kortnummer er ikke et Dankort";
		}

		if (strlen(trim($this->_expirationdate)) == 0) {
			$this->_expirationdate_error = "Udløbsdato til skal udfyldes";
		}

		if (strlen(trim($this->_cvd)) == 0) {
			$this->_cvd_error = "cvd skal udfyldes";
		}

		if (($this->_cardnumber_error == null)
		&& ($this->_expirationdate_error == null)
		&& ($this->_cvd_error == null)
		&& ($this->_page_error == null)) {
			$this->Authorize_Dankort();
		}

		if (($this->_cardnumber_error == null)
		&& ($this->_expirationdate_error == null)
		&& ($this->_cvd_error == null)
		&& ($this->_page_error == null)) {
			return true;
		}
		else {
			return false;
		}
	}

	public function validateAktUserTilmeldingBekraft($pageerror) {
		$this->_page_error = null;

		$this->_page_error = $pageerror;

		if ($this->_page_error == null) {
			return true;
		}
		else {
			return false;
		}
	}

	public function Authorize_Dankort() {
		$this->_page_error = null;

		$eval = false;
		$qp = new quickpay_puls3060;
		// Set values
		$qp->set_msgtype('1100');
		$qp->set_cardnumber($this->_cardnumber);
		$qp->set_expirationdate($this->_expirationdate);
		$qp->set_cvd($this->_cvd);
		$qp->set_ordernum($this->_ordernum); // MUST at least be of length 4
		$qp->set_amount($this->_aktafgift*100);
		// Commit the authorization
		$eval = $qp->authorize();

		if ($eval) {
			if ($eval['qpstat'] === '000') {
				// The authorization was completed
				$this->_DBRegnskab3060->autoCommit(false);
				if ($this->_DBRegnskab3060->prepare("INSERT INTO quickpay.tbltransaction (ordernum, transaction, currency, cardtype) VALUES (?, ?, ?, ?)")) {
					$hdata = array($eval['ordernum'], $eval['transaction'], $eval['currency'], 'Dankort');
					if ($this->_DBRegnskab3060->execute($hdata)) {
						$this->_DBRegnskab3060->commit();
						$this->_transaction = $eval['transaction'];
						if ($this->_DBRegnskab3060->prepare("INSERT INTO quickpay.tbltransactionhistory (ordernum, amount, state, time) VALUES (?, ?, ?, ?)")) {
							$tdata = array($eval['ordernum'], $eval['amount']/100, '1', $this->pbsdt2sqldt($eval['time']));
							if ($this->_DBRegnskab3060->execute($tdata)) {
								$this->_DBRegnskab3060->commit();
							}
						}
					}
				}
				return true;
			} else {
				// An error occured with the authorize
				$qpstatText = array(
				"000" => "Godkendt",
				"001" => "Afvist af PBS",
				"002" => "Kommunikations fejl",
				"003" => "Kort udløbet",
				"004" => "Status er forkert (Ikke autoriseret)",
				"005" => "Autorisation er forældet",
				"006" => "Fejl hos PBS",
				"007" => "Fejl hos QuickPay",
				"008" => "Fejl i parameter sendt til QuickPay"
				);
				$this->_page_error = 'Authorization: ' . $qpstatText["" . $eval['qpstat'] . ""];
				return false;
			}
		} else {
			// Communication error
			$this->_page_error = "Communication error";
			return false;
		}
	}

	public function getNextOrdernum(){
		$this->_DBPuls3060->autoCommit(false);
		if ($this->_DBPuls3060->query("select nextval('tblpbscardpayment_ordernum_seq') as ordernum", SQL_INIT, SQL_ASSOC)) {
			$this->_ordernum = $this->_DBPuls3060->record['ordernum'];
			$this->_DBPuls3060->commit();
		}
	}

	public function ReverseAuthorize_Dankort(){
		if ((isset($this->_transaction)) && ($this->_transaction > 0)) {
			$eval = false;
			$qp = new quickpay_puls3060;
			// Set values
			$qp->set_msgtype('1420');
			$qp->set_transaction($this->_DBPuls3060->record['transaction']);
			$eval = $qp->reverse();
			if ($eval) {
				if ($eval['qpstat'] === '000') {
					// The reversel was completed
					if ($this->_DBRegnskab3060->prepare("INSERT INTO quickpay.tbltransactionhistory (ordernum, amount, state, time) VALUES (?, ?, ?, ?)")) {
						$tdata = array($eval['ordernum'], $eval['amount']/100, '5', $this->pbsdt2sqldt($eval['time']));
						if ($this->_DBRegnskab3060->execute($tdata)) {
							$this->_DBRegnskab3060->commit();
						}
					}
					$this->_ordernum = 0;
					$this->_transaction = 0;
					return true;
				} else {
					// An error occured with the reversel
					return false;
				}
			} else {
				// Communication error
				return false;
			}
		}
		else {
			return true;
		}
	}

	public function StatusTrans_Dankort(){
		if ((isset($this->_ordernum)) && ($this->_ordernum > 0)) {
			$query = "SELECT transaction FROM quickpay.tbltransaction WHERE ordernum = $this->_ordernum";
			if ($this->_DBRegnskab3060->query($query, SQL_INIT, SQL_ASSOC)) {
				$this->_transaction = $this->_DBRegnskab3060->record['transaction'];
				$eval = false;
				$qp = new quickpay_puls3060;
				// Set values
				$qp->set_msgtype('status');
				$qp->set_transaction($this->_transaction);
				$eval = $qp->status();
				if ($eval) {
					if ($eval['qpstat'] === '000') {
						// The status was completed
						return true;
					} else {
						// An error occured with the status
						return false;
					}
				} else {
					// Communication error
					return false;
				}
			}
			else {
				return false;
			}
		}
		else {
			return true;
		}
	}
	public function akttilmelding($mode=1){
		$this->_DBPuls3060->autoCommit(false);
		if ($mode == 1){
			if ($this->_DBPuls3060->prepare("INSERT INTO tblakttilmelding (aktid, fornavn, efternavn, adresse, postnr, bynavn, tlfnr, mailadr, ip, ordernum, aktafgift) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)")) {
				$data = array($this->_aktid, $this->_fornavn, $this->_efternavn, $this->_adresse, $this->_postnr, $this->_bynavn, $this->_tlfnr, $this->_mailadr, $this->_ip, $this->ordernum, $this->_aktafgift);
				if ($this->_DBPuls3060->execute($data)) {
						if ($this->_DBPuls3060->query("select currval('tblakttilmelding_id_seq') as insert_id", SQL_INIT, SQL_ASSOC)) {
						$this->_id = $this->_DBPuls3060->record['insert_id'];
						$this->_DBPuls3060->commit();
						//send mail
						$subj = "Tilmelding til $this->aktnavn";
						$text_smarty = new Smarty_Puls3060dk;
						$text_smarty->assign("tilmelding",$this);
						$body = $text_smarty->fetch("Kalender/AktUserTilmeldingMail1.tpl");
						//mail($this->_mailadr, $subj, $body, "From: mha@puls3060.dk\nBcc: mha@hafsjold.dk\nReply-To: mha@puls3060.dk\nX-Mailer: PHP", "-f mha@puls3060.dk");
                        
						$email_message=new email_message_class;
                        $email_message->SetHeader("To",$this->_mailadr);
                        $email_message->SetHeader("Bcc","mha@hafsjold.dk");
                        $email_message->SetHeader("From","mha@puls3060.dk");
                        $email_message->SetHeader("Reply-To","mha@puls3060.dk");
                        $email_message->SetHeader("Sender","mha@puls3060.dk");
                        $email_message->SetHeader("Return-Path","mha@puls3060.dk");
                                  
                        $email_message->SetEncodedHeader("Subject",$subj);
                        $email_message->AddQuotedPrintableTextPart($email_message->WrapText($body));
                                  
                        $error=$email_message->Send();                						
						
						$this->_mail_sent = true;
					}
				}
			}
		}
		else {
			if ($this->_DBPuls3060->prepare("INSERT INTO tblakttilmelding (aktid, fornavn, efternavn, adresse, postnr, bynavn, tlfnr, mailadr, ip) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?)")) {
				$data = array($this->_aktid, $this->_fornavn, $this->_efternavn, $this->_adresse, $this->_postnr, $this->_bynavn, $this->_tlfnr, $this->_mailadr, $this->_ip);
				if ($this->_DBPuls3060->execute($data)) {
						if ($this->_DBPuls3060->query("select currval('tblakttilmelding_id_seq') as insert_id", SQL_INIT, SQL_ASSOC)) {
						$this->_id = $this->_DBPuls3060->record['insert_id'];
						$this->_DBPuls3060->commit();
						//send mail
						$subj = "Tilmelding til $this->aktnavn";
						$text_smarty = new Smarty_Puls3060dk;
						$text_smarty->assign("tilmelding",$this);
						$body = $text_smarty->fetch("Kalender/AktUserTilmeldingMail2.tpl");
						//mail($this->_mailadr, $subj, $body, "From: mha@puls3060.dk\nBcc: mha@hafsjold.dk\nReply-To: mha@puls3060.dk\nX-Mailer: PHP", "-f mha@puls3060.dk");
						
                        $email_message=new email_message_class;
                        $email_message->SetHeader("To",$this->_mailadr);
                        $email_message->SetHeader("Bcc","mha@hafsjold.dk");
                        $email_message->SetHeader("From","mha@puls3060.dk");
                        $email_message->SetHeader("Reply-To","mha@puls3060.dk");
                        $email_message->SetHeader("Sender","mha@puls3060.dk");
                        $email_message->SetHeader("Return-Path","mha@puls3060.dk");
                                  
                        $email_message->SetEncodedHeader("Subject",$subj);
                        $email_message->AddQuotedPrintableTextPart($email_message->WrapText($body));
                                  
                        $error=$email_message->Send();                        
                        
                        $this->_mail_sent = true;
					}
				}
			}
			
		}
	}
}

// class clsAktUserTilmelding

/* = = = = = = = = = = = = = = = = = = = = */
?>