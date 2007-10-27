<? header ("Last-Modified: " . gmdate("D, d M Y H:i:s") . " GMT"); // always modified ?>
<HTML>
<HEAD>
<SCRIPT>
function SubmitJob(url)
{
  var newwindow;
  var windowname;
  windowname= "Puls3060AdmConfirm";
  newwindow = window.open(url,windowname,"status, scrollbars, resizeable");
  newwindow.resizeTo(600, 700);
  newwindow.focus();
  window.close();
}

</SCRIPT>
<link REL="STYLESHEET" HREF="css/menu.css" TYPE="text/css">
<title>Confirm</title>
</HEAD>
<BODY class=menu>
<?
  if (isset($_REQUEST['menuid'])) 
    $menuid=$_REQUEST['menuid'];
  else
    $menuid = 0;

  include_once("conn.inc");  
  $dbLink = pg_connect($conn_www);

  $CrLf = Chr(13) . Chr(10);
  $prevparentid = 0;

  $Query="SELECT * FROM tblmenu WHERE id =$menuid;";
  $dbResult = pg_query($dbLink, $Query);
  if ($dbResult){
	if($row = pg_fetch_array($dbResult, NULL , PGSQL_ASSOC)){
       $menutext = $row["menutext"];
       $menulink = $row["menulink"];
	}
  }
?>

<p align="center"><font size="6" color="#0066CC">Bekr&aelig;ft</font></p>
<form name="form1" method="post" action="">
  <table width="100%" border="0" cellspacing="0" cellpadding="0">
    <tr> 
      <td colspan="2"> 
        <div align="center">Bekr&aelig;ft at du vil udf&oslash;re:</div>
      </td>
    </tr>
    <tr> 
      <td colspan="2">&nbsp;</td>
    </tr>
    <tr> 
      <td colspan="2"> 
        <div align="center"><b> 
          <?=$menutext?>
          </b></div>
      </td>
    </tr>
    <tr> 
      <td colspan="2">&nbsp;</td>
    </tr>
    <tr> 
      <td width="50%"> 
        <div align="center"> 
          <input type="button" name="Confirm" value="Ja" onclick="SubmitJob('<?=$menulink?>');">
        </div>
      </td>
      <td width="50%"> 
        <div align="center"> 
          <input type="button" name="Cancel" value="Nej" onclick="window.close();">
        </div>
      </td>
    </tr>
  </table>
</form>
<p>&nbsp;</p>
</BODY>
</HTML>