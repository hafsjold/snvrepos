using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace nsHafsjoldData
{
    public class clsImport
    {
        public void ImportDanskeErhverv()
        {

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
                       Skjul = false,
                       Afstem = (int?)null
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
                from d1 in details.DefaultIfEmpty(new Tblbilag { Pid = 0, Rid = (int?)null, Bilag = (int?)null, Dato = (DateTime?)null, Udskriv = (bool?)null })
                where d1.Bilag == null
                select h;

            var qry_nyebilag =
                from p in leftqry_Posteringsjournal
                group p by new { p.Rid, p.Dato, p.Bilag } into g
                select new { RDB = g.Key, Antal = g.Count() };



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
            }

            var qry_nyetrans1 =
                from h in Program.karPosteringsjournal
                join d1 in Program.dbHafsjoldData.Tblbilag on new { h.Rid, h.Bilag } equals new { d1.Rid, d1.Bilag } into details
                from d1 in details.DefaultIfEmpty(new Tblbilag { Pid = 0, Rid = (int?)null, Bilag = (int?)null, Dato = (DateTime?)null, Udskriv = (bool?)null })
                where d1.Bilag != null
                select new Tbltrans
                {
                    Skjul = (bool?)false,
                    Bilagpid = d1.Pid,
                    Tekst = h.Tekst,
                    Kontonr = h.Konto,
                    Kontonavn = h.Kontonavn,
                    Moms = h.Moms,
                    Debet = (h.Bruttobeløb > 0) ? h.Bruttobeløb : (decimal?)null,
                    Kredit = (h.Bruttobeløb < 0) ? -h.Bruttobeløb : (decimal?)null,
                    Id = h.Id,
                    Nr = h.Nr,
                    Belob = h.Bruttobeløb,
                    Afstem = (int?)null
                };

            var qry_nyetrans2 =
                from h in qry_nyetrans1
                join d1 in Program.dbHafsjoldData.Tbltrans on new { h.Bilagpid, h.Id } equals new { d1.Bilagpid, d1.Id } into details
                from d1 in details.DefaultIfEmpty(new Tbltrans { Pid = 0, Skjul = (bool?)false, Bilagpid = (int?)null, Tekst = null, Kontonr = (int?)null, Kontonavn = null, Moms = (decimal?)null, Debet = (decimal?)null, Kredit = (decimal?)null, Id = (int?)null, Nr = (int?)null, Belob = (decimal?)null, Afstem = (int?)null })
                where d1.Bilagpid == null
                select h;
            int AntalTrans = qry_nyetrans2.Count();

            if (AntalTrans > 0)
            {
                foreach (Tbltrans rec_nyetrans in qry_nyetrans2)
                {
                    Program.dbHafsjoldData.Tbltrans.InsertOnSubmit(rec_nyetrans);
                }
                Program.dbHafsjoldData.SubmitChanges();
            }
        }
        
        public void ImportKladder()
        {

            var leftqry_Kladder =
                from h in Program.karKladder
                join d1 in Program.dbHafsjoldData.Tblbilag on new { h.Rid, h.Bilag } equals new { d1.Rid, d1.Bilag } into details
                from d1 in details.DefaultIfEmpty(new Tblbilag { Pid = 0, Rid = (int?)null, Bilag = (int?)null, Dato = (DateTime?)null, Udskriv = (bool?)null })
                where d1.Bilag == null
                select h;

            var qry_nyebilag =
                from p in leftqry_Kladder
                group p by new { p.Rid, p.Dato, p.Bilag } into g
                select new { RDB = g.Key, Antal = g.Count() };



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
            }

            var qry_nyekladder1 =
                from h in Program.karKladder
                join d1 in Program.dbHafsjoldData.Tblbilag on new { h.Rid, h.Bilag } equals new { d1.Rid, d1.Bilag } into details
                from d1 in details.DefaultIfEmpty(new Tblbilag { Pid = 0, Rid = (int?)null, Bilag = (int?)null, Dato = (DateTime?)null, Udskriv = (bool?)null })
                where d1.Bilag != null
                select new Tblkladder
                {
                    Bilagpid = d1.Pid,
                    Tekst = h.Tekst,
                    Afstemningskonto = h.Afstemningskonto,
                    Belob = h.Belob,
                    Konto = h.Konto,
                    Momskode = h.MomsKode,
                    Faktura = h.Faknr,
                    Id = h.Id
                };
            var qry_nyekladder2 =
                from h in qry_nyekladder1
                join d1 in Program.dbHafsjoldData.Tblkladder on new { h.Bilagpid, h.Id } equals new { d1.Bilagpid, d1.Id } into details
                from d1 in details.DefaultIfEmpty(new Tblkladder { Pid = 0, Bilagpid = (int?)null, Tekst = null, Afstemningskonto = null, Belob = (decimal?)null, Konto = (int?)null, Momskode = null, Faktura = (int?)null, Id = (int?)null })
                where d1.Bilagpid == null
                select h;
            int AntalTrans = qry_nyekladder2.Count();

            if (AntalTrans > 0)
            {
                foreach (Tblkladder rec_nyekladder in qry_nyekladder2)
                {
                    Program.dbHafsjoldData.Tblkladder.InsertOnSubmit(rec_nyekladder);
                }
                Program.dbHafsjoldData.SubmitChanges();
            }
        }
    }
}
