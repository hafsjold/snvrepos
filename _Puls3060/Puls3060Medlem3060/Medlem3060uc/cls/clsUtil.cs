using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualBasic;
using System.Threading;
using System.Globalization;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Collections;
using System.Text.RegularExpressions;

namespace Medlem3060uc
{
    public class ColumnSorter : IComparer
    {
        private int m_CurrentColumn;
        private SortOrder m_CurrentSortOrder;
        public int CurrentColumn
        {
            get
            {
                return m_CurrentColumn;
            }
            set
            {
                if (m_CurrentColumn == value)
                {
                    switch (m_CurrentSortOrder)
                    {
                        case SortOrder.Ascending:
                            m_CurrentSortOrder = SortOrder.Descending;
                            break;
                        case SortOrder.Descending:
                            m_CurrentSortOrder = SortOrder.Ascending;
                            break;
                        case SortOrder.None:
                            m_CurrentSortOrder = SortOrder.Ascending;
                            break;
                        default:
                            m_CurrentSortOrder = SortOrder.Ascending;
                            break;
                    }
                }
                else m_CurrentSortOrder = SortOrder.Ascending;

                m_CurrentColumn = value;
            }
        }

        public int Compare(object x, object y)
        {
            ListViewItem rowA = (ListViewItem)x;
            ListViewItem rowB = (ListViewItem)y;
            try
            {
                string sA = rowA.SubItems[m_CurrentColumn].Text;
                string sB = rowB.SubItems[m_CurrentColumn].Text;
                if (m_CurrentColumn == 1)
                {
                    sA = lpad(sA, 5, ' ');
                    sB = lpad(sB, 5, ' ');
                }
                if (m_CurrentSortOrder == SortOrder.Ascending)
                {
                    return String.Compare(sA, sB);
                }
                else
                {
                    return String.Compare(sB, sA);
                }

            }
            catch (ArgumentOutOfRangeException)
            {

                return 0;
            }
        }

        public ColumnSorter()
        {
            m_CurrentColumn = 0;
            m_CurrentSortOrder = SortOrder.Descending;

        }

        public string lpad(Object oVal, int Length, char PadChar)
        {
            string Val = oVal.ToString();
            return Val.PadLeft(Length, PadChar);
        }

    }

    public class Fieldattr : Attribute
    {
        public string Heading { get; set; }
    }

    class ExcelUILanguageHelper : IDisposable
    {
        private CultureInfo m_CurrentCulture;

        public ExcelUILanguageHelper()
        {
            // save current culture and set culture to en-US 
            m_CurrentCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
        }

        #region IDisposable Members

        public void Dispose()
        {
            // return to normal culture 
            Thread.CurrentThread.CurrentCulture = m_CurrentCulture;
        }

        #endregion
    }

    public static class clsUtil
    {
        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        public static bool IsProcessOpen(string name)
        {
            foreach (Process p in Process.GetProcesses())
            {
                if (p.ProcessName.Contains(name)) return true;
            }
            return false;
        }
    }
}
