from google.appengine.ext import db 
from models import UserGroup, User, Menu, MenuMenuLink

def deleteMenuAndUserGroup():
  try:
    for m in MenuMenuLink.all():
      m.delete()
  except:
    pass
  try:  
    for m in Menu.all():
      m.delete()
  except:
    pass
  try:
    for m in UserGroup.all():
      m.delete()
  except:
    pass

def createMenu(Id, Menutext, Menulink = None, Target = None, Confirm = None):
  m = Menu.get_or_insert('%s' % (Id))
  m.Id = Id
  m.Menutext = Menutext
  m.Menulink = Menulink
  m.Target = Target
  m.Confirm = Confirm
  return m
  
def createMenuMenuLink(Id, Parent_key, Child_key = None, Menuseq = None):
  l = MenuMenuLink.get_or_insert('%s' % (Id))
  l.Id = Id
  l.Parent_key = Parent_key
  l.Child_key = Child_key
  l.Menuseq = Menuseq
  return l  
    
def createMenuAndUserGroup():
  """ UserGroup """
  g = UserGroup(key_name = '0', GroupName = 'Unknown')
  g.put()  

  g = UserGroup(key_name = '1', GroupName = 'Admin')
  g.put()
  
  g = UserGroup(key_name = '2', GroupName = 'SuperUser')
  g.put()
  
  qry = User.all().filter('email ==', 'mogens.hafsjold@gmail.com')
  if qry.count() == 1:
    u = qry.fetch(1)[0]
    u.UserGroup_key = db.Key.from_path('UserGroup','1')
    u.put()
    
  """ Menu """
  root = createMenu(Id = 10, Menutext = 'root', Menulink = None, Target = None, Confirm = None)
  root.put()
  
  p = createMenu(Id = 20, Menutext = 'Medlemmer', Menulink = None, Target = None)
  p.put()
  l = createMenuMenuLink(Id = 10, Parent_key = root, Child_key = p, Menuseq = 1)  
  l.put()
  
  c = createMenu(Id = 30, Menutext = 'Medlems Opdatering', Menulink = '/adm/findmedlem3', Target = 'new', Confirm = False)
  c.put()
  l = createMenuMenuLink(Id = 20, Parent_key = p, Child_key = c, Menuseq = 1)  
  l.put()

  p = createMenu(Id = 40, Menutext = 'Teknik', Menulink = None, Target = None)
  p.put()
  l = createMenuMenuLink(Id = 30, Parent_key = root, Child_key = p, Menuseq = 2)  
  l.put()
  
  c = createMenu(Id = 50, Menutext = 'Create Menu', Menulink = '/teknik/createmenu', Target = None, Confirm = False)
  c.put()
  l = createMenuMenuLink(Id = 40, Parent_key = p, Child_key = c, Menuseq = 1)  
  l.put()

  c = createMenu(Id = 60, Menutext = 'Flush Cache', Menulink = '/teknik/flushcache', Target = None, Confirm = False)
  c.put()
  l = createMenuMenuLink(Id = 50, Parent_key = p, Child_key = c, Menuseq = 2)  
  l.put()

  c = createMenu(Id = 70, Menutext = 'Reindex Medlem', Menulink = '/teknik/reindex', Target = None, Confirm = False)
  c.put()
  l = createMenuMenuLink(Id = 60, Parent_key = p, Child_key = c, Menuseq = 3)  
  l.put()  
  
  c = createMenu(Id = 80, Menutext = 'List User', Menulink = '/teknik/listuser', Target = 'new', Confirm = False)
  c.put()
  l = createMenuMenuLink(Id = 70, Parent_key = p, Child_key = c, Menuseq = 4)  
  l.put()
  
  c = createMenu(Id = 81, Menutext = 'Link Betlin til Fak', Menulink = '/data/linkbetline', Target = None, Confirm = False)
  c.put()
  l = createMenuMenuLink(Id = 71, Parent_key = p, Child_key = c, Menuseq = 4)  
  l.put()
  
  p = createMenu(Id = 90, Menutext = 'Logoff', Menulink = '/logoff', Target = None, Confirm = False)
  p.put()
  l = createMenuMenuLink(Id = 80, Parent_key = root, Child_key = p, Menuseq = 99)  
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
