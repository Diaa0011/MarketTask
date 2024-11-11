using Market.Dtos.CartItemDto;

namespace Services.Service.IService
{
    public interface ICartItemService
    {
        Task<bool> AddToCart(string clientId,CartItemCreateDto cartItemCreateDto);
        Task<bool> RemoveFromCart();
        /*
        Task<bool> AddToCart(CartRequestDTO cartRequest);
        Task<bool> RemoveFromCart(int cartId);
        Task<bool> ClearCart();
        Task<IEnumerable<CartDTO>> GetCart();
        */
    }
}