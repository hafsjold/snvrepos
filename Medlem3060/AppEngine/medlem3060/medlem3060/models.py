# coding=utf-8 
from google.appengine.ext import db
import logging
from datetime import datetime, timedelta, date, tzinfo
from util import lpad, rpad

def first_sunday_on_or_after(dt):
  days_to_go = 6 - dt.weekday()
  if days_to_go:
    dt += timedelta(days_to_go)
  return dt

class CET(tzinfo):
  def __init__(self):
    self.stdoffset = timedelta(hours=1)
    self.reprname = 'Central European Time '
    self.stdname = 'CET'
    self.dstname = 'CST'

  def __repr__(self):
    return self.reprname

  def tzname(self, dt):
    if self.dst(dt):
      return self.dstname
    else:
      return self.stdname

  def utcoffset(self, dt):
    return self.stdoffset + self.dst(dt)

  def dst(self, dt):
    if dt is None or dt.tzinfo is None:
      # An exception may be sensible here, in one or both cases.
      # It depends on how you want to treat them.  The default
      # fromutc() implementation (called by the default astimezone()
      # implementation) passes a datetime with dt.tzinfo is self.
      return ZERO
    assert dt.tzinfo is self

    dststart = datetime(1, 3, 25, 2)
    dstend = datetime(1, 10, 25, 1)
    start = first_sunday_on_or_after(dststart.replace(year=dt.year))
    end = first_sunday_on_or_after(dstend.replace(year=dt.year))

    # Can't compare naive to aware objects, so strip the timezone from
    # dt first.
    if start <= dt.replace(tzinfo=None) < end:
      return timedelta(hours=1)
    else:
      return timedelta(0)
         
class UserGroup(db.Model): 
  GroupName = db.StringProperty()
  
class User(db.Model): 
  account = db.StringProperty()
  password = db.StringProperty()
  email = db.EmailProperty()
  UserGroup_key = db.ReferenceProperty(UserGroup, collection_name="usergroup_set")
  
class Menu(db.Model):
  #key = db.Key.from_path('Menu', '%s' % (Id))
  Id = db.IntegerProperty()
  Menutext = db.StringProperty()
  Menulink = db.StringProperty()
  Target = db.StringProperty()
  Confirm = db.BooleanProperty()

class MenuMenuLink(db.Model):
  #key = db.Key.from_path('MenuMenuLink', '%s' % (Id))
  Id = db.IntegerProperty()
  Parent_key = db.ReferenceProperty(Menu, collection_name="menu_child_set")
  Child_key = db.ReferenceProperty(Menu, collection_name="menu_parent_set")
  Menuseq = db.IntegerProperty()  


class Kreditor(db.Model): 
    #key = db.Key.from_path('rootKreditor','root', 'Kreditor', '%s' % (Id))
    Id = db.IntegerProperty()
    Datalevnr = db.StringProperty()
    Datalevnavn = db.StringProperty()      
    Pbsnr = db.StringProperty()
    Delsystem = db.StringProperty() 
    Regnr = db.StringProperty() 
    Kontonr = db.StringProperty() 
    Debgrpnr = db.StringProperty() 
    Sektionnr = db.StringProperty() 
    Transkodebetaling = db.StringProperty()

class Pbsforsendelse(db.Model): 
    #key = db.Key.from_path('rootPbsforsendelse','root', 'Pbsforsendelse', '%s' % (Id))
    Id = db.IntegerProperty() 
    Delsystem = db.StringProperty()
    Leverancetype = db.StringProperty()
    Oprettetaf = db.StringProperty()
    Oprettet =  db.DateTimeProperty()
    Leveranceid = db.IntegerProperty()
    
class Pbsfiles(db.Model): 
    #key = db.Key.from_path('rootPbsfiles','root', 'Pbsfiles', '%s' % (Id))
    Id = db.IntegerProperty() 
    Type = db.IntegerProperty() 
    Path = db.StringProperty()
    Filename = db.StringProperty()
    Size = db.IntegerProperty() 
    Atime = db.DateTimeProperty()
    Mtime = db.DateTimeProperty()
    Perm = db.StringProperty()
    Uid = db.IntegerProperty() 
    Gid = db.IntegerProperty() 
    Transmittime = db.DateTimeProperty()
    Idlev = db.IntegerProperty()   
    Pbsforsendelseref = db.ReferenceProperty(Pbsforsendelse, collection_name='listPbsfiles')

    
