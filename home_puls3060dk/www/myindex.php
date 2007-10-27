<? header ("Last-Modified: " . gmdate("D, d M Y H:i:s") . " GMT"); // always modified ?>
<?
  include_once("conn.inc");  
  $dbLink = pg_connect($conn_www);

  //$dbLink = false;

  if ($dbLink) {

    $leftFrame = "/Velkom/Velkom.php";
    $mainFrame = "Velkom/Velkommen.php";
    $info = 1;

    if (isSet($_REQUEST['p0'])){
      $Query="SELECT Left_url, Right_url, Info FROM tblLink WHERE LinkId='" . $_REQUEST['p0'] . "'";
      if ($dbResult = pg_query($dbLink, $Query))
        if($row = pg_fetch_array($dbResult, NULL , PGSQL_ASSOC)){
          $leftFrame = $row["left_url"];
          if (strpos($row["right_url"], '?'))
            $mainFrame = $row["right_url"] . "&p0=" . $_REQUEST['p0'];
		  else
            $mainFrame = $row["right_url"] . "?p0=" . $_REQUEST['p0'];
          $info      = $row["info"];
        }
    }
    else{	
      if (isSet($_REQUEST['p1'])) $leftFrame = $_REQUEST['p1'];
      if (isSet($_REQUEST['p2'])){
         $mainFrame = $_REQUEST['p2'];
		 if (ereg ("^.*\&px=1\&(.*)\$", $_SERVER['QUERY_STRING'], $parms)){
   			if ($parms[1])	$mainFrame .= "?" .   $parms[1];
		}
	  }
	  //  print( $mainFrame . "<-->" . $_SERVER['QUERY_STRING']);
      if (isSet($_REQUEST['p3'])){
        if ($_REQUEST['p3']=="no") $info = 0;
      }
    }
  }
?>
<html>

<head>

<title>Puls 3060</title>

<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">

<script language="JavaScript">
var ShowInfoWindow = <?= (($info) ? "true" : "false");?>; 
function leftmain(leftFrame, mainFrame){
  if (leftFrame)
    top.frames[1].location.href = leftFrame;
  else
    top.frames[1].location.href = "/Frame/TomMenu.htm";
  top.frames[2].location.href = mainFrame;
}
function rwlink(link){
  var exp = new RegExp("^(/|http://www.puls3060.dk/|https://www.puls3060.dk/)(nyhmnu|lobmnu|kalmnu|medmnu|tramnu|velmnu|lnkmnu|formnu)(/.*)$");
  var m = exp.exec(link);
  var map = new Array();
  map["nyhmnu"] = "/Nyheder/nyhedermenu.htm";
  map["lobmnu"] = "/Lob/lobmenu.htm";
  map["kalmnu"] = "/Kalender/kalendermenu.htm";
  map["medmnu"] = "/Medlem/Medlem.htm";
  map["tramnu"] = "/Traening/traeningmenu.htm";
  map["velmnu"] = "/Velkom/Velkom.php";
  map["lnkmnu"] = "/Frame/TomMenu.htm";
  map["formnu"] = "/Frame/TomMenu.htm";

  if (m != null) {
    top.leftmain(map[m[2]], m[3]);  
    return false;
  }
  return true;
}
</script>

</head>
<? if ($dbLink) { ?>
  <frameset cols="770,*" frameborder="NO" border="0" framespacing="0"> 

    <frameset rows="700,*" frameborder="NO" border="0" framespacing="0"> 

      <frameset rows="102,*" cols="*" frameborder="NO" border="0" framespacing="0"> 

        <frame name="topFrame" scrolling="NO" noresize src="Frame/header.htm" frameborder="NO" marginwidth="0" marginheight="0" >

        <frameset cols="117,*" frameborder="NO" border="0" framespacing="0"> 

          <frame name="leftFrame" noresize scrolling="NO" src="<?=$leftFrame;?>" frameborder="NO" marginwidth="0" marginheight="0">

            <frameset cols="*,29" frameborder="NO" border="0" framespacing="0"> 

            <frameset rows="*,29" frameborder="NO" border="0" framespacing="0"> 

              <frame name="mainFrame" src="<?=$mainFrame;?>" marginwidth="0" marginheight="0" frameborder="NO" noresize>

              <frame name="bottomBorderFrame" scrolling="NO" noresize src="Frame/ramme.htm">

            </frameset>

            <frame name="rightBorderFrame" scrolling="NO" noresize src="Frame/ramme.htm" frameborder="NO" marginwidth="0" marginheight="0">

          </frameset>

        </frameset>

      </frameset>

      <frame name="bottomMarginFrame" scrolling="NO" noresize src="Frame/margin.htm" marginwidth="0" marginheight="0" frameborder="NO">

    </frameset>

    <frame name="rightMarginFrame" scrolling="NO" noresize src="Frame/margin.htm" frameborder="NO">

  </frameset>

  <noframes> 

  <body bgcolor="#FFFFFF" text="#000000">
    <a href="robots.htm">robots</a>
  </body>

  </noframes> 

<? } else { ?>
   <body bgcolor="#FFFFFF" text="#000000">
     <p><font color="#0000FF" size="7">www.puls3060.dk</font> </p>
     <p><font size="5"><b>Puls 3060's hjemmeside er for &oslash;jeblikket ved at blive opdateret.</b></font></p>
     <p><b><font size="5">Pr&oslash;v igen senere.</font></b></p>
   </body>
<? } ?>

</html>

