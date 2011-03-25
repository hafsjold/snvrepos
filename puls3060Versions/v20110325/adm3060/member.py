#!/usr/bin/env python
#
# Copyright 2007 Google Inc.
#
# Licensed under the Apache License, Version 2.0 (the "License");
# you may not use this file except in compliance with the License.
# You may obtain a copy of the License at
#
#     http://www.apache.org/licenses/LICENSE-2.0
#
# Unless required by applicable law or agreed to in writing, software
# distributed under the License is distributed on an "AS IS" BASIS,
# WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
# See the License for the specific language governing permissions and
# limitations under the License.
#

from util import CreateCookieData, SetUserInfoCookie, COOKIE_NAME, LOGIN_URL
from pprint import pformat

from google.appengine.api import users 
from google.appengine.ext import webapp
from google.appengine.ext.webapp import util
import logging 
from models import Person
from google.appengine.ext import db
import os
from google.appengine.ext.webapp import template
import time
import Cookie

class MainHandler(webapp.RequestHandler):

  def get(self):
    if users.get_current_user():
      current_user = users.get_current_user() 
      person = db.Query(Person).filter('owner =', current_user).get()
    
      if person:
        template_values = { 
          'name': person.name,
          'user': person.owner,
        }
      else:
        template_values = { 
          'name': '', 
          'user': current_user,
        }  
    else:
      template_values = { 
        'name': '', 
        'user': None,      
      }
    
    #C = Cookie.SimpleCookie()
    #C["number"] = 7
    #C["string"] = "seven"
    #logging.error(C.output())
    #self.response.set_cookie('lastvisit', 'fredag', max_age=3600, path='/', domain='localhost', secure=True)

    #self.response.headers['Set-Cookie'] = 'lastvisit=fredag'
    path = os.path.join(os.path.dirname(__file__), 'templates/index.html') 
    self.response.out.write(template.render(path, template_values))
    user3060 = self.request._environ['user3060'] 
    userAuth = self.request._environ['userAuth']    
    self.response.out.write('TEST Du er nu logget ind som %s, userAuth: %s' % (user3060, userAuth))
    for keyvalue in self.request.environ:
      logging.info(keyvalue)   

class TestHandler(webapp.RequestHandler):

  def get(self):
    if users.get_current_user():
      current_user = users.get_current_user()        
      self.response.out.write('Hello TestHandler User')
      user3060 = self.request.environ['user3060']   
      self.response.out.write('Du er nu logget ind som %s' % user3060)
      # #logging.debug('User: %s ,Email: %s ,User_id: %s, Auth_domain: %s, Administrator: %s' % ( users.get_current_user().nickname(), current_user.email(), current_user.user_id(), current_user.auth_domain(), users.is_current_user_admin() )  )
      #assert users.is_current_user_admin(), "User is not Administrator"
      # #logging.debug('MHA: ' + pformat(current_user))
    else: 
      self.response.out.write('Hello TestHandler User do not exists!') 
      user3060 = self.request.environ['user3060']  
      self.response.out.write('Du er nu logget ind som %s' % user3060)
     
  def post(self): 
    person = Person() 
    if users.get_current_user(): 
      person.owner = users.get_current_user() 
 
    person.name = self.request.get('navn') 
    person.put() 
    self.redirect('/')
 
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
      
class NytmedlemHandler(webapp.RequestHandler):
  def get(self):
    template_values = {
      'kontingent_fra_dato': '1. januar 2010',
      'kontingent_til_dato': '31. december 2010',
      'kontingent': '150,00',
      'Fornavn': '',
      'Efternavn': '',
      'Adresse': '',
      'Postnr': '',
      'Bynavn': '',
      'Telefonnr': '',
      'Email': '',
      'Fodelsdato': '',
    }
    path = os.path.join(os.path.dirname(__file__), 'templates/nytmedlem.html') 
    self.response.out.write(template.render(path, template_values))
    
  def post(self):
    Fornavn = self.request.get('Fornavn') 
    Efternavn = self.request.get('Efternavn')
    Adresse = self.request.get('Adresse')
    Postnr = self.request.get('Postnr')
    Bynavn = self.request.get('Bynavn')
    Telefonnr = self.request.get('Telefonnr')
    Email = self.request.get('Email')
    Fodelsdato = self.request.get('Fodelsdato')
    logging.info('Fornavn: %s, Efternavn: %s' % (Fornavn,Efternavn))
    template_values = {
      'kontingent_fra_dato': '1. januar 2010',
      'kontingent_til_dato': '31. december 2010',
      'kontingent': '150,00',
      'Fornavn': Fornavn,
      'Efternavn': Efternavn,
      'Adresse': Adresse,
      'Postnr': Postnr,
      'Bynavn': Bynavn,
      'Telefonnr': Telefonnr,
      'Email': Email,
      'Fodelsdato': Fodelsdato,
    }
    path = os.path.join(os.path.dirname(__file__), 'templates/nytmedlem.html') 
    self.response.out.write(template.render(path, template_values))
      
def main():
  logging.getLogger().setLevel(logging.DEBUG) 
  application = webapp.WSGIApplication([('/', MainHandler),
                                        (LOGIN_URL, LoginHandler),
                                        ('/nytmedlem', NytmedlemHandler),
                                        ('/test', TestHandler)],
                                       debug=True)
  util.run_wsgi_app(application)


if __name__ == '__main__':
  main()
