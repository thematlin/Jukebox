using Raven.Client;

namespace Jukebox.Infrastructure.Repositories.Ravendb
{
    public interface IRavenSessionFactory
    {
        IDocumentSession CreateSession();
    }
}