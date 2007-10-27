<? header ("Last-Modified: " . gmdate("D, d M Y H:i:s") . " GMT"); // always modified ?>
<?
setlocale (LC_TIME, "da_DK.ISO8859-1");

if (isset($_REQUEST['Id']))
  $Id=$_REQUEST['Id'];

if (isset($_REQUEST['ForumId']))
  $ForumId=$_REQUEST['ForumId'];

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


$Query="SELECT Id, Forum, Description 	
 		FROM tblForums
        WHERE Id=$ForumId"; 
$dbResult = pg_query($dbLink, $Query);
if ($row = pg_fetch_array($dbResult, NULL , PGSQL_ASSOC)){
	$strForum = $row["forum"];
	$strDescription = $row["description"];
	$intForumId = $row["id"];
} 
$strDEBUG .= "<p>$strForum</>";
$strDEBUG .= "<p>$strDescription</>";
$strDEBUG .= "<p>$intForumId</>";
$Query="SELECT 	Id, subject, date_trunc('second', date_x) as date_x 
		FROM tblForumEntries 
		WHERE (ForumId=$ForumId) AND (OrgThread<>0) 
		ORDER BY Id DESC";
$dbResult = pg_query($dbLink, $Query);
while($row = pg_fetch_array($dbResult, NULL , PGSQL_ASSOC))
{
  $rowId=$row["id"];
$strDEBUG .= "<p>$rowId</>";
  $Query="SELECT Count(*) AS ItemCount 
           FROM tblForumEntries 
		   WHERE (replyid=$rowId) AND (orgthread=0)";
  $CountResult = pg_query($dbLink, $Query);
  $CountRow = pg_fetch_array($CountResult, NULL , PGSQL_NUM);
  $ItemCount = $CountRow[0];
$strDEBUG .= "<p>Subject: " .$row["subject"] . "</>";
  $strHTML .= "<tr><td><font face=Verdana size=2><a href=\"javascript:LineViewMsgLink($rowId, $intForumId)\">" . htmlentities($row["subject"]) . "</font></a></td>";
  $strHTML .= "<td valign=top><font face=Verdana size=2>$CountRow[0]</font></td>";
  $strHTML .= "<td valign=top><font face=Verdana size=2>" . DatoFormat($row["date_x"]) . "</font></td></tr>";
}
?>
<html>
<head>
<title>Puls 3060 Forum [ Indl&aelig;g ]</title>
<script language="JavaScript">
  function ForumDefaultLink(){
    //top.document.all.mainFrame.src = "Forum/forumdefault.php"
    //top.document.all.leftFrame.src = "Forum/forumdefaultmenu.htm"
    top.leftmain("/Forum/forumdefaultmenu.htm", "/Forum/forumdefault.php")
  }
  function ForumThreadsLink(){}
  function ForumPostLink(){
    //top.document.all.mainFrame.src = "Forum/forumpost.php?ForumId=<?=$intForumId?>"
    //top.document.all.leftFrame.src = "Forum/forumpostmenu.htm"
    top.leftmain("/Forum/forumpostmenu.htm", "/Forum/forumpost.php?ForumId=<?=$intForumId?>")
  }
  function LineViewMsgLink(id, forumid){
    //top.document.all.mainFrame.src = "Forum/viewmsg.php?Id="+id+"&ForumId="+forumid
    //top.document.all.leftFrame.src = "Forum/viewmsgmenu.htm"
    top.leftmain("/Forum/viewmsgmenu.htm", "/Forum/viewmsg.php?Id="+id+"&ForumId="+forumid)
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
      <table border="0" width="547" cellspacing="1" cellpadding="0" >
        <tr> 
          <td colspan="2"><font face="Verdana" size="2" color="#003399"><strong>Forum:</strong><strong><?=$strForum?></strong></font></td>
        </tr>
        <tr> 
          <td valign="top" align="left" width="306"><font face="Verdana" size="2"><?=$strDescription?></font></td>
          <td valign="top" align="left" nowrap width="209"> <font face="Verdana"><strong><br>
            &nbsp;&nbsp; </strong></font></td>
        </tr>
        <tr> 
          <td colspan="2"> 
            <table border="0" width="100%" cellspacing="1" cellpadding="0">
              <tr> 
                <td width="346"><font face="Verdana" color="#003399" size="2"><strong>Indl&aelig;g</strong></font></td>
                <td width="58"><font face="Verdana" color="#003399" size="2"><strong>Svar</strong></font></td>
                <td width="137"><font face="Verdana" color="#003399" size="2"><strong>Seneste 
                  indlæg</strong></font></td>
              </tr>
			  <?//=$strDEBUG?>
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
