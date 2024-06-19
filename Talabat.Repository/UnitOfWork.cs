using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Repository.Contract;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _storeContext;
        private readonly Hashtable _repository;

        public UnitOfWork(StoreContext storeContext)
        {
            _storeContext = storeContext;
            _repository = new Hashtable();
        }
        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            //get the name of t entity like (product)
            var key =typeof(TEntity).Name;
            if(!_repository.ContainsKey(key))
            {
                var repository=new GenericRepository<TEntity>(_storeContext);
                _repository.Add(key, repository);
            }
            return _repository[key] as IGenericRepository<TEntity>;
        }
        public async Task<int> CompleteAsync()
         => await _storeContext.SaveChangesAsync();

        public async ValueTask DisposeAsync()
        =>await _storeContext.DisposeAsync();   
    }
}
