
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using KH.Pepper.Core.Domain;

namespace KH.Pepper.Infra.DataBase
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        DbSet<TEntity> _dbSet;
        private AppDbContext _dbContext;
         

        public BaseRepository(AppDbContext DbContext)
        {
            _dbContext = DbContext;
            _dbSet = _dbContext.Set<TEntity>();

        }

        public void Add(TEntity entity)
        {
            _dbSet.Add(entity);
            _dbContext.SaveChanges();
        }

        public async Task<bool> Create(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            //await _dbContext.Set<TEntity>().AddAsync(entity);
            await _dbSet.AddAsync(entity);
            _dbContext.SaveChanges();
            return true;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            await _dbSet.AddAsync(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public void Update(TEntity entity)
        {
            _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }


        public Task<TEntity> UpdateAsync(TEntity entity)
        {
            //_dbContext.Entry(entity).State = EntityState.Detached;
           //// _dbSet.Update(entity);
            _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            return Task.FromResult(entity);
        }

        public void Remove(TEntity entity)
        {
            if (_dbContext.Entry(entity).State == EntityState.Detached)
                _dbSet.Attach(entity);
            _dbSet.Remove(entity);
        }

        public Task<TEntity> RemoveAsync(TEntity entity)
        {
            if (_dbContext.Entry(entity).State == EntityState.Detached)
                _dbSet.Attach(entity);
            _dbSet.Remove(entity);
            return Task.FromResult(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            _dbSet.RemoveRange(entity);
        }
        public void Delete(TEntity entity)
        {
            if (_dbContext.Entry(entity).State == EntityState.Deleted)
                _dbSet.Attach(entity);
            _dbSet.Remove(entity);
        }

        public Task<TEntity> DeleteAsync(TEntity entity)
        {
            if (_dbContext.Entry(entity).State == EntityState.Detached)
                _dbSet.Attach(entity);
            _dbSet.Remove(entity);
            return Task.FromResult(entity);
        }

        public void RemoveByWhereClause(Expression<Func<TEntity, bool>> wherePredict)
        {
            TEntity entity = _dbSet.AsNoTracking().Where(wherePredict).FirstOrDefault();
            Remove(entity);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _dbSet.AsNoTracking().ToList();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public TEntity GetById(Expression<Func<TEntity, bool>> wherePredict)
        {
            return _dbSet.AsNoTracking().Where(wherePredict).FirstOrDefault();
        }

        public async Task<TEntity> GetByIdAsync(Expression<Func<TEntity, bool>> wherePredict)
        {
            return await _dbSet.AsNoTracking().Where(wherePredict).FirstOrDefaultAsync(); 
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> wherePredict)
        {
            return _dbSet.AsNoTracking().Where(wherePredict).FirstOrDefault();
        }

        public IQueryable<TEntity> GetAllRecordsIQueryable()
        {
            return _dbSet.AsNoTracking();
        }

        public async Task<int> ExecuteSqlNonQuery(string StoredProcName, params object[] parameters)
        {
            int affectedRows = await _dbContext.Database.ExecuteSqlRawAsync($"{StoredProcName}", parameters );
            //int affectedRows = await context.Database.ExecuteSqlRawAsync("[dbo].[sp_GetStudentsNew] @Name, @Standard, @TotalStudents out", param);

            return affectedRows;
        }

        public bool Any(Expression<Func<TEntity, bool>> wherePredict)
        {
           // _dbContext.Database.ExecuteSqlRawAsync
            return _dbSet.AsNoTracking().Any(wherePredict);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
