# coding=utf-8 
from google.appengine.ext import webapp
from google.appengine.ext.webapp import util
from google.appengine.ext import db 
from google.appengine.ext.webapp import template
from google.appengine.api import users
from google.appengine.api.labs import taskqueue
from google.appengine.api import memcache
from django.utils import simplejson

from xml.dom import minidom
from datetime import datetime, timedelta, date, tzinfo

import time

import logging
import rest
import os
import re
import sys  

from models import nextval, UserGroup, User, NrSerie, Kreditor, Kontingent, Pbsforsendelse, Tilpbs, Fak, Overforsel, Rykker, Pbsfiles, Pbsfile, Sendqueue, Frapbs, Bet, Betlin, Aftalelin, Indbetalingskort, Sftp, Infotekst, Sysinfo, Menu, MenuMenuLink, Medlog, Person
from util import utc, cet,TestCrypt, COOKIE_NAME, LOGIN_URL, CreateCookieData, SetUserInfoCookie
from menuusergroup import deleteMenuAndUserGroup, createMenuAndUserGroup
from menu import MenuHandler, ListUserHandler, UserHandler
from pbs601 import TestHandler, DatatilpbsHandler, DatafrapbsHandler, DatasftpHandler

webapp.template.register_template_library('templatetags.medlem3060_extras')

logging.info( '%s' % sys.path)

def getFullKeyPath(pkey):
  pkeyparent = pkey.parent()
  if pkeyparent:
    return '%s,%s,%s' % (getFullKeyPath(pkeyparent), pkey.kind(), pkey.id_or_name())
  else:
    return '%s,%s' % (pkey.kind(), pkey.id_or_name())
    
class MainHandler(webapp.RequestHandler):
    def get(self):
      try:
        menu_count = Menu.all().count()
      except:
        menu_count = 0
      if menu_count == 0:
        deleteMenuAndUserGroup()
        createMenuAndUserGroup()
        memcache.flush_all()
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
      Nr = nextval('Personid')

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
      Id = nextval('tblMedlemlog')
      k = db.Key.from_path('Persons','root','Person','%s' % (Nr))
      p = Medlog.get_or_insert('%s' % (Id), parent=k)
      p.Id = Id
      p.Source = 'Medlog'
      p.Source_id = Id
      p.Nr = int(Nr)
      p.Logdato = datetime.now()
      p.Akt_id = int(self.request.get('Akt_id'))
      Akt_dato = self.request.get('Akt_dato')
      logging.info('Akt_dato: %s' % (Akt_dato))
      p.Akt_dato = datetime.strptime(Akt_dato, "%Y-%m-%d").date()
      p.put()
      jData += '"bMedlemlog":"true"'
      jData += ',"MedlemlogTablePos":"%s"' % (self.request.get('MedlemlogTablePos'))
      jData += ',"MedlemlogData":["%s","%s","%s","%s","%s","%s","%s"]' % (p.Nr,p.Source,p.Source_id,p.Logdato,p.Akt_id,p.Akt_dato,p.Akt_id)
      memcache.delete('jLogData', namespace='jLogData')
      
      Akt_id = self.request.get('Akt_id')
      if Akt_id == '10':
        Kontingent_id = nextval('Kontingentid')
        t = db.Key.from_path('Persons','root','Person','%s' % (Nr))
        q = Kontingent.get_or_insert('%s' % (Kontingent_id), parent=t)      
        q.Id = int(Kontingent_id)
        q.Nr = int(Nr)
        dtFradato = datetime.strptime(Akt_dato, "%Y-%m-%d")
        q.Fradato = date(dtFradato.year, dtFradato.month, dtFradato.day)
        q.beregnKontingent()
        q.put()
        Navn = self.request.get('Navn')
        jData += ',"bKontingent":"true"'
        jData += ',"KontingentTablePos":"%s"' % (self.request.get('KontingentTablePos'))
        jData += ',"KontingentData":["%s","%s","%s","%s","%s","%s"]' % (q.Nr, Navn, q.Fradato, q.Advisbelob, q.Tildato, q.Id)
        memcache.delete('jKontingentData', namespace='jKontingentData')
      else:   
        jData += ',"bKontingent":"false"'
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
      m.Telefon = self.request.get('Telefon')
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
      memcache.delete('jMedlemXmlData', namespace='jMedlemXmlData')    
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
    log_list = db.Query(Medlog).ancestor(k).order('-Logdato')
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
      qry = db.Query(Medlog).ancestor(root)
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

