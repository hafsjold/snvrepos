  <div class="clslvl1">
    <div class="clslvl2L">
      {$clsdta->html_label}:
    </div>
    <div class="clslvl2M">
      <input name="{$clsdta->html_name}" type="{$clsdta->html_type}" maxlength="{$clsdta->html_maxlength}" id="{$clsdta->html_id}" size="{$clsdta->html_size}" value="{$clsdta->html_value}"/>
    </div>
{if $clsdta->html_error_display}
      <div class="clslvl2E">
        {$clsdta->html_error}
      </div>
{/if}
  </div>
