namespace Pbs3060
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class tblRegnskab
    {
        [Key]
        public int rid { get; set; }
        public string Navn { get; set; }
        public Nullable<System.DateTime> Oprettet { get; set; }
        public Nullable<System.DateTime> Start { get; set; }
        public Nullable<System.DateTime> Slut { get; set; }
        public Nullable<System.DateTime> DatoLaas { get; set; }
        public string Firmanavn { get; set; }
        public string Placering { get; set; }
        public string Eksportmappe { get; set; }
        public string TilPBS { get; set; }
        public string FraPBS { get; set; }
        public Nullable<bool> Afsluttet { get; set; }
    }
}
