# coding=utf-8 
from google.appengine.ext import webapp
from google.appengine.ext import db 
from google.appengine.ext.webapp import template

from models import nextval, UserGroup, User, NrSerie, Kreditor, Kontingent, Pbsforsendelse:, Pbsfiles, Pbsfile, Sendqueue, Recievequeue, Tilpbs, Fak, Frapbs, Bet, Betlin, Aftalelin, Indbetalingskort, Sftp, Infotekst, Sysinfo, Menu, MenuMenuLink, Medlog, Person
from util import lpad, rpad, utc, cet
from datetime import datetime, date, timedelta
import logging
import os
import re
from xml.dom import minidom

class Pbs603Error(Exception):
  def __init__(self, value):
    self.value = value
  def __str__(self):
    return repr(self.value)

class pbs603Handler(webapp.RequestHandler):

  self.m_rec_recievequeue = None
  self.m_rec_pbsforsendelse = None
  self.m_rec_pbsfiles = None
  self.m_rec_frapbs = None
  self.m_rec_aftalelin = None
  self.m_rec_indbetalingskort = None

  def get(self):
    antal = self.aftaleoplysninger_fra_pbs()
    self.response.out.write('Antal filer %s' % (antal))
    
  def aftaleoplysninger_fra_pbs():
    rec = None
    leverance = None
    leverancespecifikation = None
    leverancedannelsesdato = None
    sektion = None
    wpbsfilesid = None
    wleveranceid = None
    AntalFiler = 0

    qry = db.Query(Recievequeue).filter('Recieved_from_pbs =',False).filter('Onhold =',False)
    for self.m_rec_recievequeue in qry:
      data = self.m_rec_recievequeue.Pbsfileref.Data
      datalines = data.split(crlf)
      #Strip off PBCNET records
      if datalines[0][:6] == 'PBCNET':
        del datalines[0]
        if datalines[-1][:6] == 'PBCNET':
          del datalines[-1]
      #Test for BS 0603 record
      if datalines[0][16:20] == '0603' and datalines[0][:2] == 'BS':
        try:
          wpbsfilesid = self.m_rec_recievequeue.Pbsfileref.Pbsfilesref.Id
          AntalFiler += 1
          leverancetype = ''
          sektion = ''
          leverancespecifikation = ''
          Seqnr = 0
          for rec in datalines:
            Seqnr += 1
            #  Bestem Leverance Type
            if rstpbsfile.Seqnr == 1:
              if rec[:5] == 'BS002':
                #  Leverance Start
                leverance = rec[16:20]
                leverancespecifikation = rec[20:30]
                leverancedannelsesdato = datetime.strptime('20' + rec[53:55] + '-' + rec[51:53] + '-' + rec[49:51], "%Y-%m-%d")
              else:
                raise Pbs603Error('241 - Foerste record er ikke en Leverance start record')

              if leverance == '0603':
                # -- Leverance 0603
                qry = Frapbs.all().filter('Leverancespecifikation =', leverancespecifikation)
                antal = qry.count()
                if antal > 0: 
                  raise Pbs603Error('242 - Leverance med pbsfilesid: %s og leverancespecifikation: %s er indlaest tidligere' % (wpbsfilesid, leverancespecifikation))

                wleveranceid = nextval('leveranceid')
                pbsforsendelseid  = nextval('Pbsforsendelseid')    
                root_pbsforsendelse = db.Key.from_path('rootPbsforsendelse','root')    
                self.m_rec_pbsforsendelse = Pbsforsendelse.get_or_insert('%s' % (pbsforsendelseid), parent=root_pbsforsendelse)
                self.m_rec_pbsforsendelse.Id = pbsforsendelseid
                self.m_rec_pbsforsendelse.Delsystem = 'BS1'
                self.m_rec_pbsforsendelse.Leverancetype = '0603'
                self.m_rec_pbsforsendelse.Oprettetaf = 'Bet'
                self.m_rec_pbsforsendelse.Oprettet = datetime.now()
                self.m_rec_pbsforsendelse.Leveranceid = wleveranceid
                self.m_rec_pbsforsendelse.put()

                self.m_rec_recievequeue.Pbsfileref.Pbsfilesref.Pbsforsendelseref = self.m_rec_pbsforsendelse.key()
                self.m_rec_recievequeue.Pbsfileref.Pbsfilesref.put()
                
                frapbsid  = nextval('Frapbsid') 
                root_frapbs = db.Key.from_path('rootFrapbs','root')    
                self.m_rec_frapbs = Frapbs.get_or_insert('%s' % (frapbsid), parent=root_frapbs)
                self.m_rec_frapbs.Id = frapbsid
                self.m_rec_frapbs.Delsystem = 'BS1'
                self.m_rec_frapbs.Leverancetype = '0603'
                self.m_rec_frapbs.LBilagdato = None
                self.m_rec_frapbs.Pbsforsendelseref = self.m_rec_pbsforsendelse.key()
                self.m_rec_frapbs.Leverancespecifikation = leverancespecifikation
                self.m_rec_frapbs.Leverancedannelsesdato = leverancedannelsesdato
                self.m_rec_frapbs.Udtrukket = datetime.now()                
                self.m_rec_frapbs.put()

                
            if leverance == '0603':
              #  Leverance 0603

              #  Bestem Sektions Type
              if sektion == '':
                if rec[:5] == 'BS012':
                  #  Sektion Start
                  sektion = rec[13:17]
                else:
                  if not (rec[:5] == 'BS992' or rec[:5] == 'BS002'):
                    raise Pbs603Error('243 - Foerste record er ikke en Sektions start record')

              if rec[:5] == 'BS002':
                #  Leverance start
                #  BEHANDL: Leverance start
                pass
                # -******************************************************************************************************
                # -******************************************************************************************************
              elif sektion == '0210':
                #  Sektion 0210 Aktive aftaler
                if rec[:5] == 'BS012' and rec[13:17] == '0210':
                  #  Sektion Start
                  #  BEHANDL: Sektion Start
                  pass
                elif rec[:5] == 'BS042' and rec[13:17] == '0230':
                  #  Aktive aftaler
                  #  BEHANDL: Aktive aftaler
                  self.readaftale042(sektion, '0230', rec)
                elif rec[:5] == 'BS092' and rec[13:17] == '0210':
                  #  Sektion Slut
                  #  BEHANDL: Sektion Slut
                  sektion = ''
                else:
                  raise Pbs603Error('244 - Rec# %s ukendt: %s' % (Seqnr, rec))
                # -******************************************************************************************************
                # -******************************************************************************************************
              elif sektion == '0212':
                #  Sektion 0212 Til- og afgang af betalingsaftaler
                if rec[:5] == 'BS012' and rec[13:17] == '0212':
                  #  Sektion Start
                  #  BEHANDL: Sektion Start
                  pass
                elif rec[:5] == 'BS042' and rec[13:17] == '0231':
                  #  Tilgang af nye betalingsaftaler
                  #  BEHANDL: Tilgang af nye betalingsaftaler
                  self.readaftale042( sektion, '0231', rec)
                elif rec[:5] == 'BS042' and rec[13:17] == '0232':
                  #  Aftale afmeldt af pengeinstitut
                  #  BEHANDL: aftale afmeldt af pengeinstitut
                  self.readaftale042(sektion, '0232', rec)
                elif rec[:5] == 'BS042' and rec[13:17] == '0233':
                  #  Aftaler afmeldt af kreditor
                  #  BEHANDL: aftaler afmeldt af kreditor
                  self.readaftale042(sektion, '0233', rec)
                elif rec[:5] == 'BS042' and rec[13:17] == '0234':
                  #  Aftaler afmeldt af betalingsservice
                  #  BEHANDL: aftaler afmeldt af betalingsservice
                  self.readaftale042(sektion, '0234', rec)
                elif rec[:5] == 'BS092' and rec[13:17] == '0212'
                  #  Sektion Slut
                  #  BEHANDL: Sektion Slut
                  sektion = ''
                else:
                  raise Pbs603Error('245 - Rec# %s ukendt: %s' % (Seqnr, rec))
                # -******************************************************************************************************
                # -******************************************************************************************************
              elif sektion == '0214':
                #  Sektion 0214 Forfaldsoplysninger
                if rec[:5] == 'BS012' and rec[13:17] == '0214':
                  #  Sektion Start
                  #  BEHANDL: Sektion Start
                  pass
                elif rec[:5] == 'BS042' and rec[13:17] == '0235':
                  #  Forfald automatisk betaling
                  #  BEHANDL: Forfald automatisk betaling
                  pass
                elif rec[:5] == 'BS042' and rec[13:17] == '0295':
                  #  Forfald manuel betaling
                  #  BEHANDL: Forfald manuel betaling
                  pass
                elif rec[:5] == 'BS092' and rec[13:17] == '0214':
                  #  Sektion Slut
                  #  BEHANDL: Sektion Slut
                  sektion = ''
                else:
                  raise Pbs603Error('246 - Rec# %s ukendt: %s' % (Seqnr, rec))
                # -******************************************************************************************************
                # -******************************************************************************************************
              elif sektion == '0215':
                #  Sektion 0215 Debitornavn/-adresse
                if rec[:5] == 'BS012' and rec[13:17) == '0215':
                  #  Sektion Start
                  #  BEHANDL: Sektion Start
                  pass
                elif rec[:5] == 'BS022' and rec[13:17] == '0240':
                  #  Navn/adresse på debitor
                  #  BEHANDL: Navn/adresse på debitor
                  pass
                elif rec[:5] == 'BS092' and rec[13:17) == '0215':
                  #  Sektion Slut
                  #  BEHANDL: Sektion Slut
                  sektion = ''
                else:
                  raise Pbs603Error('247 - Rec# %s ukendt: %s' % (Seqnr, rec))
                # -******************************************************************************************************
                # -******************************************************************************************************
              elif sektion == '0217':
                #  Sektion 0217 Oplysninger fra indbetalingskort
                if rec[:5] == 'BS012' and rec[13:17] == '0217':
                  #  Sektion Start
                  #  BEHANDL: Sektion Start
                  pass
                elif rec[:5] == 'BS042' and rec[13:17] == '0295':
                  #  Oplysninger fra indbetalingskort
                  #  BEHANDL: Oplysninger fra indbetalingskort
                  self.readgirokort042(sektion, '0295', rec)
                elif rec[:5] == 'BS092' and rec[13:17] == '0217'
                  #  Sektion Slut
                  #  BEHANDL: Sektion Slut
                  sektion = ''
                else:
                  raise Pbs603Error('248 - Rec# %s ukendt: %s' % (Seqnr, rec))
                # -******************************************************************************************************
                # -******************************************************************************************************
              elif sektion == '0219':
                #  Sektion 0219 Aktive aftaler om Elektronisk Indbetalingskort
                if rec[:5] == 'BS012' and rec[13:17] == '0219':
                  #  Sektion Start
                  #  BEHANDL: Sektion Start
                  pass
                elif rec[:5] == 'BS042' and rec[13:17] == '0230':
                  #  Aktiv aftale om Elektronisk Indbetalingskort
                  #  BEHANDL: Aktiv aftale om Elektronisk Indbetalingskort
                  pass
                elif rec[:5] == 'BS092' and rec[(13:17) == '0219':
                  #  Sektion Slut
                  #  BEHANDL: Sektion Slut
                  sektion = ''
                else:
                  raise Pbs603Error('249 - Rec# %s ukendt: %s' % (Seqnr, rec))
                # -******************************************************************************************************
                # -******************************************************************************************************
              elif rec[:5] == 'BS992':
                #  Leverance slut
                #  BEHANDL: Leverance Slut
                leverance = ''
              else:
                raise Pbs603Error('250 - Rec# %s ukendt: %s' % (Seqnr, rec))
         
            else:
              raise Pbs603Error('251 - Rec# %s ukendt: %s' % (Seqnr, rec))

     
      except Pbs603Error, e:
        msg = '%s' % e.value
        if msg[:3] == '241':   
            #241 - Første record er ikke en Leverance start record
            AntalFiler -= 1
        elif msg[:3] == '242':  
            #242 - Leverancen er indlæst tidligere
            AntalFiler -= 1
        elif msg[:3] == '243':   
            #243 - Første record er ikke en Sektions start record
            AntalFiler -= 1
        elif msg[:3] == '244':   
            #244 - Record ukendt
            AntalFiler -= 1
        elif msg[:3] == '245':   
            #245 - Record ukendt
            AntalFiler -= 1
        elif msg[:3] == '246':   
            #246 - Record ukendt
            AntalFiler -= 1
        elif msg[:3] == '247':   
            #247 - Record ukendt
            AntalFiler -= 1
        elif msg[:3] == '248':   
            #248 - Record ukendt
            AntalFiler -= 1
        elif msg[:3] == '249':   
            #249 - Record ukendt
            AntalFiler -= 1
        elif msg[:3] == '250':   
            #250 - Record ukendt
            AntalFiler -= 1
        elif msg[:3] == '251':   
            #251 - Record ukendt
            AntalFiler -= 1
        else:
            AntalFiler -= 1
            raise Pbs602Error(msg)

    return AntalFiler


  def readaftale042(self, sektion, transkode, rec):
    # --  pbssektionnr
    # --  pbstranskode
    # - transkode 0230, aktiv aftale
    # - transkode 0231, tilgang af ny betalingsaftale
    # - transkode 0232, aftale afmeldt af pengeinstitut
    # - transkode 0233, aftale afmeldt af kreditor
    # - transkode 0234, aftale afmeldt af betalingsservice

    #  Medlem Nr
    try:
      Nr = int(rec[33:40])
    except:
      Nr = 0

    aftalelinid  = nextval('Aftalelinid') 
    root_aftalelin = db.Key.from_path('Persons','root','Person','%s' % (Nr))    
    self.m_rec_aftalelin = Aftalelin.get_or_insert('%s' % (aftalelinid), parent=root_aftalelin)
    self.m_rec_aftalelin.Id = aftalelinid
    self.m_rec_aftalelin.Pbssektionnr = sektion
    self.m_rec_aftalelin.Pbstranskode = transkode   
    self.m_rec_aftalelin.Nr = Nr   
    
    #  debitorkonto
    self.m_rec_aftalelin.Debitorkonto = rec[25:40]

    #  debgrpnr
    self.m_rec_aftalelin.Debgrpnr = rec[20:25]

    #  aftalenr
    self.m_rec_aftalelin.Aftalenr = int(rec[40:49])

    #  aftalestartdato
    if rec[49:55] != '000000':
      self.m_rec_aftalelin.Aftalestartdato = datetime.strptime('20' + rec[53:55] + '-' + rec[51:53] + '-' + rec[49:51], "%Y-%m-%d").date()
    else:
      self.m_rec_aftalelin.Aftalestartdato = None

    #  aftaleslutdato
    if rec[55:61] != '000000':
      self.m_rec_aftalelin.Aftaleslutdato = datetime.strptime('20' + rec[59:61] + '-' + rec[57:59] + '-' + rec[55:57], "%Y-%m-%d").date()
    else:
      self.m_rec_aftalelin.Aftaleslutdato = None

    qry = Person.all().filter('Nr =, self.m_rec_aftalelin.Nr)
    if qry.count() == 1:
      # Add tblaftalelin
      self.m_rec_aftalelin.put()
    else:
      self.m_rec_aftalelin.delete()    


  def readgirokort042(self, sektion, transkode, rec):
    # --  pbssektionnr
    # --  pbstranskode

    belobmun = float(0)
    belob = int(0)

    #  Medlem Nr
    try:
      Nr = int(rec[33:40])
    except:
      Nr = 0
    
    indbetalingskortid  = nextval('Indbetalingskortid') 
    root_indbetalingskort = db.Key.from_path('Persons','root','Person','%s' % (Nr))    
    self.m_rec_indbetalingskort = Indbetalingskort.get_or_insert('%s' % (indbetalingskortid), parent=root_indbetalingskort)
    self.m_rec_indbetalingskort.Id = indbetalingskortid
    self.m_rec_indbetalingskort.Pbssektionnr = sektion
    self.m_rec_indbetalingskort.Pbstranskode = transkode   
    self.m_rec_indbetalingskort.Nr = Nr      
    
    #  debitorkonto
    self.m_rec_indbetalingskort.Debitorkonto = rec[25:40]

    #  debgrpnr
    self.m_rec_indbetalingskort.Debgrpnr = rec[20:25]

    #  Kortartkode
    self.m_rec_indbetalingskort.Kortartkode = rec[40:42]

    #  FI-kreditor
    self.m_rec_indbetalingskort.Fikreditornr  = rec[42:50]

    #  Indbetalerident
    self.m_rec_indbetalingskort.Indbetalerident  = rec[50:69]

    #  dato
    if rec[55:61] != '000000':
      self.m_rec_indbetalingskort.Dato = datetime.strptime('20' + rec[73:75] + '-' + rec[71:73] + '-' + rec[69:71], "%Y-%m-%d").date()
    else:
      self.m_rec_indbetalingskort.Dato = None

    #  Beløb
    belob = int(rec[75:88])
    belobmun = float(float(belob) / 100)
    self.m_rec_indbetalingskort.Belob = belobmun

    #  Faknr
    self.m_rec_indbetalingskort.Faknr = int(rec[88:97])

    qry = Person.all().filter('Nr =, self.m_rec_indbetalingskort.Nr)
    if qry.count() == 1:
      # Add tblindbetalingskort
      self.m_rec_indbetalingskort.put()
    else:
      self.m_rec_indbetalingskort.delete()   