<html>
<head>
<link href="/css/puls3060.css" rel="stylesheet" type="text/css">
<title>L&oslash;bsTilmelding</title>
	<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
</head>
<body bgcolor="#FFFFFF" text="#000000">	
{include file="Test/LobUserTilmeldingHeading.tpl" step="3"}
<div class="pagepuls3060">
  <h3>{$tilmelding->lobnavn} {$tilmelding->lobdato|date_format:"%A den %e. %B kl %H.%M"}</h3>
  <p> Udfyld nedenst&aring;ende fra dit Dankort for at betale l&oslash;bsafgiften 
    og trykke p&aring; &quot;N&aelig;ste side&quot;.</p>

  <form method="post" action="{$SCRIPT_NAME}?DoWhat=3">
  
  <div class="clslvl1">
    <div class="clslvl2L">
      L&oslash;bsafgift:
    </div>
    <div class="clslvl2M">
      {$tilmelding->lobsafgift}
    </div>
  </div>
  <div class="clslvl1">
    <div class="clslvl2L">
      Kortnummer:
    </div>
    <div class="clslvl2M">
      <input name="cardnumber" type="text" maxlength="16" id="cardnumber" size="25" value="{$tilmelding->cardnumber}"/>
    </div>
    {if $tilmelding->cardnumber_error_display}
      <div class="clslvl2E">
        {$tilmelding->cardnumber_error}
      </div>
    {/if}
  </div>

  <div class="clslvl1">
      <div class="clslvl2L"> Udl&oslash;bsdato: </div>
    <div class="clslvl2M">
	   <select name="expire_month">
	   	<option value="01">jan</option>
	   	<option value="02">feb</option>
	   	<option value="03">mar</option>
	   	<option value="04">apr</option>
	   	<option value="05">maj</option>
	   	<option value="06">jun</option>
	   	<option value="07">jul</option>
	   	<option value="08">aug</option>
	   	<option value="09">sep</option>
	   	<option value="10">okt</option>
	   	<option value="11">nov</option>
	   	<option value="12">dec</option>
	   </select>    
	   /
       <select name="expire_year">
       	<option value="06">2006</option>
	   	<option value="07">2007</option>
	   	<option value="08">2008</option>
	   	<option value="09">2009</option>
	   	<option value="10">2010</option>
	   	<option value="11">2011</option>
	   	<option value="12">2012</option>
	   	<option value="13">2013</option>
	   	<option value="14">2014</option>
	   	<option value="15">2015</option>
	   	<option value="16">2016</option>
	   	<option value="17">2017</option>
	   	<option value="18">2018</option>
	   	<option value="19">2019</option>
	   	<option value="20">2020</option>
	   	<option value="21">2021</option>
       </select>	
	
	</div>
    {if $tilmelding->expirationdate_error_display}
      <div class="clslvl2E">
        {$tilmelding->expirationdate_error}
      </div>
    {/if}
  </div>

  <div class="clslvl1">
      <div class="clslvl2L"> Kontrolcifre: </div>
    <div class="clslvl2M">
      <input name="cvd" type="text" maxlength="3" id="cvd" size="3" value="{$tilmelding->cvd}"/>
    </div>
    {if $tilmelding->cvd_error_display}
      <div class="clslvl2E">
        {$tilmelding->cvd_error}
      </div>
    {/if}
  </div>

  <br/>
  <div class="clslvl1">
    <div class="clslvl2L">
    </div>
    <div class="clslvl2M">
      <input type="submit" Name="cmdOk" value="Næste side" />
    </div>
    <div class="clslvl2R">
      <input type="submit" Name="cmdBack" value="Tilbage" />
    </div>
  </div>
  </form>
</div>
</body>
</html>
