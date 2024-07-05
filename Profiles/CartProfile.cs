using AutoMapper;
using Market.Dtos.Cart;
using Market.Model;
using Market.Models;

namespace Market
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