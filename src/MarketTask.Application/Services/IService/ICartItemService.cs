using MarketTask.Application.Dtos.CartItemDto;

namespace MarketTask.Application.Services.IService
{
    public interface ICartItemService
    {
        Task<bool> AddToCart(string clientId,CartItemCreateDto cartItemCreateDto);
        Task<bool> RemoveFromCart(string clientId,CartItemRemoveDto cartItemCreateDto);
        /*
        Task<bool> AddToCart(CartRequestDTO cartRequest);
        Task<bool> RemoveFromCart(int cartId);
        Task<bool> ClearCart();
        Task<IEnumerable<CartDTO>> GetCart();
        */
    }
}