
using MarketTask.Domain.Entites;
using AutoMapper;
using MarketTask.Application.Dtos.Store;
namespace MarketTask.Application.Profiles
{
    public class StoreProfile:Profile
    {
        public StoreProfile()
        {

            CreateMap<Store, StoreReadDto>()
            .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products));
            CreateMap<StoreCreateDto, Store>();
            CreateMap<StoreCreateDto, StoreReadDto>();

            CreateMap<StoreEditDto, Store>();
            CreateMap<Store, StoreEditDto>();
        }
    }
}