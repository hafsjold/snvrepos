import code
import getpass 

import sys  
sys.path = ['C:\\Documents and Settings\\mha\\Dokumenter\\Medlem3060\\AppEngine\\medlem3060\\medlem3060', 'C:\\Programmer\\Google\\google_appengine', 'C:\\Programmer\\Google\\google_appengine\\lib\\antlr3', 'C:\\Programmer\\Google\\google_appengine\\lib\\django', 'C:\\Programmer\\Google\\google_appengine\\lib\\fancy_urllib', 'C:\\Programmer\\Google\\google_appengine\\lib\\ipaddr', 'C:\\Programmer\\Google\\google_appengine\\lib\\webob', 'C:\\Programmer\\Google\\google_appengine\\lib\\yaml\\lib', 'C:\\Programmer\\Google\\google_appengine', 'C:\\WINDOWS\\system32\\python25.zip', 'C:\\Python25\\DLLs', 'C:\\Python25\\lib', 'C:\\Python25\\lib\\plat-win', 'C:\\Python25\\lib\\lib-tk', 'C:\\Python25', 'C:\\Python25\\lib\\site-packages']

from google.appengine.ext.remote_api import remote_api_stub 

from google.appengine.ext import webapp
from google.appengine.ext.webapp import util
from google.appengine.ext import db 
from google.appengine.ext.webapp import template
from google.appengine.api import users
from google.appengine.api import taskqueue
from google.appengine.api import memcache
from django.utils import simplejson

from xml.dom import minidom
from datetime import datetime, date
import time

import logging
import rest
import os
import re
import sys  

from models import UserGroup, User, NrSerie, Kreditor, Kontingent, Pbsforsendelse, Tilpbs, Fak, Overforsel, Rykker, Pbsfiles, Pbsfile, Frapbs, Bet, Betlin, Aftalelin, Indbetalingskort, Sftp, Infotekst, Sysinfo, Menu, MenuMenuLink, Medlog, Person
from util import TestCrypt, COOKIE_NAME, LOGIN_URL, CreateCookieData, SetUserInfoCookie
from menuusergroup import deleteMenuAndUserGroup, createMenuAndUserGroup
from menu import MenuHandler, ListUserHandler, UserHandler
from pbs601 import TestHandler, nextval
 

def auth_func():     
  #return raw_input('Username:'), getpass.getpass('Password:')
  return 'mogens.hafsjold@gmail.com', getpass.getpass('Password:')  
  
if len(sys.argv) < 2:
  print "Usage: %s app_id [host]" % (sys.argv[0],) 
app_id = sys.argv[1] 
if len(sys.argv) > 2:
  host = sys.argv[2] 
else:
  host = '%s.appspot.com' % app_id  

print host
  
remote_api_stub.ConfigureRemoteDatastore(app_id, '/_ah/remote_api', auth_func, host)

code.interact('App Engine interactive console for %s' % (app_id,), None, locals())   