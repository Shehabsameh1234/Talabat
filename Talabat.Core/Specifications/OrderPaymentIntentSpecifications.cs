using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Core.Specifications
{

	public class OrderPaymentIntentSpecifications: BaseSpecifications<Order>
	{
		public OrderPaymentIntentSpecifications(string PaymentIntetId)
		   :base(o => o.PaymentIntitId == PaymentIntetId)
		{

		}
	}
	
}
