using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public class BaseSpecifications<T> : ISepcifications<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Criteria { get; set; } = null;
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();

        //will use this ctor to set criteria by null value in get all
        public BaseSpecifications()
        {
            //Criteria=null
        }
        //will use this ctor to set criteria value  in get  by id
        public BaseSpecifications(Expression<Func<T, bool>> criteriaExpression)
        {
            Criteria=criteriaExpression; //p=>p.id==10
        }
    }
}
