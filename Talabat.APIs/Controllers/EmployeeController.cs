using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.Repository.Contract;
using Talabat.Core.Specifications.productSpecifications;

namespace Talabat.APIs.Controllers
{
 
    public class EmployeeController : BaseApiController
    {
        private readonly IGenericRepository<Emoloyee> _empRepository;

        public EmployeeController(IGenericRepository<Emoloyee> empRepository)
        {
           _empRepository = empRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Emoloyee>>> GetEmps()
        {
            var spec =new EmployeeWithDepartmentSpecifications();
            var emps =await _empRepository.GetAllWithSpecAsync(spec);
            return Ok(emps);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Emoloyee>> GetEmp(int id)
        {
            var spec = new EmployeeWithDepartmentSpecifications(id);

            var emp = await _empRepository.GetWithSpecAsync(spec);

            if(emp == null) { return NotFound(); }

            return Ok(emp);
        }
    }
}
