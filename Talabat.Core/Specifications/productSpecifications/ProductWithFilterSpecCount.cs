using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications.productSpecifications
{
    public class ProductWithFilterSpecCount:BaseSpecifications<Product>
    {
        public ProductWithFilterSpecCount(QuerySpecParams querySpec)
            :base(p =>
             (string.IsNullOrEmpty(querySpec.Search) || p.Name.ToLower().Contains(querySpec.Search)) &&
            (!querySpec.BrandId.HasValue || p.BrandId == querySpec.BrandId.Value) &&
            (!querySpec.CategoryId.HasValue || p.CategoryId == querySpec.CategoryId.Value))
        {
            
        }
    }
}
