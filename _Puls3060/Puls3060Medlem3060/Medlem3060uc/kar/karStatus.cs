using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Medlem3060uc
{
    public class recStatus
    {
        public recStatus() { }
        public string key { get; set; }
        public string value { get; set; }
    }

    public class KarStatus : List<recStatus>
    {
        public KarStatus()
        {
        }

        private void open()
        {
        }

        public void save()
        {

        }

    }
}
