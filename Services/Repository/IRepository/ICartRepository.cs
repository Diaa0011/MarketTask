using Market.Dtos.Product;
using Market.Model;

namespace Market.Services.Repository.IRepository
{
    public interface ICartRepository:IBaseRepository<Cart>
    {
        //Task AddProductToCartAsync(int cartId, ProductToCartDto productToCartDto);
        Task<IEnumerable<Cart>> GetAllCartsAsync();
        Task<Cart> GetCartByIdAsync(int cartId);
        Task UpdateCartAsync(Cart cart);
        public bool HasCartItems(int cartId);



    }
}
