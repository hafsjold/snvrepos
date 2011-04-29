using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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

        private void Copy2NyFakturaToolStripButton_Click(object sender, EventArgs e)
        {
            bool bVareforbrug = true;
            Tblactebisfaktura recActebisfaktura = tblactebisfakturaBindingSource.Current as Tblactebisfaktura;
            if (recActebisfaktura.Leveringsadresse.ToUpper().Contains("HAFSJOLD"))
                bVareforbrug = false;
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
                    Tekst = recActebisordre.Beskrivelse,
                    Konto = getVarenrKonto(recActebisordre.Varenr, bVareforbrug),
                    Momskode = KarKontoplan.getMomskode(getVarenrKonto(recActebisordre.Varenr, bVareforbrug))
                };
                recWfak.Tblwfaklin.Add(recWfaklin);
            }
            Program.dbDataTransSumma.Tblwfak.InsertOnSubmit(recWfak);
            Program.dbDataTransSumma.SubmitChanges();
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
    }
}
