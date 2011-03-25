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

class RykkerForslagHandler(webapp.RequestHandler):
  def get(self):
    forslag = []
    antal = 0
    dt = datetime.now(cet).replace(tzinfo = None)
    nu = dt.date()
    root = db.Key.from_path('Persons','root')
    fak_rec_list = db.Query(Fak).ancestor(root).filter('SFaknr =', None).filter('Rykkerstop =', False)
    for fak_rec in fak_rec_list:
      rykkerdato = fak_rec.Betalingsdato + timedelta(days = 7)
      if nu > rykkerdato:
        antalrykkere = 0
        for rykker_rec in fak_rec.listRykker:
          if rykker_rec.Faknr == fak_rec.Faknr:
            antalrykkere += 1
        if antalrykkere == 0:
          objMedlemsStatus = MedlemsStatus(fak_rec.person.key())
          if objMedlemsStatus.kanRykkes() == True:
            forslag.append(fak_rec)
            antal += 1
    
    status = (antal > 0)    
    template_values = {
      'status': status,
      'forslag': forslag,
    }
    path = os.path.join(os.path.dirname(__file__), 'templates/rykkerforslag.xml')
    self.response.out.write(template.render(path, template_values))  
    
class TidligereRykkerHandler(webapp.RequestHandler):
  def get(self):
    forslag = []
    antal = 0
    dt = datetime.now(cet).replace(tzinfo = None)
    nu = dt.date()
    root = db.Key.from_path('Persons','root')
    fak_rec_list = db.Query(Fak).ancestor(root).filter('SFaknr =', None).filter('Rykkerstop =', False)
    for fak_rec in fak_rec_list:
        antalrykkere = 0
        for rykker_rec in fak_rec.listRykker:
          if rykker_rec.Faknr == fak_rec.Faknr:
            antalrykkere += 1
        if antalrykkere > 0:
          objMedlemsStatus = MedlemsStatus(fak_rec.person.key())
          if objMedlemsStatus.kanRykkes() == True:
            forslag.append(fak_rec)
            antal += 1
    
    status = (antal > 0)    
    template_values = {
      'status': status,
      'forslag': forslag,
    }
    path = os.path.join(os.path.dirname(__file__), 'templates/rykkerforslag.xml')
    self.response.out.write(template.render(path, template_values))  