using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.Repository.Contract;
using Talabat.Core.Service.Contract;
using Product = Talabat.Core.Entities.Product;

namespace Talabat.Srevice.PaymentService
{
	public class PaymentService : IPaymentService
	{
		private readonly IConfiguration _configuration;
		private readonly IBasektRepository _basektRepository;
		private readonly IUnitOfWork _unitOfWork;

		public PaymentService(
			IConfiguration configuration,
			IBasektRepository basektRepository,
			IUnitOfWork unitOfWork

			)
        {
			_configuration = configuration;
			_basektRepository = basektRepository;
			_unitOfWork = unitOfWork;
		}
        public async Task<CustomerBasket?> CreateOrUpdatePaymentIntent(string basketId)
		{
			StripeConfiguration.ApiKey = _configuration["StripeSettings:SecretKey"];

			var basket =await  _basektRepository.GetBasketAsync(basketId);
			if( basket == null )  return null;

			var ShippingPrice = 0m;

			if (basket.DeliveryMethodId.HasValue)
			{
				var deliveryMethod=await _unitOfWork.Repository<DeliveryMethod>().GetAsync(basket.DeliveryMethodId.Value);
				ShippingPrice = deliveryMethod.Cost;
				basket.ShippingPrice = ShippingPrice;

			}

			if(basket.Items?.Count() > 0 )
			{
				var productRepo = _unitOfWork.Repository<Product>();
                foreach (var item in basket.Items)
                {
                    var product =await productRepo.GetAsync(item.Id);
					if (item.Price != product?.Price)
						item.Price = product.Price;
                }
            }

			PaymentIntent paymentIntent;
			PaymentIntentService paymentIntentService=new PaymentIntentService();

			if(string.IsNullOrEmpty(basket.PaymentIntentId)) //create new intent
			{
				var options = new PaymentIntentCreateOptions()
				{
					Amount = (long) basket.Items.Sum(I => I.Price * I.Quantity) + (long)ShippingPrice*100,
					Currency ="usd",
					PaymentMethodTypes=new List<string> { "card"}

				};
				paymentIntent =await paymentIntentService.CreateAsync(options);
				basket.PaymentIntentId = paymentIntent.Id;
				basket.ClientSecret = paymentIntent.ClientSecret;

			}
			else //update
			{
				var options = new PaymentIntentUpdateOptions()
				{
					Amount = (long)basket.Items.Sum(I => I.Price * I.Quantity) + (long)ShippingPrice * 100,

				};
				await paymentIntentService.UpdateAsync(basket.PaymentIntentId, options);	
			}

			await _basektRepository.UpdateBasketAsync(basket);

			return basket;
		}
	}
}
