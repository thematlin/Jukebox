namespace Jukebox.Infrastructure.Repositories.Ravendb
{
    public interface IRavenSessionFactoryBuilder
    {
        IRavenSessionFactory GetSessionFactory();
    }
}