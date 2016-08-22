using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WebApiMGR.Data.Interfaces.Base
{
    public interface IGenericRepository<TEntity, TContext>
        where TEntity : class
        where TContext : DbContext
    {
        // Crud:
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Save(TEntity entity);
        void Delete(TEntity entity);

        // Additional:
        IList<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includedNavigationProperties);
        IList<TEntity> GetList(Func<TEntity, bool> where, params Expression<Func<TEntity, object>>[] includedNavigationProperties);
        TEntity GetSingle(Func<TEntity, bool> where, params Expression<Func<TEntity, object>>[] includedNavigationProperties);
        IQueryable<TEntity> AsQueryable();
        bool Exists(int entityId);

        // Context configuration:
        void EnableDisableProxyAndLazyLoading(bool isEnabled);
    }
}
