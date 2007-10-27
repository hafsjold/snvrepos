<?php
require ("quickpay.php");

class quickpay_puls3060 extends quickpay {
   function quickpay_puls3060()
   {
        // Class Constructor.
        $this->quickpay();
		$this->set_md5checkword('8iqW6acj5d88xsJ46249h3wG1b28APgn7QD34X71UIV1NEH3pmz7Bkf9utl63Z5R');
		$this->set_merchant('29349444');
		$this->set_posc('K00500K00130');
		$this->set_currency('DKK');
   }
}
?> 