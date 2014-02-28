using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace nsMenu
{
    public class clsRDP
    {
        public int desktopwidth { get; set; }
        public int desktopheight { get; set; }
        public string full_address { get; set; }

        public Boolean useFuulScreen() 
        {
            if (desktopwidth >= SystemParameters.PrimaryScreenWidth) return true;
            if (desktopheight >= SystemParameters.PrimaryScreenHeight) return true;
            if (SystemParameters.PrimaryScreenHeight < 1400) return true;
            if (SystemParameters.PrimaryScreenWidth < 2500) return true;
            return false;
        }
    }
}
