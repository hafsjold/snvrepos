using Uniconta.ClientTools.DataModel;

namespace nsPbs3060
{
    public class DebtorOrderLineClientUser : DebtorOrderLineClient
    {
        public string lintype
        {
            get { return this.GetUserFieldString(0); }
            set { this.SetUserFieldString(0, value); }
        }
    }
}
