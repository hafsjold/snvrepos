from google.appengine.ext import webapp
from google.appengine.ext.webapp import util
from google.appengine.ext import db 
from google.appengine.ext.webapp import template
from google.appengine.api import users
from google.appengine.api.labs import taskqueue
from google.appengine.api import memcache
from django.utils import simplejson

from xml.dom import minidom
from datetime import datetime
import time

import logging
import rest
import os
import re

from models import UserGroup, User, NrSerie, Menu, MenuMenuLink, Medlemlog, Person
from util import TestCrypt, COOKIE_NAME, LOGIN_URL, CreateCookieData, SetUserInfoCookie
from menuusergroup import deleteMenuAndUserGroup, createMenuAndUserGroup
from menu import MenuHandler, ListUserHandler, UserHandler

webapp.template.register_template_library('templatetags.medlem3060_extras')

def getFullKeyPath(pkey):
  pkeyparent = pkey.parent()
  if pkeyparent:
    return '%s,%s,%s' % (getFullKeyPath(pkeyparent), pkey.kind(), pkey.id_or_name())
  else:
    return '%s,%s' % (pkey.kind(), pkey.id_or_name())
    
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

class Findmedlem4Handler(webapp.RequestHandler):
  def get(self):
    template_values = {
    }
    path = os.path.join(os.path.dirname(__file__), 'templates/findmedlem4.html') 
    self.response.out.write(template.render(path, template_values))
    
class Findmedlem3Handler(webapp.RequestHandler):
  def get(self):
    root = db.Key.from_path('Persons','root')
    person_list = db.Query(Person).ancestor(root)
    template_values = {
    }
    path = os.path.join(os.path.dirname(__file__), 'templates/findmedlem3.html') 
    self.response.out.write(template.render(path, template_values))
    
class FindmedlemHandler(webapp.RequestHandler):
  def get(self):
    template_values = {
    }
    #time.sleep(10)
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
 
    #query = query.order('Nr')
    person_list = query.fetch(50)
 
    template_values = {
      'person_list': person_list,
    }
    path = os.path.join(os.path.dirname(__file__), 'templates/findmedlem.html') 
    self.response.out.write(template.render(path, template_values))

class UpdatemedlemHandler(webapp.RequestHandler):
  def post(self):
    jData = '{ '
    Nr = self.request.get('Nr')
    if Nr == '*':
      recNrSerie = NrSerie.get_or_insert('tblMedlem')
      Nr = '%s' % (recNrSerie.NextNumber)
      recNrSerie.NextNumber += 1
      recNrSerie.put()    

    #Test for Valid dato
    bAkt_dato = False
    Akt_dato = self.request.get('Akt_dato')
    if Akt_dato:
      try:
        Testdato = datetime.strptime(Akt_dato, "%Y-%m-%d")
        bAkt_dato = True
      except:
        bAkt_dato = False
        
    if bAkt_dato:
      recNrSerie = NrSerie.get_or_insert('tblMedlemlog')
      Source_id = recNrSerie.NextNumber
      recNrSerie.NextNumber += 1
      recNrSerie.put()

      k = db.Key.from_path('Persons','root','Person','%s' % (Nr))
      p = Medlemlog.get_or_insert('2-%s' % (Source_id), parent=k)
      p.Source = 2
      p.Source_id = Source_id
      p.Nr = int(Nr)
      p.Logdato = datetime.now()
      p.Akt_id = int(self.request.get('Akt_id'))
      Akt_dato = self.request.get('Akt_dato')
      logging.info('Akt_dato: %s' % (Akt_dato))
      p.Akt_dato = datetime.strptime(Akt_dato, "%Y-%m-%d")
      p.put()
      jData += '"bMedlemlog":"true"'
      jData += ',"MedlemlogTablePos":"%s"' % (self.request.get('MedlemlogTablePos'))
      jData += ',"MedlemlogData":["%s","%s","%s","%s","%s","%s","%s"]' % (p.Nr,p.Source,p.Source_id,p.Logdato,p.Akt_id,p.Akt_dato,p.Akt_id)
    else:
      jData += '"bMedlemlog":"false"'
     
    root = db.Key.from_path('Persons','root')
    m = Person.get_or_insert('%s' % (Nr), parent=root)
    try:       
      m.Navn = self.request.get('Navn') 
      m.Kaldenavn = self.request.get('Kaldenavn')
      m.Adresse = self.request.get('Adresse')
      m.Postnr = self.request.get('Postnr')
      m.Bynavn = self.request.get('Bynavn')
      m.Telefonnr = self.request.get('Telefonnr')
      m.Email = self.request.get('Email')
      m.Kon = self.request.get('Kon')
      dt = datetime.strptime(self.request.get('FodtDato'), "%Y-%m-%d")
      m.FodtDato = dt.date()
      logging.info('%s=%s' % ('FodtDato', getattr(m, 'FodtDato')) )
      m.Bank = self.request.get('Bank')
      m.setNameTags()
      m.put()
      jData += ',"PersonTablePos":"%s"' % (self.request.get('PersonTablePos'))
      jData += ',"PersonTableData": ["%s","%s","%s","%s","%s","%s","%s","%s","%s","%s","%s","%s","%s"]' % (m.Nr,m.Navn,m.Kaldenavn,m.Adresse,m.Postnr,m.Bynavn,m.Email,m.Telefon,m.Kon,m.FodtDato,m.Bank,m.MedlemtilDato,m.MedlemAabenBetalingsdato)
      jData += ' }'
      memcache.delete('jData', namespace='jData')
      logging.info('UpdatemedlemHandler OK Navn: %s, Kaldenavn: %s' % (self.request.get('Navn'), self.request.get('Kaldenavn')))
      logging.info('%s' % (jData))
      self.response.headers["Content-Type"] = "application/json"
      self.response.out.write(jData)      
    except:
      logging.info('UpdatemedlemHandler ERROR Navn: %s, Kaldenavn: %s' % (self.request.get('Navn'), self.request.get('Kaldenavn')))
      self.response.out.write('ERROR from Server')
   
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