class Pbsfile(db.Model): 
    #key = db.Key.from_path('rootPbsfile','root', 'Pbsfile', '%s' % (Id))
    Id = db.IntegerProperty() 
    Pbsfilesref = db.ReferenceProperty(Pbsfiles, collection_name='listPbsfile')
    Data = db.TextProperty()

    @property
    def Transmisionsdato(self):
      if self.Pbsfilesref.Transmittime:
        return self.Pbsfilesref.Transmittime.date()
      else:
        return datetime.now(CET())
    
    @property
    def SendData(self):
      delsystem = self.Pbsfilesref.Pbsforsendelseref.Delsystem
      transmisionsdato = self.Transmisionsdato
      idlev = self.Pbsfilesref.Idlev
      leveranceid = self.Pbsfilesref.Pbsforsendelseref.Leveranceid
      antal = self.Data.count("\n") + 1
      pbcnet00 = write00(self, delsystem, transmisionsdato, idlev, leveranceid)
      pbcnet90 = write90(self, delsystem, transmisionsdato, idlev, leveranceid, antal)
      return pbcnet00 + "\n" + self.Data + "\n" + pbcnet90
      
    @property
    def TilPBSFilename(self):
      if self.Pbsfilesref.Filename:
        return self.Pbsfilesref.Filename
      else:
        delsystem = self.Pbsfilesref.Pbsforsendelseref.Delsystem
        transmisionsdato = self.Transmisionsdato
        idlev = self.Pbsfilesref.Idlev
        filename = "D"
        filename += lpad(transmisionsdato.strftime("%y%m%d"), 6, '?')
        filename += lpad(idlev, 2, '0')
        filename += "."
        filename += rpad(delsystem, 3, '_')
        return filename
      
    def add_to_sendqueue(self):
      Id = nextval('Sendqueueid')
      recSendqueue = Sendqueue.get_or_insert('%s' % Id)
      recSendqueue.Id = Id
      recSendqueue.Pbsfileref = self
      recSendqueue.Onhold = False
      recSendqueue.put()
      return Id  

      
class Sendqueue(db.Model):
    #key = db.Key.from_path('Sendqueue', '%s' % (Id))
    Id = db.IntegerProperty()
    Pbsfileref = db.ReferenceProperty(Pbsfile, collection_name='listSendqueue')
    Send_to_pbs = db.BooleanProperty(default=False)
    Onhold = db.BooleanProperty(default=True)
        
class Tilpbs(db.Model): 
    #key = db.Key.from_path('rootTilpbs','root', 'Tilpbs', '%s' % (Id))
    Id = db.IntegerProperty()
    Delsystem = db.StringProperty()
    Leverancetype = db.StringProperty()
    Bilagdato = db.DateProperty()
    Pbsforsendelseref = db.ReferenceProperty(Pbsforsendelse, collection_name='listTilpbs')
    Udtrukket = db.DateTimeProperty()
    Leverancespecifikation = db.StringProperty()
    Leverancedannelsesdato = db.DateTimeProperty()

