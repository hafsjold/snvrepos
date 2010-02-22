import sys
import logging
import Cookie
from Cookie import BaseCookie
import hashlib
from google.appengine.api import memcache
import random

COOKIE_NAME = 'adm3060_session'
LOGIN_URL = '/login'
LOGIN_TEMPLATE = """<html>
<head>
  <title>Login</title>
</head>
<body>

<form method='%(method)s' action='%(login_url)s'
      style='text-align:center; font: 13px sans-serif'>
  <div style='width: 22em; margin: 1em auto;
              text-align:left;
              padding: 0 2em 1.25em 2em;
              background-color: #d6e9f8;
              border: 2px solid #67a7e3'>
    <h3>%(login_message)s</h3>
    <p style='padding: 0; margin: 0'>
      <label for='account' style="width: 5em">User:</label>
      <input name='account' type='text' value='%(account)s' id='account'/>
    </p>
    <p style='padding: 0; margin: 0'>
      <label for='password' style="width: 5em">Password:</label>
      <input name='password' type='password' id='password'/>
    </p>
    <p style='margin-left: 5em'>
      <input name='action' value='Login' type='submit' id='submit-login' />
      <input name='action' value='Logout' type='submit' id='submit-logout' />
      <input name='continue' type='hidden' value='%(continue_url)s'/>
    </p>
  </div>
</form>

</body>
</html>
""" 

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