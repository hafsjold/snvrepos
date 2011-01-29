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
import sqlite3  

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
  
class Mapper(object):
  # Subclasses should replace this with a model class (eg, model.Person).
  KIND = None

  # Subclasses can replace this with a list of (property, value) tuples to filter by.
  FILTERS = []

  def map(self, entity):
    """Updates a single entity.

    Implementers should return a tuple containing two iterables (to_update, to_delete).
    """
    return ([], [])

  def get_query(self):
    """Returns a query over the specified kind, with any appropriate filters applied."""
    q = self.KIND.all()
    for prop, value in self.FILTERS:
      q.filter("%s =" % prop, value)
    q.order("__key__")
    return q

  def run(self, batch_size=100):
    """Executes the map procedure over all matching entities."""
    q = self.get_query()
    entities = q.fetch(batch_size)
    while entities:
      to_put = []
      to_delete = []
      antal = 0
      for entity in entities:
        antal += 1
        map_updates, map_deletes = self.map(entity)
        to_put.extend(map_updates)
        to_delete.extend(map_deletes)
      if to_put:
        db.put(to_put)
      if to_delete:
        db.delete(to_delete)
      q = self.get_query()
      q.filter("__key__ >", entities[-1].key())
      print 'Table: %s, batch_size: %s' % (self.KIND, antal)
      entities = q.fetch(batch_size)
        
class MyModelDeleter(Mapper):
  KIND = Medlog

  def map(self, entity):
    return ([], [entity]) 


class UserGroupBackup(Mapper):
  KIND = UserGroup
  def __init__(self, cur):
    self.cur = cur
  def map(self, e):
    t = (e.GroupName, e.key().__str__() )
    self.cur.execute('insert into tblusergroup ([groupname], [key]) values(?,?)', t)
    return ([], [])

class UserBackup(Mapper):
  KIND = User
  def __init__(self, cur):
    self.cur = cur
  def map(self, e):
    t = (e.account, e.password, e.email, e.UserGroup_key.GroupName, e.key().__str__() )
    self.cur.execute('insert into tbluser ([account], [password], [email], [groupname], [key]) values(?,?,?,?,?)', t)
    return ([], [])

class MenuBackup(Mapper):
  KIND = Menu
  def __init__(self, cur):
    self.cur = cur
  def map(self, e):
    t = (e.Menutext, e.Menulink, e.Target, e.Confirm, e.key().__str__() )
    self.cur.execute('insert into tblmenu ([menutext], [menulink], [target], [confirm],[key]) values(?,?,?,?,?)', t)
    return ([], [])

class MenuMenuLinkBackup(Mapper):
  KIND = MenuMenuLink
  def __init__(self, cur):
    self.cur = cur
  def map(self, e):
    if e.Parent_key:
      ParentMenutext = e.Parent_key.Menutext
    else:
      ParentMenutext = None
    if e.Child_key:
      ChildMenutext = e.Child_key.Menutext
    else:
      ChildMenutext = None
    t = (ParentMenutext, ChildMenutext, e.Menuseq, e.key().__str__() )
    self.cur.execute('insert into tblmenumenulink ([parent], [child], [menuseq], [key]) values(?,?,?,?)', t)
    return ([], [])
    
class PersonBackup(Mapper):
  KIND = Person
  def __init__(self, cur):
    self.cur = cur
  def map(self, e):
    t = (e.Nr, e.Navn, e.Kaldenavn, e.Adresse, e.Postnr, e.Bynavn, e.Email, e.Telefon, e.Kon, e.FodtDato, e.Bank, e.MedlemtilDato, e.MedlemAabenBetalingsdato, e.key().__str__() )
    self.cur.execute('insert into tblperson ([Nr], [navn], [kaldenavn], [adresse], [postnr], [bynavn], [email], [telefon], [kon], [fodtdato], [bank], [medlemtildato], [medlemaabenbetalingsdato], [key]) values(?,?,?,?,?,?,?,?,?,?,?,?,?,?)', t)
    return ([], [])

class MedlogBackup(Mapper):
  KIND = Medlog
  def __init__(self, cur):
    self.cur = cur
  def map(self, e):
    t = (e.Id, e.Source, e.Source_id, e.Nr, e.Logdato, e.Akt_id, e.Akt_dato, e.key().__str__() )
    self.cur.execute('insert into tblmedlog ([id], [Source], [Source_id], [Nr], [logdato], [akt_id], [akt_dato], [key]) values(?,?,?,?,?,?,?,?)', t)
    return ([], [])
    
