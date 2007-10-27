<!-- Clear msie buffer <?=str_repeat(":",500);?> -->
<?
    ini_set("max_execution_time",3000000);

    setlocale (LC_TIME, "da_DK.ISO8859-1");

    function DatoFormat($Sender)
	{
      return(strtolower(strftime("%A den %d. %B kl. %H:%M", strtotime($Sender))));
	}

    if (isset($_REQUEST['MaillistId'])) 
      $MaillistId=$_REQUEST['MaillistId'];
    else
      $MaillistId = 999;

    $Subject = "Puls 3060 Info mail";

    //error_reporting(E_ALL);
    
    include('class.html.mime.mail.inc');
    include('class.smtp.inc');
    include('dbFunctions.inc');  
	define('CRLF', "\r\n", TRUE);

    include_once("conn.inc");  
    $dbLink = pg_connect($conn_www);
	
	if ($MaillistId == 998) {
	  $Query="SELECT opretmailliste('Infomailliste', 2, 0) AS MaillistId;";
	  $dbResult = pg_query($dbLink, $Query);
      if ($row = pg_fetch_array($dbResult, NULL , PGSQL_ASSOC)) {
        $MaillistId = $row["maillistid"];
	  }
	}

	$Query="
	  SELECT
	    tblMaillistLinie.PersonId As PersonId, 
		tblMailadresse.MailAdr As MailAdr, 
		tblPerson.Fornavn As Fornavn, 
		(tblPerson.Fornavn || ' ' || tblPerson.Efternavn) AS Navn 
	  FROM tblMaillistLinie
		INNER JOIN tblPerson ON tblMaillistLinie.PersonId = tblPerson.Id
		INNER JOIN tblMailadresse ON tblMaillistLinie.PersonId = tblMailadresse.PersonId
	  WHERE tblMaillistLinie.MaillistId=$MaillistId
	  AND tblPerson.nomail <> 1
	  ORDER BY tblMaillistLinie.Id
		";
	$dbResult = pg_query($dbLink, $Query);

	while($row = pg_fetch_array($dbResult, NULL , PGSQL_ASSOC))
	{
		$PersonId = $row["personid"];
		$MailAdr = $row["mailadr"];
		$Fornavn = $row["fornavn"];
		$Navn = $row["navn"];
		echo("$Navn  ($MailAdr)<br>");
        flush();

	    $mail = new html_mime_mail(array('X-Mailer: Html Mime Mail Class'));
        $background = $mail->get_file($_SERVER['DOCUMENT_ROOT'] . '/images/puls3060logo.gif');
        $text = $mail->get_file('infomail.txt');
        $html = $mail->get_file('infomail.htm');

        $text = addslashes($text); 
        eval ("\$text = \"$text\";");
        $text = stripslashes($text); 
        
        $html = addslashes($html); 
        eval ("\$html = \"$html\";");
        $html = stripslashes($html); 

	
        $mail->add_html($html, $text);
        $mail->add_html_image($background, '/images/puls3060logo.gif', 'image/gif');
	    
        if(!$mail->build_message())
            die('Failed to build email');

		$To = 'To: "' . $Navn . '" <' . $MailAdr . '>';
		$send_params = array(
							'from'			=> 'mha@puls3060.dk',		// The return path
							'recipients'	=> array(
														$MailAdr
														,'mailcopy@puls3060.dk'
													),
							'headers'		=> array(
														'From: "Puls 3060" <mha@puls3060.dk>',
														$To,
														"Subject: " . $Subject
													)
					 	    );
		$params = array(
						'host' => '192.168.3.99',	// Mail server address
						'port' => 25,				// Mail server port
						'helo' => 'hafsjold.dk',	// Use your domain here.
						'auth' => FALSE,			// Whether to use authentication or not.
						'user' => '',				// Authentication username
						'pass' => ''				// Authentication password
					   );

    	$smtp =& smtp::connect($params);

        $mail->smtp_send($smtp, $send_params);

		unset($mail);
   }
   echo("Alle mails sendt<br>");

?>
