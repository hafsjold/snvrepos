CREATE OR REPLACE FUNCTION "pbs"."udbetaling_til_pbsfiles" (integer) RETURNS integer AS'
DECLARE
     lobnr ALIAS FOR $1;        -- Leveranceidentifikation
     seq integer :=  0;
     rec varchar(128);
     rcd record;
     til record;
     krd record;
     deb record;
     bel record;
     lin record;
     curlin refcursor;
     recnr smallint;
     command varchar(255);
     fortegn integer;
     selector boolean;
     wbilagid integer;
     wtransid integer;
     wpbsfilesid integer;
     wpbsforsendelseid integer;
     wleveranceid integer;
     

     -- Betalingsoplysninger
     h_linie  varchar(38);    -- Tekst til hovedlinie på advis
     belobint integer;

     -- Tællere
     antal042 integer;        -- Antal 042: Antal foranstående 042 records
     belob042 integer;        -- Beløb: Nettobeløb i 042 records
     antal052 integer;        -- Antal 052: Antal foranstående 052 records
     antal022 integer;        -- Antal 022: Antal foranstående 022 records

     antalsek integer;        -- Antal sektioner i leverancen
     antal042tot integer;     -- Antal 042: Antal foranstående 042 records
     belob042tot integer;     -- Beløb: Nettobeløb i 042 records
     antal052tot integer;     -- Antal 052: Antal foranstående 052 records
     antal022tot integer;     -- Antal 022: Antal foranstående 022 records

BEGIN
--  lobnr := 191;

  h_linie       := ''LØBEKLUBBEN PULS 3060'';

  antal042 := 0;
  belob042 := 0;
  antal052 := 0;
  antal022 := 0;

  antalsek := 0;
  antal042tot := 0;
  belob042tot := 0;
  antal052tot := 0;
  antal022tot := 0;

  IF NOT EXISTS( SELECT 1 FROM "pbs".tbltilpbs WHERE id = lobnr) then
    RAISE EXCEPTION  ''Der er ingen pbs pbsforsendelse for id: %'', lobnr;
  END IF;

  IF EXISTS( SELECT 1 FROM "pbs".tbltilpbs WHERE id = lobnr and pbsforsendelseid is not null) then
    RAISE EXCEPTION  ''Pbsforsendelse for id: % er allerede sendt'', lobnr;
  END IF;

  IF NOT EXISTS( SELECT 1 FROM "pbs".tblfak WHERE tilpbsid = lobnr) then
    RAISE EXCEPTION  ''Der er ingen pbs transaktioner for tilpbsid: %'', lobnr;
  END IF;

--      pbsforsendelseid = wpbsforsendelseid


  SELECT INTO til * FROM pbs.tbltilpbs WHERE id = lobnr;
  til.dannelsesdato := CURRENT_DATE;
  if til.bilagdato is null then
    til.bilagdato := CURRENT_DATE;
  end if;

  wpbsforsendelseid := nextval(''pbs.tblpbsforsendelse_id_seq'');
  wleveranceid := nextval(''pbs.leveranceid'');
  INSERT INTO pbs.tblpbsforsendelse (id, delsystem, leverancetype, oprettetaf, oprettet, leveranceid)
                              values(wpbsforsendelseid, til.delsystem, til.leverancetype, ''Fak'', now(), wleveranceid);

  wpbsfilesid := nextval(''pbs.tblpbsfiles_id_seq'');
  INSERT INTO pbs.tblpbsfiles (id, pbsforsendelseid ) values(wpbsfilesid, wpbsforsendelseid);

  SELECT INTO krd * FROM pbs.tblkreditor WHERE delsystem = til.delsystem;

