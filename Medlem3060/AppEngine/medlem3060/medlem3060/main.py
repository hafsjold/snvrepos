from google.appengine.ext import webapp
from google.appengine.ext.webapp import util
from google.appengine.ext import db 
from google.appengine.ext.webapp import template
from google.appengine.api import users
from google.appengine.api.labs import taskqueue
from google.appengine.api import memcache
from xml.dom import minidom
from datetime import datetime

import logging
import rest
import os
import re

from models import UserGroup, User, Menu, MenuMenuLink, Medlemlog, Person
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

  
  def post(self):
    SNr = self.request.get('SNr').strip() 
    SNavn = self.request.get('SNavn').strip() 
    SAdresse = self.request.get('SAdresse').strip()  
    SBy = self.request.get('SBy').strip()  
    
    query = Person.all()
    if SNr:
      query = query.filter('Nr =', int(SNr))
    if SNavn:
      query = query.filter('NavnTags =', '%s' % (SNavn.lower()))
    if SAdresse:
      query = query.filter('AdresseTags =', '%s' % (SAdresse.lower()))
    if SBy:
      query = query.filter('BynavnTags =', '%s' % (SBy.lower()))
 
    query = query.order('Nr')
    person_list = query.fetch(50)
 
    template_values = {
      'person_list': person_list,
    }
    path = os.path.join(os.path.dirname(__file__), 'templates/findmedlem.html') 
    self.response.out.write(template.render(path, template_values))
    
class MedlemHandler(webapp.RequestHandler):
  def getNrfromPath(self):
    path = self.request.environ['PATH_INFO']
    mo = re.match("/adm/medlem/([0-9]+)", path)
    if mo:
      if mo.groups()[0]:
        return mo.groups()[0]
      else:
        return None
    else:
      return None
  
  def get(self):
    Nr = self.getNrfromPath()
    logging.info('getNrfromPath = %s' % (Nr))
    try:
      k = db.Key.from_path('Persons','root','Person',Nr)
      m = Person.get(k)
    except:
      m = False
    Ermedlem = m.erMedlem()
    log_list = db.Query(Medlemlog).ancestor(k).order('-Logdato')
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
        'Ermedlem': Ermedlem,
        'log_list': log_list,
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
        'Ermedlem': None,
        'log_list': None,
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
    
class SyncMedlemHandler(webapp.RequestHandler):
  def post(self):
    doc = minidom.parse(self.request.body_file)
    try:
      Nr = doc.getElementsByTagName("Nr")[0].childNodes[0].data
    except:
      Nr = None
    root = db.Key.from_path('Persons','root')
    person = Person.get_or_insert('%s' % (Nr), parent=root)
    try:
      person.Navn = doc.getElementsByTagName("Navn")[0].childNodes[0].data
    except:
      person.Navn = None
    try:
      person.Kaldenavn = doc.getElementsByTagName("Kaldenavn")[0].childNodes[0].data
    except:
      Kaldenavn = None
    try:
      person.Adresse = doc.getElementsByTagName("Adresse")[0].childNodes[0].data
    except:
      person.Adresse = None
    try:
      person.Postnr = doc.getElementsByTagName("Postnr")[0].childNodes[0].data
    except:
      Postnr = None
    try:
      person.Bynavn = doc.getElementsByTagName("Bynavn")[0].childNodes[0].data
    except:
      person.Bynavn = None
    try:
      person.Email = doc.getElementsByTagName("Email")[0].childNodes[0].data
    except:
      person.Email = None
    try:
      person.Telefon = doc.getElementsByTagName("Telefon")[0].childNodes[0].data
    except:
      person.Telefon = None
    try:
      person.Kon = doc.getElementsByTagName("Kon")[0].childNodes[0].data
    except:
      person.Kon = None
    try:
      FodtDato = doc.getElementsByTagName("FodtDato")[0].childNodes[0].data
      dt = datetime.strptime(FodtDato, "%Y-%m-%d")
      person.FodtDato = dt.date()
    except:
      person.FodtDato = None
    try:
      person.Bank = doc.getElementsByTagName("Bank")[0].childNodes[0].data
    except:
      person.Bank = None
    person.setNameTags()

    logging.info('XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX')
    logging.info('%s - %s - %s - %s - %s - %s - %s - %s - %s - %s - %s' % (person.Nr, person.Navn, person.Kaldenavn, person.Adresse, person.Postnr, person.Bynavn, person.Email, person.Telefon, person.Kon, person.FodtDato, person.Bank))
    logging.info('XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX')
    self.response.out.write('Status: 404')
    
