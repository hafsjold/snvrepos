<!-- Clear msie buffer <?=str_repeat(":",500);?> -->
<?
//adm.puls3060.dk/admin_test/dankort_test.php

ini_set("max_execution_time",3000000);
setlocale (LC_TIME, "da_DK.ISO8859-1");
define('CRLF', "\r\n", TRUE);

require_once('clsRegnskab3060SQL.php');
require_once 'smarty_puls3060adm.php';
require_once ("quickpay_puls3060.php");


class clsTrans {
	protected $_DBRegnskab3060;
	protected $_DBRegnskab3060_2;
	protected $_ordernum;
	protected $_transaction;
	protected $_currency;
	protected $_cardtype;
	protected $_id;
	protected $_amount;
	protected $_state;
	protected $_statetext;
	protected $_time;

	public function __construct(){
		$this->_DBRegnskab3060 = new clsRegnskab3060SQL();
		$this->_DBRegnskab3060_2 = new clsRegnskab3060SQL();
		$this->initTransList();
		return ;
	} // ctor

	public function __destruct() {
	}

	private function __get($property) {
		switch ($property) {
			case "ordernum":
			case "transaction":
			case "currency":
			case "cardtype":
			case "id":
			case "amount":
			case "state":
			case "statetext":
			case "time":
				$_prop = '_' . $property;
				return $this->$_prop;
				break;

			case "rows":
				return $this->_DBRegnskab3060->result->numRows();
				break;


			default:
				throw new Exception("$property is not a valid property");
				break;
		}
		return null;
	}

	public function initTransList(){
		$Query="
	  		SELECT
	    	  ordernum,
			  transaction,
			  currency,
			  cardtype,
			  id,
			  amount,  
	  		  state,
	  		  statetext,
	  		  time
	  		FROM quickpay.vtransacctionstatus
	  		ORDER BY time
			";
		$QueryDataArr = array();
		$this->_DBRegnskab3060->prepare($Query);
		$this->_DBRegnskab3060->execute($QueryDataArr);
	}

	private function pbsdt2sqldt($dt){
		//$dt format 'ååmmddhhmmss'
		$sqldt =  mktime(substr($dt,6,2), substr($dt,8,2), substr($dt,10,2), substr($dt,2,2), substr($dt,4,2), substr($dt,0,2)); //mktime(hh,mm,ss,mm,dd,yy)
		return date("Y-m-d H:i:s", $sqldt);
	}
	public function nextTransList(){
		if ($this->_DBRegnskab3060->next(SQL_ASSOC)) {
			$this->_ordernum = $this->_DBRegnskab3060->record['ordernum'];
			$this->_transaction = $this->_DBRegnskab3060->record['transaction'];
			$this->_currency = $this->_DBRegnskab3060->record['currency'];
			$this->_cardtype = $this->_DBRegnskab3060->record['cardtype'];
			$this->_id = $this->_DBRegnskab3060->record['id'];
			$this->_amount = $this->_DBRegnskab3060->record['amount'];
			$this->_state = $this->_DBRegnskab3060->record['state'];
			$this->_statetext = $this->_DBRegnskab3060->record['statetext'];
			$this->_time = $this->_DBRegnskab3060->record['time'];
			return true;
		}
		else {
			return false;
		}
	}

