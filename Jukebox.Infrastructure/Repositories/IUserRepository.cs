using System.Collections.Generic;
using System.Linq;
using Jukebox.Business.Models;

namespace Jukebox.Infrastructure.Repositories
{
    public interface IUserRepository
    {
        void AddUser(User user);
        IEnumerable<User> GetAll();
        User GetUser(string id);
        IQueryable<User> Query();
    }
}
