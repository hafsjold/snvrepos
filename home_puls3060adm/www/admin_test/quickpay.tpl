<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"
	   "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" lang="en_DK" xml:lang="en_Dk">
	<head>
		<title>QuickPay - Det sikre valg til betalingsløsninger</title>
		<meta http-equiv="Content-type" content="text/html;charset=iso-8859-1" />
		<script type="text/javascript" language="javascript" src="/Scripts/calendar.js"></script>
		<script type="text/javascript" language="javascript" src="/Scripts/popup.js"></script>
		<link rel="stylesheet" href="/css/page_secure.css" type="text/css" />
	</head>
	<body>
		<div id="header"><img src="/images/logo.gif" alt="Logo" /></div>
		<div id="menu"><ul id="menu-level-1"><li><a href="/start/index.php" class="iconHome" title="Start">Start</a><ul class="menu-level-2 hide"><li><a href="/start/index.php" class="iconHome" title="Startside">Startside</a></li><li><a href="/start/pw_renew.php" class="iconFOO" title="Forny adgangskode">Forny adgangskode</a></li><li><a href="/start/driftsstatus.php" class="iconFOO" title="Driftsstatus">Driftsstatus</a></li></ul></li><li><a href="/payments/index.php" class="act iconTransactions" title="Transaktioner">Transaktioner</a><ul class="menu-level-2 show"><li><a href="/payments/index.php" class="iconListview" title="Betalt">Betalt</a></li><li><a href="/payments/index.php?searchType=state&amp;state=3%2C7%2C8" class="iconListview" title="Hævet">Hævet</a></li><li><a href="/payments/index.php?searchType=state&amp;state=2%2C4%2C6%2C8" class="iconListview" title="Fejlet">Fejlet</a></li><li><a href="/payments/recurring.php" class="iconRecurring" title="Abonnementer">Abonnementer</a></li><li><a href="/payments/search.php" class="iconSearch" title="Søg">Søg</a></li></ul></li><li><a href="/tools/terminal.php" class="iconTools" title="Værktøjer">Værktøjer</a><ul class="menu-level-2 hide"><li><a href="/tools/terminal.php" class="tbTerminal" title="Virtuel terminal">Virtuel terminal</a></li><li><a href="/tools/md5.php" class="tbMD5" title="MD5-sum beregning">MD5-sum beregning</a></li></ul></li><li><a href="/statistics/index.php" class="iconStatistic" title="Statistik">Statistik</a><ul class="menu-level-2 hide"><li><a href="/statistics/index.php" class="tbTerminal" title="Omsætning">Omsætning</a></li><li><a href="/statistics/time.php" class="tbMD5" title="Døgngennemsnit">Døgngennemsnit</a></li><li><a href="/statistics/cardtype.php" class="tbMD5" title="Korttyper">Korttyper</a></li></ul></li><li><a href="/config/user/index.php" class="iconSettings" title="Indstillinger">Indstillinger</a><ul class="menu-level-2 hide"><li><a href="/config/user/index.php" class="tbTerminal" title="Generelt">Generelt</a></li><li><a href="/config/user/clearinghouse.php" class="tbMD5" title="Indløsere">Indløsere</a></li><li><a href="/config/user/export.php" class="tbMD5" title="Eksport">Eksport</a></li></ul></li><li><a href="/logout.php" class="iconLock" title="Log ud">Log ud (mha@hafsjold.dk)</a></li></ul></div>
		<div id="content">


