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

namespace menubj
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [DllImport("user32.dll", SetLastError = true)]
        static extern void SwitchToThisWindow(IntPtr hWnd, bool turnOn);

        public DispatcherTimer dispatcherTimer = new DispatcherTimer();

        private string menuFolder;
        private Dictionary<string, Process> proclist = new Dictionary<string, Process>();

        public MainWindow()
        {
            InitializeComponent();
            this.Left = Properties.Settings.Default.WinStartupLocation.X;
            this.Top = Properties.Settings.Default.WinStartupLocation.Y;

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

        private void win1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Properties.Settings.Default.WinStartupLocation = new System.Drawing.Point((int)this.Left, (int)this.Top);
            Properties.Settings.Default.Save();
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

                if ((Keyboard.Modifiers & ModifierKeys.Control) > 0)
                {
                    var proc = Process.Start(@"c:\windows\system32\mstsc.exe", "/edit " + rdpfile);
                    proclist.Add(rdpfile, proc);
                    mnuButton.ledbutton.Visibility = System.Windows.Visibility.Visible;
                }
                else if ((Keyboard.Modifiers & ModifierKeys.Shift) > 0)
                {
                    try
                    {
                        var proc = Process.Start(@"C:\Program Files\Notepad++\notepad++.exe", rdpfile);
                    }
                    catch
                    {
                        var proc = Process.Start(@"C:\Program Files (x86)\Notepad++\notepad++.exe", rdpfile);
                    }
                }
                else
                {
                    Process proc = Process.Start(rdpfile);
                    proclist.Add(rdpfile, proc);
                    mnuButton.ledbutton.Visibility = System.Windows.Visibility.Visible;
                }
            }
        }

        private void getFile(string rdpfile, string content)
        {
            using (Stream file = File.Create(rdpfile))
            {
                using (StreamWriter sw = new StreamWriter(file))
                {
                    using (MemoryStream ms = new MemoryStream(menubj.rdp.Default))
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

