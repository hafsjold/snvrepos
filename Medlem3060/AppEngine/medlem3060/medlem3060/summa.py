# coding=utf-8 
from google.appengine.ext import webapp
from google.appengine.ext import db 
from google.appengine.ext.webapp import template

from models import MedlemsStatus, nextval, UserGroup, User, NrSerie, Kreditor, Kontingent, Pbsforsendelse, Pbsfiles, Pbsfile, Sendqueue, Recievequeue, Tilpbs, Fak, Frapbs, Bet, Betlin, Sftp, Infotekst, Sysinfo, Menu, MenuMenuLink, Medlog, Person
from util import PassXmlDoc, lpad, rpad, utc, cet
from datetime import datetime, date, timedelta
import logging
import os
import re
from xml.dom import minidom

class BogforIndBetalingerHandler(webapp.RequestHandler, PassXmlDoc):
  def get(self):
    betalinger = []
    status = False
    qry = Bet.all().filter('Summabogfort !=', True)
    for bet in qry:
      for betlin in bet.listBetlin:
        if betlin.Fakref:
          if betlin.Fakref.SFaknr > 0:
            betalinger.append(betlin)
            status = True
    template_values = {
      'status': status,
      'betalinger': betalinger,
    }
    path = os.path.join(os.path.dirname(__file__), 'templates/bogforindbetalinger.xml')
    self.response.out.write(template.render(path, template_values)) 

  def post(self):
    doc = minidom.parse(self.request.body_file)
    if doc.documentElement.tagName == 'SummabogfortUpdate':
      bets = doc.getElementsByTagName("Bet")
      for bet in bets:
        Id = attr_val(bet, 'Id', 'IntegerProperty') 
        Summabogfort = attr_val(bet, 'Summabogfort', 'BooleanProperty') 
        Key = db.Key.from_path('rootBet','root', 'Bet', '%s' % (Id))
        betrec = db.get(Key)
        betrec.Summabogfort = Summabogfort
        betrec.put()
        logging.info('JJJJJJJJJJJJJJJJJ Id: %s, Summabogfort: %s JJJJJJJJJJJJJJJJJJJ' % (Id, Summabogfort))
      
class OrderFaknrUpdateHandler(webapp.RequestHandler, PassXmlDoc):
  def get(self):
    status = False
    faks = Fak.all().filter('SFakID >', 0).filter('SFaknr =', None)
    if faks:
      status = True
    template_values = {
      'status': status,
      'faks': faks,
    }
    path = os.path.join(os.path.dirname(__file__), 'templates/orderfaknrupdate.xml')
    self.response.out.write(template.render(path, template_values))  

  def post(self):
    doc = minidom.parse(self.request.body_file)
    if doc.documentElement.tagName == 'SFaknrupdate':
      faks = doc.getElementsByTagName("Fak")
      for fak in faks:
        Key = fak.getElementsByTagName("Key")[0].childNodes[0].data
        Id = attr_val(fak, 'Id', 'IntegerProperty')         
        SFaknr = attr_val(fak, 'SFaknr', 'IntegerProperty')         
        fakrec = db.get(Key)
        fakrec.SFaknr = SFaknr
        fakrec.put()
        logging.info('JJJJJJJJJJJJJJJJJ Id: %s, Key: %s, SFaknr: %s JJJJJJJJJJJJJJJJJJJ' % (Id, Key, SFaknr))
        
class Udbetaling2SummaHandler(webapp.RequestHandler):
  def get(self):
    overforsel = []
    status = False
    path = self.request.environ['PATH_INFO']
    mo = re.match("/data/udbetaling2summa/.([0-9]+)", path)
    if mo:
      if mo.groups()[0]:
        lobnr = int(mo.groups()[0])
      else:
        lobnr = None
    else:
      lobnr = None
    if lobnr:
      tilpbskey = db.Key.from_path('rootTilpbs','root','Tilpbs','%s' % (lobnr))
      rsttil = Tilpbs.get(tilpbskey)
      if not rsttil: 
        #raise PbsOverforselError('101 - Der er ingen PBS forsendelse for id: %s' % (lobnr))
        status = False
      else:
        for rec in rsttil.listOverforsel:
          overforsel.append(rec)
          status = True
          
    template_values = {
      'allFields': True,
      'status': status,
      'forslag': overforsel,
    }
    path = os.path.join(os.path.dirname(__file__), 'templates/overforsel.xml')
    self.response.out.write(template.render(path, template_values))
        

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