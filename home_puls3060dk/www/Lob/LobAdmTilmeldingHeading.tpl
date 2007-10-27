{config_load file="puls3060.conf" section="LobAdmTilmelding"}
<div class="pagepuls3060" style="padding-top: 0px;padding-right: 5px;">
  <table align="right" cellpadding="0" cellspacing="0" border="0">
    <tr>
      {if $step == 1}
        <td class="astep">
   	  {else}
        <td class="step">
	  {/if}
        &nbsp;&nbsp;{#step1#}&nbsp;&nbsp;</td>
      <td>&nbsp;&nbsp;</td>

      {if $step == 2}
        <td class="astep">
   	  {else}
        <td class="step">
	  {/if}
        &nbsp;&nbsp;{#step2#}&nbsp;&nbsp;</td>
      <td>&nbsp;&nbsp;</td>

      {if $step == 3}
	    <td style="background: #ffffff url(../gfx/done_aleft.gif) no-repeat right center; width:11px;">&nbsp;</td>
	    <td class="astepok">&nbsp;{#step3#}&nbsp;&nbsp;</td>
	    <td style="background: #ffffff url(../gfx/done_aright.gif) no-repeat left center; width:11px;">&nbsp;</td>
   	  {else}
	    <td style="background: #ffffff url(../gfx/done_left.gif) no-repeat right center; width:11px;">&nbsp;</td>
	    <td class="stepok">&nbsp;{#step3#}&nbsp;&nbsp;</td>
	    <td style="background: #ffffff url(../gfx/done_right.gif) no-repeat left center; width:11px;">&nbsp;</td>
	  {/if}

    </tr>
  </table>
  <h3 style="float: left; vertical-align: bottom; margin-top: 15px;">
  {if $step == 1}
     {#step1#}
  {elseif $step == 2}
     {#step2#}
  {elseif $step == 3}
     {#step3#}
  {/if}
  </h3>
</div>
