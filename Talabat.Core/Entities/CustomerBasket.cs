

namespace Talabat.Core.Entities
{
    public class CustomerBasket
    {
        public string Id { get; set; }
        public List<BasketItems> Items { get; set; }
        public string? PaymentIntentId { get; set; } = null!;
		public string? ClientSecret { get; set; } = null!;
		public decimal ShippingPrice { get; set; }
		public int? DeliveryMethodId { get; set; } 

		public CustomerBasket(string id)
        {
            Id= id;
            Items = new List<BasketItems>();
        }
    }
}
