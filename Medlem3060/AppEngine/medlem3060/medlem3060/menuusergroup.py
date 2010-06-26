from google.appengine.ext import db 
from models import UserGroup, Menu, MenuMenuLink, MenuUserGroupLink

def deleteMenuAndUserGroup():
  for m in MenuUserGroupLink.all():
    m.delete()
  for m in MenuMenuLink.all():
    m.delete()
  for m in Menu.all():
    m.delete()
  for m in UserGroup.all():
    m.delete()

def createMenuAndUserGroup():
  """ Menu """
  p = Menu(key_name = '1'
      ,Menutext = 'Medlemmer'
      ,Menulink = None
      ,Target = None
      ,Confirm = False
      ,Secure = True)
  p.put()

  c = Menu(key_name = '2'
      ,Menutext = 'Medlems Opdatering'
      ,Menulink = '/adm/findmedlem'
      ,Target = 'new'
      ,Confirm = False
      ,Secure = True)
  c.put()

  l = MenuMenuLink(key_name = '1'
      ,Parent_key = p
      ,Child_key = c
      ,Menuseq = 1)  
  l.put()

  c = Menu(key_name = '33'
      ,Menutext = 'Opret nyt Medlem'
      ,Menulink = '/adm/medlem'
      ,Target = 'new'
      ,Confirm = False
      ,Secure = True)
  c.put()

  l = MenuMenuLink(key_name = '3'
     ,Parent_key = p
     ,Child_key = c
     ,Menuseq = 2)  
  l.put()

  c = Menu(key_name = '4101'
      ,Menutext = 'Create Menu'
      ,Menulink = '/createmenu'
      ,Target = None
      ,Confirm = False
      ,Secure = True)
  c.put()

  l = MenuMenuLink(key_name = '102'
     ,Parent_key = p
     ,Child_key = c
     ,Menuseq = 3)  
  l.put()
  
  p = Menu(key_name = '201'
      ,Menutext = 'Logoff'
      ,Menulink = '/logoff'
      ,Target = None
      ,Confirm = False
      ,Secure = False)
  p.put()

  """ UserGroup """
  g = UserGroup(key_name = '1'
      ,Menutext = 'Admin')
  g.put()  
  
  mu = MenuUserGroupLink(key_name = '1'
       ,UserGroup_key = g
       ,Menu_key = db.Key.from_path('Menu','1'))
  mu.put()
    
  mu = MenuUserGroupLink(key_name = '2'
       ,UserGroup_key = g
       ,Menu_key = db.Key.from_path('Menu','2'))
  mu.put()  
      
  mu = MenuUserGroupLink(key_name = '3'
       ,UserGroup_key = g
       ,Menu_key = db.Key.from_path('Menu','33'))
  mu.put() 
      
  mu = MenuUserGroupLink(key_name = '4'
       ,UserGroup_key = g
       ,Menu_key = db.Key.from_path('Menu','4101'))
  mu.put()    