using MarketTask.Application.Dtos.Cart;

namespace MarketTask.Application.Services.IService
{
    public interface ICartService
    {
        Task<CartReadDto> GetCart(string clientId);
        Task<bool> RemoveCart();
    }
}