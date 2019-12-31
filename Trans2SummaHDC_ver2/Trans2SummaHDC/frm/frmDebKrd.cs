using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Uniconta.ClientTools.DataModel;
using Uniconta.Common;

namespace Trans2SummaHDC
{
    public partial class FrmDebKrd : Form
    {
        public FrmDebKrd()
        {
            InitializeComponent();
        }

        private void FrmDebKrd_Load(object sender, EventArgs e)
        {
            this.karKartotekBindingSource.DataSource = Program.karKartotek;
        }

        private void FrmDebKrd_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void karKartotekBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            var api = UCInitializer.GetBaseAPI;

            //var task = api.Query<CreditorClient>();
            //task.Wait();
            //var col1 = task.Result;

            foreach (var recKartotek in this.karKartotekBindingSource.DataSource as KarKartotek)
            {
                if (recKartotek.DK == "1") //Kreditor
                {
                    var crit = new List<PropValuePair>();
                    var pair = PropValuePair.GenereteWhereElements("Account", typeof(String), recKartotek.Kontonr.ToString());
                    crit.Add(pair);
                    var taskQueryCreditor = api.Query<CreditorClient>(crit);
                    taskQueryCreditor.Wait();
                    var col = taskQueryCreditor.Result;
                    if (col.Count() == 0)
                    {
                        CreditorClient recCreditor = new CreditorClient()
                        {
                            Account = recKartotek.Kontonr.ToString(),
                            Name = recKartotek.Kontonavn,
                            Address1 = recKartotek.Adresse,
                            ZipCode = recKartotek.Postnr,
                            City = recKartotek.Bynavn,
                            ContactEmail = recKartotek.Email,
                            CompanyRegNo = recKartotek.Cvrnr,
                            PaymentMethod = "Kreditors bankkonto",
                            //PaymentId = recKartotek.bankkonto,

                        };
                        var taskInsertCreditor = api.Insert(recCreditor);
                        taskInsertCreditor.Wait();
                        var err = taskInsertCreditor.Result;
                    }
                }
                else //Debitor
                {
                    var crit = new List<PropValuePair>();
                    var pair = PropValuePair.GenereteWhereElements("Account", typeof(String), recKartotek.Kontonr.ToString());
                    crit.Add(pair);
                    var taskQueryDebtor = api.Query<DebtorClient>(crit);
                    taskQueryDebtor.Wait();
                    var col = taskQueryDebtor.Result;
                    if (col.Count() == 0)
                    {
                        DebtorClient recDebtor = new DebtorClient()
                        {
                            Account = recKartotek.Kontonr.ToString(),
                            Name = recKartotek.Kontonavn,
                            Address1 = recKartotek.Adresse,
                            ZipCode = recKartotek.Postnr,
                            City = recKartotek.Bynavn,
                            ContactEmail = recKartotek.Email,
                            CompanyRegNo = recKartotek.Cvrnr,
                        };
                        var taskInsertDebtor = api.Insert(recDebtor);
                        taskInsertDebtor.Wait();
                        var err = taskInsertDebtor.Result;
                    }
                }
            }
        }

    }
}
