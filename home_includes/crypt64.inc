<?php

define("CRYPTKEY", "UD43Pj5L6scVZqv6pUYnAXPMClWUvgjUDUi");    
function encrypt64($plain_text, $key = CRYPTKEY) 
{
    /* Open module, and create IV */ 
    $td = mcrypt_module_open('des', '', 'ecb', '');
    $key = substr($key, 0, mcrypt_enc_get_key_size($td));
    $iv_size = mcrypt_enc_get_iv_size($td);
    $iv = mcrypt_create_iv($iv_size, MCRYPT_RAND);
	$c_t64 = '';

    /* Initialize encryption handle */
    if (mcrypt_generic_init($td, $key, $iv) != -1) {

        /* Encrypt data */
        $c_t = mcrypt_generic($td, $plain_text);
       $c_t64 = base64_encode($c_t);

        /* Clean up */
        mcrypt_generic_deinit($td);
        mcrypt_module_close($td);
    }
	return $c_t64;
}

function decrypt64($encrypted_text, $key = CRYPTKEY) 
{
    /* Open module, and create IV */ 
    $td = mcrypt_module_open('des', '', 'ecb', '');
    $key = substr($key, 0, mcrypt_enc_get_key_size($td));
    $iv_size = mcrypt_enc_get_iv_size($td);
    $iv = mcrypt_create_iv($iv_size, MCRYPT_RAND);
	$p_t = '';
	
    /* Initialize encryption handle */
    if (mcrypt_generic_init($td, $key, $iv) != -1) {

        /* Decrypt data  */
        $c_t = base64_decode($encrypted_text);		
        $p_t = mdecrypt_generic($td, $c_t);
        $p_t = rtrim($p_t, "\0");     

        /* Clean up */
        mcrypt_generic_deinit($td);
        mcrypt_module_close($td);
    }
	return $p_t;
}
?> 