using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace nsHafsjoldData
{
    public class clsImport
    {
        public void ImportDanskeErhverv() {

            var leftqry_DanskeErhverv =
                from h in Program.karDanskeErhverv
                join d1 in Program.dbHafsjoldData.Tblbankkonto on new { h.Dato, h.Belob, h.Saldo } equals new { d1.Dato, d1.Belob, d1.Saldo } into details
                from d1 in details.DefaultIfEmpty(new Tblbankkonto { Id = 0, Dato = (DateTime?)null, Tekst = null, Belob = (decimal?)null, Saldo = (decimal?)null, Skjul = (bool?)null })
                where d1.Saldo == null
                select h;
            int Antal = leftqry_DanskeErhverv.Count();
            if (Antal > 0)
            {
                foreach (var rec_DanskeErhverv in leftqry_DanskeErhverv)
                {
                    Tblbankkonto m_rec_bankkonto = new Tblbankkonto
                   {
                       Dato = rec_DanskeErhverv.Dato,
                       Tekst = rec_DanskeErhverv.Tekst,
                       Belob = rec_DanskeErhverv.Belob,
                       Saldo = rec_DanskeErhverv.Saldo,
                       Skjul = false
                   };
                    Program.dbHafsjoldData.Tblbankkonto.InsertOnSubmit(m_rec_bankkonto);
                }
            }
            Program.dbHafsjoldData.SubmitChanges();

        }
    }
}
