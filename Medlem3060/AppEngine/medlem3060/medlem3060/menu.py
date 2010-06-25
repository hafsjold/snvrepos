from google.appengine.ext import webapp
from google.appengine.ext.webapp import util
from google.appengine.ext import db 
from google.appengine.ext.webapp import template
from google.appengine.api import users

import logging
import re
import os

from models import UserGroup, User, Menu, MenuMenuLink, MenuUserGroupLink

class IndexHandler(webapp.RequestHandler):
  def get(self):
    template_values = {}
    path = os.path.join(os.path.dirname(__file__), 'templates/index1.html') 
    self.response.out.write(template.render(path, template_values))
    
class Main2Handler(webapp.RequestHandler):
  def get(self):
    template_values = {}
    path = os.path.join(os.path.dirname(__file__), 'templates/main.html') 
    self.response.out.write(template.render(path, template_values))
    
class MenuHandler(webapp.RequestHandler):
  def get(self):
    countMenu = 0
    tmpMenu = ''
    tmpTree_Menu = ''
    wrkTree_Menu = ''
    UserGroup_key = db.Key.from_path('UserGroup','1')
    
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
              wrkTree_Menu += '  <tr><td><p ONCLICK="SubTree(' + "'" + objChildMenu.Child_key.Menulink + "', '" + objChildMenu.Child_key.Target + "'" + ')">&nbsp;<a href="' + '#' + '">' + objChildMenu.Child_key.Menutext + '</a></p></tr></td>\n'
            
          if wrkTree_Menu == '':
            continue
        
        """Menu is now a valid parent"""
        countMenu += 1
        if countMenu == 1: 
          tmpMenu += '<DIV ID="MainTree">\n'
          tmpMenu +=  '<table border="0" width="100%" bordercolorlight="#FFFF99" bordercolordark="#FFFF99"  cellspacing="0" style="padding-top: 0; padding-bottom: 0">\n'
        
        """Level-0 Start"""
        tmpTree_Menu += '<DIV ID="Tree_Menu' + objMenu.key().id_or_name() + '" STYLE="display: none">\n'
        tmpTree_Menu += '<table border="0" width="100%" bordercolorlight="#FFFF99" bordercolordark="#FFFF99"  cellspacing="0" style="padding-top: 0; padding-bottom: 0">\n'
        tmpMenu += '  <tr><td><p ID="Menu' + objMenu.key().id_or_name() + '" ONCLICK="SubTreeShow(this)"><a href="#">' + objMenu.Menutext + '</a></p></tr></td>\n'
        
        tmpTree_Menu += wrkTree_Menu
        
        """Level-0 End"""
        tmpTree_Menu += '</TABLE></DIV>\n'

      else:
        continue 
    
    if countMenu > 0:
      tmpMenu += "</TABLE></DIV>\n"    
    
    template_values = {
      'Tree_Menu': tmpTree_Menu,
      'Menu':tmpMenu
    }
    path = os.path.join(os.path.dirname(__file__), 'templates/menu.html') 
    self.response.out.write(template.render(path, template_values))