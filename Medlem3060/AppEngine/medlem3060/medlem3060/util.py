import logging

from tlslite.utils.RSAKey import RSAKey 
import tlslite.utils.keyfactory 
import tlslite.utils.compat 
import tlslite.utils.cryptomath
from tlslite import X509
import sha
import time

mysecret = """2X4JYVdklF2YdsDxbWt8ygUaueUmgETSpwgOJ5wV0DpTrTSyEGFI4VobVi3fHQxfbh24aPdvbwPTRkZiRob9PEnbOAAlqnJ2iziBG0744kusyr6ykVUSkZLowBYrNsf2P1hojKOPLAIKSdKuPTRx5Ulrk91EnTd24JS2QLRFOD2sAcI9zEZ6ICsv5cWRzIeV4u0TlWmULmQGxN6s3mZA8d1KyxvwovqJwGN9aeVaMQQ5LHimm8gOXBIhmpmGKp9m"""

mycert = """-----BEGIN CERTIFICATE-----
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
-----END CERTIFICATE-----"""

mykey = """-----BEGIN RSA PRIVATE KEY-----
MIIEowIBAAKCAQEA0G2MsoKo/jc43syrGS/AKV81hrL/BBOHWALjG5efOKXSY8ZFrh573vnTX1MV/RPv8VUKpfUXOQ0NAB8t+KWJ7pLvNcYWol3E6U4XGexVlkr5P/hPKUC9ia7ZcMtEkxjR7JLQOEVyZ2dBhLsX70xUOsP59y8eJbG+HeL58NsJnbaDQkDEbyoFjxPyKbeBmI30lbo1dpRPe2y9iAu4tzC8Yf+D7hnPqpYhBqD7yacMlvpO3YLtUN86aNjRAYOwlK5hU2DmJA3yVku8fbAsHOL/PbKcNK8CkCR4zhxtGJhcOJc7BSgPTPmeUuzfhyff2kc7MhD0fDS9wL2huHx/4ASNWwIDAQABAoIBABDbCynUjz4f0SWTf7LFvdCatoVyLFV0Dtn7QcqVdHbsUhtniXMPXA0oPwPSgFC7MAhgTEAnlf0zJP4Bh4I4QPNeRqIepu3yj14expd+GV3SKl4WArDfX3SnA0av6ZfLxg5PwS8Lzri2DQJi7wiXL6ig+LIYyWNbAHkCRhxIWnq6hnSOBPcqeqAoO+CVznHU8QYuR1z8JVq/PKok9cAyLSuZS37h1mSMSXkZSA/IdGjqPJAwLLcTCHjCUDuDu3ailDb4Ah1bYJLUNgJdgECkYSR8wLztEI8stiED1o0HKM3q1/PQPLjJMsD/DmgDY08fec6F+y7099AMB7akMeKY2oECgYEA8bfRt/GZqM6fmMwbNtU596NdIzlSDmA7kR8tJzGxhiriIWgmN5m2hkTGN9NJtjP0r12vG8Ez01EbUlKfa3OqKTF9zu/PIxuU46H2CK+IP+ghHU9OHG15wrI+l+fTkJ6gq7qcrU8I2/B0jtKdmKUdl3SpO8IrSG/h6U4TpJQnQesCgYEA3L4wwzUjToUx4k9sz1IjWuNTuQNR+/3LxJcpJtCofgC0Ut1pAKtPX8MS7s2Sp0ZMpurUKVBPQ77sZXpXRyPQ4bGfamkctjLys3HffoSCRMx+HaGrNk4TwNhXkxJaP9xAFNzLHYwMaQRKXFe2v6XcgUCac+ZGL1NgqKMGGiWrllECgYEAtPJKSEzQHpIu3w9MAAw2zK66djfeuWxIqyaPgpusrSdFCIUStuSWwoSRbhD5STAzp2OWRkynIzXAIiw/swxvAU9PQq46famUF6OSroXYlR6MS4imjJlXYOxV9xlQQx68YFHeH87ebubeGlyIJVDVih+G4HlGNX+ruh78jWNqz+kCgYA/Qwp6h1oNAMMhFp4adHHJdGjkFv2B+GRTfPbANwByzATh0q5rEK14xlFAuw2SfuUs2RPgmzF8OtVI59zneG4+oEcNmf4ugT9pCfOBMLyctvZVy6VjtNCYbef7MEFJF/gNgpF7cE2GM0KUYFbxableGYOqP45RtdV3vvDawX0BYQKBgAWKU6oIYwIwbkmO+sg/J7jdBOi2zw6MEkjmvL5V79iLitCymthuRvbUdvWGctGWylRcl7DguOCENBcJkwijnkOoQ2MBuYtSARVTtSwM5nvhybaJmDbk91cVr5mRr35LXXpnqPDbATFNc9v4kCjZdiD80KI/LwJhg89eskOnM8aY
-----END RSA PRIVATE KEY-----"""

def AuthRest(http_timestamp, http_signed):
  #logging.info('%s: %s' % ('http_timestamp', http_timestamp))
  #logging.info('%s: %s' % ('http_signed', http_signed))
  dif = abs(float(http_timestamp) - time.time())
  logging.info('%s: %s' % ('dif', dif))
  if dif > 120:
    return False
  
  mycertobj = X509.X509()
  mycertobj.parse(mycert)
  mypublickey = mycertobj.publicKey
  
  verify = mypublickey.hashAndVerify (tlslite.utils.cryptomath.base64ToBytes(http_signed), tlslite.utils.compat.stringToBytes('%s%s' % (http_timestamp, mysecret)))
  if verify:
    logging.info('%s: %s' % ('verify', 'TRUE'))
    return True
  else:
    logging.info('%s: %s' % ('verify', 'FALSE'))
    return False

def TestCrypt(data):
  mycertobj = X509.X509()
  mycertobj.parse(mycert)
  Fingerprint = mycertobj.getFingerprint()
  
  mypublickey = mycertobj.publicKey
  myprivatekey = tlslite.utils.keyfactory.parsePEMKey(mykey, private=True)
  mypublicXMLkeyString = myprivatekey.writeXMLPublicKey()
  
  data2 = '2010-02-02 21:15:00' 
  logging.info('%s: %s' % ('data', data))
  logging.info('%s: %s' % ('time.time', time.time()))


  
  myhashAndSign = myprivatekey.hashAndSign (tlslite.utils.compat.stringToBytes('%s%s' % (data2, mysecret)))
  logging.info('%s: %s' % ('myhashAndSign', tlslite.utils.cryptomath.bytesToBase64(myhashAndSign)))
  
  verify = mypublickey.hashAndVerify (myhashAndSign, '%s%s' % (data2, mysecret))
  if verify:
    logging.info('%s: %s' % ('verify', 'TRUE'))
  else:
    logging.info('%s: %s' % ('verify', 'FALSE'))

  mycrypt = mypublickey.encrypt(tlslite.utils.compat.stringToBytes(data))
  
  mydecrypt = myprivatekey.decrypt(mycrypt)
  logging.info('%s: %s' % ('mydecrypt', mydecrypt.tostring()))
  