<?php
require ("quickpay.php");

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
    $qp = new quickpay;
    // Set values
    $qp->set_msgtype('1100');
    $qp->set_md5checkword('8iqW6acj5d88xsJ46249h3wG1b28APgn7QD34X71UIV1NEH3pmz7Bkf9utl63Z5R');
    $qp->set_merchant('29349444');
    $qp->set_posc('K00500K00130');
    $qp->set_cardnumber('4571318320843714');
    $qp->set_expirationdate('1203');
    $qp->set_cvd('387');
    $qp->set_ordernum('123462'); // MUST at least be of length 4
    $qp->set_amount('100');
    $qp->set_currency('DKK');
    // Set some custom variables
    $qp->add_customVars('mha1', '1234');
    $qp->add_customVars('mha2', 'abcd');
    // Commit the authorization
    $eval = $qp->authorize();
    
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
