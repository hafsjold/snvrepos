using System;
using System.ServiceProcess;
using System.Linq;

namespace MedlemServicePuls3060
{
    class Program
    {
        static void Main(string[] args)
        {
            System.ServiceProcess.ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] { new mcMedlem3060Service() };
            ServiceBase.Run(ServicesToRun);
        }

    }
}
