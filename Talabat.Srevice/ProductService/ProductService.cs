using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Service.Contract;
using Talabat.Core.Specifications.productSpecifications;

namespace Talabat.Srevice.ProductService
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IReadOnlyList<ProductBrand>> GetBrandsAsync()
        => await _unitOfWork.Repository<ProductBrand>().GetAllAsync();

        public async Task<IReadOnlyList<ProductCategory>> GetCategoriesAsync()
        => await _unitOfWork.Repository<ProductCategory>().GetAllAsync();

        public async Task<int> GetCountAsync([FromQuery] QuerySpecParams querySpec)
        {
            var specCount = new ProductWithFilterSpecCount(querySpec);

            var Count = await _unitOfWork.Repository<Product>().GetCountAsync(specCount);

            return Count;
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            var spec = new ProductWithBrandAndCategorySpecifications(id);

            var product = await _unitOfWork.Repository<Product>().GetWithSpecAsync(spec);

            return product;
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync([FromQuery] QuerySpecParams querySpec)
        {
            var spec = new ProductWithBrandAndCategorySpecifications(querySpec);

            var products = await _unitOfWork.Repository<Product>().GetAllWithSpecAsync(spec);

            return products;
        }
    }
}
