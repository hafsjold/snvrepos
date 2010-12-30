from google.appengine.ext import db
import logging
from datetime import datetime, timedelta, date
  
class UserGroup(db.Model): 
  GroupName = db.StringProperty()
  
class User(db.Model): 
  account = db.StringProperty()
  password = db.StringProperty()
  email = db.EmailProperty()
  UserGroup_key = db.ReferenceProperty(UserGroup, collection_name="usergroup_set")
  
class Menu(db.Model):
  Menutext = db.StringProperty()
  Menulink = db.StringProperty()
  Target = db.StringProperty()
  Confirm = db.BooleanProperty()

class MenuMenuLink(db.Model):
  Parent_key = db.ReferenceProperty(Menu, collection_name="menu_child_set")
  Child_key = db.ReferenceProperty(Menu, collection_name="menu_parent_set")
  Menuseq = db.IntegerProperty()  

class NrSerie(db.Model):
    Name  = db.StringProperty() 
    NextNumber = db.IntegerProperty()

class Kreditor(db.Model): 
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

class Tilpbs(db.Model): 
    Id = db.IntegerProperty()
    Delsystem = db.StringProperty()
    Leverancetype = db.StringProperty()
    Bilagdato = db.DateProperty()
    Pbsforsendelseid = db.IntegerProperty()
    Udtrukket = db.DateTimeProperty()
    Leverancespecifikation = db.StringProperty()
    Leverancedannelsesdato = db.DateTimeProperty()

class Fak(db.Model): 
    Id = db.IntegerProperty()
    Tilpbsid = db.IntegerProperty()
    Betalingsdato = db.DateProperty()
    Nr = db.IntegerProperty()
    Faknr = db.IntegerProperty()
    Advisbelob = db.FloatProperty()
    Infotekst = db.IntegerProperty()
    Fradato = db.DateProperty()
    Tildato = db.DateProperty()

class Kontingent(db.Model): 
    Id = db.IntegerProperty()
    Nr = db.IntegerProperty()
    Advisbelob = db.FloatProperty()
    Fradato = db.DateProperty()
    Tildato = db.DateProperty()
    
    def beregnKontingent(self):
      if self.Fradato.month <= 6:
        self.Tildato = date(self.Fradato.year, 12, 31)
        self.Advisbelob = 150.0
      else:
        self.Tildato = date(self.Fradato.year + 1, 12, 31)
        self.Advisbelob = 225.0         
    
class Sftp(db.Model): 
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
    Id = db.IntegerProperty()
    Navn = db.StringProperty()
    Msgtext = db.TextProperty()
    
class Sysinfo(db.Model):
    Id = db.IntegerProperty() 
    Vkey = db.StringProperty()
    Val = db.StringProperty()
    
class Medlemlog(db.Model): 
    Source = db.IntegerProperty()
    Source_id = db.IntegerProperty()
    Nr = db.IntegerProperty()
    Logdato = db.DateTimeProperty()
    Akt_id = db.IntegerProperty()  
    Akt_dato = db.DateTimeProperty()
    
class Person(db.Model):
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
    MedlemtilDato = db.DateTimeProperty()
    MedlemAabenBetalingsdato = db.DateTimeProperty()
    
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
      
      pDate = datetime.now()
      m_BetalingsFristiDageGamleMedlemmer = 31
      m_BetalingsFristiDageNyeMedlemmer = 61

      qrylog = db.Query(Medlemlog).ancestor(self.key()).order('-Logdato')
      for MedlemLog in qrylog:
        logging.info('XXXXXXXXXXXXXXXXXXXXXXXXXX %s - %s - %s' % (MedlemLog.Logdato, MedlemLog.Akt_id, MedlemLog.Akt_dato))
        if MedlemLog.Akt_id == 10: #Seneste Indmelses dato
          if not m_b10:
            m_b10 = True
            m_indmeldelsesDato = MedlemLog.Akt_dato
        elif MedlemLog.Akt_id == 20: #Seneste PBS opkraevnings dato
          if not m_b20:
            m_b20 = True
            m_opkraevningsDato = MedlemLog.Akt_dato
        elif MedlemLog.Akt_id == 30: # Kontingent betalt til dato
          if m_b30 and not m_b31: #Naest seneste Kontingent betalt til dato
            m_b31 = True
            m_kontingentBetaltDato31 = MedlemLog.Logdato
            m_kontingentTilDato31 = MedlemLog.Akt_dato
          if not m_b30 and not m_b31: #Seneste Kontingent betalt til dato
            m_b30 = True
            m_kontingentBetalingsDato = MedlemLog.Logdato
            m_kontingentBetaltTilDato = MedlemLog.Akt_dato            
        elif MedlemLog.Akt_id == 40: #Seneste PBS betaling tilbagefoert
          if not m_b40:
            m_b40 = True
            m_kontingentTilbagefoertDato = MedlemLog.Akt_dato            
        elif MedlemLog.Akt_id == 50: #Udmeldelses dato
          if not m_b50:
            m_b50 = True
            m_udmeldelsesDato = MedlemLog.Akt_dato

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
          return datetime.now() - timedelta(days=(365 * 30))
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
    qrylog = db.Query(Medlemlog).ancestor(self.key).order('-Logdato')
    for MedlemLog in qrylog:
      logging.info('XXXXXXXXXXXMedlemsStatusXXXXXXXXXXXXXXX %s - %s - %s - %s - %s - %s' % (MedlemLog.Nr, MedlemLog.Source , MedlemLog.Source_id , MedlemLog.Logdato, MedlemLog.Akt_id, MedlemLog.Akt_dato))
      if MedlemLog.Akt_id == 10: #Seneste Indmelses dato
        if not self.b10:
          self.b10 = True
          self.indmeldelsesDato = MedlemLog.Akt_dato
      elif MedlemLog.Akt_id == 20: #Seneste PBS opkraevnings dato
        if not self.b20:
          self.b20 = True
          self.opkraevningsDato = MedlemLog.Akt_dato
      elif MedlemLog.Akt_id == 30: # Kontingent betalt til dato
        if self.b30 and not self.b31: #Naest seneste Kontingent betalt til dato
          self.b31 = True
          self.kontingentBetaltDato31 = MedlemLog.Logdato
          self.kontingentTilDato31 = MedlemLog.Akt_dato
        if not self.b30 and not self.b31: #Seneste Kontingent betalt til dato
          self.b30 = True
          self.kontingentBetalingsDato = MedlemLog.Logdato
          self.kontingentBetaltTilDato = MedlemLog.Akt_dato            
      elif MedlemLog.Akt_id == 40: #Seneste PBS betaling tilbagefoert
        if not self.b40:
          self.b40 = True
          self.kontingentTilbagefoertDato = MedlemLog.Akt_dato            
      elif MedlemLog.Akt_id == 50: #Udmeldelses dato
        if not self.b50:
          self.b50 = True
          self.udmeldelsesDato = MedlemLog.Akt_dato
          
  def MedlemTildato(self):
    #Undersoeg vedr ind- og udmeldelse
    if self.b10: #Findes der en indmeldelse
      if self.b50:  #Findes der en udmeldelse
        if self.udmeldelsesDato >= self.indmeldelsesDato: #Er udmeldelsen aktiv
          return self.udmeldelsesDato
    else:  #Der findes ingen indmeldelse
      return datetime.now() - timedelta(days=(365 * 30))

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
    