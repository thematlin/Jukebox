using Jukebox.Infrastructure.Security;
using Jukebox.Web.DependencyResolution;
using StructureMap;
using Jukebox.Infrastructure.Repositories.Ravendb;
using Jukebox.Infrastructure.Membership;
using Jukebox.Web.Code;
using DotNetOpenAuth.OAuth.ChannelElements;
using Jukebox.Infrastructure.Services;

namespace Jukebox.Web {
    public static class IoC {
        public static IContainer Initialize()
        {
            ObjectFactory.Initialize(x =>
                        {
                            x.Scan(scan =>
                                    {
                                        scan.Assembly("Jukebox.Infrastructure");
                                        scan.Assembly("Jukebox.Business");

                                        //scan.ExcludeNamespace("Jukebox.Infrastructure.Repositories.Ravendb");
                                        //scan.ExcludeNamespace("Jukebox.Infrastructure.Repositories.Base");
                                        //scan.ExcludeType<UserRepository>();
                                        ////scan.ExcludeType<IUserRepository>();
                                        scan.ExcludeType<IRavenSessionFactory>();
                                        scan.ExcludeType<RavenSessionFactory>();
                                        //scan.ExcludeType<IUserRepository>();
                                        //scan.ExcludeType<UserRepository>();

                                        scan.TheCallingAssembly();
                                        scan.WithDefaultConventions();
                                    });
            //                x.For<IExample>().Use<Example>();
                            x.For<IFormsAuthentication>().Use<FormsAuthenticationService>();
                            x.For<IConsumerTokenManager>().Singleton().Use<InMemoryTokenManager>().Ctor<string>("consumerKey").Is("ewATkXLGvQtJ20wEgkVJDQ").Ctor<string>("consumerSecret").Is("AvSQdbm4TdLXHJjkMWxlOGXYNBjignACKzYko5KEOQ");
                            x.For<IPlaylistHolder>().Singleton().Use<PlaylistHolder>();
                            x.AddRegistry(new SpotiFireRegistry());
                            x.AddRegistry(new RavenRegistry());
                            x.AddRegistry(new ValidatorsRegistry());
                        });

            ObjectFactory.AssertConfigurationIsValid();
            return ObjectFactory.Container;
        }
    }
}