using Common.Base.Interfaces.Repository;
using Common.Base.Interfaces.UnitOfWork;
using Common.Infrastructure.Context;
using Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;
        private readonly ILogging _logging;

        public UnitOfWork(AppDbContext dbContext, ILogging logging)
        {
            _dbContext = dbContext;
            _logging = logging;
        }

        public async ValueTask DisposeAsync() => await _dbContext.DisposeAsync();

        public int Save() => _dbContext.SaveChanges();

        public async Task<int> SaveAsync() => await _dbContext.SaveChangesAsync();

        IGenericService<T> IUnitOfWork.GetGenericService<T>() => new GenericService<T>(_dbContext, _logging);

    }
}
