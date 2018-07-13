using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Pbs3060
{
    public class clsHelper
    {
        public static User_data unpack_UserData(string user_data)
        {
            try
            {
                User_data rec = new User_data();

                string st_php = "a" + user_data.Substring(14);
                Pbs3060.Serializer serializer = new Pbs3060.Serializer();
                Hashtable php = (Hashtable)serializer.Deserialize(st_php);

                rec.name = (string)php["name"];
                rec.username = (string)php["username"];
                rec.email = (string)php["email"];
                Hashtable fields = (Hashtable)php["fields"];
                rec.adresse = (string)fields["adresse"];
                rec.postnr = (string)fields["postnr"];
                rec.bynavn = (string)fields["bynavn"];
                rec.mobil = (string)fields["mobil"];
                rec.memberid = (string)fields["memberid"];
                rec.kon = (string)((ArrayList)((Hashtable)php["membership_fields"])["kon"])[0];
                rec.fodtaar = (string)((ArrayList)((Hashtable)php["membership_fields"])["fodtaar"])[0];
                rec.message = (string)((Hashtable)php["membership_fields"])["message"];
                rec.fiknr = (string)((Hashtable)php["membership_fields"])["fiknr"];
                rec.password = (string)php["password"];

                return rec;
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("unpack_UserData failed: {0} end", user_data));
                throw ex;
            }
        }

        public static string pack_UserData(User_data rec_user_data)
        {
            Hashtable php = new Hashtable(25);
            Hashtable fields = new Hashtable(6);
            Hashtable membership_fields = new Hashtable(4);

            fields.Add("adresse", rec_user_data.adresse);
            fields.Add("postnr", rec_user_data.postnr);
            fields.Add("bynavn", rec_user_data.bynavn);
            fields.Add("mobil", rec_user_data.mobil);
            fields.Add("memberid", rec_user_data.memberid);

            ArrayList kon = new ArrayList(1);
            kon.Add(rec_user_data.kon);
            membership_fields.Add("kon", kon);
            ArrayList fodtaar = new ArrayList(1);
            fodtaar.Add(rec_user_data.fodtaar);
            membership_fields.Add("fodtaar", fodtaar);
            membership_fields.Add("message", rec_user_data.message);
            if (rec_user_data.fiknr != null) membership_fields.Add("fiknr", rec_user_data.fiknr);

            php.Add("name", rec_user_data.name);
            php.Add("username", rec_user_data.username);
            if (rec_user_data.email != null) php.Add("email", rec_user_data.email);
            php.Add("fields", fields);
            php.Add("membership_fields", membership_fields);
            if (rec_user_data.password != null) php.Add("password", rec_user_data.password);

            Pbs3060.Serializer serializer = new Pbs3060.Serializer();
            string user_data = @"O:8:""stdClass""" + serializer.Serialize(php).Substring(1); ;

            return user_data;
        }

        public static int? getParam(string parm, string name)
        {
            char[] simikolon = { ';' };
            char[] lighedstegn = { '=' };
            string[] arr = parm.Split(simikolon);
            foreach (string a in arr)
            {
                string[] Var = a.Split(lighedstegn);
                if (Var.Length == 2)
                {
                    if (Var[0].ToLower() == name.ToLower())
                        return int.Parse(Var[1]);
                }
            }
            return null;
        }

        public static string generateIndbetalerident(int faknr)
        {
            string indbetalerident = null;
            try
            {
                char zero = '0';
                indbetalerident = (faknr.ToString()).PadLeft(14, zero);
            }
            catch { }

            //// check whether input string is null or empty
            if (string.IsNullOrEmpty(indbetalerident))
            {
                return "";
            }

            int sumOfDigits = indbetalerident.Where((e) => e >= '0' && e <= '9')
                            .Reverse()
                            .Select((e, i) => ((int)e - 48) * (i % 2 == 0 ? 2 : 1))
                            .Sum((e) => e / 10 + e % 10);

            int diget = (10 - (sumOfDigits % 10)) % 10;

            return indbetalerident + diget;
        }

        public static DateTime bankdageplus(DateTime pfradato, int antdage)
        {
            DateTime fradato = new DateTime(pfradato.Year, pfradato.Month, pfradato.Day);
            DateTime dato;
            int antal = 0;
            bool fridag;
            bool negativ;
            DateTime[] paaskedag = {   new DateTime(2009, 4, 12)
                                     , new DateTime(2010, 4, 4)
                                     , new DateTime(2011, 4, 24)
                                     , new DateTime(2012, 4, 8)
                                     , new DateTime(2013, 3, 31)
                                     , new DateTime(2014, 4, 20)
                                     , new DateTime(2015, 4, 5)
                                     , new DateTime(2016, 3, 27)
                                     , new DateTime(2017, 4, 16)
                                     , new DateTime(2018, 4, 1)
                                     , new DateTime(2019, 4, 21)
                                     , new DateTime(2020, 4, 12)
                                   };
            if (antdage < 0)
            {
                negativ = true;
                dato = fradato.AddDays(1);
            }
            else
            {
                negativ = false;
                dato = fradato.AddDays(-1);
            }

            while (antal <= Math.Abs(antdage))
            {
                if (negativ) dato = dato.AddDays(-1);
                else dato = dato.AddDays(1);

                if (dato.DayOfWeek == DayOfWeek.Saturday) fridag = true; //lørdag
                else if (dato.DayOfWeek == DayOfWeek.Sunday) fridag = true; //søndag
                else if ((dato.Month == 1) && (dato.Day == 1)) fridag = true; //1. nytårsdag
                else if ((from p in paaskedag select p).Contains(dato.AddDays(3))) fridag = true; //skærtorsdag
                else if ((from p in paaskedag select p).Contains(dato.AddDays(2))) fridag = true; //langfredag
                else if ((from p in paaskedag select p).Contains(dato.AddDays(-1))) fridag = true; //2. påskedag
                else if ((from p in paaskedag select p).Contains(dato.AddDays(-26))) fridag = true; //st. bededag (fredag)
                else if ((from p in paaskedag select p).Contains(dato.AddDays(-39))) fridag = true; //kristi himmelfartsdag (torsdag)
                else if ((from p in paaskedag select p).Contains(dato.AddDays(-40))) fridag = true; //fredag efter kristi himmelfartsdag
                else if ((from p in paaskedag select p).Contains(dato.AddDays(-50))) fridag = true; //2. pinsedag
                else if ((dato.Month == 6) && (dato.Day == 5)) fridag = true; //grundlovsdag
                else if ((dato.Month == 12) && (dato.Day == 24)) fridag = true; //juleaften
                else if ((dato.Month == 12) && (dato.Day == 25)) fridag = true; //1. juledag
                else if ((dato.Month == 12) && (dato.Day == 26)) fridag = true; //2. juledag
                else if ((dato.Month == 12) && (dato.Day == 31)) fridag = true; //nytårsaftens dag
                else fridag = false;
                if (!fridag) ++antal;
            }

            return dato;
        }

        public static void Update_memberid_in_rsmembership_transaction(recRSMembershipTransactions t1)
        {
            puls3060_nyEntities dbPuls3060_dk = new puls3060_nyEntities();

            var qry2 = from s2 in dbPuls3060_dk.ecpwt_rsmembership_transactions where s2.id == t1.id select s2;
            int c2 = qry2.Count();
            if (c2 == 1)
            {
                ecpwt_rsmembership_transactions t2 = qry2.First();
                User_data recud = clsHelper.unpack_UserData(t2.user_data);
                recud.memberid = t1.memberid.ToString();
                t2.user_data = clsHelper.pack_UserData(recud);
                dbPuls3060_dk.SaveChanges();
            }
            return;
        }

        public static string GenerateStringHash(string thisString)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] tmpSource;
            byte[] tmpHash;

            tmpSource = ASCIIEncoding.ASCII.GetBytes(thisString); // Turn password into byte array
            tmpHash = md5.ComputeHash(tmpSource);

            StringBuilder sOutput = new StringBuilder(tmpHash.Length);
            for (int i = 0; i < tmpHash.Length; i++)
            {
                sOutput.Append(tmpHash[i].ToString("X2").ToLower());  // X2 formats to hexadecimal
            }
            return sOutput.ToString();
        }

    }

    public class Serializer
        {
            //types:
            // N = null
            // s = string
            // i = int
            // d = double
            // a = array (hashtable)

            private Dictionary<Hashtable, bool> seenHashtables; //for serialize (to infinte prevent loops)
            private Dictionary<ArrayList, bool> seenArrayLists; //for serialize (to infinte prevent loops) lol

            private int pos; //for unserialize

            public bool XMLSafe = true; //This member tells the serializer wether or not to strip carriage returns from strings when serializing and adding them back in when deserializing
                                        //http://www.w3.org/TR/REC-xml/#sec-line-ends

            public Encoding StringEncoding = new System.Text.UTF8Encoding();

            private System.Globalization.NumberFormatInfo nfi;

            public Serializer()
            {
                this.nfi = new System.Globalization.NumberFormatInfo();
                this.nfi.NumberGroupSeparator = "";
                this.nfi.NumberDecimalSeparator = ".";
            }

            public string Serialize(object obj)
            {
                this.seenArrayLists = new Dictionary<ArrayList, bool>();
                this.seenHashtables = new Dictionary<Hashtable, bool>();

                return this.serialize(obj, new StringBuilder()).ToString();
            }//Serialize(object obj)

            private StringBuilder serialize(object obj, StringBuilder sb)
            {
                if (obj == null)
                {
                    return sb.Append("N;");
                }
                else if (obj is string)
                {
                    string str = (string)obj;
                    if (this.XMLSafe)
                    {
                        str = str.Replace("\r\n", "\n");//replace \r\n with \n
                        str = str.Replace("\r", "\n");//replace \r not followed by \n with a single \n  Should we do this?
                    }
                    return sb.Append("s:" + this.StringEncoding.GetByteCount(str) + ":\"" + str + "\";");
                }
                else if (obj is bool)
                {
                    return sb.Append("b:" + (((bool)obj) ? "1" : "0") + ";");
                }
                else if (obj is int)
                {
                    int i = (int)obj;
                    return sb.Append("i:" + i.ToString(this.nfi) + ";");
                }
                else if (obj is long)
                {
                    long i = (long)obj;
                    return sb.Append("i:" + i.ToString(this.nfi) + ";");
                }
                else if (obj is double)
                {
                    double d = (double)obj;

                    return sb.Append("d:" + d.ToString(this.nfi) + ";");
                }
                else if (obj is ArrayList)
                {
                    if (this.seenArrayLists.ContainsKey((ArrayList)obj))
                        return sb.Append("N;");//cycle detected
                    else
                        this.seenArrayLists.Add((ArrayList)obj, true);

                    ArrayList a = (ArrayList)obj;
                    sb.Append("a:" + a.Count + ":{");
                    for (int i = 0; i < a.Count; i++)
                    {
                        this.serialize(i, sb);
                        this.serialize(a[i], sb);
                    }
                    sb.Append("}");
                    return sb;
                }
                else if (obj is Hashtable)
                {
                    if (this.seenHashtables.ContainsKey((Hashtable)obj))
                        return sb.Append("N;");//cycle detected
                    else
                        this.seenHashtables.Add((Hashtable)obj, true);

                    Hashtable a = (Hashtable)obj;
                    sb.Append("a:" + a.Count + ":{");
                    foreach (DictionaryEntry entry in a)
                    {
                        this.serialize(entry.Key, sb);
                        this.serialize(entry.Value, sb);
                    }
                    sb.Append("}");
                    return sb;
                }
                else
                {
                    return sb;
                }
            }//Serialize(object obj)

            public object Deserialize(string str)
            {
                this.pos = 0;
                return deserialize(str);
            }//Deserialize(string str)

            private object deserialize(string str)
            {
                if (str == null || str.Length <= this.pos)
                    return new Object();

                int start, end, length;
                string stLen;
                switch (str[this.pos])
                {
                    case 'N':
                        this.pos += 2;
                        return null;
                    case 'b':
                        char chBool;
                        chBool = str[pos + 2];
                        this.pos += 4;
                        return chBool == '1';
                    case 'i':
                        string stInt;
                        start = str.IndexOf(":", this.pos) + 1;
                        end = str.IndexOf(";", start);
                        stInt = str.Substring(start, end - start);
                        this.pos += 3 + stInt.Length;
                        object oRet = null;
                        try
                        {
                            //firt try to parse as int
                            oRet = Int32.Parse(stInt, this.nfi);
                        }
                        catch
                        {
                            //if it failed, maybe it was too large, parse as long
                            oRet = Int64.Parse(stInt, this.nfi);
                        }
                        return oRet;
                    case 'd':
                        string stDouble;
                        start = str.IndexOf(":", this.pos) + 1;
                        end = str.IndexOf(";", start);
                        stDouble = str.Substring(start, end - start);
                        this.pos += 3 + stDouble.Length;
                        return Double.Parse(stDouble, this.nfi);
                    case 's':
                        start = str.IndexOf(":", this.pos) + 1;
                        end = str.IndexOf(":", start);
                        stLen = str.Substring(start, end - start);
                        int bytelen = Int32.Parse(stLen);
                        length = bytelen;
                        //This is the byte length, not the character length - so we might  
                        //need to shorten it before usage. This also implies bounds checking
                        if ((end + 2 + length) >= str.Length) length = str.Length - 2 - end;
                        string stRet = str.Substring(end + 2, length);
                        while (this.StringEncoding.GetByteCount(stRet) > bytelen)
                        {
                            length--;
                            stRet = str.Substring(end + 2, length);
                        }
                        this.pos += 6 + stLen.Length + length;
                        if (this.XMLSafe)
                        {
                            stRet = stRet.Replace("\n", "\r\n");
                        }
                        return stRet;
                    case 'a':
                        //if keys are ints 0 through N, returns an ArrayList, else returns Hashtable
                        start = str.IndexOf(":", this.pos) + 1;
                        end = str.IndexOf(":", start);
                        stLen = str.Substring(start, end - start);
                        length = Int32.Parse(stLen);
                        Hashtable htRet = new Hashtable(length);
                        ArrayList alRet = new ArrayList(length);
                        this.pos += 4 + stLen.Length; //a:Len:{
                        for (int i = 0; i < length; i++)
                        {
                            //read key
                            object key = deserialize(str);
                            //read value
                            object val = deserialize(str);

                            if (alRet != null)
                            {
                                if (key is int && (int)key == alRet.Count)
                                    alRet.Add(val);
                                else
                                    alRet = null;
                            }
                            htRet[key] = val;
                        }
                        this.pos++; //skip the }
                        if (this.pos < str.Length && str[this.pos] == ';')//skipping our old extra array semi-colon bug (er... php's weirdness)
                            this.pos++;
                        if (alRet != null)
                            return alRet;
                        else
                            return htRet;
                    default:
                        return "";
                }//switch
            }//unserialzie(object)	
        }//class Serializer

    public class User_data
    {
        public string name { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string adresse { get; set; }
        public string postnr { get; set; }
        public string bynavn { get; set; }
        public string mobil { get; set; }
        public string memberid { get; set; }
        public string kon { get; set; }
        public string fodtaar { get; set; }
        public string message { get; set; }
        public string fiknr { get; set; }
        public string password { get; set; }
    }

    public class recKontingentforslag
    {
        public DateTime betalingsdato { get; set; }
        public bool? bsh { get; set; }
        public int user_id { get; set; }
        public int membership_id { get; set; }
        public DateTime fradato { get; set; }
        public decimal advisbelob { get; set; }
        public DateTime? tildato { get; set; }
        public bool tilmeldtpbs { get; set; }
        public bool indmeldelse { get; set; }
        public int subscriber_id { get; set; }
        public int memberid { get; set; }
        public string name { get; set; }

    }
    public class Memkontingentforslag : List<recKontingentforslag>
    {
    }


}