class KontingentJsonHandler(webapp.RequestHandler):
  def get(self):
    jKontingentData = memcache.get('jKontingentData', namespace='jKontingentData')
    #jKontingentData = None
    if jKontingentData is None:
      root = db.Key.from_path('Persons','root')
      qry = db.Query(Kontingent).ancestor(root).filter('Faktureret =',False)
      antal = qry.count()
      logging.info('TTTTTTTTTTTTTTT jKontingentData Antal: %s' % (antal))
      FirstPage = True
      jKontingentData = '{ "aaData": ['
      for q in qry:
        if not FirstPage:
          jKontingentData += ','
        FirstPage = False
        m = q.parent()
        jKontingentData += '["%s","%s","%s","%s","%s","%s"]' % (q.Nr, m.Navn, q.Fradato, q.Advisbelob, q.Tildato, q.Id)
      
      jKontingentData += '] }'
      memcache.set('jKontingentData', jKontingentData, namespace='jKontingentData')
    
    self.response.headers["Content-Type"] = "application/json"
    self.response.out.write(jKontingentData)
    
    
class SyncConvertHandler(webapp.RequestHandler):
  def post(self):
    doc = minidom.parse(self.request.body_file)
    ModelName  = doc.documentElement.tagName
    logging.info('ModelName==>%s<=' % (ModelName))

    if ModelName == 'Person':
      try:
        Nr = doc.getElementsByTagName("Nr")[0].childNodes[0].data
      except:
        Nr = None
      root = db.Key.from_path('Persons','root')
      rec = Person.get_or_insert('%s' % (Nr), parent=root)
      
      for attr_name, value in Person.__dict__.iteritems():
        if isinstance(value, db.Property):
          attr_type = value.__class__.__name__        
          if not attr_type in ['_ReverseReferenceProperty']:
            val = self.attr_val(doc, attr_name, attr_type)
            logging.info('%s=%s' % (attr_name, val))
            try:
              setattr(rec, attr_name, val)
            except:
              setattr(rec, attr_name, None)
          
            logging.info('==>%s<==>%s<==' % (attr_name, attr_type))
      rec.put()
    
    if ModelName == 'Medlog':
      try:
        Id = doc.getElementsByTagName("Id")[0].childNodes[0].data
      except:
        Id = None
      try:
        Nr = doc.getElementsByTagName("Nr")[0].childNodes[0].data
      except:
        Nr = None
      root = db.Key.from_path('Persons','root','Person','%s' % (Nr))
      rec = Medlog.get_or_insert('%s' % (Id), parent=root)
      
      for attr_name, value in Medlog.__dict__.iteritems():
        if isinstance(value, db.Property):
          attr_type = value.__class__.__name__        
          if not attr_type in ['_ReverseReferenceProperty']:
            val = self.attr_val(doc, attr_name, attr_type)
            logging.info('%s=%s' % (attr_name, val))
            try:
              setattr(rec, attr_name, val)
            except:
              setattr(rec, attr_name, None)
          
            logging.info('==>%s<==>%s<==' % (attr_name, attr_type))
      rec.put()
      
    elif ModelName == 'Pbsforsendelse':
      try:
        Id = doc.getElementsByTagName("Id")[0].childNodes[0].data
      except:
        Id = None
      root = db.Key.from_path('rootPbsforsendelse','root')
      rec = Pbsforsendelse.get_or_insert('%s' % (Id), parent=root)
      
      for attr_name, value in Pbsforsendelse.__dict__.iteritems():
        if isinstance(value, db.Property):
          attr_type = value.__class__.__name__        
          if not attr_type in ['_ReverseReferenceProperty']:
            val = self.attr_val(doc, attr_name, attr_type)
            logging.info('%s=%s' % (attr_name, val))
            try:
              setattr(rec, attr_name, val)
            except:
              setattr(rec, attr_name, None)
          
            logging.info('==>%s<==>%s<==' % (attr_name, attr_type))
      rec.put()

    elif ModelName == 'Tilpbs':
      try:
        Id = doc.getElementsByTagName("Id")[0].childNodes[0].data
      except:
        Id = None
      root = db.Key.from_path('rootTilpbs','root')
      rec = Tilpbs.get_or_insert('%s' % (Id), parent=root)
      
      for attr_name, value in Tilpbs.__dict__.iteritems():
        if isinstance(value, db.Property):
          attr_type = value.__class__.__name__        
          if not attr_type in ['_ReverseReferenceProperty']:
            val = self.attr_val(doc, attr_name, attr_type)
            logging.info('%s=%s' % (attr_name, val))
            try:
              setattr(rec, attr_name, val)
            except:
              setattr(rec, attr_name, None)
          
            logging.info('==>%s<==>%s<==' % (attr_name, attr_type))
      rec.put()
      
    elif ModelName == 'Fak':
      try:
        Id = doc.getElementsByTagName("Id")[0].childNodes[0].data
      except:
        Id = None
      try:
        Nr = doc.getElementsByTagName("Nr")[0].childNodes[0].data
      except:
        Nr = None
      root = db.Key.from_path('Persons','root','Person','%s' % (Nr))
      rec = Fak.get_or_insert('%s' % (Id), parent=root)
      
      for attr_name, value in Fak.__dict__.iteritems():
        if isinstance(value, db.Property):
          attr_type = value.__class__.__name__        
          if not attr_type in ['_ReverseReferenceProperty']:
            val = self.attr_val(doc, attr_name, attr_type)
            logging.info('%s=%s' % (attr_name, val))
            try:
              setattr(rec, attr_name, val)
            except:
              setattr(rec, attr_name, None)
          
            logging.info('==>%s<==>%s<==' % (attr_name, attr_type))
      rec.put()
      rec.addMedlog()
      
    elif ModelName == 'Rykker':
      try:
        Id = doc.getElementsByTagName("Id")[0].childNodes[0].data
      except:
        Id = None
      try:
        Nr = doc.getElementsByTagName("Nr")[0].childNodes[0].data
      except:
        Nr = None
      root = db.Key.from_path('Persons','root','Person','%s' % (Nr))
      rec = Rykker.get_or_insert('%s' % (Id), parent=root)
      
      for attr_name, value in Rykker.__dict__.iteritems():
        if isinstance(value, db.Property):
          attr_type = value.__class__.__name__        
          if not attr_type in ['_ReverseReferenceProperty']:
            val = self.attr_val(doc, attr_name, attr_type)
            logging.info('%s=%s' % (attr_name, val))
            try:
              setattr(rec, attr_name, val)
            except:
              setattr(rec, attr_name, None)
          
            logging.info('==>%s<==>%s<==' % (attr_name, attr_type))
      rec.put()
      
    elif ModelName == 'Overforsel':
      try:
        Id = doc.getElementsByTagName("Id")[0].childNodes[0].data
      except:
        Id = None
      try:
        Nr = doc.getElementsByTagName("Nr")[0].childNodes[0].data
      except:
        Nr = None
      root = db.Key.from_path('Persons','root','Person','%s' % (Nr))
      rec = Overforsel.get_or_insert('%s' % (Id), parent=root)
      
      for attr_name, value in Overforsel.__dict__.iteritems():
        if isinstance(value, db.Property):
          attr_type = value.__class__.__name__        
          if not attr_type in ['_ReverseReferenceProperty']:
            val = self.attr_val(doc, attr_name, attr_type)
            logging.info('%s=%s' % (attr_name, val))
            try:
              setattr(rec, attr_name, val)
            except:
              setattr(rec, attr_name, None)
          
            logging.info('==>%s<==>%s<==' % (attr_name, attr_type))
      rec.put()
      
    elif ModelName == 'Pbsfiles':
      try:
        Id = doc.getElementsByTagName("Id")[0].childNodes[0].data
      except:
        Id = None
      root = db.Key.from_path('rootPbsfiles','root')
      rec = Pbsfiles.get_or_insert('%s' % (Id), parent=root)
      
      for attr_name, value in Pbsfiles.__dict__.iteritems():
        if isinstance(value, db.Property):
          attr_type = value.__class__.__name__        
          if not attr_type in ['_ReverseReferenceProperty']:
            val = self.attr_val(doc, attr_name, attr_type)
            logging.info('%s=%s' % (attr_name, val))
            try:
              setattr(rec, attr_name, val)
            except:
              setattr(rec, attr_name, None)
          
            logging.info('==>%s<==>%s<==' % (attr_name, attr_type))
      rec.put()

    elif ModelName == 'Pbsfile':
      try:
        Id = doc.getElementsByTagName("Id")[0].childNodes[0].data
      except:
        Id = None
      root = db.Key.from_path('rootPbsfile','root')
      rec = Pbsfile.get_or_insert('%s' % (Id), parent=root)
      
      for attr_name, value in Pbsfile.__dict__.iteritems():
        if isinstance(value, db.Property):
          attr_type = value.__class__.__name__        
          if not attr_type in ['_ReverseReferenceProperty']:
            val = self.attr_val(doc, attr_name, attr_type)
            logging.info('%s=%s' % (attr_name, val))
            try:
              setattr(rec, attr_name, val)
            except:
              setattr(rec, attr_name, None)
          
            logging.info('==>%s<==>%s<==' % (attr_name, attr_type))
      rec.put()

    elif ModelName == 'Frapbs':
      try:
        Id = doc.getElementsByTagName("Id")[0].childNodes[0].data
      except:
        Id = None
      root = db.Key.from_path('rootFrapbs','root')
      rec = Frapbs.get_or_insert('%s' % (Id), parent=root)
      
      for attr_name, value in Frapbs.__dict__.iteritems():
        if isinstance(value, db.Property):
          attr_type = value.__class__.__name__        
          if not attr_type in ['_ReverseReferenceProperty']:
            val = self.attr_val(doc, attr_name, attr_type)
            logging.info('%s=%s' % (attr_name, val))
            try:
              setattr(rec, attr_name, val)
            except:
              setattr(rec, attr_name, None)
          
            logging.info('==>%s<==>%s<==' % (attr_name, attr_type))
      rec.put()

    elif ModelName == 'Bet':
      try:
        Id = doc.getElementsByTagName("Id")[0].childNodes[0].data
      except:
        Id = None
      root = db.Key.from_path('rootBet','root')
      rec = Bet.get_or_insert('%s' % (Id), parent=root)
      
      for attr_name, value in Bet.__dict__.iteritems():
        if isinstance(value, db.Property):
          attr_type = value.__class__.__name__        
          if not attr_type in ['_ReverseReferenceProperty']:
            val = self.attr_val(doc, attr_name, attr_type)
            logging.info('%s=%s' % (attr_name, val))
            try:
              setattr(rec, attr_name, val)
            except:
              setattr(rec, attr_name, None)
          
            logging.info('==>%s<==>%s<==' % (attr_name, attr_type))
      rec.put()
      
    elif ModelName == 'Betlin':
      try:
        Id = doc.getElementsByTagName("Id")[0].childNodes[0].data
      except:
        Id = None
      try:
        Nr = doc.getElementsByTagName("Nr")[0].childNodes[0].data
      except:
        Nr = None
      root = db.Key.from_path('Persons','root','Person','%s' % (Nr))
      rec = Betlin.get_or_insert('%s' % (Id), parent=root)
      
      for attr_name, value in Betlin.__dict__.iteritems():
        if isinstance(value, db.Property):
          attr_type = value.__class__.__name__        
          if not attr_type in ['_ReverseReferenceProperty']:
            val = self.attr_val(doc, attr_name, attr_type)
            logging.info('%s=%s' % (attr_name, val))
            try:
              setattr(rec, attr_name, val)
            except:
              setattr(rec, attr_name, None)
          
            logging.info('==>%s<==>%s<==' % (attr_name, attr_type))
      rec.put()
      rec.addMedlog()      

    elif ModelName == 'Aftalelin':
      try:
        Id = doc.getElementsByTagName("Id")[0].childNodes[0].data
      except:
        Id = None
      try:
        Nr = doc.getElementsByTagName("Nr")[0].childNodes[0].data
      except:
        Nr = None
      root = db.Key.from_path('Persons','root','Person','%s' % (Nr))
      rec = Aftalelin.get_or_insert('%s' % (Id), parent=root)
      
      for attr_name, value in Aftalelin.__dict__.iteritems():
        if isinstance(value, db.Property):
          attr_type = value.__class__.__name__        
          if not attr_type in ['_ReverseReferenceProperty']:
            val = self.attr_val(doc, attr_name, attr_type)
            logging.info('%s=%s' % (attr_name, val))
            try:
              setattr(rec, attr_name, val)
            except:
              setattr(rec, attr_name, None)
          
            logging.info('==>%s<==>%s<==' % (attr_name, attr_type))
      rec.put()

    elif ModelName == 'Indbetalingskort':
      try:
        Id = doc.getElementsByTagName("Id")[0].childNodes[0].data
      except:
        Id = None
      try:
        Nr = doc.getElementsByTagName("Nr")[0].childNodes[0].data
      except:
        Nr = None
      root = db.Key.from_path('Persons','root','Person','%s' % (Nr))
      rec = Indbetalingskort.get_or_insert('%s' % (Id), parent=root)
      
      for attr_name, value in Indbetalingskort.__dict__.iteritems():
        if isinstance(value, db.Property):
          attr_type = value.__class__.__name__        
          if not attr_type in ['_ReverseReferenceProperty']:
            val = self.attr_val(doc, attr_name, attr_type)
            logging.info('%s=%s' % (attr_name, val))
            try:
              setattr(rec, attr_name, val)
            except:
              setattr(rec, attr_name, None)
          
            logging.info('==>%s<==>%s<==' % (attr_name, attr_type))
      rec.put()

    elif ModelName == 'Sftp':
      try:
        Id = doc.getElementsByTagName("Id")[0].childNodes[0].data
      except:
        Id = None
      root = db.Key.from_path('rootSftp','root')
      rec = Sftp.get_or_insert('%s' % (Id), parent=root)
      
      for attr_name, value in Sftp.__dict__.iteritems():
        if isinstance(value, db.Property):
          attr_type = value.__class__.__name__        
          if not attr_type in ['_ReverseReferenceProperty']:
            val = self.attr_val(doc, attr_name, attr_type)
            logging.info('%s=%s' % (attr_name, val))
            try:
              setattr(rec, attr_name, val)
            except:
              setattr(rec, attr_name, None)
          
            logging.info('==>%s<==>%s<==' % (attr_name, attr_type))
      rec.put()

    elif ModelName == 'Infotekst':
      try:
        Id = doc.getElementsByTagName("Id")[0].childNodes[0].data
      except:
        Id = None
      root = db.Key.from_path('rootInfotekst','root')
      rec = Infotekst.get_or_insert('%s' % (Id), parent=root)
      
      for attr_name, value in Infotekst.__dict__.iteritems():
        if isinstance(value, db.Property):
          attr_type = value.__class__.__name__        
          if not attr_type in ['_ReverseReferenceProperty']:
            val = self.attr_val(doc, attr_name, attr_type)
            logging.info('%s=%s' % (attr_name, val))
            try:
              setattr(rec, attr_name, val)
            except:
              setattr(rec, attr_name, None)
          
            logging.info('==>%s<==>%s<==' % (attr_name, attr_type))
      rec.put()

    elif ModelName == 'Sysinfo':
      try:
        Vkey = doc.getElementsByTagName("Vkey")[0].childNodes[0].data
      except:
        Vkey = None
      root = db.Key.from_path('rootSysinfo','root')
      rec = Sysinfo.get_or_insert('%s' % (Vkey), parent=root)
      
      for attr_name, value in Sysinfo.__dict__.iteritems():
        if isinstance(value, db.Property):
          attr_type = value.__class__.__name__        
          if not attr_type in ['_ReverseReferenceProperty']:
            val = self.attr_val(doc, attr_name, attr_type)
            logging.info('%s=%s' % (attr_name, val))
            try:
              setattr(rec, attr_name, val)
            except:
              setattr(rec, attr_name, None)
          
            logging.info('==>%s<==>%s<==' % (attr_name, attr_type))
      rec.put()

    elif ModelName == 'Kreditor':
      try:
        Id = doc.getElementsByTagName("Id")[0].childNodes[0].data
      except:
        Id = None
      root = db.Key.from_path('rootKreditor','root')
      rec = Kreditor.get_or_insert('%s' % (Id), parent=root)
      
      for attr_name, value in Kreditor.__dict__.iteritems():
        if isinstance(value, db.Property):
          attr_type = value.__class__.__name__        
          if not attr_type in ['_ReverseReferenceProperty']:
            val = self.attr_val(doc, attr_name, attr_type)
            logging.info('%s=%s' % (attr_name, val))
            try:
              setattr(rec, attr_name, val)
            except:
              setattr(rec, attr_name, None)
          
            logging.info('==>%s<==>%s<==' % (attr_name, attr_type))
      rec.put()

    elif ModelName == 'NrSerie':
      try:
        Nrserienavn = doc.getElementsByTagName("Nrserienavn")[0].childNodes[0].data
      except:
        Nrserienavn = None
      rec = NrSerie.get_or_insert('%s' % (Nrserienavn))
      
      for attr_name, value in NrSerie.__dict__.iteritems():
        if isinstance(value, db.Property):
          attr_type = value.__class__.__name__        
          if not attr_type in ['_ReverseReferenceProperty']:
            val = self.attr_val(doc, attr_name, attr_type)
            logging.info('%s=%s' % (attr_name, val))
            try:
              setattr(rec, attr_name, val)
            except:
              setattr(rec, attr_name, None)
          
            logging.info('==>%s<==>%s<==' % (attr_name, attr_type))
      rec.put()
      
    self.response.out.write('Status: 404')
    
  def attr_val(self, doc, attr_name, attr_type):
    try:
      strval = doc.getElementsByTagName(attr_name)[0].childNodes[0].data
    except:
      strval = None
    
    if attr_type == 'IntegerProperty':
      try:
        return int(strval)
      except:
        return None
    elif attr_type == 'FloatProperty':
      try:
        return float(strval)
      except:
        return None
    elif attr_type == 'BooleanProperty':
      try:
        return strval.lower() in ["yes", "true", "t", "1"] 
      except:
        return None 
    elif attr_type == 'TextProperty':
      try:
        return strval
      except:
        return None 
    elif attr_type == 'DateProperty':
      try:
        dt = datetime.strptime(strval[:19], "%Y-%m-%dT%H:%M:%S")
        return dt.date()
      except:
        return None
    elif attr_type == 'DateTimeProperty':
      try:
        dt = datetime.strptime(strval[:19], "%Y-%m-%dT%H:%M:%S")
        if strval[-6:] == '+01:00':
          return dt.replace(tzinfo = cet)
        elif strval[-6:] == '+00:00':
          return dt.replace(tzinfo = utc)
        else:          
          return dt
      except:
        return None        
    elif attr_type == 'ReferenceProperty':
      if attr_name == 'TilPbsref':
        try:
          strval = doc.getElementsByTagName('Tilpbsid')[0].childNodes[0].data
          return db.Key.from_path('rootTilpbs','root', 'Tilpbs', '%s' % (strval))
        except:
          return None
      elif attr_name == 'Pbsforsendelseref':
        try:
          strval = doc.getElementsByTagName('Pbsforsendelseid')[0].childNodes[0].data
          return db.Key.from_path('rootPbsforsendelse','root', 'Pbsforsendelse', '%s' % (strval))
        except:
          return None
      elif attr_name == 'Pbsfilesref':
        try:
          strval = doc.getElementsByTagName('Pbsfilesid')[0].childNodes[0].data
          return db.Key.from_path('rootPbsfiles','root', 'Pbsfiles', '%s' % (strval))
        except:
          return None
      if attr_name == 'Frapbsref':
        try:
          strval = doc.getElementsByTagName('Frapbsid')[0].childNodes[0].data
          return db.Key.from_path('rootFrapbs','root', 'Frapbs', '%s' % (strval))
        except:
          return None
      if attr_name == 'Betref':
        try:
          strval = doc.getElementsByTagName('Betid')[0].childNodes[0].data
          return db.Key.from_path('rootBet','root', 'Bet', '%s' % (strval))
        except:
          return None
      else:
        return None  
 
    else:
      try:
        return strval
      except:
        return None 

