<? header ("Last-Modified: " . gmdate("D, d M Y H:i:s") . " GMT"); // always modified ?>
<?
setlocale (LC_TIME, "da_DK.ISO8859-1");

if (isset($_REQUEST['Id']))
  $Id=$_REQUEST['Id'];

if (isset($_REQUEST['ForumId']))
  $ForumId=$_REQUEST['ForumId'];

if (isset($_REQUEST['Showsub']))
  $Showsub=$_REQUEST['Showsub'];

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
$Query = "SELECT id, forumid, name_x, email, subject, 
		    comment_x, date_trunc('second', date_x) as date_x, 
		    replies, orgthread, replyid
		  FROM tblForumEntries 
		  WHERE Id =$Id AND orgthread<>0";
$dbResult = pg_query($dbLink, $Query);
if ($row = pg_fetch_array($dbResult, NULL , PGSQL_ASSOC)){
	$intMessageId = $row["id"];
	$strSubject = htmlentities($row["subject"]);
	$strName = htmlentities($row["name_x"]);
	$strEmail = htmlentities($row["email"]);
	$dtmFormDate = DatoFormat($row["date_x"]);
	$strComment = nl2br(htmlentities($row["comment_x"]));
} 
$Query = "SELECT id, forumid, name_x, email, subject, 
		    comment_x, date_trunc('second', date_x) as date_x, 
		    replies, orgthread, replyid
		  FROM tblForumEntries 
		  WHERE (replyid=$Id) AND (orgthread=0) 
		  ORDER BY ID DESC";
$dbResult = pg_query($dbLink, $Query);
while($row = pg_fetch_array($dbResult, NULL , PGSQL_ASSOC))
{
  $strSvar .= "<li><font face=Verdana size=1><strong><a href=\"javascript:LineViewMsgLink(" . $intMessageId . "," . $intForumId . "," . $row["id"] . ")\">Re: " . $strSubject . "</a> <em>(" . htmlentities($row["name_x"]) . ", " . DatoFormat($row["date_x"]) . ")</em></strong></font></li>";
  If (($Showsub) And ($Showsub=$row["id"])){
    $strSvar .= "<br><font face=Verdana size=1>" . nl2br(htmlentities($row["comment_x"])) . "</font><br>&nbsp;";
  }
}
?>
<html>

<head>
<title>Puls 3060 Forum [ L&aelig;s indl&aelig;g ]</title>
<script language="JavaScript">
  function ForumDefaultLink(){
    //top.document.all.mainFrame.src = "Forum/forumdefault.php"
    //top.document.all.leftFrame.src = "Forum/forumdefaultmenu.htm"
    top.leftmain("/Forum/forumdefaultmenu.htm", "/Forum/forumdefault.php")
  }
  function ForumThreadsLink(){
    //top.document.all.mainFrame.src = "Forum/forumthreads.php?ForumId=<?=$intForumId?>"
    //top.document.all.leftFrame.src = "Forum/forumthreadsmenu.htm"
    top.leftmain("/Forum/forumthreadsmenu.htm", "/Forum/forumthreads.php?ForumId=<?=$intForumId?>")
  }
  function ForumPostLink(){
    //top.document.all.mainFrame.src = "Forum/forumpost.php?ForumId=<?=$intForumId?>&MessageId=<?=$intMessageId?>"
    //top.document.all.leftFrame.src = "Forum/forumpostmenu.htm"
    top.leftmain("/Forum/forumpostmenu.htm", "/Forum/forumpost.php?ForumId=<?=$intForumId?>&MessageId=<?=$intMessageId?>")
  }
  function LineViewMsgLink(id, forumid, subid){
    //top.document.all.mainFrame.src = "Forum/viewmsg.php?Id="+id+"&ForumId="+forumid+"&Showsub="+subid
    //top.document.all.leftFrame.src = "Forum/viewmsgmenu.htm"
    top.leftmain("/Forum/viewmsgmenu.htm", "/Forum/viewmsg.php?Id="+id+"&ForumId="+forumid+"&Showsub="+subid)
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
        <td width="598" colspan="2"><strong><font face="Verdana" size="2" color="#003399">Forum: 
          <?=$strForum?> </font></strong></td>
      </tr>
      <tr> 
        <td valign="top" width="70%"><font face="verdana" size="2"><?=$strDescription?> 
          </font></td>
          <td width="30%">&nbsp; </td>
      </tr>
      <tr> 
        <td width="598" colspan="2">
          <table border="0" width="100%" cellspacing="1" cellpadding="0">
            <tr> 
              <td width="57" align="left"><font face="Verdana" size="2" color="#003399"><strong>Emne:</strong></font></td>
              <td width="339"><font face="Verdana" size="2" color="#003399"><strong><?=$strSubject?></strong></font></td>
            </tr>
            <tr> 
              <td width="57" align="left"><font face="Verdana" size="2" color="#003399"><strong>Navn:</strong></font></td>
              <td width="339"><font face="Verdana" size="2" color="#003399"><?=$strName?></font></td>
            </tr>
            <tr> 
              <td width="57" align="left"><font face="Verdana" size="2" color="#003399"><strong>E-mail:</strong></font></td>
              <td width="339"><font face="Verdana" size="2" color="#003399"><?=$strEmail?></font></td>
            </tr>
            <tr> 
              <td width="57" align="left"><font face="Verdana" size="2" color="#003399"><strong>Dato:</strong></font></td>
              <td width="339"><font face="Verdana" size="2" color="#003399"><?=$dtmFormDate?></font></td>
            </tr>
            <tr> 
              <td width="396" align="left" colspan="2"><small><font face="Verdana"><?=$strComment?></font></small></td>
            </tr>
            <tr> 
              <td align="left" colspan="2"> 
                <hr noshade size="1" color="#000000">
              </td>
            </tr>
            <tr> 
                <td align="left" colspan="2"><font face="Verdana" color="#003399"><strong><small>Kommentar:</small></strong></font></td>
            </tr>
            <tr> 
              <td align="left" colspan="2"><small><font face="Verdana"> 
                <ul
        type="square">
                  <?=$strSvar?> 
                </ul>
                </font></small></td>
            </tr>
          </table>
        </td>
      </tr>
    </table>
    <td width="30">&nbsp;</td>
  </tr>
  <tr> 
    <td width="30">&nbsp;</td>
    <td align="right"><br>
      <font color=#a03952 face="Arial, Helvetica, sans-serif" 
size=2><i><b></b></i></font> <br>
      <br>
    </td>
    <td width="30">&nbsp;</td>
  </tr>
</table>
</body>
</html>
