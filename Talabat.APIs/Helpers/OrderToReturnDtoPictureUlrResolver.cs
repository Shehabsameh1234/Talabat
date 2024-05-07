using AutoMapper;
using Talabat.APIs.Dtos;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.APIs.Helpers
{
    public class OrderToReturnDtoPictureUlrResolver : IValueResolver<OrderItem,OrderItemDto, string>
    {
        private readonly IConfiguration _configuration;

        public OrderToReturnDtoPictureUlrResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
     
        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.ProductItem.PictureUrl))
                return $"{_configuration["AppUrl"]}/{source.ProductItem.PictureUrl}";
            return string.Empty;
        }
    }
}
