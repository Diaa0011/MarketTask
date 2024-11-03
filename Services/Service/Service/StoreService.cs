using System.Security.Claims;
using AutoMapper;
using Market.Dtos.Store;
using Market.Model;
using Market.Models;
using Market.Services.Repository.IRepository;
using Market.Services.Service.IService;
using Microsoft.AspNetCore.JsonPatch;


namespace Market.Services.Service
{
    public class StoreService : IStoreService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public StoreService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StoreReadDto>> GetAllStoresForMerchant(string merchant_Id)
        {
           // var stores_fetch = await _unitOfWork.Stores.GetAllStoresWithProductsAsync();
            
           // var stores_by_merchant = await _unitOfWork.Stores.FindAllAsync(c => c.MerchantId == user_Id);
            var stores_by_merchant = await _unitOfWork.Stores.GetStoresByMerchantIdAsync(merchant_Id);
            var stores = _mapper.Map<IEnumerable<StoreReadDto>>(stores_by_merchant);
            return stores;
        }
        public async Task<StoreReadDto> GetStoreByIdAsync(int id,string merchant_Id)
        {
            var store_fetch = await _unitOfWork.Stores.GetStoreByIdAsync(id,merchant_Id);
            
            var store = _mapper.Map<StoreReadDto>(store_fetch);
            
            return store;
        }
        public async Task<StoreReadDto> GetStoreByIdWithProductsAsync(int id,string merchant_Id)
        {
            var store_fetch = await _unitOfWork.Stores.GetStoreByIdWithProductsAsync(id,merchant_Id);
            
            var store = _mapper.Map<StoreReadDto>(store_fetch);
            
            return store;
        }

        public async Task<StoreReadDto> FindByNameAsync(string name, string merchant_Id)
        {
            var store_by_name_merchant = await _unitOfWork.Stores.FindByNameAsync(name, merchant_Id);
            
            var store = _mapper.Map<StoreReadDto>(store_by_name_merchant);

            return store;
        }
        public async Task<StoreReadDto> CreateStoreAsync(StoreCreateDto storeCreateDto, string merchant_Id)
        {
            var store = _mapper.Map<Store>(storeCreateDto);

            var merchant = _unitOfWork.Merchants.Find(m => m.Id == merchant_Id);

            if (merchant == null)
            {
                throw new Exception("Merchant not found.");
            }

            store.MerchantId = merchant_Id;
            store.Merchant = merchant;

            await _unitOfWork.Stores.AddAsync(store);
            await _unitOfWork.SaveAsync();

            var store_read = _mapper.Map<StoreReadDto>(store);

            return store_read;
        }

        public async Task<StoreReadDto> UpdateStoreAsync(int id,JsonPatchDocument<StoreEditDto> patchDoc, string merchant_Id)
        {
            var store = await _unitOfWork.Stores.GetStoreByIdAsync(id, merchant_Id);

            if (store == null)
            {
                throw new Exception("Store not found.");
            }
            var StoreToPatch = _mapper.Map<StoreEditDto>(store); 
            patchDoc.ApplyTo(StoreToPatch);

            _mapper.Map(StoreToPatch, store);

            _unitOfWork.Stores.Update(store);
            await _unitOfWork.SaveAsync();

            var store_read = _mapper.Map<StoreReadDto>(store);

            return store_read;
        }

        public async Task<bool> DeleteStoreAsync(int id, string merchant_Id)
        {
            var store = await _unitOfWork.Stores.GetStoreByIdAsync(id, merchant_Id);

            if (store == null)
            {
                throw new Exception("Store not found.");
            }

            _unitOfWork.Stores.Delete(store);
            
            await _unitOfWork.SaveAsync();
            return true;
        }
    }
}