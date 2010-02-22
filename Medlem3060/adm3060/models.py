#!/usr/bin/env python

"""Datastore models."""

from google.appengine.ext import db

class User(db.Model): 
  account = db.StringProperty()
  password = db.StringProperty()
  email = db.EmailProperty()
  
  
class Person(db.Model): 
  owner = db.UserProperty()
  name = db.StringProperty()
  
class Medlem(db.Model):
  Nr = db.IntegerProperty()
  Navn  = db.StringProperty()
  Kaldenavn  = db.StringProperty()
  Adresse  = db.StringProperty()
  Postnr  = db.StringProperty()
  Bynavn  = db.StringProperty()
  Email = db.EmailProperty()
  Telefon = db.PhoneNumberProperty()
  Knr = db.IntegerProperty()
  Kon = db.StringProperty()
  FodtDato  = db.DateProperty()
