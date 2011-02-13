# coding=utf-8 
from google.appengine.ext import webapp
from google.appengine.ext import db 
from google.appengine.ext.webapp import template

from models import nextval, UserGroup, User, NrSerie, Kreditor, Kontingent, Pbsforsendelse, Pbsfiles, Pbsfile, Sendqueue, Recievequeue, Tilpbs, Fak, Sftp, Infotekst, Sysinfo, Menu, MenuMenuLink, Medlog, Person
from util import lpad, rpad, utc, cet
from datetime import datetime, date, timedelta
import logging
import os
import re
from xml.dom import minidom

from clsInfotekst import clsInfotekstParam, clsInfotekst

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
    self.TilPbsref = f.TilPbsref
    self.Advistekst = None
    self.Belob = f.Advisbelob
    self.OcrString = None
        
class DatasftpHandler(webapp.RequestHandler):
  def get(self):
    path = self.request.environ['PATH_INFO']
    mo = re.match("/data/sftp/(.*)", path)
    if mo:
      if mo.groups()[0]:
        Navn = mo.groups()[0]
      else:
        Navn =  None
    else:
      Navn =  None
    logging.info('FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF Navn: %s' % (Navn))
    
    try:
      template_values = {
        'status': True,
        'sftp': db.Query(Sftp).filter('Navn =', Navn).fetch(1)[0] ,
      }
    except:
      template_values = {
        'status': False,
      }
    
    path = os.path.join(os.path.dirname(__file__), 'templates/sftp.xml')
    self.response.out.write(template.render(path, template_values))
    
class DatafrapbsHandler(webapp.RequestHandler):
  def post(self):
    status = False
    doc = minidom.parse(self.request.body_file)
    tagName  = doc.documentElement.tagName
    logging.info('tagName==>%s<=' % (tagName))
    
    if tagName == 'Pbsfiles':
      try:
        PbsfileId = doc.getElementsByTagName("PbsfileId")[0].childNodes[0].data
      except:
        PbsfileId = None
      if PbsfileId == '*':
        PbsfileId = nextval('Pbsfilesid')
      rootPbsfiles = db.Key.from_path('rootPbsfiles','root')
      rec = Pbsfiles.get_or_insert('%s' % (PbsfileId), parent=rootPbsfiles)
      rec.Id = PbsfileId
      try:
        Type = int(doc.getElementsByTagName("Type")[0].childNodes[0].data)
        rec.Type = Type
      except:
        pass
      try:
        Path = doc.getElementsByTagName("Path")[0].childNodes[0].data
        rec.Path = Path
      except:
        pass      
      try:
        Filename = doc.getElementsByTagName("Filename")[0].childNodes[0].data
        rec.Filename = Filename
      except:
        pass     
      try:
        Size = int(doc.getElementsByTagName("Size")[0].childNodes[0].data)
        rec.Size = Size
      except:
        pass     
      try:
        strval = doc.getElementsByTagName("Atime")[0].childNodes[0].data
        dt = datetime.strptime(strval[:19], "%Y-%m-%dT%H:%M:%S")
        if strval[-6:] == '+01:00':
          Atime = dt.replace(tzinfo = cet)
        elif strval[-6:] == '+00:00':
          Atime =  dt.replace(tzinfo = utc)
        else:          
          Atime =  dt
        rec.Atime = Atime
      except:
        pass     
      try:
        strval = doc.getElementsByTagName("Mtime")[0].childNodes[0].data
        dt = datetime.strptime(strval[:19], "%Y-%m-%dT%H:%M:%S")
        if strval[-6:] == '+01:00':
          Mtime = dt.replace(tzinfo = cet)
        elif strval[-6:] == '+00:00':
          Mtime =  dt.replace(tzinfo = utc)
        else:          
          Mtime =  dt
        rec.Mtime = Mtime
      except:
        pass     
      try:
        strval = doc.getElementsByTagName("Transmittime")[0].childNodes[0].data
        dt = datetime.strptime(strval[:19], "%Y-%m-%dT%H:%M:%S")
        if strval[-6:] == '+01:00':
          Transmittime = dt.replace(tzinfo = cet)
        elif strval[-6:] == '+00:00':
          Transmittime =  dt.replace(tzinfo = utc)
        else:          
          Transmittime =  dt
        rec.Transmittime = Transmittime
      except:
        pass     
      rec.put() 

      rootPbsfile = db.Key.from_path('rootPbsfile','root')
      file_rec = Pbsfile.get_or_insert('%s' % (PbsfileId), parent=rootPbsfile)
      file_rec.Id = PbsfileId
      logging.info('Debug 1')
      file_rec.Pbsfilesref = rec.key()
      logging.info('Debug 2')

      try:
        Dataval = doc.getElementsByTagName("Data")[0].childNodes[0].data
        logging.info('Debug 3: %s' % (Dataval))      
        status = True
      except:
        Dataval = None
        logging.info('Debug 3: %s' % (Dataval)) 
      
      file_rec.Data = Dataval
      file_rec.put()
      file_rec.add_to_recievequeue()      

      template_values = {
      'status': status,
    }
    path = os.path.join(os.path.dirname(__file__), 'templates/status.xml')
    self.response.out.write(template.render(path, template_values))
        
