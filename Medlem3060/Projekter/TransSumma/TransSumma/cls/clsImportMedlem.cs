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

            System.Windows.Forms.OpenFileDialog fd = new System.Windows.Forms.OpenFileDialog();
            fd.Title = "Vælg MedlemImport Excel file";
            fd.InitialDirectory = rec_regnskab.Eksportmappe;
            fd.Filter = "xls files (*.xls)|*.xls|All files (*.*)|*.*";
            fd.FilterIndex = 1;
            System.Windows.Forms.DialogResult r = fd.ShowDialog();
            if (r == System.Windows.Forms.DialogResult.OK)
            {
                m_kartotek_dat = fd.FileNames[0];
                Open();
            }
        }

        public void Open()
        {
            FileStream fs = new FileStream(m_kartotek_dat, FileMode.Open, FileAccess.Read, FileShare.None);
            IExcelDataReader excelReader = ExcelReaderFactory.CreateBinaryReader(fs);
            excelReader.IsFirstRowAsColumnNames = true;
            DataSet result = excelReader.AsDataSet();
            excelReader.Close();

            System.Data.DataTable tbl = result.Tables["MedlemEkstern"];
            if (tbl != null)
            {
                DataRowCollection recs = tbl.Rows;
                DataColumnCollection cols = tbl.Columns;
                int maxcol = cols.Count;

                foreach (DataRow rec in recs)
                {
                    recImportMedlem r = new recImportMedlem();
                    int NotEmptyCount = 0;
                    for (int i = 0; i < maxcol; i++)
                    {
                        string Name = cols[i].ColumnName;
                        switch (Name)
                        {
                            case "Nr":
                                try
                                {
                                    r.Nr = int.Parse(rec.ItemArray[i].ToString());
                                    NotEmptyCount++;
                                }
                                catch
                                {
                                    r.Nr = null;
                                }
                                break;
                            case "Navn":
                                if (rec.ItemArray[i].ToString().Length > 0)
                                {
                                    r.Navn = rec.ItemArray[i].ToString();
                                    NotEmptyCount++;
                                }
                                else
                                {
                                    r.Navn = null;
                                }
                                break;
                            case "Kaldenavn":
                                if (rec.ItemArray[i].ToString().Length > 0)
                                {
                                    r.Kaldenavn = rec.ItemArray[i].ToString();
                                    NotEmptyCount++;
                                }
                                else
                                {
                                    r.Kaldenavn = null;
                                }
                                break;
                            case "Adresse":
                                if (rec.ItemArray[i].ToString().Length > 0)
                                {
                                    r.Adresse = rec.ItemArray[i].ToString();
                                    NotEmptyCount++;
                                }
                                else
                                {
                                    r.Adresse = null;
                                }
                                break;
                            case "Postnr":
                                if (rec.ItemArray[i].ToString().Length > 0)
                                {
                                    r.Postnr = rec.ItemArray[i].ToString();
                                    NotEmptyCount++;
                                }
                                else
                                {
                                    r.Postnr = null;
                                }
                                break;
                            case "By":
                                if (rec.ItemArray[i].ToString().Length > 0)
                                {
                                    r.Bynavn = rec.ItemArray[i].ToString();
                                    NotEmptyCount++;
                                }
                                else
                                {
                                    r.Bynavn = null;
                                }
                                break;
                            case "Email":
                                if (rec.ItemArray[i].ToString().Length > 0)
                                {
                                    r.Email = rec.ItemArray[i].ToString();
                                    NotEmptyCount++;
                                }
                                else
                                {
                                    r.Email = null;
                                }
                                break;
                            case "Telefon":
                                if (rec.ItemArray[i].ToString().Length > 0)
                                {
                                    r.Telefon = rec.ItemArray[i].ToString();
                                    NotEmptyCount++;
                                }
                                else
                                {
                                    r.Telefon = null;
                                }
                                break;
                            case "Køn":
                                if (rec.ItemArray[i].ToString().Length > 0)
                                {
                                    r.Kon = rec.ItemArray[i].ToString().ToUpper();
                                    if ((r.Kon != "M") && (r.Kon != "K"))
                                    {
                                        r.Kon = null;
                                    }
                                    else
                                    {
                                        NotEmptyCount++;
                                    }
                                }
                                else
                                {
                                    r.Kon = null;
                                }
                                break;
                            case "Født":
                                try
                                {
                                    r.FodtDato = clsUtil.MSSerial2DateTime(Double.Parse(rec.ItemArray[i].ToString()));
                                    NotEmptyCount++;
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
                                    NotEmptyCount++;
                                }
                                catch
                                {
                                    r.erMedlem = null;
                                }
                                break;

                            default:
                                break;
                        }
                    }
                    if ((NotEmptyCount > 0) && (r.erMedlem == null))
                    {
                        this.Add(r);
                    }
                }

            }

        }
    }
}

