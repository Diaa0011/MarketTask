namespace Services.Service.IService
{
    public interface ICartService
    {
        Task<bool> AddToCart();
        /*
        Task<bool> AddToCart(CartRequestDTO cartRequest);
        Task<bool> RemoveFromCart(int cartId);
        Task<bool> ClearCart();
        Task<IEnumerable<CartDTO>> GetCart();
        */
    }
}