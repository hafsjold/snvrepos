<?php
/*
 * Smarty {while} compiler function plugin
 * -------------------------------------------------------------
 * File: compiler.while.php
 * Type: compiler
 * Name: break
 * Purpose: Output while.
 * -------------------------------------------------------------
 */
function smarty_compiler_while($tag_args, &$compiler)
{
	$compiler->_push_tag('while');
    
    /* Tokenize args for 'while' tag. */
    preg_match_all('~(?>
            ' . $compiler->_obj_call_regexp . '(?:' . $compiler->_mod_regexp . '*)? | # valid object call
            ' . $compiler->_var_regexp . '(?:' . $compiler->_mod_regexp . '*)?    | # var or quoted string
            \-?0[xX][0-9a-fA-F]+|\-?\d+(?:\.\d+)?|\.\d+|!==|===|==|!=|<>|<<|>>|<=|>=|\&\&|\|\||\(|\)|,|\!|\^|=|\&|\~|<|>|\||\%|\+|\-|\/|\*|\@    | # valid non-word token
            \b\w+\b                                                        | # valid word token
            \S+                                                           # anything else
            )~x', $tag_args, $match);

    $tokens = $match[0];

    if(empty($tokens)) {
        $_error_msg .= 'while statement requires arguments'; 
        $compiler->_syntax_error($_error_msg, E_USER_ERROR, __FILE__, __LINE__);
    }
        
            
    // make sure we have balanced parenthesis
    $token_count = array_count_values($tokens);
    if(isset($token_count['(']) && $token_count['('] != $token_count[')']) {
        $compiler->_syntax_error("unbalanced parenthesis in if statement", E_USER_ERROR, __FILE__, __LINE__);
    }

    $is_arg_stack = array();

    for ($i = 0; $i < count($tokens); $i++) {

        $token = &$tokens[$i];

        switch (strtolower($token)) {
            case '!':
            case '%':
            case '!==':
            case '==':
            case '===':
            case '>':
            case '<':
            case '!=':
            case '<>':
            case '<<':
            case '>>':
            case '<=':
            case '>=':
            case '&&':
            case '||':
            case '|':
            case '^':
            case '&':
            case '~':
            case ')':
            case ',':
            case '+':
            case '-':
            case '*':
            case '/':
            case '@':
                break;

            case 'eq':
                $token = '==';
                break;

            case 'ne':
            case 'neq':
                $token = '!=';
                break;

            case 'lt':
                $token = '<';
                break;

            case 'le':
            case 'lte':
                $token = '<=';
                break;

            case 'gt':
                $token = '>';
                break;

            case 'ge':
            case 'gte':
                $token = '>=';
                break;

            case 'and':
                $token = '&&';
                break;

            case 'or':
                $token = '||';
                break;

            case 'not':
                $token = '!';
                break;

            case 'mod':
                $token = '%';
                break;

            case '(':
                array_push($is_arg_stack, $i);
                break;

            case 'is':
                /* If last token was a ')', we operate on the parenthesized
                   expression. The start of the expression is on the stack.
                   Otherwise, we operate on the last encountered token. */
                if ($tokens[$i-1] == ')')
                    $is_arg_start = array_pop($is_arg_stack);
                else
                    $is_arg_start = $i-1;
                /* Construct the argument for 'is' expression, so it knows
                   what to operate on. */
                $is_arg = implode(' ', array_slice($tokens, $is_arg_start, $i - $is_arg_start));

                /* Pass all tokens from next one until the end to the
                   'is' expression parsing function. The function will
                   return modified tokens, where the first one is the result
                   of the 'is' expression and the rest are the tokens it
                   didn't touch. */
                $new_tokens = $compiler->_parse_is_expr($is_arg, array_slice($tokens, $i+1));

                /* Replace the old tokens with the new ones. */
                array_splice($tokens, $is_arg_start, count($tokens), $new_tokens);

                /* Adjust argument start so that it won't change from the
                   current position for the next iteration. */
                $i = $is_arg_start;
                break;

            default:
                if(preg_match('~^' . $compiler->_func_regexp . '$~', $token) ) {
                        // function call
                        if($compiler->security &&
                           !in_array($token, $compiler->security_settings['IF_FUNCS'])) {
                            $compiler->_syntax_error("(secure mode) '$token' not allowed in if statement", E_USER_ERROR, __FILE__, __LINE__);
                        }
                } elseif(preg_match('~^' . $compiler->_var_regexp . '$~', $token) && (strpos('+-*/^%&|', substr($token, -1)) === false) && isset($tokens[$i+1]) && $tokens[$i+1] == '(') {
                    // variable function call
                    $compiler->_syntax_error("variable function call '$token' not allowed in if statement", E_USER_ERROR, __FILE__, __LINE__);                      
                } elseif(preg_match('~^' . $compiler->_obj_call_regexp . '|' . $compiler->_var_regexp . '(?:' . $compiler->_mod_regexp . '*)$~', $token)) {
                    // object or variable
                   	$token = $compiler->_parse_var_props($token); 
                } elseif(!is_numeric($token)) {
                    // number, skip it
                } else {
                    $compiler->_syntax_error("unidentified token '$token'", E_USER_ERROR, __FILE__, __LINE__);
                }
                break;
        }
    }

	$compiler->register_compiler_function('/while','smarty_compiler_while_end');
    return 'while ('.implode(' ',$tokens).'):';

}

function smarty_compiler_while_end($tag_args, &$compiler) {
        $compiler->_pop_tag('while');
        return 'endwhile;';
}
?>
