using System;
using System.Collections.Generic;
using System.Text;

namespace Pbs3060
{
    public class clsApp
    {
        public static string UniContaUser { get { return PH("puls3060"); } }
        public static string UniContaPW { get { return PH("1234West+"); } }
        public static string UniContaCompanyId { get { return PH("4852"); } }
        public static string GigaHostImapUser { get { return PH("regnskab@puls3060.dk"); } }
        public static string GigaHostImapPW { get { return PH("1234West+"); } }
        public static string dbPuls3060MedlemUser { get { return PH("sqlUser"); } }
        public static string dbPuls3060MedlemPW { get { return PH("Puls3060"); } }
        public static string puls3060_dkUser { get { return PH("puls3060"); } }
        public static string puls3060_dkPW { get { return PH("tasja123"); } }

        private static string PH(string key)
        {
            return key;
        }
    }
}
