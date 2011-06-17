using System;
using System.Collections.Generic;
using Jukebox.Business.Models.Contracts;

namespace Jukebox.Infrastructure.Repositories
{
    public interface IRavenRepository
    {
        T SingleOrDefault<T>(Func<T, bool> predicate) where T : IPersistable;
        IEnumerable<T> All<T>() where T : IPersistable;
        void Add<T>(T item) where T : IPersistable;
        void AddRange<T>(IList<T> items) where T : IPersistable;
        void Delete<T>(T item) where T : IPersistable;
    }
}
