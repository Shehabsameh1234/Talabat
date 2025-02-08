using Talabat.Core.Specifications;

namespace Talabat.Core.Repository.Contract
{
    public interface IGenericRepository<T> where T : BaseEntity
	{
		Task<T?> GetAsync(int id);
		Task<IReadOnlyList<T>> GetAllAsync();
        Task<T?> GetWithSpecAsync(ISepcifications<T> spec);
        Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISepcifications<T> spec);
		Task<int> GetCountAsync(ISepcifications<T> spec);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
