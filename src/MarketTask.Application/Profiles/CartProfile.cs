using AutoMapper;
using MarketTask.Application.Dtos.Cart;
using MarketTask.Domain.Entites;

namespace MarketTask.Application.Profiles
{
    public class CartProfile : Profile
    {
        public CartProfile()
        {
            // Source -> Target
            CreateMap<Cart, CartReadDto>()
                .ForMember(dest=>dest.CartItems, opt=>opt.MapFrom(src=>src.CartItems));
            CreateMap<CartCreateDto, Cart>();

        }
    }
}