using System;
using System.Collections.Generic;

namespace Jukebox.Infrastructure.Repositories.RavenBaseRepo
{
    public interface IRavendbRepository<T>
    {
        T SingleOrDefault(Func<T, bool> predicate);
        IEnumerable<T> All<T>();
        void Add<T>(T item);
        void AddRange(IList<T> items);
        void Delete<T>(T item);
    }
}
