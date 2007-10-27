<? header ("Last-Modified: " . gmdate("D, d M Y H:i:s") . " GMT"); // always modified ?>
<?
setlocale (LC_TIME, "da_DK.ISO8859-1");

if (isset($_REQUEST['ForumId']))
  $ForumId=$_REQUEST['ForumId'];

if (isset($_REQUEST['MessageId']))
  $MessageId=$_REQUEST['MessageId'];

if (isset($_REQUEST['FormForumId']))
  $FormForumId=$_REQUEST['FormForumId'];

if (isset($_REQUEST['FormMessageId']))
  $FormMessageId=$_REQUEST['FormMessageId'];

if (isset($_REQUEST['FormName']))
  $FormName=$_REQUEST['FormName'];

if (isset($_REQUEST['FormEmail']))
  $FormEmail=$_REQUEST['FormEmail'];

if (isset($_REQUEST['FormSubject']))
  $FormSubject=$_REQUEST['FormSubject'];

if (isset($_REQUEST['FormComment']))
  $FormComment=$_REQUEST['FormComment'];


function DatoFormat($Sender)
{
  return(strtolower(strftime("%d-%b-%Y %H:%M", strtotime($Sender))));
}

include_once("conn.inc");  
$dbLink = pg_connect($conn_www);


$Posted = 0;
if ((strlen(trim($FormSubject))==0) AND (strlen(trim($FormComment))==0))
{ 
	$Query="SELECT Id, Forum
            FROM tblForums
            WHERE Id = $ForumId ";
    $dbResult = pg_query($dbLink, $Query);
    $row = pg_fetch_array($dbResult);
    if ($row)
    {
        $intForumId = $row["id"];
        $strForum = $row["forum"];
    } 
}
else
{ 
	$Query = "INSERT INTO tblForumEntries";
	if (strlen(trim($FormMessageId))==0)
	{
		$Query .= " (name_x, email, subject, comment_x, ForumId, orgthread, date_x)";
		$Query .= " VALUES('" . trim($FormName) . "', '" . trim($FormEmail) . "', '" . trim($FormSubject) . "', '" . trim($FormComment) . "', " . $FormForumId . ", 1, (SELECT CURRENT_TIMESTAMP) )";
	}
	else
	{
		$tempSQL = "SELECT subject FROM tblForumEntries WHERE (Id=$FormMessageId)";
        $tempResult = pg_query($dbLink, $tempSQL);
        $temprow = pg_fetch_array($tempResult, NULL , PGSQL_ASSOC);
		
		$Query .= " (name_x, email, comment_x, ForumId, replyid, subject, date_x)";
		$Query .= " VALUES('" . trim($FormName) . "', '" . trim($FormEmail) . "', '" . trim($FormComment) . "', " . $FormForumId . "," . $FormMessageId . ",'" . trim($temprow["subject"]) . "', (SELECT CURRENT_TIMESTAMP) )";
	}
    $dbResult = pg_query($dbLink, $Query);
	$Posted = 1;
}
?>
<html>

<head>
<title>Puls 3060 Forum [ Send et indl&aelig;g ]</title>
<script language="JavaScript">
  function ForumDefaultLink(){
    //top.document.all.mainFrame.src = "Forum/forumdefault.php"
    //top.document.all.leftFrame.src = "Forum/forumdefaultmenu.htm"
    top.leftmain("/Forum/forumdefaultmenu.htm", "/Forum/forumdefault.php")
  }
  function ForumThreadsLink(){}
  function ForumPostLink(){}
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
    <td width="100%" ><strong><font face="Verdana" color="#003399"><small><b>Forum:</b> 
      <? if ($Posted==0){echo $strForum;} ?>
      </small></font></strong></td>
  </tr>
  <tr> 
    <td width="100%"> 
      <? if ($Posted==0){echo "<p align=\"left\">";} ?>
          </td>
  </tr>
  <tr> 
    <td width="100%"> 
    <? if ( (strlen(trim($FormSubject))==0) AND (strlen(trim($FormComment))==0) ){ ?>
    
        <form method="POST" action="forumpost.php">
        
        <input type="hidden" name="FormForumId" value="<?=$intForumId?>">
        
        <input type="hidden" name="FormMessageId" value="<?=trim($MessageId)?>">
        
        <table border="0" width="100%" cellspacing="1" cellpadding="0">
          <tr> 
            <td align="left" ><small><font face="Verdana">Navn:</font></small></td>
            <td align="left" ><font face="Verdana"><small>
              <input type="text" name="FormName" size="20">
              </small></font></td>
          </tr>
          <tr> 
            <td align="left"><small><font face="Verdana">E-mail:</font></small></td>
            <td align="left"><small><font face="Verdana">
              <input type="text" name="FormEmail" size="20">
              </font></small></td>
          </tr>
          <tr> 
            <td align="left"><small><font face="Verdana">Emne:</font></small></td>
            <td align="left"><small><font face="Verdana">
              <input type="text" name="FormSubject" size="30">
              </font></small></td>
          </tr>
          <tr> 
            <td valign="top" align="left"><font face="Verdana"><small>Besked:</small></font></td>
            <td align="left">
              <textarea rows="8" name="FormComment" cols="50"></textarea>
            </td>
          </tr>
        </table>
        
        <p>
        <input type="submit" value="Send" name="Action">
        </p>
      </form>
      
    <? } else { ?>
      <p align="center"><font face="Verdana"><small>&nbsp;</p></small></font>
      <p align="center"><font face="Verdana"><small>Dit indlæg er nu optaget i forumet</small></font></p>
      <p align="center"><font face="Verdana"><a href="javascript:ForumDefaultLink()"><small>Til oversigt</small></a></p></font><p> 
    <? } ?>
    </td>
  </tr>
</table>
    <td width="30">&nbsp;</td>
  </tr>
  <tr> 
    <td width="30">&nbsp;</td>
    <td align="right"><br>
      <font color=#a03952 face="Arial, Helvetica, sans-serif" size=2><i><b></b></i></font> <br>
      <br>
    </td>
    <td width="30">&nbsp;</td>
  </tr>
</table>
</body>
</html>
