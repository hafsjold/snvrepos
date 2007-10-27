<?php
  echo "Test";
?>


<html>
<head>
<title>Untitled Document</title>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
</head>

<body bgcolor="#FFFFFF" text="#000000">
<form name="form1" method="post" action="testform.php">
  <p>Test-1: 
    <input type="text" name="Felt[0]['felt1']" value="1111">
    <br>
    Test-2: 
    <input type="text" name="Felt[0]['felt2']" value="2222">
    <br>
    Test-3: 
    <input type="text" name="Felt[0]['felt3']" value="3333">
    <br>
    Test-4: 
    <input type="text" name="Felt[0]['felt4']" value="4444">
    <br>
    Test-5: 
    <input type="text" name="Felt[0]['felt5']" value="5555">
  </p>
  <p>Test-1: 
    <input type="text" name="Felt[1]['felt1']" value="1111">
    <br>
    Test-2: 
    <input type="text" name="Felt[1]['felt2']" value="2222">
    <br>
    Test-3: 
    <input type="text" name="Felt[1]['felt3']" value="3333">
    <br>
    Test-4: 
    <input type="text" name="Felt[1]['felt4']" value="4444">
    <br>
    Test-5: 
    <input type="text" name="Felt[1]['felt5']" value="5555">
  </p>
  <p>
    <input type="submit" name="Submit" value="Submit">
  </p>
</form>
</body>
</html>
