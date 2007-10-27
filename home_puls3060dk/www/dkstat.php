<?php
    setlocale (LC_TIME, "da_DK.ISO8859-1");
	require ("quickpay_puls3060.php");
	require_once('clsRegnskab3060SQL.php');
	
	function pbsdt2sqldt($dt){
		//$dt format 'ååmmddhhmmss'
		$sqldt =  mktime(substr($dt,6,2), substr($dt,8,2), substr($dt,10,2), substr($dt,2,2), substr($dt,4,2), substr($dt,0,2)); //mktime(hh,mm,ss,mm,dd,yy)
		return date("Y-m-d H:i:s", $sqldt);
	}
	
	function StatusTrans_Dankort($db, $p_transaction){
		if ((isset($p_transaction)) && ($p_transaction > 0)) {
			$eval = false;
			$qp = new quickpay_puls3060;
			// Set values
			$qp->set_msgtype('status');
			$qp->set_transaction($p_transaction);
			$eval = $qp->status();
			if ($eval) {
				if ($eval['qpstat'] === '000') {
					// The reversel was completed
					if ($db->prepare("SELECT quickpay.updatetransactionhistory(?, ?, ?, ?) AS ok")) {
						foreach($eval['history'] as $hist) {
							$tdata = array($eval['ordernum'], $hist['amount']/100, $hist['state'], pbsdt2sqldt($hist['time']));
							if ($db->execute($tdata)) {
								$db->commit();
							}
						}
					}
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

	$db1 = new clsRegnskab3060SQL();
	$db2 = new clsRegnskab3060SQL();

	$Query="SELECT transaction FROM quickpay.vtransacctionstatus WHERE state=? ORDER BY time";
	$QueryDataArr = array('3');
	if ($db2->prepare($Query)) {
		if ($db2->execute($QueryDataArr)) {
			while ($db2->next(SQL_ASSOC)) {
				StatusTrans_Dankort($db1, $db2->record['transaction']);
			}
		}
	}
?> 