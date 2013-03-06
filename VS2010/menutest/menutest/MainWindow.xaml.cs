using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace menutest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [DllImport("user32.dll", SetLastError = true)]
        static extern void SwitchToThisWindow(IntPtr hWnd, bool turnOn);

        private string menuFolder;
        private Dictionary<string, Process> proclist = new Dictionary<string, Process>();

        public MainWindow()
        {
            InitializeComponent();

            if (Properties.Settings.Default.guidMenuFolder == new Guid("{00000000-0000-0000-0000-000000000000}"))
            {
                Properties.Settings.Default.guidMenuFolder = Guid.NewGuid();
                Properties.Settings.Default.Save();
            };
            menuFolder = System.IO.Path.Combine(System.IO.Path.GetTempPath(), Properties.Settings.Default.guidMenuFolder.ToString());
            DirectoryInfo di = new DirectoryInfo(menuFolder);
            try { CreateMissingFolders(di); }
            catch { }
        }

        public static void CreateMissingFolders(DirectoryInfo di)
        {
            if (!di.Exists)
            {
                CreateMissingFolders(di.Parent);
                di.Create();
            }
        }

        private void but_Click(object sender, RoutedEventArgs e)
        {


            string content = (sender as Button).Content.ToString();
            string rdpname = content + @".rdp";
            string rdpfile = System.IO.Path.Combine(menuFolder, rdpname);
            if (proclist.ContainsKey(rdpfile))
            {
                try
                {
                    if (proclist[rdpfile].HasExited)
                    {
                        proclist.Remove(rdpfile);
                    }
                    else
                    {
                        SwitchToThisWindow(proclist[rdpfile].MainWindowHandle, true);
                        return;
                    }
                }
                catch
                {
                    proclist.Remove(rdpfile);
                }
            }

            FileInfo fi = new FileInfo(rdpfile);
            if (!fi.Exists) getFile(rdpfile, content); 
 
            if ((Keyboard.Modifiers & ModifierKeys.Control) > 0)
            {
                var proc = Process.Start(@"c:\windows\system32\mstsc.exe", "/edit " + rdpfile);
                proclist.Add(rdpfile, proc);
            }
            else
            {
                Process proc = Process.Start(rdpfile);
                proclist.Add(rdpfile, proc);
            }



        }

        private void Group_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            string content = (sender as GroupBox).Name.ToString();
            string rdpname = content + @".rdp";
            string rdpfile = System.IO.Path.Combine(menuFolder, rdpname);
            if (proclist.ContainsKey(rdpfile))
            {
                try
                {
                    if (proclist[rdpfile].HasExited)
                    {
                        proclist.Remove(rdpfile);
                    }
                    else
                    {
                        SwitchToThisWindow(proclist[rdpfile].MainWindowHandle, true);
                        return;
                    }
                }
                catch
                {
                    proclist.Remove(rdpfile);
                }
            }

            FileInfo fi = new FileInfo(rdpfile);
            if (!fi.Exists) getFile(rdpfile, content);
            if ((Keyboard.Modifiers & ModifierKeys.Control) > 0)
            {
                var proc = Process.Start(@"c:\windows\system32\mstsc.exe", "/edit " + rdpfile);
                proclist.Add(rdpfile, proc);
            }
            else
            {
                Process proc = Process.Start(rdpfile);
                proclist.Add(rdpfile, proc);
            }

        }

        private void getFile(string rdpfile, string content) 
        {
            using (Stream file = File.Create(rdpfile))
            {
                using (StreamWriter sw = new StreamWriter(file))
                {
                    using (MemoryStream ms = new MemoryStream(menutest.rdp.Default))
                    {
                        ms.Position = 0;
                        using (StreamReader sr = new StreamReader(ms))
                        {
                            while (!sr.EndOfStream)
                            {
                                string line = sr.ReadLine();
                                char[] kolon = { ':' };
                                string[] part = line.Split(kolon);
                                if (part[0].ToLower() == "full address")
                                {
                                    char[] dot = { '.' };
                                    string[] doamin = part[2].Split(dot, 2);
                                    line = part[0] + ":" + part[1] + ":" + content + @"." + doamin[1];
                                }
                                sw.WriteLine(line);
                            }
                        }
                    }
                }
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {

        }

    }
}
