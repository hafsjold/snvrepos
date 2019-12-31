using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Data;
using Excel;
using Trans2SummaHDC;

namespace Trans2SummaHDC
{
    public class clsImportFaktura 
    {
        private string m_kartotek_dat;

        public clsImportFaktura()
        {
            var rec_regnskab = Program.qryAktivRegnskab();

            System.Windows.Forms.OpenFileDialog fd = new System.Windows.Forms.OpenFileDialog();
            fd.Title = "Vælg Import Faktura Excel file";
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
            excelReader.IsFirstRowAsColumnNames = false;

            tblwfak recwFak = new tblwfak
            {
                sk = "K",
                dato = DateTime.Today,
                konto = 100001,
            };
            
            while (excelReader.Read())
            {
                tblwfaklin recwfaklin = new tblwfaklin
                {
                    varenr = "",
                    tekst = excelReader.GetString(0),
                    konto = 9100,
                    momskode = "K25",
                    antal =1,
                    enhed = "stk",
                    pris = excelReader.GetDecimal(1),
                    rabat = 0,
                    moms = excelReader.GetDecimal(1)/4,
                    nettobelob = excelReader.GetDecimal(1),
                    bruttobelob = excelReader.GetDecimal(1) + excelReader.GetDecimal(1)/4,              
                };
                recwFak.tblwfaklins.Add(recwfaklin);
            } 
            excelReader.Close();

            Program.dbDataTransSumma.tblwfaks.InsertOnSubmit(recwFak);
            Program.dbDataTransSumma.SubmitChanges();

        }
 
    }
}

