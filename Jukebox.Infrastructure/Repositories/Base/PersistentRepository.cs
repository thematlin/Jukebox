using System;
using System.Collections.Generic;
using Jukebox.Business.Models.Contracts;
using Raven.Client;
using System.Linq.Expressions;
using System.Linq;

namespace Jukebox.Infrastructure.Repositories.Base
{
    public abstract class PersistentRepository<T> : IPersistentRepository<T> where T : IPersistable
    {
        protected IDocumentSession DocumentSession { get; set; }

        protected PersistentRepository(IDocumentSession documentSession)
        {
            DocumentSession = documentSession;
        }

        public IEnumerable<T> GetAll()
        {
            return DocumentSession.Query<T>();
        }

        public void Save(T obj)
        {
            DocumentSession.Store(obj);
        }

        public T Update(T obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(T obj)
        {
            throw new NotImplementedException();
        }

        public T Get(string id)
        {
            return DocumentSession.Load<T>(id);
        }

        public IQueryable<T> Query<T>()
        {
            return DocumentSession.Query<T>();
        }

        public void SaveChanges()
        {
            DocumentSession.SaveChanges();
        }

        public void InsertAll(IList<T> objs)
        {
            throw new NotImplementedException();
        }
    }
}