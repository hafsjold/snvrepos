using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;

namespace nsPuls3060
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
                Registry.CurrentUser.OpenSubKey(@"SOFTWARE\STONE'S SOFTWARE\SUMMAPRO\START", true).SetValue("SidsteMappe", m_rid);

            }
        }

        public recActivRegnskab()
        {
            m_rid = (string)Registry.CurrentUser.OpenSubKey(@"SOFTWARE\STONE'S SOFTWARE\SUMMAPRO\START").GetValue("SidsteMappe");
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

