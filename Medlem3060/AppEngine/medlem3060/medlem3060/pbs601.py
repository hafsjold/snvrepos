# coding=iso-8859-15 
from google.appengine.ext import webapp
from google.appengine.ext import db 
from google.appengine.ext.webapp import template

from models import UserGroup, User, NrSerie, Kreditor, Kontingent, Tilpbs, Fak, Sftp, Infotekst, Sysinfo, Menu, MenuMenuLink, Medlemlog, Person
from datetime import datetime, date, timedelta
import logging
import os

def lpad (oVal, Length, PadChar):
  Val = '%s' % (oVal)
  return Val.rjust(Length, PadChar)

def rpad (oVal, Length, PadChar):
  Val = '%s' % (oVal)
  return Val.ljust(Length, PadChar)
  
def nextval(nrserie):
  recNrSerie = NrSerie.get_or_insert(nrserie)
  nr = 1
  if recNrSerie.NextNumber:
    nr = recNrSerie.NextNumber
  else:
    recNrSerie.NextNumber = nr
  recNrSerie.NextNumber += 1
  recNrSerie.put()  
  return nr

class Pbs601Error(Exception):
  def __init__(self, value):
    self.value = value
  def __str__(self):
    return repr(self.value)

class clsRstdeb(object):
  def __init__(self, f):
    key = db.Key.from_path('Persons','root','Person','%s' % (f.Nr))
    k = Person.get(key)
    self.Nr = k.Nr
    self.Kundenr = 32001610000000 + k.Nr
    self.Kaldenavn = k.Kaldenavn
    self.Navn = k.Navn
    self.Adresse = k.Adresse
    self.Postnr = k.Postnr
    self.Faknr = f.Faknr
    self.Betalingsdato = f.Betalingsdato
    self.Fradato = f.Fradato
    self.Tildato = f.Tildato
    self.Infotekst = f.Infotekst
    self.Tilpbsid = f.Tilpbsid
    self.Advistekst = None
    self.Belob = f.Advisbelob
    self.OcrString = None

