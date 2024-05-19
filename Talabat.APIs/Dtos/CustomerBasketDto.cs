using System.ComponentModel.DataAnnotations;
using Talabat.Core.Entities;

namespace Talabat.APIs.Dtos
{
    public class CustomerBasketDto
    {
		[Required]
		public string Id { get; set; }
		public List<BasketItems> Items { get; set; }
		public string? PaymentIntentId { get; set; } = null!;
		public string? ClientSecret { get; set; } = null!;
		public decimal ShippingPrice { get; set; }
		public int? DeliveryMethodId { get; set; }
	}
}