class Fak(db.Model):
    #key = db.Key.from_path('Persons','root','Person','%s' % (Nr), 'Fak', '%s' % (Id))
    Id = db.IntegerProperty()
    TilPbsref = db.ReferenceProperty(Tilpbs, collection_name='listFak')
    Betalingsdato = db.DateProperty()
    Nr = db.IntegerProperty()
    Faknr = db.IntegerProperty()
    Advistekst = db.TextProperty()
    Advisbelob = db.FloatProperty()
    Infotekst = db.IntegerProperty()
    Bogfkonto = db.IntegerProperty(default=1800)
    Vnr = db.IntegerProperty(default=1)
    Fradato = db.DateProperty()
    Tildato = db.DateProperty()
    SFakID = db.IntegerProperty()
    SFaknr = db.IntegerProperty()
    Rykkerdato = db.DateTimeProperty()
    Maildato =  db.DateTimeProperty()
    Rykkerstop =  db.BooleanProperty(default=False)
    Betalt =  db.BooleanProperty(default=False)
    Tilmeldtpbs = db.BooleanProperty(default=False)
    Indmeldelse = db.BooleanProperty(default=False)
    
    def addMedlog(self):
      fakroot = db.Key.from_path('Persons','root','Person','%s' % (self.Nr), 'Fak', '%s' % (self.Id))
      medlog = Medlog.get_or_insert('%s' % (self.Id), parent=fakroot)
      medlog.Id = self.Id
      medlog.Source = 'Fak'
      medlog.Source_id = self.Id
      medlog.Nr = self.Nr
      try:
        medlog.Logdato = self.TilPbsref.Udtrukket
      except:
        pass
      medlog.Akt_id = 20
      medlog.Akt_dato = self.Betalingsdato
      medlog.put()

class Overforsel(db.Model):
    #key = db.Key.from_path('Persons','root','Person','%s' % (Nr), 'Overforsel', '%s' % (Id))
    Id = db.IntegerProperty()
    TilPbsref = db.ReferenceProperty(Tilpbs, collection_name='listOverforsel')
    Nr = db.IntegerProperty()
    SFaknr = db.IntegerProperty()
    SFakID =  db.IntegerProperty()
    Advistekst =  db.StringProperty()
    Advisbelob = db.FloatProperty()
    Emailtekst = db.TextProperty()
    Emailsent = db.BooleanProperty(default=False)
    Bankregnr = db.StringProperty()
    Bankkontonr = db.StringProperty()
    Betalingsdato = db.DateProperty()
    
class Rykker(db.Model):
    #key = db.Key.from_path('Persons','root','Person','%s' % (Nr), 'Rykker', '%s' % (Id))
    Id = db.IntegerProperty()
    TilPbsref = db.ReferenceProperty(Tilpbs, collection_name='listRykker')
    Betalingsdato = db.DateProperty()
    Nr = db.IntegerProperty()
    Faknr = db.IntegerProperty()
    Advistekst = db.TextProperty()
    Advisbelob = db.FloatProperty()
    Infotekst = db.IntegerProperty()
    Rykkerdato = db.DateProperty()
    Maildato = db.DateProperty()
    
class Frapbs(db.Model):
    #key = db.Key.from_path('rootFrapbs','root', 'Frapbs', '%s' % (Id))
    Id = db.IntegerProperty()
    Delsystem = db.StringProperty()
    Leverancetype = db.StringProperty()
    Bilagdato = db.DateProperty()
    Pbsforsendelseref = db.ReferenceProperty(Pbsforsendelse, collection_name='listFrapbs')
    Udtrukket = db.DateTimeProperty()
    Leverancespecifikation = db.StringProperty()
    Leverancedannelsesdato = db.DateTimeProperty()
      
class Bet(db.Model):
    #key = db.Key.from_path('rootBet','root', 'Bet', '%s' % (Id))
    Id = db.IntegerProperty()
    Frapbsref = db.ReferenceProperty(Frapbs, collection_name='listBet')
    Pbssektionnr = db.StringProperty()
    Transkode = db.StringProperty()
    Bogforingsdato = db.DateProperty()
    Indbetalingsbelob = db.FloatProperty()
    Summabogfort = db.BooleanProperty(default=False)
    
