using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Linq.SqlClient;

namespace Trans2Summa
{
    public partial class FrmActebisfaktura : Form
    {
        public FrmActebisfaktura()
        {
            InitializeComponent();
        }

        private void FrmActebisfaktura_Load(object sender, EventArgs e)
        {
            this.tblactebisfakturaBindingSource.DataSource = Program.dbDataTransSumma.tblactebisfakturas;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Sog();
        }

        private void Sog()
        {
            string strLike = "%" + FindTextBox.Text + "%";
            IEnumerable<tblactebisfaktura> qry;
            qry = (from u in Program.dbDataTransSumma.tblactebisordres
                   where SqlMethods.Like(u.beskrivelse, strLike)
                      || SqlMethods.Like(u.producent, strLike)
                      || SqlMethods.Like(u.serienr, strLike)
                   join b in Program.dbDataTransSumma.tblactebisfakturas on u.fakpid equals b.pid
                   orderby b.ordredato descending
                   select b).Distinct();

            this.tblactebisfakturaBindingSource.DataSource = qry;
        }

        private void Copy2NyFakturaToolStripButton_Click(object sender, EventArgs e)
        {
            bool bVareforbrug = true;
            tblactebisfaktura recActebisfaktura = tblactebisfakturaBindingSource.Current as tblactebisfaktura;
            if (recActebisfaktura.leveringsadresse.ToUpper().Contains("HAFSJOLD"))
            {
                DialogResult result = DotNetPerls.BetterDialog.ShowDialog(
                    "Trans2Summa", //titleString 
                    " JA: Dette er en Hafsjold Data ApS anskaffelse.\nNEJ: Dette et vareforbrug.", //bigString 
                    null, //smallString
                    "JA", //leftButton == OK
                    "NEJ", //rightButton == Cancel
                    global::Trans2Summa.Properties.Resources.Message_info); //iconSet

                if (result == DialogResult.OK)
                    bVareforbrug = false;
            }
            tblwfak recWfak = new tblwfak
            {
                sk = "K",
                dato = recActebisfaktura.ordredato,
                konto = 200064,
                kreditorbilagsnr = recActebisfaktura.fakturanr
            };
            foreach (tblactebisordre recActebisordre in recActebisfaktura.tblactebisordres)
            {
                tblwfaklin recWfaklin = new tblwfaklin
                {
                    antal = recActebisordre.antal,
                    enhed = "stk",
                    pris = recActebisordre.stkpris,
                    varenr = recActebisordre.varenr.ToString(),
                    nettobelob = recActebisordre.antal * recActebisordre.stkpris,
                    tekst = getVaretekst(recActebisfaktura, recActebisordre, bVareforbrug),
                    konto = getVarenrKonto(recActebisordre.varenr, bVareforbrug),
                    momskode = KarKontoplan.getMomskode(getVarenrKonto(recActebisordre.varenr, bVareforbrug))
                };
                recWfak.tblwfaklins.Add(recWfaklin);
            }

            FrmMain frmMain = this.ParentForm as FrmMain;
            try
            {
                FrmNyfaktura frmNyfaktura = frmMain.GetChild("Ny faktura") as FrmNyfaktura;
                frmNyfaktura.AddNyActebisFaktura(recWfak);
            }
            catch
            {

                Program.dbDataTransSumma.tblwfaks.InsertOnSubmit(recWfak);
                Program.dbDataTransSumma.SubmitChanges();
            }
        }

        private string getVaretekst(tblactebisfaktura recActebisfaktura, tblactebisordre recActebisordre, bool bVareforbrug)
        {
            string Tekst = recActebisordre.beskrivelse.Trim();
            if (!bOmkostningsVarenr(recActebisordre.varenr))
            {
                if (bVareforbrug)
                {
                    if ((recActebisfaktura.ordreref != null) && (recActebisfaktura.ordreref.Length > 0))
                        Tekst += ". Indkøbsordre: " + recActebisfaktura.ordreref.Trim();
                }
                if ((recActebisordre.sku != null) && (recActebisordre.sku.Length > 0))
                    Tekst += ". Producent varenr: " + recActebisordre.sku.Trim();
                if ((recActebisordre.serienr != null) && (recActebisordre.serienr.Length > 0))
                    Tekst += ". Serienr: " + recActebisordre.serienr.Trim();
                if ((recActebisordre.producent != null) && (recActebisordre.producent.Length > 0))
                    Tekst += ". Producent: " + recActebisordre.producent.Trim();
            }
            if (Tekst.Length > 512)
                return Tekst.Substring(0, 511);
            else
                return Tekst;
        }

        private int getVarenrKonto(int? varenr, bool bVareforbrug)
        {
            switch (varenr)
            {
                case 7000061:    //Fragt
                    return 6900;
                case 7000221:    //Minimumordregebyr
                    return 6900;
                case 7900154:    //Transportforsikring
                    return 6900;
                default:
                    if (bVareforbrug)
                        return 2100;
                    else
                        return 9100;
            }
        }

        private bool bOmkostningsVarenr(int? varenr)
        {
            switch (varenr)
            {
                case 7000061:    //Fragt
                    return true;
                case 7000221:    //Minimumordregebyr
                    return true;
                case 7900154:    //Transportforsikring
                    return true;
                default:
                    return false;
            }
        }


    }
}
