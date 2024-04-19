using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications.productSpecifications
{
    public class ProductWithBrandAndCategorySpecifications:BaseSpecifications<Product>
    {
        public ProductWithBrandAndCategorySpecifications(string? sort,int? brandId,int? categoryId)
            :base(p=>
            (!brandId.HasValue    || p.BrandId==brandId.Value)&&
            (!categoryId.HasValue || p.CategoryId==categoryId.Value)
            )      
        {
            Includes.Add(p=>p.Brand);
            Includes.Add(p => p.Category);
            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDesc(p => p.Price);
                        break;
                    default:
                        AddOrderBy(p => p.Name);
                        break;
                }

            }
            else
             AddOrderBy(p => p.Name);
        }
        public ProductWithBrandAndCategorySpecifications(int id)
       : base(p=>p.id==id)
        {
            Includes.Add(p => p.Brand);
            Includes.Add(p => p.Category);
        }
    }
}
