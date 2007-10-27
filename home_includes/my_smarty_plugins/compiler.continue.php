<?php
/*
 * Smarty {continue} compiler function plugin
 * -------------------------------------------------------------
 * File: compiler.continue.php
 * Type: compiler
 * Name: break
 * Purpose: Output break.
 * -------------------------------------------------------------
 */
function smarty_compiler_continue($tag_attrs, &$compiler)
{
	return "continue;";
}
?>