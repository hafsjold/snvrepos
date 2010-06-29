from google.appengine.ext import webapp
from google.appengine.ext.webapp import util
from google.appengine.ext import db 
from google.appengine.ext.webapp import template
from google.appengine.api import users
from google.appengine.api import memcache

import logging
import re
import os

from models import UserGroup, User, Menu, MenuMenuLink
from util import AuthUserGroupPath
    
class MenuHandler(webapp.RequestHandler):
  def get(self):
    usergroup = '0'
    try:
      usergroup = self.request.environ['usergroup']
    except:
      usergroup = '0'
    user_is_admin = False
    try:
      user_is_admin = self.request.environ['user_is_admin'] 
    except:
      user_is_admin = False    
    UserGroup_key = db.Key.from_path('UserGroup',usergroup)
    menu = None
    if user_is_admin:
      menu = memcache.get('Admin', namespace='menu')
      if menu is None:
        menu =  self.getMenu(UserGroup_key, True)
        memcache.set('Admin', menu, namespace='menu')
    else:
      menu = memcache.get(UserGroup_key.id_or_name(), namespace='menu')
      if menu is None:
        menu =  self.getMenu(UserGroup_key, False)
        memcache.set(UserGroup_key.id_or_name(), menu, namespace='menu')
    
    template_values = {
      'menu': menu, 
      'user' : self.request.environ['USER_EMAIL'],
      'host' : self.request.environ['HTTP_HOST']
    }
    path = os.path.join(os.path.dirname(__file__), 'templates/menu.html') 
    self.response.out.write(template.render(path, template_values))

  def getMenu(self, UserGroup_key, user_is_admin ):
    countMenu = 0
    tmpMenu = ''
    wrkMenu = ''
    tmpTree_Menu = ''
    wrkTree_Menu = ''
    
    q = Menu.all().filter('Menutext ==', 'root')
    root = q.fetch(1)[0]
    for objMenuLink in root.menu_child_set.order('Menuseq'):
      wrkMenu = ''
      wrkTree_Menu = ''
      objMenu = objMenuLink.Child_key
      logging.info('Menutext: %s , menu_parent_set.count %s, menu_child_set.count %s' % (objMenu.Menutext, objMenu.menu_parent_set.count(), objMenu.menu_child_set.count()))
      if objMenu.Menulink:
        if not AuthUserGroupPath(objMenu.Menulink, UserGroup_key.id_or_name(), user_is_admin):
          continue
      if objMenu.menu_child_set.count() == 0:
        """No child menues """
        if objMenu.Menulink:
          if objMenu.Target == 'new':
            wrkMenu += '        <TR><TD><P ONCLICK="SubTree(' + "'" + objMenu.Menulink + "', '" + 'new' + "'" + ')"><A HREF="' + '#' + '">' + objMenu.Menutext + '</A></P></TD></TR>\n'
          else:
            wrkMenu += '        <TR><TD><P><A HREF="' + objMenu.Menulink  + '">' + objMenu.Menutext + '</A></P></TD></TR>\n'           

      else:
        for objChildMenu in objMenu.menu_child_set.order('Menuseq'):
          childfound = False
          logging.info('objChildMenu.Child_key: %s' % (objChildMenu.Child_key))
          if objChildMenu.Child_key.Menulink:
            if not AuthUserGroupPath(objChildMenu.Child_key.Menulink, UserGroup_key.id_or_name(), user_is_admin):
              continue
            else:
              childfound = True
          else:
            childfound = True
          
          if childfound:
            if objChildMenu.Child_key.Target == 'new':
              wrkTree_Menu += '        <TR><TD><P ONCLICK="SubTree(' + "'" + objChildMenu.Child_key.Menulink + "', '" + objChildMenu.Child_key.Target + "'" + ')">&nbsp;<A HREF="' + '#' + '">' + objChildMenu.Child_key.Menutext + '</A></P></TD></TR>\n'
            else:
              wrkTree_Menu += '        <TR><TD><P>&nbsp;<A HREF="' + objChildMenu.Child_key.Menulink + '">' + objChildMenu.Child_key.Menutext + '</A></P></TD></TR>\n'
            
        if wrkTree_Menu == '':
          continue
        
      """Menu is now a valid parent"""
      countMenu += 1
      if countMenu == 1: 
        tmpMenu += '  <DIV ID="MainTree">\n'
        tmpMenu += '      <TABLE BORDER="0" WIDTH="100%" BORDERCOLORLIGHT="#FFFF99" BORDERCOLORDARK="#FFFF99"  CELLSPACING="0" STYLE="padding-top: 0; padding-bottom: 0">\n'
      
      if not wrkMenu == '':
        tmpMenu += wrkMenu
      else:
        tmpMenu += '        <TR><TD><P ID="Menu' + '%s' % (objMenu.key().id_or_name()) + '" ONCLICK="SubTreeShow(this)"><A HREF="#">' + objMenu.Menutext + '</A></P></TD></TR>\n'
        tmpTree_Menu += '    <DIV ID="Tree_Menu' + '%s' % (objMenu.key().id_or_name()) + '" STYLE="display: none">\n'
        tmpTree_Menu += '      <TABLE BORDER="0" WIDTH="100%" BORDERCOLORLIGHT="#FFFF99" BORDERCOLORDARK="#FFFF99"  CELLSPACING="0" STYLE="padding-top: 0; padding-bottom: 0">\n'
        tmpTree_Menu += wrkTree_Menu
        tmpTree_Menu += '      </TABLE>\n'
        tmpTree_Menu += '    </DIV>\n'
    
    if countMenu > 0:
      tmpMenu += "      </TABLE>\n"
      tmpMenu += "    </DIV>\n"
    
    return tmpMenu + tmpTree_Menu
    
