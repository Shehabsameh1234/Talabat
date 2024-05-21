using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Talabat.APIs.Errors;
using Talabat.Core.Entities;
using Talabat.Core.Service.Contract;

namespace Talabat.APIs.Controllers
{

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public class PaymentController : BaseApiController
	{
		private readonly IPaymentService _paymentService;

		// This is your Stripe CLI webhook secret for testing your endpoint locally.
		private const string WHSecret = "whsec_f339fbb28ce262b8a4b468659a7858bd63543eb87e823ed51f52b038b1b5decb";

		public PaymentController(IPaymentService paymentService)
        {
			_paymentService = paymentService;
		}

		[ProducesResponseType(typeof(CustomerBasket), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApisResponse), StatusCodes.Status400BadRequest)]
		[HttpGet("{basketId}")]
		public async Task<ActionResult<CustomerBasket>> CreateOrUpdatePaymentIntent(string basketId)
		{
			var basket =await _paymentService.CreateOrUpdatePaymentIntent(basketId);
			if (basket is null) return BadRequest(new ApisResponse(400,"An Error In Your Basket"));
			return Ok(basket);
		}


		[HttpPost("webhook")]
		public async Task<IActionResult> WebHook()
		{
			var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
		
				var stripeEvent = EventUtility.ConstructEvent(json,
					Request.Headers["Stripe-Signature"], WHSecret);

				var paymentIntent = (PaymentIntent) stripeEvent.Data.Object;

				// Handle the event

				switch (stripeEvent.Type)
				{
					case Events.PaymentIntentSucceeded:
						await _paymentService.UpdateOrderStatus(paymentIntent.Id, true);
						break;
					case Events.PaymentIntentPaymentFailed:
						await _paymentService.UpdateOrderStatus(paymentIntent.Id, false);
						break;
				}
			   return Ok();
		}

	}
}
