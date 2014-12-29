using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;

namespace nsPbs3060v2
{
    static class Program
    {
        public static void Log(string message)
        {
            string msg = DateTime.Now.ToString() + "||" + message;
            Trace.WriteLine(msg);
        }

        public static void Log(string message, string category)
        {
            string msg = DateTime.Now.ToString() + "|" + category + "|" + message;
            Trace.WriteLine(msg);
        }
    }

    public class clsHelper
    {
        public static string FormatConnectionString(string connectstring) 
        {
            var cb = new SqlConnectionStringBuilder(connectstring);
            return string.Format("Database={0} on {1}", cb.InitialCatalog, cb.DataSource);
        }
    }

    public partial class dbData3060 : DbContext
    {


        public string GetSysinfo(string pvkey)
        {
            var pvalParameter = new ObjectParameter("pval", typeof(string));
            var ret = this.GetSysinfo_v2(pvkey, pvalParameter);
            return (string)pvalParameter.Value;
        }

        public int nextval(string Pnrserienavn)
        {
            var PsidstbrugtenrParameter = new ObjectParameter("Psidstbrugtenr", typeof(int));
            var ret = this.nextval_v2(Pnrserienavn, PsidstbrugtenrParameter);
            return (int)PsidstbrugtenrParameter.Value;
        }

        public string OcrString(int? pFaknr)
        {
            var ResultParameter = new ObjectParameter("Result", typeof(string));
            var ret = this.OcrString_v2(pFaknr, ResultParameter);
            return (string)ResultParameter.Value;
        }

        public string SendtSomString(int? pFaknr)
        {
            var ResultParameter = new ObjectParameter("Result", typeof(string));
            var ret = this.SendtSomString_v2(pFaknr, ResultParameter);
            return (string)ResultParameter.Value;
        }

        public bool kanRykkes(int? pNr)
        {
            var ResultParameter = new ObjectParameter("Result", typeof(bool));
            var ret = this.kanRykkes_v2(pNr, ResultParameter);
            return (bool)ResultParameter.Value;
        }

        public bool erMedlem(int? pNr)
        {
            var ResultParameter = new ObjectParameter("Result", typeof(bool));
            var ret = this.erMedlem_v2(pNr, ResultParameter);
            return (bool)ResultParameter.Value;
        }

        public DateTime indmeldtdato(int? pNr)
        {
            var ResultParameter = new ObjectParameter("Result", typeof(DateTime));
            var ret = this.indmeldtdato_2(pNr, ResultParameter);
            return (DateTime)ResultParameter.Value;
        }

        public DateTime? udmeldtdato(int? pNr)
        {
            var UdmedltParameter = new ObjectParameter("Udmedlt", typeof(DateTime?));
            var ret = this.udmeldtdato_v2(pNr, UdmedltParameter);
            return (DateTime?)UdmedltParameter.Value;
        }

        public DateTime? kontingentdato(int? pNr)
        {
            var ResultParameter = new ObjectParameter("Result", typeof(DateTime?));
            var ret = this.kontingentdato_v2(pNr, ResultParameter);
            return (DateTime?)ResultParameter.Value;
        }

        public DateTime? forfaldsdato(int? pNr)
        {
            var ResultParameter = new ObjectParameter("Result", typeof(DateTime?));
            var ret = this.forfaldsdato_v2(pNr, ResultParameter);
            return (DateTime?)ResultParameter.Value;
        }

        public bool erPBS(int? pNr)
        {
            var ResultParameter = new ObjectParameter("Result", typeof(bool));
            var ret = this.erPBS_v2(pNr, ResultParameter);
            return (bool)ResultParameter.Value;
        }

        public DateTime? tilbageførtkontingentdato(int? pNr)
        {
            var ResultParameter = new ObjectParameter("Result", typeof(DateTime?));
            var ret = this.tilbageførtkontingentdato_v2(pNr, ResultParameter);
            return (DateTime?)ResultParameter.Value;
        }

        public bool MedlemPusterummet(int? pNr)
        {
            var ResultParameter = new ObjectParameter("Result", typeof(bool));
            var ret = this.MedlemPusterummet_v2(pNr, ResultParameter);
            return (bool)ResultParameter.Value;
        }
    }
}