class Betlin(db.Model):
    #key = db.Key.from_path('Persons','root','Person','%s' % (Nr), 'Betlin', '%s' % (Id))
    Id = db.IntegerProperty()
    Betref = db.ReferenceProperty(Bet, collection_name='listBetlin')
    Pbssektionnr = db.StringProperty()
    Pbstranskode = db.StringProperty()
    Nr = db.IntegerProperty()
    Faknr = db.IntegerProperty()
    Debitorkonto = db.StringProperty()
    Aftalenr = db.IntegerProperty()
    Betalingsdato = db.DateProperty()
    Belob = db.FloatProperty()
    Indbetalingsdato = db.DateProperty()
    Bogforingsdato = db.DateProperty()
    Indbetalingsbelob = db.FloatProperty()
    Pbskortart = db.StringProperty()
    Pbsgebyrbelob = db.FloatProperty()
    Pbsarkivnr = db.StringProperty()
    
    def addMedlog(self):
      if self.Pbstranskode in ['0236','0237','0297']:      
        fakroot = db.Key.from_path('Persons','root','Person','%s' % (self.Nr), 'Betlin', '%s' % (self.Id))
        medlog = Medlog.get_or_insert('%s' % (self.Id), parent=fakroot)
        medlog.Id = self.Id
        medlog.Source = 'Betlin'
        medlog.Source_id = self.Id
        medlog.Nr = self.Nr
        try:
          medlog.Logdato = self.Betref.Frapbsref.Udtrukket
        except:
          pass
        if self.Pbstranskode in ['0236','0297']:
          qry = Fak.all().filter('Faknr =', self.Faknr)
          for fakrec in qry:
            if fakrec.Nr == self.Nr:
              medlog.Akt_dato = fakrec.Tildato
              medlog.Akt_id = 30
              medlog.put()
              return
        elif self.Pbstranskode in ['0237']:
          medlog.Akt_id = 40
          medlog.Akt_dato = self.Betalingsdato
          medlog.put()
          return

        medlog.delete()
        return
          
class Aftalelin(db.Model):
    #key = db.Key.from_path('Persons','root','Person','%s' % (Nr), 'Aftalelin', '%s' % (Id))
    Id = db.IntegerProperty()
    Frapbsref = db.ReferenceProperty(Frapbs, collection_name='listAftalelin')
    Pbstranskode = db.StringProperty()
    Nr = db.IntegerProperty()
    Debitorkonto = db.StringProperty()
    Debgrpnr = db.StringProperty()
    Aftalenr = db.IntegerProperty()
    Aftalestartdato = db.DateProperty()
    Aftaleslutdato = db.DateProperty()
    Pbssektionnr = db.StringProperty()
    
class Indbetalingskort(db.Model):
    #key = db.Key.from_path('Persons','root','Person','%s' % (Nr), 'Indbetalingskort', '%s' % (Id))
    Id = db.IntegerProperty()
    Frapbsref = db.ReferenceProperty(Frapbs, collection_name='listIndbetalingskort')
    Pbstranskode = db.StringProperty()
    Nr = db.IntegerProperty()
    Faknr = db.IntegerProperty()
    Debitorkonto = db.StringProperty()
    Debgrpnr = db.StringProperty()
    Kortartkode = db.StringProperty()
    Fikreditornr = db.StringProperty()
    Indbetalerident = db.StringProperty()
    Dato = db.DateProperty()
    Belob = db.FloatProperty()
    Pbssektionnr = db.StringProperty()
      
class Kontingent(db.Model):
    #key = db.Key.from_path('Persons','root','Person','%s' % (Nr), 'Kontingent', '%s' % (Id)) 
    Id = db.IntegerProperty()
    Nr = db.IntegerProperty()
    Advisbelob = db.FloatProperty()
    Fradato = db.DateProperty()
    Tildato = db.DateProperty()
    Faktureret = db.BooleanProperty(default=False)
    
    def beregnKontingent(self):
      if self.Fradato.month <= 6:
        self.Tildato = date(self.Fradato.year, 12, 31)
        self.Advisbelob = 150.0
      else:
        self.Tildato = date(self.Fradato.year + 1, 12, 31)
        self.Advisbelob = 225.0  
    
class Sftp(db.Model):
    #key = db.Key.from_path('rootSftp','root', 'Sftp', '%s' % (Id)) 
    Id = db.IntegerProperty()
    Navn = db.StringProperty()
    Host = db.StringProperty()
    Port = db.StringProperty()
    User = db.StringProperty()
    Outbound = db.StringProperty()
    Inbound = db.StringProperty()
    Pincode = db.StringProperty()
    Certificate = db.TextProperty()

