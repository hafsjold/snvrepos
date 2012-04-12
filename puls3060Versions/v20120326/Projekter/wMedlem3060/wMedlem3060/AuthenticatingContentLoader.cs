using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Navigation;

namespace wMedlem3060.ContextLoader
{
    public class AuthenticatingContentLoader : INavigationContentLoader
    {        
        public string SecuredFolder {get; set;}
               
        private PageResourceContentLoader loader = new PageResourceContentLoader();

        public IAsyncResult BeginLoad(Uri targetUri, Uri currentUri, AsyncCallback userCallback, object asyncState)
        {
            var UserIsAuthenticated = WebContext.Current.User.IsAuthenticated;
            if (!UserIsAuthenticated)
            {
                var targetfolder = System.IO.Path.GetDirectoryName(targetUri.ToString()).Trim('\\');
                if (System.IO.Path.GetDirectoryName(targetUri.ToString()).Trim('\\') == SecuredFolder)
                {
                    targetUri = new Uri("/Views/Home.xaml", UriKind.Relative);
                }
            }
            return loader.BeginLoad(targetUri, currentUri, userCallback, asyncState);
        }

        public bool CanLoad(Uri targetUri, Uri currentUri)
        {
            return loader.CanLoad(targetUri, currentUri);
        }

        public void CancelLoad(IAsyncResult asyncResult)
        {
            loader.CancelLoad(asyncResult);
        }

        public LoadResult EndLoad(IAsyncResult asyncResult)
        {
            return loader.EndLoad(asyncResult);            
        }
    }
}
