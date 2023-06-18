using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace AdminApp.Data.NET.Repositories
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(Expression<Func<T, bool>> where);
        T GetByID(long id);
        T GetOne(Expression<Func<T, bool>> where);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetMany(Expression<Func<T, bool>> where);
    }
}