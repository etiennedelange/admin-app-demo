//using System;
//using System.Collections.Generic;
//using System.Configuration;
//using System.Data.Entity;
//using System.Linq;
//using System.Linq.Expressions;

//namespace AdminApp.Data.NET.Repositories
//{
//    public abstract class RepositoryBase<T>
//        where T : class
//    {
//        private readonly AdminAppContext _dbContext;
//        private readonly DbSet<T> _dbSet;

//        /// <summary>
//        /// Creates a db context with DbContextOptions when the db is not accessed from within the API project that uses dependency injection to create the db context
//        /// </summary>
//        /// <returns>An AdminAppContext instance with DbContextOptions</returns>
//        protected static AdminAppContext CreateDbContext() => new AdminAppContext(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

//        public virtual void Add(T entity) => _dbSet.Add(entity);

//        public virtual void Delete(T entity) => _dbSet.Remove(entity);

//        public virtual void Update(T entity)
//        {
//            _dbSet.Attach(entity);
//            _dbContext.Entry(entity).State = EntityState.Modified;
//        }

//        public virtual void Delete(Expression<Func<T, bool>> where)
//        {
//            IEnumerable<T> objects = GetMany(where).AsEnumerable();

//            foreach (T obj in objects)
//            {
//                _dbSet.Remove(obj);
//            }
//        }

//        public virtual T GetByID(long id) => _dbSet.Find(id);

//        public virtual IEnumerable<T> GetAll() => _dbSet.ToList();

//        public virtual void Insert(T entity) => _dbSet.Add(entity);

//        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where) => _dbSet.Where<T>(where).ToList();

//        public virtual T GetOne(Expression<Func<T, bool>> where) => _dbSet.Where(where).FirstOrDefault<T>();
//    }
//}