class SyncMedlogHandler(webapp.RequestHandler):
  def get(self):
    jLogXmlData = memcache.get('jLogXmlData', namespace='jLogXmlData')
    if jLogXmlData is None:
      root = db.Key.from_path('Persons','root')
      qry = db.Query(Medlog).ancestor(root)
      antal = qry.count()
      logging.info('TTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTT Antal: %s' % (antal))
      template_values = {
        'user_log': qry,
      }
      path = os.path.join(os.path.dirname(__file__), 'templates/medlog.xml')
      jLogXmlData = template.render(path, template_values)
      memcache.set('jLogXmlData', jLogXmlData, namespace='jLogXmlData')      
    
    self.response.out.write(jLogXmlData)

  def post(self):
    doc = minidom.parse(self.request.body_file)
    ModelName  = doc.documentElement.tagName
    
    if ModelName == 'Medlog':
      try:
        Id = nextval('Medlogid')
      except:
        Id = None
      try:
        Nr = doc.getElementsByTagName("Nr")[0].childNodes[0].data
      except:
        Nr = None
      root = db.Key.from_path('Persons','root','Person','%s' % (Nr))
      rec = Medlog.get_or_insert('%s' % (Id), parent=root)
      
      for attr_name, value in Medlog.__dict__.iteritems():
        if isinstance(value, db.Property):
          attr_type = value.__class__.__name__        
          if not attr_type in ['_ReverseReferenceProperty']:
            val = self.attr_val(doc, attr_name, attr_type)
            logging.info('%s=%s' % (attr_name, val))
            try:
              setattr(rec, attr_name, val)
            except:
              setattr(rec, attr_name, None)
          
            logging.info('==>%s<==>%s<==' % (attr_name, attr_type))
      rec.put()
    
    self.response.out.write('Status: 404')
    
  def attr_val(self, doc, attr_name, attr_type):
    try:
      strval = doc.getElementsByTagName(attr_name)[0].childNodes[0].data
    except:
      strval = None
    
    if attr_type == 'IntegerProperty':
      try:
        return int(strval)
      except:
        return None
    elif attr_type == 'DateProperty':
      try:
        dt = datetime.strptime(strval[:19], "%Y-%m-%dT%H:%M:%S")
        return dt.date()
      except:
        return None
    elif attr_type == 'DateTimeProperty':
      try:
        return datetime.strptime(strval[:19], "%Y-%m-%dT%H:%M:%S")
      except:
        return None        
      else:
        return None  
 
    else:
      try:
        return strval
      except:
        return None 
      
