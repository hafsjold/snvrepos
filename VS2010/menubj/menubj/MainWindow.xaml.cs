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
        public static string menuFolder;

        public MainWindow()
        {
            InitializeComponent();

            string newTitle = this.Title + " - " + SystemParameters.PrimaryScreenWidth + "x" + SystemParameters.PrimaryScreenHeight;
            this.Title = newTitle;
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
        }

        public static void CreateMissingFolders(DirectoryInfo di)
        {
            if (!di.Exists)
            {
                CreateMissingFolders(di.Parent);
                di.Create();
            }
        }

        private void win1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Properties.Settings.Default.WinStartupLocation = new System.Drawing.Point((int)this.Left, (int)this.Top);
            Properties.Settings.Default.Save();
        }

    }
}