class PbsforsendelseBackup(Mapper):
  KIND = Pbsforsendelse
  def __init__(self, cur):
    self.cur = cur
  def map(self, e):
    t = (e.Id, e.Delsystem, e.Leverancetype, e.Oprettetaf, e.Oprettet, e.Leveranceid, e.key().__str__() )
    self.cur.execute('insert into tblpbsforsendelse ([id], [delsystem], [leverancetype], [oprettetaf], [oprettet], [leveranceid], [key]) values(?,?,?,?,?,?,?)', t)
    return ([], [])
    
class TilpbsBackup(Mapper):
  KIND = Tilpbs
  def __init__(self, cur):
    self.cur = cur
  def map(self, e):
    t = (e.Id, e.Delsystem, e.Leverancetype, e.Bilagdato, e.Pbsforsendelseref.Id, e.Udtrukket, e.Leverancespecifikation, e.Leverancedannelsesdato, e.key().__str__())
    self.cur.execute('insert into tbltilpbs ([id], [delsystem], [leverancetype], [bilagdato], [pbsforsendelseid], [udtrukket], [leverancespecifikation], [leverancedannelsesdato], [key]) values(?,?,?,?,?,?,?,?,?)', t)
    return ([], [])
    
class FakBackup(Mapper):
  KIND = Fak
  def __init__(self, cur):
    self.cur = cur
  def map(self, e):
    t = (e.Id , e.TilPbsref.Id, e.Betalingsdato, e.Nr, e.Faknr, e.Advistekst, e.Advisbelob, e.Infotekst, e.Bogfkonto, e.Vnr, e.Fradato, e.Tildato, e.SFakID, e.SFaknr, e.Rykkerdato, e.Maildato, e.Rykkerstop, e.Betalt, e.Tilmeldtpbs, e.Indmeldelse, e.key().__str__())
    self.cur.execute('insert into tblfak ([id], [tilpbsid], [betalingsdato], [Nr], [faknr], [advistekst], [advisbelob], [infotekst], [bogfkonto], [vnr], [fradato], [tildato], [SFakID], [SFaknr], [rykkerdato], [maildato], [rykkerstop], [betalt], [tilmeldtpbs], [indmeldelse], [key]) values(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)', t)
    return ([], [])
    
class OverforselBackup(Mapper):
  KIND = Overforsel
  def __init__(self, cur):
    self.cur = cur
  def map(self, e):
    t = (e.Id, e.TilPbsref.Id, e.Nr, e.SFaknr, e.SFakID, e.Advistekst, e.Advisbelob, e.Emailtekst, e.Emailsent, e.Bankregnr, e.Bankkontonr, e.Betalingsdato, e.key().__str__())
    self.cur.execute('insert into tbloverforsel([id], [tilpbsid], [Nr], [SFaknr], [SFakID], [advistekst], [advisbelob], [emailtekst], [emailsent], [bankregnr], [bankkontonr], [betalingsdato], [key]) values(?,?,?,?,?,?,?,?,?,?,?,?,?)', t)
    return ([], [])
    
class RykkerBackup(Mapper):
  KIND = Rykker
  def __init__(self, cur):
    self.cur = cur
  def map(self, e):
    t = (e.Id, e.TilPbsref.Id, e.Betalingsdato, e.Nr, e.Faknr, e.Advistekst, e.Advisbelob, e.Infotekst, e.Rykkerdato, e.Maildato, e.key().__str__())
    self.cur.execute('insert into tblrykker([id], [tilpbsid], [betalingsdato], [Nr], [faknr], [advistekst], [advisbelob], [infotekst], [rykkerdato], [maildato], [key]) values(?,?,?,?,?,?,?,?,?,?,?)', t)
    return ([], [])

class PbsfilesBackup(Mapper):
  KIND = Pbsfiles
  def __init__(self, cur):
    self.cur = cur
  def map(self, e):
    if e.Pbsforsendelseref:
      pbsforsendelseid = e.Pbsforsendelseref.Id
    else:
      pbsforsendelseid = None
    t = (e.Id, e.Type, e.Path, e.Filename, e.Size, e.Atime, e.Mtime, e.Perm, e.Uid, e.Gid, e.Transmittime, pbsforsendelseid, e.key().__str__())
    self.cur.execute('insert into tblpbsfiles([id], [type], [path], [filename], [size], [atime], [mtime], [perm], [uid], [gid], [transmittime], [pbsforsendelseid], [key]) values(?,?,?,?,?,?,?,?,?,?,?,?,?)', t)
    return ([], [])

class PbsfileBackup(Mapper):
  KIND = Pbsfile
  def __init__(self, cur):
    self.cur = cur
  def map(self, e):
    t = (e.Id, e.Pbsfilesref.Id, e.Data, e.key().__str__())
    self.cur.execute('insert into tblpbsfile([id], [pbsfilesid], [data], [key]) values(?,?,?,?)', t)
    return ([], [])
    
