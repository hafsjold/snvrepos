<?php

  $ch = curl_init("https://www.puls3060.dk/Velkom/maalsaetning.htm");
  $fp = fopen("/tmp/maalsaetning_x2.htm", "w");

  curl_setopt($ch, CURLOPT_FILE, $fp);
  curl_setopt($ch, CURLOPT_HEADER, 0);
  curl_exec($ch);
  curl_close($ch);
  fclose($fp);
?> 
