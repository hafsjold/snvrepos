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

namespace Trans2Summa3060
{
    public partial class FrmNyKontoplan : Form
    {
        public FrmNyKontoplan()
        {
            InitializeComponent();
        }

        private void frmNyKontoplan_Load(object sender, EventArgs e)
        {
            this.karNyKontoplanBindingSource.DataSource = Program.karNyKontoplan;
        }

        private void frmNyKontoplan_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.karNyKontoplan.save();
        }

        private void OpretNyeKonti_Click(object sender, EventArgs e)
        {
            var api = UCInitializer.GetBaseAPI;
            foreach (var recNyKontoplan in this.karNyKontoplanBindingSource.DataSource as KarNyKontoplan)
            {
                recKontoplan recKontoplan = null;
                if (recNyKontoplan.SkalOprettes)
                {
                    try
                    {
                        recKontoplan = (from x in Program.karKontoplan where x.Kontonr == recNyKontoplan.Kontonr select x).First();

                        var crit = new List<PropValuePair>();
                        var pair = PropValuePair.GenereteWhereElements("Account", typeof(String), recNyKontoplan.NytKontonr);
                        crit.Add(pair);
                        var taskQueryGLAccount = api.Query<GLAccountClient>(null, crit);
                        taskQueryGLAccount.Wait();
                        var col = taskQueryGLAccount.Result;
                        if (col.Count() == 0)
                        {

                            GLAccountClient recGLAccount = new GLAccountClient()
                            {
                                 Account = recNyKontoplan.NytKontonr,
                                 Name = recNyKontoplan.Kontonavn,
                            };
                            if (recKontoplan.Type == "Drift")
                            {
                                if (recKontoplan.DK == "1")
                                {
                                    recGLAccount.AccountTypeEnum = Uniconta.DataModel.GLAccountTypes.Revenue;
                                }
                                else
                                {
                                    recGLAccount.AccountTypeEnum = Uniconta.DataModel.GLAccountTypes.Expense;
                                }
                            }
                            else
                            {
                                if (recKontoplan.DK == "0")
                                {
                                    recGLAccount.AccountTypeEnum = Uniconta.DataModel.GLAccountTypes.Asset;
                                }
                                else
                                {
                                    recGLAccount.AccountTypeEnum = Uniconta.DataModel.GLAccountTypes.Liability;
                                }
                            }

                            var taskInsertGLAccount = api.Insert(recGLAccount);
                            taskInsertGLAccount.Wait();
                            var err = taskInsertGLAccount.Result;
                        }
                     }
                    catch
                    {
                        var ss = 1;
                    }
                }
            }
        }
    }
}
