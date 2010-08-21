from google.appengine.ext import db
import logging
from datetime import datetime, timedelta
  
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
    
    def setNameTags(self):
      self.Nr = int(self.key().name())
      self.MedlemtilDato = self.erMedlem()
      
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