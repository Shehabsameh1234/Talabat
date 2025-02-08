using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Core.Specifications
{
    public class OrderSpecifications :BaseSpecifications<Order>
    {
        public OrderSpecifications(string buyerEmail)
            :base(o=>o.BuyerEmail==buyerEmail)
        {
            Includes.Add(o => o.DeliveryMethod);
            Includes.Add(o => o.OrderItems);
            AddOrderByDesc(o => o.OrederDate);

        }
        public OrderSpecifications(int orderId,string buyerEmail)
        : base(o=>o.id==orderId&&o.BuyerEmail == buyerEmail)
        {
            Includes.Add(o => o.DeliveryMethod);
            Includes.Add(o => o.OrderItems);
           
        }
		
	}
}
