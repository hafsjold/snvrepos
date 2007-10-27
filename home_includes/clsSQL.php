<?php

// define the query types
define('SQL_NONE', 1);
define('SQL_ALL', 2);
define('SQL_INIT', 3);

// define the query formats
define('SQL_ASSOC', 1);
define('SQL_INDEX', 2);

class clsSQL {

	public $db = null;
	public $result = null;
	public $error = null;
	public $record = null;
	public $statement  = null;


	function __construct() { }
	/**
     * connect to the database
     *
     * @param string $dsn the data source name
     */
	function connect($dsn) {
		$this->db = DB::connect($dsn);

		if(DB::isError($this->db)) {
			$this->error = $this->db->getMessage();
			return false;
		}
		return true;
	}

	function autoCommit($onoff =false) {
		$this->db->autoCommit($onoff);
	}

	function commit() {
		$this->db->commit();
	}

	function rollback () {
		$this->db->autoCommit();
	}


	/**
     * disconnect from the database
     */
	function disconnect() {
		$this->db->disconnect();
	}

	/**
     * query the database
     *
     * @param string $query the SQL query
     * @param string $type the type of query
     * @param string $format the query format
     */
	function query($query, $type = SQL_NONE, $format = SQL_INDEX) {

		$this->record = array();
		$_data = array();

		// determine fetch mode (index or associative)
		$_fetchmode = ($format == SQL_ASSOC) ? DB_FETCHMODE_ASSOC : null;

		$this->result = $this->db->query($query);
		if (DB::isError($this->result)) {
			$this->error = $this->result->getMessage();
			return false;
		}
		switch ($type) {
			case SQL_ALL:
				// get all the records
				while($_row = $this->result->fetchRow($_fetchmode)) {
					$_data[] = $_row;
				}
				$this->result->free();
				$this->record = $_data;
				break;
			case SQL_INIT:
				// get the first record
				$this->record = $this->result->fetchRow($_fetchmode);
				break;
			case SQL_NONE:
			default:
				// records will be looped over with next()
				break;
		}
		return true;
	}

	/**
     * connect to the database
     *
     * @param string $format the query format
     */
	function next($format = SQL_INDEX) {
		// fetch mode (index or associative)
		$_fetchmode = ($format == SQL_ASSOC) ? DB_FETCHMODE_ASSOC : null;
		if ($this->record = $this->result->fetchRow($_fetchmode)) {
			return true;
		} else {
			$this->result->free();
			return false;
		}
	}

	function prepare($query){
		$this->statement = $this->db->prepare($query);
		if (DB::isError($this->statement)) {
			$this->error = $this->statement->getMessage();
			return false;
		}
		return true;
	}

	function execute($data){
		$this->result = $this->db->execute($this->statement, $data);
		if (DB::isError($this->result)) {
			$this->error = $this->result->getMessage();
			return false;
		}
		return true;
	}
	
	function affectedRows () {
		return $this->db->affectedRows();
	}
}
?>