-- Leverance Start - 0601 Betalingsoplysninger
  SELECT INTO rec pbs.write002(krd.datalevnr,      -- Dataleverandørnr.: Dataleverandørens SE-nummer
                               til.delsystem,      -- Delsystem:  Dataleverandør delsystem
                               ''0601'',             -- Leverancetype: 0601 (Betalingsoplysninger)
                               wleveranceid,       -- Leveranceidentifikation: Løbenummer efter eget valg
                               til.dannelsesdato); -- Dato: 000000 eller leverancens dannelsesdato
  seq := seq + 1;
  INSERT INTO "pbs".tblpbsfile (pbsfilesid, seqnr, data) values(wpbsfilesid, seq, rec);

  -- Sektion start – sektion 0110/0115
  SELECT INTO rec pbs.write012( krd.pbsnr,         -- PBS-nr.: Kreditors PBS-nummer
                                krd.sektionnr,     -- Sektionsnr.: 0110/0115 (Betalinger med lang advistekst)
                                krd.debgrpnr,      -- Debitorgruppenr.: Debitorgruppenummer
                                ''PULS3060'',        -- Leveranceidentifikation: Brugers identifikation hos dataleverandør
                                til.dannelsesdato, -- Dato: 000000 eller leverancens dannelsesdato
                                krd.regnr,         -- Reg.nr.: Overførselsregistreringsnummer
                                krd.kontonr,       -- Kontonr.: Overførselskontonummer
                                h_linie );         -- H-linie: Tekst til hovedlinie på advis

  seq := seq + 1;
  INSERT INTO "pbs".tblpbsfile (pbsfilesid, seqnr, data) values(wpbsfilesid, seq, rec);
  antalsek := antalsek + 1;

  -- Opret bogføringsbilag.
  SELECT INTO wbilagid public.opretbilag( til.bilagdato);  -- Dato: leverancens dannelsesdato
  til.bilagid := wbilagid;

  FOR deb IN SELECT
      "pbs".tblfak.id As id,
      "pbs".tblfak.debitorkonto AS kundenr,
      "public".vdebitor.kontonavn AS navn,
      "public".vdebitor.kontoadresse AS adresse,
      "public".vdebitor.kontopostnr AS postnr,
      "pbs".tblfak.faknr AS faknr,
      "pbs".tblfak.betalingsdato AS betalingsdato
    FROM
      "pbs".tblfak
      LEFT OUTER JOIN "public".vdebitor
        ON ("pbs".tblfak.debitorkonto = "public".vdebitor.konto)
    WHERE "pbs".tblfak.debitorkonto IS NOT NULL
    AND   "pbs".tblfak.tilpbsid = lobnr
  LOOP
    -- Debitornavn
    SELECT INTO rec pbs.write022( krd.sektionnr,
                                  krd.pbsnr,     -- PBS-nr.: Kreditors PBS-nummer
                                  ''0240'',        -- Transkode: 0240 (Navn/adresse på debitor)
                                  1,             -- Recordnr.: 001
                                  krd.debgrpnr,  -- Debitorgruppenr.: Debitorgruppenummer
                                  deb.kundenr,   -- Kundenr.: Debitors kundenummer hos kreditor
                                  0,             -- Aftalenr.: 000000000 eller 999999999
                                  deb.navn);     -- Navn: Debitors navn
    antal022 := antal022 + 1;
    antal022tot := antal022tot + 1;
    seq := seq + 1;
    INSERT INTO "pbs".tblpbsfile (pbsfilesid, seqnr, data) values(wpbsfilesid, seq, rec);

    -- Debitoradresse 1/adresse 2
    SELECT INTO rec pbs.write022( krd.sektionnr,
                                  krd.pbsnr,     -- PBS-nr.: Kreditors PBS-nummer
                                  ''0240'',        -- Transkode: 0240 (Navn/adresse på debitor)
                                  2,             -- Recordnr.: 001
                                  krd.debgrpnr,  -- Debitorgruppenr.: Debitorgruppenummer
                                  deb.kundenr,   -- Kundenr.: Debitors kundenummer hos kreditor
                                  0,             -- Aftalenr.: 000000000 eller 999999999
                                  deb.adresse);  -- Adresse 1: Adresselinie 1
    antal022 := antal022 + 1;
    antal022tot := antal022tot + 1;
    seq := seq + 1;
    INSERT INTO "pbs".tblpbsfile (pbsfilesid, seqnr, data) values(wpbsfilesid, seq, rec);

    -- Debitorpostnummer
    SELECT INTO rec pbs.write022( krd.sektionnr,
                                  krd.pbsnr,     -- PBS-nr.: Kreditors PBS-nummer
                                  ''0240'',        -- Transkode: 0240 (Navn/adresse på debitor)
                                  3,             -- Recordnr.: 003
                                  krd.debgrpnr,  -- Debitorgruppenr.: Debitorgruppenummer
                                  deb.kundenr,   -- Kundenr.: Debitors kundenummer hos kreditor
                                  0,             -- Aftalenr.: 000000000 eller 999999999
                                  deb.postnr);   -- Postnr.: Postnummer
    antal022 := antal022 + 1;
    antal022tot := antal022tot + 1;
    seq := seq + 1;
    INSERT INTO "pbs".tblpbsfile (pbsfilesid, seqnr, data) values(wpbsfilesid, seq, rec);

    SELECT into bel fakid, SUM(advisbelob) AS belob
    FROM "pbs".tblfaklin WHERE (fakid = deb.id) GROUP BY fakid ORDER BY fakid;

    -- Forfald betaling
    IF bel.belob > 0 THEN
      fortegn := 1;                           -- Fortegnskode: 1 = træk
      belobint := CAST((bel.belob*100) AS integer);
      belob042 := belob042 + belobint;
      belob042tot := belob042tot + belobint;
    ELSIF bel.belob < 0 THEN
      fortegn := 2;                           -- Fortegnskode: 2 = indsættelse
      belobint := CAST((bel.belob*(-100)) AS integer);
      belob042 := belob042 - belobint;
      belob042tot := belob042tot - belobint;
    ELSE
      fortegn := 0;                           -- Fortegnskode: 0 = 0-beløb
      belobint := 0;
    END IF;
    SELECT INTO rec pbs.write042( krd.sektionnr,
                                  krd.pbsnr,             -- PBS-nr.: Kreditors PBS-nummer
                                  krd.transkodebetaling, -- Transkode: 0280/0285 (Betaling)
                                  krd.debgrpnr,          -- Debitorgruppenr.: Debitorgruppenummer
                                  deb.kundenr,           -- Kundenr.: Debitors kundenummer hos kreditor
                                  0,                     -- Aftalenr.: 000000000 eller 999999999
                                  deb.betalingsdato,
                                  fortegn,
                                  belobint,              -- Beløb: Beløb i øre uden fortegn
                                  deb.faknr);            -- faknr: Information vedrørende betalingen.
    antal042 := antal042 + 1;
    antal042tot := antal042tot + 1;
    seq := seq + 1;
    INSERT INTO "pbs".tblpbsfile (pbsfilesid, seqnr, data) values(wpbsfilesid, seq, rec);

    -- Opret bogføringstrans.
    SELECT INTO wtransid public.oprettrans( wbilagid,                         -- bilagsid
                                           cast(deb.kundenr as smallint),     -- kontonr
                                           ''PBS opkrævning nr '' || deb.faknr, -- bilagstekst
                                           bel.belob);                        -- beløb
    UPDATE "pbs"."tblfak" SET transid = wtransid WHERE id = deb.id;

    recnr := 0;
    FOR lin IN SELECT * FROM pbs.tblfaklin WHERE fakid = deb.id ORDER BY linnr
    LOOP
      recnr := recnr +1;
      -- Tekst til advis
      antal052 := antal052 + 1;
      antal052tot := antal052tot + 1;
      selector = true;
      SELECT INTO rec pbs.write052( krd.sektionnr,
                                    krd.pbsnr,        -- PBS-nr.: Kreditors PBS-nummer
                                    ''0241'',           -- Transkode: 0241 (Tekstlinie)
                                    recnr,            -- Recordnr.: 001-999
                                    krd.debgrpnr,     -- Debitorgruppenr.: Debitorgruppenummer
                                    deb.kundenr,      -- Kundenr.: Debitors kundenummer hos kreditor
                                    0,                -- Aftalenr.: 000000000 eller 999999999
                                    lin.advistekst,   -- Advistekst 1: Tekstlinie på advis
                                    lin.advisbelob,   -- Advisbeløb 1: Beløb på advis
                                    '''',               -- Advistekst 2: Tekstlinie på advis
                                    0.0);             -- Advisbeløb 2: Beløb på advis
      seq := seq + 1;
      INSERT INTO "pbs".tblpbsfile (pbsfilesid, seqnr, data) values(wpbsfilesid, seq, rec);

      -- Opret bogføringstrans.
      if lin.advisbelob <> 0.0 then
        SELECT INTO wtransid public.oprettrans(
          wbilagid,                                       -- bilagsid
          cast(lin.bogfkonto as smallint),                -- kontonr
          ''PBSnr '' || deb.faknr || '' '' || lin.advistekst, -- bilagstekst
          - lin.advisbelob);                              -- beløb
        UPDATE "pbs".tblfaklin SET transid = wtransid WHERE id = lin.id;
      end if;
    END LOOP;
  END LOOP;

-- Sektion slut – sektion 0110
  SELECT INTO rec pbs.write092(krd.pbsnr,     -- PBS-nr.: Kreditors PBS-nummer
                               krd.sektionnr, -- Sektionsnr.: 0110/0115 (Betalinger)
                               krd.debgrpnr,  -- Debitorgruppenr.: Debitorgruppenummer
                               antal042,      -- Antal 042: Antal foranstående 042 records
                               belob042,      -- Beløb: Nettobeløb i 042 records
                               antal052,      -- Antal 052: Antal foranstående 052 records
                               antal022);     -- Antal 022: Antal foranstående 022 records
  seq := seq + 1;
  INSERT INTO "pbs".tblpbsfile (pbsfilesid, seqnr, data) values(wpbsfilesid, seq, rec);

-- Leverance slut - 0601 Betalingsoplysninger
  SELECT INTO rec pbs.write992(krd.datalevnr, -- Dataleverandørnr.: Dataleverandørens SE-nummer
                               krd.delsystem, -- Delsystem:  Dataleverandør delsystem
                               ''0601'',        -- Leverancetype: 0601 (Betalingsoplysninger)
                               antalsek,      -- Antal sektioner: Antal sektioner i leverancen
                               antal042tot,   -- Antal 042: Antal foranstående 042 records
                               belob042tot,   -- Beløb: Nettobeløb i 042 records
                               antal052tot,   -- Antal 052: Antal foranstående 052 records
                               antal022tot);  -- Antal 022: Antal foranstående 022 records
  seq := seq + 1;
  INSERT INTO "pbs".tblpbsfile (pbsfilesid, seqnr, data) values(wpbsfilesid, seq, rec);

  UPDATE pbs.tbltilpbs
  SET dannelsesdato = til.dannelsesdato,
      bilagid = til.bilagid,
      bilagdato = til.bilagdato,
      pbsforsendelseid = wpbsforsendelseid
  WHERE id = lobnr;

  RETURN wpbsforsendelseid;
END;
'LANGUAGE 'plpgsql' STABLE CALLED ON NULL INPUT SECURITY INVOKER;















