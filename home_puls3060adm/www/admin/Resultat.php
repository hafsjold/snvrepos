<? header ("Last-Modified: " . gmdate("D, d M Y H:i:s") . " GMT"); // always modified ?>
<html>
<head>
<title>Resultat</title>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
</head>
<body bgcolor="#FFFFFF" text="#000000" onLoad="fonLoad()">
<?
include_once("conn.inc");
setlocale (LC_TIME, "da_DK.ISO8859-1");

if (isset($_REQUEST['SQLUpdate']))
  $SQLUpdate=$_REQUEST['SQLUpdate'];

if (isset($_REQUEST['lines']))
  $lines=$_REQUEST['lines'];

if (isset($_REQUEST['checkbox_Nr']))
  $checkbox_Nr=$_REQUEST['checkbox_Nr'];

if (isset($_REQUEST['checkbox_M']))
  $checkbox_M=$_REQUEST['checkbox_M'];

if (isset($_REQUEST['checkbox_S']))
$checkbox_S=$_REQUEST['checkbox_S'];

if (isset($_REQUEST['checkbox_T']))
$checkbox_T=$_REQUEST['checkbox_T'];

if (isset($_REQUEST['DoWhat']))
$DoWhat=$_REQUEST['DoWhat'];
else
$DoWhat=1;

if (isset($_REQUEST['max_lines']))
$max_lines=$_REQUEST['max_lines'];
$nr = 0;

for ($i=0; $i < $max_lines; $i++) {
	${"nr_{$i}"}           = ++$nr;
	${"nummer_{$i}"}       = $_POST["nummer_{$i}"];
	${"tid_t_{$i}"}        = $_POST["tid_t_{$i}"];
	${"tid_m_{$i}"}        = $_POST["tid_m_{$i}"];
	${"tid_s_{$i}"}        = $_POST["tid_s_{$i}"];
	${"nummer_{$i}_old"}   = $_POST["nummer_{$i}_old"];
	${"tid_t_{$i}_old"}    = $_POST["tid_t_{$i}_old"];
	${"tid_m_{$i}_old"}    = $_POST["tid_m_{$i}_old"];
	${"tid_s_{$i}_old"}    = $_POST["tid_s_{$i}_old"];
}


if (isset($_REQUEST['lobid']))
$lobid=$_REQUEST['lobid'];
else
$lobid=52;

function getcolor()
{
	global $colorswitch;
	if ($colorswitch==0){$colorswitch=1; return("#CFE3CC");}
	else{$colorswitch=0; return("#CBE2E4");}
}
function DatoFormat($Sender)
{
	return(strtolower(strftime("%d. %b", strtotime($Sender))));
}
function Timer($Sender)
{
	$T =ltrim(strtolower(strftime("%H", strtotime($Sender))),"0");
	return($T);
}
function Minuter($Sender)
{
	$T =ltrim(strtolower(strftime("%H", strtotime($Sender))),"0");
	$M =ltrim(strtolower(strftime("%M", strtotime($Sender))),"0");
	if ((empty($T))
	&&  (empty($M)))
	return($M);

	if (empty($M))
	return(0);
	else
	return($M);
}
function Sekunder($Sender)
{
	$T =ltrim(strtolower(strftime("%H", strtotime($Sender))),"0");
	$M =ltrim(strtolower(strftime("%M", strtotime($Sender))),"0");
	$S =ltrim(strtolower(strftime("%S", strtotime($Sender))),"0");

	if ((empty($T))
	&&  (empty($M))
	&&  (empty($S)))
	return($S);

	if (empty($S))
	return(0);
	else
	return($S);
}

function TidFormat($Sender)
{
	return(substr($Sender, 11, 5));
}

function SQLnum($Tal)
{
	if (empty($Tal))
	return("null");

	return($Tal);
}

function SQLtid($T, $M, $S)
{
	if ((!(empty($T)))
	&& (($T < 0) || ($T > 24)) )
	return("null");

	if ((!(empty($M)))
	&& (($M < 0) || ($M > 60)))
	return("null");

	if ((!(empty($S)))
	&& (($S < 0) || ($S > 60)))
	return("null");

	if ((empty($T))
	&&  (empty($M))
	&&  (empty($S)))
	return("null");

	if (empty($T))
	$T = 0;
	if (empty($M))
	$M = 0;
	if (empty($S))
	$S = 0;
	$ret = "time '" . $T . ":" . $M . ":" . $S . "'";
	return($ret);
}

    $dbLink = pg_connect($conn_www);
    if ($SQLUpdate) {
      for($i = 0; $i < $lines; $i++) {
	  	if (!((${"nummer_{$i}"} == ${"nummer_{$i}_old"})
	  	&&    (${"tid_t_{$i}"} == ${"tid_t_{$i}_old"})
	  	&&    (${"tid_m_{$i}"} == ${"tid_m_{$i}_old"})
	  	&&    (${"tid_s_{$i}"} == ${"tid_s_{$i}_old"}))) {
	      $Query="
			 SELECT Upd_tblnrtid (
			   $lobid, 
			   ${"nr_{$i}"}, "
			   . SQLnum(${"nummer_{$i}"}) . ", "
			   . SQLtid(${"tid_t_{$i}"}, ${"tid_m_{$i}"}, ${"tid_s_{$i}"}) . "
			 ) AS result;
			 ";
	      $dbResult = pg_query($dbLink, $Query);
          $row = pg_fetch_array($dbResult, NULL , PGSQL_ASSOC);
        }
	  }
    }

