# coding=utf-8 
from google.appengine.ext import webapp
from google.appengine.ext import db 
from google.appengine.ext.webapp import template

from models import nextval, UserGroup, User, NrSerie, Kreditor, Kontingent, Pbsforsendelse:, Pbsfiles, Pbsfile, Sendqueue, Recievequeue, Tilpbs, Fak, Frapbs, Bet, Betlin, Sftp, Infotekst, Sysinfo, Menu, MenuMenuLink, Medlog, Person
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

    self.m_rec_pbsforsendelse: = None
    self.m_rec_pbsfiles = None
    self.m_rec_frapbs = None
    self.m_rec_aftalelin = None
    self.m_rec_indbetalingskort = None

  def get(self):
    antal = self.aftaleoplysninger_fra_pbs()
    self.response.out.write('Antal filer %s' % (antal))
    
  def aftaleoplysninger_fra_pbs():
    dummy = 0
    rec = None
    leverance = None
    leverancespecifikation = None
    leverancedannelsesdato = None
    sektion = None
    wpbsfilesid = None
    wleveranceid = None
    AntalFiler = 0


    var qrypbsfiles = from h in Program.dbData3060.Tblpbsfiles
              where h.Pbsforsendelse:id == None
              join d in Program.dbData3060.Tblpbsfile on h.Id equals d.Pbsfilesid
              where d.Seqnr == 1 and d.Data[16:20] == '0603' and d.Data[0:0+2] == 'BS'
              select new
              {
                h.Id,
                h.Path,
                h.Filename,
                leverancetype = d.Data[16:20],
                delsystem = d.Data[13:16],
              }

    int DebugCount = qrypbsfiles.Count()
    foreach (var rstpbsfiles in qrypbsfiles)
    {
      #try
      {
        wpbsfilesid = rstpbsfiles.Id
        AntalFiler += 1
        leverance = ''
        sektion = ''
        leverancespecifikation = ''


        var qrypbsfile = from d in Program.dbData3060.Tblpbsfile
                 where d.Pbsfilesid == wpbsfilesid and d.Data[:6] != 'PBCNET'
                 orderby d.Seqnr
                 select d

        foreach (var rstpbsfile in qrypbsfile)
        {
          rec = rstpbsfile.Data
          #  Bestem Leverance Type
          if (rstpbsfile.Seqnr == 1)
          {
            if (rec[:5] == 'BS002')
            {  #  Leverance Start
              leverance = rec[16:20]
              leverancespecifikation = rec[20:30]
              leverancedannelse:sdato = datetime.strptime('20' + rec[53:55] + '-' + rec[51:53] + '-' + rec[49:51], "%Y-%m-%d").date()

            }
            else:
            {
              throw new Exception('241 - Første record er ikke en Leverance start record')
            }

            if (leverance == '0603')
            {
              # -- Leverance 0603
              var antal = (from c in Program.dbData3060.Tblfrapbs
                       where c.Leverancespecifikation == leverancespecifikation
                       select c).Count()
              if (antal > 0) { throw new Exception('242 - Leverance med pbsfilesid: ' + wpbsfilesid + ' og leverancespecifikation: ' + leverancespecifikation + ' er indlæst tidligere') }

              wleveranceid = clsPbs.nextval('leveranceid')
              self.m_rec_pbsforsendelse: = new Tblpbsforsendelse:
              {
                Delsystem = 'BS1',
                Leverancetype = '0603',
                Oprettetaf = 'Bet',
                Oprettet = DateTime.Now,
                Leveranceid = wleveranceid
              }
              Program.dbData3060.Tblpbsforsendelse:.InsertOnSubmit(self.m_rec_pbsforsendelse:)

              self.m_rec_frapbs = new Tblfrapbs
              {
                Delsystem = 'BS1',
                Leverancetype = '0603',
                Leverancespecifikation = leverancespecifikation,
                Leverancedannelse:sdato = leverancedannelse:sdato,
                Udtrukket = DateTime.Now
              }
              self.m_rec_pbsforsendelse:.Tblfrapbs.Add(self.m_rec_frapbs)

              self.m_rec_pbsfiles = (from c in Program.dbData3060.Tblpbsfiles
                            where c.Id == rstpbsfiles.Id
                            select c).First()
              self.m_rec_pbsforsendelse:.Tblpbsfiles.Add(self.m_rec_pbsfiles)
            }
          }

          if (leverance == '0603')
          { #  Leverance 0603

            #  Bestem Sektions Type
            if (sektion == '')
            {
              if (rec[:5] == 'BS012')
              {  #  Sektion Start
                sektion = rec[13:17]
              }
              else:
              {
                if (!((rec[:5] == 'BS992') or (rec[:5] == 'BS002')))
                {
                  throw new Exception('243 - Første record er ikke en Sektions start record')
                }
              }
            }

            if (rec[:5] == 'BS002')
            {  #  Leverance start
              #  BEHANDL: Leverance start
              dummy = 1
              # -******************************************************************************************************
              # -******************************************************************************************************
            }
            elif (sektion == '0210')
            {  #  Sektion 0210 Aktive aftaler
              if ((rec[:5] == 'BS012') and (rec[13:17] == '0210'))
              {  #  Sektion Start
                #  BEHANDL: Sektion Start
                dummy = 1

              }
              elif ((rec[:5] == 'BS042') and (rec[13:17] == '0230'))
              {  #  Aktive aftaler
                #  BEHANDL: Aktive aftaler
                self.readaftale042(sektion, '0230', rec)

              }
              elif ((rec[:5] == 'BS092') and (rec[13:17] == '0210'))
              {  #  Sektion Slut
                #  BEHANDL: Sektion Slut
                sektion = ''
              }
              else:
              {
                throw new Exception('244 - Rec# ' + rstpbsfile.Seqnr + ' ukendt: ' + rec)
              }
              # -******************************************************************************************************
              # -******************************************************************************************************
            }
            elif (sektion == '0212')
            {  #  Sektion 0212 Til- og afgang af betalingsaftaler
              if ((rec[:5] == 'BS012') and (rec[13:17] == '0212'))
              {  #  Sektion Start
                #  BEHANDL: Sektion Start
                dummy = 1

              }
              elif ((rec[:5] == 'BS042') and (rec[13:17] == '0231'))
              {  #  Tilgang af nye betalingsaftaler
                #  BEHANDL: Tilgang af nye betalingsaftaler
                self.readaftale042( sektion, '0231', rec)

              }
              elif ((rec[:5] == 'BS042') and (rec[13:17] == '0232'))
              {  #  Aftale afmeldt af pengeinstitut
                #  BEHANDL: aftale afmeldt af pengeinstitut
                self.readaftale042(sektion, '0232', rec)

              }
              elif ((rec[:5] == 'BS042') and (rec[13:17] == '0233'))
              {  #  Aftaler afmeldt af kreditor
                #  BEHANDL: aftaler afmeldt af kreditor
                self.readaftale042(sektion, '0233', rec)

              }
              elif ((rec[:5] == 'BS042') and (rec[13:17] == '0234'))
              {  #  Aftaler afmeldt af betalingsservice
                #  BEHANDL: aftaler afmeldt af betalingsservice
                self.readaftale042(sektion, '0234', rec)

              }
              elif ((rec[:5] == 'BS092') and (rec[13:17] == '0212'))
              {  #  Sektion Slut
                #  BEHANDL: Sektion Slut
                sektion = ''
              }
              else:
              {
                throw new Exception('245 - Rec# ' + rstpbsfile.Seqnr + ' ukendt: ' + rec)
              }
              # -******************************************************************************************************
              # -******************************************************************************************************
            }
            elif (sektion == '0214')
            {  #  Sektion 0214 Forfaldsoplysninger
              if ((rec[:5] == 'BS012') and (rec[13:17] == '0214'))
              {  #  Sektion Start
                #  BEHANDL: Sektion Start
                dummy = 1

              }
              elif ((rec[:5] == 'BS042') and (rec[13:17] == '0235'))
              {  #  Forfald automatisk betaling
                #  BEHANDL: Forfald automatisk betaling
                dummy = 1

              }
              elif ((rec[:5] == 'BS042') and (rec[13:17] == '0295'))
              {  #  Forfald manuel betaling
                #  BEHANDL: Forfald manuel betaling
                dummy = 1

              }
              elif ((rec[:5] == 'BS092') and (rec[13:17] == '0214'))
              {  #  Sektion Slut
                #  BEHANDL: Sektion Slut
                sektion = ''
              }
              else:
              {
                throw new Exception('246 - Rec# ' + rstpbsfile.Seqnr + ' ukendt: ' + rec)
              }
              # -******************************************************************************************************
              # -******************************************************************************************************
            }
            elif (sektion == '0215')
            {  #  Sektion 0215 Debitornavn/-adresse
              if ((rec[:5] == 'BS012') and (rec[13:17) == '0215'))
              {  #  Sektion Start
                #  BEHANDL: Sektion Start
                dummy = 1

              }
              elif ((rec[:5] == 'BS022') and (rec[13:17] == '0240'))
              {  #  Navn/adresse på debitor
                #  BEHANDL: Navn/adresse på debitor
                dummy = 1

              }
              elif ((rec[:5] == 'BS092') and (rec[13:17) == '0215'))
              {  #  Sektion Slut
                #  BEHANDL: Sektion Slut
                sektion = ''
              }
              else:
              {
                throw new Exception('247 - Rec# ' + rstpbsfile.Seqnr + ' ukendt: ' + rec)
              }
              # -******************************************************************************************************
              # -******************************************************************************************************
            }
            elif (sektion == '0217')
            {  #  Sektion 0217 Oplysninger fra indbetalingskort
              if ((rec[:5] == 'BS012') and (rec[13:17] == '0217'))
              {  #  Sektion Start
                #  BEHANDL: Sektion Start
                dummy = 1

              }
              elif ((rec[:5] == 'BS042') and (rec[13:17] == '0295'))
              {  #  Oplysninger fra indbetalingskort
                #  BEHANDL: Oplysninger fra indbetalingskort
                self.readgirokort042(sektion, '0295', rec)

              }
              elif ((rec[:5] == 'BS092') and (rec[13:17] == '0217'))
              {  #  Sektion Slut
                #  BEHANDL: Sektion Slut
                sektion = ''
              }
              else:
              {
                throw new Exception('248 - Rec# ' + rstpbsfile.Seqnr + ' ukendt: ' + rec)
              }
              # -******************************************************************************************************
              # -******************************************************************************************************
            }
            elif (sektion == '0219')
            {  #  Sektion 0219 Aktive aftaler om Elektronisk Indbetalingskort
              if ((rec[:5] == 'BS012') and (rec[13:17] == '0219'))
              {  #  Sektion Start
                #  BEHANDL: Sektion Start
                dummy = 1

              }
              elif ((rec[:5] == 'BS042') and (rec[13:17] == '0230'))
              {  #  Aktiv aftale om Elektronisk Indbetalingskort
                #  BEHANDL: Aktiv aftale om Elektronisk Indbetalingskort
                dummy = 1

              }
              elif ((rec[:5] == 'BS092') and (rec[(13:17) == '0219'))
              {  #  Sektion Slut
                #  BEHANDL: Sektion Slut
                sektion = ''
              }
              else:
              {
                throw new Exception('249 - Rec# ' + rstpbsfile.Seqnr + ' ukendt: ' + rec)
              }
              # -******************************************************************************************************
              # -******************************************************************************************************
            }
            elif (rec[:5] == 'BS992')
            {  #  Leverance slut
              #  BEHANDL: Leverance Slut
              leverance = ''
            }
            else:
            {
              throw new Exception('250 - Rec# ' + rstpbsfile.Seqnr + ' ukendt: ' + rec)
            }
          }
         
          else:
          {
            throw new Exception('251 - Rec# ' + rstpbsfile.Seqnr + ' ukendt: ' + rec)
          }

        }

      }
      /*  
      catch (Exception e)
      {
        switch (e.Message[:3])
        {
          case '241':   #241 - Første record er ikke en Leverance start record
          case '242':   #242 - Leverancen er indlæst tidligere
          case '243':   #243 - Første record er ikke en Sektions start record
          case '244':   #244 - Record ukendt
          case '245':   #245 - Record ukendt
          case '246':   #246 - Record ukendt
          case '247':   #247 - Record ukendt
          case '248':   #248 - Record ukendt
          case '249':   #249 - Record ukendt
          case '250':   #250 - Record ukendt
          case '251':   #251 - Record ukendt
            AntalFiler--
            break

          default:
            throw
        }
      }
      */
    }
    if (dummy == 1) dummy = 2
    Program.dbData3060.SubmitChanges()
    return AntalFiler


  def readaftale042(self, sektion, transkode, rec)
    # --  pbssektionnr
    # --  pbstranskode
    # - transkode 0230, aktiv aftale
    # - transkode 0231, tilgang af ny betalingsaftale
    # - transkode 0232, aftale afmeldt af pengeinstitut
    # - transkode 0233, aftale afmeldt af kreditor
    # - transkode 0234, aftale afmeldt af betalingsservice
    self.m_rec_aftalelin = new Tblaftalelin
    {
      Pbssektionnr = sektion,
      Pbstranskode = transkode
    }

    #  Medlem Nr
    try
    {
      self.m_rec_aftalelin.Nr = int(rec[33:40])
    }
    catch
    {
      self.m_rec_aftalelin.Nr = 0
    }
    #  debitorkonto
    self.m_rec_aftalelin.Debitorkonto = rec[25:40]

    #  debgrpnr
    self.m_rec_aftalelin.Debgrpnr = rec[20:25]

    #  aftalenr
    self.m_rec_aftalelin.Aftalenr = int(rec[40:49])

    #  aftalestartdato
    if (rec[49:55] != '000000')
    {
      self.m_rec_aftalelin.Aftalestartdato = datetime.strptime('20' + rec[53:55] + '-' + rec[51:53] + '-' + rec[49:51], "%Y-%m-%d").date()
    }
    else:
    {
      self.m_rec_aftalelin.Aftalestartdato = None
    }

    #  aftaleslutdato
    if (rec[55:61] != '000000')
    {
      self.m_rec_aftalelin.Aftaleslutdato = datetime.strptime('20' + rec[59:61] + '-' + rec[57:59] + '-' + rec[55:57], "%Y-%m-%d").date()
    }
    else:
    {
      self.m_rec_aftalelin.Aftaleslutdato = None
    }

    if ((from h in Program.dbData3060.TblMedlem where h.Nr == self.m_rec_aftalelin.Nr select h).Count() == 1)
    {
      # Add tblaftalelin
      self.m_rec_frapbs.Tblaftalelin.Add(self.m_rec_aftalelin)
    }


  def readgirokort042(self, sektion, transkode, rec)
    # --  pbssektionnr
    # --  pbstranskode

    decimal belobmun
    int belob

    self.m_rec_indbetalingskort = new Tblindbetalingskort
    {
      Pbssektionnr = sektion,
      Pbstranskode = transkode
    }

    #  Medlem Nr
    try
    {
      self.m_rec_indbetalingskort.Nr = int(rec[33:40])
    }
    catch
    {
      self.m_rec_indbetalingskort.Nr = 0
    }
    
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
    if (rec[55:61] != '000000')
    {
      self.m_rec_indbetalingskort.Dato = datetime.strptime('20' + rec[73:75] + '-' + rec[71:73] + '-' + rec[69:71], "%Y-%m-%d").date()
    }
    else:
    {
      self.m_rec_indbetalingskort.Dato = None
    }

    #  Beløb
    belob = int(rec[75:88])
    belobmun = ((decimal)belob) / 100
    self.m_rec_indbetalingskort.Belob = belobmun

    #  Faknr
    self.m_rec_indbetalingskort.Faknr = int(rec[88:97])

    if ((from h in Program.dbData3060.TblMedlem where h.Nr == self.m_rec_indbetalingskort.Nr select h).Count() == 1)
    {
      # Add tblindbetalingskort
      self.m_rec_frapbs.Tblindbetalingskort.Add(self.m_rec_indbetalingskort)
    }