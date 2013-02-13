using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace bjArkiv
{
    [ComVisible(true)]  
    [Guid("CCD669C6-0163-4716-A5FE-F973D18440A3")]
    public interface IbjArkiv
    {
        int editbjArkiv(string name);
    }

    [ComVisible(true)]
    [Guid("F3231909-4408-4306-B3E9-6592777D83AC")]
    public class InterfaceImplementation : IbjArkiv
    {
        public int editbjArkiv(string name)
        {
            string messageBoxText = "edit bjArkiv file " + name;
            string caption = "bjArkiv";
            MessageBoxButtons button = MessageBoxButtons.OK;
            MessageBoxIcon icon = MessageBoxIcon.Warning;
            MessageBox.Show(messageBoxText, caption, button, icon);
            return 0;
        }

    }
}
