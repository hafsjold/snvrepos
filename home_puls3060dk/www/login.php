<? header ("Last-Modified: " . gmdate("D, d M Y H:i:s") . " GMT"); // always modified ?>
<?
  function auth($UserId, $Password) {
    include_once("conn.inc");  
    $dbLink = pg_connect($conn_www);
    mysql_select_db("dbcyrus", $dbLink);
	$Query="SELECT count(*) As cnt FROM tblpersonlig
	  WHERE brugerid='$UserId' AND Password='$Password'";
	if ($dbResult = pg_query($dbLink, $Query))
	  if ($row = pg_fetch_array($dbResult, NULL , PGSQL_ASSOC))
	    if ($row["cnt"] == 1)
		  return true;
	return false;
  }

  if (isset($_SERVER['SERVER_PORT']) && ($_SERVER['SERVER_PORT']==80))
    if((!isset($_SERVER['PHP_AUTH_USER'])) || (isset($_SERVER['PHP_AUTH_USER']) && !auth($_SERVER['PHP_AUTH_USER'], $_SERVER['PHP_AUTH_PW']))) {
      Header("WWW-Authenticate: Basic realm=\"Puls3060 Administrator\"");
      Header("HTTP/1.0 401 Unauthorized");
      echo "Du skal indtaste et gyldigt Brugernavn og Password for at få adgang.\n";
      exit;
    }
?>
<body onLoad="WindowClose()">
</body>
<script language="javascript">
<!--
function WindowClose(){
    var ParentWindow;
	ParentWindow = top.opener;
	ParentWindow.top.location = "http://www.puls3060.dk";
	window.close();
}
// -->
</script>
