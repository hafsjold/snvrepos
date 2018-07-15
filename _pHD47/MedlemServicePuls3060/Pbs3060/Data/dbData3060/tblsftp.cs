namespace Pbs3060
{
    using System;
    using System.Collections.Generic;

    public partial class Tblsftp
    {
        public int Id { get; set; }
        public string Navn { get; set; }
        public string Host { get; set; }
        public string Port { get; set; }
        public string User { get; set; }
        public string Outbound { get; set; }
        public string Inbound { get; set; }
        public string Pincode { get; set; }
        public string Certificate { get; set; }
    }
}
