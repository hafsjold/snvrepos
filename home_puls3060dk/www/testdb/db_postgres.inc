<?
/*
 *  Misc PostgreSQL utils to use with PHPEd generated scripts
 *
 */

require_once "db_utils.inc";

class postgres_dbconn extends dbconn {
	var $fconn;
	function db_extension_installed() {
		if (function_exists('pg_connect')) {
				return(true);
		} else {
				dbu_handle_error("Please make sure PostgreSQL extension is loaded in php.ini");
		}
	}
	function db_connect($dbhost, $dbuser, $dbpass, $dbname) {
		if ($this->fconn) $this->close();

		$connectionString = "host='".addcslashes($dbhost, "'\\").
			"' port=$dbport dbname='".addcslashes($dbname, "'\\")."'";

		if ($dbuser != '') $connectionString .= " user='$dbuser'";
		if ($dbpass != '') $connectionString .= " password='$dbpass'";

		$this->fconn = @pg_connect($connectionString) or dbu_handle_error($this->db_lasterror());
		$this->db_query('SET CLIENT_ENCODING TO \'UNICODE\'');
		@pg_setclientencoding($this->fconn, 'UNICODE');
	}
	function db_close() {
		if ($this->fconn) pg_close($this->fconn);
		$this->fconn = NULL;
	}
	function db_lasterror() {
		return ($this->fconn) ? pg_last_error($this->fconn) : pg_last_error();
	}
	function db_query($sql) {
		return pg_query($this->fconn, $sql);
	}
	function db_query_limit($sql, $limit) {
		return $sql;
	}
	function db_query_seek($qry, $offset) {
		if ($offset > 0)
			@pg_fetch_assoc($qry, $offset-1);
	}
	function db_next($qry) {
		return @pg_fetch_assoc($qry);
	}
	function db_quote_value(&$value) {
		if (!is_numeric($value)) {
			return "'" . pg_escape_string($value) . "'";
		} else {
			return $value;
		}
	}
	function db_quote_field(&$fld) {
		return '"' . $fld . '"';
	}
}
