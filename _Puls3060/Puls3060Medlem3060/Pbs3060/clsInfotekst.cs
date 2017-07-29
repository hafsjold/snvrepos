
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
//using System.Drawing;
using System.Linq;
using System.Text;
//using System.Windows.Forms;
using Microsoft.Win32;
using System.IO;
using Microsoft.VisualBasic;
//using System.Transactions;
using System.Text.RegularExpressions;
using MimeKit;

namespace nsPbs3060
{
    public class clsInfotekst
    {
        public int? infotekst_id { get; set; }
        public int? numofcol { get; set; }
        public string navn_medlem { get; set; }
        public string kaldenavn { get; set; }
        public DateTime? fradato { get; set; }
        public DateTime? tildato { get; set; }
        public DateTime? betalingsdato { get; set; }
        public decimal? advisbelob { get; set; }
        public string ocrstring { get; set; }
        public string underskrift_navn { get; set; }
        public string bankkonto { get; set; }
        public string advistekst { get; set; }
        public string sendtsom { get; set; }
        public string kundenr { get; set; }

        public string getinfotekst(dbData3060DataContext dbData3060)
        {
            string[] crlf = { "\r\n" };
            string infotext = null;

            try { infotext = (from i in dbData3060.tblinfoteksts where i.id == infotekst_id select i).First().msgtext; }
            catch (System.InvalidOperationException) { }

            RegexOptions options = ((RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline) | RegexOptions.IgnoreCase);
            Regex regexParam = new Regex(@"(\#\#[^\#]+\#\#)", options);

            string MsgtextSub = "";
            if (infotext != null)
            {
                MsgtextSub = regexParam.Replace(infotext, delegate(Match match)
                {
                    string v = match.ToString();
                    switch (v.ToLower())
                    {
                        case "##navn_medlem##":
                            if (navn_medlem != null) return navn_medlem;
                            break;

                        case "##kaldenavn##":
                            if (kaldenavn != null) return kaldenavn;
                            if (navn_medlem != null) return navn_medlem;
                            break;

                        case "##fradato##":
                            if (fradato != null) return string.Format("{0:d. MMMM yyyy}", fradato);
                            break;

                        case "##tildato##":
                            if (tildato != null) return string.Format("{0:d. MMMM yyyy}", tildato);
                            break;

                        case "##betalingsdato##":
                            if (betalingsdato != null) return string.Format("{0:dddd}", betalingsdato) + " den " + string.Format("{0:d. MMMM}", betalingsdato);
                            break;

                        case "##advisbelob##":
                            if (advisbelob != null) return String.Format("{0:###0.00}", advisbelob).Replace('.', ',');
                            break;

                        case "##ocrstring##":
                            if (ocrstring != null) return ocrstring;
                            break;

                        case "##underskrift_navn##":
                            if (underskrift_navn != null) return underskrift_navn;
                            break;

                        case "##bankkonto##":
                            if (bankkonto != null) return bankkonto;
                            break;

                        case "##advistekst##":
                            if (advistekst != null) return advistekst;
                            break;

                        case "##sendtsom##":
                            if (sendtsom != null) return sendtsom;
                            break;

                        case "##kundenr##":
                            if (sendtsom != null) return kundenr;
                            break;
                    }
                    return v;
                });

                if (numofcol == null)
                {
                    return MsgtextSub;
                }
                else
                {
                    return splittextincolumns(MsgtextSub, (int)numofcol);
                }
            }
            return "";
        }

        public string splittextincolumns(string msg, int numofcol)
        {
            return formattext(msg, numofcol);
        }

