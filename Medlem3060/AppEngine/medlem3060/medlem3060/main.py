from google.appengine.ext import webapp
from google.appengine.ext.webapp import util
from google.appengine.ext import db 
from google.appengine.ext.webapp import template

import logging
import rest
import os
import re


from util import TestCrypt, COOKIE_NAME, LOGIN_URL, CreateCookieData, SetUserInfoCookie

class User(db.Model): 
  account = db.StringProperty()
  password = db.StringProperty()
  email = db.EmailProperty()
  
class Medlem(db.Model): 
    Nr = db.IntegerProperty()
    Navn  = db.StringProperty()
    Kaldenavn  = db.StringProperty()
    Adresse  = db.StringProperty()
    Postnr  = db.StringProperty()
    Bynavn  = db.StringProperty()
    Email = db.EmailProperty()
    Telefon = db.PhoneNumberProperty()
    Kon = db.StringProperty()
    FodtDato  = db.DateProperty()
    Bank = db.StringProperty()

class Medlemlog(db.Model): 
    Medlem_key = db.ReferenceProperty(Medlem, collection_name="medlemlog_set")
    Source = db.IntegerProperty()
    Source_id = db.IntegerProperty()
    Nr = db.IntegerProperty()
    Logdato = db.DateTimeProperty()
    Akt_id = db.IntegerProperty()  
    Akt_dato = db.DateTimeProperty()

class MainHandler(webapp.RequestHandler):
    def get(self):
        #TestCrypt('Mogens Hafsjold')
        self.response.out.write('Hello medlem3060!')
        
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
    Nr = self.request.get('nr') 
    
    query = Medlem.all()
    query.filter('Nr <', int(Nr))
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

    if m:
      template_values = {
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
    template_values = {
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

application = webapp.WSGIApplication([ ('/', MainHandler),
                                       (LOGIN_URL, LoginHandler),
                                       ('/adm/medlem.*', MedlemHandler),
                                       ('/adm/findmedlem', FindmedlemHandler),
                                       ('/rest/.*', rest.Dispatcher) ],
                                     debug=True )
rest.Dispatcher.base_url = "/rest"
rest.Dispatcher.add_models_from_module(__name__)

def main():
    util.run_wsgi_app(application)

if __name__ == '__main__':
    main()