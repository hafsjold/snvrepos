<html>
<head>
<link href="/css/puls3060.css" rel="stylesheet" type="text/css">
<title>L&oslash;bsTilmelding</title>
	<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
{literal}
<SCRIPT Language="JavaScript">
<!-- hide script for old browsers	
function eDankortRouter(form) {
		var oHeight = 0;
		var oWidth = 0;		
		var iHeight = 300;		
		var iWidth = 350;		
		var iLeft = 0;		
		var iTop = 0;		
		
		oHeight = top.window.screen.availHeight;		
		oWidth = top.window.screen.availWidth;		
		if(oHeight > 3*iHeight) iHeight = oHeight/3;		
		if(oWidth > 3*iWidth) iWidth = oWidth/3;		
		if(oWidth > iWidth) iLeft = (oWidth - iWidth)/2;		
		if(oHeight > iHeight) iTop = (oHeight - iHeight)/2;		
		
		window.open('', 'router', 'scrollbars=yes, toolbar=no, directories=no, menubar=no, resizable=no, status=yes, width=' + iWidth + ', height=' + iHeight + ', left=' + iLeft + ', top=' + iTop + 'dependent=yes');
		form.target = 'router';		
		form.submit();	
};
function DankortRouter(form) {
		form.submit();	
};
// -- end hiding -->
</SCRIPT>
{/literal}
</head>
<body bgcolor="#FFFFFF" text="#000000">	
{include file="Lob/LobUserTilmeldingHeading.tpl" step="3"}
<div class="pagepuls3060" id="pagepuls3060">
  <h3>{$tilmelding->lobnavn} {$tilmelding->lobdato|date_format:"%A den %e. %B kl %H.%M"}</h3>
  <p>Kontroller at nedenst&aring;ende oplysninger og tryk p&aring; knappen &quot;eDK&quot; eller &quot;DK&quot; 
    for at tilmelde dig.</p>
  <div class="clslvl1">
    <div class="clslvl2L">
      Afdeling:
    </div>
    <div class="clslvl2M">
      {$tilmelding->afdnavn}
    </div>
  </div>
  <div class="clslvl1">
    <div class="clslvl2L">
      Fornavn:
    </div>
    <div class="clslvl2M">
      {$tilmelding->fornavn}
    </div>
  </div>
  <div class="clslvl1">
    <div class="clslvl2L">
      Efternavn:
    </div>
    <div class="clslvl2M">
      {$tilmelding->efternavn} 
    </div>
  </div>
  <div class="clslvl1">
    <div class="clslvl2L">
      Adresse:
    </div>
    <div class="clslvl2M">
      {$tilmelding->adresse}
    </div>
  </div>
  <div class="clslvl1">
    <div class="clslvl2L">
      Postnummer:
    </div>
    <div class="clslvl2M">
      {$tilmelding->postnr}
    </div>
  </div>
  <div class="clslvl1">
    <div class="clslvl2L">
      By:
    </div>
    <div class="clslvl2M">
      {$tilmelding->bynavn}
    </div>
  </div>
  <div class="clslvl1">
    <div class="clslvl2L">
      Telefon
    </div>
    <div class="clslvl2M">
      {$tilmelding->tlfnr}
    </div>
  </div>
  <div class="clslvl1">
    <div class="clslvl2L">
      E-mail:
    </div>
    <div class="clslvl2M">
      {$tilmelding->mailadr}
    </div>
  </div>
  <div class="clslvl1">
    <div class="clslvl2L">
      F&oslash;dsels &Aring;r:
    </div>
    <div class="clslvl2M">
      {$tilmelding->fodtaar}
    </div>
  </div>
  <div class="clslvl1">
    <div class="clslvl2L">
      K&oslash;n:
    </div>
    <div class="clslvl2M">
      {$tilmelding->kon}
    </div>
  </div>
  <br/>
  <div class="clslvl1">
    <div class="clslvl2LBetal">
      L&oslash;bsafgift:
    </div>
    <div class="clslvl2MBetal">
      {$tilmelding->lobsafgift|string_format:"%d DKK"}
    </div>
  </div>

	<br/>

  <form action="https://secure.quickpay.dk/eDankort.php" method="POST" name="QuickPay_eDankort">  	
	<input type="hidden" name="autocapture" value="{$tilmelding->autocapture}"/>  	
	<input type="hidden" name="ordernum" value="{$tilmelding->ordernum}"/>  	
	<input type="hidden" name="merchant" value="{$tilmelding->merchantid}"/>  	
	<input type="hidden" name="amount" value="{$tilmelding->amount}" />  	
	<input type="hidden" name="currency" value="{$tilmelding->currency}"/>  	
	<input type="hidden" name="okpage" value="{$tilmelding->okpage}"/>  	
	<input type="hidden" name="errorpage" value="{$tilmelding->errorpage}"/>  	
	<input type="hidden" name="resultpage" value="{$tilmelding->resultpage}"/>  	
	<input type="hidden" name="md5checkV2" value="{$tilmelding->md5check}"/>  	

	<div class="clslvl1">
      <div class="clslvl2LBetal">
	    Betal med eDankort
      </div>
      <div class="clslvl2MBetal">
        <input type="image" name="cmdOk2" src="../gfx/edan-s.gif" style="border: 0px;" onClick="eDankortRouter(document.QuickPay_eDankort);"/>
      </div>
    </div>  
  
  </form>

  <form method="post" action="{$SCRIPT_NAME}?DoWhat=3" name="Dankort">
    <div class="clslvl1">
      <div class="clslvl2LBetal">
        Betal med Dankort
      </div>
      <div class="clslvl2MBetal">
        <input type="image" name="cmdOk" src="../gfx/dan-s.gif" style="border: 0px;" onClick="DankortRouter(document.Dankort);"/>
      </div>
      <div class="clslvl2R">
        <input type="submit" Name="cmdBack" value="Tilbage" />
        <input type="submit" Name="cmdCancel" value="Fortryd" />
      </div>
    </div>
  </form>

  {if $tilmelding->page_error_display}
  <p><font color="#FF0000">
    {$tilmelding->page_error}
  </font></p>
  {/if}



</div>
</body>
</html>