class DatatilpbsHandler(webapp.RequestHandler):
  def get(self):
    qry = db.Query(Sendqueue).filter('Send_to_pbs =', False).filter('Onhold =', False)  
    antal = qry.count()
    logging.info('TTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTT Antal: %s' % (antal))
    if antal > 0:
      sendqueue = qry.fetch(1)[0]
      pbsfile = sendqueue.Pbsfileref
      #sendqueue.Onhold = True
      sendqueue.put()
      template_values = {
        'status': True,
        'sendqueue': sendqueue,
        'pbsfile': pbsfile,
      }
    else:
      template_values = {
        'status': False,
      }
    
    path = os.path.join(os.path.dirname(__file__), 'templates/datatilpbs.xml')
    self.response.out.write(template.render(path, template_values))
    
  
  def post(self):
    status = False
    doc = minidom.parse(self.request.body_file)
    tagName  = doc.documentElement.tagName
    logging.info('tagName==>%s<=' % (tagName))
    
    if tagName == 'Pbsfiles':
      try:
        PbsfileId = doc.getElementsByTagName("PbsfileId")[0].childNodes[0].data
      except:
        PbsfileId = None
      rootPbsfiles = db.Key.from_path('rootPbsfiles','root')
      rec = Pbsfiles.get_or_insert('%s' % (PbsfileId), parent=rootPbsfiles)
      try:
        Type = int(doc.getElementsByTagName("Type")[0].childNodes[0].data)
        rec.Type = Type
      except:
        pass
      try:
        Path = doc.getElementsByTagName("Path")[0].childNodes[0].data
        rec.Path = Path
      except:
        pass      
      try:
        Filename = doc.getElementsByTagName("Filename")[0].childNodes[0].data
        rec.Filename = Filename
      except:
        pass     
      try:
        Size = int(doc.getElementsByTagName("Size")[0].childNodes[0].data)
        rec.Size = Size
      except:
        pass     
      try:
        strval = doc.getElementsByTagName("Atime")[0].childNodes[0].data
        dt = datetime.strptime(strval[:19], "%Y-%m-%dT%H:%M:%S")
        if strval[-6:] == '+01:00':
          Atime = dt.replace(tzinfo = cet)
        elif strval[-6:] == '+00:00':
          Atime =  dt.replace(tzinfo = utc)
        else:          
          Atime =  dt
        rec.Atime = Atime
      except:
        pass     
      try:
        strval = doc.getElementsByTagName("Mtime")[0].childNodes[0].data
        dt = datetime.strptime(strval[:19], "%Y-%m-%dT%H:%M:%S")
        if strval[-6:] == '+01:00':
          Mtime = dt.replace(tzinfo = cet)
        elif strval[-6:] == '+00:00':
          Mtime =  dt.replace(tzinfo = utc)
        else:          
          Mtime =  dt
        rec.Mtime = Mtime
      except:
        pass     
      try:
        strval = doc.getElementsByTagName("Transmittime")[0].childNodes[0].data
        dt = datetime.strptime(strval[:19], "%Y-%m-%dT%H:%M:%S")
        if strval[-6:] == '+01:00':
          Transmittime = dt.replace(tzinfo = cet)
        elif strval[-6:] == '+00:00':
          Transmittime =  dt.replace(tzinfo = utc)
        else:          
          Transmittime =  dt
        rec.Transmittime = Transmittime
      except:
        pass     
      rec.put()
      
      try:
        SendqueueId = doc.getElementsByTagName("SendqueueId")[0].childNodes[0].data
      except:
        SendqueueId = None
      keySendqueue = db.Key.from_path('Sendqueue', '%s' % (SendqueueId))        
      recSendqueue = Sendqueue.get(keySendqueue)      
      recSendqueue.Send_to_pbs = True
      recSendqueue.put()
      status = True

    template_values = {
      'status': status,
    }
    path = os.path.join(os.path.dirname(__file__), 'templates/status.xml')
    self.response.out.write(template.render(path, template_values))

