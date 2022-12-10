using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace KH.Pepper.Core.Domain
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Add(TEntity entity);

        Task<TEntity> AddAsync(TEntity entity);

        Task<bool> Create(TEntity entity);

        void Update(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);

        void Remove(TEntity entity);

        Task<TEntity> RemoveAsync(TEntity entity);

        void Delete(TEntity entity);

        Task<TEntity> DeleteAsync(TEntity entity);

        void RemoveRange(IEnumerable<TEntity> entity);
        void RemoveByWhereClause(Expression<Func<TEntity, bool>> wherePredict);

        IQueryable<TEntity> GetAllRecordsIQueryable();

        IEnumerable<TEntity> GetAll();

        Task<IEnumerable<TEntity>> GetAllAsync();

        TEntity GetById(Expression<Func<TEntity, bool>> wherePredict);

        Task<TEntity> GetByIdAsync(Expression<Func<TEntity, bool>> wherePredict);

        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> wherePredict);

        bool Any(Expression<Func<TEntity, bool>> wherePredict);

        Task<int> ExecuteSqlNonQuery(string StoredProcName, params object[] parameters);

        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