class FrapbsBackup(Mapper):
  KIND = Frapbs
  def __init__(self, cur):
    self.cur = cur
  def map(self, e):
    t = (e.Id, e.Delsystem, e.Leverancetype, e.Udtrukket, e.Bilagdato, e.Pbsforsendelseref.Id, e.Leverancespecifikation, e.Leverancedannelsesdato, e.key().__str__())
    self.cur.execute('insert into tblfrapbs([id], [delsystem], [leverancetype], [udtrukket], [bilagdato], [pbsforsendelseid], [leverancespecifikation], [leverancedannelsesdato], [key]) values(?,?,?,?,?,?,?,?,?)', t)
    return ([], [])
    
class BetBackup(Mapper):
  KIND = Bet
  def __init__(self, cur):
    self.cur = cur
  def map(self, e):
    t = (e.Id, e.Frapbsref.Id, e.Pbssektionnr, e.Transkode, e.Bogforingsdato, e.Indbetalingsbelob, e.Summabogfort, e.key().__str__())
    self.cur.execute('insert into tblbet([id], [frapbsid], [pbssektionnr], [transkode], [bogforingsdato], [indbetalingsbelob], [summabogfort], [key]) values(?,?,?,?,?,?,?,?)', t)
    return ([], [])    
    
class BetlinBackup(Mapper):
  KIND = Betlin
  def __init__(self, cur):
    self.cur = cur
  def map(self, e):
    t = (e.Id, e.Betref.Id, e.Pbssektionnr, e.Pbstranskode, e.Nr, e.Faknr, e.Debitorkonto, e.Aftalenr, e.Betalingsdato, e.Belob, e.Indbetalingsdato, e.Bogforingsdato, e.Indbetalingsbelob, e.Pbskortart, e.Pbsgebyrbelob, e.Pbsarkivnr, e.key().__str__())
    self.cur.execute('insert into tblbetlin([id], [betid], [pbssektionnr], [pbstranskode], [Nr], [faknr], [debitorkonto], [aftalenr], [betalingsdato], [belob], [indbetalingsdato], [bogforingsdato], [indbetalingsbelob], [pbskortart], [pbsgebyrbelob], [pbsarkivnr], [key]) values(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)', t)
    return ([], [])    

class AftalelinBackup(Mapper):
  KIND = Aftalelin
  def __init__(self, cur):
    self.cur = cur
  def map(self, e):
    t = (e.Id, e.Frapbsref.Id, e.Pbstranskode, e.Nr, e.Debitorkonto, e.Debgrpnr, e.Aftalenr, e.Aftalestartdato, e.Aftaleslutdato, e.Pbssektionnr, e.key().__str__())
    self.cur.execute('insert into tblaftalelin([id], [frapbsid], [pbstranskode], [Nr], [debitorkonto], [debgrpnr], [aftalenr], [aftalestartdato], [aftaleslutdato], [pbssektionnr], [key]) values(?,?,?,?,?,?,?,?,?,?,?)', t)
    return ([], [])    
    
class IndbetalingskortBackup(Mapper):
  KIND = Indbetalingskort
  def __init__(self, cur):
    self.cur = cur
  def map(self, e):
    t = (e.Id, e.Frapbsref.Id, e.Pbstranskode, e.Nr, e.Faknr, e.Debitorkonto, e.Debgrpnr, e.Kortartkode, e.Fikreditornr, e.Indbetalerident, e.Dato, e.Belob, e.Pbssektionnr, e.key().__str__())
    self.cur.execute('insert into tblindbetalingskort([id], [frapbsid], [pbstranskode], [Nr], [faknr], [debitorkonto], [debgrpnr], [kortartkode], [fikreditornr], [indbetalerident], [dato], [belob], [pbssektionnr], [key]) values(?,?,?,?,?,?,?,?,?,?,?,?,?,?)', t)
    return ([], [])    
    
class KreditorBackup(Mapper):
  KIND = Kreditor
  def __init__(self, cur):
    self.cur = cur
  def map(self, e):
    t = (e.Id, e.Datalevnr, e.Datalevnavn, e.Pbsnr, e.Delsystem, e.Regnr, e.Kontonr, e.Debgrpnr, e.Sektionnr, e.Transkodebetaling, e.key().__str__())
    self.cur.execute('insert into tblkreditor (id, datalevnr, datalevnavn, pbsnr, delsystem, regnr, kontonr, debgrpnr, sektionnr, transkodebetaling, [key]) values(?,?,?,?,?,?,?,?,?,?,?)', t)
    return ([], []) 
    
class SysinfoBackup(Mapper):
  KIND = Sysinfo
  def __init__(self, cur):
    self.cur = cur
  def map(self, e):
    t = (e.Vkey, e.Val, e.key().__str__())
    self.cur.execute('insert into tblsysinfo ([vkey], [val], [key]) values(?,?,?)', t)
    return ([], []) 

