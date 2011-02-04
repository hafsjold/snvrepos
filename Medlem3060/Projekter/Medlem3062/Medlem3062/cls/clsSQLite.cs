using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Data.Common;
using System.Data;
using System.Xml.Linq;

namespace nsPuls3060
{
    public static class clsSQLite
    {
        private static SQLiteConnection m_conn;

        static clsSQLite()
        {
            m_conn = new SQLiteConnection(@"Data Source=c:\mydatabasefile.db3");
            m_conn.Open();
            createDB();
        }

        public static void createDB()
        {
            SQLiteCommand sqltablecmd = new SQLiteCommand();
            sqltablecmd.Connection = m_conn;
            sqltablecmd.CommandText =
              @"CREATE TABLE IF NOT EXISTS [StoreXML] ( " +
              @"[id] GUID NOT NULL ON CONFLICT REPLACE, " +
              @"[created] DATETIME NOT NULL DEFAULT (datetime('now')), " +
              @"[target] VARCHAR DEFAULT (NULL), " +
              @"[ontarget] BOOLEAN DEFAULT (0), " +
              @"[source] VARCHAR DEFAULT (NULL), " +
              @"[data] TEXT DEFAULT (NULL), " +
              @"CONSTRAINT [sqlite_autoindex_StoreXML_1] PRIMARY KEY ([id]));";
            sqltablecmd.ExecuteNonQuery();
        }

        public static Guid insertStoreXML(String pvTarget, Boolean pvOntarget, string pvSource, string pvData)
        {
            SQLiteCommand sqlinsertcmd = new SQLiteCommand();
            sqlinsertcmd.Connection = m_conn;
            sqlinsertcmd.CommandText = @"INSERT INTO StoreXML (id, target, ontarget, source, data) values(@pId, @pTarget, @pOntarget, @pSource, @pData);";

            Guid guidId = Guid.NewGuid();

            DbParameter pId = sqlinsertcmd.CreateParameter();
            pId.ParameterName = "pId";
            pId.DbType = DbType.Guid;
            pId.Value = guidId;
            sqlinsertcmd.Parameters.Add(pId);
            
            DbParameter pTarget = sqlinsertcmd.CreateParameter();
            pTarget.ParameterName = "pTarget";
            pTarget.DbType = DbType.String;
            pTarget.Value = pvTarget;
            sqlinsertcmd.Parameters.Add(pTarget);
            
            DbParameter pOntarget = sqlinsertcmd.CreateParameter();
            pOntarget.ParameterName = "pOntarget";
            pOntarget.DbType = DbType.Boolean;
            pOntarget.Value = pvOntarget;
            sqlinsertcmd.Parameters.Add(pOntarget);
            
            DbParameter pSource = sqlinsertcmd.CreateParameter();
            pSource.ParameterName = "pSource";
            pSource.DbType = DbType.String;
            pSource.Value = pvSource;
            sqlinsertcmd.Parameters.Add(pSource);
            
            DbParameter pData = sqlinsertcmd.CreateParameter();
            pData.ParameterName = "pData";
            pData.DbType = DbType.String;
            pData.Value = pvData;
            sqlinsertcmd.Parameters.Add(pData);

            sqlinsertcmd.ExecuteNonQuery();

            return guidId;
        }

        public static void updateStoreXML(Guid pvId, bool pvOntarget)
        {
            SQLiteCommand sqlupdcmd = new SQLiteCommand();
            sqlupdcmd.Connection = m_conn;
            sqlupdcmd.CommandText = @"UPDATE StoreXML SET ontarget = @pOntarget WHERE id = @pId;";
            
            DbParameter pId = sqlupdcmd.CreateParameter();
            pId.ParameterName = "pId";
            pId.DbType = DbType.Guid;
            pId.Value = pvId;
            sqlupdcmd.Parameters.Add(pId);
            
            DbParameter pOntarget = sqlupdcmd.CreateParameter();
            pOntarget.ParameterName = "pOntarget";
            pOntarget.DbType = DbType.Boolean;
            pOntarget.Value = pvOntarget;
            sqlupdcmd.Parameters.Add(pOntarget);

            sqlupdcmd.ExecuteNonQuery();
        }

    }
}
