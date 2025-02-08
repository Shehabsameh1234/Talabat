using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Dtos;
using Talabat.APIs.Errors;
using Talabat.APIs.Helpers;
using Talabat.Core.Entities;
using Talabat.Core.Service.Contract;
using Talabat.Core.Specifications.productSpecifications;

namespace Talabat.APIs.Controllers
{
	
	public class ProductController : BaseApiController
	{
		
        private readonly IMapper _mapper;
        private readonly IProductService _productService;

        public ProductController(
            IMapper mapper,IProductService productService)
        {
            _mapper = mapper;
            _productService = productService;
        }
		
		[Cashed(600)]
		[HttpGet]
		public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery]QuerySpecParams querySpec)
		{
			
			var products = await _productService.GetProductsAsync(querySpec);

			var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);

			var Count = await _productService.GetCountAsync(querySpec);

            return Ok(new Pagination<ProductToReturnDto>(querySpec.PageIndex,querySpec.pageSize,Count,data));
		}
		[ProducesResponseType(typeof(ProductToReturnDto),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApisResponse), StatusCodes.Status404NotFound)]

        [HttpGet("{id}")]
		public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProduct(int id)
		{
			var product = await _productService.GetProductByIdAsync(id);
			if(product == null)
				return NotFound(new ApisResponse(404));
			return Ok(_mapper.Map<Product, ProductToReturnDto>(product));
		}
		[HttpGet("brands")]
		public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
		{
			var brands = await _productService.GetBrandsAsync();
			return Ok(brands);
		}
        [HttpGet("categories")]
        public async Task<ActionResult<IReadOnlyList<ProductCategory>>> GetCategories()
        {
			var category = await _productService.GetCategoriesAsync();
            return Ok(category);
        }

    }
}
