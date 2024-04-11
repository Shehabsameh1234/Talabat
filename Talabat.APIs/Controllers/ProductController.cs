﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.Repository.Contract;

namespace Talabat.APIs.Controllers
{
	
	public class ProductController : BaseApiController
	{
		private readonly IGenericRepository<Product> _productRepository;

		public ProductController(IGenericRepository<Product> productRepository)
        {
			_productRepository = productRepository;
		}
    }
}
