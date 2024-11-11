using Market.Dtos.Cart;

namespace Market.Services.Service.IService
{
    public interface ICartService
    {
        Task<CartReadDto> GetCart(string clientId);
        Task<bool> RemoveCart();
    }
}