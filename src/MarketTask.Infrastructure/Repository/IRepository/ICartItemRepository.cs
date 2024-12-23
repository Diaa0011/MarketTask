using MarketTask.Domain.Entites;
namespace MarketTask.Infrastructure.Repository.IRepository
{
    public interface ICartItemRepository:IBaseRepository<CartItem>
    {
        Task<IEnumerable<CartItem>> GetAllCartItemsAsync();
    }
}
