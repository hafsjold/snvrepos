from google.appengine.ext import webapp
from google.appengine.ext.webapp import util
from google.appengine.ext import db 
from google.appengine.ext.webapp import template
from google.appengine.api import users
from google.appengine.api import memcache

import logging
import re
import os

from models import UserGroup, User, Menu, MenuMenuLink, MenuUserGroupLink
    
class MenuHandler(webapp.RequestHandler):
  def get(self):
    UserGroup_key = db.Key.from_path('UserGroup','1')
    menu = None
    menu = memcache.get(UserGroup_key.id_or_name(), namespace='menu')
    if menu is None:
      menu =  self.getMenu(UserGroup_key)
      memcache.set(UserGroup_key.id_or_name(), menu, namespace='menu')
    template_values = {
      'menu': menu,
      'user' : self.request.environ['USER_EMAIL'],
      'host' : self.request.environ['HTTP_HOST']
    }
    path = os.path.join(os.path.dirname(__file__), 'templates/menu.html') 
    self.response.out.write(template.render(path, template_values))

  def getMenu(self, UserGroup_key):
    countMenu = 0
    tmpMenu = ''
    tmpTree_Menu = ''
    wrkTree_Menu = ''
    
    for objMenu in Menu.all():
      logging.info('Menutext: %s , menu_parent_set.count %s, menu_child_set.count %s' % (objMenu.Menutext, objMenu.menu_parent_set.count(), objMenu.menu_child_set.count()))
      if objMenu.menu_parent_set.count() == 0:
        logging.info('objMenu.menu_parent_set.count() == 0')        
        if objMenu.Secure:
          qryUserGroup = objMenu.menu_user_set.filter('UserGroup_key', UserGroup_key)
          if qryUserGroup.count() == 0:
            continue
        if objMenu.menu_child_set.count() == 0:
          """No child menues """
          if objMenu.Menulink:
            if objMenu.Target == 'new':
              tmpMenu += '        <TR><TD><P ONCLICK="SubTree(' + "'" + objMenu.Menulink + "', '" + 'new' + "'" + ')"><A HREF="' + '#' + '">' + objMenu.Menutext + '</A></P></TD></TR>\n'
            else:
              tmpMenu += '        <TR><TD><P><A HREF="' + objMenu.Menulink  + '">' + objMenu.Menutext + '</A></P></TD></TR>\n'           
          continue
        else:
          wrkTree_Menu = ''
          for objChildMenu in objMenu.menu_child_set:
            childfound = False
            logging.info('objChildMenu.Child_key: %s' % (objChildMenu.Child_key))
            if objChildMenu.Child_key.Secure:
              qryChildUserGroup = objChildMenu.Child_key.menu_user_set.filter('UserGroup_key', UserGroup_key)
              if qryChildUserGroup.count() == 0:
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
        
        """Level-0 Start"""
        tmpTree_Menu += '    <DIV ID="Tree_Menu' + objMenu.key().id_or_name() + '" STYLE="display: none">\n'
        tmpTree_Menu += '      <TABLE BORDER="0" WIDTH="100%" BORDERCOLORLIGHT="#FFFF99" BORDERCOLORDARK="#FFFF99"  CELLSPACING="0" STYLE="padding-top: 0; padding-bottom: 0">\n'
        tmpMenu += '        <TR><TD><P ID="Menu' + objMenu.key().id_or_name() + '" ONCLICK="SubTreeShow(this)"><A HREF="#">' + objMenu.Menutext + '</A></P></TD></TR>\n'
        
        tmpTree_Menu += wrkTree_Menu
        
        """Level-0 End"""
        tmpTree_Menu += '      </TABLE>\n'
        tmpTree_Menu += '    </DIV>\n'

      else:
        continue 
    
    if countMenu > 0:
      tmpMenu += "      </TABLE>\n"
      tmpMenu += "    </DIV>\n"
    
    return tmpMenu + tmpTree_Menu
