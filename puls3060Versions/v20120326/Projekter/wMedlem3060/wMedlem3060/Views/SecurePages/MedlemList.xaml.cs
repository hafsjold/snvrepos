using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Navigation;
using wMedlem3060.Web.Services;

namespace wMedlem3060.Views
{
    public partial class MedlemList : Page
    {
        public MedlemList()
        {
            InitializeComponent();
            //Loaded += new RoutedEventHandler(MedlemList_Loaded);
        }

        /*
        void MedlemList_Loaded(object sender, RoutedEventArgs e)
        {
            MedlemDomainContext ctx = new MedlemDomainContext();
            MedlemGrid.ItemsSource = ctx.tblMedlems;
            ctx.Load(ctx.GetTblMedlemsQuery());
        }
        */

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

        }

        private void SaveChangesButton_Click(object sender, RoutedEventArgs e)
        {
            MyData.SubmitChanges();
        }

        private void tblMedlemDomainDataSource_LoadedData(object sender, LoadedDataEventArgs e)
        {

            if (e.HasError)
            {
                System.Windows.MessageBox.Show(e.Error.ToString(), "Load Error", System.Windows.MessageBoxButton.OK);
                e.MarkErrorAsHandled();
            }
        }

    }
}
