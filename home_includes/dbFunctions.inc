<?
  function getGUID() 
  { 
    // get the current ip, and convert it to its positive long value 
    $long_ip = ip2long($_SERVER['SERVER_ADDR']); 
    if($long_ip < 0) 
      $long_ip += pow(2,32); 

    // get the current microtime and make sure it's a positive 
    // long value 
    $time = microtime(); 
    if($time < 0){ 
      $time += pow(2,32); 
    } 

    // put those strings together 
    $combined = $long_ip . $time; 

    // md5 it and throw in some dashes for easy checking 
    $guid = md5($combined); 
    $guid = substr($guid, 0, 8) . "-" . 
    substr($guid, 8, 4) . "-" . 
    substr($guid, 12, 4) . "-" . 
    substr($guid, 16, 4) . "-" . 
    substr($guid, 20); 

    return $guid; 
  }

  function GetPersonGruppeNavn($PersonGruppeId)
  {
    global $conn_www;
    if (!isset($PersonGruppeId))
      return("");
    $dbLink = pg_connect($conn_www);
    $Query="SELECT GruppeNavn FROM tblPersonGruppe WHERE Id=$PersonGruppeId";
    if ($dbResult = pg_query($dbLink, $Query))
      if($row = pg_fetch_array($dbResult, NULL , PGSQL_ASSOC))
        return($row["gruppenavn"]);
    return("");
  }

  function GetPersonNavn($PersonId)
  {
    global $conn_www;
    if (!isset($PersonId))
      return("");
    $dbLink = pg_connect($conn_www);
    $Query="SELECT (Fornavn || ' ' || Efternavn) AS Navn FROM tblPerson WHERE Id=$PersonId";
    if ($dbResult = pg_query($dbLink, $Query))
      if($row = pg_fetch_array($dbResult, NULL , PGSQL_ASSOC))
        return($row["navn"]);
    return("");
  }

  function InsertPersonGruppe($GruppeNavn, $Privat)
  {
    global $conn_www;
    $dbLink = pg_connect($conn_www);
    $Query="INSERT INTO tblPersonGruppe (GruppeNavn, Privat) VALUES('$GruppeNavn', $Privat)";
    if ($dbResult = pg_query($dbLink, $Query))
      return(pg_insert_id($dbLink, "tblpersongruppe_id_seq"));
    return(0);
  }

  function ClearPrivatPersonGruppe($PersonGruppeId)
  {
    global $conn_www;
    $dbLink = pg_connect($conn_www);
    if ($dbResult = pg_query($dbLink, "SELECT Privat FROM tblPersonGruppe WHERE Id=$PersonGruppeId"))
      if($row = pg_fetch_array($dbResult, NULL , PGSQL_ASSOC))
        if ($row["privat"])
		{
    	  pg_query($dbLink, "DELETE FROM tblPersonRef WHERE PersonGruppeId=$PersonGruppeId");
    	  return($PersonGruppeId);
    	}
    return(InsertPersonGruppe("", 1));
  }

  
  function InsertPersonIntoPersonGruppe($PersonGruppeId, $PersonId, $FunktionId)
  {
    global $conn_www;
    $dbLink = pg_connect($conn_www);
    if ($dbResult = pg_query($dbLink, "SELECT COUNT(*) AS antal FROM tblPerson WHERE Id=$PersonId"))
      if($row = pg_fetch_array($dbResult, NULL , PGSQL_ASSOC))
	    if ($row["antal"] != 1) return(-1);
    if ($dbResult = pg_query($dbLink, "SELECT COUNT(*) AS antal FROM tblPersonGruppe WHERE Id=$PersonGruppeId"))
      if($row = pg_fetch_array($dbResult, NULL , PGSQL_ASSOC))
	    if ($row["antal"]!=1) return(-2);
    if ($FunktionId)
      if ($dbResult = pg_query($dbLink, "SELECT COUNT(*) AS antal FROM tblFunktion WHERE Id=$FunktionId"))
        if($row = pg_fetch_array($dbResult, NULL , PGSQL_ASSOC))
	      if ($row["antal"]!=1) return(-3);
    $Query="INSERT INTO tblPersonRef (PersonGruppeId, PersonId, FunktionId) VALUES($PersonGruppeId, $PersonId, $FunktionId)";
    if ($dbResult = pg_query($dbLink, $Query))
      return(pg_insert_id($dbLink, "tblpersonref_id_seq"));

    return(-4);
  }

  function CopyPersonGruppe($PersonGruppeId)
  {
    global $conn_www;
    $dbLink = pg_connect($conn_www);
    if ($dbResult = pg_query($dbLink, "SELECT GruppeNavn FROM tblPersonGruppe WHERE Id=$PersonGruppeId"))
      if($row = pg_fetch_array($dbResult, NULL , PGSQL_ASSOC))
        if ($dbResult = pg_query($dbLink, "INSERT INTO tblPersonGruppe (GruppeNavn, Privat) VALUES('Kopi-" . $row["gruppenavn"] . "', 1)"))
        {
		  $CopyPersonGruppeId = pg_insert_id($dbLink, "tblpersongruppe_id_seq");
          if ($dbResult = pg_query($dbLink, "SELECT PersonId, FunktionId FROM tblPersonRef WHERE PersonGruppeId=$PersonGruppeId"))
          {
            while ($row = pg_fetch_array($dbResult, NULL , PGSQL_ASSOC))
              InsertPersonIntoPersonGruppe($CopyPersonGruppeId, $row["personid"], $row["funktionid"]);
            return($CopyPersonGruppeId);
		  }
    	}
    return(-1);
  }

  function pg_insert_id($dbLink, $Sequence)
  {
    if ($dbResult = pg_query($dbLink, "select currval('" . $Sequence . "') as insert_id"))
      if($row = pg_fetch_array($dbResult, NULL , PGSQL_ASSOC))
        return($row["insert_id"]);
    return(0);
  }

  function get_persongruppe_id($get_persongruppe_id_parm)
  {
    global $conn_www;
    $dbLink = pg_connect($conn_www);
    if ($dbResult = pg_query($dbLink, "SELECT get_persongruppe_id(" . $get_persongruppe_id_parm . ")"))
      if($row = pg_fetch_array($dbResult, NULL , PGSQL_ASSOC))
        if ($row["get_persongruppe_id"])
		{
    	  return $row["get_persongruppe_id"];
    	}
    return 0;
  }
?>