class Infotekst(db.Model): 
    #key = db.Key.from_path('rootInfotekst','root', 'Infotekst', '%s' % (Id)) 
    Id = db.IntegerProperty()
    Navn = db.StringProperty()
    Msgtext = db.TextProperty()
    
class Sysinfo(db.Model):
    #key = db.Key.from_path('rootSysinfo','root', 'Sysinfo', '%s' % (Vkey)) 
    Vkey = db.StringProperty()
    Val = db.StringProperty()

class Medlog(db.Model): 
    #key = db.Key.from_path('Persons','root','Person','%s' % (Nr), Source, '%s' % (Source_id),'Medlog', '%s' % (Id)) 
    #key = db.Key.from_path('Persons','root','Person','%s' % (Nr)                            ,'Medlog', '%s' % (Id)) 
    Id = db.IntegerProperty() 
    Source = db.StringProperty()
    Source_id = db.IntegerProperty()
    Nr = db.IntegerProperty()
    Logdato = db.DateTimeProperty()
    Akt_id = db.IntegerProperty()  
    Akt_dato = db.DateProperty()
    
class Person(db.Model):
    #key = db.Key.from_path('Persons','root','Person','%s' % (Nr)) 
    Nr = db.IntegerProperty()
    Navn  = db.StringProperty()
    Kaldenavn  = db.StringProperty()
    Adresse  = db.StringProperty()
    Postnr  = db.StringProperty()
    Bynavn  = db.StringProperty()
    Email = db.EmailProperty()
    Telefon = db.PhoneNumberProperty()
    Kon = db.StringProperty()
    FodtDato  = db.DateProperty()
    Bank = db.StringProperty()
    MedlemtilDato = db.DateProperty()
    MedlemAabenBetalingsdato = db.DateProperty()
    
    def setNameTags(self):
      self.Nr = int(self.key().name())
      recMedlemsStatus = MedlemsStatus(self.key())
      self.MedlemtilDato = recMedlemsStatus.MedlemTildato()
      self.MedlemAabenBetalingsdato = recMedlemsStatus.MedlemAabenBetalingsdato()
      
    def erMedlem(self, bMedlemTilDato=True):
      m_b10 = False
      m_b20 = False
      m_b30 = False
      m_b31 = False
      m_b40 = False
      m_b50 = False
      
      dt = datetime.now()
      pDate = dt.date()
      m_BetalingsFristiDageGamleMedlemmer = 31
      m_BetalingsFristiDageNyeMedlemmer = 61

      qrylog = db.Query(Medlog).ancestor(self.key()).order('-Logdato')
      for Mlog in qrylog:
        logging.info('XXXXXXXXXXXXXXXXXXXXXXXXXX %s - %s - %s' % (Mlog.Logdato, Mlog.Akt_id, Mlog.Akt_dato))
        if Mlog.Akt_id == 10: #Seneste Indmelses dato
          if not m_b10:
            m_b10 = True
            m_indmeldelsesDato = Mlog.Akt_dato
        elif Mlog.Akt_id == 20: #Seneste PBS opkraevnings dato
          if not m_b20:
            m_b20 = True
            m_opkraevningsDato = Mlog.Akt_dato
        elif Mlog.Akt_id == 30: # Kontingent betalt til dato
          if m_b30 and not m_b31: #Naest seneste Kontingent betalt til dato
            m_b31 = True
            m_kontingentBetaltDato31 = Mlog.Logdato
            m_kontingentTilDato31 = Mlog.Akt_dato
          if not m_b30 and not m_b31: #Seneste Kontingent betalt til dato
            m_b30 = True
            m_kontingentBetalingsDato = Mlog.Logdato
            m_kontingentBetaltTilDato = Mlog.Akt_dato            
        elif Mlog.Akt_id == 40: #Seneste PBS betaling tilbagefoert
          if not m_b40:
            m_b40 = True
            m_kontingentTilbagefoertDato = Mlog.Akt_dato            
        elif Mlog.Akt_id == 50: #Udmeldelses dato
          if not m_b50:
            m_b50 = True
            m_udmeldelsesDato = Mlog.Akt_dato

      #Undersoeg vedr ind- og udmeldelse
      if m_b10: #Findes der en indmeldelse
        if m_b50:  #Findes der en udmeldelse
            if m_udmeldelsesDato >= m_indmeldelsesDato: #Er udmeldelsen aktiv
                if bMedlemTilDato:
                  return m_udmeldelsesDato
                else:
                  if m_udmeldelsesDato <= pDate: #Er udmeldelsen aktiv
                    return False
      else:  #Der findes ingen indmeldelse
        if bMedlemTilDato:
          dt = datetime.now() - timedelta(days=(365 * 30))
          return dt.date()
        else:
          return False
        
      #Find aktive betalingsrecord
      if m_b40: #Findes der en kontingent tilbagefoert
        if m_kontingentTilbagefoertDato >= m_kontingentBetalingsDato: #Kontingenttilbagefoert er aktiv
          #''!!!Kontingent er tilbagefoert !!!!!!!!!
          if m_b31:
            m_kontingentBetalingsDato = m_kontingentBetaltDato31
            m_kontingentBetaltTilDato = m_kontingentTilDato31
          else:
            m_b30 = False

      #Undersoeg om der er betalt kontingent
      if m_b30 and m_kontingentBetaltTilDato > m_indmeldelsesDato: #Findes der en betaling efter indmelsesdato
        m_restanceTilDatoGamleMedlemmer = m_kontingentBetaltTilDato + timedelta(days=m_BetalingsFristiDageGamleMedlemmer)
        if bMedlemTilDato:
          return m_restanceTilDatoGamleMedlemmer
        else:
          if m_restanceTilDatoGamleMedlemmer >= pDate: #Er kontingentTilDato aktiv
            return True
          else:
            return False
      else: #Der findes ingen betalinger. Nyt medlem?
        restanceTilDatoNyeMedlemmer = m_indmeldelsesDato + timedelta(days=m_BetalingsFristiDageNyeMedlemmer)
        if bMedlemTilDato:
          return restanceTilDatoNyeMedlemmer
        else:  
          if restanceTilDatoNyeMedlemmer >= pDate: #Er kontingentTilDato aktiv
            return True
          else:
            return False
            
