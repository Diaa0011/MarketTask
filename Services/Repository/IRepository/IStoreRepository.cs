using Market.Model;

namespace Market.Services.Repository.IRepository
{
    public interface IStoreRepository:IBaseRepository<Store>
    {
        Task<List<Store>> GetStoresByMerchantIdAsync(string merchantId);
        Task<Store> GetStoreByIdAsync(int id,string merchant_Id);
        Task<Store> GetStoreByIdWithProductsAsync(int id,string merchantId);
        Task<IEnumerable<Store>> GetAllStoresWithProductsAsync();

        Task<Store> FindByNameAsync(string name,string merchantId);
    }
    
}
