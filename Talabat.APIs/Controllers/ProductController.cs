﻿using AutoMapper;
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
        private readonly IMapper _mapper;

        public ProductController(IGenericRepository<Product> productRepository,IMapper mapper)
        {
			_productRepository = productRepository;
            _mapper = mapper;
        }
		[HttpGet]
		public async Task<ActionResult<IEnumerable<ProductToReturnDto>>> GetProducts()
		{
			var spec =new ProductWithBrandAndCategorySpecifications();

			var products = await _productRepository.GetAllWithSpecAsync(spec);
			return Ok(_mapper.Map<IEnumerable<Product>,IEnumerable<ProductToReturnDto>>(products));
		}
		[HttpGet("id")]
		public async Task<ActionResult<IEnumerable<ProductToReturnDto>>> GetProduct(int id)
		{
            var spec = new ProductWithBrandAndCategorySpecifications(id);

            var product =await _productRepository.GetWithSpecAsync(spec);
			if(product == null)
				return NotFound(new ApisResponse(404));
			return Ok(_mapper.Map<Product, ProductToReturnDto>(product));
		}
	}
}
