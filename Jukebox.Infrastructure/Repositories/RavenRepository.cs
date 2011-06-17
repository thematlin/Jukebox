using System;
using System.Collections.Generic;
using System.Linq;
using Jukebox.Business.Models.Contracts;
using Raven.Client;

namespace Jukebox.Infrastructure.Repositories
{
    public class RavenRepository : IRavenRepository
    {
        private IDocumentSession _session;

        public RavenRepository(IDocumentSession session)
        {
            _session = session;
        }


        public T SingleOrDefault<T>(Func<T, bool> predicate) where T : IPersistable
        {
            return _session.Query<T>().SingleOrDefault(predicate);
        }

        public IEnumerable<T> All<T>() where T : IPersistable
        {
            return _session.Query<T>();
        }

        public void Add<T>(T item) where T : IPersistable
        {
            _session.Store(item);
            _session.SaveChanges();
        }

        public void AddRange<T>(IList<T> items) where T : IPersistable
        {
            foreach (var item in items)
            {
                _session.Store(item);
            }
            _session.SaveChanges();
        }

        public void Delete<T>(T item) where T : IPersistable
        {
            _session.Delete(item);
            _session.SaveChanges();
        }
    }
}