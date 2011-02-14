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

class Order2SummaHandler(webapp.RequestHandler):
  def get(self):
    betlines = []
    antal = 0
    qry = Betlin.all().filter('Pbstranskode in', ['0236', '0297'])
    for b in qry:
      if b.Fakref:
        if b.Fakref.SFakID == None:
          betlines.append(b)
          antal += 1
    
    status = (antal > 0)    
    template_values = {
      'status': status,
      'betlines': betlines,
    }
    path = os.path.join(os.path.dirname(__file__), 'templates/ordre2summa.xml')
    self.response.out.write(template.render(path, template_values))  

  def post(self):
    doc = minidom.parse(self.request.body_file)
    if doc.documentElement.tagName == 'SFakIDupdate':
      faks = doc.getElementsByTagName("Fak")
      for fak in faks:
        Key = fak.getElementsByTagName("Key")[0].childNodes[0].data 
        Id = fak.getElementsByTagName("Id")[0].childNodes[0].data 
        strval = fak.getElementsByTagName("SFakID")[0].childNodes[0].data 
        SFakID = int(strval)
        fakrec = db.get(Key)
        fakrec.SFakID = SFakID
        fakrec.put()
        logging.info('JJJJJJJJJJJJJJJJJ Id: %s, Key: %s, SFakID: %s JJJJJJJJJJJJJJJJJJJ' % (Id, Key, SFakID))