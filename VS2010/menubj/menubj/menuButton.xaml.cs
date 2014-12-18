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
using System.Runtime.InteropServices;
using System.IO;
using System.Timers;
using System.Windows.Threading;

namespace nsMenu
{
    /// <summary>
    /// Interaction logic for menuButton.xaml
    /// </summary>
    public partial class menuButton : UserControl
    {
        enum evt
        {
            mnuClick,
            editClick,
            edit2Click,
            fullscreenClick,
            smallScreen
        }

        [DllImport("user32.dll", SetLastError = true)]
        static extern void SwitchToThisWindow(IntPtr hWnd, bool turnOn);

        private DispatcherTimer dispatcherTimer = new DispatcherTimer();
        private Process proc = null;

        public menuButton()
        {
            InitializeComponent();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 5);
            dispatcherTimer.Start();
        }

        private void LayoutRoot_Loaded(object sender, RoutedEventArgs e)
        {
            char[] s = { ' ' };
            Process[] mstsc = Process.GetProcessesByName("mstsc");
            foreach (Process p in mstsc)
            {
                if (ServerName == p.MainWindowTitle.Split(s)[0])
                {
                    proc = p;
                    ledbutton.Visibility = System.Windows.Visibility.Visible;
                }
            }
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (proc == null)
            {
                ledbutton.Visibility = System.Windows.Visibility.Hidden;
            }
            else
            {
                if (proc.HasExited)
                {
                    ledbutton.Visibility = System.Windows.Visibility.Hidden;
                    proc = null;
                }
            }
        }

 
        public string ServerName
        {
            get { return (string)GetValue(ServerNameProperty); }
            set { SetValue(ServerNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ServerName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ServerNameProperty =
            DependencyProperty.Register("ServerName", typeof(string), typeof(menuButton),
                new UIPropertyMetadata(null, new PropertyChangedCallback(menuButton.ServerNamePropertyChanced)));


        private static void ServerNamePropertyChanced(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            menuButton but = (menuButton)d;
            if (!(but.ServerName == null))
            {
                but.buttonMenu.Content = but.ServerName;
                but.ledbutton.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        public string ServerOS
        {
            get { return (string)GetValue(ServerOSProperty); }
            set { SetValue(ServerOSProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ServerOS.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ServerOSProperty =
            DependencyProperty.Register("ServerOS", typeof(string), typeof(menuButton),
                new UIPropertyMetadata(null, new PropertyChangedCallback(menuButton.ServerOSPropertyChanced)));

        private static void ServerOSPropertyChanced(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            menuButton but = (menuButton)d;
            switch (but.ServerOS)
            {
                case "Win2012":
                    but.ledbutton.Fill = Brushes.Green;
                    but.buttonMenu.Background = Brushes.LightGreen;
                    break;
                case "Win2008":
                    but.ledbutton.Fill = Brushes.Green;
                    but.buttonMenu.Background = Brushes.LightBlue;
                    break;
                case "Win2003":
                    but.ledbutton.Fill = Brushes.Green;
                    but.buttonMenu.Background = Brushes.Red;
                    break;
                case "Win7":
                    but.ledbutton.Fill = Brushes.Green;
                    but.buttonMenu.Background = Brushes.Yellow;
                    break;
                case "Win8":
                    but.ledbutton.Fill = Brushes.Green;
                    but.buttonMenu.Background = Brushes.DodgerBlue;
                    break;
                case "freeBSD":
                    but.ledbutton.Fill = Brushes.Green;
                    but.buttonMenu.Background = Brushes.PaleGoldenrod;
                    break;
                default:
                    but.ledbutton.Fill = Brushes.Green;
                    but.buttonMenu.Background = Brushes.Brown;
                    break;
            }
        }

        private void buttonMenu_Click(object sender, RoutedEventArgs e)
        {
            server_Click(sender, e, evt.mnuClick);
        }

        private void Rediger_Click(object sender, RoutedEventArgs e)
        {
            server_Click(sender, e, evt.editClick);
        }

        private void RedigerNotepadpp_Click(object sender, RoutedEventArgs e)
        {
            server_Click(sender, e, evt.edit2Click);
        }

        private void FullScreen_Click(object sender, RoutedEventArgs e)
        {
            server_Click(sender, e, evt.fullscreenClick);
        }

        private void SmallScreen_Click(object sender, RoutedEventArgs e)
        {
            server_Click(sender, e, evt.smallScreen);
        }

        private void server_Click(object sender, RoutedEventArgs e, evt Evt)
        {
            string rdpname = ServerName + @".rdp";
            string rdpfile = System.IO.Path.Combine(menubj.MainWindow.menuFolder, rdpname);
            
            if (proc != null)
            {
                try
                {
                    if (proc.HasExited)
                    {
                        proc = null;
                        ledbutton.Visibility = System.Windows.Visibility.Hidden;
                    }
                    else
                    {
                        SwitchToThisWindow(proc.MainWindowHandle, true);
                        return;
                    }
                }
                catch
                {
                    proc = null;
                    ledbutton.Visibility = System.Windows.Visibility.Hidden;
                }
            }

            FileInfo fi = new FileInfo(rdpfile);
            if (!fi.Exists) getFile(rdpfile, ServerName);
            clsRDP rdp = new clsRDP(rdpfile);

            switch (Evt)
            {
                case evt.editClick:
                    proc = Process.Start(@"c:\windows\system32\mstsc.exe", "/edit " + rdpfile);
                    ledbutton.Visibility = System.Windows.Visibility.Visible;
                    break;

                case evt.edit2Click:
                    try
                    {
                        Process.Start(@"C:\Program Files\Notepad++\notepad++.exe", rdpfile);
                    }
                    catch
                    {
                        Process.Start(@"C:\Program Files (x86)\Notepad++\notepad++.exe", rdpfile);
                    }
                    break;

                case evt.fullscreenClick:
                    proc = Process.Start(@"c:\windows\system32\mstsc.exe", "/f " + rdpfile);
                    ledbutton.Visibility = System.Windows.Visibility.Visible;
                    break;

                case evt.smallScreen:
                    proc = Process.Start(@"c:\windows\system32\mstsc.exe", "/w:1024 /h:768 " + rdpfile);
                    ledbutton.Visibility = System.Windows.Visibility.Visible;
                    break;

                case evt.mnuClick:
                    rdp.checkPosition();
                    if (rdp.useFuulScreen) proc = Process.Start(@"c:\windows\system32\mstsc.exe", "/f " + rdpfile);
                    else proc = Process.Start(rdpfile);
                    ledbutton.Visibility = System.Windows.Visibility.Visible;
                    break;
                
                default:
                    break;

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
