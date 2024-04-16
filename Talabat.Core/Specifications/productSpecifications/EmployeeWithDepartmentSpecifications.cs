using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications.productSpecifications
{
    public class EmployeeWithDepartmentSpecifications:BaseSpecifications<Emoloyee>
    {
        public EmployeeWithDepartmentSpecifications()
            :base()
        {
            Includes.Add(e => e.Department);
        }
        public EmployeeWithDepartmentSpecifications(int id)
           : base(p=>p.id==id)
        {
            Includes.Add(e => e.Department);
        }
    }
}
