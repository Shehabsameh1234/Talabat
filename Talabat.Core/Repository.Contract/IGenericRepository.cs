using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
