<?php
require('clsPuls3060SQL.php');
require("mailFunctions.inc");
require ("quickpay_puls3060.php");


/* = = = = = = = = = = = = = = = = = = = = */
class clsLobUserTilmelding {

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
	protected $_fodtaar;
	protected $_kon;
	protected $_afdeling;
	protected $_distance;
	protected $_gruppe;
	protected $_lobsafgift;
	protected $_harbetaltdato;
	protected $_lobid;
	protected $_nummer;
	protected $_nrtype;
	protected $_lobnavn;
	protected $_lobdato;
	protected $_afhente_dit_loebsnummer;
	protected $_ip;
	protected $_afdnavn;
	protected $_dist;
	protected $_distid;
	protected $_grup;
	protected $_grupid;
	protected $_cardnumber;
	protected $_expirationdate;
	protected $_cvd;
	protected $_mail_sent = false;
	protected $_afdeling_error = null;
	protected $_fornavn_error = null;
	protected $_efternavn_error = null;
	protected $_adresse_error = null;
	protected $_postnr_error = null;
	protected $_bynavn_error = null;
	protected $_tlfnr_error = null;
	protected $_mailadr_error = null;
	protected $_fodtaar_error = null;
	protected $_kon_error = null;
	protected $_cardnumber_error = null;
	protected $_expirationdate_error = null;
	protected $_page_error = null;
	protected $_cvd_error = null;
	protected $_linked = false;
	protected $_ordernum = 0;

	public function __construct(){
		return ;
	} // ctor

	public function setDB($inDB){
		$this->_DB = $inDB;
		return ;
	} // ctor

