using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Specifications;

namespace Talabat.Repository
{
    internal class SpecificationsEvaluetor<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuey,ISepcifications<TEntity> spec)
        {
            var query = inputQuey; 

            if (spec.Criteria is not null)
                query = query.Where(spec.Criteria);

            if(spec.OrderBy is not null)
                query=query.OrderBy(spec.OrderBy);

            else if (spec.OrderByDesc is not null)
                query = query.OrderByDescending(spec.OrderByDesc);
            if(spec.IsPagenationEnabled)
                query=query.Skip(spec.Skip).Take(spec.Take);

            query = spec.Includes.Aggregate(query, (currentQuery, includeExpression) => currentQuery.Include(includeExpression));

            return query;

        }
    }
}
