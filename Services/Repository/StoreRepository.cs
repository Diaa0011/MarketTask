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
        public async Task<IEnumerable<Store>> GetAllStoresWithProductsAsync()
        {
            return await _db.Stores
                .Include(s => s.Products)
                .ToListAsync();
        }
        public async Task<Store> GetStoreByIdWithProductsAsync(int id)
        {
            return await _db.Stores
                .Include(s => s.Products)
                .SingleOrDefaultAsync(s => s.Id == id);
        }
        public async Task<Store> FindByNameAsync(string name)
        {
            return await _db.Stores.FirstOrDefaultAsync(p => p.Name == name);
        }


    }
}
