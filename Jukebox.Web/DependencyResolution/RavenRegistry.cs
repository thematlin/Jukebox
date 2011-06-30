using Jukebox.Infrastructure.Repositories.RavendbSessionManagement;
using Raven.Client;
using StructureMap.Configuration.DSL;

namespace Jukebox.Web.DependencyResolution
{
    public class RavenRegistry : Registry
    {
        public RavenRegistry()
        {
            For<IRavenSessionFactoryBuilder>().Singleton().Use<RavenSessionFactoryBuilder>();

            //ObjectFactory.Configure(x => x.For<IRavenSessionFactoryBuilder>().Singleton().Use<RavenSessionFactoryBuilder>());

            For<IDocumentSession>().HttpContextScoped().Use(context => context.GetInstance<IRavenSessionFactoryBuilder>().GetSessionFactory().CreateSession());

            //ObjectFactory.Configure(
            //    x =>
            //    x.For<IDocumentSession>().HttpContextScoped().AddInstances(
            //        inst =>
            //        inst.ConstructedBy(
            //            context =>
            //            context.GetInstance<IRavenSessionFactoryBuilder>().GetSessionFactory().CreateSession())));
        }
    }
}