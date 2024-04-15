using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.Repository.Contract;
using Talabat.Core.Specifications;
using Talabat.Core.Specifications.productSpecifications;

namespace Talabat.APIs.Controllers
{
	
	public class ProductController : BaseApiController
	{
		private readonly IGenericRepository<Product> _productRepository;

		public ProductController(IGenericRepository<Product> productRepository)
        {
			_productRepository = productRepository;
		}
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
		{
			var spec =new ProductWithBrandAndCategorySpecifications();

			var products = await _productRepository.GetAllWithSpecAsync(spec);
			return Ok(products);
		}
		[HttpGet("id")]
		public async Task<ActionResult<IEnumerable<Product>>> GetProduct(int id)
		{
            var spec = new ProductWithBrandAndCategorySpecifications(id);

            var product =await _productRepository.GetWithSpecAsync(spec);
			if(product == null)
				return NotFound(new {messege="not found",StatusCode=404});
			return Ok(product);
		}
	}
}