class SyncMedlemHandler(webapp.RequestHandler):
  def get(self):
    jMedlemXmlData = memcache.get('jMedlemXmlData', namespace='jMedlemXmlData')
    if jMedlemXmlData is None:
      root = db.Key.from_path('Persons','root')
      qry = db.Query(Person).ancestor(root)
      antal = qry.count()
      logging.info('TTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTT Antal: %s' % (antal))
      template_values = {
        'user_list': qry,
      }
      path = os.path.join(os.path.dirname(__file__), 'templates/medlem.xml') 
      jMedlemXmlData = template.render(path, template_values)
      memcache.set('jMedlemXmlData', jMedlemXmlData, namespace='jMedlemXmlData')      
    
    self.response.out.write(jMedlemXmlData)
  
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
          memcache.delete('jData', namespace='jData')
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
    memcache.delete('jData', namespace='jData')
    memcache.delete('jMedlemXmlData', namespace='jMedlemXmlData')    
    
    logging.info('XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX')
    logging.info('%s - %s - %s - %s - %s - %s - %s - %s - %s - %s - %s' % (person.Nr, person.Navn, person.Kaldenavn, person.Adresse, person.Postnr, person.Bynavn, person.Email, person.Telefon, person.Kon, person.FodtDato, person.Bank))
    logging.info('XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX')
    self.response.out.write('Status: 404')

