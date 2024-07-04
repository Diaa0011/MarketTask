using Market.Models;

namespace Market.Services.Repository.IRepository
{
    public interface IStoreRepository:IBaseRepository<Store>
    {
        Task<Store> GetStoreByIdWithProductsAsync(int id);
        Task<IEnumerable<Store>> GetAllStoresWithProductsAsync();

        Task<Store> FindByNameAsync(string name);
    }
    
}
