from google.appengine.ext import webapp
from google.appengine.ext.webapp import util
from google.appengine.ext import db 
from google.appengine.ext.webapp import template
from google.appengine.api import users
from google.appengine.api.labs import taskqueue
from google.appengine.api import memcache

import logging
import rest
import os
import re

from models import UserGroup, User, Menu, MenuMenuLink, Medlem, Medlemlog
from util import TestCrypt, COOKIE_NAME, LOGIN_URL, CreateCookieData, SetUserInfoCookie
from menuusergroup import deleteMenuAndUserGroup, createMenuAndUserGroup
from menu import MenuHandler, ListUserHandler, UserHandler

webapp.template.register_template_library('templatetags.medlem3060_extras')

class MainHandler(webapp.RequestHandler):
    def get(self):
        #TestCrypt('Mogens Hafsjold')
        #self.response.out.write('Hello medlem3060!')
        self.redirect("/adm")
        
class LoginHandler(webapp.RequestHandler):
  def get(self):
    user = self.request.environ['user3060']
    userAuth = self.request.environ['userAuth']
    template_values = {
      'account': user,
      'login_message': 'Login',
      'method': 'post',
      'login_url': LOGIN_URL,
      'continue_url': '/test', 
    }
    path = os.path.join(os.path.dirname(__file__), 'templates/login.html') 
    self.response.out.write(template.render(path, template_values))
  
  def post(self):
    user = self.request.get('account') 
    password = self.request.get('password')
    continue_url = self.request.get('continue')
    action = self.request.get('action')
    logging.info('XXXXXXXXXXXX action: %s , continue_url: %s ' % (action, continue_url))
    if action == 'Login':
      if password == 'Ok':
        self.request.environ['userAuth'] = True
        sessioncookie = SetUserInfoCookie(COOKIE_NAME, CreateCookieData(user), '3600')
        self.response.headers['Set-Cookie'] = sessioncookie
        assert continue_url, "continue_url is nothing"
        self.redirect(continue_url)
      else:
        self.redirect(LOGIN_URL)
        
    elif action == 'Logout':
      sessioncookie = SetUserInfoCookie(COOKIE_NAME, '', '0')
      self.response.headers['Set-Cookie'] = sessioncookie
      self.redirect('/')

class FindmedlemHandler(webapp.RequestHandler):
  def get(self):
    template_values = {
    }
    path = os.path.join(os.path.dirname(__file__), 'templates/findmedlem.html') 
    self.response.out.write(template.render(path, template_values))
    #taskqueue.add(url='/_ah/queue/default', params={'Nr':'all'})
  
  def post(self):
    SNr = self.request.get('SNr').strip() 
    SNavn = self.request.get('SNavn').strip() 
    SAdresse = self.request.get('SAdresse').strip()  
    SBy = self.request.get('SBy').strip()  
    
    query = Medlem.all()
    if SNr:
      query = query.filter('Nr =', int(SNr))
    if SNavn:
      query = query.filter('Tags =', 'N%s' % (SNavn.lower()))
    if SAdresse:
      query = query.filter('Tags =', 'A%s' % (SAdresse.lower()))
    if SBy:
      query = query.filter('Tags =', 'B%s' % (SBy.lower()))
 
    medlem_list = query.fetch(1000)
 
    template_values = {
      'medlem_list': medlem_list,
    }
    path = os.path.join(os.path.dirname(__file__), 'templates/findmedlem.html') 
    self.response.out.write(template.render(path, template_values))
    
