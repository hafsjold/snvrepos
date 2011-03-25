# coding=utf-8
from google.appengine.ext import webapp
from google.appengine.ext import db
from google.appengine.ext.webapp import template

from models import MedlemsStatus, nextval, UserGroup, User, NrSerie, Kreditor, Kontingent, Pbsforsendelse, Pbsfiles, Pbsfile, Sendqueue, Recievequeue, Tilpbs, Fak, Frapbs, Bet, Betlin, Overforsel, Sftp, Infotekst, Sysinfo, Menu, MenuMenuLink, Medlog, Person
from util import PassXmlDoc, lpad, rpad, utc, cet
from datetime import datetime, date, timedelta
import logging
import os
import re
from xml.dom import minidom
from clsInfotekst import clsInfotekstParam, clsInfotekst

class PbsOverforselError(Exception):
  def __init__(self, value):
    self.value = value
  def __str__(self):
    return repr(self.value)

class clsOverforselEmail(object):
  def __init__(self, navn, email, emne, tekst):
    self.Navn = navn
    self.Email = email
    self.Tekst = tekst
    
class OverforselMailHandler(webapp.RequestHandler, PassXmlDoc):
  def post(self):
    listOverforselEmail = []
    status = False
    doc = minidom.parse(self.request.body_file)
    if doc.documentElement.tagName == 'OverforselMail':
      lobnr = attr_val(doc, 'Lobnr', 'IntegerProperty')
      tilpbskey = db.Key.from_path('rootTilpbs','root','Tilpbs','%s' % (lobnr))
      rsttil = Tilpbs.get(tilpbskey)
      if not rsttil: 
        #raise PbsOverforselError('101 - Der er ingen PBS forsendelse for id: %s' % (lobnr))
        status = False
    
      for rec_overfoersel in rsttil.listOverforsel:
        person = rec_overfoersel.parent()  
        param = clsInfotekstParam()
        param.infotekst_id = 40
        param.numofcol = None
        param.navn_medlem = person.Navn
        param.kaldenavn = person.Kaldenavn 
        param.fradato = None 
        param.tildato = None 
        param.betalingsdato = Betalingsdato
        param.advisbelob = rec_overfoersel.Advisbelob 
        param.ocrstring = None
        param.underskrift_navn= u'\r\nMogens Hafsjold\r\nRegnskabsfører'
        param.bankkonto = rec_overfoersel.Bankregnr + '-' + rec_overfoersel.Bankkontonr
        param.advistekst = rec_overfoersel.Advistekst
        objInfotekst  = clsInfotekst(param)
        infotekst = objInfotekst.getinfotekst() 
        logging.info('QQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQinfotekst: %s' % (infotekst))
        rec_overfoersel.Emailtekst = infotekst
        rec_overfoersel.put()
        if (infotekst) and (len(infotekst) > 0):
          #Send email
          objOverforselEmail = clsOverforselEmail(person.Navn, person.Email, infotekst)
          listOverforselEmail.append(objOverforselEmail)
          status = True

    template_values = {
      'status': status,
      'overforselemail': listOverforselEmail,
    }
    path = os.path.join(os.path.dirname(__file__), 'templates/overforselemail.xml')
    self.response.out.write(template.render(path, template_values))
    
    
