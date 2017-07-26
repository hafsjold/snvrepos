using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nsPbs3060
{
    [Serializable]
    public class clsStatusData
    {
        public bool bUniConta_Online { get; set; }
        public bool bGigaHostImap_Online { get; set; }
        public bool bdbPuls3060Medlem_Online { get; set; }
        public bool bpuls3060_dk_Online { get; set; }
        public bool bSFTP_Online { get; set; }

    }
}
