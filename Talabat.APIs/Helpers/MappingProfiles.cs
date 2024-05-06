using AutoMapper;
using Talabat.APIs.Dtos;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.APIs.Helpers
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
                    .ForMember(p=>p.Brand,o=>o.MapFrom(s=>s.Brand.Name))
                    .ForMember(p => p.Category, o => o.MapFrom(s => s.Category.Name))
                    .ForMember(p => p.PictureUrl, o => o.MapFrom<ProductPictureUrlResolver>());
            CreateMap<CustomerBasketDto,CustomerBasket>();
            CreateMap<BasketItemsDto, BasketItems>();

            CreateMap<Address, AddressDto>().ReverseMap();

            CreateMap<AddressDto,OrderAddress>();



        }
    }
}
