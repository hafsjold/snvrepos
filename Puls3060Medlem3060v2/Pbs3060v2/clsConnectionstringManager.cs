using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;


namespace nsPbs3060v2
{
    public static class clsConnectionstringManager
    {
        public static string EFConnection = GetConnection();

        private static string GetConnection()
        {
            var sqlBuilder = new SqlConnectionStringBuilder();
            sqlBuilder.DataSource = System.Configuration.ConfigurationManager.AppSettings["SqlDataSource"];
            // fill in the rest
            sqlBuilder.InitialCatalog = ConfigurationManager.AppSettings["SqlInitialCatalog"];
            sqlBuilder.IntegratedSecurity = true;
            sqlBuilder.MultipleActiveResultSets = true;
            var eBuilder = new EntityConnectionStringBuilder();
            eBuilder.Provider = "System.Data.SqlClient";
            eBuilder.Metadata = "res://*/dbData3060.csdl|res://*/dbData3060.ssdl|res://*/dbData3060.msl";
            eBuilder.ProviderConnectionString = sqlBuilder.ToString();
            return eBuilder.ToString();
        }
    }

    public partial class dbData3060 : DbContext
    {
        public dbData3060(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }
    }
}
