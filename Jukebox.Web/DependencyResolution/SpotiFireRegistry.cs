﻿using Jukebox.Infrastructure.Services;
using StructureMap.Configuration.DSL;
using Castle.DynamicProxy;

namespace Jukebox.Web.DependencyResolution
{
    public class SpotiFireRegistry : Registry
    {
        public SpotiFireRegistry()
        {
            var pg = new ProxyGenerator();

            //For<ISpotiFireService>().EnrichAllWith((context, z) => pg.CreateInterfaceProxyWithTarget(z, new SpotiFireServiceInterceptor(context.GetInstance<ILibraryValidator>()))).OnCreationForAll((context, x) => x.ConfigureSpotiFire());
            For<ISpotiFireService>().HybridHttpOrThreadLocalScoped().Use<SpotiFireService>().OnCreation(x => x.ConfigureSpotiFire());
        }
    }
}