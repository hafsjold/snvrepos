namespace Pbs3060
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class tblNytMedlem
    {
        [Key]
        public int id { get; set; }
        public System.DateTime MessageDate { get; set; }
        public string MessageFrom { get; set; }
        public string MessageID { get; set; }
        public string Fornavn { get; set; }
        public string Efternavn { get; set; }
        public string Adresse { get; set; }
        public string Postnr { get; set; }
        public string Bynavn { get; set; }
        public string Telefon { get; set; }
        public string Mobil { get; set; }
        public string Email { get; set; }
        public Nullable<System.DateTime> FodtDato { get; set; }
        public string Besked { get; set; }
        public string Kon { get; set; }
        public Nullable<int> Nr { get; set; }
    }
}
