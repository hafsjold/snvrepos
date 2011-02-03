# coding=utf-8 
import sys
from certificates import MYSECRET, MYCERT, MYKEY
import Cookie
from Cookie import BaseCookie
import hashlib
from google.appengine.api import memcache
import random
from tlslite.utils.RSAKey import RSAKey 
import tlslite.utils.keyfactory 
import tlslite.utils.compat 
import tlslite.utils.cryptomath
from tlslite import X509
import sha
import time
import logging
import re

COOKIE_NAME = 'medlem3060_session'
LOGIN_URL = '/login'
PUBLIC_URL = ['/', '/login']




def AuthUserGroupPath(path, usergroup, is_admin):
  if is_admin:
    return True
    
  authpath = [u'/adm']
  if usergroup == '0':
    authpath = [u'/adm'
    ,u'/logoff'
    ]
  elif usergroup == '1':  
    authpath = [u'/adm'
      ,u'/logoff'
      ,u'/adm/findmedlem3'
      ,u'/adm/medlemjson'
      ,u'/adm/medlemlogjson'
      ,u'/adm/medlem'
      ,u'/adm/medlem/[0-9]+'
      ,u'/teknik/createmenu'
      ,u'/teknik/flushcache' 
      ,u'/teknik/reindex'
      ,u'/teknik/listuser'
      ,u'/teknik/user/.+'      
    ]
  elif usergroup == '2':  
    authpath = [u'/adm'
      ,u'/logoff'
      ,u'/adm/medlemjson'
      ,u'/adm/medlemlogjson'
      ,u'/adm/findmedlem3'
      ,u'/adm/medlem'
      ,u'/adm/medlem/[0-9]+'
    ]
  logging.info('AuthUserGroupPath path: %s, authpath: %s' % (path, authpath))
  
  for p in authpath[:]:
    mo = re.match(p + '$', path)
    if mo:
      return True
  return False
  
def AuthRest(http_timestamp, http_signed):
  #logging.info('%s: %s' % ('http_timestamp', http_timestamp))
  #logging.info('%s: %s' % ('http_signed', http_signed))
  dif = abs(float(http_timestamp) - time.time())
  logging.info('%s: %s' % ('dif', dif))
  if dif > 120:
    return False
  
  mycertobj = X509.X509()
  mycertobj.parse(MYCERT)
  mypublickey = mycertobj.publicKey
  
  verify = mypublickey.hashAndVerify (tlslite.utils.cryptomath.base64ToBytes(http_signed), tlslite.utils.compat.stringToBytes('%s%s' % (http_timestamp, MYSECRET)))
  if verify:
    logging.info('%s: %s' % ('verify', 'TRUE'))
    return True
  else:
    logging.info('%s: %s' % ('verify', 'FALSE'))
    return False

def TestCrypt(data):
  mycertobj = X509.X509()
  mycertobj.parse(MYCERT)
  Fingerprint = mycertobj.getFingerprint()
  
  mypublickey = mycertobj.publicKey
  myprivatekey = tlslite.utils.keyfactory.parsePEMKey(MYKEY, private=True)
  mypublicXMLkeyString = myprivatekey.writeXMLPublicKey()
  
  data2 = '2010-02-02 21:15:00' 
  logging.info('%s: %s' % ('data', data))
  logging.info('%s: %s' % ('time.time', time.time()))


  
  myhashAndSign = myprivatekey.hashAndSign (tlslite.utils.compat.stringToBytes('%s%s' % (data2, MYSECRET)))
  logging.info('%s: %s' % ('myhashAndSign', tlslite.utils.cryptomath.bytesToBase64(myhashAndSign)))
  
  verify = mypublickey.hashAndVerify (myhashAndSign, '%s%s' % (data2, MYSECRET))
  if verify:
    logging.info('%s: %s' % ('verify', 'TRUE'))
  else:
    logging.info('%s: %s' % ('verify', 'FALSE'))

  mycrypt = mypublickey.encrypt(tlslite.utils.compat.stringToBytes(data))
  
  mydecrypt = myprivatekey.decrypt(mycrypt)
  logging.info('%s: %s' % ('mydecrypt', mydecrypt.tostring()))
 
def CreateCookieData(user):
  if user:
    secret = '%d' % random.randint(10000, 32000)
    secret_index = memcache.incr('secret_index', delta=1, namespace=None, initial_value=0) 
    if memcache.add('secret_index_%d' % secret_index, secret, time=360): 
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
  
def lpad (oVal, Length, PadChar):
  Val = '%s' % (oVal)
  return Val.rjust(Length, PadChar)

def rpad (oVal, Length, PadChar):
  Val = '%s' % (oVal)
  return Val.ljust(Length, PadChar)