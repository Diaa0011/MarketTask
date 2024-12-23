using AutoMapper;
using MarketTask.Application.Dtos.CartItemDto;
using MarketTask.Domain.Entites;

namespace MarketTask.Application.Profiles
{
    public class CartItemProfile:Profile
    {
        public CartItemProfile()
        {
            CreateMap<CartItemCreateDto, CartItem>();
            CreateMap<CartItem, CartItemReadDto>()
                .ForMember(dest => dest.cartId, opt => opt.MapFrom(src => src.cart.CartId));
            CreateMap<CartItemRemoveDto, CartItem>();
            CreateMap<CartItem, CartItemInCartDto>();
            CreateMap<CartItemEditDto, CartItem>();

        }
    }
}
