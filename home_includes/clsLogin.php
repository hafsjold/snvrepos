<?php
require('clsPuls3060SQL.php');

/* = = = = = = = = = = = = = = = = = = = = */
class clsLogin {

	protected $_login;
	protected $_brugerid;
	protected $_personid;
	protected $_fornavn;
	protected $_efternavn;

	public function __construct(){
		$this->_login = false;
		return ;
	} // ctor

	private function __get($property) {
		switch ($property) {
			case "login":
			case "brugerid":
			case "personid":
			case "fornavn":
			case "efternavn":
				$_prop = '_' . $property;
				return $this->$_prop;
				break;

			default:
				throw new Exception("$property is not a valid property");
				break;
		}
		return null;
	}

	private function __set($property, $value) {
		switch ($property) {
			case "login":
			case "brugerid":
			case "personid":
			case "fornavn":
			case "efternavn":
				$_prop = '_' . $property;
				$this->$_prop = $value;
				break;

			default:
				throw new Exception("$property is not a valid property");
				break;
		}
	}

	public function Login(){
	    $this->_login = false;
		if (isset($_SERVER['PHP_AUTH_USER'])) {
		    $DBPuls3060 = new clsPuls3060SQL();
			if ($DBPuls3060->query("SELECT * FROM tblpersonlig WHERE passwd is not null and brugerid = '" . $_SERVER['PHP_AUTH_USER'] . "'", SQL_INIT, SQL_ASSOC)) {
				$this->_brugerid = $DBPuls3060->record['brugerid'];
				$this->_personid = $DBPuls3060->record['personid'];
				if ($DBPuls3060->query("SELECT * FROM tblperson WHERE id = $this->_personid ", SQL_INIT, SQL_ASSOC)) {
				    $this->_fornavn = $DBPuls3060->record['fornavn'];
				    $this->_efternavn = $DBPuls3060->record['efternavn'];
				    $this->_login = true;
			    }
			}
		}
		return $this->_login;
	}

} // class clsLogin

/* = = = = = = = = = = = = = = = = = = = = */
?>