namespace wMedlem3060.Web
{
    using System.Security.Authentication;
    using System.ServiceModel.DomainServices.Hosting;
    using System.ServiceModel.DomainServices.Server;
    using System.ServiceModel.DomainServices.Server.ApplicationServices;
    using System.Threading;
    using System.Collections.Generic;
    using System.Web.Security;
    using System.Security.Principal;
    using System.Linq;


    // TODO: Switch to a secure endpoint when deploying the application.
    //       The user's name and password should only be passed using https.
    //       To do this, set the RequiresSecureEndpoint property on EnableClientAccessAttribute to true.
    //   
    //       [EnableClientAccess(RequiresSecureEndpoint = true)]
    //
    //       More information on using https with a Domain Service can be found on MSDN.

    /// <summary>
    /// Domain Service responsible for authenticating users when they log on to the application.
    ///
    /// Most of the functionality is already provided by the AuthenticationBase class.
    /// </summary>
    [EnableClientAccess]
    public class AuthenticationService : AuthenticationBase<User>
    {
         [RequiresRole("Administrator")]
         public IEnumerable<User> GetAllUsers()
         {
             return Membership.GetAllUsers().Cast<MembershipUser>().Select(mu => this.GetUserForMembershipUser(mu));
         }

        private User GetUserForMembershipUser(MembershipUser membershipUser)
        {
            return this.GetAuthenticatedUser(new GenericPrincipal(new GenericIdentity(membershipUser.UserName), new string[0]));
        }

    }
}