class pbs601Handler(webapp.RequestHandler):
  def get(self):
    (lobnr, antal) = self.kontingent_fakturer_bs1()
    if lobnr:
      sendqueueid  = self.faktura_og_rykker_601_action(lobnr)
      logging.info('WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW sendqueueid: %s' % (sendqueueid))
      self.response.out.write('%s faktureret' % (antal))
    else:
      self.response.out.write('Intet at fakturere')
    
  def kontingent_fakturer_bs1(self):
    root = db.Key.from_path('Persons','root')
    qry = db.Query(Kontingent).ancestor(root).filter('Faktureret =',False)
    antal = qry.count()
    if antal == 0:
      return (None, antal)
    
    lobnr = nextval('Tilpbsid')
    rootTilpbs = db.Key.from_path('rootTilpbs','root')
    t = Tilpbs.get_or_insert('%s' % (lobnr), parent=rootTilpbs)
    t.Id = lobnr
    t.Delsystem = "BSH"
    t.Leverancetype = "0601"
    t.Udtrukket = datetime.now()
    t.put()

    for q in qry:
      fakid = nextval('Fakid')
      keyPerson = db.Key.from_path('Persons','root','Person','%s' % (q.Nr))
      keyTilpbs = db.Key.from_path('rootTilpbs','root','Tilpbs','%s' % (lobnr))
      f = Fak.get_or_insert('%s' % (fakid), parent=keyPerson)
      f.Id = fakid
      f.TilPbsref = keyTilpbs
      dt = datetime.now(cet) + timedelta(days=7)
      f.Betalingsdato = dt.date()
      f.Nr = q.Nr
      f.Faknr = nextval('faknr')
      f.Advisbelob = q.Advisbelob
      f.Infotekst = 11
      f.Fradato = q.Fradato
      f.Tildato = q.Tildato
      f.put()
      q.Faktureret = True
      q.put()
      
    return (lobnr, antal)

  def faktura_og_rykker_601_action(self, lobnr):
    rec = ''
    crlf = "\n"
    #crlf = "\r\n"
    #lintype lin
    #infolintype infolin
    recnr = 0
    fortegn = 0
    wleveranceid = 0
    seq = 0
    # Betalingsoplysninger
    h_linie = u'LØBEKLUBBEN PULS 3060'
    # Tekst til hovedlinie på advis
    belobint = 0
    advistekst = ''
    advisbelob = 0
    # Tællere
    antal042 = 0
    # Antal 042: Antal foranstående 042 records
    belob042 = 0
    # Beløb: Nettobeløb i 042 records
    antal052 = 0
    # Antal 052: Antal foranstående 052 records
    antal022 = 0
    # Antal 022: Antal foranstående 022 records
    antalsek = 0
    # Antal sektioner i leverancen
    antal042tot = 0
    # Antal 042: Antal foranstående 042 records
    belob042tot = 0
    # Beløb: Nettobeløb i 042 records
    antal052tot = 0
    # Antal 052: Antal foranstående 052 records
    antal022tot = 0
    # Antal 022: Antal foranstående 022 records
    # TODO: On Error GoTo Warning!!!: The statement is not translatable 
    #lobnr = 275 '--debug

    tilpbskey = db.Key.from_path('rootTilpbs','root','Tilpbs','%s' % (lobnr))
    rsttil = Tilpbs.get(tilpbskey)
    if not rsttil: 
      raise Pbs601Error('101 - Der er ingen PBS forsendelse for id: %s' % (lobnr))    
 
    if rsttil.Pbsforsendelseref:
       raise Pbs601Error('102 - Pbsforsendelse for id: %s er allerede sendt' % (lobnr))
 
    qry = rsttil.listFak
    if qry.count() == 0:
      raise Pbs601Error('103 - Der er ingen pbs transaktioner for tilpbsid: %s' % (lobnr))

    if not rsttil.Udtrukket:
      rsttil.Udtrukket = datetime.now()
    if not rsttil.Bilagdato:
      rsttil.Bilagdato = date.today()
    if not rsttil.Delsystem:
      rsttil.Delsystem = "BS1"  # ????????????????
    if not rsttil.Leverancetype:
      rsttil.Leverancetype = ""
    
    wleveranceid = nextval("leveranceid")



    qry = Kreditor.all()
    qry.filter("Delsystem =", rsttil.Delsystem)
    rstkrd = qry.fetch(1)[0]

    # Leverance Start - 0601 Betalingsoplysninger
    # - rstkrd.Datalevnr - Dataleverandørnr.: Dataleverandørens SE-nummer
    # - rsttil.Delsystem - Delsystem:  Dataleverandør delsystem
    # - "0601"           - Leverancetype: 0601 (Betalingsoplysninger)
    # - wleveranceid     - Leveranceidentifikation: Løbenummer efter eget valg
    # - rsttil!udtrukket - Dato: 000000 eller leverancens dannelsesdato
    rec += self.write002(rstkrd.Datalevnr, rsttil.Delsystem, "0601", '%s' % (wleveranceid), rsttil.Udtrukket)

    # Sektion start - sektion 0112/0117
    # -  rstkrd.Pbsnr       - PBS-nr.: Kreditors PBS-nummer
    # -  rstkrd.Sektionnr   - Sektionsnr.: 0112/0117 (Betalinger med lang advistekst)
    # -  rstkrd.Debgrpnr    - Debitorgruppenr.: Debitorgruppenummer
    # -  rstkrd.Datalevnavn - Leveranceidentifikation: Brugers identifikation hos dataleverandør
    # -  rsttil.Udtrukket   - Dato: 000000 eller leverancens dannelsesdato
    # -  rstkrd.Regnr       - Reg.nr.: Overførselsregistreringsnummer
    # -  rstkrd.Kontonr     - Kontonr.: Overførselskontonummer
    # -  h_linie            - H-linie: Tekst til hovedlinie på advis
    rec += crlf + self.write012(rstkrd.Pbsnr, rstkrd.Sektionnr, rstkrd.Debgrpnr, rstkrd.Datalevnavn, rsttil.Udtrukket, rstkrd.Regnr, rstkrd.Kontonr, h_linie)
    antalsek += 1

    for rstfak in rsttil.listFak:
      rstdeb = clsRstdeb(rstfak)
      
      # Debitornavn
      # - rstkrd.Sektionnr -
      # - rstkrd.Pbsnr     - PBS-nr.: Kreditors PBS-nummer
      # - "0240"           - Transkode: 0240 (Navn/adresse på debitor)
      # - 1                - Recordnr.: 001
      # - rstkrd.Debgrpnr  - Debitorgruppenr.: Debitorgruppenummer
      # - rstdeb.Kundenr   - Kundenr.: Debitors kundenummer hos kreditor
      # - 0                - Aftalenr.: 000000000 eller 999999999
      # - rstdeb.Navn      - Navn: Debitors navn
      rec += crlf + self.write022(rstkrd.Sektionnr, rstkrd.Pbsnr, "0240", 1, rstkrd.Debgrpnr, '%s' % (rstdeb.Kundenr), 0, rstdeb.Navn)
      antal022 += 1
      antal022tot += 1

      # Split adresse i 2 felter hvis længde > 35
      rstdeb_Adresse1 = None
      rstdeb_Adresse2 = None
      bStart_Adresse1 = True
      bStart_Adresse2 = True
      if len(rstdeb.Adresse) <= 35:
        rstdeb_Adresse1 = rstdeb.Adresse
      else:
        words = rstdeb.Adresse.split(' ')
        for word in words:
          if bStart_Adresse1:
            rstdeb_Adresse1 = word
            bStart_Adresse1 = False
          elif (bStart_Adresse2) and ((len(rstdeb_Adresse1) + 1 + len(word)) <= 35):
            rstdeb_Adresse1 += " " + word
          elif bStart_Adresse2:
            rstdeb_Adresse2 = word
            bStart_Adresse2 = False
          else:
            rstdeb_Adresse2 += " " + word

      # Debitoradresse 1/adresse 2
      # - rstkrd.Sektionnr -
      # - rstkrd.Pbsnr     - PBS-nr.: Kreditors PBS-nummer
      # - "0240"           - Transkode: 0240 (Navn/adresse på debitor)
      # - 2                - Recordnr.: 002
      # - rstkrd.Debgrpnr  - Debitorgruppenr.: Debitorgruppenummer
      # - rstdeb.Kundenr   - Kundenr.: Debitors kundenummer hos kreditor
      # - 0                - Aftalenr.: 000000000 eller 999999999
      # - rstdeb.Adresse   - Adresse 1: Adresselinie 1
      rec += crlf + self.write022(rstkrd.Sektionnr, rstkrd.Pbsnr, "0240", 2, rstkrd.Debgrpnr, '%s' % (rstdeb.Kundenr), 0, rstdeb_Adresse1)
      antal022 += 1
      antal022tot += 1

      if not bStart_Adresse2:
        # Debitoradresse 1/adresse 3
        # - rstkrd.Sektionnr -
        # - rstkrd.Pbsnr     - PBS-nr.: Kreditors PBS-nummer
        # - "0240"           - Transkode: 0240 (Navn/adresse på debitor)
        # - 2                - Recordnr.: 002
        # - rstkrd.Debgrpnr  - Debitorgruppenr.: Debitorgruppenummer
        # - rstdeb.Kundenr   - Kundenr.: Debitors kundenummer hos kreditor
        # - 0                - Aftalenr.: 000000000 eller 999999999
        # - rstdeb.Adresse   - Adresse 1: Adresselinie 1
        rec += crlf + self.write022(rstkrd.Sektionnr, rstkrd.Pbsnr, "0240", 3, rstkrd.Debgrpnr, '%s' % (rstdeb.Kundenr), 0, rstdeb_Adresse2)
        antal022 += 1
        antal022tot += 1

      # Debitorpostnummer
      # - rstkrd.Sektionnr -
      # - rstkrd.Pbsnr     - PBS-nr.: Kreditors PBS-nummer
      # - "0240"           - Transkode: 0240 (Navn/adresse på debitor)
      # - 3                - Recordnr.: 003
      # - rstkrd.Debgrpnr  - Debitorgruppenr.: Debitorgruppenummer
      # - rstdeb.Kundenr   - Kundenr.: Debitors kundenummer hos kreditor
      # - 0                - Aftalenr.: 000000000 eller 999999999
      # - rstdeb.Postnr    - Postnr.: Postnummer
      rec += crlf + self.write022(rstkrd.Sektionnr, rstkrd.Pbsnr, "0240", 9, rstkrd.Debgrpnr, '%s' % (rstdeb.Kundenr), 0, rstdeb.Postnr)
      antal022 += 1
      antal022tot += 1

      # Forfald betaling
      if rstdeb.Belob > 0.0:
        fortegn = 1
        # Fortegnskode: 1 = træk
        belobint = int(round(rstdeb.Belob * 100)) 
        belob042 += belobint
        belob042tot += belobint
      elif rstdeb.Belob < 0.0:
        fortegn = 2
        # Fortegnskode: 2 = indsættelse
        belobint = int(round(rstdeb.Belob * (-100))) 
        belob042 -= belobint
        belob042tot -= belobint
      else:
        fortegn = 0  # Fortegnskode: 0 = 0-beløb
        belobint = 0

      # - rstkrd.Sektionnr         -
      # - rstkrd.Pbsnr             - PBS-nr.: Kreditors PBS-nummer
      # - rstkrd.Transkodebetaling - Transkode: 0280/0285 (Betaling)
      # - rstkrd.Debgrpnr          - Debitorgruppenr.: Debitorgruppenummer
      # - rstdeb.Kundenr           - Kundenr.: Debitors kundenummer hos kreditor
      # - 0                        - Aftalenr.: 000000000 eller 999999999
      # - rstdeb.Betalingsdato     -
      # - fortegn                  -
      # - belobint                 - Beløb: Beløb i øre uden fortegn
      # - rstdeb.Faknr             - faknr: Information vedrørende betalingen.
      rec += crlf + self.write042(rstkrd.Sektionnr, rstkrd.Pbsnr, rstkrd.Transkodebetaling, rstkrd.Debgrpnr, '%s' % (rstdeb.Kundenr), 0, rstdeb.Betalingsdato, fortegn,belobint, rstdeb.Faknr)
      antal042 += 1
      antal042tot += 1

      recnr = 0
      firstAdvistekst = True      
      if (rstdeb.Advistekst) and (rstdeb.Advistekst.len() > 0):
        arradvis = rstdeb.Advistekst.split("\r\n")
        for advis in arradvis:
          if firstAdvistekst:
            recnr += 1
            advistekst = advis
            advisbelob = int(rstdeb.Belob)
            firstAdvistekst = False
          else:
            recnr += 1
            advistekst = advis
            advisbelob = 0

          # Tekst til advis
          antal052 += 1
          antal052tot += 1

          # - rstkrd!sektionnr  -
          # - rstkrd!pbsnr      - PBS-nr.: Kreditors PBS-nummer
          # - "0241"            - Transkode: 0241 (Tekstlinie)
          # - recnr             - Recordnr.: 001-999
          # - rstkrd!debgrpnr   - Debitorgruppenr.: Debitorgruppenummer
          # - rstdeb!kundenr    - Kundenr.: Debitors kundenummer hos kreditor
          # - 0                 - Aftalenr.: 000000000 eller 999999999
          # - advistekst        - Advistekst 1: Tekstlinie på advis
          # - 0.0               - Advisbeløb 1: Beløb på advis
          # - ""                - Advistekst 2: Tekstlinie på advis
          # - 0.0               - Advisbeløb 2: Beløb på advis
          rec += crlf + self.write052(rstkrd.Sektionnr, rstkrd.Pbsnr, "0241", recnr, rstkrd.Debgrpnr, '%s' % (rstdeb.Kundenr), 0, advistekst, advisbelob, "", 0)
      
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
      param.underskrift_navn= "\r\nMogens Hafsjold\r\nRegnskabsfører"
      param.bankkonto = None
      param.advistekst = None
      
      objInfotekst  = clsInfotekst(param)
      infotekst = objInfotekst.getinfotekst() 
      logging.info('QQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQinfotekst: %s' % (infotekst))
      
      if (infotekst) and (len(infotekst) > 0):
        arradvis = infotekst.split("\r\n")
        for advisline in arradvis:
          recnr += 1
          antal052 += 1
          antal052tot += 1

          # - rstkrd!sektionnr     -
          # - rstkrd!pbsnr         - PBS-nr.: Kreditors PBS-nummer
          # - "0241"               - Transkode: 0241 (Tekstlinie)
          # - recnr                - Recordnr.: 001-999
          # - rstkrd!debgrpnr      - Debitorgruppenr.: Debitorgruppenummer
          # - rstdeb!kundenr       - Kundenr.: Debitors kundenummer hos kreditor
          # - 0                    - Aftalenr.: 000000000 eller 999999999
          # - infolin.getinfotekst - Advistekst 1: Tekstlinie på advis
          # - 0.0                  - Advisbeløb 1: Beløb på advis
          # - ""                   - Advistekst 2: Tekstlinie på advis
          # - 0.0                  - Advisbeløb 2: Beløb på advis
          rec += crlf + self.write052(rstkrd.Sektionnr, rstkrd.Pbsnr, "0241", recnr, rstkrd.Debgrpnr, '%s' % (rstdeb.Kundenr), 0, advisline, 0, "", 0)

    # End rstdebs

    # Sektion slut - sektion 0112/117
    # - rstkrd!pbsnr     - PBS-nr.: Kreditors PBS-nummer
    # - rstkrd!sektionnr - Sektionsnr.: 0112/0117 (Betalinger)
    # - rstkrd!debgrpnr  - Debitorgruppenr.: Debitorgruppenummer
    # - antal042         - Antal 042: Antal foranstående 042 records
    # - belob042         - Beløb: Nettobeløb i 042 records
    # - antal052         - Antal 052: Antal foranstående 052 records
    # - antal022         - Antal 022: Antal foranstående 022 records
    rec += crlf + self.write092(rstkrd.Pbsnr, rstkrd.Sektionnr, rstkrd.Debgrpnr, antal042, belob042, antal052, antal022)

    # Leverance slut  - 0601 Betalingsoplysninger
    # - rstkrd.Datalevnr - Dataleverandørnr.: Dataleverandørens SE-nummer
    # - rstkrd.Delsystem - Delsystem:  Dataleverandør delsystem
    # - "0601"           - Leverancetype: 0601 (Betalingsoplysninger)
    # - antalsek         - Antal sektioner: Antal sektioner i leverancen
    # - antal042tot      - Antal 042: Antal foranstående 042 records
    # - belob042tot      - Beløb: Nettobeløb i 042 records
    # - antal052tot      - Antal 052: Antal foranstående 052 records
    # - antal022tot      - Antal 022: Antal foranstående 022 records
    rec += crlf + self.write992(rstkrd.Datalevnr, rstkrd.Delsystem, "0601", antalsek, antal042tot, belob042tot, antal052tot, antal022tot)

    
    pbsforsendelseid  = nextval('Pbsforsendelseid')    
    root_pbsforsendelse = db.Key.from_path('rootPbsforsendelse','root')    
    rec_pbsforsendelse = Pbsforsendelse.get_or_insert('%s' % (pbsforsendelseid), parent=root_pbsforsendelse)
    rec_pbsforsendelse.Id = pbsforsendelseid
    rec_pbsforsendelse.Delsystem = rsttil.Delsystem
    rec_pbsforsendelse.Leverancetype = rsttil.Leverancetype
    rec_pbsforsendelse.Oprettetaf = 'Fak'
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
    rec_pbsfile.countTxtlines()
    rec_pbsfile.put()
    
    sendqueueid = rec_pbsfile.add_to_sendqueue()
    return sendqueueid   
    
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
    

  