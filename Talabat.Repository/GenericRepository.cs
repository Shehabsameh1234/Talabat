﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
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

            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
		{
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

        public void Add(T entity)
        =>_dbContext.Add(entity);

        public void Update(T entity)
       => _dbContext.Update(entity);

        public void Delete(T entity)
       => _dbContext.Remove(entity);
    }
}
