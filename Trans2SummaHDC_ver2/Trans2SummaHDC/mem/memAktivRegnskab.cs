using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;

namespace Trans2SummaHDC
{

    public class recActivRegnskab
    {
        private string m_rid;
        public int Rid
        {
            get
            {
                return int.Parse(m_rid);
            }
            set
            {
                m_rid = value.ToString();
                try
                {
                    Registry.CurrentUser.OpenSubKey(@"SOFTWARE\STONE'S SOFTWARE\SUMMAPRO\START", true).SetValue("SidsteMappe", m_rid);
                }
                catch (System.NullReferenceException)
                {
                }
            }
        }

        public recActivRegnskab()
        {
            try
            {
                m_rid = (string)Registry.CurrentUser.OpenSubKey(@"SOFTWARE\STONE'S SOFTWARE\SUMMAPRO\START").GetValue("SidsteMappe");
            }
            catch (System.NullReferenceException)
            {
                m_rid = "999";
            }
        }
    }

    public class MemAktivRegnskab : List<recActivRegnskab>
    {
        public MemAktivRegnskab()
        {
            this.Add(new recActivRegnskab());
        }

    }
}

