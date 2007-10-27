<? header ("Last-Modified: " . gmdate("D, d M Y H:i:s") . " GMT"); // always modified ?>
<?php
   setlocale (LC_TIME, "da_DK.ISO8859-1");
   require('clsLogin.php');
   session_start();
?>
<head>
<title>Untitled Document</title>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
</head>

<body bgcolor="#99CC00" text="#000000" leftmargin="0" topmargin="6" marginwidth="0" marginheight="0">
<p align=right ><font face="Arial, Helvetica, sans-serif" size="2" color="#003399">
<?php
   if (isset($_SESSION['login'])){
	  $login = $_SESSION['login'];
	  echo "$login->fornavn $login->efternavn";
   }
   else echo "Anonym bruger";
?>
</font></p>

</body>