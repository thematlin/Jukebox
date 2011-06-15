using System.Web.Security;
using Jukebox.Infrastructure.Membership;

namespace Jukebox.Infrastructure.Security
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