class OverforselHandler(webapp.RequestHandler, PassXmlDoc):
  def get(self):
    root = db.Key.from_path('Persons','root')
    qry = db.Query(Overforsel).ancestor(root).order('SFakID')
    allFields = False

    status = (qry.count() > 0)
    template_values = {
      'allFields': allFields,
      'status': status,
      'forslag': qry,
    }
    path = os.path.join(os.path.dirname(__file__), 'templates/overforsel.xml')
    self.response.out.write(template.render(path, template_values))

  def post(self):
    status = False
    antal = 0
    sendqueueid = None    
    doc = minidom.parse(self.request.body_file)
    if doc.documentElement.tagName == 'TempBetalforslag':
      (lobnr, antal) = self.kreditor_fakturer_os1(doc)
    if antal > 0:
      sendqueueid  = self.krdfaktura_overfoersel_action(lobnr)
      logging.info('WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW sendqueueid: %s' % (sendqueueid))
      status = True
    template_values = {
      'status': status,
      'lobnr': lobnr,
      'antal': antal,
      'sendqueueid': sendqueueid,     
    }
    path = os.path.join(os.path.dirname(__file__), 'templates/overforselstatus.xml')
    self.response.out.write(template.render(path, template_values))  


  def kreditor_fakturer_os1(self, doc):
    antal = 0
    Betalingsdato = self.attr_val(doc, 'Betalingsdato', 'DateProperty')

    lobnr = nextval('Tilpbsid')
    rootTilpbs = db.Key.from_path('rootTilpbs','root')
    t = Tilpbs.get_or_insert('%s' % (lobnr), parent=rootTilpbs)
    t.Id = lobnr
    t.Delsystem = "OS1"
    t.Leverancetype = None
    t.Udtrukket = datetime.now()
    t.put()

    TempBetalforslaglinie = doc.getElementsByTagName("TempBetalforslaglinie")
    for TempBetalforslaglinie in TempBetalforslaglinies:
      Nr = self.attr_val(TempBetalforslaglinie, 'Nr', 'IntegerProperty')
      Navn = self.attr_val(TempBetalforslaglinie, 'Navn', 'StringProperty')
      Advisbelob = self.attr_val(TempBetalforslaglinie, 'Advisbelob', 'FloatProperty')
      Fakid = self.attr_val(TempBetalforslaglinie, 'Fakid', 'IntegerProperty')
      Bankregnr = self.attr_val(TempBetalforslaglinie, 'Bankregnr', 'StringProperty')
      Bankkontonr = self.attr_val(TempBetalforslaglinie, 'Bankkontonr', 'StringProperty')
      Faknr = self.attr_val(TempBetalforslaglinie, 'Faknr', 'IntegerProperty')
      wadvistekst = 'Puls3060-' + Faknr

      overforselid = nextval('Overforselid')
      keyPerson = db.Key.from_path('Persons','root','Person','%s' % (Nr))
      keyTilpbs = db.Key.from_path('rootTilpbs','root','Tilpbs','%s' % (lobnr))
      o = Overforsel.get_or_insert('%s' % (overforselid), parent=keyPerson)
      o.Id = overforselid
      o.TilPbsref = keyTilpbs
      o.Nr = Nr
      o.Advistekst = wadvistekst
      o.Advisbelob = Advisbelob
      o.SFakID = Fakid
      o.SFaknr = Faknr
      o.Bankregnr = Bankregnr
      o.Bankkontonr = Bankkontonr
      o.Betalingsdato = bankdageplus(Betalingsdato, 0)
      o.put()
      antal += 1
    return (lobnr, antal)

  def krdfaktura_overfoersel_action(self, lobnr):
    rec = ''
    seq = 0
    wleveranceid = None
    wdispositionsdato = None
    whour = None
    wbankdage = None

    # Betalingsoplysninger
    belobint = None

    # Tællere
    antalos5 = 0      # Antal OS5: Antal foranstående OS5 records
    belobos5 = 0      # Beløb: Nettobeløb i OS5 records

    antalsek = 0      # Antal sektioner i leverancen
    antalos5tot = 0   # Antal OS5: Antal foranstående OS5 records
    belobos5tot = 0   # Beløb: Nettobeløb i OS5 records

    nu = datetime.now(cet).replace(tzinfo = None)
    whour = nu.hour
    if whour > 17: wbankdage = 3
    else: wbankdage = 2
    wdispositionsdato = bankdageplus(nu, wbankdage)

    tilpbskey = db.Key.from_path('rootTilpbs','root','Tilpbs','%s' % (lobnr))
    rsttil = Tilpbs.get(tilpbskey)
    if not rsttil: 
      raise PbsOverforselError('101 - Der er ingen PBS forsendelse for id: %s' % (lobnr))    
 
    if rsttil.Pbsforsendelseref:
       raise PbsOverforselError('102 - Pbsforsendelse for id: %s er allerede sendt' % (lobnr))
       
    qry = rsttil.listOverforsel
    if qry.count() == 0:
      raise PbsOverforselError('103 - Der er ingen pbs transaktioner for tilpbsid: %s' % (lobnr))

    if not rsttil.Udtrukket:
      rsttil.Udtrukket = datetime.now()
    if not rsttil.Bilagdato:
      rsttil.Bilagdato = date.today()
    if not rsttil.Delsystem:
      rsttil.Delsystem = "OS1"
    if not rsttil.Leverancetype:
      rsttil.Leverancetype = ""
      
    wleveranceid = nextval("leveranceid")

    qry = Kreditor.all()
    qry.filter("Delsystem =", rsttil.Delsystem)
    krd = qry.fetch(1)[0]
    
    # -- Leverance Start - OS1
    # - rstkrd.Datalevnr - Dataleverandørnr.: Dataleverandørens SE-nummer
    # - wleveranceid     - Leveranceidentifikation: Løbenummer efter eget valg
    rec += self.writeOS1(krd.Datalevnr, wleveranceid)

    # Start loop over betalinger i tbloverforsel
    for rec_overfoersel in rsttil.listOverforsel:
      if rec_overfoersel.Betalingsdato == None:
        Betalingsdato = wdispositionsdato
      else:
        Betalingsdato = rec_overfoersel.Betalingsdato
      if Betalingsdato < wdispositionsdato: Betalingsdato = wdispositionsdato
      rec_overfoersel.Betalingsdato = Betalingsdato #opdater aktuel betalingsdato

      # Sektion start – (OS2)
      antalos5 = 0
      belobos5 = 0

      # -- OS2
      # - Betalingsdato  - Dispositionsdato
      # - krd.regnr      - Reg.nr.: Overførselsregistreringsnummer
      # - krd.kontonr    - Kontonr.: Overførselskontonummer
      # - krd.datalevnr  - Dataleverandørnr.: Dataleverandørens SE-nummer
      rec += self.writeOS2(Betalingsdato, krd.Regnr, krd.Kontonr, krd.Datalevnr)

      antalsek += 1

      # -- Forfald betaling
      if rec_overfoersel.Advisbelob > 0:
        belobint = int(rec_overfoersel.Advisbelob * 100)
        belobos5 += belobint
        belobos5tot += belobint
      else:
        belobint = 0

      # -- OS5
      # - debinfo.bankregnr   - Betalingsmodtager registreringsnummer
      # - debinfo.bankkontonr - Betalingsmodtager kontonummer
      # - belobint            - Beløb: Beløb i øre uden fortegn
      # - Betalingsdato       - Dispositionsdato
      # - krd.regnr           - Reg.nr.: Overførselsregistreringsnummer
      # - krd.kontonr         - Kontonr.: Overførselskontonummer
      # - deb.advistekst      - Tekst på Betalingsmodtagers kontoudtog
      # - deb.SFakID          - Ref til betalingsmodtager til eget brug
      rec += self.writeOS5(rec_overfoersel.Bankregnr, rec_overfoersel.Bankkontonr, belobint, Betalingsdato, krd.Regnr, krd.Kontonr, rec_overfoersel.Advistekst, rec_overfoersel.SFakID)
      antalos5 += 1
      antalos5tot += 1

      # -- Sektion slut – (OS8)
      # - OS8
      # - antalos5          - Antal 042: Antal foranstående 042 records
      # - belobos5          - Beløb: Nettobeløb i 042 records
      # - Betalingsdato     - Dispositionsdato
      # - krd.regnr         - Reg.nr.: Overførselsregistreringsnummer
      # - krd.kontonr       - Kontonr.: Overførselskontonummer
      # - krd.datalevnr     - Dataleverandørnr.: Dataleverandørens SE-nummer
      rec += self.writeOS8(antalos5, belobos5, Betalingsdato, krd.Regnr, krd.Kontonr, krd.Datalevnr)

    # -- Leverance slut - (OS9)

    # --OS9
    # - antalos5tot    - Antal 042: Antal foranstående 042 records
    # - belobos5tot    - Beløb: Nettobeløb i 042 records
    # - krd.datalevnr  - Dataleverandørnr.: Dataleverandørens SE-nummer
    rec += self.writeOS9(antalos5tot, belobos5tot, krd.Datalevnr)
    
    pbsforsendelseid  = nextval('Pbsforsendelseid')    
    root_pbsforsendelse = db.Key.from_path('rootPbsforsendelse','root')    
    rec_pbsforsendelse = Pbsforsendelse.get_or_insert('%s' % (pbsforsendelseid), parent=root_pbsforsendelse)
    rec_pbsforsendelse.Id = pbsforsendelseid
    rec_pbsforsendelse.Delsystem = rsttil.Delsystem
    rec_pbsforsendelse.Leverancetype = rsttil.Leverancetype
    rec_pbsforsendelse.Oprettetaf = 'Udb'
    rec_pbsforsendelse.Oprettet = datetime.now()
    rec_pbsforsendelse.Leveranceid = wleveranceid
    rec_pbsforsendelse.put()
    
    rsttil.Udtrukket = datetime.now()
    rsttil.Leverancespecifikation = '%s' % (wleveranceid)
    rsttil.Pbsforsendelseref = rec_pbsforsendelse.key()
    rsttil.put()

    pbsfilesid  = nextval('Pbsfilesid')  
    idlev  = nextval('idlev')  
    root_pbsfiles = db.Key.from_path('rootPbsfiles','root')    
    rec_pbsfiles = Pbsfiles.get_or_insert('%s' % (pbsfilesid), parent=root_pbsfiles)
    rec_pbsfiles.Id = pbsfilesid
    rec_pbsfiles.Idlev = idlev
    rec_pbsfiles.Pbsforsendelseref = rec_pbsforsendelse.key()
    rec_pbsfiles.put()
    
    root_pbsfile = db.Key.from_path('rootPbsfile','root')    
    rec_pbsfile = Pbsfile.get_or_insert('%s' % (pbsfilesid), parent=root_pbsfile)
    rec_pbsfile.Id = pbsfilesid
    rec_pbsfile.Pbsfilesref = rec_pbsfiles.key()        
    rec_pbsfile.Data = rec
    rec_pbsfile.put()
    
    sendqueueid = rec_pbsfile.add_to_sendqueue()
    return sendqueueid   


  def writeOS1(self, datalevnr, levident):
    kontroltal = '000000000'
    rec = 'OS121PBS-OVERFØRSEL'
    rec += lpad(kontroltal, 9, '?')         # Kontroltal for dataleverandør
    rec += lpad(levident, 20, '0')          # Leveranceidentifikation
    rec += rpad('', 3, '0')                 # Filler
    rec += lpad(datalevnr, 8, '?')          # Dataleverandørens CVR-nummer
    rec += rpad('', 1, '0')                 # Leverancekvitering
    rec += rpad('', 20, '0')                # Filler
    return rec

  def writeOS2(self, dispdato, regnr, kontonr, datalevnr):
    rec = 'OS2'                                                   # Recordtype
    rec += lpad('80', 2, '0')                                     # Overførselstype = 80
    rec += rpad('', 26, '0')                                      # Filler
    rec += lpad(String.Format('{0:ddMMyy}', dispdato), 6, '?')    # Dispositionsdato
    rec += lpad(regnr, 4, '0')                                    # Reg.nr.: Betalingsafsender registreringsnummer
    rec += lpad(kontonr, 10, '0')                                 # Kontonr.: Betalingsafsender kontonummer
    rec += lpad(datalevnr, 8, '?')                                # Dataleverandørens CVR-nummer
    rec += lpad(datalevnr, 8, '?')                                # Betalingsafsenders CVR-nummer
    rec += rpad('', 13, '0')                                      # Filler
    return rec

  def writeOS5(self, bankregnr, bankkontonr, belob, betaldato, regnr, kontonr, advistekst, modtager):
    rec = 'OS5'                                                # Recordtype
    rec += lpad('80', 2, '0')                                  # Overførselstype = 80
    rec += lpad(bankregnr, 4, '0')                             # Reg.nr.: Betalingsmodtager registreringsnummer
    rec += lpad(bankkontonr, 10, '0')                          # Kontonr.: Betalingsmodtager kontonummer
    rec += lpad(belob, 12, '0')                                # Beløb uden fortegn i øre
    rec += lpad(String.Format('{0:ddMMyy}', betaldato), 6, '?')# Dispositionsdato
    rec += lpad(regnr, 4, '0')                                 # Reg.nr.: Betalingsafsender registreringsnummer
    rec += lpad(kontonr, 10, '0')                              # Kontonr.: Betalingsafsender kontonummer
    rec += rpad(advistekst, 20, ' ')                           # Tekst på Betalingsmodtagers kontoudtog
    rec += lpad(modtager, 13, '0')                             # Ref til betalingsmodtager til eget brug
    rec += rpad('', 44, '0')                                   # Filler
    return rec

  def writeOS8(self, antal1, belob1, dispdato, regnr, kontonr, datalevnr):
    rec = 'OS8'
    rec += lpad('80', 2, '0')                                  # Overførselstype = 80
    rec += rpad('', 4, '0')                                    # Filler
    rec += lpad(antal1, 10, '0')                               # Antal overførsler i denne sektion
    rec += lpad(belob1, 12, '0')                               # Totalbeløb denne sektion
    rec += lpad(String.Format('{0:ddMMyy}', dispdato), 6, '?') # Dispositionsdato
    rec += lpad(regnr, 4, '0')                                 # Reg.nr.: Betalingsafsender registreringsnummer
    rec += lpad(kontonr, 10, '0')                              # Kontonr.: Betalingsafsender kontonummer
    rec += lpad(datalevnr, 8, '?')                             # Dataleverandørens CVR-nummer
    rec += lpad(datalevnr, 8, '?')                             # Betalingsafsenders CVR-nummer
    rec += rpad('', 13, '0')                                   # Filler
    return rec

  def writeOS9(self, antal2, belob2, datalevnr):
    rec = 'OS929'
    rec += rpad('', 4, '0')                # Filler
    rec += lpad(antal2, 10, '0')           # Total antal overførsler
    rec += lpad(belob2, 12, '0')           # Total beløb
    rec += rpad('', 6, '0')                # Filler
    rec += rpad('', 14, '9')               # Filler
    rec += lpad(datalevnr, 8, '?')         # Betalingsafsenders CVR-nummer
    rec += rpad('', 21, '0')               # Filler
    return rec