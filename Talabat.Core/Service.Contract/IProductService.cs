using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications.productSpecifications;

namespace Talabat.Core.Service.Contract
{
    public interface IProductService
    {
        Task<IReadOnlyList<Product>> GetProductsAsync(QuerySpecParams querySpec);
        Task<Product?> GetProductByIdAsync(int id);
        Task<int> GetCountAsync(QuerySpecParams querySpec);
        Task<IReadOnlyList<ProductBrand>> GetBrandsAsync();
        Task<IReadOnlyList<ProductCategory>> GetCategoriesAsync();

    }
}