class SyncMedlemlogHandler(webapp.RequestHandler):
  def post(self):
    doc = minidom.parse(self.request.body_file)
    try:
      Source = doc.getElementsByTagName("Source")[0].childNodes[0].data
    except:
      Source = None
    try:
      Source_id = doc.getElementsByTagName("Source_id")[0].childNodes[0].data
    except:
      Source_id = None
    try:
      Nr = doc.getElementsByTagName("Nr")[0].childNodes[0].data
    except:
      Nr = None
    
    personroot = db.Key.from_path('Persons','root','Person','%s' % (Nr))
    medlemlog = Medlemlog.get_or_insert('%s-%s' % (Source,Source_id), parent=personroot)
    
    medlemlog.Source = int(Source)
    medlemlog.Source_id = int(Source_id)
    medlemlog.Nr = int(Nr)
    try:
      Logdato = doc.getElementsByTagName("Logdato")[0].childNodes[0].data
      medlemlog.Logdato = datetime.strptime(Logdato, "%Y-%m-%dT%H:%M:%S")
    except:
      medlemlog.Logdato = None
    try:
      medlemlog.Akt_id = int(doc.getElementsByTagName("Akt_id")[0].childNodes[0].data)
    except:
      medlemlog.Akt_id = None
    try:
      Akt_dato = doc.getElementsByTagName("Akt_dato")[0].childNodes[0].data
      medlemlog.Akt_dato = datetime.strptime(Akt_dato, "%Y-%m-%dT%H:%M:%S")
    except:
      medlemlog.Akt_dato = None

    medlemlog.put()

    logging.info('XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX')
    logging.info('%s - %s - %s - %s - %s - %s' % (medlemlog.Source, medlemlog.Source_id, medlemlog.Nr, medlemlog.Logdato, medlemlog.Akt_id, medlemlog.Akt_dato))
    logging.info('XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX')
    self.response.out.write('Status: 404')

class LogoffHandler(webapp.RequestHandler):
  def get(self):
    self.redirect(users.create_logout_url("/"))

class ReindexHandler(webapp.RequestHandler):
    """Handler for submiting SearchIndexing to JobQ"""
    def get(self):
      root = db.Key.from_path('Persons','root')
      query = db.Query(keys_only=True).ancestor(root)
      i = 0
      perkeys = ''
      for per in query:
        i +=1
        perkeys += ' ' + str(per)
        if i == 10:
          taskqueue.add(url='/_ah/queue/default', params={'perkeys':perkeys})
          i = 0
          perkeys = ''
      if i > 0:
        taskqueue.add(url='/_ah/queue/default', params={'perkeys':perkeys})      
      self.redirect("/adm")

class SearchIndexing(webapp.RequestHandler):
    """Handler for full text indexing task."""
    def post(self):
      perkeys = self.request.get('perkeys') 
      keys = [db.Key(k) for k in perkeys.split()]
      persom_list = db.get(keys)
      for per in persom_list:
        per.setNameTags()


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
                                       ('/sync/Medlem', SyncMedlemHandler),
                                       ('/sync/Medlemlog', SyncMedlemlogHandler),
                                       ('/sync/.*', MenuHandler),
                                       ('/logoff', LogoffHandler),
                                       ('/teknik/createmenu', CreateMenu),
                                       ('/teknik/flushcache', FlushCache),
                                       ('/teknik/reindex', ReindexHandler),
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
