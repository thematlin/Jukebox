using NUnit.Framework;
using StructureMap;
using Jukebox.Web.DependencyResolution;
using Jukebox.Infrastructure.Repositories;

namespace Jukebox.Web.Tests
{
    [TestFixture]
    public class RepositoryRegistryTests
    {
        [Test]
        public void Should_connect_user_repo_handler_by_registry()
        {
            var container = new Container(new RepositoryRegistry());
            container.Configure(x => x.AddRegistry(new RavenRegistry()));

            var handler = container.GetInstance<IUserRepository>();
            
            Assert.IsInstanceOf(typeof (UserRepository), handler);
        }
    }
}