	public function SyncTrans($id){
		if ((isset($id)) && ($id > 0)) {
			$eval = false;
			$qp = new quickpay_puls3060;
			// Set values
			$qp->set_msgtype('status');
			$qp->set_transaction($id);
			//$qp->set_ordernum($this->_ordernum);
			$eval = $qp->status();
			if ($eval) {
				if ($eval['qpstat'] === '000') {
					// The status was completed
					$this->_DBRegnskab3060_2->autoCommit(true);
					if ($this->_DBRegnskab3060_2->prepare("SELECT quickpay.updatetransactionhistory(?, ?, ?, ?)")) {
						$history = $eval['history'];
						foreach ($history as $evalhistory) {
							$data = array($eval['ordernum'], $evalhistory['amount']/100, $evalhistory['state'], $this->pbsdt2sqldt($evalhistory['time']));
							$this->_DBRegnskab3060_2->execute($data);
						}
					}
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
		else{
			return false;
		}
	}

	public function ReverseTrans($id){
		if ((isset($id)) && ($id > 0)) {
			$eval = false;
			$qp = new quickpay_puls3060;
			// Set values
			$qp->set_msgtype('1420');
			$qp->set_transaction($id);
			$eval = $qp->reverse();
			if ($eval) {
				if ($eval['qpstat'] === '000') {
					// The status was completed
					$this->SyncTrans( $id );
					return true;
				} else {
					// An error occured with the status
					$this->SyncTrans( $id );
					return false;
				}
			} else {
				// Communication error
				return false;
			}
		}
		else{
			return false;
		}
	}

	public function CaptureTrans($id){
		if ((isset($id)) && ($id > 0)) {
			$query = "SELECT * FROM quickpay.vtransactionauthorized WHERE transaction = $id";
			if ($this->_DBRegnskab3060_2->query($query, SQL_INIT, SQL_ASSOC)) {
				$amount = 100 * $this->_DBRegnskab3060_2->record['amount'];
			}
			$eval = false;
			$qp = new quickpay_puls3060;
			// Set values
			$qp->set_msgtype('1220');
			$qp->set_transaction($id);
			$qp->set_amount($amount);
			$eval = $qp->capture();
			if ($eval) {
				if ($eval['qpstat'] === '000') {
					// The status was completed
					$this->SyncTrans( $id );
					return true;
				} else {
					// An error occured with the status
					$this->SyncTrans( $id );
					return false;
				}
			} else {
				// Communication error
				return false;
			}
		}
		else{
			return false;
		}
	}

	public function CreditTrans($id){
		if ((isset($id)) && ($id > 0)) {
			$query = "SELECT * FROM quickpay.vtransactionauthorized WHERE transaction = $id";
			if ($this->_DBRegnskab3060_2->query($query, SQL_INIT, SQL_ASSOC)) {
				$amount = 100 * $this->_DBRegnskab3060_2->record['amount'];
			}
			$eval = false;
			$qp = new quickpay_puls3060;
			// Set values
			$qp->set_msgtype('credit');
			$qp->set_transaction($id);
			$qp->set_amount($amount);
			$eval = $qp->credit();
			if ($eval) {
				if ($eval['qpstat'] === '000') {
					// The status was completed
					$this->SyncTrans( $id );
					return true;
				} else {
					// An error occured with the status
					$this->SyncTrans( $id );
					return false;
				}
			} else {
				// Communication error
				return false;
			}
		}
		else{
			return false;
		}
	}
}



//******************************************
//MAIN FUNCTION
//******************************************
if (isset($_REQUEST['action'])) $action=$_REQUEST['action'];
else $action = 'list';

switch ($action) {
	case "list":   //List transaction
	break;

	case "syncall":   //Syncronize all transactions
	$objTrans = new clsTrans();
	while ($objTrans->nextTransList()){
		$objTrans->SyncTrans($objTrans->transaction);
	}
	break;

	case "capture":   //Capture transaction
	if (isset($_REQUEST['id'])) $id=$_REQUEST['id'];
	$objTrans = new clsTrans();
	$objTrans->CaptureTrans($id);
	break;

	case "reverse":   //Reverse transaction
	if (isset($_REQUEST['id'])) $id=$_REQUEST['id'];
	$objTrans = new clsTrans();
	$objTrans->ReverseTrans($id);
	break;

	//case "credit":   //Credit transaction
	case "capedit":   //Test Credit transaction
	if (isset($_REQUEST['id'])) $id=$_REQUEST['id'];
	$objTrans = new clsTrans();
	$objTrans->CreditTrans($id);
	break;	


	default:
		throw new Exception("Action: $action is not valid action");
		break;
}

$objTrans = new clsTrans();
$smarty = new Smarty_Puls3060adm();
$smarty->caching = false;
$smarty->assign("trans",$objTrans);
$smarty->display('admin_test/quickpay.tpl');


?>
