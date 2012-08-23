using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Linq.SqlClient;

namespace nsPuls3060
{
    public partial class FrmActebisfaktura : Form
    {
        public FrmActebisfaktura()
        {
            InitializeComponent();
        }

        private void FrmActebisfaktura_Load(object sender, EventArgs e)
        {
            this.tblactebisfakturaBindingSource.DataSource = Program.dbDataTransSumma.Tblactebisfaktura;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Sog();
        }        
        
        private void Sog()
        {
            string strLike = "%" + FindTextBox.Text + "%";
            IEnumerable<Tblactebisfaktura> qry;
            qry = (from u in Program.dbDataTransSumma.Tblactebisordre
                   where SqlMethods.Like(u.Beskrivelse, strLike)
                      || SqlMethods.Like(u.Producent, strLike)
                      || SqlMethods.Like(u.Serienr, strLike)
                   join b in Program.dbDataTransSumma.Tblactebisfaktura on u.Fakpid equals b.Pid
                   orderby b.Ordredato descending
                   select b).Distinct();

            this.tblactebisfakturaBindingSource.DataSource = qry;
        }

        private void Copy2NyFakturaToolStripButton_Click(object sender, EventArgs e)
        {
            bool bVareforbrug = true;
            Tblactebisfaktura recActebisfaktura = tblactebisfakturaBindingSource.Current as Tblactebisfaktura;
            if (recActebisfaktura.Leveringsadresse.ToUpper().Contains("HAFSJOLD"))
            {
                DialogResult result = DotNetPerls.BetterDialog.ShowDialog(
                    "TransSumma", //titleString 
                    " JA: Dette er en Hafsjold Data ApS anskaffelse.\nNEJ: Dette et vareforbrug.", //bigString 
                    null, //smallString
                    "JA", //leftButton == OK
                    "NEJ", //rightButton == Cancel
                    global::nsPuls3060.Properties.Resources.Message_info); //iconSet

                if (result == DialogResult.OK)
                    bVareforbrug = false;
            }
            Tblwfak recWfak = new Tblwfak
            {
                Sk = "K",
                Dato = recActebisfaktura.Ordredato,
                Konto = 200064,
                Kreditorbilagsnr = recActebisfaktura.Fakturanr
            };
            foreach (Tblactebisordre recActebisordre in recActebisfaktura.Tblactebisordre)
            {
                Tblwfaklin recWfaklin = new Tblwfaklin
                {
                    Antal = recActebisordre.Antal,
                    Enhed = "stk",
                    Pris = recActebisordre.Stkpris,
                    Varenr = recActebisordre.Varenr.ToString(),
                    Nettobelob = recActebisordre.Antal * recActebisordre.Stkpris,
                    Tekst = getVaretekst(recActebisfaktura, recActebisordre, bVareforbrug),
                    Konto = getVarenrKonto(recActebisordre.Varenr, bVareforbrug),
                    Momskode = KarKontoplan.getMomskode(getVarenrKonto(recActebisordre.Varenr, bVareforbrug))
                };
                recWfak.Tblwfaklin.Add(recWfaklin);
            }

            FrmMain frmMain = this.ParentForm as FrmMain;
            try
            {
                FrmNyfaktura frmNyfaktura = frmMain.GetChild("Ny faktura") as FrmNyfaktura;
                frmNyfaktura.AddNyActebisFaktura(recWfak);
            }
            catch
            {

                Program.dbDataTransSumma.Tblwfak.InsertOnSubmit(recWfak);
                Program.dbDataTransSumma.SubmitChanges();
            }
        }

        private string getVaretekst(Tblactebisfaktura recActebisfaktura, Tblactebisordre recActebisordre, bool bVareforbrug)
        {
            string Tekst = recActebisordre.Beskrivelse.Trim();
            if (!bOmkostningsVarenr(recActebisordre.Varenr))
            {
                if (bVareforbrug)
                {
                    if ((recActebisfaktura.Ordreref != null) && (recActebisfaktura.Ordreref.Length > 0))
                        Tekst += ". Indkøbsordre: " + recActebisfaktura.Ordreref.Trim();
                }
                if ((recActebisordre.Sku != null) && (recActebisordre.Sku.Length > 0))
                    Tekst += ". Producent varenr: " + recActebisordre.Sku.Trim();
                if ((recActebisordre.Serienr != null) && (recActebisordre.Serienr.Length > 0))
                    Tekst += ". Serienr: " + recActebisordre.Serienr.Trim();
                if ((recActebisordre.Producent != null) && (recActebisordre.Producent.Length > 0))
                    Tekst += ". Producent: " + recActebisordre.Producent.Trim(); 
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
