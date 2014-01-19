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
using wMedlem3060.Web;
using System.ServiceModel.DomainServices.Client;

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

        void Test()
        {
            MedlemDomainContext ctx = MyvLogData.DomainContext as MedlemDomainContext;
            var qry = from r in ctx.vMedlemLogs select r;
            int cnt = qry.Count();
        }

        void Test2()
        {
            MedlemDomainContext ctx = MyvLogData.DomainContext as MedlemDomainContext;
            string keyname = "tblMedlemlog";
            object myState = new object();
            var key = ctx.Getnextval(keyname, Getnextval_Completed, myState);
        }

        private void Getnextval_Completed(InvokeOperation<int?> temp)
        {
            MedlemDomainContext ctx = MyvLogData.DomainContext as MedlemDomainContext;
            int next_id = (int)temp.Value;
            tblMedlemLog rec = new tblMedlemLog
            {
                id = next_id,
                logdato = DateTime.Today,
                Nr = 5,
                akt_id = 10,
                akt_dato = DateTime.Today,
            };
            ctx.tblMedlemLogs.Add(rec);
            ctx.SubmitChanges();
            MyData.SubmitChanges();
        }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

        }

        private void SaveChangesButton_Click(object sender, RoutedEventArgs e)
        {
            //Test2(); 
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

        private void MyData_LoadingData(object sender, LoadingDataEventArgs e)
        {
            e.Cancel = !WebContext.Current.User.IsAuthenticated;
        }

        private void vMedlemLogDomainDataSource_LoadedData(object sender, LoadedDataEventArgs e)
        {

            if (e.HasError)
            {
                System.Windows.MessageBox.Show(e.Error.ToString(), "Load Error", System.Windows.MessageBoxButton.OK);
                e.MarkErrorAsHandled();
            }
        }

        private void MyvLogData_LoadingData(object sender, LoadingDataEventArgs e)
        {
            e.Cancel = !WebContext.Current.User.IsAuthenticated;
        }

        private void MedlemDetails_AddingNewItem(object sender, DataFormAddingNewItemEventArgs e)
        {
            int x = 1;
        }

        private void MedlemDetails_BeginningEdit(object sender, System.ComponentModel.CancelEventArgs e)
        {
            int x = 1;
        }

        private void MedlemDetails_EditEnding(object sender, DataFormEditEndingEventArgs e)
        {
            DataForm objMedlemDetails = sender as DataForm; 

            int x = 1;
 
        }

        private void MedlemGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            tblMedlem m = e.Row.DataContext as tblMedlem;
            if (m != null && m.Nr == 5)
            {
                e.Row.Background = new SolidColorBrush(Colors.Yellow);
                e.Row.Foreground = new SolidColorBrush(Colors.Green);
            }
            else 
            {
                e.Row.Background = new SolidColorBrush(Colors.LightGray);
                e.Row.Foreground = new SolidColorBrush(Colors.Black);           
            }
        }

    }
}
