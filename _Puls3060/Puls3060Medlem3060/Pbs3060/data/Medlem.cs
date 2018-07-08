using Uniconta.DataModel;

namespace nsPbs3060
{
    public class Medlem : TableDataWithKey
    {
        public override int UserTableId { get { return 1155; } }
        public string Adresse
        {
            get { return this.GetUserFieldString(0); }
            set { this.SetUserFieldString(0, value); }
        }

        public string Postnr
        {
            get { return this.GetUserFieldString(1); }
            set { this.SetUserFieldString(1, value); }
        }

        public string By
        {
            get { return this.GetUserFieldString(2); }
            set { this.SetUserFieldString(2, value); }
        }

        public string Mobil
        {
            get { return this.GetUserFieldString(3); }
            set { this.SetUserFieldString(3, value); }
        }

        public string Email
        {
            get { return this.GetUserFieldString(4); }
            set { this.SetUserFieldString(4, value); }
        }

        public string Debitor
        {
            get { return this.GetUserFieldString(5); }
            set { this.SetUserFieldString(5, value); }
        }

        public string Gender
        {
            get { return this.GetUserFieldString(6); }
            set { this.SetUserFieldString(6, value); }
        }

        public long Fodtaar
        {
            get { return this.GetUserFieldInt64(7); }
            set { this.SetUserFieldInt64(7, value); }
        }

        public string username
        {
            get { return this.GetUserFieldString(8); }
            set { this.SetUserFieldString(8, value); }
        }

        public long userid
        {
            get { return this.GetUserFieldInt64(9); }
            set { this.SetUserFieldInt64(9, value); }
        }

        public string subscriberid
        {
            get { return this.GetUserFieldString(10); }
            set { this.SetUserFieldString(10, value); }
        }

    }
}
