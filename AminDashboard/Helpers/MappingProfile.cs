using AminDashboard.Models;
using AutoMapper;
using Talabat.APIs.Helpers;
using Talabat.Core.Entities;

namespace AminDashboard.Helpers
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Product,ProductViewModel>()
                .ReverseMap();
        }
    }
}
