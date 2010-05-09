using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Data;
using Excel;

namespace nsPuls3060
{
    public class recImportMedlem
    {
        public recImportMedlem() { }

        [Fieldattr(Heading = "Nr")]
        public int? Nr { get; set; }
        [Fieldattr(Heading = "Navn")]
        public string Navn { get; set; }
        [Fieldattr(Heading = "Kaldenavn")]
        public string Kaldenavn { get; set; }
        [Fieldattr(Heading = "Adresse")]
        public string Adresse { get; set; }
        [Fieldattr(Heading = "Postnr")]
        public string Postnr { get; set; }
        [Fieldattr(Heading = "By")]
        public string Bynavn { get; set; }
        [Fieldattr(Heading = "Email")]
        public string Email { get; set; }
        [Fieldattr(Heading = "Telefon")]
        public string Telefon { get; set; }
        [Fieldattr(Heading = "Køn")]
        public string Kon { get; set; }
        [Fieldattr(Heading = "Født")]
        public DateTime? FodtDato { get; set; }
        [Fieldattr(Heading = "erMedlem")]
        public int? erMedlem { get; set; }
    }


    public class clsImportMedlem : List<recImportMedlem>
    {
        private string m_kartotek_dat;

        public clsImportMedlem()
        {
            var rec_regnskab = Program.qryAktivRegnskab();
            m_kartotek_dat = rec_regnskab.Eksportmappe + "MedlemEkstern_20100503_021228.xls";
            Open();
        }

        public void Open()
        {
            FileStream fs = new FileStream(m_kartotek_dat, FileMode.Open, FileAccess.Read, FileShare.None);
            IExcelDataReader excelReader = ExcelReaderFactory.CreateBinaryReader(fs);
            excelReader.IsFirstRowAsColumnNames = true;
            DataSet result = excelReader.AsDataSet();
            excelReader.Close();

            System.Data.DataTable tbl = result.Tables["MedlemEkstern"];
            DataRowCollection recs = tbl.Rows;
            DataColumnCollection cols = tbl.Columns;
            int maxcol = cols.Count;

            foreach (DataRow rec in recs)
            {
                recImportMedlem r = new recImportMedlem();
                for(int i = 0; i < maxcol; i++)
                {
                    string Name = cols[i].ColumnName;
                    switch (Name)
                    {
                        case "Nr":
                            try
                            {
                                r.Nr = int.Parse(rec.ItemArray[i].ToString());
                            }
                            catch
                            {
                                r.Nr = null;
                            }
                            break;
                        case "Navn":
                            r.Navn = rec.ItemArray[i].ToString();
                            break;
                        case "Kaldenavn":
                            r.Kaldenavn = rec.ItemArray[i].ToString();
                            break;
                        case "Adresse":
                            r.Adresse = rec.ItemArray[i].ToString();
                            break;
                        case "Postnr":
                            r.Postnr = rec.ItemArray[i].ToString();
                            break;
                        case "By":
                            r.Bynavn = rec.ItemArray[i].ToString();
                            break;
                        case "Email":
                            r.Email = rec.ItemArray[i].ToString();
                            break;
                        case "Telefon":
                            r.Telefon = rec.ItemArray[i].ToString();
                            break;
                        case "Køn":
                            r.Kon = rec.ItemArray[i].ToString();
                            break;
                        case "Født":
                            try 
                            { 
                                r.FodtDato = clsUtil.MSSerial2DateTime(Double.Parse(rec.ItemArray[i].ToString()));
                            }
                            catch 
                            {
                                r.FodtDato = null;
                            }
                            break;
                        case "erMedlem":
                            try
                            {
                                r.erMedlem = int.Parse(rec.ItemArray[i].ToString());
                            }
                            catch
                            {
                                r.erMedlem = 3;
                            }
                            break;

                        default:
                            break;
                    }
                }
                this.Add(r);
            }

        }

    }
}

