<?php

/* = = = = = = = = = = = = = = = = = = = = */
class clsField {
	protected $_id;
	protected $_db_name;
	protected $_db_type;
	protected $_db_length;
	protected $_db_value;
	protected $_html_name;
	protected $_html_type;
	protected $_html_maxlength;
	protected $_html_id;
	protected $_html_size;
	protected $_html_label;
	protected $_html_value;
	protected $_html_error;
	protected $_html_error_display;
	protected $_html_display;
	
	public function __construct($field_id){
		$this->_id =  $field_id;
		$this->get_metadata();
		return ;
	} // ctor

	private function __get($property) {
		switch ($property) {
			case "id":
			case "db_name":
			case "db_type":
			case "db_length":
			case "db_value":
			case "html_name":
			case "html_type":
			case "html_maxlength":
			case "html_id":
			case "html_size":
			case "html_label":
			case "html_value":
			case "html_error":
			case "html_error_display":
			case "html_display":
				$_prop = '_' . $property;
				return $this->$_prop;
				break;

			default:
				throw new Exception("$property is not a valid property");
				break;
		}
	}
	
	public function fetchSmarty(){
		if ($this->_html_display == 1 ) {		
			$smarty = new Smarty_Puls3060dk;
			$smarty->caching = false;
			$smarty->assign("clsdta",$this);
			$ret = $smarty->fetch("Medlem/Field.tpl");			
			return $ret;
		}
		else return null;
	}
	
	private function get_metadata(){

		$this->_db_name = $this->_id;
		$this->_db_type = null;
		$this->_db_length = null;
		$this->_db_value = null;
		$this->_html_name = "html_$this->_db_name";
		$this->_html_type = 'text';
		$this->_html_maxlength = null;
		$this->_html_id = "html_id_$this->_db_name";
		$this->_html_size = null;
		$this->_html_label = null;
		$this->_html_value = null;
	    $this->_html_error = null;
	    $this->_html_error_display = null;
		$this->_html_display = 1;
	
		switch ($this->_id) {
			case 'id':
				$this->_db_type =  'integer';
				$this->_html_label =  'Id';
				$this->_html_maxlength = 5;
				$this->_html_size = 5;
				$this->_html_display = 0;
				break;
		
			case 'personid':
				$this->_db_type =  'integer';
				$this->_html_label =  'Pid';
				$this->_html_maxlength = 5;
				$this->_html_size = 5;
				$this->_html_display = 0;
				break;
		
			case 'fornavn':
				$this->_db_type =  'varchar';
				$this->_db_length =  25;
				$this->_html_label =  'Fornavn';
				$this->_html_maxlength = 25;
				$this->_html_size = 25;
				//$this->_html_value = 'Mogens';
				//$this->_html_error = "Fejl i navn";
				//$this->_html_error_display = 1;
				break;
		
			case 'efternavn':
				$this->_db_type =  'varchar';
				$this->_db_length =  25;
				$this->_html_label =  'Efternavn';
				$this->_html_maxlength = 25;
				$this->_html_size = 25;
				break;
		
			case 'adresse':
				$this->_db_type =  'varchar';
				$this->_db_length =  35;
				$this->_html_label =  'Adresse';
				$this->_html_maxlength = 35;
				$this->_html_size = 35;
				break;
		
			case 'postnr':
				$this->_db_type =  'varchar';
				$this->_db_length =  4;
				$this->_html_label =  'Postnummer';
				$this->_html_maxlength = 4;
				$this->_html_size = 4;
				break;
		
			case 'bynavn':
				$this->_db_type =  'varchar';
				$this->_db_length =  25;
				$this->_html_label =  'By';
				$this->_html_maxlength = 25;
				$this->_html_size = 25;
				break;
		
			case 'tlfnr':
				$this->_db_type =  'varchar';
				$this->_db_length =  15;
				$this->_html_label =  'Telefon';
				$this->_html_maxlength = 15;
				$this->_html_size = 15;
				break;
		
			case 'mailadr':
				$this->_db_type =  'varchar';
				$this->_db_length =  50;
				$this->_html_label =  'E-mail';
				$this->_html_maxlength = 50;
				$this->_html_size = 25;
				break;
		
			case 'fodtdato':
				$this->_db_type =  'date';
				$this->_html_label =  'Fødselsdato';
				$this->_html_maxlength = 15;
				$this->_html_size = 15;
				break;
		
			case 'kon':
				$this->_db_type =  'char';
				$this->_db_length =  1;
				$this->_html_label =  'Køn';
				$this->_html_maxlength = 1;
				$this->_html_size = 1;
				break;
		
			case 'ip':
				$this->_db_type =  'varchar';
				$this->_db_length =  15;
				$this->_html_label =  'IP';
				$this->_html_maxlength = 15;
				$this->_html_size = 15;
				$this->_html_display = 0;
				break;
		
			case 'indmeldtdato':
				$this->_db_type =  'date';
				$this->_html_label =  'Indmeldt dato';
				$this->_html_maxlength = 15;
				$this->_html_size = 15;			
				$this->_html_display = 0;
				break;
		
			case 'kontingenttildato':
				$this->_db_type =  'date';
				$this->_html_label =  'Betalt til';
				$this->_html_maxlength = 15;
				$this->_html_size = 15;			
				$this->_html_display = 0;
				break;
		
			case 'kontingentkr': 
				$this->_db_type =  'numeric';
				$this->_db_length =  6;
				$this->_html_label =  'Kontingent';
				$this->_html_maxlength = 6;
				$this->_html_size = 4;			
				$this->_html_display = 0;
				break;
		
			case 'action': 
				$this->_db_type =  'char';
				$this->_db_length =  1;
				$this->_html_label =  'Action';
				$this->_html_maxlength = 1;
				$this->_html_size = 1;			
				$this->_html_display = 0;
				break;
		
			default:
				throw new Exception("$this->_id is not valid field");
				break;
		}
		
		return ;
	}
	
} // class clsField
/* = = = = = = = = = = = = = = = = = = = = */
?>
