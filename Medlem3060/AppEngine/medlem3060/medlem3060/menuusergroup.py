from google.appengine.ext import db 
from models import UserGroup, User, Menu, MenuMenuLink

def deleteMenuAndUserGroup():
  for m in MenuMenuLink.all():
    m.delete()
  for m in Menu.all():
    m.delete()
  for m in UserGroup.all():
    m.delete()

def createMenuAndUserGroup():
  """ Menu """
  root = Menu(Menutext = 'root', Menulink = None, Target = None)
  root.put()
  
  p = Menu(Menutext = 'Medlemmer', Menulink = None, Target = None)
  p.put()
  l = MenuMenuLink(Parent_key = root, Child_key = p, Menuseq = 1)  
  l.put()
  
  c = Menu(Menutext = 'Medlems Opdatering', Menulink = '/adm/findmedlem', Target = 'new', Confirm = False)
  c.put()
  l = MenuMenuLink(Parent_key = p, Child_key = c, Menuseq = 1)  
  l.put()

  c = Menu(Menutext = 'Opret nyt Medlem', Menulink = '/adm/medlem', Target = 'new', Confirm = False)
  c.put()
  l = MenuMenuLink(Parent_key = p, Child_key = c, Menuseq = 2)  
  l.put()


  p = Menu(Menutext = 'Teknik', Menulink = None, Target = None)
  p.put()
  l = MenuMenuLink(Parent_key = root, Child_key = p, Menuseq = 2)  
  l.put()
  
  c = Menu(Menutext = 'Create Menu', Menulink = '/teknik/createmenu', Target = None, Confirm = False)
  c.put()
  l = MenuMenuLink(Parent_key = p, Child_key = c, Menuseq = 1)  
  l.put()

  c = Menu(Menutext = 'Flush Cache', Menulink = '/teknik/flushcache', Target = None, Confirm = False)
  c.put()
  l = MenuMenuLink(Parent_key = p, Child_key = c, Menuseq = 2)  
  l.put()
  
  p = Menu(Menutext = 'Logoff', Menulink = '/logoff', Target = None, Confirm = False)
  p.put()
  l = MenuMenuLink(Parent_key = root, Child_key = p, Menuseq = 99)  
  l.put()
  
  """ UserGroup """
  g = UserGroup(key_name = '0', GroupName = 'Unknown')
  g.put()  

  g = UserGroup(key_name = '1', GroupName = 'Admin')
  g.put()

  qry = User.all().filter('email ==', 'mogens.hafsjold@gmail.com')
  if qry.count() == 1:
    u = qry.fetch(1)[0]
    u.UserGroup_key = db.Key.from_path('UserGroup','1')
    u.put()
