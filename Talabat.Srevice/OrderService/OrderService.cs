using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.Service.Contract;

namespace Talabat.Srevice.OrderService
{
    public class OrderService : IOrderService
    {
        public Task<Order> CreateOrderAsync(string basketId, string buyerEmail, Address shippingAddress, int deliveryMethodId)
        {
            throw new NotImplementedException();
        }
        public Task<Order> GetOrderByIdForUser(int orderId, string buyerEmail)
        {
            throw new NotImplementedException();
        }
        public Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            throw new NotImplementedException();
        }
        public Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
