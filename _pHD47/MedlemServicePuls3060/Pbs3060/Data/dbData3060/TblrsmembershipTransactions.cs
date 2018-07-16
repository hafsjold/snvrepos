namespace Pbs3060
{
    using System;
    using System.Collections.Generic;

    public partial class TblrsmembershipTransactions
    {
        public int Id { get; set; }
        public int TransId { get; set; }
        public int UserId { get; set; }
        public string UserEmail { get; set; }
        public string UserData { get; set; }
        public string Type { get; set; }
        public string Params { get; set; }
        public DateTime Date { get; set; }
        public string Ip { get; set; }
        public decimal Price { get; set; }
        public string Coupon { get; set; }
        public string Currency { get; set; }
        public string Hash { get; set; }
        public string Custom { get; set; }
        public string Gateway { get; set; }
        public string Status { get; set; }
        public string ResponseLog { get; set; }
        public string Name { get; set; }
        public string Adresse { get; set; }
        public string Postnr { get; set; }
        public string Bynavn { get; set; }
        public int Memberid { get; set; }
        public int MembershipId { get; set; }
        public int? SubscriberId { get; set; }

        //public Tblfak IdNavigation { get; set; }
    }
}
