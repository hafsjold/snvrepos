<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0//EN">
<? 
   if (isset($_SERVER['SSL_CLIENT_S_DN_CN'])) $Client = $_SERVER['SSL_CLIENT_S_DN_CN'];
   else $Client = NULL;
?>
<head>
  <title>Puls 3060 Admin</title>
  
  <LINK REL="stylesheet" HREF="css/menu.css" TYPE="text/css">

  
</head>


<body bgcolor=ffffff>

<pre>

</pre>
<center>
<a><img src=images/puls3060logo.gif border=0></a>
<h1 class=login>Version 0.1.0
  
  </h1>

<p>

Licensed to

  <p> <b> Puls 3060<br>
    co/Mogens Hafsjold<br>
    Nørremarken 31<br>
	Espergærde
</b>

<p>
<table border=0>

<tr>

    <th align=right>User</th>
      <td><?=$Client ?></td>
  
  </tr><tr>
  
    <th align=right>Dataset</th>
    <td>puls3060</td>
  
  </tr><tr>
  
    <th align=right>Database Host</th>
      <td>192.168.3.99</td>
  
  </tr></table>

</center>

</body>
</html>
