using System;
using System.Collections.Generic;
using System.Linq;
using Jukebox.Business.Models.Contracts;

namespace Jukebox.Infrastructure.Repositories.Base
{
    public interface IPersistentRepository<T> where T : IPersistable
    {
        IEnumerable<T> GetAll();
        void Save(T obj);
        T Update(T obj);
        void Delete(T obj);
        T Get(string id);
        void SaveChanges();
        void InsertAll(IList<T> objs);
        IQueryable<T> Query<T>();
    }
}
