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
using System.Windows.Controls.Primitives;
using System.Timers;
using System.Windows.Threading;
using nsMenu;

namespace menutest
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        enum evt
        {
            server_mnuClick,
            server_editClick,
            server_edit2Click,
            server_fullscreenClick
        }

        [DllImport("user32.dll", SetLastError = true)]
        static extern void SwitchToThisWindow(IntPtr hWnd, bool turnOn);

        public DispatcherTimer dispatcherTimer = new DispatcherTimer();

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

            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 5);
            dispatcherTimer.Start();

            char[] s = { ' ' };
            Process[] mstsc = Process.GetProcessesByName("mstsc");
            foreach (Process p in mstsc)
            {
                string server = p.MainWindowTitle.Split(s)[0];
                string rdpname = server + @".rdp";
                string rdpfile = System.IO.Path.Combine(menuFolder, rdpname);
                lock (proclist)
                {
                    if (!proclist.ContainsKey(rdpfile))
                        proclist.Add(rdpfile, p);
                }
            }
            foreach (object o in LogicalTreeHelper.GetChildren(win1))
            {
                RecurseChildren((DependencyObject)o);
            }
        }

        public static void CreateMissingFolders(DirectoryInfo di)
        {
            if (!di.Exists)
            {
                CreateMissingFolders(di.Parent);
                di.Create();
            }
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            foreach (object o in LogicalTreeHelper.GetChildren(win1))
            {
                RecurseChildren((DependencyObject)o);
            }
        }

        private void RecurseChildren(DependencyObject current)
        {
            foreach (object o in LogicalTreeHelper.GetChildren(current))
            {
                menuButton uc = o as menuButton;
                if (uc == null)
                {
                    if (o is DependencyObject)
                        RecurseChildren((DependencyObject)o);
                    continue;
                }

                string server = uc.ServerName;
                string rdpname = server + @".rdp";
                string rdpfile = System.IO.Path.Combine(menuFolder, rdpname);
                lock (proclist)
                {
                    if (proclist.ContainsKey(rdpfile))
                    {
                        try
                        {
                            if (proclist[rdpfile].HasExited)
                            {
                                proclist.Remove(rdpfile);
                                uc.ledbutton.Visibility = System.Windows.Visibility.Hidden;
                            }
                            else
                            {
                                uc.ledbutton.Visibility = System.Windows.Visibility.Visible;
                            }
                        }
                        catch
                        {
                            proclist.Remove(rdpfile);
                            uc.ledbutton.Visibility = System.Windows.Visibility.Hidden;
                        }
                    }
                    else
                        uc.ledbutton.Visibility = System.Windows.Visibility.Hidden;
                }
            }
        }


        private void server_mnuClick(object sender, RoutedEventArgs e)
        {
            server_Click(sender, e, evt.server_mnuClick);
        }

        private void server_editClick(object sender, RoutedEventArgs e)
        {
            server_Click(sender, e, evt.server_editClick);
        }

        private void server_edit2Click(object sender, RoutedEventArgs e)
        {
            server_Click(sender, e, evt.server_edit2Click);
        }

        private void server_fullscreenClick(object sender, RoutedEventArgs e)
        {
            server_Click(sender, e, evt.server_fullscreenClick);
        }

        private void server_Click(object sender, RoutedEventArgs e, evt Evt)
        {
            menuButton mnuButton = sender as menuButton;
            string server = mnuButton.ServerName;
            string rdpname = server + @".rdp";
            string rdpfile = System.IO.Path.Combine(menuFolder, rdpname);
            lock (proclist)
            {
                if (proclist.ContainsKey(rdpfile))
                {
                    try
                    {
                        if (proclist[rdpfile].HasExited)
                        {
                            proclist.Remove(rdpfile);
                            mnuButton.ledbutton.Visibility = System.Windows.Visibility.Hidden;
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
                        mnuButton.ledbutton.Visibility = System.Windows.Visibility.Hidden;
                    }
                }

                FileInfo fi = new FileInfo(rdpfile);
                if (!fi.Exists) getFile(rdpfile, server);

                switch (Evt)
                {
                    case evt.server_editClick:
                        var proc = Process.Start(@"c:\windows\system32\mstsc.exe", "/edit " + rdpfile);
                        proclist.Add(rdpfile, proc);
                        mnuButton.ledbutton.Visibility = System.Windows.Visibility.Visible;
                        break;

                    case evt.server_edit2Click:
                        try
                        {
                            Process.Start(@"C:\Program Files\Notepad++\notepad++.exe", rdpfile);
                        }
                        catch
                        {
                            Process.Start(@"C:\Program Files (x86)\Notepad++\notepad++.exe", rdpfile);
                        } 
                        break;

                    case evt.server_fullscreenClick:
                        proc = Process.Start(@"c:\windows\system32\mstsc.exe", "/f " + rdpfile);
                        proclist.Add(rdpfile, proc);
                        mnuButton.ledbutton.Visibility = System.Windows.Visibility.Visible;
                        break;

                    case evt.server_mnuClick:
                        proc = Process.Start(rdpfile);
                        proclist.Add(rdpfile, proc);
                        mnuButton.ledbutton.Visibility = System.Windows.Visibility.Visible;
                        break;
                    default:
                        break;
                }
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

    }
}
