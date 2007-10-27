<?
  $conn_www = "host=hd21.hafsjold.dk port=5432 sslmode=allow dbname=puls3060 user=www";

  function rewrite_link($link)
  {
	$map = array( 
             "nyhmnu" => "/Nyheder/nyhedermenu.htm",
             "lobmnu" => "/Lob/lobmenu.htm",
             "kalmnu" => "/Kalender/kalendermenu.htm",
             "medmnu" => "/Medlem/Medlem.htm",
             "tramnu" => "/Traening/traeningmenu.htm",
             "velmnu" => "/Velkom/Velkom.php",
             "lnkmnu" => "/Frame/TomMenu.htm"
		   );

		if (ereg ("^/(nyhmnu|lobmnu|kalmnu|medmnu|tramnu|velmnu|lnkmnu)(/.*)\$", $link, $regs)){
   			return "javascript:top.rwlink('" .$link . "');";
		}
		else{
   			return $link;
   		}
  }
?>
