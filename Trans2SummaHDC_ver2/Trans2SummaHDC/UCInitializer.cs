using Uniconta.API.Service;
using Uniconta.API.System;
using Uniconta.DataModel;
using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Uniconta.Common;

namespace Trans2SummaHDC
{
    public static class UCInitializer
    {
        /// <summary>
        /// Proeprty for Current Company
        /// </summary>
        public static Company CurrentCompany;

        /// <summary>
        /// Property for Containing Companies List 
        /// </summary>
        public static Company[] Companies;

        /// <summary>
        /// Property for Session Variable
        /// </summary>
        public static Session CurrentSession;

        /// <summary>
        /// Property for Getting and setting the UserName
        /// </summary>
        public static string UserName;

        /// <summary>
        /// Property for getting and setting the Password
        /// </summary>
        public static string Password;

        /// <summary>
        /// Property to save User Information
        /// </summary>
        public static bool IsUserPersist;

        /// <summary>
        /// Proeprty for Current CompanyFinanceYear
        /// </summary>
        public static CompanyFinanceYear CurrentCompanyFinanceYear;

        /// <summary>
        /// Readonly Property to Get base API instance
        /// </summary>
        public static CrudAPI GetBaseAPI
        {
            get { return new CrudAPI(CurrentSession, CurrentCompany); }
        }

        /// </summary>
        /// <returns></returns>
        public static void SetCurrentCompanyFinanceYear()
        {
            var api = UCInitializer.GetBaseAPI;
            //var cols = await api.Query<CompanyFinanceYear>();
            var taskCompanyFinanceYear = api.Query<CompanyFinanceYear>();
            taskCompanyFinanceYear.Wait();
            var cols = taskCompanyFinanceYear.Result;
            foreach (var col in cols)
            {
                if (col._Current)
                {
                    CurrentCompanyFinanceYear = col;
                }
            }
        }

        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public static void SetCompany(int companyId)
        {
            CurrentCompany = CurrentSession.GetOpenCompany(companyId);
            if (CurrentCompany != null)
                CurrentSession.DefaultCompany = CurrentCompany;
            else
            {
                //CurrentCompany = await CurrentSession.OpenCompany(companyId, true);
                var taskOpenCompany = CurrentSession.OpenCompany(companyId, true);
                taskOpenCompany.Wait(10000);
                CurrentCompany = taskOpenCompany.Result;
            }
        }

        /// <summary>
        /// Method to Get the Current Session
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Session GetSession() { return CurrentSession; }

        /// <summary>
        /// Method to Set all the Companies for the Loged In User
        /// </summary>
        /// <returns></returns>
        public static void SetupCompanies()
        {
            if (CurrentSession != null)
            {
                var taskCompanies = CurrentSession.GetCompanies();
                taskCompanies.Wait(10000);
                Companies = taskCompanies.Result;
            }
        }

        /// <summary>
        /// Method which Initializes the Session
        /// </summary>
        /// <returns></returns>
        public static Session InitUniconta()
        {
            if (CurrentSession == null)
            {
                var corasauConnection = new UnicontaConnection(APITarget.Live);
                CurrentSession = new Session(corasauConnection);
            }
            return CurrentSession;
        }

        public static void UnicontaLogin()
        {
            UC_Login();

            SetupCompanies();
            var cmbCompanies = Companies;

            if (CurrentSession.User._DefaultCompany != 0)
            {
                var comp = Companies.Where(c => c.CompanyId == CurrentSession.User._DefaultCompany).FirstOrDefault();
                SetCompany(comp.CompanyId);
                SetCurrentCompanyFinanceYear();
            }
            else if (Companies.Count() > 0)
            {
                var comp = UCInitializer.Companies[0];
                UCInitializer.SetCompany(comp.CompanyId);
                UCInitializer.SetCurrentCompanyFinanceYear();
            }
            else
                Program.Log(string.Format("Trans2SummaHDC UnicontaLogin() failed with message: You do not have any access to company"));
        }

        static void UC_Login()
        {
            ErrorCodes errorCode = SetLogin(clsApp.UniContaUser, clsApp.UniContaPW);
            switch (errorCode)
            {
                case ErrorCodes.Succes:
                    Uniconta.ClientTools.Localization.SetDefault((Language)CurrentSession.User._Language);
                    Program.Log(string.Format("Trans2SummaHDC UC_Login() login as {0} OK", clsApp.UniContaUser));
                    break;

                case ErrorCodes.UserOrPasswordIsWrong:
                    Program.Log(string.Format("Trans2SummaHDC UC_Login() failed with message: Please Check Your Credentials & Try again !!!"));
                    break;

                default:
                    Program.Log(string.Format("Trans2SummaHDC UC_Login() failed with message: {0} {1}", "Unable to login", errorCode.ToString()));
                    break;
            }
        }

        static ErrorCodes SetLogin(string username, string password)
        {
            try
            {
                var ses = GetSession();
                var task = ses.LoginAsync(username, password, Uniconta.Common.User.LoginType.API, new Guid("73c93c84-af78-41e4-ada1-b8101c95ba89"), Uniconta.ClientTools.Localization.InititalLanguageCode);
                task.Wait();
                var res = task.Result;
                return res;
            }
            catch
            {
                Program.Log(string.Format("Trans2SummaHDC SetLogin() failed with message: {0}", "Fatal Error"));
                return ErrorCodes.Exception;
            }
        }

    }
}