class InfotekstBackup(Mapper):
  KIND = Infotekst
  def __init__(self, cur):
    self.cur = cur
  def map(self, e):
    t = (e.Id, e.Navn, e.Msgtext, e.key().__str__())
    self.cur.execute('insert into tblinfotekst ([id], [navn], [msgtext], [key]) values(?,?,?,?)', t)
    return ([], []) 

class SftpBackup(Mapper):
  KIND = Sftp
  def __init__(self, cur):
    self.cur = cur
  def map(self, e):
    t = (e.Id, e.Navn, e.Host, e.Port, e.User, e.Outbound, e.Inbound, e.Pincode, e.Certificate, e.key().__str__())
    self.cur.execute('insert into tblSftp ([id], [navn], [host], [port], [user], [outbound], [inbound], [pincode], [certificate], [key]) values(?,?,?,?,?,?,?,?,?,?)', t)
    return ([], []) 
    
class NrSerieBackup(Mapper):
  KIND = NrSerie
  def __init__(self, cur):
    self.cur = cur
  def map(self, e):
    if not e.Nrserienavn:
      Nrserienavn = e.key().name()
    else:
      Nrserienavn = e.Nrserienavn
    t = (Nrserienavn, e.Sidstbrugtenr, e.key().__str__())
    self.cur.execute('insert into tblnrserie ([nrserienavn], [sidstbrugtenr], [key]) values(?,?,?)', t)
    return ([], []) 

class KontingentBackup(Mapper):
  KIND = Kontingent
  def __init__(self, cur):
    self.cur = cur
  def map(self, e):
    t = (e.Id, e.Nr, e.Advisbelob, e.Fradato, e.Tildato, e.key().__str__())
    self.cur.execute('insert into tblkontingent ([id], [Nr], [advisbelob], [fradato], [tildato], [key]) values(?,?,?,?,?,?)', t)
    return ([], []) 
    
def createDatabase(database_name):
  sql_script_path = os.path.join(os.path.dirname(__file__), 'createDatabase.sql')
  sql_script_file = open(sql_script_path, "r")
  sql_script = sql_script_file.read()
  sql_script_file.close()
  database_path = os.path.join(os.path.dirname(__file__), '%s.sql3' % (database_name))
  conn = sqlite3.connect(database_path)
  conn.executescript(sql_script)
  conn.commit()
  return conn

def mha(database_name):
  conn = createDatabase(database_name)
  cur = conn.cursor()
  
  mapper = UserGroupBackup(cur)
  mapper.run() 
  conn.commit()
  
  mapper = UserBackup(cur)
  mapper.run() 
  conn.commit()
  
  mapper = MenuBackup(cur)
  mapper.run() 
  conn.commit()
  
  mapper = MenuMenuLinkBackup(cur)
  mapper.run() 
  conn.commit()
  
  mapper = PersonBackup(cur)
  mapper.run() 
  conn.commit()
  
  mapper = MedlogBackup(cur)
  mapper.run() 
  conn.commit()
      
  mapper = SysinfoBackup(cur)
  mapper.run() 
  conn.commit()
      
  mapper = InfotekstBackup(cur)
  mapper.run() 
  conn.commit()  
      
  mapper = SftpBackup(cur)
  mapper.run() 
  conn.commit()  

  mapper = NrSerieBackup(cur)
  mapper.run() 
  conn.commit()  

  mapper = KontingentBackup(cur)
  mapper.run() 
  conn.commit()  
  
def mhaxx():  
  mapper = PbsforsendelseBackup(cur)
  mapper.run() 
  conn.commit()

  mapper = TilpbsBackup(cur)
  mapper.run() 
  conn.commit()
  
  mapper = FakBackup(cur)
  mapper.run() 
  conn.commit()
  
  mapper = OverforselBackup(cur)
  mapper.run() 
  conn.commit()
  
  mapper = RykkerBackup(cur)
  mapper.run() 
  conn.commit() 
  
  mapper = PbsfilesBackup(cur)
  mapper.run() 
  conn.commit()

  mapper = PbsfileBackup(cur)
  mapper.run() 
  conn.commit()

  mapper = FrapbsBackup(cur)
  mapper.run() 
  conn.commit()

  mapper = BetBackup(cur)
  mapper.run() 
  conn.commit()

  mapper = BetlinBackup(cur)
  mapper.run() 
  conn.commit()

  mapper = AftalelinBackup(cur)
  mapper.run() 
  conn.commit()

  mapper = IndbetalingskortBackup(cur)
  mapper.run() 
  conn.commit()
  
  mapper = KreditorBackup(cur)
  mapper.run() 
  conn.commit()

  conn.close()
  
remote_api_stub.ConfigureRemoteDatastore(app_id, '/_ah/remote_api', auth_func, host)


code.interact('App Engine interactive console for %s' % (app_id,), None, locals())   