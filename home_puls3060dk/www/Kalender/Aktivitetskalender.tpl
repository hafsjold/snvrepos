<html>
<link href="/css/puls3060.css" rel="stylesheet" type="text/css">
<script language="JavaScript">
<!--
{literal}
function MM_findObj(n, d) { //v3.0
  var p,i,x;  if(!d) d=document; if((p=n.indexOf("?"))>0&&parent.frames.length) {
    d=parent.frames[n.substring(p+1)].document; n=n.substring(0,p);}
  if(!(x=d[n])&&d.all) x=d.all[n]; for (i=0;!x&&i<d.forms.length;i++) x=d.forms[i][n];
  for(i=0;!x&&d.layers&&i<d.layers.length;i++) x=MM_findObj(n,d.layers[i].document); return x;
}
function MM_displayStatusMsg(msgStr) { //v1.0
  status=msgStr;
  document.MM_returnValue = true;
}
function MM_swapImage() { //v3.0
  var i,j=0,x,a=MM_swapImage.arguments; document.MM_sr=new Array; for(i=0;i<(a.length-2);i+=3)
   if ((x=MM_findObj(a[i]))!=null){document.MM_sr[j++]=x; if(!x.oSrc) x.oSrc=x.src; x.src=a[i+2];}
}
function MM_swapImgRestore() { //v3.0
  var i,x,a=document.MM_sr; for(i=0;a&&i<a.length&&(x=a[i])&&x.oSrc;i++) x.src=x.oSrc;
}

function MM_preloadImages() { //v3.0
 var d=document; if(d.images){ if(!d.MM_p) d.MM_p=new Array();
   var i,j=d.MM_p.length,a=MM_preloadImages.arguments; for(i=0; i<a.length; i++)
   if (a[i].indexOf("#")!=0){ d.MM_p[j]=new Image; d.MM_p[j++].src=a[i];}}
}
{/literal}
//-->
</script>
<head>
<title>Aktivitetskalender</title>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
</head>
<body bgcolor="#FFFFFF" text="#000000" onLoad="MM_preloadImages('/image/vpil_f2.gif');">
<div class="pagepuls3060" id="pagepuls3060">
    <p align=left><font color=#003399 size=4>Puls 3060 Aktivitetskalender</font></p>

    <table>
      <tr style=background-color:#FF0000>
      	<font color=#003399> 
        	<th align=center>Dato</th>
        	<th align=left>Aktivitet</th>
        	<th align=left>&nbsp;</th>
        	<th align=left>&nbsp;</th>
        	<th align=left>Kontakt</th>
      	</font>
      </tr>
	{while $akt->nextAktivitet()}
      <tr valign=top  style=background-color:{cycle values="#CFE3CC,#CBE2E4"}>
        <td align=center>
          {$akt->kliniedato|date_format:"%a %d. %b"|lower}
        </td>
        
        <td>
          {if $akt->link == ""}
            {$akt->klinietext|nl2br}
            {if $akt->kliniested != ""}
              <br><br>Sted:<br>{$akt->kliniested|nl2br}
            {/if}
          {else}
            <a href={$akt->link} 
			   {if $akt->newwindow == 1}
			     target=_blank
			   {/if}
			   onClick="return top.rwlink(href);" > 
               {$akt->klinietext|nl2br}
            </a>
            {if $akt->kliniested != ""}
              <br><br>Sted:<br>{$akt->kliniested|nl2br}
            {/if}
          {/if}
        </td>

        <td>
          {if $akt->etilmelding == 1}
            {$akt->ebuttom}
          {else}
            &nbsp;
          {/if}
        </td>
        
        <td>
          {if $akt->etilmelding == 1}
            {$akt->tbuttom}
          {else}
            &nbsp;
          {/if}
        </td>

        <td>
          {$akt->linknavn}<br>
        </td>
        
      </tr>
	{/while}
    </table>
</div>
</body>
</html>
