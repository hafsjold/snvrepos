<body onLoad="return top.rwlink('/velmnu/Velkom/Velkommen.php');">
<?php
setlocale (LC_TIME, "da_DK.ISO8859-1");
require('clsLogin.php');

session_start();

if (session_is_registered ('login')) {
	session_unregister ('login');
}

?>
</body>