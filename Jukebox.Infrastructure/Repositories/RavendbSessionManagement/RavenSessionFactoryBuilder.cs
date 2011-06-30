using System.Diagnostics;
using Raven.Client.Document;

namespace Jukebox.Infrastructure.Repositories.RavendbSessionManagement
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

            var store = new DocumentStore();
            store.Url = "http://localhost:8082/";

            return new RavenSessionFactory(store);
        }
    }
}