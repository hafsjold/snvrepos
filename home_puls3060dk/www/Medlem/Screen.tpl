<html>
<head>
<link href="/css/puls3060.css" rel="stylesheet" type="text/css">
<title>L&oslash;bsTilmelding</title>
	<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
</head>
<body bgcolor="#FFFFFF" text="#000000">	
<div class="pagepuls3060">
  <form method="post" action="{$SCRIPT_NAME}?DoWhat=2">

  {foreach from=$fields item=curr_field}
    {$medlem->fetchSmarty($curr_field)}
  {/foreach}

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
