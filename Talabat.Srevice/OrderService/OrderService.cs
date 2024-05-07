﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.Repository.Contract;
using Talabat.Core.Service.Contract;
using Talabat.Core.Specifications;
using OrderAddress = Talabat.Core.Entities.Order_Aggregate.OrderAddress;

namespace Talabat.Srevice.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly IBasektRepository _basektRepo;
        private readonly IUnitOfWork _unitOfWork;

        ///private readonly IGenericRepository<Product> _productRepo;
        ///private readonly IGenericRepository<DeliveryMethod> _deliveryMethodsRepo;
        ///private readonly IGenericRepository<Order> _orderRepo;

        public OrderService(
            IBasektRepository basektRepo,
            IUnitOfWork unitOfWork
            ///IGenericRepository<Product> productRepo,
            ///IGenericRepository<DeliveryMethod> deliveryMethodsRepo,
            ///IGenericRepository<Order> orderRepo
            ///


            )
        {
            _basektRepo = basektRepo;
            _unitOfWork = unitOfWork;
            ///_productRepo = productRepo;
            ///_deliveryMethodsRepo = deliveryMethodsRepo;
            ///_orderRepo = orderRepo;
        }
        public async Task<Order?> CreateOrderAsync(string basketId, string buyerEmail, OrderAddress shippingAddress, int deliveryMethodId)
        {
            // 1.Get Basket From Baskets Repo
            var basket= await _basektRepo.GetBasketAsync(basketId);

            // 2. Get Selected Items at Basket From Products Repo
            var orderItems = new List<OrderItem>();
            if(basket?.Items?.Count > 0)
            {
                
                foreach (var item in basket.Items)
                {
                    var product = await _unitOfWork.Repository<Product>().GetAsync(item.Id);
                    var productItemOrder = new ProductItemOrder(product.id,product.Name,product.PictureUrl);
                    var orderItem = new OrderItem(productItemOrder,product.Price,item.Quantity);
                    orderItems.Add(orderItem);
                }

            }

            // 3. Calculate SubTotal
            var subTotal = orderItems.Sum(o => o.Price * o.Quantity);

            // 4. Get Delivery Method From DeliveryMethods Repo
            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetAsync(deliveryMethodId);

            // 5. Create Order
            var order = new Order(buyerEmail,shippingAddress
                ,deliveryMethod, orderItems,subTotal);

            // 6. Save To Database [TODO]
            _unitOfWork.Repository<Order>().Add(order);
            
            //save changes
            var result=await _unitOfWork.CompleteAsync();
            if (result <= 0) return null;
            return order;
        }
        public async Task<Order?> GetOrderByIdForUser(int orderId, string buyerEmail)
        {
            var orderRepo = _unitOfWork.Repository<Order>();

            var spec =new OrderSpecifications(orderId,buyerEmail);

            var order = await orderRepo.GetWithSpecAsync(spec);

            return order;
        }
        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            var orderRepo = _unitOfWork.Repository<Order>();

            var spec = new OrderSpecifications(buyerEmail);

            var orders = await orderRepo.GetAllWithSpecAsync(spec);

            return orders;
        }
        public Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
