using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jukebox.Business.Models;
using Jukebox.Infrastructure.Repositories.Base;
using Raven.Client;

namespace Jukebox.Infrastructure.Repositories
{
    public class UserRepository : PersistentRepository<User>, IUserRepository
    {
        public UserRepository(IDocumentSession documentSession) : base(documentSession)
        {
            
        }

        public void AddUser(User user)
        {
            Save(user);
            SaveChanges();
        }

        public new IEnumerable<User> GetAll()
        {
            return base.GetAll();
        }

        public User GetUser(string id)
        {
            return Get(id);
        }

        public IQueryable<User> Query()
        {
            return Query<User>();
        }
    }
}
