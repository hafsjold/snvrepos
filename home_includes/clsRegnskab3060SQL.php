<?php
require_once('DB.php'); // PEAR DB
require_once('clsSQL.php');

class clsRegnskab3060SQL  extends clsSQL{
	public function __construct(){
		// dbtype://user:pass@host/dbname
		$dsn = "pgsql://pgsql@hd21.hafsjold.dk/regnskab3060";
		$this->connect($dsn) || die('could not connect to database');
		return ;
	} // ctor
}

?>