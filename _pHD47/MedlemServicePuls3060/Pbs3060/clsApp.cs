using System;
using System.Collections.Generic;
using System.Text;

namespace Pbs3060
{
    public class clsApp
    {
        public static string UniContaUser { get { return PH("UniContaUser"); } }
        public static string UniContaPW { get { return PH("UniContaPW"); } }
        public static string UniContaCompanyId { get { return PH("UniContaCompanyId"); } }
        public static string GigaHostImapUser { get { return PH("GigaHostImapUser"); } }
        public static string GigaHostImapPW { get { return PH("GigaHostImapPW"); } }
        public static string dbPuls3060MedlemUser { get { return PH("dbPuls3060MedlemUser"); } }
        public static string dbPuls3060MedlemPW { get { return PH("dbPuls3060MedlemPW"); } }
        public static string puls3060_dkUser { get { return PH("puls3060_dkUser"); } }
        public static string puls3060_dkPW { get { return PH("puls3060_dkPW"); } }

        private static string PH(string key)
        {
            return key;
        }
    }
}
