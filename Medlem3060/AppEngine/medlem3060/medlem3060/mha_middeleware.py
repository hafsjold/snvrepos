import logging
import pprint
from google.appengine.ext.webapp import template
import os

class LoggingMiddleware: 

  def __init__(self, application): 
    self.__application = application 
 
  """This is the REQUEST side of the middleware"""
  def __call__(self, environ, start_response):
    for key, value in environ.iteritems():
      logging.info('%s: %s' % (key, value))

      
    """This is the RESPONSE side of the middleware"""
    def _start_response(status, headers):
      return start_response(status, headers) 
 
    return self.__application(environ, _start_response) 