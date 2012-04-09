using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace nsPbs3060
{
    public class clsHelper
    {
        public static string FormatConnectionString(string connectstring) 
        {
            var cb = new SqlConnectionStringBuilder(connectstring);
            return string.Format("Database={0} on {1}", cb.InitialCatalog, cb.DataSource);
        }
    }
}
