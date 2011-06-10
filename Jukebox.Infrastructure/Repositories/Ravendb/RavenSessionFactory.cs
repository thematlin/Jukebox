using System.Diagnostics;
using Raven.Client;

namespace Jukebox.Infrastructure.Repositories.Ravendb
{
    public class RavenSessionFactory : IRavenSessionFactory
    {
        private readonly IDocumentStore _documentStore;

        public RavenSessionFactory(IDocumentStore documentStore)
        {
            if (_documentStore == null)
            {
                _documentStore = documentStore;
                _documentStore.Initialize();
                var conventions = _documentStore.Conventions;
                conventions.IdentityPartsSeparator = "-";
            }
        }

        public IDocumentSession CreateSession()
        {
            Debug.WriteLine("Created IDocumentSession.");
            return _documentStore.OpenSession();
        }
    }
}