<?php
setlocale (LC_TIME, "da_DK.ISO8859-1");
require ("quickpay_puls3060.php");
require('clsRegnskab3060SQL.php');

function pbsdt2sqldt($dt){
	//ååmmddhhmmss
	$sqldt =  mktime(substr($dt,6,2), substr($dt,8,2), substr($dt,10,2), substr($dt,2,2), substr($dt,4,2), substr($dt,0,2)); //mktime(hh,mm,ss,mm,dd,yy)
	return date("Y-m-d H:i:s", $sqldt);
}
$qp = new quickpay_puls3060();
$db = new clsRegnskab3060SQL();
$ResultpageMail = false;

// Set keys we wish to read from $_POST array
$keys = array('amount','time','ordernum','pbsstat','qpstat','qpstatmsg','merchantemail','merchant','currency','cardtype','transaction', 'md5checkV2');
$field = array();
$Md5source = '';
// Loop through $keys array, check if key exists in $_POST array, if so collect the value
while (list(,$k) = each($keys)) {
	if (isset($_POST[$k])) {
		$message .= "$k: " .$_POST[$k] . "\r\n";
		$field[$k] = $_POST[$k];
		if ($k != 'md5checkV2'){
			$Md5source .= $_POST[$k];
		}
	}
}
$md5kode = md5($Md5source.$qp->get_md5checkword());
$message .= "md5konrtol: " . $md5kode . "\r\n";

if ($md5kode == $field['md5checkV2']) {
	if ($field['qpstat'] == '000') {
		$db->autoCommit(false);
		if ($db->prepare("INSERT INTO quickpay.tbltransaction (ordernum, transaction, currency, cardtype) VALUES (?, ?, ?, ?)")) {
			$hdata = array($field['ordernum'], $field['transaction'], $field['currency'], $field['cardtype']);
			if ($db->execute($hdata)) {
				$db->commit();
				if ($db->prepare("INSERT INTO quickpay.tbltransactionhistory (ordernum, amount, state, time) VALUES (?, ?, ?, ?)")) {
					$tdata = array($field['ordernum'], $field['amount']/100, '1', pbsdt2sqldt($field['time']));
					if ($db->execute($tdata)) {
						$db->commit();
					}
				}
			}
		}
	}
	else {
		$ResultpageMail = true;
	}
}

if ($ResultpageMail) {
	ob_start();
	var_dump($_POST);
	$dump = ob_get_contents();
	ob_end_clean();

	$message .= "var_dump: \r\n" . $dump;
	// Send an email with the data posted to your resultapage
	mail('mha@hafsjold.dk', 'resultpage', $message);
}
?> 