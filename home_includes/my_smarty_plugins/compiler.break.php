<?php
/*
 * Smarty {break} compiler function plugin
 * -------------------------------------------------------------
 * File: compiler.break.php
 * Type: compiler
 * Name: break
 * Purpose: Output break.
 * -------------------------------------------------------------
 */
function smarty_compiler_break($tag_attrs, &$compiler)
{
	return "break;";
}
?>