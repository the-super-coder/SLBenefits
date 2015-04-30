using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Linq;
using EntityFramework.BulkInsert.Extensions;

namespace SLBenefits.Core
{
    public interface IRepository<T> : IDisposable where T : class
    {

        IEnumerable<T> GetAll();
        IEnumerable<T> GetAll<TProperty>(Expression<Func<T, TProperty>> includePath);

        IEnumerable<T> GetPage<TKey>(int pageIndex, int pageSize, Expression<Func<T, TKey>> orderBy);

        IEnumerable<T> Where(Expression<Func<T, bool>> where);

        T Single(Expression<Func<T, bool>> where);

        T First(Expression<Func<T, bool>> where);

        T FirstOrDefault(Expression<Func<T, bool>> where);

        T GetById(params object[] id);

        void Delete(object id);

        void Delete(T entity);

        void ExecuteSql(string sqlQuery, params object[] parameters);

        void Insert(T entity);

        void BulkInsert(IEnumerable<T> entities);

        void Update(T entity);

        void Save();

        IQueryable<T> GetQuery();

        IEnumerable<T> GetWithSql(string sql, params object[] parameters);

        void UndoChangesDbContextLevel();

        void UndoChangesDbEntityLevel(T entity);
    }
    public class Repository<T>
        : DisposableBase, IRepository<T> where T : class
    {
        public Repository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        private readonly DbContext _context;
        protected readonly DbSet<T> _dbSet;

        public object GetBaseContext()
        {
            return _context;
        }

        public IQueryable<T> GetQuery()
        {
            return _dbSet;
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.AsEnumerable();
        }

        public IEnumerable<T> GetAll<TProperty>(Expression<Func<T, TProperty>> includePath)
        {
            return _dbSet.Include<T, TProperty>(includePath);
        }

        public IEnumerable<T> GetPage<TKey>(int pageIndex, int pageSize, Expression<Func<T, TKey>> orderBy)
        {
            return GetQuery().OrderBy(orderBy).Skip(pageIndex * pageSize).Take(pageSize);
        }

        public IEnumerable<T> Where(Expression<Func<T, bool>> where)
        {
            return _dbSet.Where(where);
        }

        public T Single(Expression<Func<T, bool>> where)
        {
            return _dbSet.Single(where);
        }

        public T First(Expression<Func<T, bool>> where)
        {
            return _dbSet.First(where);
        }

        public T FirstOrDefault(Expression<Func<T, bool>> where)
        {
            return _dbSet.FirstOrDefault(where);
        }

        public virtual T GetById(params object[] id)
        {
            var e = _dbSet.Find(id);
            return e;
        }

        public void Detach(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual void Delete(object id)
        {
            T entity = GetById(id);
            Delete(entity);
        }

        public void Delete(T entity)
        {
            AttachIfNeeded(entity);
            _dbSet.Remove(entity);
        }

        public void ExecuteSql(string sqlQuery, params object[] parameters)
        {
            _context.Database.ExecuteSqlCommand(sqlQuery, parameters);
        }

        public void Insert(T entity)
        {
            _dbSet.Add(entity);
        }

        public void BulkInsert(IEnumerable<T> entities)
        {
            _context.BulkInsert(entities);
        }

        public void Update(T entity)
        {
            var entry = _context.Entry<T>(entity);

            var oCtx = (_context as System.Data.Entity.Infrastructure.IObjectContextAdapter).ObjectContext;
            var stateManager = oCtx.ObjectStateManager;

            var key = oCtx.CreateEntityKey(oCtx.CreateObjectSet<T>().EntitySet.Name, entity);

            if (entry.State == EntityState.Detached)
            {
                var set = _context.Set<T>();
                T attachedEntity = set.Find(key.EntityKeyValues.Select(v => v.Value).ToArray());

                if (attachedEntity != null)
                {
                    var attachedEntry = _context.Entry(attachedEntity);
                    attachedEntry.CurrentValues.SetValues(entity);
                }
                else
                {
                    entry.State = EntityState.Modified; // This should attach entity
                }
            }

            //An object with the same key already exists in the ObjectStateManager. The ObjectStateManager cannot track multiple objects with the same key.
            //_dbSet.Attach(entity);
            //_context.Entry(entity).State = EntityState.Modified;
        }

        public void UndoChangesDbEntityLevel(T entity)
        {
            var entry = _context.Entry<T>(entity);
            if (entry != null)
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Deleted:
                        entry.Reload();
                        break;
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    default: break;
                }
            }
        }

        public void UndoChangesDbContextLevel()
        {
            // Undo the changes of the all entries. 
            foreach (var entry in _context.ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    // Under the covers, changing the state of an entity from  
                    // Modified to Unchanged first sets the values of all  
                    // properties to the original values that were read from  
                    // the database when it was queried, and then marks the  
                    // entity as Unchanged. This will also reject changes to  
                    // FK relationships since the original value of the FK  
                    // will be restored. 
                    case EntityState.Modified:
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    // If the EntityState is the Deleted, reload the date from the database.   
                    case EntityState.Deleted:
                        entry.Reload();
                        break;
                    default: break;
                }
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public virtual IEnumerable<T> GetWithSql(string sql, params object[] parameters)
        {
            return _dbSet.SqlQuery(sql, parameters).ToList();
        }

        protected override void OnDisposing(bool disposing)
        {
            if (_context != null)
                _context.Dispose();
        }

        private void AttachIfNeeded(T entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
                _dbSet.Attach(entity);
        }
    }
}