if ($DoWhat==1) {
	$checkbox_Nr = "checkbox";
	$checkbox_M = "checkbox";
	$checkbox_S = "checkbox";

	$dbLink = pg_connect($conn_www);

	$Query="
		SELECT 	lobid, nr, nummer, tid 
		FROM 	tblnrtid
		WHERE   lobid=$lobid
		ORDER BY nr;";

	$dbResult = pg_query($dbLink, $Query);

	$lines = 0;
	while($row = pg_fetch_array($dbResult, NULL , PGSQL_ASSOC)) {
		${"nr_$lines"}           = $row["nr"];
		${"nummer_$lines"}       = $row["nummer"];
		${"tid_t_$lines"}        = Timer($row["tid"]);
		${"tid_m_$lines"}        = Minuter($row["tid"]);
		${"tid_s_$lines"}        = Sekunder($row["tid"]);
		${"nummer_{$lines}_old"} = $row["nummer"];
		${"tid_t_{$lines}_old"}  = Timer($row["tid"]);
		${"tid_m_{$lines}_old"}  = Minuter($row["tid"]);
		${"tid_s_{$lines}_old"}  = Sekunder($row["tid"]);
		$lines++;
	}
}

if ($lines < $max_lines) {
	if ($lines == 0)
	$nr = 0;
	else {
		$wrk = $lines-1;
		$nr = ${"nr_{$wrk}"};
	}
	for ($lines; $lines < $max_lines; $lines++) {
		${"nr_$lines"}           = ++$nr;
		${"nummer_$lines"}       = "";
		${"tid_t_$lines"}        = "";
		${"tid_m_$lines"}        = "";
		${"tid_s_$lines"}        = "";
		${"nummer_{$lines}_old"} = "";
		${"tid_t_{$lines}_old"}  = "";
		${"tid_m_{$lines}_old"}  = "";
		${"tid_s_{$lines}_old"}  = "";
	}
}


$max_lines = $lines;


if ($checkbox_Nr)
$Col_Nr = true;
else
$Col_Nr = false;

if ($checkbox_T)
$Col_T = true;
else
$Col_T = false;

if ($checkbox_M)
$Col_M = true;
else
$Col_M = false;

if ($checkbox_S)
$Col_S = true;
else
$Col_S = false;

$width = 36;
?>
<?
$Query="SELECT navn, dato FROM tblLob WHERE id = $lobid;";
//echo $Query;
$dbResult = pg_query($dbLink, $Query);
if ($row = pg_fetch_array($dbResult, NULL , PGSQL_ASSOC)) {
	$LobNavn  = $row["navn"];
	$LobDato  = $row["dato"];
}
?>
 
 <p align=left>
   <font color=#003399 size=4>
     Resultat Tider<br>
     <?=$LobNavn;?><br>
     <?=$LobDato;?><br>
   </font>
 </p>

