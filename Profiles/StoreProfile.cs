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

            CreateMap<Store, StoreReadDto>();
            CreateMap<StoreCreateDto, Store>();
            CreateMap<StoreEditDto, Store>();
        }
    }
}