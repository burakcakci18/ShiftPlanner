using Common.Base.Common;
using Common.Base.Interfaces.Repository;
using Common.Infrastructure.Context;
using Common.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Common.Infrastructure.Repository
{
    public class GenericService<T> : IGenericService<T> where T : class, IEntityBase, new()
    {
        private readonly AppDbContext _dbContext;
        private readonly ILogging _logger;

        public GenericService(AppDbContext dbContext, ILogging logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        private DbSet<T> Table => _dbContext.Set<T>();

        private static readonly SemaphoreSlim _lock = new SemaphoreSlim(1, 1);

        private async Task<TResult> ExecuteThreadSafeAsync<TResult>(Func<Task<TResult>> action, string? operationName = null)
        {
            await _lock.WaitAsync();
            try
            {
                _logger.Debug($"Thread-safe start: {operationName ?? "Unnamed"}");
                return await action();
            }
            catch (Exception ex)
            {
                _logger.Error($"Thread-safe error in {operationName ?? "Unnamed"}", ex);
                throw;
            }
            finally
            {
                _lock.Release();
            }
        }

        public async Task<T> AddAsync(T entity)
        {
            return await ExecuteThreadSafeAsync(async () =>
            {
                await Table.AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                return entity;
            }, $"Add<{typeof(T).Name}>");
        }

        public async Task<IList<T>> AddRangeAsync(IList<T> entity)
        {
            return await ExecuteThreadSafeAsync(async () =>
            {
                await Table.AddRangeAsync(entity);
                await _dbContext.SaveChangesAsync();
                return entity;
            }, $"AddRange<{typeof(T).Name}>");
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await Table.AnyAsync(predicate);
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null, bool enableTracking = false)
        {
            IQueryable<T> query = Table;

            if (!enableTracking)
                query = query.AsNoTracking();

            if (predicate != null)
                query = query.Where(predicate);

            return await query.CountAsync();
        }

        public async Task<T> UpdateAsync(T entity)
        {
            return await ExecuteThreadSafeAsync(async () =>
            {
                Table.Update(entity);
                await _dbContext.SaveChangesAsync();
                return entity;
            }, $"Update<{typeof(T).Name}>");
        }

        public async Task DeleteAsync(int id)
        {
            await ExecuteThreadSafeAsync(async () =>
            {
                var entity = await Table.FindAsync(id);
                if (entity != null)
                {
                    Table.Remove(entity);
                    await _dbContext.SaveChangesAsync();
                }
                return true;
            }, $"DeleteById<{typeof(T).Name}>");
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> predicate, bool enableTracking = false)
        {
            IQueryable<T> query = Table;
            if (!enableTracking)
                query = query.AsNoTracking();
            return query.Where(predicate);
        }

        public async Task<T?> FirstOrDefaultAsync(
            Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IIncludableQueryable<T, object?>>? include = null,
            bool enableTracking = false)
        {
            IQueryable<T> query = Table;
            if (!enableTracking)
                query = query.AsNoTracking();
            if (include != null)
                query = include(query);
            return await query.FirstOrDefaultAsync(predicate);
        }

        public async Task<IList<T>> GetAllAsync(
            Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            bool enableTracking = false)
        {
            IQueryable<T> query = Table;
            if (!enableTracking)
                query = query.AsNoTracking();
            if (include != null)
                query = include(query);
            if (predicate != null)
                query = query.Where(predicate);
            if (orderBy != null)
                query = orderBy(query);
            return await query.ToListAsync();
        }

        public async Task<IList<T>> GetAllByPagingAsync(
            Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object?>>? include = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            bool enableTracking = false,
            int pageNumber = 1,
            int pageSize = 10)
        {
            IQueryable<T> query = Table;
            if (!enableTracking)
                query = query.AsNoTracking();
            if (include != null)
                query = include(query);
            if (predicate != null)
                query = query.Where(predicate);
            if (orderBy != null)
                query = orderBy(query);
            return await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<T> GetAsync(
            Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            bool enableTracking = false)
        {
            IQueryable<T> query = Table;
            if (!enableTracking)
                query = query.AsNoTracking();
            if (include != null)
                query = include(query);
            return await query.FirstOrDefaultAsync(predicate) ?? throw new Exception($"{typeof(T).Name} not found.");
        }

        public async Task DeleteAsync(T entity)
        {
            await ExecuteThreadSafeAsync(async () =>
            {
                _logger.Debug($"Deleting {typeof(T).Name} entity.");

                Table.Remove(entity);

                await _dbContext.SaveChangesAsync();

                _logger.Debug($"{typeof(T).Name} entity deleted successfully.");

                return Task.CompletedTask;

            }, $"Delete<{typeof(T).Name}>");
        }
    }
}
