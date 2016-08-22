using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WebApiMGR.Data.Interfaces.Base;

namespace WebApiMGR.Data.Repositories.Base
{
    internal class GenericRepository<TEntity, TContext> : IGenericRepository<TEntity, TContext>
        where TEntity : class
        where TContext : DbContext
    {
        protected TContext _context;

        public bool LazyLoadingEnabled
        {
            get
            {
                return _context.Configuration.LazyLoadingEnabled;
            }
            set
            {
                _context.Configuration.LazyLoadingEnabled = value;
            }
        }

        public bool ProxyCreationEnabled
        {
            get
            {
                return _context.Configuration.ProxyCreationEnabled;
            }
            set
            {
                _context.Configuration.ProxyCreationEnabled = value;
            }
        }

        public GenericRepository(TContext context)
        {
            _context = context;
        }

        public virtual void Add(TEntity entity)
        {
            _context.Entry(entity).State = System.Data.Entity.EntityState.Added;
            try
            {
                _context.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting
                        // the current instance as InnerException
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }
        }

        public virtual void Update(TEntity entity)
        {
            _context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }

        public virtual void Save(TEntity entity)
        {
            _context.SaveChanges();
        }

        public virtual void Delete(TEntity entity)
        {
            _context.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
            _context.SaveChanges();
        }

        public virtual IList<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includedNavigationProperties)
        {
            List<TEntity> list;
            IQueryable<TEntity> dbQuery = _context.Set<TEntity>();

            //Apply eager loading
            if (includedNavigationProperties != null)
                foreach (Expression<Func<TEntity, object>> navigationProperty in includedNavigationProperties)
                    dbQuery = dbQuery.Include<TEntity, object>(navigationProperty);

            list = dbQuery.AsNoTracking().ToList<TEntity>();

            return list;
        }

        public virtual IList<TEntity> GetList(Func<TEntity, bool> where, params Expression<Func<TEntity, object>>[] includedNavigationProperties)
        {
            this.EnableDisableProxyAndLazyLoading(true);
            List<TEntity> list;
            IQueryable<TEntity> dbQuery = _context.Set<TEntity>();

            //Apply eager loading
            if (includedNavigationProperties != null)
                foreach (Expression<Func<TEntity, object>> navigationProperty in includedNavigationProperties)
                    dbQuery = dbQuery.Include<TEntity, object>(navigationProperty);

            list = dbQuery.AsNoTracking().Where(where).ToList<TEntity>();

            return list;
        }

        public virtual TEntity GetSingle(Func<TEntity, bool> where, params Expression<Func<TEntity, object>>[] includedNavigationProperties)
        {
            TEntity item = null;
            IQueryable<TEntity> dbQuery = _context.Set<TEntity>();

            //Apply eager loading
            if (includedNavigationProperties != null)
                foreach (Expression<Func<TEntity, object>> navigationProperty in includedNavigationProperties)
                    dbQuery = dbQuery.Include<TEntity, object>(navigationProperty);

            item = dbQuery
                .AsNoTracking() //Don't track any changes for the selected item
                .FirstOrDefault(where); //Apply where clause

            return item;
        }

        public virtual bool Exists(int entityId)
        {
            return _context.Set<TEntity>().Find(entityId) != null;
        }

        public virtual bool Exists(string entityId)
        {
            return _context.Set<TEntity>().Find(entityId) != null;
        }

        public virtual bool Exists(Guid entityId)
        {
            return _context.Set<TEntity>().Find(entityId) != null;
        }

        public void EnableDisableProxyAndLazyLoading(bool isEnabled)
        {
            this.LazyLoadingEnabled = isEnabled;
            this.ProxyCreationEnabled = isEnabled;
        }

        public virtual IQueryable<TEntity> AsQueryable()
        {
            this.EnableDisableProxyAndLazyLoading(true);

            return _context.Set<TEntity>();
        }
    }
}
