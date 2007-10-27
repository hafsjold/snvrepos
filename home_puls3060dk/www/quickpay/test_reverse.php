<?php
require ("quickpay_puls3060.php");

    $qpstatText = array(
                "000" => "Godkendt",
                "001" => "Afvist af PBS",
                "002" => "Kommunikations fejl",
                "003" => "Kort udløbet",
                "004" => "Status er forkert (Ikke autoriseret)",
                "005" => "Autorisation er forældet",
                "006" => "Fejl hos PBS",
                "007" => "Fejl hos QuickPay",
                "008" => "Fejl i parameter sendt til QuickPay"
                );


//    *****************************
//    *** To authorize a transaction: ***
 
    $eval = false;
    $qp = new quickpay_puls3060;
    // Set values
    $qp->set_msgtype('1420');
    $qp->set_transaction('664222');
    $eval = $qp->reverse();
    
    if ($eval) {
        if ($eval['qpstat'] === '000') {
            // The authorization was completed
            echo 'Authorization: ' . $qpstatText["" . $eval['qpstat'] . ""] . '<br />';
            echo "<pre>";
            var_dump($eval);
            echo "</pre>";
        } else {
            // An error occured with the authorize
            echo 'Authorization: ' . $qpstatText["" . $eval['qpstat'] . ""] . '<br />';
            echo "<pre>";
            var_dump($eval);
            echo "</pre>";
        }
    } else {
        // Communication error
            echo 'Communication error:<br />';
            echo "<pre>";
            var_dump($eval);
            echo "</pre>";
    }
 
?>
