using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uniconta.ClientTools.DataModel;

namespace nsPbs3060
{
    public class DebtorClientUser : DebtorClient
    {
        public string subscriberid
        {
            get { return this.GetUserFieldString(0); }
            set { this.SetUserFieldString(0, value); }
        }

    }
}
