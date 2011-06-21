using Jukebox.Infrastructure;
using Jukebox.Infrastructure.Services;
using StructureMap.Configuration.DSL;
using Castle.DynamicProxy;
using Jukebox.Infrastructure.Interceptors;
using Jukebox.Infrastructure.Validators;

namespace Jukebox.Web.DependencyResolution
{
    public class SpotiFireRegistry : Registry
    {
        public SpotiFireRegistry()
        {
            var pg = new ProxyGenerator();

            For<ISpotiFireService>().EnrichAllWith((context, z) => pg.CreateInterfaceProxyWithTarget(z, new SpotiFireServiceInterceptor(context.GetInstance<ILibraryValidator>()))).OnCreationForAll((context, x) => x.ConfigureSpotiFire());
        }
    }
}