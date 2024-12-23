using MarketTask.Application.Dtos.Store;
using Microsoft.AspNetCore.JsonPatch;

namespace MarketTask.Application.Services.IService
{
    public interface IStoreService
    {
        
        Task<IEnumerable<StoreReadDto>> GetAllStoresForMerchant(string merchant_Id);
        Task<StoreReadDto> GetStoreByIdAsync(int id,string merchant_Id);
        Task<StoreReadDto> GetStoreByIdWithProductsAsync(int id,string merchant_Id);
        Task<StoreReadDto> FindByNameAsync(string name, string merchant_Id);
        Task<StoreReadDto> CreateStoreAsync(StoreCreateDto storeCreateDto, string merchant_Id);
        Task<StoreReadDto> UpdateStoreAsync(int id,JsonPatchDocument<StoreEditDto> patchDoc, string merchant_Id);
        Task<bool> DeleteStoreAsync(int id, string merchant_Id);
    }
}