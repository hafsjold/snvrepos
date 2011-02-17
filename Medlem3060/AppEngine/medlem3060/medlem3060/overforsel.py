# coding=utf-8 
from google.appengine.ext import webapp
from google.appengine.ext import db 
from google.appengine.ext.webapp import template

from models import MedlemsStatus, nextval, UserGroup, User, NrSerie, Kreditor, Kontingent, Pbsforsendelse, Pbsfiles, Pbsfile, Sendqueue, Recievequeue, Tilpbs, Fak, Frapbs, Bet, Betlin, Overforsel, Sftp, Infotekst, Sysinfo, Menu, MenuMenuLink, Medlog, Person
from util import lpad, rpad, utc, cet
from datetime import datetime, date, timedelta
import logging
import os
import re
from xml.dom import minidom

class OverforselHandler(webapp.RequestHandler):
  def get(self):
    root = db.Key.from_path('Persons','root')
    qry = db.Query(Overforsel).ancestor(root).order('SFakID')
    allFields = False
    
    status = (qry.count() > 0)    
    template_values = {
      'allFields': allFields,
      'status': status,
      'forslag': qry,
    }
    path = os.path.join(os.path.dirname(__file__), 'templates/overforsel.xml')
    self.response.out.write(template.render(path, template_values))  