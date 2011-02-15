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

class clsForslag(object):
  def __init__(self, person, kontingentfradato, indmeldelse, tilmeldtpbs):
    self.Nr = person.Nr
    self.Navn = person.Navn
    self.Adresse = person.Adresse
    self.Postnr = person.Postnr
    self.KontingentFradato = kontingentfradato
    self.Indmeldelse = indmeldelse
    self.Tilmeldtpbs = tilmeldtpbs
    logging.info('%s %s' % (person.Nr,person.Navn))
        
class KontingentForslagHandler(webapp.RequestHandler):
  def get(self):
    forslag = []
    antal = 0
    root = db.Key.from_path('Persons','root')
    person_list = db.Query(Person).ancestor(root)
    for rec_person in person_list:
      objMedlemsStatus = MedlemsStatus(rec_person.key())
      (KontingentFradato, Indmeldelse, Tilmeldtpbs) = objMedlemsStatus.KontingentForslag()
      if KontingentFradato:
        objForslag = clsForslag(rec_person, KontingentFradato, Indmeldelse, Tilmeldtpbs)
        forslag.append(objForslag)
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
    person_list = db.Query(Person).ancestor(root)
    for rec_person in person_list:
      objMedlemsStatus = MedlemsStatus(rec_person.key())
      forslag.append(objMedlemsStatus)
      antal += 1
    
    status = (antal > 0)    
    template_values = {
      'status': status,
      'forslag': forslag,
    }
    path = os.path.join(os.path.dirname(__file__), 'templates/personlist.xml')
    self.response.out.write(template.render(path, template_values))  