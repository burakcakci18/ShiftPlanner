using Common.Base.Common;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Common.Base.Interfaces.Repository
{
    public interface IGenericService<T> where T : class, IEntityBase, new()
    {
        Task<IList<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            bool enableTracking = false);

        Task<IList<T>> GetAllByPagingAsync(Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            bool enableTracking = false, int pageNumber = 1, int pageSize = 10);

        Task<T> GetAsync(Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            bool enableTracking = false);

        IQueryable<T> Find(Expression<Func<T, bool>> predicate, bool enableTracking = false);

        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);


        Task<T?> FirstOrDefaultAsync(
          Expression<Func<T, bool>> predicate,
          Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
          bool enableTracking = false);

        Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null, bool enableTracking = false);

        Task<T> AddAsync(T entity);

        Task<IList<T>> AddRangeAsync(IList<T> entity);

        Task<T> UpdateAsync(T entity);

        Task DeleteAsync(T entity);
        Task DeleteAsync(int id);
    }
}
