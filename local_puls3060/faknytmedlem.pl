#!/usr/bin/perl

use strict;
use DBI;
use DBD::Pg;
use DBI qw(:sql_types);

my $db_puls = "puls3060";
my $db_regnskab = "regnskab3060";

PbsFakturaForslag();

sub PbsFakturaForslag {
  my $dbh_puls;
  my $dbh_regnskab;
  my $sql_select;
  my $sth_select;
  my $sql_update;
  my $sth_update;
  my $sql_insert_tilpbs;
  my $sth_insert_tilpbs;
  my $sql_insert_head;
  my $sth_insert_head;
  my $sql_insert_linie;
  my $sth_insert_linie;
  my $sql_faktura_601_action;
  my $sth_faktura_601_action;
  my $sql_insert_tblsendqueue;
  my $sth_insert_tblsendqueue;
  my $count =0;
  
  $dbh_puls = DBI->connect( "dbi:Pg:dbname=$db_puls", "pgsql", "", {RaiseError => 1, AutoCommit => 0} )
  					|| die "Database connection not made: $DBI::errstr";

  $dbh_regnskab = DBI->connect( "dbi:Pg:dbname=$db_regnskab", "pgsql", "", {RaiseError => 1, AutoCommit => 0} ) 
  					|| die "Database connection not made: $DBI::errstr";

  $sql_select = <<SQL;
	SELECT tblperson.id as id,
    	   (((tblperson.fornavn)::text || ' '::text) || (tblperson.efternavn)::text) AS navn,
    	   tblperson.adresse,
    	   (((tblperson.postnr)::text || ' '::text) || (tblperson.bynavn)::text) AS pnrby,
    	   tblnytmedlem.id as refid,
    	   tblnytmedlem.indmeldtdato::date as indmeldtdato,
    	   tblnytmedlem.kontingenttildato::date as kontingenttildato,
    	   tblnytmedlem.kontingentkr as kontingentkr
	FROM tblnytmedlem
    LEFT JOIN tblpuls3060medlem ON tblnytmedlem.personid = tblpuls3060medlem.personid
    JOIN tblperson ON tblnytmedlem.personid = tblperson.id
    WHERE tblnytmedlem.action ='p'::bpchar
    ORDER BY tblnytmedlem.id desc;
SQL

  $sth_select = $dbh_puls->prepare( $sql_select );

  $sql_update = <<SQL;
	UPDATE tblnytmedlem
    SET action ='P'::bpchar
    WHERE id = ?;
SQL

  $sth_update = $dbh_puls->prepare( $sql_update );
  
  $sql_insert_tilpbs = <<SQL;
    INSERT INTO pbs.tbltilpbs 
        (
            id, 
            delsystem, 
            leverancetype, 
            sendes_senest
        )
        VALUES(
            nextval('pbs.tblpbs_id_seq'::text), 
            'BSH',
            '0601',
            CURRENT_DATE
        );
SQL
  
  $sth_insert_tilpbs = $dbh_regnskab->prepare( $sql_insert_tilpbs );

  $sql_insert_head = <<SQL;
  	INSERT INTO pbs.tblfak 
  		(
  			tilpbsid, 
  			betalingsdato,
			debitorkonto,
			faknr
  		) 
  		VALUES(
  			currval('pbs.tblpbs_id_seq'::text), 
  			(?::date + interval '14 days'), 
  			?,
  			nextval('pbs.faknr'::text) 
  		);
SQL
  
  $sth_insert_head = $dbh_regnskab->prepare( $sql_insert_head );

  $sql_insert_linie = <<SQL;
  	INSERT INTO pbs.tblfaklin 
  		(
  			fakid, 
  			linnr,
			advistekst,
			advisbelob,
			bogfkonto
  		) 
  		VALUES(
  			currval('pbs.tblfak_id_seq'::text), 
  			?, 
  			?, 
  			?, 
  			?
  		);
SQL
  
  $sth_insert_linie = $dbh_regnskab->prepare( $sql_insert_linie );

  $sql_faktura_601_action = <<SQL;
    SELECT pbs.faktura_601_action ((currval('pbs.tblpbs_id_seq'::text))::integer, 1) AS pbsfilesid;
SQL

  $sth_faktura_601_action = $dbh_regnskab->prepare( $sql_faktura_601_action );
  
  $sql_insert_tblsendqueue = <<SQL;
    INSERT INTO pbs.tblsendqueue (pbsfilesid, onhold) VALUES(?, true);
SQL

  $sth_insert_tblsendqueue = $dbh_regnskab->prepare( $sql_insert_tblsendqueue );


  
  
  my( $id, $navn, $adresse, $pnrby, $refid, $indmeldtdato, $kontingenttildato, $kontingentkr );

  $sth_select->execute();
  $sth_select->bind_columns( undef, \$id, \$navn, \$adresse, \$pnrby, \$refid, \$indmeldtdato, \$kontingenttildato, \$kontingentkr );


  while( $sth_select->fetch() ) {
    eval {
      if ($count++ == 0) {
        $sth_insert_tilpbs->execute();
	  };
      
      $sth_insert_head->bind_param( 1, $indmeldtdato, SQL_VARCHAR );
      $sth_insert_head->bind_param( 2, $id+10000, SQL_INTEGER );
      $sth_insert_head->execute();

      $sth_insert_linie->bind_param( 1, 1, SQL_INTEGER );
      $sth_insert_linie->bind_param( 2, "Kontingent Puls 3060", SQL_VARCHAR );
      $sth_insert_linie->bind_param( 3, $kontingentkr, SQL_INTEGER );
      $sth_insert_linie->bind_param( 4, 15, SQL_INTEGER );
      $sth_insert_linie->execute();

      $sth_insert_linie->bind_param( 1, 2, SQL_INTEGER );
      $sth_insert_linie->bind_param( 2, "for perioden $indmeldtdato til $kontingenttildato for", SQL_VARCHAR );
      $sth_insert_linie->bind_param( 3, 'null', SQL_INTEGER );
      $sth_insert_linie->bind_param( 4, 'null', SQL_INTEGER );
      $sth_insert_linie->execute();

      $sth_insert_linie->bind_param( 1, 3, SQL_INTEGER );
      $sth_insert_linie->bind_param( 2, "$navn", SQL_VARCHAR );
      $sth_insert_linie->bind_param( 3, 'null', SQL_INTEGER );
      $sth_insert_linie->bind_param( 4, 'null', SQL_INTEGER );
      $sth_insert_linie->execute();

      $sth_insert_linie->bind_param( 1, 4, SQL_INTEGER );
      $sth_insert_linie->bind_param( 2, "$adresse", SQL_VARCHAR );
      $sth_insert_linie->bind_param( 3, 'null', SQL_INTEGER );
      $sth_insert_linie->bind_param( 4, 'null', SQL_INTEGER );
      $sth_insert_linie->execute();

      $sth_insert_linie->bind_param( 1, 5, SQL_INTEGER );
      $sth_insert_linie->bind_param( 2, "$pnrby", SQL_VARCHAR );
      $sth_insert_linie->bind_param( 3, 'null', SQL_INTEGER );
      $sth_insert_linie->bind_param( 4, 'null', SQL_INTEGER );
      $sth_insert_linie->execute();

      $sth_insert_linie->bind_param( 1, 6, SQL_INTEGER );
      $sth_insert_linie->bind_param( 2, "Husk EIF-bladet på www.puls3060.dk", SQL_VARCHAR );
      $sth_insert_linie->bind_param( 3, 'null', SQL_INTEGER );
      $sth_insert_linie->bind_param( 4, 'null', SQL_INTEGER );
      $sth_insert_linie->execute();

      $sth_update->bind_param( 1, $refid, SQL_INTEGER );
      $sth_update->execute();

    };
  };

  my $pbsfilesid;

  if ($count > 0) {
    $sth_faktura_601_action->execute();
    $sth_faktura_601_action->bind_columns( undef, \$pbsfilesid );
    $sth_faktura_601_action->fetch();

    $sth_insert_tblsendqueue->bind_param( 1, $pbsfilesid, SQL_INTEGER );
    $sth_insert_tblsendqueue->execute();
    $dbh_regnskab->commit();

    $dbh_regnskab->do( "NOTIFY sendqueue;;" );
    $dbh_regnskab->commit();

    $dbh_puls->commit();
  };

  
  $sth_insert_tilpbs->finish();
  $sth_insert_head->finish();
  $sth_insert_linie->finish();
  $sth_faktura_601_action->finish();
  $sth_insert_tblsendqueue->finish();
  $dbh_regnskab->disconnect();

  $sth_select->finish();
  $sth_update->finish();
  $dbh_puls->disconnect();
};