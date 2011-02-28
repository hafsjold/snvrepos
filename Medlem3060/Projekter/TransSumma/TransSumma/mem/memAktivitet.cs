using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace nsPuls3060
{
    public class recMemAktivitet
    {
        public int Id { get; set; }
        public string Akt_tekst { get; set; }

        public recMemAktivitet(int pId, string pAkt_tekst)
        {
            Id = pId;
            Akt_tekst = pAkt_tekst;
        }
    }

    public class MemAktivitet : List<recMemAktivitet>
    {
        public MemAktivitet()
        {
            this.Add(new recMemAktivitet(10, "Indmeldelses dato"));
            this.Add(new recMemAktivitet(20, "PBS opkrævnings dato"));
            this.Add(new recMemAktivitet(30, "Kontingent betalt til dato"));
            this.Add(new recMemAktivitet(40, "PBS betaling tilbageført"));
            this.Add(new recMemAktivitet(50, "Udmeldelses dato"));
        }
    }
}
