from google.appengine.ext import db 
  
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
  Secure = db.BooleanProperty()

class MenuUserGroupLink(db.Model): 
  UserGroup_key = db.ReferenceProperty(UserGroup, collection_name="usergroup_menu_set")
  Menu_key = db.ReferenceProperty(Menu, collection_name="menu_user_set")

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
    Tags = db.ListProperty(basestring)
    
    def setNameTags(self):
      tokens = [] 
      for word in self.Navn.split():
        tokens.append('N%s' % (word.strip('.,').lower()))
      for word in self.Kaldenavn.split():
        tokens.append('N%s' % (word.strip('.,').lower()))
      for word in self.Adresse.split():
        tokens.append('A%s' % (word.strip('.,').lower()))
      for word in self.Bynavn.split():
        tokens.append('B%s' % (word.strip('.,').lower()))
      self.Tags = tokens
      self.put()
      logging.info('tokens: %s' % (tokens))

class Medlemlog(db.Model): 
    Medlem_key = db.ReferenceProperty(Medlem, collection_name="medlemlog_set")
    Source = db.IntegerProperty()
    Source_id = db.IntegerProperty()
    Nr = db.IntegerProperty()
    Logdato = db.DateTimeProperty()
    Akt_id = db.IntegerProperty()  
    Akt_dato = db.DateTimeProperty()
