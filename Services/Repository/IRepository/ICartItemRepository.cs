using Market.Dtos.Product;
using Market.Model;

namespace Market.Services.Repository.IRepository
{
    public interface ICartItemRepository:IBaseRepository<CartItem>
    {
        Task<IEnumerable<CartItem>> GetAllCartItemsAsync();
        Task SetIdentityInsertAsync(string tableName, bool enable);
    }
}
