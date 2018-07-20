﻿using System;
using System.ServiceProcess;
using System.Linq;
using Pbs3060;

namespace MedlemServicePuls3060
{
    class Program
    {
        static void Main(string[] args)
        {
            Uniconta.ClientTools.Localization.SetLocalizationStrings(System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName);
            Uniconta.WindowsAPI.Startup.OnLoad();
            UCInitializer.InitUniconta();

            System.ServiceProcess.ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] { new mcMedlem3060Service() };
            ServiceBase.Run(ServicesToRun);
        }

    }
}