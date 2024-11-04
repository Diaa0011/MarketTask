using Market.Data;
using Market.Models;
using Market.Services.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Market.Services.Repository
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        private readonly AppDbContext _db;
        public ProductRepository(AppDbContext db):base(db)
        {
            _db = db;
        }
        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _db.Products.Include(p => p.store).FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _db.Products.Include(p => p.store).ToListAsync();
        }
        public async Task<Product> FindByNameAsync(string name)
        {
            return await _db.Products.Include(p => p.store.Id).FirstOrDefaultAsync(p => p.Name == name);
        }


    }
}
