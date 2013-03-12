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

namespace menutest
{
    /// <summary>
    /// Interaction logic for menuButton.xaml
    /// </summary>
    public partial class menuButton : UserControl
    {
        public static RoutedEvent mnuClickEvent;

        public menuButton()
        {
            InitializeComponent();
        }

        static menuButton()
        {
            mnuClickEvent = EventManager.RegisterRoutedEvent("mnuClick", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(menuButton));
        }

        public event RoutedEventHandler mnuClick
        {
            add { AddHandler(mnuClickEvent, value); }
            remove { RemoveHandler(mnuClickEvent, value); }
        }

        protected virtual void OnmnuClick()
        {
            RoutedEventArgs args = new RoutedEventArgs(mnuClickEvent);
            RaiseEvent(args);
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

        private void buttonMenu_Click(object sender, RoutedEventArgs e)
        {
            OnmnuClick();
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
                case "freeBSD":
                    but.ledbutton.Fill = Brushes.Green;
                    but.buttonMenu.Background = Brushes.PaleGoldenrod;
                    break;
                default:
                    break;
            }
        }

    }
}
