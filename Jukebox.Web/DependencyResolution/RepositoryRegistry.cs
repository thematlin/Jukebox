using Jukebox.Infrastructure.Repositories;
using StructureMap;
using StructureMap.Configuration.DSL;

namespace Jukebox.Web.DependencyResolution
{
    public class RepositoryRegistry : Registry
    {
        public RepositoryRegistry()
        {
            For<IUserRepository>().Use<UserRepository>();

            //ObjectFactory.Configure(x => x.For<IUserRepository>().Use<UserRepository>());
        }
    }
}