class SyncNrSerieHandler(webapp.RequestHandler):
  def get(self):
    path = self.request.environ['PATH_INFO']
    logging.info('path: %s' % (path))
    mo = re.match("/sync/NrSerie/([0-9a-zA-Z]+)", path)
    if mo:
      if mo.groups()[0]:
        try:
          nrserie = mo.groups()[0]
          logging.info('nrserie: %s' % (nrserie))
          nextnr = nextval('%s' % (nrserie))
          logging.info('NrSerie=%s, nextnr=%s' % (nrserie,nextnr))
          self.response.out.write('%s' % (nextnr))
        except:
          self.response.out.write('Error')
          
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
                                       ('/adm/kontingentjson', KontingentJsonHandler),
                                       ('/adm/medlem.*', MedlemHandler),
                                       ('/adm/test.*', TestHandler),
                                       ('/adm/findmedlem', FindmedlemHandler),
                                       ('/adm/findmedlem3', Findmedlem3Handler),
                                       ('/adm/updatemedlem', UpdatemedlemHandler),
                                       ('/adm/findmedlem4', Findmedlem4Handler),
                                       ('/adm', MenuHandler),
                                       ('/rest/.*', rest.Dispatcher),
                                       ('/sync/Convert/.*', SyncConvertHandler),
                                       ('/sync/NrSerie/.*', SyncNrSerieHandler),
                                       ('/sync/Medlem/.*', SyncMedlemHandler),
                                       ('/sync/Medlem', SyncMedlemHandler),
                                       ('/sync/Medlog/.*', SyncMedlogHandler),
                                       ('/sync/Medlog', SyncMedlogHandler), 
                                       ('/data/tilpbs', DatatilpbsHandler),   
                                       ('/data/frapbs', DatafrapbsHandler),   
                                       ('/data/sftp/.*', DatasftpHandler),   
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
