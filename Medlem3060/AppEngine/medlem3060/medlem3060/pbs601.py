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
      # Leveranceidentifikation: Brugers identifikation hos dataleverandør
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
      # Leveranceidentifikation: Brugers identifikation hos dataleverandør
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
      # Leveranceidentifikation: Brugers identifikation hos dataleverandør
      rec += rpad("", 4, ' ')
      # Filler
      # Dato: 00000000 eller leverancens dannelsesdato
      rec += lpad(levdato.strftime("%d%m%Y"), 8, '?')

    rec += lpad(regnr, 4, '0')
    # Reg.nr.: Overførselsregistreringsnummer
    rec += lpad(kontonr, 10, '0')
    # Kontonr.: Overførselskontonummer
    rec += h_linie
    # H-linie: Tekst til hovedlinie på advis
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
        # Advistekst 1: Tekstlinie på advis
        # Advisbeløb 1: Beløb på advis
        advisbelob1_formated =  '%.2f' % (advisbelob1)
        rec += rpad(advisbelob1_formated.replace('.', ','), 9, ' ')
      else:
        # Advistekst 1: Tekstlinie på advis
        rec += advistekst1
    elif sektionnr == "0117":
      if advisbelob1 != 0:
        rec += rpad(advistekst1, 38, ' ')
        # Advistekst 1: Tekstlinie på advis
        # Advisbeløb 1: Beløb på advis
        advisbelob1_formated = '%.2f' % (advisbelob1)
        rec += rpad(advisbelob1_formated.replace('.', ','), 9, ' ')
      else:
        # Advistekst 1: Tekstlinie på advis
        rec += advistekst1
    else:
      if advisbelob1 != 0:
        rec += rpad(advistekst1, 29, ' ')
        # Advistekst 1: Tekstlinie på advis
        # Advisbeløb 1: Beløb på advis
        advisbelob1_formated = '%.2f' % (advisbelob1)
        rec += rpad(advisbelob1_formated.replace('.', ','), 9, ' ')
      else:
        # Advistekst 1: Tekstlinie på advis
        rec += rpad(advistekst1, 38, ' ')

      rec += '0'
      if advisbelob2 != 0:
        rec += rpad(advistekst2, 29, ' ')
        # Advistekst 2: Tekstlinie på advis
        # Advisbeløb 1: Beløb på advis
        advisbelob2_formated = '%.2f' % (advisbelob1)
        rec += rpad(advisbelob2_formated.replace('.', ','), 9, ' ')
      elif advistekst2 != null:
        # Advistekst 2: Tekstlinie på advis
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
    

  