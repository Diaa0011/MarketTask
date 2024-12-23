﻿using Market.Dtos.Product;
using Market.Model;

namespace Market.Services.Repository.IRepository
{
    public interface ICartRepository:IBaseRepository<Cart>
    {
        //Task AddProductToCartAsync(int cartId, ProductToCartDto productToCartDto);
        Task<IEnumerable<Cart>> GetAllCartsAsync();
        Task<Cart> GetCartAsync(int cartId);
        Task<Cart> GetCartByClientIdAsync(string clientId);
        Task UpdateCartAsync(Cart cart);
        public bool HasCartItems(int cartId);



    }
}
