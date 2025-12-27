using Common.Base.Common;
using Common.Base.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Base.Interfaces.UnitOfWork
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IGenericService<T> GetGenericService<T>() where T : class, IEntityBase, new();
        Task<int> SaveAsync();
        int Save();
    }
}
