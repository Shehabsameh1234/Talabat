﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Specifications
{
    public class BaseSpecifications<T> : ISepcifications<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Criteria { get; set; } = null!;
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();
        public Expression<Func<T, object>> OrderBy { get; set; } = null!;
        public Expression<Func<T, object>> OrderByDesc { get; set; } = null!;
        public int Take { get ; set ; }
        public int Skip { get ; set ; }
        public bool IsPagenationEnabled { get ; set ; }

        //will use this ctor to set criteria by null value in get all
        public BaseSpecifications()
        {
            //Criteria=null
        }
        //will use this ctor to set criteria value  in get  by id
        public BaseSpecifications(Expression<Func<T, bool>> criteriaExpression)
        {
            Criteria=criteriaExpression; 
        }
        public void AddOrderBy(Expression<Func<T, object>> orderBy)
        {
            OrderBy=orderBy;
        }
        public void AddOrderByDesc(Expression<Func<T, object>> orderByDesc)
        {
            OrderByDesc =orderByDesc ;
        }

        public void ApplyPagination(int skip ,int take)
        {
            IsPagenationEnabled = true;
            Skip=skip;
            Take=take;
        }
    }
}
