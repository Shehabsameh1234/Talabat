using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core;
using Talabat.Core.Entities;

namespace AminDashboard.Controllers
{
	public class ProductBrandController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public ProductBrandController(IUnitOfWork unitOfWork,IMapper mapper)
        {
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}
        public async Task<IActionResult> Index()
		{
			var brands= await _unitOfWork.Repository<ProductBrand>().GetAllAsync();
			return View(brands);
		}
		[HttpPost]
		public async Task<IActionResult> Create(ProductBrand brand)
		{
			if (brand != null)
			{
				try
				{
					_unitOfWork.Repository<ProductBrand>().Add(brand);
					await _unitOfWork.CompleteAsync();
					return RedirectToAction("Index");
				}
				catch (Exception)
				{
					ModelState.AddModelError("Name", "brand is exist");
				    return View("Index" , await _unitOfWork.Repository<ProductBrand>().GetAllAsync()); 
                }
			}
		    return View(brand);

		}

        public async Task<IActionResult> Delete(int id)
		{
			var brand =await _unitOfWork.Repository<ProductBrand>().GetAsync(id);
			return View(brand);
		}
		[HttpPost]
		public async Task<IActionResult> Delete(ProductBrand brand)
		{
			_unitOfWork.Repository<ProductBrand>().Delete(brand);
			await _unitOfWork.CompleteAsync();
			return RedirectToAction("Index");
		}
	}
}
