using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nsPbs3060
{


    [Serializable]
    public class clsPipeData
    {
        public enum command
        {
            ProcessAppData,
            ResponseAppData,
            ProcessStatusData,
            ResponseStatusData
        }
        public override string ToString() { return string.Format("\"{0}\" (message ID = {1})", message, Id); }
        public int Id { get; set; }
        public command cmd { get; set; }
        public string message { get; set; }
        public clsAppData AppData { get; set; }
        public clsStatusData StatusData { get; set; }

    }
}
