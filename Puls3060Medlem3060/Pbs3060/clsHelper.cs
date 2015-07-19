using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Collections;

namespace nsPbs3060
{
    static class Program
    {
        public static void Log(string message)
        {
            string msg = DateTime.Now.ToString() + "||" + message;
            Trace.WriteLine(msg);
        }

        public static void Log(string message, string category)
        {
            string msg = DateTime.Now.ToString() + "|" + category + "|" + message;
            Trace.WriteLine(msg);
        }
    }

    public class clsHelper
    {
        public static string FormatConnectionString(string connectstring) 
        {
            var cb = new SqlConnectionStringBuilder(connectstring);
            return string.Format("Database={0} on {1}", cb.InitialCatalog, cb.DataSource);
        }

        public static int? getParam(string parm, string name)
        {
            char[] simikolon = { ';' };
            char[] lighedstegn = { '=' };
            string[] arr = parm.Split(simikolon);
            foreach (string a in arr)
            {
                string[] var = a.Split(lighedstegn);
                if (var.Count() == 2)
                {
                    if (var[0].ToLower() == name.ToLower())
                        return int.Parse(var[1]);
                }
            }
            return null;
        }

        public static User_data unpack_UserData(string user_data) 
        {
            User_data rec = new User_data();
            string st_php = "a" + user_data.Substring(14);
            PHPSerializationLibrary.Serializer serializer = new PHPSerializationLibrary.Serializer();
            Hashtable php = (Hashtable)serializer.Deserialize(st_php);

            rec.name = (string)php["name"];
            rec.username = (string)php["username"];
            rec.email = (string)php["email"];
            Hashtable fields = (Hashtable)php["fields"];
            rec.adresse = (string)fields["adresse"];
            rec.postnr = (string)fields["postnr"];
            rec.bynavn = (string)fields["bynavn"];
            rec.mobil = (string)fields["mobil"];
            rec.memberid = (string)fields["memberid"];
            rec.kon = (string)((ArrayList)((Hashtable)php["membership_fields"])["kon"])[0];
            rec.fodtaar = (string)((ArrayList)((Hashtable)php["membership_fields"])["fodtaar"])[0];
            rec.message = (string)((Hashtable)php["membership_fields"])["message"];
            rec.password = (string)php["password"];

            return rec;
        }

        public static string pack_UserData(User_data rec_user_data)
        {
            Hashtable php = new Hashtable(25);
            Hashtable fields = new Hashtable(6);
            Hashtable membership_fields = new Hashtable(4);

            fields.Add("adresse",rec_user_data.adresse);
            fields.Add("postnr",rec_user_data.postnr);
            fields.Add("bynavn",rec_user_data.bynavn);
            fields.Add("mobil",rec_user_data.mobil);
            fields.Add("memberid",rec_user_data.memberid);
            
            ArrayList kon = new ArrayList(1);
            kon.Add(rec_user_data.kon);
            membership_fields.Add("kon",kon);
            ArrayList fodtaar = new ArrayList(1);
            fodtaar.Add(rec_user_data.fodtaar);
            membership_fields.Add("fodtaar",fodtaar);
            membership_fields.Add("message",rec_user_data.message);
            
            php.Add("name",rec_user_data.name);
            php.Add("username",rec_user_data.username);
            if (rec_user_data.email != null) php.Add("email", rec_user_data.email);
            php.Add("fields", fields);
            php.Add("membership_fields",membership_fields);
            if (rec_user_data.password != null) php.Add("password", rec_user_data.password);

            PHPSerializationLibrary.Serializer serializer = new PHPSerializationLibrary.Serializer();
            string user_data = @"O:8:""stdClass""" + serializer.Serialize(php).Substring(1); ;

            return user_data;
        }

    }
}
