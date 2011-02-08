# coding=utf-8 
from google.appengine.ext import webapp
from google.appengine.ext import db 
from google.appengine.ext.webapp import template

from models import nextval, UserGroup, User, NrSerie, Kreditor, Kontingent, Pbsforsendelse, Pbsfiles, Pbsfile, Sendqueue, Recievequeue, Tilpbs, Fak, Sftp, Infotekst, Sysinfo, Menu, MenuMenuLink, Medlog, Person
from util import lpad, rpad, utc, cet
from datetime import datetime, date, timedelta
import logging
import os
import re
from xml.dom import minidom

class pbs602Handler(webapp.RequestHandler):
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
    crlf = "\n"
    #  wpbsfilesid = 3450  #'--test test
    #  leverancetype = "0602"
    #  sektion = "0211"
    #  rec = "BS0420398564402360000000100000000001231312345678910120310000000012755000000125                         3112031112030000000012755"

    qry = db.Query(Recievequeue).filter('Recieved_from_pbs =',False).filter('Onhold =',False)
    for q in qry:
      data = q.Pbsfileref.Data
      datalines = data.split(crlf)
      #Strip off PBCNET records
      if datalines[0][:6] == 'PBCNET':
        del datalines[0]
        if datalines[-1][:6] == 'PBCNET':
          del datalines[-1]
      #Test for BS 0602 record
      if datalines[0][16:20] == "0602" and datalines[0][:2] == "BS":
        rstpbsfiles = {
          'Id': q.Pbsfileref.Pbsfilesref.Id,
          'Path': q.Pbsfileref.Pbsfilesref.Path,
          'Filename': q.Pbsfileref.Pbsfilesref.Filename,
          'leverancetype': datalines[0][16:20],
          'delsystem': datalines[0][:2],}
