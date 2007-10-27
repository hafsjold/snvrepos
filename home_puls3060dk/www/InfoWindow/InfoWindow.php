<? header ("Last-Modified: " . gmdate("D, d M Y H:i:s") . " GMT"); // always modified ?>
<html>
<head>
<title>Puls 3060 Info </title>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
</head>

<body onLoad="fonLoad();" bgcolor="#99CC00" text="#000000">
<?
    setlocale (LC_TIME, "da_DK.ISO8859-1");

    function getcolor()
	{
	  global $colorswitch;
	  if ($colorswitch==0){$colorswitch=1; return("#CFE3CC");}
	  else{$colorswitch=0; return("#CBE2E4");}
	}
    function DatoFormat($Sender)
	{
      return(strtolower(strftime("%a %d. %b", strtotime($Sender) )));
	}
    function gettarget($Sender)
	{
	  if ($Sender=="1"){ return("_blank");}
	  else{return("_self");}
	}

	$line = 0;
    include_once("conn.inc");  
    $dbLink = pg_connect($conn_www);
	
	$Query="SELECT klinieid, kliniedato, klinietext, klines, link, newwindow
            FROM tblklinie 
			WHERE kliniedato >= now() 
			AND kliniedato < (now() + interval '28 days') 
			AND info = 1
            ORDER BY kliniedato, klinieid;";
	$dbResult = pg_query($dbLink, $Query);
?>

<p><font face="Times New Roman, Times, serif" size="6" color="#003399"><b>Puls 
  3060 Info</b></font> </p>
<table name="InfoTable">
  <tr style=background-color:#FF0000> 
    <th align=center style="width:80px;"><font color="#003399">Dato</font></th>
    <th align=left><font color="#003399">Aktivitet</font></th>
  </tr>
  <?
	while($row = pg_fetch_array($dbResult, NULL , PGSQL_ASSOC))
	{
       $line += $row["klines"];
  ?>
      <tr valign=top style=background-color:<?=getcolor();?>> 
        <td align=center> <?=DatoFormat($row["kliniedato"]);?> </td>
        <? if (eregi ("<a([^>]+)>", $row["klinietext"])){ 
           $klinietext_html = eregi_replace("<a([^>]+)>", 
                   			"<a\\1 onClick=\"return top.rwlink(href);\">", 
                  			$row["klinietext"]);        	
        ?>
          <td> 
            <?=$klinietext_html;?>
          </td>
        <? }elseif ($row["link"]==""){ ?>
          <td> 
            <?=nl2br($row["klinietext"]);?>
          </td>
        <? }else{ ?>
          <td>
            <a href=<?=$row["link"];?> onClick="return top.rwlink(href);" target=<?=gettarget($row["newwindow"]);?>> 
              <?=nl2br($row["klinietext"]);?> 
            </a> 
          </td>
        <? } ?>
      </tr>
<?
	}
?>
</table>
<p><a href="/medmnu/Medlem/NytMedlem.php" onClick="return top.rwlink(href);" >Bliv medlem af Puls 3060 . .</a><br>
  <a href="/kalmnu/Kalender/Aktivitetskalender.php" onClick="return top.rwlink(href);" >Flere aktiviteter . .</a><br>
  <a href="/nyhmnu/Nyheder/Nyheder.php" onClick="return top.rwlink(href);" >Flere nyheder . .</a></p>
</body>

<script language="javascript">
<!--
function fonLoad(){
	var h = 250;
	var w = 450;
	var l = <?=$line?>;
	window.resizeTo(w, h+(l*20));
}

function leftmain(parm1,parm2){
    var newwindow;
	newwindow = top.opener;
    try{
   	  newwindow.top.leftmain(parm1,parm2);
    }
    catch (e) {
      var url = "http://www.puls3060.dk?p1=" + parm1 + "&p2=" + parm2 + "&p3=no";
	  newwindow = window.open(url,"Puls3060");
    }
	newwindow.focus();
}

function rwlink(link){
  var exp = new RegExp("^(/|http://www.puls3060.dk/)(nyhmnu|lobmnu|kalmnu|medmnu|tramnu|velmnu|lnkmnu|formnu|tstmnu)(/[^?]*)(.*)$");
  var m = exp.exec(link);
  var map = new Array();
  map["nyhmnu"] = "/menu/nyhedermenu.php";
  map["lobmnu"] = "/menu/lobmenu.php";
  map["kalmnu"] = "/menu/kalendermenu.php";
  map["medmnu"] = "/menu/Medlem.php";
  map["tramnu"] = "/menu/traeningmenu.php";
  map["velmnu"] = "/menu/Velkom.php";
  map["lnkmnu"] = "/Frame/TomMenu.htm";
  map["formnu"] = "/Frame/TomMenu.htm";
  map["tstmnu"] = "/menu/testmenu.php";

  if (m != null) {
    if (m[1] != 'https://www.puls3060.dk/') {
      if ((m[3] == '/Medlem/NytMedlem.php')
      ||  (m[3] == '/Lob/LobUserTilmelding.php')
      ||  (m[3] == '/pbstest/LobUserTilmelding.php')
      ||  (m[3] == '/Kalender/AktUserTilmelding.php')
      ||  (m[3] == '/Test/Test.htm')
    ) {
        var newwindow;
	    newwindow = top.opener;
        try{
   	      newwindow.top.location.href = 'https://www.puls3060.dk/' + m[2] + m[3] + m[4];
        }
        catch (e) {
          var url = 'https://www.puls3060.dk/' + m[2] + m[3] + m[4];
	      newwindow = window.open(url,"Puls3060");
        }
	    newwindow.focus();
        return false;
      }
    }
  }

  if (m != null) {
    top.leftmain(map[m[2]], m[3] + m[4]);  
    return false;
  }
  return true;
}
// -->
</script>
</html>
