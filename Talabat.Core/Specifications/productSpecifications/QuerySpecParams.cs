using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Specifications.productSpecifications
{
    public class QuerySpecParams
    {
        private const int MaxPageSize = 10;
        private int PageSize=5;
        public int pageSize
        {
            get { return PageSize; }
            set { PageSize =  value > MaxPageSize ? MaxPageSize : value ; }
        }
        public int PageIndex { get; set; } = 1;
        public string? Sort { get; set; }
        public int? BrandId { get; set; }
        public int? CategoryId { get; set; }

    }
}
