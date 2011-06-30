using Raven.Client;

namespace Jukebox.Infrastructure.Repositories.RavendbSessionManagement
{
    public interface IRavenSessionFactory
    {
        IDocumentSession CreateSession();
    }
}