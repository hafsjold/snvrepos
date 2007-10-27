<?php
require_once('DB.php'); // PEAR DB
require_once('clsSQL.php');

class clsPuls3060SQL  extends clsSQL{
	public function __construct(){
		// dbtype://user:pass@host/dbname
		$dsn = "pgsql://pgsql@hd21.hafsjold.dk/puls3060";
		$this->connect($dsn) || die('could not connect to database');
		return ;
	} // ctor
}

?>