using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.IO;


namespace @namespace
{
    class Program
    {
        // Fields
        private const string XML_FILE_NAME = "TelerikSafeControl.xml";

        // Methods
        private static void Main(string[] args)
        {
            using (StreamWriter writer = new StreamWriter(XML_FILE_NAME))
            {
                ProcessDirectory(Directory.GetCurrentDirectory(), writer);
            }
            Console.Out.WriteLine("Successfully created telerik safecontrol's XML TelerikSafeControl.xml");
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
            if (currentDir.GetFiles().Length > 0)
            {
                //sw.WriteLine();
                //sw.WriteLine(".Set DestinationDir=" + ProcessDirectoryName(dirInfo, currentDir));
                foreach (FileInfo info3 in currentDir.GetFiles())
                {
                    if (info3.Name != "TelerikSafeControl.xml")
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

        private static void ProcessFilename(DirectoryInfo dirInfo, FileInfo file, StreamWriter sw)
        {
            if (file.Extension.ToLower() == ".dll")
            {
                List<string> namespaceList = new List<string>();
                try
                {
                    Assembly asm;
                    asm = Assembly.LoadFrom(file.FullName);
                    AssemblyName asmn = asm.GetName();

                    string strGAC = "<Assembly DeploymentTarget=\"GlobalAssemblyCache\" Location=\"" + file.Name + "\">";
                    sw.WriteLine(strGAC);
                    sw.WriteLine("<SafeControls>");

                    string strAssembly1 = "<SafeControl Assembly=\"" + asmn.Name + ", Version=" + asmn.Version + ", Culture=neutral, PublicKeyToken=" + ByteToHex(asmn.GetPublicKeyToken()) + "\" ";

                    foreach (Type type in asm.GetTypes())
                    {
                        try
                        {
                            string name = type.Namespace.ToString();
                            if (!namespaceList.Contains(name))
                                namespaceList.Add(type.Namespace.ToString());
                        }
                        catch {
                            //fejl
                        }

                    }
                    namespaceList.Sort();

                    foreach (string name in namespaceList)
                    {
                        string strAssembly2 = strAssembly1 + "Namespace=\"" + name + "\" TypeName=\"*\" Safe=\"True\" />";
                        sw.WriteLine(strAssembly2);
                    }
                    sw.WriteLine("</SafeControls>");
                    sw.WriteLine("<ClassResources>");
                    sw.WriteLine("<!-- ClassResource used by RadControls to wpresources -->");
                    sw.WriteLine("</ClassResources>");
                    sw.WriteLine("</Assembly>");
            
                }
                catch {
                    sw.WriteLine("FEJL");
                }
                string str = file.FullName.Replace(dirInfo.FullName, string.Empty);
                if (str.StartsWith(@"\"))
                {
                    str = str.Substring(1, str.Length - 1);
                }

            }
        }

    }
}
