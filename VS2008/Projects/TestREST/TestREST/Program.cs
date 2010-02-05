using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Web;

namespace TestREST
{
    class Program
    {
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

        static void Main(string[] args)
        {
            testdecrypt();
            //testencrypt();
            //string XMLMedlem = @"<?xml version=""1.0"" encoding=""utf-8"" ?><Medlem><key></key><Nr>5</Nr><FodtDato>1946-1-23</FodtDato><Kon>M</Kon><Postnr>3060</Postnr><Adresse>Pærevej 17 A</Adresse><Telefon>3026 0513</Telefon><Navn>Aksel Sloth Nørgaard</Navn><Bynavn>Espergærde</Bynavn><Kaldenavn>Aksel</Kaldenavn><Knr>1</Knr><Email>anx@msssd.dk</Email></Medlem>";

            //string fromhttp;
            //fromhttp = HttpGet(@"http://localhost:8080");

            //string[] paramName = { "content" };
            //string[] paramVal = { "Automatisk svar nummer 2" };
            //fromhttp = HttpPost(@"http://localhost:8080/sign", paramName, paramVal);

            //string fromXML = HttpGet2(@"http://localhost:8080/rest/Greeting/agpoZWxsb3dvcmxkcg4LEghHcmVldGluZxgBDA");


            //fromhttp = HttpPut2(@"http://localhost:8080/rest/Greeting/agpoZWxsb3dvcmxkcg4LEghHcmVldGluZxgBDA", fromXML);
            //fromhttp = HttpPost2(@"http://localhost:8080/rest/Medlem", XMLMedlem);

            //string fromXML = HttpDelete2(@"http://localhost:8080/rest/Greeting/agpoZWxsb3dvcmxkcg4LEghHcmVldGluZxgEDA");

        }
        static void testdecrypt()
        {
            //byte[] encryptedArr = {145, 110, 51, 179, 147, 38, 228, 145, 55, 179, 143, 45, 179, 239, 28, 251, 127, 202, 47, 73, 49, 31, 36, 232, 81, 219, 2, 180, 16, 104, 203, 148, 207, 36, 110, 184, 225, 133, 190, 185, 22, 75, 49, 69, 129, 101, 161, 215, 102, 66, 218, 127, 193, 201, 222, 181, 187, 251, 221, 205, 103, 188, 5, 77, 94, 236, 43, 121, 182, 233, 109, 123, 64, 93, 61, 61, 204, 157, 23, 17, 220, 187, 150, 187, 29, 230, 91, 89, 241, 27, 34, 18, 21, 195, 220, 231, 237, 47, 123, 247, 128, 107, 169, 115, 84, 103, 129, 126, 99, 231, 2, 23, 152, 183, 136, 70, 64, 116, 125, 198, 240, 128, 129, 133, 5, 144, 179, 255, 10, 14, 148, 216, 164, 78, 253, 190, 231, 153, 157, 64, 212, 78, 212, 191, 230, 120, 58, 223, 147, 241, 222, 191, 22, 99, 80, 126, 212, 172, 14, 43, 135, 43, 117, 47, 172, 161, 38, 67, 125, 205, 186, 91, 35, 89, 110, 243, 184, 200, 158, 220, 161, 222, 172, 53, 211, 90, 55, 126, 190, 183, 71, 101, 215, 218, 90, 68, 122, 226, 237, 119, 139, 176, 51, 129, 7, 71, 154, 196, 52, 16, 136, 104, 4, 108, 136, 112, 25, 45, 88, 232, 94, 159, 199, 221, 152, 88, 156, 73, 183, 158, 241, 10, 102, 50, 166, 183, 86, 252, 102, 4, 190, 144, 149, 136, 255, 115, 163, 177, 88, 67, 88, 85, 247, 0, 30, 159};

            string encryptedStr = "";
            encryptedStr = "YrOiK4Efq2oM4Ny0YW7VFnOtkWHpcFg1zHHfxpdEX1k/wbjbMHyBYn6Uo1qB6fcRNmokO1L48fQGfpeyErgWSt/D1pBlffV+QkGNLtApXIShPdOj8uqElP0T8skNTZCUNqsaB5MPwFsUDiuynimqikM6tBNdLss+z81LbxARUlao/yN112GoxQQ4wpnaDk/eOA51J+5aZb2jMssHwAFx3M3K8AJmUv4qBN8lOAnahd3QnDgaxk7gNydjKyVi1eRGHYxwQVNwMsV1we7EFDMBICG97i4GmWrP2BkrXs8J09osP2O++TIVZrxYpEGwwrRmtno1bBCfySWLNuxd33jwcg==";
            encryptedStr = "SnGp1BBb0fU7poFpM5Z0oD0YtfYZizU97GxUF9pn58yGEhpTdJ2vWy/NrhYMPflf4yym77zUyNVhDaOZ+1Q2H4imkiXa5q9DsBRN+dl5dVvosEse3OSyOTEbxn4AcrreWGhKTJ+/3mEhRotUE/rUuWskXKzSA+WQ0nSGUxSG/Rw0yaCeWsa1MU3UM0ugxxxd2gExY+K39+4nXUzNMv+H4XniTvmmUI7k8TEGAsZujwDsU9Oh+MbMtH8nlTu+yvfS++/dDTX9bFmpbkkH7FgbVvKcoICXsI5UfTATfRL4LkzdipO8VJNiPKT8TkgZyIJC/m1daLGAmIs+fM98rWm9yg==";
            //DECRYPT
            Chilkat.PrivateKey privkey1 = new Chilkat.PrivateKey();
            bool success = privkey1.LoadPem(mykey);

            Chilkat.Rsa rsa4 = new Chilkat.Rsa();
            success = rsa4.UnlockComponent("HAFSJORSA_K36nxU3n1Yui");

            rsa4.EncodingMode = "base64";
            rsa4.Charset = "ANSI";
            rsa4.LittleEndian = true; 
            rsa4.OaepPadding = false;
            success = rsa4.ImportPrivateKey(privkey1.GetXml());
            bool usePrivateKey = true;

            //byte[] decryptedArr = rsa4.DecryptBytes(encryptedArr, usePrivateKey);
            string decryptedStr = rsa4.DecryptStringENC(encryptedStr, usePrivateKey);

        }
 
        
        static void testencrypt()
        {

            string Token = String.Format("{0:yyyy-MM-dd HH:mm:ss}", new DateTime(2010, 02, 02, 21, 15, 0));

            //ENCRYPT
            Chilkat.Cert cert3 = new Chilkat.Cert();
            bool success = cert3.LoadFromBase64(mycert);
            Chilkat.PublicKey pubKey3 = null;
            pubKey3 = cert3.ExportPublicKey();
            Chilkat.Rsa rsa3 = new Chilkat.Rsa();
            success = rsa3.UnlockComponent("HAFSJORSA_K36nxU3n1Yui");
            rsa3.EncodingMode = "base64";
            rsa3.Charset = "ANSI";
            rsa3.LittleEndian = true;
            rsa3.OaepPadding = false;
            rsa3.ImportPublicKey(pubKey3.GetXml());
            bool usePrivateKey = false;
            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            byte[] TokenArr = encoding.GetBytes(Token);
            //array('B', [50, 48, 49, 48, 45, 48, 50, 45, 48, 50, 32, 50, 49, 58, 49, 53, 58, 48, 48])
            //byte[] encryptedArr = rsa3.EncryptBytes(TokenArr, usePrivateKey);
            string encryptedStr = rsa3.EncryptBytesENC(TokenArr, usePrivateKey);

        }

