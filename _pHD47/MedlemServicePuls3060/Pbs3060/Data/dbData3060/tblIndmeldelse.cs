namespace Pbs3060
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class tblIndmeldelse
    {
        [Key]
        public int Id { get; set; }
        public int? Nr { get; set; }
        public string Fornavn { get; set; }
        public string Efternavn { get; set; }
        public string Adresse { get; set; }
        public string Postnr { get; set; }
        public string Bynavn { get; set; }
        public string Mobil { get; set; }
        public string Email { get; set; }
        public int? FodtAar { get; set; }
        public string Kon { get; set; }
    }
}
