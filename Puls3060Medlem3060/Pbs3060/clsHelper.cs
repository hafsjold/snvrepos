using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Diagnostics;

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
    }
}
