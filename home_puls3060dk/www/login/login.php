<body onLoad="return top.rwlink('/velmnu/Velkom/Velkommen.php');">
<?php
setlocale (LC_TIME, "da_DK.ISO8859-1");
require('clsLogin.php');

session_start();

if (session_is_registered ('login')) {
	session_unregister ('login');
}

$login = new clsLogin();
$bool = $login->Login();
//echo "$login->fornavn $login->efternavn";
$_SESSION['login'] = $login;
?>
</body>