	public function setLobId($inLobId){
		if ((isset($inLobId)) &&  ($inLobId > 0)) {
			$this->_lobid = $inLobId;
		}
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
			case "fodtaar":
			case "kon":
			case "afdeling":
			case "distance":
			case "gruppe":
			case "lobsafgift":
			case "harbetaltdato":
			case "lobid":
			case "nummer":
			case "nrtype":
			case "lobnavn":
			case "lobdato":
			case "afhente_dit_loebsnummer":
			case "ip":
			case "afdnavn":
			case "dist":
			case "distid":
			case "grup":
			case "grupid":
			case "cardnumber":
			case "expirationdate":
			case "cvd":
			case "mail_sent":
			case "afdeling_error":
			case "fornavn_error":
			case "efternavn_error":
			case "adresse_error":
			case "postnr_error":
			case "bynavn_error":
			case "tlfnr_error":
			case "mailadr_error":
			case "fodtaar_error":
			case "kon_error":
			case "cardnumber_error":
			case "expirationdate_error":
			case "cvd_error":
			case "page_error":
			case "linked":
			case "ordernum":
				$_prop = '_' . $property;
				return $this->$_prop;
				break;

			case "afdeling_error_display":
				if ($this->_afdeling_error != null) return true;
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

			case "fodtaar_error_display":
				if ($this->_fodtaar_error != null) return true;
				break;

			case "kon_error_display":
				if ($this->_kon_error != null) return true;
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

			case "afdeling_dropdown_html":
				$select_afdeling = (isset($this->_afdeling)) ? $this->_afdeling : 0;
				if ($this->_DB->query("SELECT getafdelinger($this->_lobid, $select_afdeling) AS html", SQL_INIT, SQL_ASSOC)) {
					if (isset($this->_DB->record['html'])){
						return $this->_DB->record['html'];
					}
					else {
						throw new Exception("lobid is not found");
					}
				}
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
			case "fodtaar":
			case "kon":
			case "afdeling":
			case "distance":
			case "gruppe":
			case "lobsafgift":
			case "harbetaltdato":
			case "lobid":
			case "nummer":
			case "nrtype":
			case "lobnavn":
			case "lobdato":
			case "afhente_dit_loebsnummer":
			case "afdnavn":
			case "dist":
			case "distid":
			case "grup":
			case "grupid":
			case "ordernum":
				$_prop = '_' . $property;
				$this->$_prop = $value;
				break;

			default:
				throw new Exception("$property is not a valid property");
				break;
		}
	}

	public function getLob(){
		if (isset($this->_lobid)){
			if ($this->_DB->query("SELECT * FROM tbllob WHERE id = $this->_lobid", SQL_INIT, SQL_ASSOC)) {
				if (isset($this->_DB->record['id'])){
					$this->_lobnavn = $this->_DB->record['navn'];
					$this->_lobdato = $this->_DB->record['dato'];
					$this->_nrtype = $this->_DB->record['nrtype'];
					$this->_afhente_dit_loebsnummer = $this->_DB->record['afhente_dit_loebsnummer'];
				}
				else {
					unset($this->_lobid);
				}
			}
		}

		if (!isset($this->_lobid)){
			if ($this->_DB->query("SELECT * FROM tbllob WHERE Dato > NOW() ORDER BY Dato ASC", SQL_INIT, SQL_ASSOC)) {
				if (isset($this->_DB->record['id'])){
					$this->_lobid = $this->_DB->record['id'];
					$this->_lobnavn = $this->_DB->record['navn'];
					$this->_lobdato = $this->_DB->record['dato'];
					$this->_nrtype = $this->_DB->record['nrtype'];
					$this->_afhente_dit_loebsnummer = $this->_DB->record['afhente_dit_loebsnummer'];
				}
				else {
					throw new Exception("lobid is not found");
				}
			}
		}

	}

	public function getLink($linkid){
		if (isset($linkid)) {
			if ($this->_DB->query("SELECT * FROM tbllink WHERE linkid = '" . $linkid . "'", SQL_INIT, SQL_ASSOC)) {
				$this->_personid = $this->_DB->record['p3060_ref'];
				$this->_lobid = $this->_DB->record['lob_ref'];
				$this->_linked = true;

			}

			if ($this->_DB->query("SELECT * FROM tblperson WHERE id = $this->_personid ", SQL_INIT, SQL_ASSOC)) {
				$this->_fornavn = $this->_DB->record['fornavn'];
				$this->_efternavn = $this->_DB->record['efternavn'];
				$this->_adresse = $this->_DB->record['adresse'];
				$this->_postnr = $this->_DB->record['postnr'];
				$this->_bynavn = $this->_DB->record['bynavn'];
			}

			if ($this->_DB->query("SELECT * FROM tblpersonlig WHERE personid = $this->_personid ", SQL_INIT, SQL_ASSOC)) {
				$this->_fodtaar = $this->_DB->record['fodtaar'];
				$this->_kon = $this->_DB->record['kon'];
			}

			if ($this->_DB->query("SELECT * FROM tbltelefon WHERE personid = $this->_personid ", SQL_INIT, SQL_ASSOC)) {
				$this->_tlfnr = $this->_DB->record['tlfnr'];
			}

			if ($this->_DB->query("SELECT * FROM tblmailadresse WHERE personid = $this->_personid ", SQL_INIT, SQL_ASSOC)) {
				$this->_mailadr = $this->_DB->record['mailadr'];
			}

			if ($this->_DB->query("SELECT * FROM tbllobdeltager WHERE personid = $this->_personid ORDER BY id DESC LIMIT 1", SQL_INIT, SQL_ASSOC)) {
				$this->_afdeling = $this->_DB->record['afdeling'];
			}
		}
	}

	public function getFormDataLobUserTilmelding(){
		if (isset($_POST['afdeling'])) $this->_afdeling=$_POST['afdeling'];
		if (isset($_POST['fornavn'])) $this->_fornavn=$_POST['fornavn'];
		if (isset($_POST['efternavn'])) $this->_efternavn=$_POST['efternavn'];
		if (isset($_POST['adresse'])) $this->_adresse=$_POST['adresse'];
		if (isset($_POST['postnr'])) $this->_postnr=$_POST['postnr'];
		if (isset($_POST['bynavn'])) $this->_bynavn=$_POST['bynavn'];
		if (isset($_POST['tlfnr'])) $this->_tlfnr=$_POST['tlfnr'];
		if (isset($_POST['mailadr'])) $this->_mailadr=$_POST['mailadr'];
		if (isset($_POST['fodtaar'])) $this->_fodtaar=$_POST['fodtaar'];
		if (isset($_POST['kon'])) $this->_kon=$_POST['kon'];
		if (isset($_SERVER['REMOTE_ADDR'])) $this->_ip=$_SERVER['REMOTE_ADDR'];

		if ($this->_DB->prepare("SELECT * from gettilmelding(?, ?, ?, ?)")) {
			$data = array($this->_lobid, $this->_fodtaar, $this->_kon, $this->_afdeling);
			if ($this->_DB->execute($data)) {
				if ($this->_DB->next(SQL_ASSOC)) {
					$this->_afdnavn = $this->_DB->record['afdnavn'];
					$this->_dist = $this->_DB->record['dist'];
					$this->_distid = $this->_DB->record['distid'];
					$this->_grup = $this->_DB->record['grup'];
					$this->_grupid = $this->_DB->record['grupid'];
					$this->_lobsafgift = $this->_DB->record['lobsafgift'];
				}
			}
		}
	}

	public function validateLobUserTilmelding() {
		$this->_page_error = null;
		$this->_afdeling_error = null;
		$this->_fornavn_error = null;
		$this->_efternavn_error = null;
		$this->_adresse_error = null;
		$this->_postnr_error = null;
		$this->_bynavn_error = null;
		$this->_tlfnr_error = null;
		$this->_mailadr_error = null;
		$this->_fodtaar_error = null;
		$this->_kon_error = null;

		if (strlen(trim($this->_afdeling)) == 0) {
			$this->_afdeling_error = "Afdeling skal vælges";
		}
		elseif (!ereg("^[1-9]{1,1}$", $this->_afdeling)) {
			$this->_afdeling_error = "Afdeling skal vælges";
		}

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

		if (strlen(trim($this->_fodtaar)) == 0) {
			$this->_fodtaar_error = "Fødsels År skal udfyldes";
		}
		elseif (!ereg("^[1-2]{1}[09]{1}[0-9]{2}$", $this->_fodtaar)) {
			$this->_fodtaar_error = "Fødsels År skal udfyldes korrekt";
		}
		else {
			$nu =strptime (date('d-m-Y', strtotime("now")), '%d-%m-%Y');
			$alder =$nu['tm_year'] + 1900 - $this->_fodtaar;
			if (($alder < 1) || ($alder > 100)) {
				$this->_fodtaar_error = "Alder skal være mellem 2 og 99 år";
			}
		}

		if (strlen(trim($this->_kon)) == 0) {
			$this->_kon_error = "Køn skal udfyldes";
		}
		elseif (!ereg("^[mk]{1,1}$", $this->_kon)) {
			$this->_kon_error = "Køn skal udfyldes med m eller k";
		}

		if (($this->_afdeling_error == null)
		&& ($this->_fornavn_error == null)
		&& ($this->_efternavn_error == null)
		&& ($this->_adresse_error == null)
		&& ($this->_postnr_error == null)
		&& ($this->_bynavn_error == null)
		&& ($this->_tlfnr_error == null)
		&& ($this->_mailadr_error == null)
		&& ($this->_fodtaar_error == null)
		&& ($this->_kon_error == null)
		&& ($this->_page_error == null)) {
			return true;
		}
		else {
			return false;
		}
	}

	public function getFormDataLobUserTilmeldingBetaling(){
		if (isset($_POST['cardnumber'])) $this->_cardnumber=$_POST['cardnumber'];
		if ((isset($_POST['expire_year'])) &&  (isset($_POST['expire_month']))) {
			 $this->_expirationdate = $_POST['expire_year'] . $_POST['expire_month'];
		}
		if (isset($_POST['cvd'])) $this->_cvd=$_POST['cvd'];
	}

	public function validateLobUserTilmeldingBetaling() {
		$this->_page_error = null;
		$this->_cardnumber_error = null;
		$this->_expirationdate_error = null;
		$this->_cvd_error = null;

		if (strlen(trim($this->_cardnumber)) == 0) {
			$this->_cardnumber_error = "Kortnummer skal udfyldes";
		}elseif (strlen(trim($this->_cardnumber)) != 16) {
			$this->_cardnumber_error = "Kortnummer skal udfyldes med alle 16 cifre uden mellemrum";
//		}elseif (substr($this->_cardnumber,0,4) != '4571' ) {
//			$this->_cardnumber_error = "Kortnummer er ikke et Dankort";
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
		$this->getNextOrdernum();
		$qp->set_ordernum($this->_ordernum); // MUST at least be of length 4
		$qp->set_amount($this->_lobsafgift*100);
		// Commit the authorization
		$eval = $qp->authorize();

		if ($eval) {
			if ($eval['qpstat'] === '000') {
				// The authorization was completed
				$this->_DB->autoCommit(false);
				if ($this->_DB->prepare("INSERT INTO tblpbscardpayment (ordernum, transaction, amount, cardtype, currency, merchant, merchantemail, msgtype, pbsstat, qstat, qpstatmsg, time) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)")) {
					$data = array($eval['ordernum'], $eval['transaction'], $eval['amount'], $eval['cardtype'], $eval['currency'], $eval['merchant'], $eval['merchantemail'], $eval['msgtype'], $eval['pbsstat'], $eval['qstat'], $eval['qpstatmsg'], $eval['time']);
					if ($this->_DB->execute($data)) {
						$this->_DB->commit();
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
		$this->_DB->autoCommit(false);
		if ($this->_DB->query("select nextval('tblpbscardpayment_ordernum_seq') as ordernum", SQL_INIT, SQL_ASSOC)) {
			$this->_ordernum = $this->_DB->record['ordernum'];
			$this->_DB->commit();
		}
	}

	public function ReverseAuthorize_Dankort(){
		if ((isset($this->_ordernum)) && ($this->_ordernum > 0)) {
			$query = "SELECT transaction FROM tblpbscardpayment WHERE ordernum = $this->_ordernum";
			if ($this->_DB->query($query, SQL_INIT, SQL_ASSOC)) {
				$eval = false;
				$qp = new quickpay_puls3060;
				// Set values
				$qp->set_msgtype('1420');
				$qp->set_transaction($this->_DB->record['transaction']);
				$eval = $qp->reverse();
				if ($eval) {
					if ($eval['qpstat'] === '000') {
						// The reversel was completed
						$this->_ordernum = 0;
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
				return false;
			}
		}
		else {
			return true;
		}
	}


	public function lobtilmelding(){
		$this->_DB->autoCommit(false);
		if ($this->_DB->prepare("INSERT INTO tbllobtilmelding (fornavn, efternavn, adresse, postnr, bynavn, tlfnr, mailadr, fodtaar, kon, distance, gruppe, lobsafgift, harbetaltdato, ip, afdeling, ordernum) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)")) {
			$data = array($this->_fornavn, $this->_efternavn, $this->_adresse, $this->_postnr, $this->_bynavn, $this->_tlfnr, $this->_mailadr, $this->_fodtaar, $this->_kon, $this->_distid, $this->_grupid, $this->_lobsafgift, $this->_harbetaltdato, $this->_ip, $this->_afdeling, $this->ordernum);
			if ($this->_DB->execute($data)) {
				if ($this->_DB->query("select currval('tbllobtilmelding_id_seq') as insert_id", SQL_INIT, SQL_ASSOC)) {
					$this->_id = $this->_DB->record['insert_id'];
					if ($this->nrtype == "papir") {
					   $query = "
					   			 SELECT tblnrpulje.id AS nummer FROM tblnrpulje
            				     WHERE tblnrpulje.nrtype='papir'
								 AND tblnrpulje.id  NOT IN (SELECT nummer FROM tblnrtildeling WHERE lobid IN (select id from tbllob where dato > now() and nrtype = 'papir'))
            				     ORDER BY tblnrpulje.id DESC
            				    ";
					}
					else {
					   $query = "
					             SELECT tblnrpulje.id AS nummer FROM tblnrpulje
            				     WHERE tblnrpulje.nrtype='stof'
            				     AND tblnrpulje.id  NOT IN (SELECT nummer FROM tblnrtildeling WHERE lobid = $this->lobid)
            				     ORDER BY tblnrpulje.id DESC
            				    ";
					}
					if ($this->_DB->query($query, SQL_INIT, SQL_ASSOC)) {
						$this->_nummer = $this->_DB->record['nummer'];
						if ($this->_DB->prepare("INSERT INTO tblnrtildeling (lobid, nummer, lobtilmeldingid) VALUES(?, ?, ?)")) {
							$data = array($this->_lobid, $this->_nummer, $this->_id);
							if ($this->_DB->execute($data)) {
								$this->_DB->commit();
								//send mail
								$subj = "Tilmelding til $this->lobnavn";
								$text_smarty = new Smarty_Puls3060dk;
								$text_smarty->assign("tilmelding",$this);
								$body = $text_smarty->fetch("Test/LobUserTilmeldingMail.tpl");
								mail($this->_mailadr, $subj, $body, "From: mha@puls3060.dk\nBcc: mha@hafsjold.dk\nReply-To: mha@puls3060.dk\nX-Mailer: PHP", "-f mha@puls3060.dk");
								$this->_mail_sent = true;
							}
						}
					}
				}
			}
		}
	}

}
// class clsLobUserTilmelding

/* = = = = = = = = = = = = = = = = = = = = */
?>