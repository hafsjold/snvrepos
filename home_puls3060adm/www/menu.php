<? header ("Last-Modified: " . gmdate("D, d M Y H:i:s") . " GMT"); // always modified ?>
<? 
   if (isset($_SERVER['SSL_CLIENT_S_DN_CN'])) $Client = $_SERVER['SSL_CLIENT_S_DN_CN'];
   else $Client = NULL;
?>
<HTML>
<HEAD>
<SCRIPT>
var obj_open = null;
var SubTree_clik = false;
var windowindex = 0;

function Confirm(childid)
{
  SubTree_clik = true;

  var url;
  var newwindow;
  var windowname;

  url = "confirm.php?menuid="+childid;
  windowname = "Puls3060Adm"+(++windowindex);
  newwindow = window.open(url,windowname,"status");
  newwindow.resizeTo(300, 250);
  newwindow.focus();
}

function SubTree(url, target)
{
  SubTree_clik = true;

  if (target == 'new'){
    var newwindow;
    var windowname;
    windowname= "Puls3060Adm"+(++windowindex);
    newwindow = window.open(url,windowname,"status, scrollbars, resizeable");
    newwindow.focus();
  }
  else
    window.parent.document.all.main_window.src = url
}

function SubTreeShow(obj)
{
  if ( SubTree_clik == true )
  {
     SubTree_clik = false;
     return;
  }
  if ( obj == obj_open )
  {
    obj_open.children[1].outerHTML='';
    obj_open = null;
  }
  else
  {
    if ( obj_open != null ){obj_open.children[1].outerHTML='';}
    obj.insertAdjacentHTML("BeforeEnd", document.all["Tree_"+obj.id].innerHTML);
    obj_open = obj;
  }
}
</SCRIPT>
<link REL="STYLESHEET" HREF="css/menu.css" TYPE="text/css">
</HEAD>
<BODY class=menu>
<?
  $Menu = NULL;
  $Tree_Menu = NULL;
  
  include_once("conn.inc");  
  $dbLink = pg_connect($conn_www);
  
  $CrLf = Chr(13) . Chr(10);
  $prevparentid = 0;

  //$Query="SELECT * FROM vmenu;";
  
  $Query = "SELECT id FROM tblperson WHERE (fornavn || ' ' || efternavn) = '" . $Client . "';";
  $dbResult = pg_query($dbLink, $Query);
  if ($dbResult){
  	$row = pg_fetch_array($dbResult, NULL , PGSQL_ASSOC);
    if ($row)
  	  $personid = $row["id"];
	else
  	  $personid = 0;
  }
  else
  	$personid = 0;


  $Query  = "SELECT l.parentid, l.menuseq, p.menutext AS parentname, l.childid, c.menutext ";
  $Query .= " AS childname, c.menulink, c.target, c.confirm	";
  $Query .= " FROM (((tblmenulink l LEFT JOIN tblmenu p ON ((l.parentid = p.id))) ";
  $Query .= " LEFT JOIN tblmenu c ON ((l.childid = c.id))) LEFT JOIN tblmenuperson s ON ((l.childid = s.menuid)))";
  $Query .= " WHERE (((c.secure = false) OR ((c.secure = true) AND (s.personid = $personid))) AND (l.parentid IS NOT NULL))";
  $Query .= " ORDER BY l.parentid, l.menuseq;";

  $dbResult = pg_query($dbLink, $Query);
  if ($dbResult){
	while($row = pg_fetch_array($dbResult, NULL , PGSQL_ASSOC))
	{
      If ($row["parentid"] <> $prevparentid){
        If ($prevparentid <> 0){ 
          $Tree_Menu = $Tree_Menu . "</TABLE></DIV>" . $CrLf;
        }Else{ 
          $Menu = $Menu . '<DIV ID="MainTree">' . $CrLf;
          $Menu = $Menu . '<table border="0" width="100%" bordercolorlight="#FFFF99" bordercolordark="#FFFF99"  cellspacing="0" style="padding-top: 0; padding-bottom: 0">' . $CrLf;
        }
        $Tree_Menu = $Tree_Menu . '<DIV ID="Tree_Menu' . $row["parentid"] . '" STYLE="display: none">' . $CrLf;
        $Tree_Menu = $Tree_Menu . '<table border="0" width="100%" bordercolorlight="#FFFF99" bordercolordark="#FFFF99"  cellspacing="0" style="padding-top: 0; padding-bottom: 0">' . $CrLf;
        $Menu = $Menu . '  <tr><td><p ID="Menu' . $row["parentid"] . '" ONCLICK="SubTreeShow(this)"><a href="#">' . $row["parentname"] . '</a></p></tr></td>' . $CrLf;
      }   
      if ($row["confirm"] == "t")
        $Tree_Menu = $Tree_Menu . '  <tr><td><p ONCLICK="Confirm(' . "'" . $row["childid"] . "'" . ')">&nbsp;<a href="' . '#' . '">' . $row["childname"] . '</a></p></tr></td>' . $CrLf;
      else
        $Tree_Menu = $Tree_Menu . '  <tr><td><p ONCLICK="SubTree(' . "'" . $row["menulink"] . "', '" . $row["target"] . "'" . ')">&nbsp;<a href="' . '#' . '">' . $row["childname"] . '</a></p></tr></td>' . $CrLf;
      $prevparentid = $row["parentid"];
	}
  }
  If ($prevparentid <> 0) 
    $Tree_Menu = $Tree_Menu . "</TABLE></DIV>" . $CrLf;
  
  $Menu = $Menu . "</TABLE></DIV>" . $CrLf;

  echo $Tree_Menu;
  echo $Menu;
?>
</BODY>
</HTML>