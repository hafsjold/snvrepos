<?php
    setlocale (LC_TIME, "da_DK.ISO8859-1");
    
    //include_once("mailFunctions.inc");  

    function CheckEmail($email) { 
        list($user, $domain) = split("@", $email, 2); 
        getmxrr($domain, $mxlist); 
        $return = array(false, "No email."); 
        
        foreach($mxlist as $mx) { 
            $fp = fsockopen($mx, 25, $errno, $errstr, 20); 
            if(!$fp) continue; 
            
            socket_set_blocking($fp, false); 
            $s = 0; $c = 0; 
            $out = ""; 
            
            do { 
                $out = fgets($fp, 2500); 
                if(ereg("^220", $out)) { 
                    $s = 0; 
                    $out = ""; 
                    $c++; 
                } else { 
                    if(($c>0) && ($out=="")) break; 
                    else $s++; 
                } 
                if($s==9999) break; 
            } 
            
            while($out==""); 
            socket_set_blocking($fp, true); 
            
            fputs($fp, "HELO\n" ); 
            $out = fgets($fp, 3000); 
            
            fputs($fp, "MAIL FROM: mha@puls3060.dk\n" ); 
            $out = fgets($fp, 3000); 
            
            fputs($fp, "RCPT TO: $email\n"); 
            $out = fgets($fp, 3000); 
            
            if(ereg("^250", $out)) { 
                $return = array(true,$out); 
            } else { 
                $return = array(false,$out); 
            } 
            
            fputs($fp, "quit\n"); 
            fclose($fp); 
            
            if($return[0]) break; 
        } 
        
        return $return; 
    } 

    
    $Addr = "jh_hafsjold@hotmail.com";
	$Level = 5;
	$Timeout = 15000;

    $ret = CheckEMail($Addr); 

    //$result = MailVal($Addr, $Level, $Timeout);



?>
