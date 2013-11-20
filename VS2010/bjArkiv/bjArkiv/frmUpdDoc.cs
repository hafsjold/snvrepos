﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace bjArkiv
{
    public partial class frmUpdDoc : Form
    {
        public clsArkiv arkiv { get; set; }
        public xmldoc startrec { get; set; }

        public frmUpdDoc()
        {
            InitializeComponent();
        }

        private void frmUpdDoc_Load(object sender, EventArgs e)
        {
            xmldocsBindingSource.DataSource = arkiv.docdb;
            labelPath.Text = Path.GetDirectoryName(Path.GetDirectoryName(arkiv.docdb.path));
            try
            {
                int start = ((xmldocs)xmldocsBindingSource.DataSource).IndexOf(startrec);
                xmldocsBindingSource.CurrencyManager.Position = start;
            }
            catch {  }
            dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
        }

        private void frmUpdDoc_FormClosing(object sender, FormClosingEventArgs e)
        {
            arkiv.docdb.Save();
            Program.objUpdDoc = null;
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            arkiv.docdb.Save();
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            var source = new AutoCompleteStringCollection();
            String[] stringArray = { };
 
            switch (dataGridView1.CurrentCell.OwningColumn.Name)
            {
                case "colVirksomhed":
                    stringArray = (from x in arkiv.docdb where x.virksomhed != null && x.virksomhed != "" select x.virksomhed).ToArray();
                    break;
                case "colEmne":
                    stringArray = (from x in arkiv.docdb where x.emne != null && x.emne != "" select x.emne).ToArray();
                    break;
                case "colDokumenttype":
                    stringArray = (from x in arkiv.docdb where x.dokument_type != null && x.dokument_type != "" select x.dokument_type).ToArray();
                    break;
                case "colÅr":
                    stringArray = (from x in arkiv.docdb where x.år != null && x.år != 0 select x.år.ToString()).ToArray();
                    break;
                case "colEksternkilde":
                    stringArray = (from x in arkiv.docdb where x.ekstern_kilde != null && x.ekstern_kilde != "" select x.ekstern_kilde).ToArray();
                    break;
                case "colBeskrivelse":
                    stringArray = (from x in arkiv.docdb where x.beskrivelse != null && x.beskrivelse != "" select x.beskrivelse).ToArray();
                    break;
                default:
                    break;
            }
            source.AddRange(stringArray);

            TextBox prodCode = e.Control as TextBox;
            if (prodCode != null)
            {
                prodCode.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                prodCode.AutoCompleteCustomSource = source;
                prodCode.AutoCompleteSource = AutoCompleteSource.CustomSource;

            }
        }

        private void splitContainer1_SizeChanged(object sender, EventArgs e)
        {
            splitContainer1.SplitterDistance = 25;
        }

        private void splitContainer2_SizeChanged(object sender, EventArgs e)
        {
            splitContainer2.SplitterDistance= splitContainer2.Size.Height - 25;
        }
    }
}