<h1>Administration af transaktioner</h1>
<form class="inline" action="/payments/handle.php" method="post" id="translist">
	<input type="hidden" name="legalcap" id="legalCap" value="1024198" />
	<input type="hidden" name="legaldel" id="legalDel" value="" />
	<input type="hidden" name="legalexp" id="legalExp" value="1024198" />
	<table class="spreadSheet" cellspacing="0" cellpadding="0">
		<tr>
			<th class="center" colspan="10"><div class="browselist">Viser <span class="bold">1</span> til <span class="bold">1</span> af <span class="bold">1</span></div></th>
		</tr>
		<tr>
			<th colspan="2">Funktioner</th>
			<th>Transaktion ID</th>
			<th>Ordrenr.</th>
			<th>Beløb</th>
			<th>Valuta</th>
			<th>Kort</th>
			<th>Status</th>
			<th>Dato</th>
			<th>&nbsp;</th>
		</tr>
{while $trans->nextTransList()}		
		<tr onmouseover="this.className='mover';" onmouseout="this.className='';" >
			<th class="left"><input type="checkbox" name="t_id[{$trans->transaction}]" value="{$trans->transaction}" /></th>
			<th class="left">
				<a href="/payments/details.php?id={$trans->transaction}&amp;searchType=word&amp;keyword={$trans->ordernum}&amp;ordernum=1" title="Vis detaljer"><img src="/images/show.gif" alt="Vis detaljer" /></a>
				<a href="{$SCRIPT_NAME}?id={$trans->transaction}&amp;action=capture&amp;searchType=word&amp;keyword={$trans->ordernum}&amp;ordernum=1" title="Gennemfør betaling"><img src="/images/capture.gif" alt="Gennemfør betaling" /></a>
				&nbsp;<a href="{$SCRIPT_NAME}?id={$trans->transaction}&amp;action=capedit&amp;searchType=word&amp;keyword={$trans->ordernum}&amp;ordernum=1" title="Gennemfør betaling med ændret beløb"><img src="/images/capture_amount.gif" alt="Gennemfør betaling med ændret beløb" /></a>
				<a href="{$SCRIPT_NAME}?id={$trans->transaction}&amp;action=reverse&amp;searchType=word&amp;keyword={$trans->ordernum}&amp;ordernum=1" title="Annullér betaling"><img src="/images/reverse.gif" alt="Annullér betaling" /></a>
				<img src="/images/delete_dim.gif" alt="Slet transaktion" />
				<a href="#" onclick="popWin('/popups/time_limit.php', 400, 500);" title="Datoen for hævning af overskrides om 4 dage - klik for at læse mere"><img src="/images/time_ok.gif" alt="Datoen for hævning af overskrides om 4 dage - klik for at læse mere" title="Datoen for hævning af overskrides om 4 dage - klik for at læse mere" /></a>
				
			</th>
			<td class="right">{$trans->transaction}</td>
			<td class="right">{$trans->ordernum}</td>
			<td class="right">{$trans->amount}</td>
			<td class="center">{$trans->currency}</td>
			<td class="center">
				{if $trans->cardtype == 'eDankort'}
				  <img class="card" src="/images/edan.jpg" title="Korttype: eDankort" alt="Korttype: eDankort" />
				{elseif $trans->cardtype == 'Dankort'}
				  <img class="card" src="/images/visadan.jpg" title="Korttype: Dankort" alt="Korttype: Dankort" />
				{/if}
			</td>
			<td>{$trans->statetext}</td>
			<td>{$trans->time|date_format:"%e. %B %G %H:%M:%S"}</td>
			<td class="w100">&nbsp;</td>
		</tr>
{/while}
		<tr>
			<th class="left" colspan="10">
				&nbsp;<img src="/images/arrow_leftup.gif" alt="Capture transaktion" />
				&nbsp;
				<a href="#" onclick="checkAll();">Vælg alle</a> / <a href="#" onclick="uncheckAll();">Fravælg alle</a>
				&nbsp;&nbsp;-&nbsp;&nbsp;
				Med valgte udfør:
				<input type="image" src="/images/capture.gif" name="capture" value="Capture" onclick="return validateCapture();" title="Gennemfør betaling på valgte transaktioner" />
				<input type="image" src="/images/delete.gif" name="delete" value="Slet" onclick="return validateDelete();" title="Slet valgte transaktioner" />
				<input type="image" src="/images/export.gif" name="export" value="Export" onclick="return notEmpty();" title="Exportér valgte transaktioner" />
				&nbsp;&nbsp;|&nbsp;&nbsp;
				<input type="image" src="/images/export_pages.gif" name="export_pages" value="Export" onclick="return confirm('Exportér alle sider?');" title="Exportér alle sider" />
			</th>
		</tr>
		<tr>
			<th class="center" colspan="10"><div class="browselist">Viser <span class="bold">1</span> til <span class="bold">1</span> af <span class="bold">1</span></div></th>
		</tr>
	</table>
</form>
{literal}
<script type="text/javascript" >
<!-- //
function checkAll() {
	var theForm = document.getElementById('translist');
	for (z=0; z < theForm.length; z++) {
		if (theForm[z].type == 'checkbox') {
			theForm[z].checked = true;
		}
	}
}
function uncheckAll() {
	var theForm = document.getElementById('translist');
	for (z=0; z < theForm.length; z++) {
		if (theForm[z].type == 'checkbox') {
			theForm[z].checked = false;
		}
	}
}
function notEmpty() {
	var res = false;
	var theForm = document.getElementById('translist');
	for (z=0; z < theForm.length; z++) {
		if (theForm[z].type == 'checkbox') {
			if (theForm[z].checked == true) {
				res = true;
				break;
			}
		}
	}
	if (res == false) {
		alert('Der skal vælges mindst een transaktion.');
	}
	return res;
}
function validateCapture() {
	var res = notEmpty();
	var legalArr = document.getElementById('legalCap').value.split(',');
	var theForm = document.getElementById('translist');
	if (res == true) {
		for (z=0; z < theForm.length; z++) {
			if (theForm[z].type == 'checkbox' && res == true) {
				if (theForm[z].checked == true) {
					res = false;
					for (i = 0; i < legalArr.length; i++) {
						if (theForm[z].value == legalArr[i]) {
							res = true;
						}
					}
					if (res == false) {
						alert('Det er kun muligt at capture transaktioner med status "Authorized".');
					}
				}
			}
		}
	}
	return res;
}
function validateDelete() {
	var res = notEmpty();
	var legalArr = document.getElementById('legalDel').value.split(',');
	var theForm = document.getElementById('translist');
	if (res == true) {
		for (z=0; z < theForm.length; z++) {
			if (theForm[z].type == 'checkbox' && res == true) {
				if (theForm[z].checked == true) {
					res = false;
					for (i = 0; i < legalArr.length; i++) {
						if (theForm[z].value == legalArr[i]) {
							res = true;
						}
					}
					if (res == false) {
						alert('Det er kun muligt at slette transaktioner med status:\n3: Captured\n5: Reversed\n7: Credited\n8: Credit failed');
					}
				}
			}
		}
	}
	return res;
}
// -->
</script>
{/literal}
</div>
	</body>
</html>

