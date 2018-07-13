using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Data.SqlClient;

namespace Pbs3060
{
    public partial class dbData3060DataContext : DbContext
    {
        public int nextval_v2(string Pnrserienavn)
        {

            using (var command = this.Database.GetDbConnection().CreateCommand())
            {
                var p1 = new SqlParameter();
                p1.ParameterName = "@Pnrserienavn";
                p1.Value = Pnrserienavn;
                p1.SqlDbType = System.Data.SqlDbType.NVarChar;
                p1.Size = 30;
                p1.IsNullable = true;
                p1.Direction = System.Data.ParameterDirection.Input;
                command.Parameters.Add(p1);

                var p2 = new SqlParameter();
                p2.ParameterName = "@Psidstbrugtenr";
                p2.SqlDbType = System.Data.SqlDbType.Int;
                p2.IsNullable = true;
                p2.Direction = System.Data.ParameterDirection.Output;
                command.Parameters.Add(p2);

                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = @"nextval_v2";
                this.Database.OpenConnection();
                using (var result = command.ExecuteReader())
                {
                    var Psidstbrugtenr = (int)command.Parameters["@Psidstbrugtenr"].Value;
                    return Psidstbrugtenr;
                }
            }
        }

        public string OcrString(int pFaknr)
        {
            var qry = from x in this.tblfak
                      where x.faknr == pFaknr
                      select new
                      {
                          Ocr = this.OcrString_Qry(x.faknr)
                      };

            if (qry.Count() == 1)
            {
                return qry.First().Ocr;
            }
            else
            {
                return "";
            }
        }

        public string SendtSomString(int pFaknr)
        {
            var qry = from x in this.tblindbetalingskort
                      where x.faknr == pFaknr
                      select new
                      {
                          SomString = this.SendtSomString_Qry(x.faknr)
                      };

            if (qry.Count() == 1)
            {
                return qry.First().SomString;
            }
            else
            {
                return "";
            }
        }

        public bool erPBS(int pNr)
        {
            var qry = from x in this.tblaftalelin
                      where x.Nr == pNr
                      select new
                      {
                          SomBool = this.erPBS_Qry(x.Nr)
                      };

            if (qry.Count() > 0)
            {
                return qry.First().SomBool;
            }
            else
            {
                return false;
            }
        }

    }
}
