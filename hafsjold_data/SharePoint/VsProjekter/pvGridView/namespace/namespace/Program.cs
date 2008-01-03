using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections.Generic;
using System.IO;


namespace @namespace
{
    class Program
    {
        // Fields
        private const string DDF_FILE_NAME = "TelerikSkins.ddf";

        // Methods
        private static void Main(string[] args)
        {
            using (StreamWriter writer = new StreamWriter("TelerikSkins.ddf"))
            {
                writer.WriteLine(".OPTION Explicit");
                writer.WriteLine(".Set DiskDirectoryTemplate=CDROM");
                writer.WriteLine(".Set CompressionType=MSZIP");
                writer.WriteLine(".Set UniqueFiles=Off");
                writer.WriteLine(".Set Cabinet=On");
                writer.WriteLine(";**************************************************");
                writer.WriteLine();
                ProcessDirectory(Directory.GetCurrentDirectory(), writer);
            }
            Console.Out.WriteLine("Successfully created telerik skin's DDF file:TelerikSkins.ddf");
        }

        static void Main1(string[] args)
        {
            List<string> namespaceList = new List<string>();
            Assembly asm;
            asm = Assembly.LoadFrom("C:\\_Udvikl\\Telerik.RadControls\\Telerik.RadControls\\RadMenu.Net2.dll");
            AssemblyName asmn = asm.GetName();
            string strAssembly1 = "<SafeControl Assembly=\"" + asmn.Name + ", Version=" + asmn.Version + ", Culture=neutral, PublicKeyToken=" + ByteToHex(asmn.GetPublicKeyToken()) + " \"";
            //Console.WriteLine(strAssembly);

            foreach (Type type in asm.GetTypes())
            {
                string name = type.Namespace.ToString();
                if (!namespaceList.Contains(name))
                    namespaceList.Add(type.Namespace.ToString());
            }
            namespaceList.Sort();

            foreach (string name in namespaceList)
            {
                string strAssembly2 = strAssembly1 + "Namespace=\"" + name + "\" TypeName=\"*\" Safe=\"True\" />";
                Console.WriteLine(strAssembly2);
            }
        }       
        
        // Converts a byte array to a hexadecimal string.
        static string ByteToHex(byte[] byteArray)
        {
            string outString = "";
            foreach (Byte b in byteArray)
                outString += b.ToString("X2");

            return outString.ToLower();
        }

        private static void ProcessDirectory(string path, StreamWriter sw)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(Directory.GetCurrentDirectory());
            DirectoryInfo currentDir = new DirectoryInfo(path);
            if ((currentDir.GetFiles().Length > 0) && (dirInfo.FullName != currentDir.FullName))
            {
                sw.WriteLine();
                sw.WriteLine(".Set DestinationDir=" + ProcessDirectoryName(dirInfo, currentDir));
                foreach (FileInfo info3 in currentDir.GetFiles())
                {
                    if (info3.Name != "TelerikSkins.ddf")
                    {
                        ProcessFilename(dirInfo, info3, sw);
                    }
                }
            }
            if (dirInfo.GetDirectories().Length > 0)
            {
                foreach (DirectoryInfo info4 in currentDir.GetDirectories())
                {
                    ProcessDirectory(info4.FullName, sw);
                }
            }
        }

        private static string ProcessDirectoryName(DirectoryInfo dirInfo, DirectoryInfo currentDir)
        {
            string str = dirInfo.Name + currentDir.FullName.Replace(dirInfo.FullName, string.Empty);
            if (str.EndsWith(@"\"))
            {
                str = str.Substring(0, str.Length - 1);
            }
            return str;
        }

        private static string ProcessFilename(DirectoryInfo dirInfo, FileInfo file, StreamWriter sw)
        {
            string str = file.FullName.Replace(dirInfo.FullName, string.Empty);
            if (str.StartsWith(@"\"))
            {
                str = str.Substring(1, str.Length - 1);
            }
            return (@"RadControls\" + str);
        }

    }
}
