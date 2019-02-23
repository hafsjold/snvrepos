using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using nsPbs3060;
using Uniconta.ClientTools.DataModel;
using Uniconta.DataModel;
using Uniconta.Common;
using Uniconta.API.GeneralLedger;
using Uniconta.API.System;
using Uniconta.API.DebtorCreditor;

namespace nsPbs3060
{
    public class clsUniconta
    {
        private CrudAPI m_api { get; set; }
        private CompanyFinanceYear m_CurrentCompanyFinanceYear { get; set; }
        private dbData3060DataContext m_dbData3060 { get; set; }

        public clsUniconta(dbData3060DataContext p_dbData3060, CrudAPI api)
        {
            m_dbData3060 = p_dbData3060;
            m_api = api;
            var task = api.Query<CompanyFinanceYear>();
            task.Wait();
            var cols = task.Result;
            foreach (var col in cols)
            {
                if (col._Current)
                {
                    m_CurrentCompanyFinanceYear = col;
                }
            }
        }

    }

    public class msmrecs
    {
        public int? faknr { get; set; }
        public int? Nr { get; set; }
        public string name { get; set; }
        public int? bogfkonto { get; set; }
        public DateTime? fradato { get; set; }
        public DateTime? tildato { get; set; }
    }
}