<form name="ResultatForm" method="post" action="<?=$_SERVER['SCRIPT_NAME']?>?DoWhat=2">
  <input type="hidden" name="lobid" value="<?=$lobid;?>">
  <input type="hidden" name="lines" value="<?=$lines;?>">


  <table CELLPADDING="0" CELLSPACING="0" BORDER="1" style="border-collapse: collapse">
  <tr style=background-color:#FF0000> 
    <font color=#003399> 
      <th align=center width="<?=$width;?>">#</th>
      <th align=center width="<?=$width;?>">Nr</th>
      <th align=center width="<?=$width;?>">T</th>
      <th align=center width="<?=$width;?>">M</th>
      <th align=center width="<?=$width;?>">S </th>
    </font>
  </tr>
  <tr valign=top style=background-color:<?=getcolor();?>> 
      <td align=center width="<?=$width;?>">
        <input type="text" name="max_lines" size="3" maxlength="3"  value="<?=$max_lines;?>" onChange="updLayout();">
      </td>
      <td align=center width="<?=$width;?>">
        <input type="checkbox" name="checkbox_Nr" value="checkbox";"
        <? if ($Col_Nr) { ?>
          checked>
        <? } else { ?>
          >
        <? } ?>
      </td>
      <td align=center width="<?=$width;?>">
        <input type="checkbox" name="checkbox_T" value="checkbox";"
        <? if ($Col_T) { ?>
          checked>
        <? } else { ?>
          >
        <? } ?>
      </td>
      <td align=center width="<?=$width;?>">
        <input type="checkbox" name="checkbox_M" value="checkbox";"
        <? if ($Col_M) { ?>
          checked>
        <? } else { ?>
          >
        <? } ?>
      </td>
      <td align=center width="<?=$width;?>">
        <input type="checkbox" name="checkbox_S" value="checkbox";"
        <? if ($Col_S) { ?>
          checked>
        <? } else { ?>
          >
        <? } ?>
      </td>
    </tr>
    <? for($i = 0; $i < $lines; $i++) { ?>
      <tr valign=top style=background-color:<?=getcolor();?>> 
        <td align=center width="<?=$width;?>"> 
          <input type="hidden" name="nr_<?=$i;?>" value="<?=${"nr_{$i}"};?>">
          <input type="hidden" name="nummer_<?=$i;?>_old" value="<?=${"nummer_{$i}_old"};?>">
          <input type="hidden" name="tid_t_<?=$i;?>_old" value="<?=${"tid_t_{$i}_old"};?>">
          <input type="hidden" name="tid_m_<?=$i;?>_old" value="<?=${"tid_m_{$i}_old"};?>">
          <input type="hidden" name="tid_s_<?=$i;?>_old" value="<?=${"tid_s_{$i}_old"};?>">
        
          <? if ($Col_Nr == false) { ?> 
            <input type="hidden" name="nummer_<?=$i;?>" value="<?=${"nummer_{$i}"};?>">
          <? } ?> 
        
          <? if ($Col_T == false) { ?> 
            <input type="hidden" name="tid_t_<?=$i;?>" value="<?=${"tid_t_{$i}"};?>">
          <? } ?> 
        
          <? if ($Col_M == false) { ?> 
            <input type="hidden" name="tid_m_<?=$i;?>" value="<?=${"tid_m_{$i}"};?>">
          <? } ?> 
        
          <? if ($Col_S == false) { ?> 
            <input type="hidden" name="tid_s_<?=$i;?>" value="<?=${"tid_s_{$i}"};?>">
          <? } ?> 

          <?=${"nr_{$i}"};?>
        </td>
        <td align=center width="<?=$width;?>"> 
          <? if ($Col_Nr == true) { ?> 
            <input type="text" name="nummer_<?=$i;?>" size="3" maxlength="3" value="<?=${"nummer_{$i}"};?>">
          <? } else { ?> 
            <?=${"nummer_{$i}"};?>
          <? } ?> 
        </td>
        <td align="center" width="<?=$width;?>"> 
          <? if ($Col_T == true) { ?> 
            <input type="text" name="tid_t_<?=$i;?>" size="2" maxlength="2" value="<?=${"tid_t_{$i}"};?>">
          <? } else { ?> 
            <?=${"tid_t_{$i}"};?>
          <? } ?> 
        </td>
        <td align=center width="<?=$width;?>"> 
          <? if ($Col_M == true) { ?> 
            <input type="text" name="tid_m_<?=$i;?>" size="2" maxlength="2" value="<?=${"tid_m_{$i}"};?>">
          <? } else { ?> 
            <?=${"tid_m_{$i}"};?>
          <? } ?> 
        </td>
        <td align=center width="<?=$width;?>"> 
          <? if ($Col_S == true) { ?> 
            <input type="text" name="tid_s_<?=$i;?>" size="2" maxlength="2" value="<?=${"tid_s_{$i}"};?>">
          <? } else { ?> 
		    <?=${"tid_s_{$i}"};?>
          <? } ?> 
        </td>
      </tr>
    <? } //end for ?>
    <tr valign=top style=background-color:<?=getcolor();?>>
      <td align=center>&nbsp;</td>
      <td align=center colspan="2">
        &nbsp;
      </td>
      <td align=center colspan="2">
        &nbsp;
      </td>
    </tr>
    <tr valign=top style=background-color:<?=getcolor();?>> 
      <td align=center>&nbsp;</td>
      <td align=center colspan="2">
        <input type="submit" name="SQLUpdate" value="Opdater">
      </td>
      <td align=center colspan="2">
        &nbsp;
      </td>
    </tr>
  </table>
</form>
</body>
<script language="javascript">
<!--
function updLayout(){
	var form = document.all["ResultatForm"] ;
	form.submit();
}

function fonLoad(){
	var h = 270;
	var w = 250;
	var l = 25;
	if (l>25){
		w += 15;
		l = 25;
	}
	window.resizeTo(w, h+(l*25));
}
// -->
</script>
</html>
