#!/usr/bin/env python
#
# Copyright 2007 Google Inc.
#
# Licensed under the Apache License, Version 2.0 (the "License");
# you may not use this file except in compliance with the License.
# You may obtain a copy of the License at
#
#     http://www.apache.org/licenses/LICENSE-2.0
#
# Unless required by applicable law or agreed to in writing, software
# distributed under the License is distributed on an "AS IS" BASIS,
# WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
# See the License for the specific language governing permissions and
# limitations under the License.
#

"""Sample AppStats Configuration.

There are four sections:

0) WSGI middleware declaration.
1) Django version declaration.

"""

import logging
logging.info('Loading %s from %s', __name__, __file__) 

# 0) WSGI middleware declaration.

# Only use this if you're not Django; with Django, it's easier to add
#   'google.appengine.ext.appstats.recording.AppStatsDjangoMiddleware',
# to your Django settings.py file.

def webapp_add_wsgi_middleware(app):
  
  try: 
    import mha_middeleware  
  except ImportError, err: 
    logging.info('Failed to import mha_middeleware: %s', err) 
  else: 
    app = mha_middeleware.LoggingMiddleware (app)

  return app 


# 1) Django version declaration.

# If your application uses Django and requires a specific version of
# Django, uncomment the following block of three lines.  Currently
# supported values for the Django version are '0.96' (the default),
# '1.0', and '1.1'.

# # from google.appengine.dist import use_library
# # use_library('django', '1.0')
# # import django