""" 
        try:
        {
          wpbsfilesid = rstpbsfiles.Id
          AntalFiler += 1
          leverancetype = ""
          sektion = ""
          leverancespecifikation = ""
          Seqnr = 0
          for rec in datalines:
            Seqnr += 1
            # -- Bestem Leverance Type
            if (Seqnr == 1)
            {
              if ((rec.Substring(0, 5) == "BS002"))
              {
                # -- Leverance Start
                leverancetype = rec.Substring(16, 4)
                leverancespecifikation = rec.Substring(20, 10)
                leverancedannelsesdato = DateTime.Parse("20" + rec.Substring(53, 2) + "-" + rec.Substring(51, 2) + "-" + rec.Substring(49, 2))
              }
              else
              {
                throw new Exception("241 - Første record er ikke en Leverance start record")
              }
              if ((leverancetype == "0602"))
              {
                # -- Leverance 0602
                var antal = (from c in Program.dbData3060.Tblfrapbs
                           where c.Leverancespecifikation == leverancespecifikation
                           select c).Count()
                if (antal > 0) { throw new Exception("242 - Leverance med pbsfilesid: " + wpbsfilesid + " og leverancespecifikation: " + leverancespecifikation + " er indlæst tidligere") }
q
                wleveranceid = clsPbs.nextval("leveranceid")
                m_rec_pbsforsendelse = new Tblpbsforsendelse
                {
                  Delsystem = "BS1",
                  Leverancetype = "0602",
                  Oprettetaf = "Bet",
                  Oprettet = DateTime.Now,
                  Leveranceid = wleveranceid
                }
                Program.dbData3060.Tblpbsforsendelse.InsertOnSubmit(m_rec_pbsforsendelse)

                m_rec_frapbs = new Tblfrapbs
                {
                  Delsystem = "BS1",
                  Leverancetype = "0602",
                  Leverancespecifikation = leverancespecifikation,
                  Leverancedannelsesdato = leverancedannelsesdato,
                  Udtrukket = DateTime.Now
                }
                m_rec_pbsforsendelse.Tblfrapbs.Add(m_rec_frapbs)

                m_rec_pbsfiles = (from c in Program.dbData3060.Tblpbsfiles
                                where c.Id == rstpbsfiles.Id
                                select c).First()
                m_rec_pbsforsendelse.Tblpbsfiles.Add(m_rec_pbsfiles)
              }
            }
            if ((leverancetype == "0602"))
            {
              # -- Leverance 0602*********
              # -- Bestem Sektions Type
              if ((sektion == ""))
              {
                if ((rec.Substring(0, 5) == "BS012"))
                {
                  # -- Sektion Start
                  sektion = rec.Substring(13, 4)
                }
                else if (!((rec.Substring(0, 5) == "BS992") || (rec.Substring(0, 5) == "BS002")))
                {
                  throw new Exception("243 - Første record er ikke en Sektions start record")
                }
              }
              if ((rec.Substring(0, 5) == "BS002"))
              {
                # -- Leverance start
                # -- BEHANDL: Leverance start
              }
              else if ((sektion == "0211"))
              {
                # -- Sektion 0211 Betalingsinformation
                if (((rec.Substring(0, 5) == "BS012")
                          && (rec.Substring(13, 4) == "0211")))
                {
                  # -- Sektion Start
                  # -- BEHANDL: Sektion Start
                }
                else if (((rec.Substring(0, 5) == "BS042")
                          && (rec.Substring(13, 4) == "0236")))
                {
                  # -- Gennemf?rt automatisk betaling
                  # -- BEHANDL: Gennemf?rt automatisk betaling
                  read042(sektion, "0236", rec)
                }
                else if (((rec.Substring(0, 5) == "BS042")
                          && (rec.Substring(13, 4) == "0237")))
                {
                  # -- Afvist betaling
                  # -- BEHANDL: Afvist betaling
                  read042(sektion, "0237", rec)
                }
                else if (((rec.Substring(0, 5) == "BS042")
                          && (rec.Substring(13, 4) == "0238")))
                {
                  # -- Afmeldt betaling
                  # -- BEHANDL: Afmeldt betaling
                  read042(sektion, "0238", rec)
                }
                else if (((rec.Substring(0, 5) == "BS042")
                          && (rec.Substring(13, 4) == "0239")))
                {
                  # -- Tilbagef?rt betaling
                  # -- BEHANDL: Tilbagef?rt betaling
                  read042(sektion, "0239", rec)
                }
                else if (((rec.Substring(0, 5) == "BS092")
                          && (rec.Substring(13, 4) == "0211")))
                {
                  # -- Sektion Slut
                  # -- BEHANDL: Sektion Slut
                  sektion = ""
                }
                else
                {
                  throw new Exception("244 - Rec# " + Seqnr + " ukendt: " + rec)
                }
              }
              else if ((sektion == "0215"))
              {
                # -- Sektion 0215 FI-Betalingsinformation
                if (((rec.Substring(0, 5) == "BS012")
                          && (rec.Substring(13, 4) == "0215")))
                {
                  # -- Sektion Start
                  # -- BEHANDL: Sektion Start
                }
                else if (((rec.Substring(0, 5) == "BS042")
                          && (rec.Substring(13, 4) == "0297")))
                {
                  # -- Gennemf?rt FI-betaling
                  # -- BEHANDL: Gennemf?rt FI-betaling
                  read042(sektion, "0297", rec)
                }
                else if (((rec.Substring(0, 5) == "BS042")
                          && (rec.Substring(13, 4) == "0299")))
                {
                  # -- Tilbagef?rt FI-betaling
                  # -- BEHANDL: Tilbagef?rt FI-betaling
                  read042(sektion, "0299", rec)
                }
                else if (((rec.Substring(0, 5) == "BS092")
                          && (rec.Substring(13, 4) == "0215")))
                {
                  # -- Sektion Slut
                  # -- BEHANDL: Sektion Slut
                  sektion = ""
                }
                else
                {
                  throw new Exception("245 - Rec# " + Seqnr + " ukendt: " + rec)
                }
              }
              else if ((rec.Substring(0, 5) == "BS992"))
              {
                # -- Leverance slut
                # -- BEHANDL: Leverance Slut
                leverancetype = ""
              }
              else
              {
                throw new Exception("246 - Rec# " + Seqnr + " ukendt: " + rec)
              }
            }
            else
            {
              throw new Exception("247 - Rec# " + Seqnr + " ukendt: " + rec)
            }
          }  # slut rstpbsfile

          # -- Update indbetalingsbelob
          foreach (Tblbet rec_bet in m_rec_frapbs.Tblbet)
          {
            var SumIndbetalingsbelob = (
              from c in rec_bet.Tblbetlin
              group c by c.Betid into g
              select new { Betid = g.Key, SumIndbetalingsbelob = g.Sum(c => c.Indbetalingsbelob) }
              ).First().SumIndbetalingsbelob

            rec_bet.Indbetalingsbelob = SumIndbetalingsbelob
          }

        }
        catch (Exception e)
        {
          switch (e.Message.Substring(0, 3))
          {
            case "241":   #241 - Første record er ikke en Leverance start record
            case "242":   #242 - Leverancen er indlæst tidligere
            case "243":   #243 - Første record er ikke en Sektions start record
            case "244":   #244 - Record ukendt
            case "245":   #245 - Record ukendt
            case "246":   #246 - Record ukendt
            case "247":   #247 - Record ukendt
              AntalFiler--
              break

            default:
              throw
          }
"""  
#        }
#    }  # slut rstpbsfiles
#    Program.dbData3060.SubmitChanges()
#    return AntalFiler