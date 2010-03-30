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

        public void ImportPosteringsjournal()
        {

            var leftqry_Posteringsjournal =
                from h in Program.karPosteringsjournal
                join d1 in Program.dbHafsjoldData.Tblbilag on new { h.Rid, h.Bilag } equals new { d1.Rid, d1.Bilag } into details
                from d1 in details.DefaultIfEmpty(new Tblbilag {Pid = 0, Rid = (int?)null, Bilag = (int?)null, Dato = (DateTime?)null, Udskriv = (bool?)null })
                where d1.Bilag == null
                select h;

            var qry_nyebilag =
                from p in leftqry_Posteringsjournal
                group p by new { p.Rid, p.Dato, p.Bilag } into g
                select new { RDB = g.Key, Antal = g.Count() };

            var qry_nyetrans =
                from h in Program.karPosteringsjournal
                join d1 in Program.dbHafsjoldData.Tblbilag on new { h.Rid, h.Bilag } equals new { d1.Rid, d1.Bilag } into details
                from d1 in details.DefaultIfEmpty(new Tblbilag { Pid = 0, Rid = (int?)null, Bilag = (int?)null, Dato = (DateTime?)null, Udskriv = (bool?)null })
                where d1.Bilag != null
                select new Tbltrans {
                    Skjul = (bool?)false,
                    Bilagpid = d1.Pid,
                    Tekst = h.Tekst,
                    Kontonr = h.Konto,
                    Kontonavn = h.Kontonavn,
                    Moms = h.Moms,
                    Debet = (h.Bruttobeløb > 0) ? h.Bruttobeløb : (decimal?)null,
                    Kredit = (h.Bruttobeløb < 0) ? -h.Bruttobeløb : (decimal?)null,
                    Id = h.Id,
                    Nr = (byte?)h.Nr,
                    Belob = h.Bruttobeløb,
                    Afstem = (int?)null
                };

            int AntalBilag = qry_nyebilag.Count();

            if (AntalBilag > 0)
            {
                foreach (var rec_nyebilag in qry_nyebilag)
                {
                    Tblbilag m_rec_bilag = new Tblbilag
                    {
                        Rid = rec_nyebilag.RDB.Rid,
                        Bilag = rec_nyebilag.RDB.Bilag,
                        Dato = rec_nyebilag.RDB.Dato,
                        Udskriv = true
                    };
                    Program.dbHafsjoldData.Tblbilag.InsertOnSubmit(m_rec_bilag);
                }
                Program.dbHafsjoldData.SubmitChanges();

                foreach (var rec_nyetrans in leftqry_Posteringsjournal)
                {
                    Tbltrans m_rec_trans = new Tbltrans
                    {
                       // Rid = rec_nyetrans.RDB.Rid,
                       // Bilag = rec_nyetrans.RDB.Bilag,
                       // Dato = rec_nyetrans.RDB.Dato,
                       // Udskriv = true
                    };
                    Program.dbHafsjoldData.Tbltrans.InsertOnSubmit(m_rec_trans);
                }
                Program.dbHafsjoldData.SubmitChanges();

            }
        }
    }
}
