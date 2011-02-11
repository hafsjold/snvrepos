# coding=utf-8 
from google.appengine.ext import webapp
from google.appengine.ext import db 
from google.appengine.ext.webapp import template

from models import nextval, UserGroup, User, NrSerie, Kreditor, Kontingent, Pbsforsendelse, Pbsfiles, Pbsfile, Sendqueue, Recievequeue, Tilpbs, Fak, Frapbs, Bet, Betlin, Sftp, Infotekst, Sysinfo, Menu, MenuMenuLink, Medlog, Person
from util import lpad, rpad, utc, cet
from datetime import datetime, date, timedelta
import logging
import os
import re
from xml.dom import minidom

class Pbs602Error(Exception):
  def __init__(self, value):
    self.value = value
  def __str__(self):
    return repr(self.value)

class pbs602Handler(webapp.RequestHandler):
  
  self.m_rec_recievequeue = None
  self.m_rec_pbsforsendelse = None
  self.m_rec_pbsfiles = None
  self.m_rec_frapbs = None
  self.m_rec_bet = None
  self.m_rec_betlin = None

  def get(self):
    antal = self.betalinger_fra_pbs()
    self.response.out.write('Antal filer %s' % (antal))
    #if lobnr:
    #  sendqueueid  = self.faktura_og_rykker_601_action(lobnr)
    #  logging.info('WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW sendqueueid: %s' % (sendqueueid))
    #  self.response.out.write('%s faktureret' % (antal))
    #else:
    #  self.response.out.write('Intet at fakturere')
      
  def betalinger_fra_pbs(self):
    rec = None
    leverancetype = None
    leverancespecifikation  = None
    leverancedannelsesdato = None
    sektion = None
    wpbsfilesid = None
    wleveranceid = None
    AntalFiler = 0
    crlf = '\n'
    #  wpbsfilesid = 3450  #'--test test
    #  leverancetype = '0602'
    #  sektion = '0211'
    #  rec = 'BS0420398564402360000000100000000001231312345678910120310000000012755000000125                         3112031112030000000012755'

    qry = db.Query(Recievequeue).filter('Recieved_from_pbs =',False).filter('Onhold =',False)
    for self.m_rec_recievequeue in qry:
      data = self.m_rec_recievequeue.Pbsfileref.Data
      datalines = data.split(crlf)
      #Strip off PBCNET records
      if datalines[0][:6] == 'PBCNET':
        del datalines[0]
        if datalines[-1][:6] == 'PBCNET':
          del datalines[-1]
      #Test for BS 0602 record
      if datalines[0][16:20] == '0602' and datalines[0][:2] == 'BS':
        try:
          wpbsfilesid = self.m_rec_recievequeue.Pbsfileref.Pbsfilesref.Id
          AntalFiler += 1
          leverancetype = ''
          sektion = ''
          leverancespecifikation = ''
          Seqnr = 0
          for rec in datalines:
            Seqnr += 1
            # -- Bestem Leverance Type
            if Seqnr == 1:
              if rec[0:5] == 'BS002':
                # -- Leverance Start
                leverancetype = rec[16:20]
                leverancespecifikation = rec[20:30]
                leverancedannelsesdato = datetime.strptime('20' + rec[53:55] + '-' + rec[51:53] + '-' + rec[49:51], "%Y-%m-%d")
              else:
                raise Pbs602Error('241 - Foerste record er ikke en Leverance start record')

              if leverancetype == '0602':
                # -- Leverance 0602
                qry = Frapbs.all().filter('Leverancespecifikation =', leverancespecifikation)
                antal = qry.count()
                if antal > 0:
                  pass
                  raise Pbs602Error('242 - Leverance med pbsfilesid: %s og leverancespecifikation: %s er indlaest tidligere' % (wpbsfilesid, leverancespecifikation))

                wleveranceid = nextval('leveranceid')
                pbsforsendelseid  = nextval('Pbsforsendelseid')    
                root_pbsforsendelse = db.Key.from_path('rootPbsforsendelse','root')    
                self.m_rec_pbsforsendelse = Pbsforsendelse.get_or_insert('%s' % (pbsforsendelseid), parent=root_pbsforsendelse)
                self.m_rec_pbsforsendelse.Id = pbsforsendelseid
                self.m_rec_pbsforsendelse.Delsystem = 'BS1'
                self.m_rec_pbsforsendelse.Leverancetype = '0602'
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
                self.m_rec_frapbs.Leverancetype = '0602'
                self.m_rec_frapbs.LBilagdato = None
                self.m_rec_frapbs.Pbsforsendelseref = self.m_rec_pbsforsendelse.key()
                self.m_rec_frapbs.Leverancespecifikation = leverancespecifikation
                self.m_rec_frapbs.Leverancedannelsesdato = leverancedannelsesdato
                self.m_rec_frapbs.Udtrukket = datetime.now()                
                self.m_rec_frapbs.put()

            if leverancetype == '0602':
              # -- Leverance 0602*********
              # -- Bestem Sektions Type
              if sektion == '':
                if rec[0:5] == 'BS012':
                  # -- Sektion Start
                  sektion = rec[13:17]
                elif not (rec[0:5] == 'BS992' or rec[0:5] == 'BS002'):
                  raise Pbs602Error('243 - Foerste record er ikke en Sektions start record')
              
              if rec[0:5] == 'BS002':
                # -- Leverance start
                # -- BEHANDL: Leverance start
                pass
              elif sektion == '0211':
                # -- Sektion 0211 Betalingsinformation
                if rec[0:5] == 'BS012' and rec[13:17] == '0211':
                  # -- Sektion Start
                  # -- BEHANDL: Sektion Start
                  pass
                elif rec[0:5] == 'BS042' and rec[13:17] == '0236':
                  # -- Gennemført automatisk betaling
                  # -- BEHANDL: Gennemført automatisk betaling
                  self.read042(sektion, '0236', rec)
                elif rec[0:5] == 'BS042' and rec[13:17] == '0237':
                  # -- Afvist betaling
                  # -- BEHANDL: Afvist betaling
                  self.read042(sektion, '0237', rec)
                elif rec[0:5] == 'BS042' and rec[13:17] == '0238':
                  # -- Afmeldt betaling
                  # -- BEHANDL: Afmeldt betaling
                  self.read042(sektion, '0238', rec)
                elif rec[0:5] == 'BS042' and rec[13:17] == '0239':
                  # -- Tilbagef?rt betaling
                  # -- BEHANDL: Tilbagef?rt betaling
                  self.read042(sektion, '0239', rec)
                elif rec[0:5] == 'BS092' and rec[13:17] == '0211':
                  # -- Sektion Slut
                  # -- BEHANDL: Sektion Slut
                  sektion = ''
                else:
                  raise Pbs602Error('244 - Rec# ' + Seqnr + ' ukendt: ' + rec)

              elif sektion == '0215':
                # -- Sektion 0215 FI-Betalingsinformation
                if rec[0:5] == 'BS012' and rec[13:17] == '0215':
                  # -- Sektion Start
                  # -- BEHANDL: Sektion Start
                  pass
                elif rec[0:5] == 'BS042' and rec[13:17] == '0297':
                  # -- Gennemført FI-betaling
                  # -- BEHANDL: Gennemført FI-betaling
                  self.read042(sektion, '0297', rec)
                elif rec[0:5] == 'BS042' and rec[13:17] == '0299':
                  # -- Tilbageført FI-betaling
                  # -- BEHANDL: Tilbagef?rt FI-betaling
                  self.read042(sektion, '0299', rec)
                elif rec[0:5] == 'BS092' and rec[13:17] == '0215':
                  # -- Sektion Slut
                  # -- BEHANDL: Sektion Slut
                  sektion = ''
                else:
                  raise Pbs602Error('245 - Rec# ' + Seqnr + ' ukendt: ' + rec)

              elif rec[0:5] == 'BS992':
                # -- Leverance slut
                # -- BEHANDL: Leverance Slut
                leverancetype = ''
              else:
                raise Pbs602Error('246 - Rec# ' + Seqnr + ' ukendt: ' + rec)

            else:
              raise Pbs602Error('247 - Rec# ' + Seqnr + ' ukendt: ' + rec)
            
          # slut rstpbsfile

          # -- Update indbetalingsbelob in Bet
          for rec_bet in self.m_rec_frapbs.listBet:
            SumIndbetalingsbelob = 0.0
            for rec_betlin in rec_bet.listBetlin:
              SumIndbetalingsbelob += rec_betlin.Indbetalingsbelob
            rec_bet.Indbetalingsbelob = SumIndbetalingsbelob
            rec_bet.put()

          
        except Pbs602Error, e:
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
          else: 
            raise Pbs602Error(msg)

    # slut rstpbsfiles
    return AntalFiler
    
  def read042(self, sektion, transkode, rec):
    fortegn = None
    belobmun = None
    belob = None

    # --  pbssektionnr
    # --  pbstranskode
    # - transkode 0236, gennemført automatisk betaling
    # - transkode 0237, afvist automatisk betaling
    # - transkode 0238, afmeldt automatisk betaling
    # - transkode 0239, tilbageført betaling
    
    # --  debitorkonto
    if sektion == '0211':
      Nr = int(rec[33:40] )
      Debitorkonto = rec[25:40] 
    elif sektion == '0215':
      Nr = int(rec[37:44] )
      Debitorkonto = rec[29:44] 
    else:
      Nr = None
      Debitorkonto = None    
    
    betlinid  = nextval('Betlinid') 
    root_betlin = db.Key.from_path('Persons','root','Person','%s' % (Nr))    
    self.m_rec_betlin = Betlin.get_or_insert('%s' % (betlinid), parent=root_betlin)
    self.m_rec_betlin.Id = betlinid
    self.m_rec_betlin.Pbssektionnr = sektion
    self.m_rec_betlin.Pbstranskode = transkode   
    self.m_rec_betlin.Nr = Nr   
    self.m_rec_betlin.Debitorkonto = Debitorkonto   

    # --  aftalenr
    if sektion == '0211':
      self.m_rec_betlin.Aftalenr = int(rec[40:49] )
    else:
      self.m_rec_betlin.Aftalenr = None

    # --  pbskortart
    if sektion == '0215':
      self.m_rec_betlin.Pbskortart = rec[44:46] 
    else:
      self.m_rec_betlin.Pbskortart = None

    # --  pbsgebyrbelob
    if sektion == '0215':
      fortegn = int(rec[46:46+1] )
      belob = int(rec[47:47+5] )
      belobmun = float(belob / 100)
      if fortegn == 0:
        self.m_rec_betlin.Pbsgebyrbelob = float(0)
      elif fortegn == 1:
        self.m_rec_betlin.Pbsgebyrbelob = belobmun
      else:
        self.m_rec_betlin.Pbsgebyrbelob = float(0)
    else:
      self.m_rec_betlin.Pbsgebyrbelob = float(0)

    # --  betalingsdato
    if sektion == '0211':
      if rec[49:55]  != '000000':
        self.m_rec_betlin.Betalingsdato = datetime.strptime('20' + rec[53:55]  + '-' + rec[51:53]  + '-' + rec[49:51], "%Y-%m-%d").date()
      else:
        self.m_rec_betlin.Betalingsdato = None
    elif sektion == '0215':
      if rec[52:58]  != '000000':
        self.m_rec_betlin.Betalingsdato = datetime.strptime('20' + rec[56:58] + '-' + rec[54:56] + '-' + rec[52:54], "%Y-%m-%d").date()
      else:
        self.m_rec_betlin.Betalingsdato = None
    else:
      self.m_rec_betlin.Betalingsdato = None

    # --  belob
    if sektion == '0211':
      fortegn = int(rec[55:56] )
      belob = int(rec[56:69] )
      belobmun = float(belob / 100)
      if fortegn == 0:
        self.m_rec_betlin.Belob = float(0)
      elif fortegn == 1:
        self.m_rec_betlin.Belob = belobmun
      elif fortegn == 2:
        self.m_rec_betlin.Belob = (belobmun * -1)
      else:
        self.m_rec_betlin.Belob = None
    elif sektion == '0215':
      fortegn = int(rec[58:59] )
      belob = int(rec[59:72] )
      belobmun = float(belob / 100)
      if fortegn == 0:
        self.m_rec_betlin.Belob = float(0)
      elif fortegn == 1:
        self.m_rec_betlin.Belob = belobmun
      else:
        self.m_rec_betlin.Belob = None
    else:
      self.m_rec_betlin.Belob = None

    # --  faknr
    if sektion == '0211':
      self.m_rec_betlin.Faknr = int('0' + rec[69:78].strip())
    elif sektion == '0215':
      self.m_rec_betlin.Faknr = int('0' + rec[72:81].strip())
    else:
      self.m_rec_betlin.Faknr = None

    # --  pbsarkivnr
    if sektion == '0215':
      self.m_rec_betlin.Pbsarkivnr = rec[81:103] 
    else:
      self.m_rec_betlin.Pbsarkivnr = None

    # --  indbetalingsdato
    if rec[103:109]  != '000000':
      self.m_rec_betlin.Indbetalingsdato = datetime.strptime('20' + rec[107:109] + '-' + rec[105:107] + '-' + rec[103:105], "%Y-%m-%d").date()
    else:
      self.m_rec_betlin.Indbetalingsdato = None

    # --  bogforingsdato
    if rec[109:115]  != '000000':
      self.m_rec_betlin.Bogforingsdato = datetime.strptime('20' + rec[113:115] + '-' + rec[111:113]  + '-' + rec[109:111], "%Y-%m-%d").date()
    else:
      self.m_rec_betlin.Bogforingsdato = None

    # --  indbetalingsbelob
    if sektion == '0211':
      fortegn = int(rec[55:56] )
      belob = int(rec[115:128] )
      belobmun = float(belob / 100)
      if fortegn == 0:
        self.m_rec_betlin.Indbetalingsbelob = 0
      elif fortegn == 1:
        self.m_rec_betlin.Indbetalingsbelob = belobmun
      elif fortegn == 2:
        self.m_rec_betlin.Indbetalingsbelob = (belobmun * -1)
      else:
        self.m_rec_betlin.Indbetalingsbelob = None
    elif sektion == '0215':
      fortegn = int(rec[58:59] )
      belob = int(rec[115:128] )
      belobmun = float(belob / 100)
      if fortegn == 0:
        self.m_rec_betlin.Indbetalingsbelob = float(0)
      elif fortegn == 1:
        self.m_rec_betlin.Indbetalingsbelob = belobmun
      else:
        self.m_rec_betlin.Indbetalingsbelob = None
    else:
      self.m_rec_betlin.Indbetalingsbelob = None


    # Find or Create Bet record
    for wrec_bet in self.m_rec_frapbs.listBet:
      if wrec_bet.Pbssektionnr == sektion and wrec_bet.Transkode == transkode and wrec_bet.Bogforingsdato == self.m_rec_betlin.Bogforingsdato:
        self.m_rec_bet = wrec_bet
        break
    else:
      betid  = nextval('Betid') 
      root_bet = db.Key.from_path('rootBet','root')    
      self.m_rec_bet = Bet.get_or_insert('%s' % (betid), parent=root_bet)
      self.m_rec_bet.Id = betid
      self.m_rec_bet.Pbssektionnr = sektion
      self.m_rec_bet.Transkode = transkode
      self.m_rec_bet.Bogforingsdato = self.m_rec_betlin.Bogforingsdato
      
    self.m_rec_bet.Frapbsref = self.m_rec_frapbs.key()
    self.m_rec_bet.put()

    # Add Betlin
    self.m_rec_betlin.Betref = self.m_rec_bet.key()
    self.m_rec_betlin.put()