class MedlemsStatus():
  def __init__(self, key):
    self.key = key
    self.b10 = False
    self.indmeldelsesDato = None
    self.b20 = False
    self.opkraevningsDato = None
    self.b30 = False
    self.kontingentBetalingsDato = None
    self.kontingentBetaltTilDato = None 
    self.b31 = False
    self.kontingentBetaltDato31 = None
    self.kontingentTilDato31 = None
    self.b40 = False
    self.kontingentTilbagefoertDato = None
    self.b50 = False
    self.udmeldelsesDato = None          
    self.BetalingsFristiDageGamleMedlemmer = 31
    self.BetalingsFristiDageNyeMedlemmer = 61
    qrylog = db.Query(Medlog).ancestor(self.key).order('-Logdato')
    for Mlog in qrylog:
      logging.info('XXXXXXXXXXXMedlemsStatusXXXXXXXXXXXXXXX %s - %s - %s - %s - %s - %s' % (Mlog.Nr, Mlog.Source , Mlog.Source_id , Mlog.Logdato, Mlog.Akt_id, Mlog.Akt_dato))
      if Mlog.Akt_id == 10: #Seneste Indmelses dato
        if not self.b10:
          self.b10 = True
          self.indmeldelsesDato = Mlog.Akt_dato
      elif Mlog.Akt_id == 20: #Seneste PBS opkraevnings dato
        if not self.b20:
          self.b20 = True
          self.opkraevningsDato = Mlog.Akt_dato
      elif Mlog.Akt_id == 30: # Kontingent betalt til dato
        if self.b30 and not self.b31: #Naest seneste Kontingent betalt til dato
          self.b31 = True
          self.kontingentBetaltDato31 = Mlog.Logdato
          self.kontingentTilDato31 = Mlog.Akt_dato
        if not self.b30 and not self.b31: #Seneste Kontingent betalt til dato
          self.b30 = True
          self.kontingentBetalingsDato = Mlog.Logdato
          self.kontingentBetaltTilDato = Mlog.Akt_dato            
      elif Mlog.Akt_id == 40: #Seneste PBS betaling tilbagefoert
        if not self.b40:
          self.b40 = True
          self.kontingentTilbagefoertDato = Mlog.Akt_dato            
      elif Mlog.Akt_id == 50: #Udmeldelses dato
        if not self.b50:
          self.b50 = True
          self.udmeldelsesDato = Mlog.Akt_dato
          
  def MedlemTildato(self):
    #Undersoeg vedr ind- og udmeldelse
    if self.b10: #Findes der en indmeldelse
      if self.b50:  #Findes der en udmeldelse
        if self.udmeldelsesDato >= self.indmeldelsesDato: #Er udmeldelsen aktiv
          return self.udmeldelsesDato
    else:  #Der findes ingen indmeldelse
      dt = datetime.now() - timedelta(days=(365 * 30)) 
      return dt.date()

    #Find aktive betalingsrecord
    if self.b40: #Findes der en kontingent tilbagefoert
      if self.kontingentTilbagefoertDato >= self.kontingentBetalingsDato: #Kontingenttilbagefoert er aktiv
        #''!!!Kontingent er tilbagefoert !!!!!!!!!
        if self.b31:
          self.kontingentBetalingsDato = self.kontingentBetaltDato31
          self.kontingentBetaltTilDato = self.kontingentTilDato31
        else:
          self.b30 = False

    #Undersoeg om der er betalt kontingent
    if self.b30 and self.kontingentBetaltTilDato > self.indmeldelsesDato: #Findes der en betaling efter indmelsesdato
      restanceTilDatoGamleMedlemmer = self.kontingentBetaltTilDato + timedelta(days=self.BetalingsFristiDageGamleMedlemmer)
      return restanceTilDatoGamleMedlemmer
    else: #Der findes ingen betalinger. Nyt medlem?
      restanceTilDatoNyeMedlemmer = self.indmeldelsesDato + timedelta(days=self.BetalingsFristiDageNyeMedlemmer)
      return restanceTilDatoNyeMedlemmer

  def MedlemAabenBetalingsdato(self):
    #Undersoeg om der findes ubetalt opkraevning hvis medlem
    if self.b20:
      if self.b30:
        if self.kontingentBetaltTilDato < self.opkraevningsDato:
          return self.opkraevningsDato
        else:
          return None
      else:
        return self.opkraevningsDato
    else:
      return None

      
