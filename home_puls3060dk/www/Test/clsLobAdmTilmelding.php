<?php
require('clsPuls3060SQL.php');
require("mailFunctions.inc");

/* = = = = = = = = = = = = = = = = = = = = */
class clsLobAdmTilmelding {

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
	protected $_lobnavn;
	protected $_lobdato;
	protected $_ip;
	protected $_afdnavn;
	protected $_dist;
	protected $_distid;
	protected $_grup;
	protected $_grupid;
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
	protected $_nummer_error = null;
	protected $_personid_error = null;
	protected $_linked = false;
	protected $_nr_udleveret = 1;


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
			case "lobnavn":
			case "lobdato":
			case "ip":
			case "afdnavn":
			case "dist":
			case "distid":
			case "grup":
			case "grupid":
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
			case "nummer_error":
			case "personid_error":
			case "linked":
			case "nr_udleveret":
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

			case "nummer_error_display":
				if ($this->_nummer_error != null) return true;
				break;

			case "personid_error_display":
				if ($this->_personid_error != null) return true;
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

			case "afhente_dit_loebsnummer":
				if ($this->_lobid == 76){
					return "Du kan afhente dit løbsnummer på løbsdagen lørdag den 29. april mellem 9.00 og 10.30 på Vestre Torv i Espergærde Centret.";
				}else{
					return "Du kan afhente dit løbsnummer på løbsdagen mellem 9.15 og 9.45 ved startstedet i Egebæks Vang skov.";
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
			case "lobnavn":
			case "lobdato":
			case "afdnavn":
			case "dist":
			case "distid":
			case "grup":
			case "grupid":
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
				}
				else {
					unset($this->_lobid);
				}
			}
		}

		if (!isset($this->_lobid)){
			if ($this->_DB->query("SELECT * FROM tbllob WHERE Dato > (now()- interval '48 hours') ORDER BY Dato ASC", SQL_INIT, SQL_ASSOC)) {
				if (isset($this->_DB->record['id'])){
					$this->_lobid = $this->_DB->record['id'];
					$this->_lobnavn = $this->_DB->record['navn'];
					$this->_lobdato = $this->_DB->record['dato'];
				}
				else {
					throw new Exception("lobid is not found");
				}
			}
		}

	}

	public function getPerson(){
		if ((isset($this->_personid))
		&& ($this->_personid > 0)){
			if ($this->_DB->query("SELECT * FROM tblperson WHERE id = $this->_personid ", SQL_INIT, SQL_ASSOC)) {
				if (strlen(trim($this->_fornavn)) == 0) $this->_fornavn = $this->_DB->record['fornavn'];
				if (strlen(trim($this->_efternavn)) == 0) $this->_efternavn = $this->_DB->record['efternavn'];
				if (strlen(trim($this->_adresse)) == 0) $this->_adresse = $this->_DB->record['adresse'];
				if (strlen(trim($this->_postnr)) == 0) $this->_postnr = $this->_DB->record['postnr'];
				if (strlen(trim($this->_bynavn)) == 0) $this->_bynavn = $this->_DB->record['bynavn'];
			}

			if ($this->_DB->query("SELECT * FROM tblpersonlig WHERE personid = $this->_personid ", SQL_INIT, SQL_ASSOC)) {
				if (strlen(trim($this->_fodtaar)) == 0) $this->_fodtaar = $this->_DB->record['fodtaar'];
				if (strlen(trim($this->_kon)) == 0) $this->_kon = $this->_DB->record['kon'];
			}

			if ($this->_DB->query("SELECT * FROM tbltelefon WHERE personid = $this->_personid ", SQL_INIT, SQL_ASSOC)) {
				if (strlen(trim($this->_tlfnr)) == 0) $this->_tlfnr = $this->_DB->record['tlfnr'];
			}

			if ($this->_DB->query("SELECT * FROM tblmailadresse WHERE personid = $this->_personid ", SQL_INIT, SQL_ASSOC)) {
				if (strlen(trim($this->_mailadr)) == 0) $this->_mailadr = $this->_DB->record['mailadr'];
			}
		}
	}

	public function getFormData(){
		if (isset($_POST['personid'])) $this->_personid=$_POST['personid'];
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
		if (isset($_POST['nummer'])) $this->_nummer=$_POST['nummer'];
		if (isset($_SERVER['REMOTE_ADDR'])) $this->_ip=$_SERVER['REMOTE_ADDR'];

		$this->getPerson();

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

	public function validate() {
		$this->_personid_error = null;
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
		$this->_nummer_error = null;

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

		//if (strlen(trim($this->_adresse)) == 0) {
		//	$this->_adresse_error = "Adresse skal udfyldes";
		//}

		if (strlen(trim($this->_postnr)) > 0) {
			if (!ereg("^[1-9]{1}[0-9]{3}$", $this->_postnr)) {
				$this->_postnr_error = "Postnummer skal være mellem 1000 og 9900";
			}
		}

		//if (strlen(trim($this->_bynavn)) == 0) {
		//	$this->_bynavn_error = "By skal udfyldes";
		//}

		if (strlen(trim($this->_tlfnr)) > 0) {
			if (!ereg("^[1-9]{1}[0-9]{7}$", $this->_tlfnr)) {
				$this->_tlfnr_error = "Telefon skal være mellem 10000000 og 99999999";
			}
		}

		if (strlen(trim($this->_mailadr)) > 0) {
			if (!ereg("^[a-zA-Z_0-9\\.\\-]{1,40}@([a-zA-Z0-9\\-]{1,40}\\.){1,5}[a-zA-Z]{2,3}$", $this->_mailadr)) {
				$this->_mailadr_error = "E-mail skal udfyldes korrekt";
			}
			elseif (MailVal($this->_mailadr, 3)) {
				$this->_mailadr_error = "Denne e-mail findes ikke";
			}
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

		if (strlen(trim($this->_nummer)) == 0) {
			$this->_nummer_error = "Løbsnr skal udfyldes";
		}
		elseif (!ereg("^[0-9]+$", $this->_nummer)) {
			$this->_nummer_error = "Løbsnr skal være mellem 1 og 999";
		}

		if ((isset($this->_personid))
		&& ($this->_personid > 0)){
			if ($this->_DB->query("SELECT COUNT(id) AS cnt FROM tblperson WHERE id = $this->_personid ", SQL_INIT, SQL_ASSOC)) {
				if ($this->_DB->record['cnt'] == 0) {
					$this->_personid_error = "P3060-Ref findes ikke";
				}
			}
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
		&& ($this->_nummer_error == null)
		&& ($this->_personid_error == null)) {
			return true;
		}
		else {
			return false;
		}
	}


	public function LobAdmTilmelding(){

		$this->_DB->autoCommit(false);
		if ($this->_DB->prepare("INSERT INTO tbllobtilmelding (personid, fornavn, efternavn, adresse, postnr, bynavn, tlfnr, mailadr, fodtaar, kon, distance, gruppe, lobsafgift, harbetaltdato, ip, afdeling, nr_udleveret) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)")) {
			$test =$this->_personid == null ? 0:$this->_personid ;
			$data = array(($this->_personid == null ?0:$this->_personid), $this->_fornavn, $this->_efternavn, $this->_adresse, $this->_postnr, $this->_bynavn, $this->_tlfnr, $this->_mailadr, $this->_fodtaar, $this->_kon, $this->_distid, $this->_grupid, $this->_lobsafgift, $this->_harbetaltdato, $this->_ip, $this->_afdeling, $this->_nr_udleveret);
			if ($this->_DB->execute($data)) {
				if ($this->_DB->query("select currval('tbllobtilmelding_id_seq') as insert_id", SQL_INIT, SQL_ASSOC)) {
					$this->_id = $this->_DB->record['insert_id'];
					if ($this->_DB->prepare("INSERT INTO tblnrtildeling (lobid, nummer, lobtilmeldingid) VALUES(?, ?, ?)")) {
						$data = array($this->_lobid, $this->_nummer, $this->_id);
						if ($this->_DB->execute($data)) {
							$this->_DB->commit();
							$this->_mail_sent = true;
						}
					}
				}
			}
		}
	}

} // class clsLobAdmTilmelding

/* = = = = = = = = = = = = = = = = = = = = */
?>