class MedlemJsonHandler(webapp.RequestHandler):
  def get(self):
    jData = memcache.get('jData', namespace='jData')
    #jData = None
    if jData is None:
      root = db.Key.from_path('Persons','root')
      qry = db.Query(Person).ancestor(root)
      antal = qry.count()
      logging.info('TTTTTTTTTTTTTTT jData Antal: %s' % (antal))
      FirstPage = True
      jData = '{ "aaData": ['
      for p in qry:
        if not FirstPage:
          jData += ','
        FirstPage = False
        jData += '["%s","%s","%s","%s","%s","%s","%s","%s","%s","%s","%s","%s","%s"]' % (p.Nr,p.Navn,p.Kaldenavn,p.Adresse,p.Postnr,p.Bynavn,p.Email,p.Telefon,p.Kon,p.FodtDato,p.Bank,p.MedlemtilDato,p.MedlemAabenBetalingsdato)
      jData += '] }'
      memcache.set('jData', jData, namespace='jData')
    
    self.response.headers["Content-Type"] = "application/json"
    self.response.out.write(jData)

class MedlemlogJsonHandler(webapp.RequestHandler):
  def get(self):
    jLogData = memcache.get('jLogData', namespace='jLogData')
    #jLogData = None
    if jLogData is None:
      root = db.Key.from_path('Persons','root')
      qry = db.Query(Medlemlog).ancestor(root)
      antal = qry.count()
      logging.info('TTTTTTTTTTTTTTT jLogData Antal: %s' % (antal))
      FirstPage = True
      jLogData = '{ "aaData": ['
      for p in qry:
        if not FirstPage:
          jLogData += ','
        FirstPage = False
        jLogData += '["%s","%s","%s","%s","%s","%s","%s"]' % (p.Nr,p.Source,p.Source_id,p.Logdato,p.Akt_id,p.Akt_dato,p.Akt_id)
      jLogData += '] }'
      memcache.set('jLogData', jLogData, namespace='jLogData')
    
    self.response.headers["Content-Type"] = "application/json"
    self.response.out.write(jLogData)
    
