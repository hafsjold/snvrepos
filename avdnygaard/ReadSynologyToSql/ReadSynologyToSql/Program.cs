using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadSynologyToSql
{
    class Program
    {
        static void Main(string[] args)
        {
            clsReadDrive objReadDrive = new clsReadDrive();
            //objReadDrive.load("Folder");
            //objReadDrive.load("File");
            objReadDrive.DviveSize();
        }
    }
}
