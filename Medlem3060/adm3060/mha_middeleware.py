
from util import COOKIE_NAME, LOGIN_URL, LOGIN_TEMPLATE, CreateCookieData, GetUserInfo, SetUserInfoCookie
import logging
import pprint

class LoggingMiddleware: 

  def __init__(self, application): 
    self.__application = application 
 
  """This is the REQUEST side of the middleware"""
  def __call__(self, environ, start_response):
    for key, value in environ.iteritems():
      logging.info('%s: %s' % (key, value))
    
    
    try:
      userAuth, user  = GetUserInfo(environ['HTTP_COOKIE'])
    except:
      userAuth, user  = False, ''
    
    environ['userAuth'] = userAuth
    environ['user3060'] = user
    logging.info('userAuth: %s, user: %s' % (userAuth, user))
    
    errors = environ['wsgi.errors']
    pprint.pprint(('REQUEST', environ), stream=errors)

    if userAuth != True:   
      if environ['PATH_INFO'] != LOGIN_URL:
        #display login screen
        template_dict = {
          'account': user,
          'login_message': 'Login',
          'method': 'post',
          'login_url': LOGIN_URL,
          'continue_url': environ['PATH_INFO'], 
        }
        print LOGIN_TEMPLATE % template_dict
        return
    
    """This is the RESPONSE side of the middleware"""
    def _start_response(status, headers):
      cookiefound = False
      for key, value in headers:
        if key == 'Set-Cookie':
          cookiefound = True
         
      if cookiefound == False:
        if userAuth == True:
          sessioncookie = SetUserInfoCookie(COOKIE_NAME, CreateCookieData(user), '3600')
          cookie = ['Set-Cookie', sessioncookie]
          headers.append(cookie)
          
      p3p = ['P3P', 'policyref="http://adm3060.appspot.com/w3c/privacy_policy.p3p", CP="ALL DSP COR CURa ADMa DEVa TAIa IVAi IVDi CONi HISi TELi OUR IND PHY ONL FIN COM NAV INT DEM GOV"']
      headers.append(p3p)
      
      pprint.pprint(('RESPONSE', status, headers), stream=errors)    
      logging.info('MHA-Response-Logging %s' % user)
      for key, value in headers:
        logging.info('%s: %s' % (key, value))      
      return start_response(status, headers) 
 
    return self.__application(environ, _start_response) 