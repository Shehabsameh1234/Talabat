using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repository.Contract;
using Talabat.Core.Specifications;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
	public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
	{
		private readonly StoreContext _dbContext;

		public GenericRepository(StoreContext DbContext)
        {
			_dbContext = DbContext;
		}
        public async Task<T?> GetAsync(int id)
        {
            //if (typeof(T) == typeof(Product))
                //return await _dbContext.Set<Product>().Where(p => p.id == id)
                //                     .Include(p => p.Brand).Include(p => p.Category).AsNoTracking().FirstOrDefaultAsync() as T;

            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
		{
			//if (typeof(T) == typeof(Product))
				//return (IEnumerable<T>) await _dbContext.Set<Product>()
				//	           .Include(p => p.Brand).Include(p => p.Category).AsNoTracking().ToListAsync();

			return await _dbContext.Set<T>().ToListAsync();
		}

        public async Task<T?> GetWithSpecAsync(ISepcifications<T> spec)
        {
            return await ApplySpecifications(spec).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISepcifications<T> spec)
        {
            return await ApplySpecifications(spec).ToListAsync();
        }
        public async Task<int> GetCountAsync(ISepcifications<T> spec)
        {
            return await ApplySpecifications(spec).CountAsync();
        }
        private IQueryable<T> ApplySpecifications(ISepcifications<T> spec)
        {
            return SpecificationsEvaluetor<T>.GetQuery(_dbContext.Set<T>(), spec);
        }

      
    }
}
