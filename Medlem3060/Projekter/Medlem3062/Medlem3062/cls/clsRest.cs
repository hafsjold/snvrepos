using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Net;
using System.IO;

namespace nsPuls3060
{
    public class clsRest
    {
#if (DEBUG)        
        private string m_baseurl = @"http://localhost:8084/sync/";
        //private string m_baseurl = @"http://testmedlem3060.appspot.com/sync/";
#else
        private string m_baseurl = @"http://medlem3060.appspot.com/sync/";
#endif
        public string HttpGet2(string url)
        {
            string fullurl = m_baseurl + url;
            HttpWebRequest Request = (HttpWebRequest)WebRequest.Create(fullurl);
            SignRequest(ref Request);

            try
            {
                HttpWebResponse Response = (HttpWebResponse)Request.GetResponse();

                StreamReader Reader = new StreamReader(Response.GetResponseStream());
                string XMLResponse = Reader.ReadToEnd();
                Reader.Close();
                Response.Close();

                return XMLResponse;
            }

            catch (WebException e)
            {
                return "WebException: " + e.Status + " With response: " + e.Message;
            }

            catch (Exception e)
            {
                return "Exception: " + e.Message;
            }

        }

        public string HttpPut2(string url, string XMLData)
        {
            string fullurl = m_baseurl + url;
            HttpWebRequest Request = (HttpWebRequest)WebRequest.Create(fullurl);
            Request.Method = "PUT";
            Request.ContentType = "application/atom+xml";
            SignRequest(ref Request);


            byte[] byteArray = Encoding.UTF8.GetBytes(XMLData);

            try
            {
                Request.ContentLength = byteArray.Length;
                string XMLResponse = "";
                Stream streamRequest = Request.GetRequestStream();
                streamRequest.Write(byteArray, 0, byteArray.Length);
                streamRequest.Close();
                HttpWebResponse Response = (HttpWebResponse)Request.GetResponse();

                StreamReader Reader = new StreamReader(Response.GetResponseStream());
                XMLResponse = Reader.ReadToEnd();
                Reader.Close();
                Response.Close();

                return XMLResponse;
            }
            catch (WebException e)
            {
                return "WebException: " + e.Status + " With response: " + e.Message;
            }

            catch (Exception e)
            {
                return "Exception: " + e.Message;
            }

        }

        public string HttpPost2(string url, string XMLData)
        {
            string fullurl = m_baseurl + url;
            HttpWebRequest Request = (HttpWebRequest)WebRequest.Create(fullurl);
            Request.Method = "POST";
            Request.ContentType = "application/atom+xml";
            SignRequest(ref Request);
            byte[] byteArray = Encoding.UTF8.GetBytes(XMLData);

            try
            {
                Request.ContentLength = byteArray.Length;
                string XMLResponse = "";
                Stream streamRequest = Request.GetRequestStream();
                streamRequest.Write(byteArray, 0, byteArray.Length);
                streamRequest.Close();
                HttpWebResponse Response = (HttpWebResponse)Request.GetResponse();

                StreamReader Reader = new StreamReader(Response.GetResponseStream());
                XMLResponse = Reader.ReadToEnd();
                Reader.Close();
                Response.Close();

                return XMLResponse;
            }
            catch (WebException e)
            {
                return "WebException: " + e.Status + " With response: " + e.Message;
            }

            catch (Exception e)
            {
                return "Exception: " + e.Message;
            }
        }

        public string HttpDelete2(string url)
        {
            string fullurl = m_baseurl + url;
            HttpWebRequest Request = (HttpWebRequest)WebRequest.Create(fullurl);
            Request.Method = "DELETE";
            SignRequest(ref Request);


            try
            {
                HttpWebResponse Response = (HttpWebResponse)Request.GetResponse();

                StreamReader Reader = new StreamReader(Response.GetResponseStream());
                string XMLResponse = Reader.ReadToEnd();
                Reader.Close();
                Response.Close();

                return XMLResponse;
            }

            catch (WebException e)
            {
                return "WebException: " + e.Status + " With response: " + e.Message;
            }

            catch (Exception e)
            {
                return "Exception: " + e.Message;
            }

        }

