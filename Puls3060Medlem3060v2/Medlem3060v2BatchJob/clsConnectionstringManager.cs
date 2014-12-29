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


namespace nsMedlem3060v2BatchJob
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
            eBuilder.Metadata = "res://*/dbPuls3060Medlem.csdl|res://*/dbPuls3060Medlem.ssdl|res://*/dbPuls3060Medlem.msl";
            eBuilder.ProviderConnectionString = sqlBuilder.ToString();
            return eBuilder.ToString();
        }
    }

    public partial class dbPuls3060Medlem : DbContext
    {
        public dbPuls3060Medlem(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }
    }
}
