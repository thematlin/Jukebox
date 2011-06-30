using System;
using System.Collections.Generic;
using System.Linq;
using Raven.Client;

namespace Jukebox.Infrastructure.Repositories.RavenBaseRepo
{
    public class RavendbRepository<T> : IRavendbRepository<T> where T : class
    {
        private readonly IDocumentSession _session;

        public RavendbRepository(IDocumentSession session)
        {
            _session = session;
        }


        public T SingleOrDefault(Func<T, bool> predicate)
        {
            return _session.Query<T>().SingleOrDefault(predicate);
        }

        public IEnumerable<T> All<T>()
        {
            return _session.Query<T>();
        }

        public void Add<T>(T item)
        {
            _session.Store(item);
            _session.SaveChanges();
        }

        public void AddRange(IList<T> items)
        {
            foreach (var item in items)
            {
                _session.Store(item);
            }
            _session.SaveChanges();
        }

        public void Delete<T>(T item)
        {
            _session.Delete(item);
            _session.SaveChanges();
        }
    }
}