using AutoMapper;
using Market.Dtos.CartItemDto;
using Market.Model;

namespace Market.Profiles
{
    public class CartItemProfile:Profile
    {
        public CartItemProfile()
        {
            CreateMap<CartItemCreateDto, CartItem>();
            CreateMap<CartItem, CartItemReadDto>()
                .ForMember(dest => dest.cartId, opt => opt.MapFrom(src => src.cart.CartId));
            CreateMap<CartItem, CartItemInCartDto>();
            CreateMap<CartItemEditDto, CartItem>();

        }
    }
}
