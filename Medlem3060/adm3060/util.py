import sys
import logging
import Cookie
from Cookie import BaseCookie
import hashlib
from google.appengine.api import memcache
import random

COOKIE_NAME = 'adm3060_session'
LOGIN_URL = '/login'
PUBLIC_URL = ['/login', '/test']

def CreateCookieData(user):
  if user:
    secret = '%d' % random.randint(10000, 32000)
    secret_index = memcache.incr('secret_index', delta=1, namespace=None, initial_value=0) 
    if memcache.add('secret_index_%d' % secret_index, secret, time=60): 
      logging.info('rand = ' + secret)
    
    m = hashlib.sha224()
    m.update(secret)
    m.update(user)
    sessionid = m.hexdigest()
  return '%s:%s:%s' % (sessionid, user, '%d' % secret_index)

def GetUserInfo(http_cookie, cookie_name=COOKIE_NAME):
  cookie = Cookie.SimpleCookie(http_cookie)
  cookie_value = ''
  if cookie_name in cookie:
    cookie_value = cookie[cookie_name].value
  sessionid, user, secret_index = (cookie_value.split(':') + ['', '', ''])[:3]
  logging.info('%s:%s:%s' % (sessionid, user, secret_index))
  
  userAuth = False
  secret = memcache.get('secret_index_' + secret_index)
  if secret != None:  
    m = hashlib.sha224()
    m.update(secret)
    m.update(user)
    if sessionid == m.hexdigest():
      userAuth = True
  return userAuth, user

def SetUserInfoCookie(key, value='', max_age=None,
             path='/', domain=None, secure=None, httponly=False,
             version=None, comment=None):
  cookies = BaseCookie()
  cookies[key] = value
  for var_name, var_value in [
    ('max_age', max_age),
    ('path', path),
    ('domain', domain),
    ('secure', secure),
    ('HttpOnly', httponly),
    ('version', version),
    ('comment', comment),
    ]:
    if var_value is not None and var_value is not False:
      cookies[key][var_name.replace('_', '-')] = str(var_value)
  header_value = cookies[key].output(header='').lstrip()
  return header_value