        static string HttpGet(string url)
        {
            bool success = false;
            HttpWebRequest req = WebRequest.Create(url) as HttpWebRequest;

            string Token = String.Format("{0:yyyy-MM-dd HH:mm:ss}", new DateTime(2010,02,02,21,15,0));
            //SIGN
            Chilkat.PrivateKey privkey1 = new Chilkat.PrivateKey();
            privkey1.LoadPem(mykey);
            Chilkat.Rsa rsa1 = new Chilkat.Rsa();
            success = rsa1.UnlockComponent("HAFSJORSA_K36nxU3n1Yui");
            success = rsa1.ImportPrivateKey(privkey1.GetXml());
            rsa1.EncodingMode = "base64";
            rsa1.Charset = "ANSI";
            rsa1.LittleEndian = false;
            rsa1.OaepPadding = false;
            string hexSig = rsa1.SignStringENC(Token, "sha-1");


            //VERIFY
            Chilkat.Cert cert2 = new Chilkat.Cert();
            success = cert2.LoadFromBase64(mycert);
            Chilkat.PublicKey pubKey2 = null;
            pubKey2 = cert2.ExportPublicKey();
            Chilkat.Rsa rsa2 = new Chilkat.Rsa();
            success = rsa2.ImportPublicKey(pubKey2.GetXml());
            rsa2.EncodingMode = "base64";
            rsa2.Charset = "ANSI";
            rsa2.LittleEndian = false;
            rsa2.OaepPadding = false;
            success = rsa2.VerifyStringENC(Token, "sha-1", hexSig);
            
            req.Headers.Add("Token", Token);
            req.Headers.Add("Signature", hexSig);

            //ENCRYPT
            Chilkat.Cert cert3 = new Chilkat.Cert();
            success = cert3.LoadFromBase64(mycert);
            Chilkat.PublicKey pubKey3 = null;
            pubKey3 = cert3.ExportPublicKey();
            Chilkat.Rsa rsa3 = new Chilkat.Rsa();
            success = rsa3.UnlockComponent("HAFSJORSA_K36nxU3n1Yui");
            rsa3.EncodingMode = "base64";
            rsa3.Charset = "ANSI";
            rsa3.LittleEndian = true;
            rsa3.OaepPadding = false;
            rsa3.ImportPublicKey(pubKey3.GetXml());
            bool usePrivateKey = false;
            System.Text.ASCIIEncoding  encoding=new System.Text.ASCIIEncoding();
            byte[] TokenArr = encoding.GetBytes(Token);
            //byte[] encryptedArr = rsa3.EncryptBytes(TokenArr, usePrivateKey);
            string encryptedstr = rsa3.EncryptBytesENC(TokenArr, usePrivateKey);

            //DECRYPT
            Chilkat.Rsa rsa4 = new Chilkat.Rsa();
            rsa4.EncodingMode = "base64";
            rsa4.Charset = "ANSI";
            rsa4.LittleEndian = true;
            rsa4.OaepPadding = false;
            rsa4.ImportPrivateKey(privkey1.GetXml());
            usePrivateKey = true;
            //encryptedStr = "XJ65xTR/xvD2N9xBKyKPqPijqTAyJuVtOlbaFUIboJaEPHH9pv+Lhrd5o6MSwKF6TeXs6hVsKnj8jVeYFYoEDgJS95GqaaUomWBhEZYchOp/6dn3ZxCeQoljAWLt6m4C829R9b5JYatYar9YV0d+QV+jVWE4U0rlNrkTqtA02Qw4ztN4/oehgCISrBnc81N1MYNwG9vrTHSVM6tSUWjWxMRubpOBvqKqOxyA9fpJNHgUyzio2X1cp12K++1GEUWNWyYVhTiBr/QM3mUN67mHcn0vvWZvmPhYlIaVn9DqIvVdMbHRbLwrCczFgY4PdHrhcH9yDTlkkAbKUatgDQiI4w==";
            //encryptedStr = "6KQbxh+x5SGIzD89zEwj+/IVVCBocemCXWl1mr+mk9wxRMydCfmMSUHDOafnqiJ6GAJapKbLTHOc9d1OyWTwsp5BQBT5VM20hb9r+AkDrHwkgL06ifizP0gTEO17cyO95jwlRXOfkQKb3cERLBEtOAnRep4bKMSsPLyxRRBX5VT4d19yxRor2V9js0CEFONinxl7qRxjckwvQk53+qpxeQ8jOx+pmrQukX7nWkMajWi+ZFndyfLL3LfRBYZKN2R0vdrnSMKdkxUEUUJybsv4QCMWshNpQznPSantq2dKNe07eB5mX4fRufy4mY4qjqBlf8+XFKdD+J37C6r3THL6pw==";
            //string decryptedStr = rsa4.DecryptStringENC(encryptedStr, usePrivateKey);



            
            Chilkat.Crypt2 crypt = new Chilkat.Crypt2();
            success = crypt.UnlockComponent("HAFSJOCrypt_0xo09cJWVQAw");
            crypt.EncodingMode = "base64";
            crypt.CryptAlgorithm = "none";
            req.Headers.Add("authorization", "Basic " + crypt.EncryptStringENC("Mogens:Hafsjold"));

            string result = null;
            using (HttpWebResponse resp = req.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(resp.GetResponseStream());
                result = reader.ReadToEnd();
            }
            return result;
        }

