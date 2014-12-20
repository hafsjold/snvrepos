using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace nsPuls3060
{
    public class clsField
    {
        public int id { get; private set; }
        public string Name { get; private set; }
        public string accName { get; private set; }
        public int accNumber { get; private set; }
        public int sumNumber { get; private set; }
        public int sumSubNumber { get; private set; }
        public string NewValue { get; private set; }
        public string NewType { get; private set; }
        public Boolean multifld { get; private set; }


        public clsField(int p_id,
            string p_Name,
            string p_accName,
            int p_accNumber,
            int p_sumNumber,
            int p_sumSubNumber,
            string p_NewValue,
            string p_NewType,
            Boolean p_multifld)
        {
            id = p_id;
            Name = p_Name;
            accName = p_accName;
            accNumber = p_accNumber;
            sumNumber = p_sumNumber;
            sumSubNumber = p_sumSubNumber;
            NewValue = p_NewValue;
            NewType = p_NewType;
            multifld = p_multifld;
        }

        public static string buildKey(int? p_sumNumber, int? p_sumSubNumber)
        {
            string key = null;
            if (p_sumNumber > 0)
            {
                key = p_sumNumber.ToString();
                if (p_sumSubNumber > 0)
                {
                    key += "," + p_sumSubNumber.ToString();
                }
            }
            return key;
        }
    }

    public class MemMedlemDictionary : List<clsField>
    {
        private Dictionary<string, int> m_nr_subnr;

        public MemMedlemDictionary()
        {
            //  1,   2,     3,       4,         5,            6,        7,       8          9
            // id, Name, accName, accNumber, sumNumber, sumSubNumber, NewValue, NewType, multifld
            this.Add(new clsField(0, "Ex4", "", 0, 28, 0, "", "", false));
            this.Add(new clsField(1, "Nr", "Nr", 1, 1, 0, "0", "SumNr", false));
            this.Add(new clsField(2, "Adresse 1", "Adresse", 4, 3, 1, "", "", false));
            this.Add(new clsField(3, "Postnr", "Postnr", 5, 4, 0, "0000", "Const", false));
            this.Add(new clsField(4, "By", "Bynavn", 6, 5, 0, "By_Skal_udfyldes", "Const", false));
            this.Add(new clsField(5, "Land", "", 0, 6, 0, "", "", false));
            this.Add(new clsField(6, "Kontaktperson", "", 0, 7, 0, "", "", false));
            this.Add(new clsField(7, "Køn", "Kon", 10, 0, 0, "", "", false));
            this.Add(new clsField(8, "E-Mail", "Email", 8, 9, 0, "", "", false));
            this.Add(new clsField(9, "Webside", "", 0, 10, 0, "", "", false));
            this.Add(new clsField(10, "Leveringsadresse 1", "", 0, 18, 1, "", "", false));
            this.Add(new clsField(11, "Noter 1", "", 0, 17, 1, "", "", false));
            this.Add(new clsField(12, "Debitor", "", 0, 22, 0, "1", "Const", false));
            this.Add(new clsField(13, "Lås for fakturering", "", 0, 29, 0, "0", "Const", false));
            this.Add(new clsField(14, "Debitor gruppe", "", 0, 11, 0, "Standard", "Const", false));
            this.Add(new clsField(15, "Debitor kontonr", "", 0, 20, 0, "", "NewDKnr", false));
            this.Add(new clsField(16, "Kreditor", "", 0, 23, 0, "", "", false));
            this.Add(new clsField(17, "Kreditor kontonr", "", 0, 21, 0, "", "", false));
            this.Add(new clsField(18, "Kreditor gruppe", "", 0, 12, 0, "", "", false));
            this.Add(new clsField(19, "Telefon", "Telefon", 7, 13, 0, "", "", false));
            this.Add(new clsField(20, "Mobiltelefon", "", 0, 14, 0, "", "", false));
            this.Add(new clsField(21, "Født Dato", "FodtDato", 11, 0, 0, "", "", false));
            this.Add(new clsField(22, "Bank", "", 0, 16, 0, "", "", false));
            this.Add(new clsField(23, "Debitor saldo", "", 0, 24, 0, "", "", false));
            this.Add(new clsField(24, "Kreditor saldo", "", 0, 25, 0, "", "", false));
            this.Add(new clsField(25, "Debitor salg", "", 0, 26, 0, "", "", false));
            this.Add(new clsField(26, "Kreditor Køb", "", 0, 27, 0, "", "", false));
            this.Add(new clsField(27, "Ex1", "Kaldenavn", 3, 8, 0, "", "", false));
            this.Add(new clsField(28, "Navn", "Navn", 2, 2, 0, "Navn_Skal_udfyldes", "Const", false));
            this.Add(new clsField(29, "Ex2", "", 0, 15, 0, "", "", false));
            this.Add(new clsField(30, "Ex3", "", 0, 19, 0, "", "", false));
            this.Add(new clsField(31, "Adresse", "", 0, 3, 0, "Adresse_Skal_udfyldes", "Const", true));
            this.Add(new clsField(32, "Leveringsadresse", "", 0, 18, 0, "", "", true));
            this.Add(new clsField(33, "Noter", "", 0, 17, 0, "", "", true));

            m_nr_subnr = new Dictionary<string, int>();
            foreach (clsField f in this)
            {
                string key;
                if (((key = clsField.buildKey(f.sumNumber, f.sumSubNumber)) != null))
                {
                    m_nr_subnr.Add(key, f.id);
                }
            }
        }

        public Boolean multifld(string p_key)
        {
            try
            {
                return this[m_nr_subnr[p_key]].multifld;
            }
            catch
            {
                return false;
            }
        }
        public Boolean multifld(int? p_key)
        {
            return multifld(p_key.ToString());
        }
        public Boolean multifld(int? p_key, int? p_subkey)
        {
            string key;
            if (((key = clsField.buildKey(p_key, p_subkey)) != null))
            {
                return multifld(key);
            }
            else
            {
                return false;
            }

        }

    }
}

