# coding=utf-8 
from google.appengine.ext import webapp
from google.appengine.ext import db 
from google.appengine.ext.webapp import template

from models import MedlemsStatus, nextval, UserGroup, User, NrSerie, Kreditor, Kontingent, Pbsforsendelse, Pbsfiles, Pbsfile, Sendqueue, Recievequeue, Tilpbs, Fak, Frapbs, Bet, Betlin, Sftp, Infotekst, Sysinfo, Menu, MenuMenuLink, Medlog, Person
from util import lpad, rpad, utc, cet
from datetime import datetime, date, timedelta
import logging
import os
import re
from xml.dom import minidom

class KontingentForslagHandler(webapp.RequestHandler):
  def get(self):
    forslag = []
    antal = 0
    root = db.Key.from_path('Persons','root')
    person_key_list = db.Query(Person, keys_only=True).ancestor(root)
    for key_person in person_key_list:
      objMedlemsStatus = MedlemsStatus(key_person)
      if objMedlemsStatus.KontingentForslag():
        forslag.append(objMedlemsStatus)
        antal += 1
    
    status = (antal > 0)    
    template_values = {
      'status': status,
      'forslag': forslag,
    }
    path = os.path.join(os.path.dirname(__file__), 'templates/kontingentforslag.xml')
    self.response.out.write(template.render(path, template_values))  

class PersonListHandler(webapp.RequestHandler):
  def get(self):
    forslag = []
    antal = 0
    root = db.Key.from_path('Persons','root')
    person_key_list = db.Query(Person, keys_only=True).ancestor(root)
    for key_person in person_key_list:
      objMedlemsStatus = MedlemsStatus(key_person)
      forslag.append(objMedlemsStatus)
      antal += 1
    
    status = (antal > 0)    
    template_values = {
      'status': status,
      'forslag': forslag,
    }
    path = os.path.join(os.path.dirname(__file__), 'templates/personlist.xml')
    self.response.out.write(template.render(path, template_values))  