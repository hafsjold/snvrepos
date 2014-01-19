namespace wMedlem3060
{
    using System.Windows.Controls;
    using System.Windows.Navigation;
    using wMedlem3060.Web;
    using System.ServiceModel.DomainServices.Client;

    /// <summary>
    /// <see cref="Page"/> class to present information about the current application.
    /// </summary>
    public partial class About : Page
    {
        /// <summary>
        /// Creates a new instance of the <see cref="About"/> class.
        /// </summary>
        public About()
        {
            InitializeComponent();

            this.Title = ApplicationStrings.AboutPageTitle;
        }

        /// <summary>
        /// Executes when the user navigates to this page.
        /// </summary>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

        }

        private void button1_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MyDomainContext MyContext = new MyDomainContext();
            object myState = new object();
            MyContext.GetLocalTemperature("KBH", GetLocalTemperature_Completed, myState);
        }

        private void GetLocalTemperature_Completed(InvokeOperation<int> temp) 
        {
            var xx = temp.Value;
            int y = 2 + 3;
        }
    }
}