<? header ("Last-Modified: " . gmdate("D, d M Y H:i:s") . " GMT"); // always modified ?>
<?
setlocale (LC_TIME, "da_DK.ISO8859-1");
function getcolor()
{
  global $colorswitch;
  if ($colorswitch==0){
	$colorswitch=1;
    return("#CFE3CC");
  }
  else{
	$colorswitch=0;
    return("#CBE2E4");
  }
}
function DatoFormat($Sender)
{
  return(strtolower(strftime("%d-%b-%Y %H:%M", strtotime($Sender))));
}

include_once("conn.inc");  
$dbLink = pg_connect($conn_www);

$Query="SELECT Id,
			   Forum,
			   Description 	
 		FROM tblForums";
$dbResult = pg_query($dbLink, $Query);
while($row = pg_fetch_array($dbResult, NULL , PGSQL_ASSOC))
{
  $rowId=$row["id"];
  $Query ="SELECT date_trunc('second', date_x) as date_x
           FROM tblForumEntries 
           WHERE ForumId=$rowId 
           ORDER BY Id DESC";
  $DateResult = pg_query($dbLink, $Query);
  if ($ItemCount = pg_num_rows ($DateResult)){
    $DateRow = pg_fetch_array($DateResult, NULL , PGSQL_NUM);
	$dtmFormatDate = $DateRow[0];
  }
  else{
	$dtmFormatDate = "Ingen indlæg";
  }
  $strHTML .= "<tr><td><strong><font face=Verdana size=2><a href=\"javascript:LineThreadsLink($rowId)\">".$row["forum"]."</a></strong><br>" . htmlentities($row["description"]) . "</font></td>";
  $strHTML .= "<td valign=top><font face=Verdana size=2>$ItemCount</font></td>";
  $strHTML .= "<td valign=top><font face=Verdana size=2>".DatoFormat($dtmFormatDate)."</font></td></tr>";
}
?>
<html>
<head>
<title>Puls3060 Forum [ Oversigt ]</title>
<script language="JavaScript">
  function ForumDefaultLink(){}
  function ForumThreadsLink(){}
  function ForumPostLink(){}
  function LineThreadsLink(forumid){
    //top.document.all.mainFrame.src = "Forum/forumthreads.php?ForumId="+forumid
    //top.document.all.leftFrame.src = "Forum/forumthreadsmenu.htm"
    top.leftmain("/Forum/forumthreadsmenu.htm", "/Forum/forumthreads.php?ForumId="+forumid)
  }
</script>
</head>

<body bgcolor="#FFFFFF" text="#000000">
<table width="100%" border="0" cellspacing="0" cellpadding="0" dwcopytype="CopyTableRow">
  <tr> 
    <td width="30">&nbsp;</td>
    <td>&nbsp;</td>
    <td width="30">&nbsp;</td>
  </tr>
  <tr> 
    <td width="30">&nbsp;</td>
    <td> 


<table border="0" width="547" cellspacing="1" cellpadding="0">
  <tr>
    <td valign="top" width="547">
          <table border="0" width="547" cellspacing="1"
    cellpadding="0">
            <tr> 
              <td width="306"><strong><font face="Verdana" size="2" color="#003399">Forums</font></strong></td>
              <td width="115"><strong><font face="Verdana" size="2" color="#003399">Antal 
                indlæg</font></strong></td>
              <td width="126"><strong><font face="Verdana" size="2" color="#003399">Seneste 
                indlæg</font></strong></td>
            </tr>
            <?=$strHTML?> 
          </table>
    </td>
  </tr>
</table>
    <td width="30">&nbsp;</td>
  </tr>
  <tr> 
    <td width="30">&nbsp;</td>
    <td align="right"><br>
      <br>
      <br>
    </td>
    <td width="30">&nbsp;</td>
  </tr>
</table>
</body>
</html>