class NrSerie(db.Model):
    #key = db.Key.from_path('NrSerie', '%s' % (Nrserienavn))
    Nrserienavn  = db.StringProperty() 
    Sidstbrugtenr = db.IntegerProperty()

class NrSerieError(Exception):
  def __init__(self, value):
    self.value = value
  def __str__(self):
    return repr(self.value)
    
def nextval(nrserie):
  if not nrserie in NrSeries:
    raise NrSerieError('Nummer Serie %s findes ikke' % (nrserie))
  
  recNrSerie = NrSerie.get_by_key_name(nrserie)
  if recNrSerie is None:
    recNrSerie = NrSerieSetup(nrserie)
  
  if recNrSerie.Sidstbrugtenr:
    nr = TestRange(nrserie, recNrSerie.Sidstbrugtenr +1)
    recNrSerie.Sidstbrugtenr = nr
    recNrSerie.put()  
  else:
    nr = 1    
  return nr

def NrSerieSetupAll():
  for k, v in NrSeries.iteritems():
    print k, NrSerieSetup(k).Sidstbrugtenr 

def NrSerieSetup(nrserie):
  if not nrserie in NrSeries:
    raise NrSerieError('Nummer Serie %s findes ikke' % (nrserie))
  table = NrSeries[nrserie][0]
  field = NrSeries[nrserie][1]
  minval = NrSeries[nrserie][2]
  maxval = NrSeries[nrserie][3]

  try:
    maxval = TestRange(nrserie)
  except:
    maxval = minval
  
  recNrSerie = NrSerie.get_or_insert(nrserie)
  recNrSerie.Nrserienavn = nrserie
  recNrSerie.Sidstbrugtenr = maxval
  recNrSerie.put()
  return recNrSerie


