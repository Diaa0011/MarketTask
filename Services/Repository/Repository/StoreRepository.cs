using AutoMapper;
using Market.Data;
using Market.Dtos.Store;
using Market.Models;
using Market.Services.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Market.Services.Repository
{
    public class StoreRepository : BaseRepository<Store>, IStoreRepository
    {
        private readonly AppDbContext _db;

        public StoreRepository(AppDbContext db):base(db)
        {
            _db = db;
        }
        public async Task<List<Store>> GetStoresByMerchantIdAsync(string merchantId)
        {
            return await _db.Stores
                .Where(s => s.MerchantId == merchantId)
                .Include(s => s.Products) // Include related products if needed
                .ToListAsync();
        }
        public async Task<IEnumerable<Store>> GetAllStoresWithProductsAsync()
        {
            return await _db.Stores
                .Include(s => s.Products)
                .ToListAsync();
        }
        public async Task<Store> GetStoreByIdAsync(int id,string merchantId)
        {
            return await _db.Stores
                .Where(s=>s.MerchantId==merchantId)
                .SingleOrDefaultAsync(s => s.Id == id);
        }
        public async Task<Store> GetStoreByIdWithProductsAsync(int id,string merchantId)
        {
            return await _db.Stores
                .Where(s=>s.MerchantId==merchantId)
                .Include(s => s.Products)
                .SingleOrDefaultAsync(s => s.Id == id);
        }
        public async Task<Store> FindByNameAsync(string name,string merchantId)
        {
            return await _db.Stores
                        .Where(s => s.MerchantId == merchantId)
                        .FirstOrDefaultAsync(p => p.Name == name);
        }


    }
}
