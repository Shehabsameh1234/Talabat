using Talabat.Core.Repository.Contract;

namespace Talabat.Core
{
    public interface IUnitOfWork :IAsyncDisposable
    {
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity :BaseEntity;
        Task<int> CompleteAsync();
    }
}
