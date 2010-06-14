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
        private string m_baseurl = @"http://localhost:8084/rest/";
        //private string m_baseurl = @"http://testhafsjold.appspot.com/rest/";

        public string HttpGet2(string url)
        {
            string fullurl = m_baseurl + url;
            WebRequest Request = WebRequest.Create(fullurl);

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
            Request.Headers.Add("authorization", "Basic Mogens Hafsjold");

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
            Request.Headers.Add("authorization", "Basic Mogens Hafsjold");

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
            WebRequest Request = WebRequest.Create(fullurl);
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