class ListUserHandler(webapp.RequestHandler):
  def get(self):
    query = User.all()
    usercount = query.count()
    user_list = query.fetch(usercount)
    template_values = {
      'user_list': user_list,
      'usercount': usercount
    }
    path = os.path.join(os.path.dirname(__file__), 'templates/listuser.html') 
    self.response.out.write(template.render(path, template_values))

  def post(self):
    query = User.all()
    usercount = query.count()
    user_list = query.fetch(usercount)
    template_values = {
      'user_list': user_list,
      'usercount': usercount
    }
    path = os.path.join(os.path.dirname(__file__), 'templates/listuser.html') 
    self.response.out.write(template.render(path, template_values))
    
class UserHandler(webapp.RequestHandler):
  def get(self):
    account = self.getAccountfromPath()
    logging.info('getAccountfromPath = %s' % (account))
    try:
      user = User.all().filter('account ==', account)[0]
    except:
      user = False

    logging.info('user = %s' % (user)) 
    
    usergroup = user.UserGroup_key.key().id_or_name()
    grouphtml = ''
    for g in UserGroup.all().order('GroupName'):
      if usergroup == g.key().id_or_name():
        grouphtml += '<option selected value="%s">%s</option>' % (g.key().id_or_name(), g.GroupName) + '\n'
      else:
        grouphtml += '<option value="%s">%s</option>' % (g.key().id_or_name(), g.GroupName) + '\n'
    grouphtml = '<select name="group" size="1">' + grouphtml + '</select>'
    
    xonload = 'fonLoad();'
    if user:
      template_values = {
        'xonload' : xonload,
        'id' : user.key().id_or_name(),
        'user': user,
        'grouphtml' : grouphtml,
      }
    else:
      template_values = {
      }
    
    path = os.path.join(os.path.dirname(__file__), 'templates/user.html') 
    self.response.out.write(template.render(path, template_values))

  def post(self):
    id = self.request.get('id')
    Update = self.request.get('Update')    
    Delete = self.request.get('Delete')
    group = self.request.get('group')
    logging.info('UserHandler post - id: %s, Update: %s, Delete: %s' % (id,Update,Delete))
    
    userkey = db.Key.from_path('User', id) 

    user = db.get(userkey)
    if Update:
      user.UserGroup_key = db.Key.from_path('UserGroup', group)
      db.put(user)
    elif Delete:
      db.delete(user)
    
    xonload = 'UpdateParentAndClose();'
    template_values = {
      'xonload' : xonload,
    }

    
    path = os.path.join(os.path.dirname(__file__), 'templates/user.html') 
    self.response.out.write(template.render(path, template_values))
    
  def getAccountfromPath(self):
    path = self.request.environ['PATH_INFO']
    mo = re.match("/teknik/user/(.+)", path)
    if mo:
      if mo.groups()[0]:
        return mo.groups()[0]
      else:
        return ''
    else:
      return ''