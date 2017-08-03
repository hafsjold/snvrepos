using System;
using Uniconta.ClientTools.DataModel;

namespace nsPbs3060
{
    public class DebtorOrderClientUser : DebtorOrderClient
    {
        public DateTime medlemfra
        {
            get { return this.GetUserFieldDateTime(0); }
            set { this.SetUserFieldDateTime(0, value); }
        }

        public DateTime medlemtil
        {
            get { return this.GetUserFieldDateTime(1); }
            set { this.SetUserFieldDateTime(1, value); }
        }

    }
}