        public void SignRequest(ref HttpWebRequest Request)
        {
            TimeSpan diff = DateTime.UtcNow - (new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc));
            uint timestamp = (uint)Math.Floor(diff.TotalSeconds); // Seconds since 1970
            string data = timestamp.ToString();

            Chilkat.PrivateKey privkey1 = new Chilkat.PrivateKey();
            privkey1.LoadPem(mykey);
            Chilkat.Rsa rsa1 = new Chilkat.Rsa();
            bool success = rsa1.UnlockComponent("HAFSJORSA_K36nxU3n1Yui");
            success = rsa1.ImportPrivateKey(privkey1.GetXml());
            rsa1.EncodingMode = "base64";
            rsa1.Charset = "ANSI";
            rsa1.LittleEndian = false;
            rsa1.OaepPadding = false;
            
            string s_mysecre = data + mysecret;
            char[] c_mysecre = s_mysecre.ToCharArray();
            byte[] b_mysecre = System.Text.Encoding.GetEncoding("windows-1252").GetBytes(c_mysecre);
            string hexSig = rsa1.SignBytesENC(b_mysecre, "sha-1");

            Request.Headers.Add("timestamp", data);
            Request.Headers.Add("signed", hexSig);
        }
        
        private static string mysecret = "2X4JYVdklF2YdsDxbWt8ygUaueUmgETSpwgOJ5wV0DpTrTSyEGFI4VobVi3fHQxfbh24aPdvbwPTRkZiRob9PEnbOAAlqnJ2iziBG0744kusyr6ykVUSkZLowBYrNsf2P1hojKOPLAIKSdKuPTRx5Ulrk91EnTd24JS2QLRFOD2sAcI9zEZ6ICsv5cWRzIeV4u0TlWmULmQGxN6s3mZA8d1KyxvwovqJwGN9aeVaMQQ5LHimm8gOXBIhmpmGKp9m";

/*
        private static string mycert = @"-----BEGIN CERTIFICATE-----
MIIGVTCCBT2gAwIBAgIKM3loxwAAAAAAMTANBgkqhkiG9w0BAQUFADBKMRIwEAYK
CZImiZPyLGQBGRYCZGsxGjAYBgoJkiaJk/IsZAEZFgpidXVzamVuc2VuMRgwFgYD
VQQDEw9UcnVzdEJ1dXNKZW5zZW4wHhcNMDkwMjAxMTI1NDU4WhcNMTEwMjAxMTI1
NDU4WjB2MQswCQYDVQQGEwJESzEQMA4GA1UECBMHRGVubWFyazETMBEGA1UEBxMK
Q29wZW5oYWdlbjEUMBIGA1UEChMLQnV1cyBKZW5zZW4xCzAJBgNVBAsTAklUMR0w
GwYDVQQDExRyZXZpMDEuYnV1c2plbnNlbi5kazCCASIwDQYJKoZIhvcNAQEBBQAD
ggEPADCCAQoCggEBANBtjLKCqP43ON7MqxkvwClfNYay/wQTh1gC4xuXnzil0mPG
Ra4ee975019TFf0T7/FVCqX1FzkNDQAfLfilie6S7zXGFqJdxOlOFxnsVZZK+T/4
TylAvYmu2XDLRJMY0eyS0DhFcmdnQYS7F+9MVDrD+fcvHiWxvh3i+fDbCZ22g0JA
xG8qBY8T8im3gZiN9JW6NXaUT3tsvYgLuLcwvGH/g+4Zz6qWIQag+8mnDJb6Tt2C
7VDfOmjY0QGDsJSuYVNg5iQN8lZLvH2wLBzi/z2ynDSvApAkeM4cbRiYXDiXOwUo
D0z5nlLs34cn39pHOzIQ9Hw0vcC9obh8f+AEjVsCAwEAAaOCAw8wggMLMEEGA1Ud
EQQ6MDiCBnJldmkwMYIUcmV2aTAxLmJ1dXNqZW5zZW4uZGuCDWJ1dXNqZW5zZW4u
ZGuCCWxvY2FsaG9zdDAdBgNVHQ4EFgQUI+h5+FbloiADG+LA68u5EueTMfYwHwYD
VR0jBBgwFoAU/GZKrn2uD/58nZBAHS54po1/NAAwggELBgNVHR8EggECMIH/MIH8
oIH5oIH2hoG3bGRhcDovLy9DTj1UcnVzdEJ1dXNKZW5zZW4sQ049cmV2aTAyLENO
PUNEUCxDTj1QdWJsaWMlMjBLZXklMjBTZXJ2aWNlcyxDTj1TZXJ2aWNlcyxDTj1D
b25maWd1cmF0aW9uLERDPWJ1dXNqZW5zZW4sREM9ZGs/Y2VydGlmaWNhdGVSZXZv
Y2F0aW9uTGlzdD9iYXNlP29iamVjdENsYXNzPWNSTERpc3RyaWJ1dGlvblBvaW50
hjpodHRwOi8vcmV2aTAyLmJ1dXNqZW5zZW4uZGsvQ2VydEVucm9sbC9UcnVzdEJ1
dXNKZW5zZW4uY3JsMIIBIgYIKwYBBQUHAQEEggEUMIIBEDCBsAYIKwYBBQUHMAKG
gaNsZGFwOi8vL0NOPVRydXN0QnV1c0plbnNlbixDTj1BSUEsQ049UHVibGljJTIw
S2V5JTIwU2VydmljZXMsQ049U2VydmljZXMsQ049Q29uZmlndXJhdGlvbixEQz1i
dXVzamVuc2VuLERDPWRrP2NBQ2VydGlmaWNhdGU/YmFzZT9vYmplY3RDbGFzcz1j
ZXJ0aWZpY2F0aW9uQXV0aG9yaXR5MFsGCCsGAQUFBzAChk9odHRwOi8vcmV2aTAy
LmJ1dXNqZW5zZW4uZGsvQ2VydEVucm9sbC9yZXZpMDIuYnV1c2plbnNlbi5ka19U
cnVzdEJ1dXNKZW5zZW4uY3J0MCEGCSsGAQQBgjcUAgQUHhIAVwBlAGIAUwBlAHIA
dgBlAHIwDAYDVR0TAQH/BAIwADALBgNVHQ8EBAMCBaAwEwYDVR0lBAwwCgYIKwYB
BQUHAwEwDQYJKoZIhvcNAQEFBQADggEBAE9T8lSVcR7/SVkivXar2t3NV1YlVsur
9V/NIwoCUfbfiRw1rM/FGT62LRnQRIg/zUunuCVJiE3xhNRC1ZOXkeyAsRI5Qm+d
Y/nzTOCPcHj/IUSWVIKxGp/2I+w3u+uDkSTkdoAX5hKuKUJpsTFgb0lBU3B7crKG
rtsN6nde2tcIsRuv+q+oMSzKQo/o2qXGRyDQkx5rxo1fkrQXuzYaaPF52hALyEV9
yGyIscsRPJFsC8wyhFiYNeOvjDH1Yli3UKzadqZSlU5IxI3ElfcvRz0lqGLXtuaS
zRQFtWA5gB8qci4X8lH4vtyJVpiLUtHNZnfzIRo0ADxoLW+ggPgSZys=
-----END CERTIFICATE-----";
*/
        private static string mykey =
@"-----BEGIN RSA PRIVATE KEY-----
MIIEowIBAAKCAQEA0G2MsoKo/jc43syrGS/AKV81hrL/BBOHWALjG5efOKXSY8ZF
rh573vnTX1MV/RPv8VUKpfUXOQ0NAB8t+KWJ7pLvNcYWol3E6U4XGexVlkr5P/hP
KUC9ia7ZcMtEkxjR7JLQOEVyZ2dBhLsX70xUOsP59y8eJbG+HeL58NsJnbaDQkDE
byoFjxPyKbeBmI30lbo1dpRPe2y9iAu4tzC8Yf+D7hnPqpYhBqD7yacMlvpO3YLt
UN86aNjRAYOwlK5hU2DmJA3yVku8fbAsHOL/PbKcNK8CkCR4zhxtGJhcOJc7BSgP
TPmeUuzfhyff2kc7MhD0fDS9wL2huHx/4ASNWwIDAQABAoIBABDbCynUjz4f0SWT
f7LFvdCatoVyLFV0Dtn7QcqVdHbsUhtniXMPXA0oPwPSgFC7MAhgTEAnlf0zJP4B
h4I4QPNeRqIepu3yj14expd+GV3SKl4WArDfX3SnA0av6ZfLxg5PwS8Lzri2DQJi
7wiXL6ig+LIYyWNbAHkCRhxIWnq6hnSOBPcqeqAoO+CVznHU8QYuR1z8JVq/PKok
9cAyLSuZS37h1mSMSXkZSA/IdGjqPJAwLLcTCHjCUDuDu3ailDb4Ah1bYJLUNgJd
gECkYSR8wLztEI8stiED1o0HKM3q1/PQPLjJMsD/DmgDY08fec6F+y7099AMB7ak
MeKY2oECgYEA8bfRt/GZqM6fmMwbNtU596NdIzlSDmA7kR8tJzGxhiriIWgmN5m2
hkTGN9NJtjP0r12vG8Ez01EbUlKfa3OqKTF9zu/PIxuU46H2CK+IP+ghHU9OHG15
wrI+l+fTkJ6gq7qcrU8I2/B0jtKdmKUdl3SpO8IrSG/h6U4TpJQnQesCgYEA3L4w
wzUjToUx4k9sz1IjWuNTuQNR+/3LxJcpJtCofgC0Ut1pAKtPX8MS7s2Sp0ZMpurU
KVBPQ77sZXpXRyPQ4bGfamkctjLys3HffoSCRMx+HaGrNk4TwNhXkxJaP9xAFNzL
HYwMaQRKXFe2v6XcgUCac+ZGL1NgqKMGGiWrllECgYEAtPJKSEzQHpIu3w9MAAw2
zK66djfeuWxIqyaPgpusrSdFCIUStuSWwoSRbhD5STAzp2OWRkynIzXAIiw/swxv
AU9PQq46famUF6OSroXYlR6MS4imjJlXYOxV9xlQQx68YFHeH87ebubeGlyIJVDV
ih+G4HlGNX+ruh78jWNqz+kCgYA/Qwp6h1oNAMMhFp4adHHJdGjkFv2B+GRTfPbA
NwByzATh0q5rEK14xlFAuw2SfuUs2RPgmzF8OtVI59zneG4+oEcNmf4ugT9pCfOB
MLyctvZVy6VjtNCYbef7MEFJF/gNgpF7cE2GM0KUYFbxableGYOqP45RtdV3vvDa
wX0BYQKBgAWKU6oIYwIwbkmO+sg/J7jdBOi2zw6MEkjmvL5V79iLitCymthuRvbU
dvWGctGWylRcl7DguOCENBcJkwijnkOoQ2MBuYtSARVTtSwM5nvhybaJmDbk91cV
r5mRr35LXXpnqPDbATFNc9v4kCjZdiD80KI/LwJhg89eskOnM8aY
-----END RSA PRIVATE KEY-----";

    }
}
