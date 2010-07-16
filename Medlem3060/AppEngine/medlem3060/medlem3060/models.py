from google.appengine.ext import db
import logging
  
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
    
class Medlem(db.Model): 
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
    NavnTags = db.ListProperty(basestring)
    AdresseTags = db.ListProperty(basestring)
    BynavnTags = db.ListProperty(basestring)
    
    tokens = []
    
    def addtoken(self, token):
      for found in (t for t in self.tokens if t == '%s' % (token)):
        break
      else:
        self.tokens.append('%s' % (token))
    
    def setNameTags(self):
      self.tokens = [] 
      for w in (self.Navn + ' ' + self.Kaldenavn).lower().replace('.',' ').replace(',',' ').split():
        self.addtoken(w)
        for l in range(1, len(w), 1):
          for i in range(0, len(w) +1 - l, 1):
            self.addtoken(w[i:i+l])
      self.NavnTags = self.tokens
      logging.info('%s' % (self.tokens))

      self.tokens = []
      for w in self.Adresse.lower().replace('.',' ').replace(',',' ').split():
        self.addtoken(w)
        for l in range(1, len(w), 1):
          for i in range(0, len(w) +1 - l, 1):
            self.addtoken(w[i:i+l])
      self.AdresseTags = self.tokens
      logging.info('%s' % (self.tokens))

      self.tokens = [] 
      for w in (self.Bynavn + ' ' + self.Postnr).lower().replace('.',' ').replace(',',' ').split():
        self.addtoken(w)
        for l in range(1, len(w), 1):
          for i in range(0, len(w) +1 - l, 1):
            self.addtoken(w[i:i+l])
      self.BynavnTags = self.tokens
      logging.info('%s' % (self.tokens))

      self.put()

class Medlemlog(db.Model): 
    Medlem_key = db.ReferenceProperty(Medlem, collection_name="medlemlog_set")
    Source = db.IntegerProperty()
    Source_id = db.IntegerProperty()
    Nr = db.IntegerProperty()
    Logdato = db.DateTimeProperty()
    Akt_id = db.IntegerProperty()  
    Akt_dato = db.DateTimeProperty()