        static string HttpPost(string url, string[] paramName, string[] paramVal)
        {
            HttpWebRequest req = WebRequest.Create(new Uri(url)) as HttpWebRequest;
            //req.Method = "POST";
            req.Method = "PUT";
            req.ContentType = "application/x-www-form-urlencoded";

            // Build a string with all the params, properly encoded.
            // We assume that the arrays paramName and paramVal are
            // of equal length:
            StringBuilder Parm = new StringBuilder();
            for (int i = 0; i < paramName.Length; i++)
            {
                Parm.Append(paramName[i]);
                Parm.Append("=");
                Parm.Append(HttpUtility.UrlEncode(paramVal[i]));
                Parm.Append("&");
            }

            // Encode the parameters as form data:
            byte[] formData = UTF8Encoding.UTF8.GetBytes(Parm.ToString());
            req.ContentLength = formData.Length;

            // Send the request:
            using (Stream post = req.GetRequestStream())
            {
                post.Write(formData, 0, formData.Length);
            }

            // Pick up the response:
            string result = null;
            using (HttpWebResponse resp = req.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(resp.GetResponseStream());
                result = reader.ReadToEnd();
            }

            return result;
        }

        static string HttpGet2(string url)
        {
            WebRequest Request = WebRequest.Create(url); 

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

        static string HttpPut2(string url, string XMLData)
        {
            HttpWebRequest Request = (HttpWebRequest)WebRequest.Create(url);
            Request.Method = "PUT";
            Request.ContentType = "application/atom+xml";
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

        static string HttpPost2(string url, string XMLData)
        {
            HttpWebRequest Request = (HttpWebRequest)WebRequest.Create(url);
            Request.Method = "POST";
            Request.ContentType = "application/atom+xml";
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

        static string HttpDelete2(string url)
        {
            WebRequest Request = WebRequest.Create(url); 
            Request.Method = "DELETE";

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
    }
}
