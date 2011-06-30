namespace Jukebox.Infrastructure.Repositories.RavendbSessionManagement
{
    public interface IRavenSessionFactoryBuilder
    {
        IRavenSessionFactory GetSessionFactory();
    }
}