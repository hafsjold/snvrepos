//<?php
// Password to be encrypted for a .htpasswd file
////$clearTextPassword = 'xcvf345';

// Encrypt password
////$password = crypt($clearTextPassword, base64_encode($clearTextPassword));

// Print encrypted password
//echo $password;
//?>
<?php
	include_once("crypt64.inc");  
    /* Data */
     $plain_text1 = 'Test1234';
    $encrypted_text1 = encrypt64($plain_text1);
	$encrypted_text1 = 'bGEdFCJvCSM=';
	$plain_text2 = decrypt64($encrypted_text1);
	
    if (strncmp($plain_text2, $plain_text1, strlen($plain_text1)) == 0) {
        echo "ok\n";
    } else {
        echo "error\n";
    }
?>