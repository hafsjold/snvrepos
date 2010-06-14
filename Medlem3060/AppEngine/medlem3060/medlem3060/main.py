from google.appengine.ext import webapp
from google.appengine.ext.webapp import util

import logging
import rest
from google.appengine.ext import db 

from util import TestCrypt

class Medlem(db.Model): 
    Nr = db.IntegerProperty()
    Navn  = db.StringProperty()
    Kaldenavn  = db.StringProperty()
    Adresse  = db.StringProperty()
    Postnr  = db.StringProperty()
    Bynavn  = db.StringProperty()
    Email = db.EmailProperty()
    Telefon = db.PhoneNumberProperty()
    Kon = db.StringProperty()
    FodtDato  = db.DateProperty()

class Medlemlog(db.Model): 
    Medlem_key = db.ReferenceProperty(Medlem, collection_name="medlemlog_set")
    Source = db.IntegerProperty()
    Source_id = db.IntegerProperty()
    Nr = db.IntegerProperty()
    Logdato = db.DateTimeProperty()
    Akt_id = db.IntegerProperty()  
    Akt_dato = db.DateTimeProperty()

class MainHandler(webapp.RequestHandler):
    def get(self):
        logging.info('%s: %s' % ('hafsjold', 'Dette er en test'))
        TestCrypt('Mogens Hafsjold')
        self.response.out.write('Hello medlem3060!')

application = webapp.WSGIApplication(
                                     [
                                      ('/', MainHandler),
                                      ('/rest/.*', rest.Dispatcher)
                                     ], 
                                     debug=True
                                    )
rest.Dispatcher.base_url = "/rest"
rest.Dispatcher.add_models_from_module(__name__)

def main():
    util.run_wsgi_app(application)

if __name__ == '__main__':
    main()
