using Market.Dtos.Product;
using Market.Dtos.Store;
using Market.Models;
using AutoMapper;
namespace Market
{
    public class StoreProfile:Profile
    {
        public StoreProfile()
        {

            CreateMap<Store, StoreReadDto>()
            .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products));
            CreateMap<StoreCreateDto, Store>();
            CreateMap<StoreEditDto, Store>();
        }
    }
}