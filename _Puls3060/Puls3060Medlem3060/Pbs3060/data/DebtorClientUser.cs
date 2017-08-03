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
