<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
</head>
<body bgcolor="#FFFFFF" text="#000000">
<?php

  include_once("conn.inc");  
  $db = pg_connect($conn_www);

  if (!pg_send_query($db, "BEGIN; 
  						   DECLARE mycursor CURSOR FOR SELECT * FROM tblPerson limit 12; 
  						   FETCH ALL in mycursor;")) {
	echo "pg_send_query() error\n";
  }
  
  for ($j=0; $j<3; $j++){ 
    while(pg_connection_busy($db));  // busy wait: intended
  
    if (pg_connection_status($db) === PGSQL_CONNECTION_BAD) {
	  echo "pg_connection_status() error\n";
    }
  
    if (!($result = pg_get_result($db))) 
    {
	  echo "pg_get_result() error<br>";
    }
  }
  
  if (!($rows = pg_num_rows($result))) {
    echo "pg_num_rows() error<br>";
  }else
    echo "pg_num_rows() :" . $rows ."<br>";

  for ($i=0; $i < $rows; $i++) 
  {
	pg_fetch_array($result, $i, PGSQL_NUM);
  }

  for ($i=0; $i < $rows; $i++) 
  {
	pg_fetch_object($result, $i, PGSQL_ASSOC);
  }

  for ($i=0; $i < $rows; $i++) 
  {
	pg_fetch_row($result, $i);
  }
  
  for ($i=0; $i < $rows; $i++) 
  {
	pg_fetch_result($result, $i, 0);
  }

  pg_last_oid($result);
  pg_free_result($result);

echo "OK";

?>
</body>
</html>
