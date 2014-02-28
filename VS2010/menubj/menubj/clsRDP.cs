using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.IO;

namespace nsMenu
{
    public class clsRDP
    {
        private string _rdpfile;
        public int desktopwidth { get; set; }
        public int desktopheight { get; set; }
        public string full_address { get; set; }
        public string winposstr { get; set; }

        private clsRDP() { }
        public clsRDP(string rdpfile)
        {
            _rdpfile = rdpfile;
            char[] kolon = { ':' };
            using (Stream file = File.OpenRead(_rdpfile))
            {
                using (StreamReader sr = new StreamReader(file))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        string[] part = line.Split(kolon);
                        switch (part[0].ToLower())
                        {
                            case "desktopwidth":
                                desktopwidth = int.Parse(part[2]);
                                break;
                            case "desktopheight":
                                desktopheight = int.Parse(part[2]);
                                break;
                            case "full address":
                                full_address = part[2].ToLower();
                                break;
                            case "winposstr":
                                winposstr = part[2].ToLower();
                                break;
                        }
                    }
                }
            }
        }

        public bool useFuulScreen
        {
            get
            {
                if (desktopwidth >= SystemParameters.PrimaryScreenWidth) return true;
                if (desktopheight >= SystemParameters.PrimaryScreenHeight) return true;
                if (SystemParameters.PrimaryScreenHeight < 1400) return true;
                if (SystemParameters.PrimaryScreenWidth < 2500) return true;
                return false;
            }
        }

        public void checkPosition()
        {
            char[] komma = { ',' };
            string[] part = winposstr.Split(komma);
            int x = int.Parse(part[2]);
            int y = int.Parse(part[3]);
            int dx = int.Parse(part[4]);
            int dy = int.Parse(part[5]);
            bool bUpdate = false;

            if ((x + desktopwidth) > (int)(SystemParameters.PrimaryScreenWidth * .95))
            {
                x = (int)(SystemParameters.PrimaryScreenWidth * .95) - desktopwidth;
                bUpdate = true;
            }
            if ((y + desktopheight) > (int)(SystemParameters.PrimaryScreenHeight * .95))
            {
                y = (int)(SystemParameters.PrimaryScreenHeight * .95) - desktopheight;
                bUpdate = true;
            }

            if (bUpdate == true)
            {
                winposstr = part[0] + "," + part[1] + "," + x + "," + y + "," + dx + "," + dy;
                update();
            }
        }

        private void update()
        {
            char[] kolon = { ':' };

            using (MemoryStream ms = new MemoryStream())
            {
                using (Stream file = File.OpenRead(_rdpfile))
                {
                    file.CopyTo(ms);
                }
                ms.Position = 0;
                using (Stream file = File.Create(_rdpfile))
                {
                    using (StreamWriter sw = new StreamWriter(file))
                    {
                        using (StreamReader sr = new StreamReader(ms))
                        {
                            while (!sr.EndOfStream)
                            {
                                string line = sr.ReadLine();
                                string[] part = line.Split(kolon);
                                switch (part[0].ToLower())
                                {
                                    case "desktopwidth":
                                        line = part[0] + ":" + part[1] + ":" + desktopwidth.ToString();
                                        break;
                                    case "desktopheight":
                                        line = part[0] + ":" + part[1] + ":" + desktopheight.ToString();
                                        break;
                                    case "full address":
                                        line = part[0] + ":" + part[1] + ":" + full_address;
                                        break;
                                    case "winposstr":
                                        line = part[0] + ":" + part[1] + ":" + winposstr;
                                        break;
                                }
                                sw.WriteLine(line);
                            }
                        }
                    }
                }
            }
        }
    }
}