def TestRange(nrserie, startval = 0):
  if not nrserie in NrSeries:
    raise NrSerieError('Nummer Serie %s findes ikke' % (nrserie))
  table = NrSeries[nrserie][0]
  field = NrSeries[nrserie][1]
  minval = NrSeries[nrserie][2]
  maxval = NrSeries[nrserie][3]
  if startval == 0:
    start = minval
  else:
    start = startval
  dif = 100
  if nrserie == 'idlev':
    if start > maxval:
      return minval
    else:
      return start
      
  pf = []
  while len(pf) == 0:
    if nrserie == 'Medlogid':
      sql = "SELECT * FROM %s WHERE Source = 'Medlog' AND %s >= %s AND %s <= %s" % (table ,field, start, field, start + dif) 
    else:
      sql = "SELECT * FROM %s WHERE %s >= %s AND %s <= %s" % (table ,field, start, field, start + dif) 
    try:
      p = db.GqlQuery(sql).fetch(dif + 2)
      usedId = [getattr(p, field) for p in p]
    except:
      return minval
    def f(x):
      return not x in usedId
    pf = filter(f,range(start,start + dif))
    if len(pf) == 0:
      start += dif
    if start > maxval:
      return maxval    
  
  p2 = sorted(pf)
  logging.info('nrserie %s : %s' % (nrserie, NrSeries[nrserie]))
  return p2[0]
   
NrSeries= {
   'Personid'          :['Person','Nr',600, 999]
  ,'Medlogid'          :['Medlog','Id',800, 9999]
  ,'Kontingentid'      :['Kontingent','Id',1,999]
  ,'Fakid'             :['Fak','Id',31070,39999]  
  ,'Overforselid'      :['Overforsel','Id',18,9999]  
  ,'Rykkerid'          :['Rykker','Id',30663,39999]  
  ,'faknr'             :['Fak','Faknr',11084,19999] 
  ,'leveranceid'       :['Pbsforsendelse','Pbsforsendelse',590,9999] 
  ,'Pbsforsendelseid'  :['Pbsforsendelse','Id',800,9999]  
  ,'Tilpbsid'          :['Tilpbs','Id',602,9999]  
  ,'Pbsfilesid'        :['Pbsfiles','Id',1130,9999]  
  ,'Frapbsid'          :['Frapbs','Id',140,9999]  
  ,'Betid'             :['Bet','Id',70,9999]  
  ,'Betlinid'          :['Betlin','Id',360,9999]
  ,'Aftalelinid'       :['Aftalelin','Id',235,9999]    
  ,'Indbetalingskortid':['Indbetalingskort','Id',82,9999]    
  ,'Kreditorid'        :['Kreditor','Id',5,9999]  
  ,'Menuid'            :['Menu','Id',200,9999]  
  ,'MenuMenuLinkid'    :['MenuMenuLink','Id',200,9999]  
  ,'Sendqueueid'       :['Sendqueue','Id',1,9999] 
  ,'idlev'             :['idlev','idlev',0,99]    
}
 
def write00(self, delsystem, transmisionsdato, idlev, idfri):
  rec = "PBCNET00"
  rec += lpad(delsystem, 3, '?')
  rec += rpad("", 1, ' ')
  rec += lpad(transmisionsdato.strftime("%y%m%d"), 6, '?')
  rec += lpad(idlev, 2, '0')
  rec += rpad("", 2, ' ')
  rec += lpad(idfri, 6, '0')
  rec += rpad("", 8, ' ')
  rec += rpad("", 8, ' ')
  return rec;

def write90(self, delsystem, transmisionsdato, idlev, idfri, antal):
  rec = "PBCNET90"
  rec += lpad(delsystem, 3, '?')
  rec += rpad("", 1, ' ')
  rec += lpad(transmisionsdato.strftime("%y%m%d"), 6, '?')
  rec += lpad(idlev, 2, '0')
  rec += rpad("", 2, ' ')
  rec += lpad(idfri, 6, '0')
  rec += lpad(antal, 6, '0')
  return rec