class SyncMedlemHandler(webapp.RequestHandler):
  def get(self):
    root = db.Key.from_path('Persons','root')
    qry = db.Query(Person).ancestor(root)
    antal = qry.count()
    logging.info('TTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTT Antal: %s' % (antal))
    template_values = {
      'user_list': qry,
    }
    path = os.path.join(os.path.dirname(__file__), 'templates/medlem.xml') 
    self.response.out.write(template.render(path, template_values))
  
  def delete(self):
    path = self.request.environ['PATH_INFO']
    mo = re.match("/sync/Medlem/([0-9]+)", path)
    if mo:
      if mo.groups()[0]:
        try:
          Nr = mo.groups()[0]
          logging.info('DELETE Nr=%s' % (Nr))
          k = db.Key.from_path('Persons','root','Person',Nr)
          m = Person.get(k)
          m.delete()
        except:
          pass
    self.response.out.write('Status: 403')

  def post(self):
    doc = minidom.parse(self.request.body_file)
    try:
      Nr = doc.getElementsByTagName("Nr")[0].childNodes[0].data
    except:
      Nr = None
    root = db.Key.from_path('Persons','root')
    person = Person.get_or_insert('%s' % (Nr), parent=root)
    
    for n in ['Navn', 'Kaldenavn', 'Adresse', 'Postnr', 'Bynavn', 'Email', 'Telefon', 'Kon', 'Bank']:
      val = None
      bval = True
      try:
        val = doc.getElementsByTagName(n)[0].childNodes[0].data
      except:
        bval = False
      
      if bval:     
        try:
          setattr(person, n, val)
          logging.info('%s=%s' % (n, getattr(person, n)) )
        except:
          setattr(person, n, None)
    
    FodtDato = None
    bFodtDato = True
    try:
      FodtDato = doc.getElementsByTagName("FodtDato")[0].childNodes[0].data
    except:
      bFodtDato = False

    if bFodtDato: 
      try:
        dt = datetime.strptime(FodtDato, "%Y-%m-%d")
        person.FodtDato = dt.date()
        logging.info('%s=%s' % ('FodtDato', getattr(person, 'FodtDato')) )
      except:
        person.FodtDato = None

    person.setNameTags()
    person.put()

    logging.info('XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX')
    logging.info('%s - %s - %s - %s - %s - %s - %s - %s - %s - %s - %s' % (person.Nr, person.Navn, person.Kaldenavn, person.Adresse, person.Postnr, person.Bynavn, person.Email, person.Telefon, person.Kon, person.FodtDato, person.Bank))
    logging.info('XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX')
    self.response.out.write('Status: 404')
    
class SyncMedlemlogHandler(webapp.RequestHandler):
  def get(self):
    root = db.Key.from_path('Persons','root')
    qry = db.Query(Medlemlog).ancestor(root)
    antal = qry.count()
    logging.info('TTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTT Antal: %s' % (antal))
    user_log = qry.fetch(2)
    template_values = {
      'user_log': qry,
    }
    path = os.path.join(os.path.dirname(__file__), 'templates/medlemlog.xml') 
    self.response.out.write(template.render(path, template_values))
  
  def delete(self):
    path = self.request.environ['PATH_INFO']
    mo = re.match("/sync/Medlemlog/([0-9]+)/([0-9]+)/([0-9]+)", path)
    if mo:
      if mo.groups()[0] and mo.groups()[1] and mo.groups()[2]:
        try:
          Nr = mo.groups()[0]
          Source = mo.groups()[1]
          Source_id = mo.groups()[2]
          logging.info('DELETE Nr=%s Source=%s Source_id=%s' % (Nr,Source,Source_id))
          k = db.Key.from_path('Persons','root','Person','%s' % (Nr),'%s-%s' % (Source,Source_id))
          m = Medlemlog.get(k)
          m.delete()
        except:
          pass
    self.response.out.write('Status: 403')
    
  def post(self):
    doc = minidom.parse(self.request.body_file)
    bkey = True
    try:
      Source = doc.getElementsByTagName("Source")[0].childNodes[0].data
    except:
      bkey = False
    try:
      Source_id = doc.getElementsByTagName("Source_id")[0].childNodes[0].data
    except:
      bkey = False
    try:
      Nr = doc.getElementsByTagName("Nr")[0].childNodes[0].data
    except:
      bkey = False
    
    if bkey:
      personroot = db.Key.from_path('Persons','root','Person','%s' % (Nr))
      medlemlog = Medlemlog.get_or_insert('%s-%s' % (Source,Source_id), parent=personroot)
    
      medlemlog.Source = int(Source)
      medlemlog.Source_id = int(Source_id)
      medlemlog.Nr = int(Nr)
      try:
        Logdato = doc.getElementsByTagName("Logdato")[0].childNodes[0].data
        medlemlog.Logdato = datetime.strptime(Logdato, "%Y-%m-%dT%H:%M:%S")
      except:
        pass
      try:
        medlemlog.Akt_id = int(doc.getElementsByTagName("Akt_id")[0].childNodes[0].data)
      except:
        pass
      try:
        Akt_dato = doc.getElementsByTagName("Akt_dato")[0].childNodes[0].data
        medlemlog.Akt_dato = datetime.strptime(Akt_dato, "%Y-%m-%dT%H:%M:%S")
      except:
        pass

      medlemlog.put()

      logging.info('XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX')
      logging.info('%s - %s - %s - %s - %s - %s' % (medlemlog.Source, medlemlog.Source_id, medlemlog.Nr, medlemlog.Logdato, medlemlog.Akt_id, medlemlog.Akt_dato))
      logging.info('XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX')
      self.response.out.write('Status: 404')
    

