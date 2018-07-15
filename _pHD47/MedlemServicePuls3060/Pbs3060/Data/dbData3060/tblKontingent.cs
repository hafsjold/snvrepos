namespace Pbs3060
{
    using System;
    using System.Collections.Generic;

    public partial class TblKontingent
    {
        public int Id { get; set; }
        public DateTime Startdato { get; set; }
        public DateTime Slutdato { get; set; }
        public int Startalder { get; set; }
        public int Slutalder { get; set; }
        public decimal Aarskontingent { get; set; }
    }
}
