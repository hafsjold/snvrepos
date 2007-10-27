<?php
require('clsPuls3060SQL.php');
require("mailFunctions.inc");

/* = = = = = = = = = = = = = = = = = = = = */
class clsPbsUserTilmelding {

	protected $_DB;
	protected $_personid;
	protected $_fornavn;
	protected $_efternavn;
	protected $_adresse;
	protected $_postnr;
	protected $_bynavn;
	protected $_linked = false;


	public function __construct(){
		return ;
	} // ctor

	public function setDB($inDB){
		$this->_DB = $inDB;
		return ;
	} // ctor

	private function __get($property) {
		switch ($property) {
			case "linked":
				return $this->_linked;
				break;

			case "dbnr":
				return 10000 + $this->_personid;
				break;

			case "navn":
				return $this->_fornavn . ' ' . $this->_efternavn;
				break;
 
			case "adr":
				return $this->_adresse;
				break;

			case "postby":
				return $this->_postnr . ' ' . $this->_bynavn;
				break;
 
			default:
				throw new Exception("$property is not a valid property");
				break;
		}
		return null;
	}

	public function getLink($linkid){
		if (isset($linkid)) {
			if ($this->_DB->query("SELECT * FROM tbllink WHERE linkid = '" . $linkid . "'", SQL_INIT, SQL_ASSOC)) {
				$this->_personid = $this->_DB->record['p3060_ref'];
	            $this->_linked = true;

			}

			if ($this->_DB->query("SELECT * FROM tblperson WHERE id = $this->_personid ", SQL_INIT, SQL_ASSOC)) {
				$this->_fornavn = $this->_DB->record['fornavn'];
				$this->_efternavn = $this->_DB->record['efternavn'];
				$this->_adresse = $this->_DB->record['adresse'];
				$this->_postnr = $this->_DB->record['postnr'];
				$this->_bynavn = $this->_DB->record['bynavn'];
			}
		}
	}

} // class clsPbsUserTilmelding

/* = = = = = = = = = = = = = = = = = = = = */
?>