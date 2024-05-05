using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.Repository.Contract;
using Talabat.Core.Service.Contract;
using Address = Talabat.Core.Entities.Order_Aggregate.Address;

namespace Talabat.Srevice.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly IBasektRepository _basektRepo;
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<DeliveryMethod> _deliveryMethodsRepo;
        private readonly IGenericRepository<Order> _orderRepo;

        public OrderService(
            IBasektRepository basektRepo,
            IGenericRepository<Product> productRepo,
            IGenericRepository<DeliveryMethod> deliveryMethodsRepo,
            IGenericRepository<Order> orderRepo


            )
        {
            _basektRepo = basektRepo;
            _productRepo = productRepo;
            _deliveryMethodsRepo = deliveryMethodsRepo;
            _orderRepo = orderRepo;
        }
        public async Task<Order> CreateOrderAsync(string basketId, string buyerEmail, Address shippingAddress, int deliveryMethodId)
        {
            // 1.Get Basket From Baskets Repo
            var basket= await _basektRepo.GetBasketAsync(basketId);

            // 2. Get Selected Items at Basket From Products Repo
            var orderItems = new List<OrderItem>();
            if(basket?.Items?.Count > 0)
            {
                
                foreach (var item in basket.Items)
                {
                    var product = await _productRepo.GetAsync(item.Id);
                    var productItemOrder = new ProductItemOrder(product.id,product.Name,product.PictureUrl);
                    var orderItem = new OrderItem(productItemOrder,product.Price,item.Quantity);
                    orderItems.Add(orderItem);
                }

            }

            // 3. Calculate SubTotal
            var subTotal = orderItems.Sum(o => o.Price * o.Quantity);

            // 4. Get Delivery Method From DeliveryMethods Repo
            var deliveryMethod = await _deliveryMethodsRepo.GetAsync(deliveryMethodId);

            // 5. Create Order
            var order = new Order(buyerEmail,shippingAddress
                ,deliveryMethod, orderItems,subTotal);

            // 6. Save To Database [TODO]
            _orderRepo.Add(order);
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
