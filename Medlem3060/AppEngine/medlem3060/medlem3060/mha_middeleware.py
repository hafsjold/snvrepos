import logging
from google.appengine.ext.webapp import template
from google.appengine.ext import db 
from google.appengine.api import memcache

import os
import re
from util import AuthRest, COOKIE_NAME, LOGIN_URL, PUBLIC_URL, CreateCookieData, GetUserInfo, SetUserInfoCookie, AuthUserGroupPath
from models import UserGroup, User

class Mha_Middeleware: 

  def __init__(self, application): 
    self.__application = application 
 
  """This is the REQUEST side of the middleware"""
  def __call__(self, environ, start_response):
    for key, value in environ.iteritems():
      logging.info('%s: %s' % (key, value))
    
    signed = True
    try:
      test_signed = environ['HTTP_SIGNED']
    except:
      signed = False

    google_user_id = False
    try:
      if environ['USER_ID']:
        google_user_id = True
    except:
      google_user_id = False
      
    user_is_admin = False
    try:
      if environ['USER_IS_ADMIN'] == '1':
        user_is_admin = True
    except:
      user_is_admin = False

    try:
      userAuth, user  = GetUserInfo(environ['HTTP_COOKIE'])
    except:
      userAuth, user  = False, ''
    
    environ['userAuth'] = userAuth
    environ['user3060'] = user
    environ['usergroup'] = '0'
    
    logging.info('MHA-Request-Logging google_user_id: %s, user_is_admin: %s, signed: %s, userAuth: %s, user: %s' % (google_user_id, user_is_admin, signed, userAuth, user))
    
    mo = re.match("^/rest/.*", environ['PATH_INFO'])    
    if mo:
      logging.info('re.match: True')
      if user_is_admin != True:
        try:
          http_timestamp = environ['HTTP_TIMESTAMP']
          http_signed = environ['HTTP_SIGNED']
        except:
          print 'Status: 404'
          return
        
        if not AuthRest(http_timestamp, http_signed):
          print 'Status: 400'
          return
    else:
      logging.info('re.match: False')    
      if google_user_id != True:
        if userAuth != True:    
          if environ['PATH_INFO'] not in PUBLIC_URL:
            #display login screen
            template_values = {
              'account': user,
              'login_message': 'Login',
              'method': 'post',
              'login_url': LOGIN_URL,
              'continue_url': environ['PATH_INFO'], 
            }
            path = os.path.join(os.path.dirname(__file__), 'templates/login.html') 
            print template.render(path, template_values)
            return    
      else:
        """Find UserGroup"""
        q = User.all().filter('account ==', environ['USER_ID'])
        if q.count() == 0:
          u = User(key_name = environ['USER_ID']
             ,account = environ['USER_ID']
             ,password = None
             ,email = environ['USER_EMAIL']
             ,UserGroup_key = db.Key.from_path('UserGroup','0'))
          u.put()
          usergroup = '0'
        else:
          u = q.fetch(1)[0]
          usergroup = u.UserGroup_key.key().id_or_name()

        environ['usergroup'] = usergroup
        logging.info('usergroup %s' % (usergroup))

        if not AuthUserGroupPath(environ['PATH_INFO'], usergroup):
          print 'Status: 400'
          return
        else:
          pass


    """This is the RESPONSE side of the middleware"""
    def _start_response(status, headers):
      if google_user_id != True:
        if signed != True:
          cookiefound = False
          for key, value in headers:
            if key == 'Set-Cookie':
              cookiefound = True
           
          if cookiefound == False:
            if userAuth == True:
              sessioncookie = SetUserInfoCookie(COOKIE_NAME, CreateCookieData(user), '3600')
              cookie = ['Set-Cookie', sessioncookie]
              headers.append(cookie)
            
          p3p = ['P3P', 'policyref="http://medlem3060.appspot.com/w3c/privacy_policy.p3p", CP="ALL DSP COR CURa ADMa DEVa TAIa IVAi IVDi CONi HISi TELi OUR IND PHY ONL FIN COM NAV INT DEM GOV"']
          headers.append(p3p)
      
      logging.info('MHA-Response-Logging user: %s, signed: %s' % (user, signed))
      for key, value in headers:
        logging.info('%s: %s' % (key, value))   
      return start_response(status, headers) 
 
    return self.__application(environ, _start_response) 