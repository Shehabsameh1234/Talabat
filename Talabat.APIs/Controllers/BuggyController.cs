using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Repository.Data;

namespace Talabat.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuggyController : ControllerBase
    {
        private readonly StoreContext _dbContext;

        public BuggyController(StoreContext DbContext)
        {
            _dbContext = DbContext;
        }

        [HttpGet("notFound")]
        public ActionResult GetNotFoundError() 
        {
            var product = _dbContext.Products.Find(100);
            if(product == null)
                return NotFound();
            return Ok(product);       
        }
        [HttpGet("serverError")]
        public ActionResult GetserverError()
        {
            var product = _dbContext.Products.Find(100);
            var productToString = product.ToString();
            return Ok(productToString);
        }
        [HttpGet("badRequest")]
        public ActionResult GetbadRequest()
        {
            return BadRequest();
        }
        [HttpGet("badRequest/{id}")]
        public ActionResult GetbadRequest(int id)//validation error
        {
            return Ok();
        }


    }
}
