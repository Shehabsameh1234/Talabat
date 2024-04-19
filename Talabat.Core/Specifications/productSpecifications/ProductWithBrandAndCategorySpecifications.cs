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
        public ProductWithBrandAndCategorySpecifications(QuerySpecParams querySpec)
            :base(p=>
            (string.IsNullOrEmpty(querySpec.Search)||p.Name.ToLower().Contains(querySpec.Search))&&
            (!querySpec.BrandId.HasValue    || p.BrandId== querySpec.BrandId.Value)&&
            (!querySpec.CategoryId.HasValue || p.CategoryId== querySpec.CategoryId.Value)
            )      
        {
            Includes.Add(p=>p.Brand);
            Includes.Add(p => p.Category);
            if (!string.IsNullOrEmpty(querySpec.Sort))
            {
                switch (querySpec.Sort)
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

            //products =18~20
            //pageindex=3
            //pagesize=5

            ApplyPagination((querySpec.PageIndex-1)*querySpec.pageSize, querySpec.pageSize);
        }
        public ProductWithBrandAndCategorySpecifications(int id)
       : base(p=>p.id==id)
        {
            Includes.Add(p => p.Brand);
            Includes.Add(p => p.Category);
        }
    }
}
