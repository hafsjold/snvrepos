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
        static void Main(string[] args)
        {
            string fromhttp;
            //fromhttp = HttpGet(@"http://localhost:8080");

            //string[] paramName = { "content" };
            //string[] paramVal = { "Automatisk svar nummer 2" };
            //fromhttp = HttpPost(@"http://localhost:8080/sign", paramName, paramVal);

            //string fromXML = HttpGet2(@"http://localhost:8080/rest/Greeting/agpoZWxsb3dvcmxkcg4LEghHcmVldGluZxgBDA");


            //fromhttp = HttpPut2(@"http://localhost:8080/rest/Greeting/agpoZWxsb3dvcmxkcg4LEghHcmVldGluZxgBDA", fromXML);
            //fromhttp = HttpPost2(@"http://localhost:8080/rest/Greeting", fromXML);

            string fromXML = HttpDelete2(@"http://localhost:8080/rest/Greeting/agpoZWxsb3dvcmxkcg4LEghHcmVldGluZxgEDA");

        }

        static string HttpGet(string url)
        {
            HttpWebRequest req = WebRequest.Create(url) as HttpWebRequest;
            req.UserAgent = "mhaAgent";
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
