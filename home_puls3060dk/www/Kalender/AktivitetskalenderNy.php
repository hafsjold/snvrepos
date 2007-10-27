<?php 
//www.puls3060.dk/kalmnu/Kalender/AktivitetskalenderNy.php
setlocale (LC_TIME, "da_DK.ISO8859-1");

require_once('clsPuls3060SQL.php');
require_once('smarty_puls3060dk.php');

class clsAktivitet {
	protected $_status=0;
	protected $_DBPuls3060;
	protected $_klinieid;
	protected $_kliniedato;
	protected $_klinietext;
	protected $_klines;
	protected $_persongruppeid;
	protected $_link;
	protected $_info;
	protected $_newwindow;
	protected $_etilmelding;
	protected $_hidden;
	protected $_kliniested;
	protected $_tilmeldingslut;
	protected $_personid_list;
	protected $_name_list;
	
	public function __construct(){
		return ;
	} // ctor

	public function __destruct() {
		return ;
	}
	
	private function init(){
		$this->_DBPuls3060 = new clsPuls3060SQL();
		$this->initAktivitet();
		$this->_status=1;		
	}
	
	private function __get($property) {
		switch ($property) {
			case "klinieid":
			case "kliniedato":
			case "klinietext":
			case "klines":
			case "persongruppeid":
			case "link":
			case "info":
			case "newwindow":
			case "etilmelding":
			case "hidden":
			case "kliniested":
			case "tilmeldingslut":
			case "personid_list":
			case "name_list":
				$_prop = '_' . $property;
				return $this->$_prop;
				break;

			case "ebuttom":
				return "<a href=\"/kalmnu/Kalender/AktTilmelding.php?AktId=" . $this->_klinieid . "\" onClick=\"return top.rwlink(href);\" onMouseOut=\"MM_swapImgRestore();\" onMouseOver=\"MM_displayStatusMsg('eTilmelding');  MM_swapImage('vpil_A_" . $this->_klinieid . "','','/image/vpil_f2.gif',1);return document.MM_returnValue;\" ><img name=\"vpil_A_" . $this->_klinieid . "\" src=\"/image/vpil.gif\" width=\"15\" height=\"15\" border=\"0\"></a>";
				break;

			case "tbuttom":
				return "<a href=\"/kalmnu/Kalender/AktTilmeldinger.php?AktId=" . $this->_klinieid . "\" onClick=\"return top.rwlink(href);\" onMouseOut=\"MM_swapImgRestore();\" onMouseOver=\"MM_displayStatusMsg('eTilmeldinger');MM_swapImage('vpil_B_" . $this->_klinieid . "','','/image/vpil_f2.gif',1);return document.MM_returnValue;\" ><img name=\"vpil_B_" . $this->_klinieid . "\" src=\"/image/vpil.gif\" width=\"15\" height=\"15\" border=\"0\"></a>";
				break;

			case "linknavn":
				$return = null;
				$personidarr = array();
				$namearr = array();
				$personidarr = explode(':', $this->_personid_list);
				$namearr = explode(':', $this->_name_list);
				for($i=0;$i<count($personidarr);$i++){
					$link = '/kalmnu/Kalender/Aktivitetskalender.php?PersonId=' . $personidarr[$i];
					if ($i>0)
						$return .= '<br>';
					$return .= '<a href=' . $link . ' onClick="return top.rwlink(href);">' . $namearr[$i] . '</a>';
				}
				return $return;
				break;

			default:
				throw new Exception("$property is not a valid property");
				break;
		}
		return null;
	}

	private function initAktivitet(){
		$Query="SELECT * FROM vaktivitet()";
		$QueryDataArr = array();
		$this->_DBPuls3060->prepare($Query);
		$this->_DBPuls3060->execute($QueryDataArr);
	}

	public function nextAktivitet(){
		if (!$this->_status) $this->init();
		if ($this->_DBPuls3060->next(SQL_ASSOC)) {
			$this->_klinieid = $this->_DBPuls3060->record['klinieid'];
			$this->_kliniedato = $this->_DBPuls3060->record['kliniedato'];
			$this->_klinietext = $this->_DBPuls3060->record['klinietext'];
			$this->_klines = $this->_DBPuls3060->record['klines'];
			$this->_persongruppeid = $this->_DBPuls3060->record['persongruppeid'];
			$this->_link = $this->_DBPuls3060->record['link'];
			$this->_info = $this->_DBPuls3060->record['info'];
			$this->_newwindow = $this->_DBPuls3060->record['newwindow'];
			$this->_etilmelding = $this->_DBPuls3060->record['etilmelding'];
			$this->_hidden = $this->_DBPuls3060->record['hidden'];
			$this->_kliniested = $this->_DBPuls3060->record['kliniested'];
			$this->_tilmeldingslut = $this->_DBPuls3060->record['tilmeldingslut'];
			$this->_personid_list = $this->_DBPuls3060->record['personid_list'];
			$this->_name_list = $this->_DBPuls3060->record['name_list'];
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
$objAktivitet = new clsAktivitet();
$smarty = new Smarty_Puls3060dk();
$smarty->caching = false;
$smarty->assign("akt",$objAktivitet);
$smarty->display('Kalender/Aktivitetskalender.tpl');
?>
 
