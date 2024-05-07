using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.APIs.Dtos
{
    public class OrderToReturnDto
    {
        public int Id { get; set; }
        public string BuyerEmail { get; set; } = null!;
        public DateTimeOffset OrederDate { get; set; } 
        public string Status { get; set; } = null!;
        public OrderAddress ShippingAddress { get; set; } = null!;
        public string DeliveryMethod { get; set; } = null!;
        public decimal DeliveryMethodCost { get; set; } 

        public ICollection<OrderItemDto> OrderItems { get; set; } = new HashSet<OrderItemDto>();
        
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
        public string PaymentIntitId { get; set; } = string.Empty;
    }
}
