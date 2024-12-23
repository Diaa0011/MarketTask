using MarketTask.Domain.Entites;
using MarketTask.Infrastructure.Repository.IRepository; 
using MarketTask.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MarketTask.Infrastructure.Repository.Repository
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
