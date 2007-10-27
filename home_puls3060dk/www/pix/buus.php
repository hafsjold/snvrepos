<?php
//*********************************************************************
//  http://192.168.3.111/pix/buus.php?host=fr&net=101&ip=83.93.127.147
//*********************************************************************
    if ($_REQUEST['host'])
		$host = $_REQUEST['host'];
	else
		$host = "undefined";
	
    if ($_REQUEST['net'])
		$net = $_REQUEST['net'];
	else
		$net = "undefined";

    if ($_REQUEST['ip'])
		$ip = $_REQUEST['ip'];
	else
		$ip = "undefined";

    $data1 = "";
	$data1 .= "enable password CspGSaCtv0jkioVy encrypted\n";
	$data1 .= "passwd 2KFQnbNIdI.2KYOU encrypted\n";
	$data1 .= "hostname $host\n";
	$data1 .= "domain-name buusjensen.dk\n";
	$data1 .= "clock timezone CEST 1\n";
	$data1 .= "clock summer-time CEDT recurring last Sun Mar 2:00 last Sun Oct 3:00\n";
	$data1 .= "names\n";
	$data1 .= "name 192.168.10.0 buus-in\n";
	$data1 .= "name 80.160.182.74 buus-out\n";
	$data1 .= "pdm location buus-in 255.255.255.0 outside\n";
	$data1 .= "pdm location buus-out 255.255.255.255 outside\n";
	$data1 .= "\n";
	$data1 .= "http 0.0.0.0 0.0.0.0 outside\n";
	$data1 .= "\n";
	$data1 .= "access-list outside_access_in permit ip buus-in 255.255.255.0 10.169.$net.0 255.255.255.0\n";
	$data1 .= "access-list inside_nat0_outbound permit ip 10.169.$net.0 255.255.255.0 buus-in 255.255.255.0\n";
	$data1 .= "access-list outside_cryptomap_10 permit ip 10.169.$net.0 255.255.255.0 buus-in 255.255.255.0\n";
	$data1 .= "\n";
	$data1 .= "nat (inside) 0 access-list inside_nat0_outbound\n";
	$data1 .= "access-group outside_access_in in interface outside\n";
	$data1 .= "\n";
	$data1 .= "crypto ipsec transform-set ESP-3DES-MD5 esp-3des esp-md5-hmac\n";
	$data1 .= "crypto map outside_map 10 ipsec-isakmp\n";
	$data1 .= "crypto map outside_map 10 match address outside_cryptomap_10\n";
	$data1 .= "crypto map outside_map 10 set peer 80.160.182.74\n";
	$data1 .= "crypto map outside_map 10 set transform-set ESP-3DES-MD5\n";
	$data1 .= "crypto map outside_map interface outside\n";
	$data1 .= "isakmp enable outside\n";
	$data1 .= "isakmp key Parkalle112 address 80.160.182.74 netmask 255.255.255.255 no-xauth no-config-mode\n";
	$data1 .= "isakmp identity address\n";
	$data1 .= "isakmp keepalive 60 30\n";
	$data1 .= "isakmp policy 10 authentication pre-share\n";
	$data1 .= "isakmp policy 10 encryption 3des\n";
	$data1 .= "isakmp policy 10 hash md5\n";
	$data1 .= "isakmp policy 10 group 2\n";
	$data1 .= "isakmp policy 10 lifetime 86400\n";
	$data1 .= "\n";
	$data1 .= "\n";
	$data1 .= "ntp server 194.239.134.10 source outside prefer\n";
	$data1 .= "dhcpd dns 192.168.10.7 193.162.159.194\n";
	$data1 .= "dhcpd wins 192.168.10.7 \n";

	$filename1 = "buus/pix501_" . $host . "_" . $net;
	if (!$handle1 = fopen($filename1, 'w')) {
	      echo "Cannot open file ($filename1)";
	      exit;
	}
	// Write $data1 to our opened file.
	if (fwrite($handle1, $data1) === FALSE) {
	    echo "Cannot write to file ($filename1)";
	    exit;
	}
	fclose($handle1);


    $data2 = "";
	$data2 .= "names\n";
	$data2 .= "name 10.169.$net.0 $host-in\n";
	$data2 .= "name $ip $host-out\n";
	$data2 .= "\n";
	$data2 .= "pdm location $host-in 255.255.255.0 outside\n";
	$data2 .= "pdm location $host-out 255.255.255.255 outside\n";
	$data2 .= "\n";
	$data2 .= "access-list inside_nat0_outbound line 3 permit ip 192.168.10.0 255.255.255.0  10.169.$net.0 255.255.255.0\n";
	$data2 .= "nat (inside) 0 access-list inside_nat0_outbound\n";
	$data2 .= "access-list outside_cryptomap_" . $net . "0 permit ip 192.168.10.0 255.255.255.0  10.169.$net.0 255.255.255.0\n";

	$data2 .= "crypto map outside_map " . $net . "0 ipsec-isakmp\n";
	$data2 .= "crypto map outside_map " . $net . "0 match address outside_cryptomap_" . $net . "0\n";
	$data2 .= "crypto map outside_map " . $net . "0 set peer $ip\n";
	$data2 .= "crypto map outside_map " . $net . "0 set transform-set ESP-3DES-MD5\n";

	$data2 .= "crypto map outside_map interface outside\n";
	$data2 .= "\n";
	$data2 .= "isakmp key Parkalle112 address $ip netmask 255.255.255.255 no-xauth  no-config-mode\n";
	$data2 .= "\n";
	$data2 .= "access-list outside_access_in line 5 permit ip 10.169.$net.0 255.255.255.0  192.168.10.0 255.255.255.0\n";
	$data2 .= "access-group outside_access_in in interface outside\n";

	$filename2 = "buus/pix506_" . $host . "_" . $net;
	if (!$handle2 = fopen($filename2, 'w')) {
	      echo "Cannot open file ($filename2)";
	      exit;
	}
	// Write $data2 to our opened file.
	if (fwrite($handle2, $data2) === FALSE) {
	    echo "Cannot write to file ($filename2)";
	    exit;
	}
	fclose($handle2);

    $httpdata = "";
    $httpdata .= "host => $host\n";
    $httpdata .= "net ==> $net\n";
    $httpdata .= "ip ===> $ip\n\n";
    $httpdata .= "PIX Command:\n";
    $httpdata .= "configure http://192.168.3.111/pix/$filename1\n";
    $httpdata .= "configure http://62.242.212.133/pix/$filename2\n\n";
    $httpdata .= "Content of $filename1:\n\n";
    $httpdata .= "$data1\n\n\n";
    $httpdata .= "Content of $filename2:\n\n";
    $httpdata .= $data2;

    header("Content-type: text/plain");
    header("Content-disposition: inline; filename=pix$net");
    header("Content-length: " . strlen($httpdata));
    echo $httpdata;
?>
