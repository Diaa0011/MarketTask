using Market.Dtos.Store;
using Market.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace Market.Services.Service.IService
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