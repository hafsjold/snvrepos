from google.appengine.ext import webapp
from google.appengine.ext.webapp import template

import logging
import os

def lpad (oVal, Length, PadChar):
  Val = '%s' % (oVal)
  return Val.rjust(Length, PadChar)

def rpad (oVal, Length, PadChar):
  Val = '%s' % (oVal)
  return Val.ljust(Length, PadChar)


class TestHandler(webapp.RequestHandler):
  def get(self):
    mha = self.write092('12345678', '0117', 23, 24, 12345, 2345, 345)
    logging.info('QQQQQQQQQQQQQQQQQQQQQQQQQQ write092: %s' % (mha))
    template_values = {}
    path = os.path.join(os.path.dirname(__file__), 'templates/test.html') 
    self.response.out.write(template.render(path, template_values))

  def write092(self, pbsnr, sektionnr, debgrpnr, antal1, belob1, antal2, antal3):
    rec = "BS092"
    rec += lpad(pbsnr, 8, '?')
    rec += lpad(sektionnr, 4, '?')
    if sektionnr == "0112":
      rec += rpad("", 5, '0')
      rec += lpad(debgrpnr, 5, '0')
      rec += rpad("", 4, ' ')
    else:
      if sektionnr == "0117":
        rec += rpad("", 5, '0')
        rec += lpad(debgrpnr, 5, '0')
        rec += rpad("", 4, ' ')
      else:
        rec += rpad("", 3, '0')
        rec += lpad(debgrpnr, 5, '0')
        rec += rpad("", 6, ' ')

    rec += lpad(antal1, 11, '0')
    rec += lpad(belob1, 15, '0')
    rec += lpad(antal2, 11, '0')
    rec += rpad("", 15, ' ')
    rec += lpad(antal3, 11, '0')
    return rec
     
  def write992(self, datalevnr, delsystem, levtype, antal1, antal2, belob2, antal3, antal4):
    rec = "BS992"
    rec += lpad(datalevnr, 8, '?')
    rec += lpad(delsystem, 3, '?')
    rec += lpad(levtype, 4, '?')
    rec += lpad(antal1, 11, '0')
    rec += lpad(antal2, 11, '0')
    rec += lpad(belob2, 15, '0')
    rec += lpad(antal3, 11, '0')
    rec += lpad("", 15, '0')
    rec += lpad(antal4, 11, '0')
    rec += lpad("", 34, '0')
    return rec
    

  