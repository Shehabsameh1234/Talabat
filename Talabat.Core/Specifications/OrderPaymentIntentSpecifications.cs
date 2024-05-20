using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
