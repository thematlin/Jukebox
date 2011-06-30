using Jukebox.Infrastructure.Services.SpotiFireService;
using StructureMap.Configuration.DSL;

namespace Jukebox.Web.DependencyResolution
{
    public class SpotiFireRegistry : Registry
    {
        public SpotiFireRegistry()
        {
            For<ISpotiFireService>().HybridHttpOrThreadLocalScoped().Use<SpotiFireService>().OnCreation(x => x.ConfigureSpotiFire());
        }
    }
}