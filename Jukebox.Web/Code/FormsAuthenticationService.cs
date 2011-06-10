using System;
using Jukebox.Infrastructure.Membership;
using System.Web.Security;
namespace Jukebox.Web.Code
{
    public class FormsAuthenticationService : IFormsAuthentication
    {
        public void SignIn(string name, bool createPersistentCookie)
        {
            FormsAuthentication.SetAuthCookie(name, createPersistentCookie);
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }
    }
}