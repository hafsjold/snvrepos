using Pbs3060;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uniconta.API.System;
using Uniconta.Common;

namespace TestPbs3060
{
    public class clsHelp
    {
        public static void update_betlin(dbData3060DataContext p_dbData3060)
        {
            clsPbs602 objPbs602 = new clsPbs602();
            var qry = (from i in p_dbData3060.Tblbetlin select i).ToList();
            foreach(var l in qry)
            {
                var Save_Nr = l.Nr;
                var Save_Faknr = l.Faknr;
                if (l.Nr == null)
                {
                    bool changed = false;
                    Tblbetlin k = objPbs602.Update_Nr_Faknr(l, p_dbData3060);
                    if (Save_Nr != k.Nr)
                    {
                        changed = true;
                    }
                    if (Save_Faknr != k.Faknr)
                    {
                        changed = true;
                    }
                    if (changed)
                    {
                        try
                        {
                            var betlin = (from i in p_dbData3060.Tblbetlin where i.Id == l.Id select i).First();
                            betlin.Nr = k.Nr;
                            betlin.Faknr = k.Faknr;
                            p_dbData3060.SaveChanges();
                        }
                        catch
                        {
                        }
                    }

                }
            }
        }


    }
}
