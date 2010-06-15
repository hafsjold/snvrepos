import logging
from google.appengine.ext.webapp import template
import os
from util import AuthRest

class LoggingMiddleware: 

  def __init__(self, application): 
    self.__application = application 
 
  """This is the REQUEST side of the middleware"""
  def __call__(self, environ, start_response):
    #for key, value in environ.iteritems():
    #  logging.info('%s: %s' % (key, value))
    if str.find(str.lower(environ['PATH_INFO']),'/rest/', 0, 6) == 0:
      try:
        http_timestamp = environ['HTTP_TIMESTAMP']
        http_signed = environ['HTTP_SIGNED']
      except:
        print 'Status: 404'
        return
      
      if not AuthRest(http_timestamp, http_signed):
        print 'Status: 400'
        return
    
    
    """This is the RESPONSE side of the middleware"""
    def _start_response(status, headers):
      return start_response(status, headers) 
 
    return self.__application(environ, _start_response) 