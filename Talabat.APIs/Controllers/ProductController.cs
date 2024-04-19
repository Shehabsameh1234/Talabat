using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Dtos;
using Talabat.APIs.Errors;
using Talabat.Core.Entities;
using Talabat.Core.Repository.Contract;
using Talabat.Core.Specifications;
using Talabat.Core.Specifications.productSpecifications;

namespace Talabat.APIs.Controllers
{
	
	public class ProductController : BaseApiController
	{
		private readonly IGenericRepository<Product> _productRepository;
        private readonly IGenericRepository<ProductBrand> _brandRepository;
        private readonly IGenericRepository<ProductCategory> _categoryiesRepository;
        private readonly IMapper _mapper;

        public ProductController(
			IGenericRepository<Product> productRepository,
            IGenericRepository<ProductBrand> BrandRepository,
            IGenericRepository<ProductCategory> CategoryiesRepository,
            IMapper mapper)
        {
			_productRepository = productRepository;
            _brandRepository = BrandRepository;
            _categoryiesRepository = CategoryiesRepository;
            _mapper = mapper;
        }
		[HttpGet]
		public async Task<ActionResult<IEnumerable<ProductToReturnDto>>> GetProducts()
		{
			var spec =new ProductWithBrandAndCategorySpecifications();

			var products = await _productRepository.GetAllWithSpecAsync(spec);
			return Ok(_mapper.Map<IEnumerable<Product>,IEnumerable<ProductToReturnDto>>(products));
		}
		[ProducesResponseType(typeof(ProductToReturnDto),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApisResponse), StatusCodes.Status404NotFound)]

        [HttpGet("id")]
		public async Task<ActionResult<IEnumerable<ProductToReturnDto>>> GetProduct(int id)
		{
            var spec = new ProductWithBrandAndCategorySpecifications(id);

            var product =await _productRepository.GetWithSpecAsync(spec);
			if(product == null)
				return NotFound(new ApisResponse(404));
			return Ok(_mapper.Map<Product, ProductToReturnDto>(product));
		}
		[HttpGet("brands")]
		public async Task<ActionResult<IEnumerable<ProductBrand>>> GetBrands()
		{
			var brands =await _brandRepository.GetAllAsync();
			return Ok(brands);
		}
        [HttpGet("categories")]
        public async Task<ActionResult<IEnumerable<ProductCategory>>> GetCategories()
        {
            var category = await _categoryiesRepository.GetAllAsync();
            return Ok(category);
        }

    }
}
