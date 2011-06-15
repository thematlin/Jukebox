using System.Diagnostics;
using Raven.Abstractions.Indexing;
using Raven.Client.Document;
using Jukebox.Business.Models;

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

            var store = new DocumentStore();
            store.Url = "http://localhost:8082/";

            return new RavenSessionFactory(store);
        }
    }
}