class MedlemHandler(webapp.RequestHandler):
  def getNrfromPath(self):
    path = self.request.environ['PATH_INFO']
    mo = re.match("/adm/medlem/([0-9]+)", path)
    if mo:
      if mo.groups()[0]:
        return int(mo.groups()[0])
      else:
        return int(0)
    else:
      return int(0)
  
  def get(self):
    Nr = self.getNrfromPath()
    logging.info('getNrfromPath = %s' % (Nr))
    try:
      m = Medlem.all().filter('Nr =', Nr)[0]
    except:
      m = False
    
    xonload = 'fonLoad();'
    
    if m:
      template_values = {
        'xonload' : xonload,
        'kontingent_fra_dato': '1. januar 2010',
        'kontingent_til_dato': '31. december 2010',
        'kontingent': '150,00',
        'Nr': m.Nr,
        'Navn': m.Navn,
        'Kaldenavn': m.Kaldenavn,
        'Adresse': m.Adresse,
        'Postnr': m.Postnr,
        'Bynavn': m.Bynavn,
        'Telefonnr': m.Telefon,
        'Email': m.Email,
        'Kon': m.Kon,
        'Fodelsdato': m.FodtDato,
        'Bank': m.Bank,
      }
    else:
      template_values = {
        'xonload' : xonload,
        'kontingent_fra_dato': '1. januar 2010',
        'kontingent_til_dato': '31. december 2010',
        'kontingent': '150,00',
        'Nr': '',
        'Navn': '',
        'Kaldenavn': '',
        'Adresse': '',
        'Postnr': '',
        'Bynavn': '',
        'Telefonnr': '',
        'Email': '',
        'Kon': '',
        'Fodelsdato': '',
        'Bank': '',
      }
    
    path = os.path.join(os.path.dirname(__file__), 'templates/medlem.html') 
    self.response.out.write(template.render(path, template_values))
    
  def post(self):
    Nr = self.request.get('Nr') 
    Navn = self.request.get('Navn') 
    Kaldenavn = self.request.get('Kaldenavn')
    Adresse = self.request.get('Adresse')
    Postnr = self.request.get('Postnr')
    Bynavn = self.request.get('Bynavn')
    Telefonnr = self.request.get('Telefonnr')
    Email = self.request.get('Email')
    Kon = self.request.get('Kon')
    Fodelsdato = self.request.get('Fodelsdato')
    Bank = self.request.get('Bank')
    logging.info('Navn: %s, Kaldenavn: %s' % (Navn, Kaldenavn))
    xonload = 'UpdateParentAndClose();'
    template_values = {
      'xonload' : xonload,
      'kontingent_fra_dato': '1. januar 2010',
      'kontingent_til_dato': '31. december 2010',
      'kontingent': '150,00',
      'Nr': Nr,
      'Navn': Navn,
      'Kaldenavn': Kaldenavn,
      'Adresse': Adresse,
      'Postnr': Postnr,
      'Bynavn': Bynavn,
      'Telefonnr': Telefonnr,
      'Email': Email,
      'Kon': Kon,
      'Fodelsdato': Fodelsdato,
      'Bank': Bank,
    }
    path = os.path.join(os.path.dirname(__file__), 'templates/medlem.html') 
    self.response.out.write(template.render(path, template_values))
    
class LogoffHandler(webapp.RequestHandler):
  def get(self):
    self.redirect(users.create_logout_url("/"))

class SearchIndexing(webapp.RequestHandler):
    """Handler for full text indexing task."""
    def post(self):
      query = Medlem.all()
      medlem_list = query.fetch(1000)
      for medlem in medlem_list:
        medlem.setNameTags()

class CreateMenu(webapp.RequestHandler):
    """Handler for create Menu"""
    def get(self):
      deleteMenuAndUserGroup()
      createMenuAndUserGroup()
      memcache.flush_all()
      self.redirect("/adm")
      
class FlushCache(webapp.RequestHandler):
    """Handler for Flush Cache"""
    def get(self):
      memcache.flush_all()
      self.redirect("/adm")

application = webapp.WSGIApplication([ ('/', MainHandler),
                                       (LOGIN_URL, LoginHandler),
                                       ('/adm/medlem.*', MedlemHandler),
                                       ('/adm/findmedlem', FindmedlemHandler),
                                       ('/adm', MenuHandler),
                                       ('/rest/.*', rest.Dispatcher),
                                       ('/logoff', LogoffHandler),
                                       ('/teknik/createmenu', CreateMenu),
                                       ('/teknik/flushcache', FlushCache),
                                       ('/teknik/listuser', ListUserHandler),
                                       ('/teknik/user/.*',UserHandler),
                                       ('/_ah/queue/default', SearchIndexing) ],
                                     debug=True )

rest.Dispatcher.base_url = "/rest"
rest.Dispatcher.add_models_from_module('models')

def templateTest(myvar):
  return myvar

def main():
    util.run_wsgi_app(application)

if __name__ == '__main__':
    main()
