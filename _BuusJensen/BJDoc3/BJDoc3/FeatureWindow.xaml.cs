using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.Entity;

namespace BJDoc3
{
    /// <summary>
    /// Interaction logic for FeatureWindowold.xaml
    /// </summary>
    public partial class FeatureWindowold : Window
    {
        public FeatureWindowold()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            System.Windows.Data.CollectionViewSource tblFeaturesViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("tblFeaturesViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            tblFeaturesViewSource.Source = App.db.tblFeatures.Local;

            System.Windows.Data.CollectionViewSource tblFeatureTypesViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("tblFeatureTypesViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            tblFeatureTypesViewSource.Source = App.db.tblFeatureTypes.Local;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem item = sender as MenuItem;
            this.Title = "Info: " + item.Header;
        }
    }
}