class TestHandler(webapp.RequestHandler):
  def get(self):
    mha = self.write042('0117', '12345678', '1234', '00001', 123456789012345, 121234567, datetime.now(), 0, 150.0, 1234)
    logging.info('QQQQQQQQQQQQQQQQQQQQQQQQQQ write042: %s' % (mha))
    antal = self.kontingent_fakturer_bs1()
    template_values = {}
    path = os.path.join(os.path.dirname(__file__), 'templates/test.html') 
    self.response.out.write(template.render(path, template_values))


  def kontingent_fakturer_bs1(self):
    lobnr = nextval('Tilpbs')
    rootTilpbs = db.Key.from_path('rootTilpbs','root')
    t = Tilpbs.get_or_insert('%s' % (lobnr), parent=rootTilpbs)
    t.Id = lobnr
    t.Delsystem = "BSH"
    t.Leverancetype = "0601"
    t.Udtrukket = datetime.now()
    t.put()

    root = db.Key.from_path('Persons','root')
    qry = db.Query(Kontingent).ancestor(root)
    antal = qry.count()
    for q in qry:
      fakid = nextval('Fak')
      k = db.Key.from_path('rootTilpbs','root','Tilpbs','%s' % (lobnr))
      f = Fak.get_or_insert('%s' % (fakid), parent=k)
      f.Id = fakid
      f.Tilpbsid = lobnr
      dt = datetime.now() + timedelta(days=7)
      f.Betalingsdato = dt.date()
      f.Nr = q.Nr
      f.Faknr = nextval('faknr')
      f.Advisbelob = q.Advisbelob
      f.Infotekst = 11
      f.Fradato = q.Fradato
      f.Tildato = q.Tildato
      f.put()

    return antal

  def faktura_og_rykker_601_action(lobnr):
    rec = ''
    #lintype lin
    #infolintype infolin
    recnr = 0
    fortegn = 0
    wleveranceid = 0
    seq = 0
    # Betalingsoplysninger
    h_linie = 'L�BEKLUBBEN PULS 3060'
    # Tekst til hovedlinie p� advis
    belobint = 0
    advistekst = ''
    advisbelob = 0
    # T�llere
    antal042 = 0
    # Antal 042: Antal foranst�ende 042 records
    belob042 = 0
    # Bel�b: Nettobel�b i 042 records
    antal052 = 0
    # Antal 052: Antal foranst�ende 052 records
    antal022 = 0
    # Antal 022: Antal foranst�ende 022 records
    antalsek = 0
    # Antal sektioner i leverancen
    antal042tot = 0
    # Antal 042: Antal foranst�ende 042 records
    belob042tot = 0
    # Bel�b: Nettobel�b i 042 records
    antal052tot = 0
    # Antal 052: Antal foranst�ende 052 records
    antal022tot = 0
    # Antal 022: Antal foranst�ende 022 records
    # TODO: On Error GoTo Warning!!!: The statement is not translatable 
    #lobnr = 275 '--debug

    tilpbskey = db.Key.from_path('rootTilpbs','root','Tilpbs','%s' % (lobnr))
    rsttil = Tilpbs.get(tilpbskey)
    if not rsttil: 
      raise Pbs601Error("101 - Der er ingen PBS forsendelse for id: " + lobnr)    
 
    if rsttil.Pbsforsendelseid:
       raise Pbs601Error("102 - Pbsforsendelse for id: " + lobnr + " er allerede sendt")
 
    qry = db.Query(Fak).ancestor(tilpbskey)
    if qry.count() == 0:
      raise Pbs601Error("103 - Der er ingen pbs transaktioner for tilpbsid: " + lobnr)

    if not rsttil.Udtrukket:
      rsttil.Udtrukket = DateTime.Now
    if not rsttil.Bilagdato:
      rsttil.Bilagdato = rsttil.Udtrukket
    if not rsttil.Delsystem:
      rsttil.Delsystem = "BS1"  # ????????????????
    if not rsttil.Leverancetype:
      rsttil.Leverancetype = ""
    rsttil.put()

    wleveranceid = nextval("leveranceid")

    qry = Kreditor.all()
    qry.filter("Delsystem =", rsttil.Delsystem)
    rstkrd = qry.fetch(1)[0]

    # Leverance Start - 0601 Betalingsoplysninger
    # - rstkrd.Datalevnr - Dataleverand�rnr.: Dataleverand�rens SE-nummer
    # - rsttil.Delsystem - Delsystem:  Dataleverand�r delsystem
    # - "0601"           - Leverancetype: 0601 (Betalingsoplysninger)
    # - wleveranceid     - Leveranceidentifikation: L�benummer efter eget valg
    # - rsttil!udtrukket - Dato: 000000 eller leverancens dannelsesdato
    rec += write002(rstkrd.Datalevnr, rsttil.Delsystem, "0601", '%s' % (wleveranceid), rsttil.Udtrukket) + "\r\n"

    # Sektion start - sektion 0112/0117
    # -  rstkrd.Pbsnr       - PBS-nr.: Kreditors PBS-nummer
    # -  rstkrd.Sektionnr   - Sektionsnr.: 0112/0117 (Betalinger med lang advistekst)
    # -  rstkrd.Debgrpnr    - Debitorgruppenr.: Debitorgruppenummer
    # -  rstkrd.Datalevnavn - Leveranceidentifikation: Brugers identifikation hos dataleverand�r
    # -  rsttil.Udtrukket   - Dato: 000000 eller leverancens dannelsesdato
    # -  rstkrd.Regnr       - Reg.nr.: Overf�rselsregistreringsnummer
    # -  rstkrd.Kontonr     - Kontonr.: Overf�rselskontonummer
    # -  h_linie            - H-linie: Tekst til hovedlinie p� advis
    rec += write012(rstkrd.Pbsnr, rstkrd.Sektionnr, rstkrd.Debgrpnr, rstkrd.Datalevnavn, rsttil.Udtrukket, rstkrd.Regnr, rstkrd.Kontonr, h_linie) + "\r\n"
    antalsek++

    qry = db.Query(Fak).ancestor(tilpbskey)
    rstfaks = qry.fetch()

    for rstfak in rstfaks:
      rstdeb = clsRstdeb(rstfak)
      
      # Debitornavn
      # - rstkrd.Sektionnr -
      # - rstkrd.Pbsnr     - PBS-nr.: Kreditors PBS-nummer
      # - "0240"           - Transkode: 0240 (Navn/adresse p� debitor)
      # - 1                - Recordnr.: 001
      # - rstkrd.Debgrpnr  - Debitorgruppenr.: Debitorgruppenummer
      # - rstdeb.Kundenr   - Kundenr.: Debitors kundenummer hos kreditor
      # - 0                - Aftalenr.: 000000000 eller 999999999
      # - rstdeb.Navn      - Navn: Debitors navn
      rec += write022(rstkrd.Sektionnr, rstkrd.Pbsnr, "0240", 1, rstkrd.Debgrpnr, '%s' % (rstdeb.Kundenr), 0, rstdeb.Navn) + "\r\n"
      antal022++
      antal022tot++

      # Split adresse i 2 felter hvis l�ngde > 35
      rstdeb_Adresse1 = None
      rstdeb_Adresse2 = None
      bStart_Adresse1 = True
      bStart_Adresse2 = True
      if rstdeb.Adresse.len() <= 35:
        rstdeb_Adresse1 = rstdeb.Adresse
      else:
        words = rstdeb.Adresse.split(' ')
        for word in words:
          if bStart_Adresse1:
            rstdeb_Adresse1 = word
            bStart_Adresse1 = False
          else:
            if (rstdeb_Adresse1.len() + 1 + word.len()) <= 35:
              rstdeb_Adresse1 += " " + word
            else:
              if bStart_Adresse2:
                rstdeb_Adresse2 = word
                bStart_Adresse2 = False
              else:
                rstdeb_Adresse2 += " " + word

      # Debitoradresse 1/adresse 2
      # - rstkrd.Sektionnr -
      # - rstkrd.Pbsnr     - PBS-nr.: Kreditors PBS-nummer
      # - "0240"           - Transkode: 0240 (Navn/adresse p� debitor)
      # - 2                - Recordnr.: 002
      # - rstkrd.Debgrpnr  - Debitorgruppenr.: Debitorgruppenummer
      # - rstdeb.Kundenr   - Kundenr.: Debitors kundenummer hos kreditor
      # - 0                - Aftalenr.: 000000000 eller 999999999
      # - rstdeb.Adresse   - Adresse 1: Adresselinie 1
      rec += write022(rstkrd.Sektionnr, rstkrd.Pbsnr, "0240", 2, rstkrd.Debgrpnr, '%s' % (rstdeb.Kundenr), 0, rstdeb_Adresse1) + "\r\n"
      antal022++
      antal022tot++

      if not bStart_Adresse2:
        # Debitoradresse 1/adresse 3
        # - rstkrd.Sektionnr -
        # - rstkrd.Pbsnr     - PBS-nr.: Kreditors PBS-nummer
        # - "0240"           - Transkode: 0240 (Navn/adresse p� debitor)
        # - 2                - Recordnr.: 002
        # - rstkrd.Debgrpnr  - Debitorgruppenr.: Debitorgruppenummer
        # - rstdeb.Kundenr   - Kundenr.: Debitors kundenummer hos kreditor
        # - 0                - Aftalenr.: 000000000 eller 999999999
        # - rstdeb.Adresse   - Adresse 1: Adresselinie 1
        rec += write022(rstkrd.Sektionnr, rstkrd.Pbsnr, "0240", 3, rstkrd.Debgrpnr, '%s' % (rstdeb.Kundenr), 0, rstdeb_Adresse2) + "\r\n"
        antal022++
        antal022tot++

      # Debitorpostnummer
      # - rstkrd.Sektionnr -
      # - rstkrd.Pbsnr     - PBS-nr.: Kreditors PBS-nummer
      # - "0240"           - Transkode: 0240 (Navn/adresse p� debitor)
      # - 3                - Recordnr.: 003
      # - rstkrd.Debgrpnr  - Debitorgruppenr.: Debitorgruppenummer
      # - rstdeb.Kundenr   - Kundenr.: Debitors kundenummer hos kreditor
      # - 0                - Aftalenr.: 000000000 eller 999999999
      # - rstdeb.Postnr    - Postnr.: Postnummer
      rec += write022(rstkrd.Sektionnr, rstkrd.Pbsnr, "0240", 9, rstkrd.Debgrpnr, '%s' % (rstdeb.Kundenr), 0, rstdeb.Postnr.ToString()) + "\r\n"
      antal022++
      antal022tot++

      # Forfald betaling
      if rstdeb.Belob > 0.0:
        fortegn = 1
        # Fortegnskode: 1 = tr�k
        belobint = int(round(rstdeb.Belob * 100)) 
        belob042 += belobint
        belob042tot += belobint
      elif rstdeb.Belob < 0.0:
        fortegn = 2
        # Fortegnskode: 2 = inds�ttelse
        belobint = int(round(rstdeb.Belob * (-100))) 
        belob042 -= belobint
        belob042tot -= belobint
      else:
        fortegn = 0  # Fortegnskode: 0 = 0-bel�b
        belobint = 0

      # - rstkrd.Sektionnr         -
      # - rstkrd.Pbsnr             - PBS-nr.: Kreditors PBS-nummer
      # - rstkrd.Transkodebetaling - Transkode: 0280/0285 (Betaling)
      # - rstkrd.Debgrpnr          - Debitorgruppenr.: Debitorgruppenummer
      # - rstdeb.Kundenr           - Kundenr.: Debitors kundenummer hos kreditor
      # - 0                        - Aftalenr.: 000000000 eller 999999999
      # - rstdeb.Betalingsdato     -
      # - fortegn                  -
      # - belobint                 - Bel�b: Bel�b i �re uden fortegn
      # - rstdeb.Faknr             - faknr: Information vedr�rende betalingen.
      rec += write042(rstkrd.Sektionnr, rstkrd.Pbsnr, rstkrd.Transkodebetaling, rstkrd.Debgrpnr, '%s' % (rstdeb.Kundenr), 0, (DateTime)rstdeb.Betalingsdato, fortegn,belobint, (int)rstdeb.Faknr) + "\r\n"
      antal042++
      antal042tot++

      recnr = 0
      firstAdvistekst = True      
      if (rstdeb.Advistekst) and (rstdeb.Advistekst.len() > 0):
        arradvis = rstdeb.Advistekst.split("\r\n")
        for advis in arradvis:
          if firstAdvistekst:
            recnr++
            advistekst = advis
            advisbelob = int(rstdeb.Belob)
            firstAdvistekst = False
          else:
            recnr++
            advistekst = advis
            advisbelob = 0

          # Tekst til advis
          antal052++
          antal052tot++

          # - rstkrd!sektionnr  -
          # - rstkrd!pbsnr      - PBS-nr.: Kreditors PBS-nummer
          # - "0241"            - Transkode: 0241 (Tekstlinie)
          # - recnr             - Recordnr.: 001-999
          # - rstkrd!debgrpnr   - Debitorgruppenr.: Debitorgruppenummer
          # - rstdeb!kundenr    - Kundenr.: Debitors kundenummer hos kreditor
          # - 0                 - Aftalenr.: 000000000 eller 999999999
          # - advistekst        - Advistekst 1: Tekstlinie p� advis
          # - 0.0               - Advisbel�b 1: Bel�b p� advis
          # - ""                - Advistekst 2: Tekstlinie p� advis
          # - 0.0               - Advisbel�b 2: Bel�b p� advis
          rec += write052(rstkrd.Sektionnr, rstkrd.Pbsnr, "0241", recnr, rstkrd.Debgrpnr, '%s' % (rstdeb.Kundenr), 0, advistekst, advisbelob, "", 0) + "\r\n"
      
      param = clsInfotekstParam()
      param.infotekst_id = rstdeb.Infotekst
      param.numofcol = 60
      param.navn_medlem = rstdeb.Navn
      param.kaldenavn = rstdeb.Kaldenavn 
      param.fradato = rstdeb.Fradato 
      param.tildato = rstdeb.Tildato 
      param.betalingsdato = rstdeb.Betalingsdato
      param.advisbelob = rstdeb.Belob 
      param.ocrstring = None
      param.underskrift_navn= "\r\nMogens Hafsjold\r\nRegnskabsf�rer"
      param.bankkonto = None
      param.advistekst = None
      infotekst = clsInfotekst(param).getinfotekst() 

      if (infotekst) and (infotekst.len() > 0):
        arradvis = infotekst.split("\r\n")
        for advisline in arradvis:
          recnr++
          antal052++
          antal052tot++

          # - rstkrd!sektionnr     -
          # - rstkrd!pbsnr         - PBS-nr.: Kreditors PBS-nummer
          # - "0241"               - Transkode: 0241 (Tekstlinie)
          # - recnr                - Recordnr.: 001-999
          # - rstkrd!debgrpnr      - Debitorgruppenr.: Debitorgruppenummer
          # - rstdeb!kundenr       - Kundenr.: Debitors kundenummer hos kreditor
          # - 0                    - Aftalenr.: 000000000 eller 999999999
          # - infolin.getinfotekst - Advistekst 1: Tekstlinie p� advis
          # - 0.0                  - Advisbel�b 1: Bel�b p� advis
          # - ""                   - Advistekst 2: Tekstlinie p� advis
          # - 0.0                  - Advisbel�b 2: Bel�b p� advis
          rec += write052(rstkrd.Sektionnr, rstkrd.Pbsnr, "0241", recnr, rstkrd.Debgrpnr, '%s' % (rstdeb.Kundenr), 0, advisline, 0, "", 0) + "\r\n"

    # End rstdebs

    # Sektion slut - sektion 0112/117
    # - rstkrd!pbsnr     - PBS-nr.: Kreditors PBS-nummer
    # - rstkrd!sektionnr - Sektionsnr.: 0112/0117 (Betalinger)
    # - rstkrd!debgrpnr  - Debitorgruppenr.: Debitorgruppenummer
    # - antal042         - Antal 042: Antal foranst�ende 042 records
    # - belob042         - Bel�b: Nettobel�b i 042 records
    # - antal052         - Antal 052: Antal foranst�ende 052 records
    # - antal022         - Antal 022: Antal foranst�ende 022 records
    rec += write092(rstkrd.Pbsnr, rstkrd.Sektionnr, rstkrd.Debgrpnr, antal042, belob042, antal052, antal022) + "\r\n"

    # Leverance slut  - 0601 Betalingsoplysninger
    # - rstkrd.Datalevnr - Dataleverand�rnr.: Dataleverand�rens SE-nummer
    # - rstkrd.Delsystem - Delsystem:  Dataleverand�r delsystem
    # - "0601"           - Leverancetype: 0601 (Betalingsoplysninger)
    # - antalsek         - Antal sektioner: Antal sektioner i leverancen
    # - antal042tot      - Antal 042: Antal foranst�ende 042 records
    # - belob042tot      - Bel�b: Nettobel�b i 042 records
    # - antal052tot      - Antal 052: Antal foranst�ende 052 records
    # - antal022tot      - Antal 022: Antal foranst�ende 022 records
    rec += write992(rstkrd.Datalevnr, rstkrd.Delsystem, "0601", antalsek, antal042tot, belob042tot, antal052tot, antal022tot) + "\r\n"

    rsttil.Udtrukket = datetime.now()
    rsttil.Leverancespecifikation = '%s' % (wleveranceid)
    return rec
    
 
  def write002(self, datalevnr, delsystem, levtype, levident, levdato):
    rec = "BS002"
    rec += lpad(datalevnr, 8, '?')
    rec += lpad(delsystem, 3, '?')
    rec += lpad(levtype, 4, '?')
    rec += lpad(levident, 10, '0')
    rec += rpad("", 19, ' ')
    rec += lpad(levdato.strftime("%d%m%y"), 6, '?')
    return rec

  def write012(self, pbsnr, sektionnr, debgrpnr, levident, levdato, regnr, kontonr, h_linie):
    rec = "BS012"
    # Recordtype
    rec += lpad(pbsnr, 8, '?')
    # PBS-nr.: Kreditors PBS-nummer
    rec += lpad(sektionnr, 4, '?')
    # Sektionsnr.
    if sektionnr == "0100":
      rec += rpad("", 3, ' ')
      # Filler
      rec += lpad(debgrpnr, 5, '0')
      # Debitorgruppenr.: Debitorgruppenummer
      rec += rpad(levident, 15, ' ')
      # Leveranceidentifikation: Brugers identifikation hos dataleverand�r
      rec += rpad("", 9, ' ')
      # Filler
      # Dato: 000000 eller leverancens dannelsesdato
      rec += lpad(levdato.strftime("%d%m%y"), 6, '?')
    elif sektionnr == "0112":
      rec += rpad("", 5, ' ')
      # Filler
      rec += lpad(debgrpnr, 5, '0')
      # Debitorgruppenr.: Debitorgruppenummer
      rec += rpad(levident, 15, ' ')
      # Leveranceidentifikation: Brugers identifikation hos dataleverand�r
      rec += rpad("", 4, ' ')
      # Filler
      # Dato: 00000000 eller leverancens dannelsesdato
      rec += lpad(levdato.strftime("%d%m%Y"), 8, '?')
    elif sektionnr == "0117":
      rec += rpad("", 5, ' ')
      # Filler
      rec += lpad(debgrpnr, 5, '0')
      # Debitorgruppenr.: Debitorgruppenummer
      rec += rpad(levident, 15, ' ')
      # Leveranceidentifikation: Brugers identifikation hos dataleverand�r
      rec += rpad("", 4, ' ')
      # Filler
      # Dato: 00000000 eller leverancens dannelsesdato
      rec += lpad(levdato.strftime("%d%m%Y"), 8, '?')

    rec += lpad(regnr, 4, '0')
    # Reg.nr.: Overf�rselsregistreringsnummer
    rec += lpad(kontonr, 10, '0')
    # Kontonr.: Overf�rselskontonummer
    rec += h_linie
    # H-linie: Tekst til hovedlinie p� advis
    return rec
    
  def write022(self, sektionnr, pbsnr, transkode, recordnr, debgrpnr, personid, aftalenr, Data):
    rec = "BS022"
    # Recordtype
    rec += lpad(pbsnr, 8, '?')
    # PBS-nr.: Kreditors PBS-nummer
    rec += lpad(transkode, 4, '?')
    # Transkode:
    if sektionnr == "0112":
      # Recordnr.
      rec += lpad(recordnr, 5, '0')
    elif sektionnr == "0117":
      # Recordnr.
      rec += lpad(recordnr, 5, '0')
    else:
      # Recordnr.
      rec += lpad(recordnr, 3, '0')

    rec += lpad(debgrpnr, 5, '0')
    # Debitorgruppenr.: Debitorgruppenummer
    rec += lpad(personid, 15, '0')
    # Kundenr.: Debitors kundenummer hos kreditor
    rec += lpad(aftalenr, 9, '0')
    # Aftalenr.
    if recordnr == 9:
      rec += rpad("", 15, ' ')
      # Filler
      rec += rpad(Data, 4, ' ')
      # Debitor postnr
      # Debitor landekode
      rec += rpad("DK", 3, ' ')
    else:
      # Debitor navn og adresse
      rec += Data

    return rec
    
  def write042(self, sektionnr, pbsnr, transkode, debgrpnr, medlemsnr, aftalenr, betaldato, fortegn, belob, faknr):
    rec = "BS042"
    # Recordtype
    rec += lpad(pbsnr, 8, '?')
    # PBS-nr.: Kreditors PBS-nummer
    rec += lpad(transkode, 4, '?')
    # Transkode:
    if sektionnr == "0112":
      # Recordnr.
      rec += lpad("", 5, '0')
    elif sektionnr == "0117":
      # Recordnr.
      rec += lpad("", 5, '0')
    else:
      # Recordnr.
      rec += lpad("", 3, '0')

    rec += lpad(debgrpnr, 5, '0')
    # Debitorgruppenr.: Debitorgruppenummer
    rec += lpad(medlemsnr, 15, '0')
    # Kundenr.: Debitors kundenummer hos kreditor
    rec += lpad(aftalenr, 9, '0')
    # Aftalenr.
    if sektionnr == "0112":
      rec += lpad(betaldato.strftime("%d%m%Y"), 8, '?')
    elif sektionnr == "0117":
      rec += lpad(betaldato.strftime("%d%m%Y"), 8, '?')
    else:
      rec += lpad(betaldato.strftime("%d%m%Y"), 6, '?')

    rec += lpad(fortegn, 1, '0')
    rec += lpad(belob, 13, '0')
    rec += lpad(faknr, 9, '0')
    rec += rpad("", 21, ' ')
    if sektionnr == "0112":
      rec += rpad("", 2, '0')
    elif sektionnr == "0117":
      rec += rpad("", 2, '0')
    else:
      rec += rpad("", 6, '0')

    return rec

  def write052(self, sektionnr, pbsnr, transkode, recordnr, debgrpnr, medlemsnr, aftalenr, advistekst1, advisbelob1, advistekst2, advisbelob2):
    rec = "BS052"
    # Recordtype
    rec += lpad(pbsnr, 8, '?')
    # PBS-nr.: Kreditors PBS-nummer
    rec += lpad(transkode, 4, '?')
    # Transkode:
    if sektionnr == "0112":
      # Recordnr.
      rec += lpad(recordnr, 5, '0')
    elif sektionnr == "0117":
      # Recordnr.
      rec += lpad(recordnr, 5, '0')
    else:
      # Recordnr.
      rec += lpad(recordnr, 3, '0')

    rec += lpad(debgrpnr, 5, '0')
    # Debitorgruppenr.: Debitorgruppenummer
    rec += lpad(medlemsnr, 15, '0')
    # Kundenr.: Debitors kundenummer hos kreditor
    rec += lpad(aftalenr, 9, '0')
    # Aftalenr.
    rec += " "
    if sektionnr == "0112":
      if advisbelob1 != 0:
        rec += rpad(advistekst1, 38, ' ')
        # Advistekst 1: Tekstlinie p� advis
        # Advisbel�b 1: Bel�b p� advis
        advisbelob1_formated =  '%.2f' % (advisbelob1)
        rec += rpad(advisbelob1_formated.replace('.', ','), 9, ' ')
      else:
        # Advistekst 1: Tekstlinie p� advis
        rec += advistekst1
    elif sektionnr == "0117":
      if advisbelob1 != 0:
        rec += rpad(advistekst1, 38, ' ')
        # Advistekst 1: Tekstlinie p� advis
        # Advisbel�b 1: Bel�b p� advis
        advisbelob1_formated = '%.2f' % (advisbelob1)
        rec += rpad(advisbelob1_formated.replace('.', ','), 9, ' ')
      else:
        # Advistekst 1: Tekstlinie p� advis
        rec += advistekst1
    else:
      if advisbelob1 != 0:
        rec += rpad(advistekst1, 29, ' ')
        # Advistekst 1: Tekstlinie p� advis
        # Advisbel�b 1: Bel�b p� advis
        advisbelob1_formated = '%.2f' % (advisbelob1)
        rec += rpad(advisbelob1_formated.replace('.', ','), 9, ' ')
      else:
        # Advistekst 1: Tekstlinie p� advis
        rec += rpad(advistekst1, 38, ' ')

      rec += '0'
      if advisbelob2 != 0:
        rec += rpad(advistekst2, 29, ' ')
        # Advistekst 2: Tekstlinie p� advis
        # Advisbel�b 1: Bel�b p� advis
        advisbelob2_formated = '%.2f' % (advisbelob1)
        rec += rpad(advisbelob2_formated.replace('.', ','), 9, ' ')
      elif advistekst2 != null:
        # Advistekst 2: Tekstlinie p� advis
        rec += advistekst2

    return rec

  def write092(self, pbsnr, sektionnr, debgrpnr, antal1, belob1, antal2, antal3):
    rec = "BS092"
    rec += lpad(pbsnr, 8, '?')
    rec += lpad(sektionnr, 4, '?')
    if sektionnr == "0112":
      rec += rpad("", 5, '0')
      rec += lpad(debgrpnr, 5, '0')
      rec += rpad("", 4, ' ')
    elif sektionnr == "0117":
      rec += rpad("", 5, '0')
      rec += lpad(debgrpnr, 5, '0')
      rec += rpad("", 4, ' ')
    else:
      rec += rpad("", 3, '0')
      rec += lpad(debgrpnr, 5, '0')
      rec += rpad("", 6, ' ')

    rec += lpad(antal1, 11, '0')
    rec += lpad(belob1, 15, '0')
    rec += lpad(antal2, 11, '0')
    rec += rpad("", 15, ' ')
    rec += lpad(antal3, 11, '0')
    return rec
     
  def write992(self, datalevnr, delsystem, levtype, antal1, antal2, belob2, antal3, antal4):
    rec = "BS992"
    rec += lpad(datalevnr, 8, '?')
    rec += lpad(delsystem, 3, '?')
    rec += lpad(levtype, 4, '?')
    rec += lpad(antal1, 11, '0')
    rec += lpad(antal2, 11, '0')
    rec += lpad(belob2, 15, '0')
    rec += lpad(antal3, 11, '0')
    rec += lpad("", 15, '0')
    rec += lpad(antal4, 11, '0')
    rec += lpad("", 34, '0')
    return rec
    

  