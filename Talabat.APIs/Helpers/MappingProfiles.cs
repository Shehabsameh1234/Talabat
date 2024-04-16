﻿using AutoMapper;
using Talabat.APIs.Dtos;
using Talabat.Core.Entities;

namespace Talabat.APIs.Helpers
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
                    .ForMember(p=>p.Brand,o=>o.MapFrom(s=>s.Brand.Name))
                    .ForMember(p => p.Category, o => o.MapFrom(s => s.Category.Name));
        }
    }
}
