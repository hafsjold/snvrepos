namespace Pbs3060
{
    using System;
    using System.Collections.Generic;

    public partial class Tblfak
    {
        public int Id { get; set; }
        public int? Tilpbsid { get; set; }
        public DateTime? Betalingsdato { get; set; }
        public int? Nr { get; set; }
        public int? Faknr { get; set; }
        public string Advistekst { get; set; }
        public decimal? Advisbelob { get; set; }
        public int? Infotekst { get; set; }
        public int? Bogfkonto { get; set; }
        public int? Vnr { get; set; }
        public DateTime? Fradato { get; set; }
        public DateTime? Tildato { get; set; }
        public int? SfakId { get; set; }
        public int? Sfaknr { get; set; }
        public DateTime? Rykkerdato { get; set; }
        public DateTime? Maildato { get; set; }
        public bool Rykkerstop { get; set; }
        public bool Betalt { get; set; }
        public bool Tilmeldtpbs { get; set; }
        public bool Indmeldelse { get; set; }
        public string Indbetalerident { get; set; }

        //public TblrsmembershipTransactions Tblrsmembership_transactions { get; set; }
        public Tbltilpbs Tilpbs { get; set; }
    }
}