        public string formattext(string inputtext, int maxlinewith)
        {

            int currentlinelength;
            string outputtext = "";
            string workline = "";
            int linecount = 0;
            int wordcount = 0;
            string[] crlf = { "\r\n", "\n" };
            char[] bltp = { '\t', ' ' };

            string[] inputtextlines = inputtext.Split(crlf, StringSplitOptions.None);
            foreach (string currentline in inputtextlines)
            {
                if (currentline.Length <= maxlinewith)
                {
                    outputtext += currentline + "\r\n";
                    linecount++;
                }
                else
                {
                    currentlinelength = 0;
                    wordcount = 0;
                    string[] currentlines = currentline.Split(bltp);
                    foreach (string currentword in currentlines)
                    {
                        if (currentword.Length > 0)
                        {
                            if (currentlinelength == 0)
                            {
                                workline += currentword;
                                currentlinelength = currentword.Length;
                                wordcount++;
                            }
                            else
                            {
                                if ((currentlinelength + 1 + currentword.Length) <= maxlinewith)
                                {
                                    workline += " " + currentword;
                                    currentlinelength += 1 + currentword.Length;
                                    wordcount++;
                                }
                                else
                                {
                                    outputtext += expandline(workline, wordcount, maxlinewith) + "\r\n";
                                    linecount++;
                                    workline = currentword;
                                    currentlinelength = currentword.Length;
                                    wordcount = 1;
                                }
                            }
                        }
                    }

                    if (currentlinelength > 0)
                    {
                        outputtext += workline + "\r\n";
                        workline = "";
                        linecount++;
                    }
                }
            }
            return outputtext;
        }

        public string expandline(string inputline, int wordsinline, int outputlinewith)
        {
            string outputline = "";
            bool firstword = true;
            char[] splitchar = { ' ' };

            int blanksmissingcount = outputlinewith - inputline.Length;
            int blankscount = Math.Abs(blanksmissingcount / (wordsinline - 1));
            int blankscountmodulo = blanksmissingcount % (wordsinline - 1);
            string[] inputwords = inputline.Split(splitchar);
            foreach (string inputword in inputwords)
            {
                if (firstword)
                {
                    outputline = inputword;
                }
                else
                {
                    if (blankscountmodulo > 0)
                    {
                        outputline += inputword.PadLeft(inputword.Length + blankscount + 2, ' ');
                        blankscountmodulo--;
                    }
                    else
                    {
                        outputline += inputword.PadLeft(inputword.Length + blankscount + 1, ' ');
                    }
                }
                firstword = false;
            }
            return outputline;
        }

        public string substitute_message(string infotext)
        {
            RegexOptions options = ((RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline) | RegexOptions.IgnoreCase);
            Regex regexParam = new Regex(@"(\#\#[^\#]+\#\#)", options);

            string MsgtextSub = "";
            if (infotext != null)
            {
                MsgtextSub = regexParam.Replace(infotext, delegate(Match match)
                {
                    string v = match.ToString();
                    switch (v.ToLower())
                    {
                        case "##navn_medlem##":
                            if (navn_medlem != null) return navn_medlem;
                            break;

                        case "##kaldenavn##":
                            if (kaldenavn != null) return kaldenavn;
                            if (navn_medlem != null) return navn_medlem;
                            break;

                        case "##fradato##":
                            if (fradato != null) return string.Format("{0:d. MMMM yyyy}", fradato);
                            break;

                        case "##tildato##":
                            if (tildato != null) return string.Format("{0:d. MMMM yyyy}", tildato);
                            break;

                        case "##betalingsdato##":
                            if (betalingsdato != null) return string.Format("{0:dddd}", betalingsdato) + " den " + string.Format("{0:d. MMMM}", betalingsdato);
                            break;

                        case "##advisbelob##":
                            if (advisbelob != null) return String.Format("{0:###0.00}", advisbelob).Replace('.', ',');
                            break;

                        case "##ocrstring##":
                            if (ocrstring != null) return ocrstring;
                            break;

                        case "##underskrift_navn##":
                            if (underskrift_navn != null) return underskrift_navn;
                            break;

                        case "##bankkonto##":
                            if (bankkonto != null) return bankkonto;
                            break;

                        case "##advistekst##":
                            if (advistekst != null) return advistekst;
                            break;

                        case "##sendtsom##":
                            if (sendtsom != null) return sendtsom;
                            break;

                        case "##kundenr##":
                            if (sendtsom != null) return kundenr;
                            break;
                    }
                    return v;
                });
            }
            return MsgtextSub;
        }
    }
}