class LogoffHandler(webapp.RequestHandler):
  def get(self):
    self.redirect(users.create_logout_url("/"))

class ReindexHandlerXXXXXXXXXXX(webapp.RequestHandler):
    """Handler for submiting SearchIndexing to JobQ"""
    def get(self):
      Nr = '852'
      k = db.Key.from_path('Persons','root','Person',Nr)
      perkeys = str(k)
      taskqueue.add(url='/_ah/queue/default', params={'perkeys':perkeys})      
      self.redirect("/adm")
      
class ReindexHandler(webapp.RequestHandler):
    """Handler for submiting SearchIndexing to JobQ"""
    def get(self):
      root = db.Key.from_path('Persons','root')
      query = db.Query(Person, keys_only=True).ancestor(root)
      i = 0
      perkeys = ''
      for per in query:
        i +=1
        perkeys += ' ' + str(per)
        if i == 100:
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
      person_list = db.get(keys)
      for per in person_list:
        per.setNameTags()
        per.put()

      memcache.delete('jData', namespace='jData')



class CreateMenu(webapp.RequestHandler):
    """Handler for create Menu"""
    def get(self):
      deleteMenuAndUserGroup()
      createMenuAndUserGroup()
      memcache.flush_all()
      recNrSerie = NrSerie.get_or_insert('tblMedlemlog')
      recNrSerie.Name = 'tblMedlemlog'
      if not recNrSerie.NextNumber:
        recNrSerie.NextNumber = 2000
      recNrSerie.put()
      recNrSerie = NrSerie.get_or_insert('tblMedlem')
      recNrSerie.Name = 'tblMedlem'
      if not recNrSerie.NextNumber:
        recNrSerie.NextNumber = 850
      recNrSerie.put()      
      self.redirect("/adm")
      
class FlushCache(webapp.RequestHandler):
    """Handler for Flush Cache"""
    def get(self):
      memcache.flush_all()
      self.redirect("/adm")

      
application = webapp.WSGIApplication([ ('/', MainHandler),
                                       (LOGIN_URL, LoginHandler),
                                       ('/adm/medlemjson', MedlemJsonHandler),
                                       ('/adm/medlemlogjson', MedlemlogJsonHandler),
                                       ('/adm/medlem.*', MedlemHandler),
                                       ('/adm/findmedlem', FindmedlemHandler),
                                       ('/adm/findmedlem3', Findmedlem3Handler),
                                       ('/adm/updatemedlem', UpdatemedlemHandler),
                                       ('/adm/findmedlem4', Findmedlem4Handler),
                                       ('/adm', MenuHandler),
                                       ('/rest/.*', rest.Dispatcher),
                                       ('/sync/Medlemlog/.*', SyncMedlemlogHandler),
                                       ('/sync/Medlemlog', SyncMedlemlogHandler),
                                       ('/sync/Medlem/.*', SyncMedlemHandler),
                                       ('/sync/Medlem', SyncMedlemHandler),
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
