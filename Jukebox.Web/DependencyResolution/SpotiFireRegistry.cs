using Jukebox.Infrastructure;
using Jukebox.Infrastructure.Services;
using StructureMap.Configuration.DSL;

namespace Jukebox.Web.DependencyResolution
{
    public class SpotiFireRegistry : Registry
    {
        public SpotiFireRegistry()
        {
            For<ISpotiFireService>().Use<SpotiFireService>().OnCreation(x => x.ConfigureSpotiFire());
        }
    }
}