using NUnit.Framework;
using StructureMap;
using Jukebox.Web.DependencyResolution;
using Jukebox.Infrastructure.Repositories.Ravendb;
using Raven.Client;
using FakeItEasy;

namespace Jukebox.Web.Tests
{
    [TestFixture]
    public class RavenRegistryTests
    {
        [Test]
        public void Should_connect_raven_session_builder_handler_by_registry()
        {
            var container = new Container(new RavenRegistry());

            var handler = container.GetInstance<IRavenSessionFactoryBuilder>();

            Assert.IsInstanceOf(typeof (RavenSessionFactoryBuilder), handler);
        }

        [Test]
        public void Should_connect_document_session_handler_by_registry()
        {
            var container = new Container(new RavenRegistry());
            var factoryBuilder = container.GetInstance<IRavenSessionFactoryBuilder>();
            var context = A.Fake<IContext>();
            A.CallTo(() => context.GetInstance(typeof (RavenSessionFactoryBuilder))).Returns(factoryBuilder);

            var handler = container.GetInstance<IDocumentSession>();

            Assert.IsInstanceOf(typeof (IDocumentSession), handler);
        }
    }
}
