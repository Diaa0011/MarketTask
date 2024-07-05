using AutoMapper;
using Market.Dtos.Cart;
using Market.Dtos.CartItemDto;
using Market.Model;
using Market.Models;

namespace Market.Profiles
{
    public class CartItemProfile:Profile
    {
        public CartItemProfile()
        {
            CreateMap<CartItemCreateDto, CartItem>();  // Map cartId to Cart.CartId

        }
    }
}
