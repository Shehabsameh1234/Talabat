using AminDashboard.Helpers;
using AminDashboard.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Specifications.productSpecifications;

namespace AminDashboard.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            
            var products= await _unitOfWork.Repository<Product>().GetAllAsync();

            var mappedProducts= _mapper.Map<IReadOnlyList<Product>,IReadOnlyList<ProductViewModel>>(products);

            return View(mappedProducts);
        }
		public  IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductViewModel productVm)
        {
            if(ModelState.IsValid)
            {
                if (productVm.Image != null )
                {
                    string fileExtension = Path.GetExtension(productVm.Image.FileName);
                    if (fileExtension != ".jpg")
                    {
                        ModelState.AddModelError(string.Empty, "Please upload file with .jpg extension only.");
                        return View(productVm);
                    }
                    else
                    {
                        productVm.PictureUrl = PictureSetting.UploadFile(productVm.Image, "products");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Please Chose photo");
                    return View(productVm);
                }
                var mappedProduct= _mapper.Map<ProductViewModel,Product>(productVm);
                 mappedProduct.PictureUrl = "images/products/" + productVm.PictureUrl;
                _unitOfWork.Repository<Product>().Add(mappedProduct);
                await _unitOfWork.CompleteAsync();
                return RedirectToAction("Index");
            }
            else return View(productVm);
        }

	}
}
