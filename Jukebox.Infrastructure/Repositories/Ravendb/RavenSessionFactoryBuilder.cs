using System.Diagnostics;
using Raven.Client.Document;

namespace Jukebox.Infrastructure.Repositories.Ravendb
{
    public class RavenSessionFactoryBuilder : IRavenSessionFactoryBuilder
    {
        private IRavenSessionFactory _ravenSessionFactory;

        public IRavenSessionFactory GetSessionFactory()
        {
            return _ravenSessionFactory ?? (_ravenSessionFactory = CreateSessionFactory());
        }

        private static IRavenSessionFactory CreateSessionFactory()
        {
            Debug.WriteLine("Created IRavenSessionFactory");
            return new RavenSessionFactory(new DocumentStore { Url = "http://localhost:8082"});
        }
    }
}