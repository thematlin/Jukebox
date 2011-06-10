namespace Jukebox.Infrastructure.Membership
{
    public interface IFormsAuthentication
    {
        void SignIn(string name, bool createPersistentCookie);
        void SignOut();
    }
}