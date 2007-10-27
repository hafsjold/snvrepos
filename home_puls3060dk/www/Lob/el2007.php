<?php
  ini_set("max_execution_time",3000000);
  setlocale (LC_TIME, "da_DK.ISO8859-1");
  define('CRLF', "\r\n", TRUE);

  require_once('clsPuls3060SQL.php');
  require_once 'smarty_puls3060dk.php';
  require_once('dbFunctions.inc');


  class clsLobListe {
  	protected $_DBPuls3060;
  	protected $_lobid;
  	protected $_navn;
  	protected $_dato;
  	protected $_lobsafgift;
  	protected $_lobsafgiftunge;
  	protected $_count;
	

  	public function __construct(){
  		$this->_DBPuls3060 = new clsPuls3060SQL();
  		$this->initLobList();
  		return ;
  	} // ctor

  	public function __destruct() {
  	}

  	private function __get($property) {
  		switch ($property) {
  			case "lobid":
  			case "navn":
  			case "dato":
  			case "lobsafgift":
  			case "lobsafgiftunge":
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

  	public function initLobList(){
  		$this->_count = 0;	
  		$Query="
  	        select l.id as lobid, l.navn, l.dato, a.lobsafgift, a.lobsafgiftunge
  	        from public.tbllob l
  	        join public.tbllobafdeling a on l.id = a.lobid and a.afdid = 1
  	        where year(dato) = year(now())
  	        and dato >= now()
  	        and substring(navn, 1, 15) = 'Espergærdeløbet'
  	        order by dato
  	 	";
  		$QueryDataArr = array();
  		$this->_DBPuls3060->prepare($Query);
  		$this->_DBPuls3060->execute($QueryDataArr);
     }

  	public function nextLobList(){
  		if ($this->_DBPuls3060->next(SQL_ASSOC)) {
  			$this->_lobid = $this->_DBPuls3060->record['lobid'];
  			$this->_navn = $this->_DBPuls3060->record['navn'];
  			$this->_dato = $this->_DBPuls3060->record['dato'];
  			if ($this->_count++ == 0) {	
  			   $this->_lobsafgift = $this->_DBPuls3060->record['lobsafgift'];
  			   $this->_lobsafgiftunge = $this->_DBPuls3060->record['lobsafgiftunge'];
  			}
  			return true;
  		}
  		else {
  			return false;
  		}
  	}

  }
  $objLobListe = new clsLobListe();
  $objLobListe2 = new clsLobListe();
  $smarty = new Smarty_Puls3060dk();
  $smarty->caching = false;
  $smarty->assign("lobliste",$objLobListe);
  $smarty->assign("lobliste2",$objLobListe2);
  $smarty->display('Lob/el2007.tpl');
?>
