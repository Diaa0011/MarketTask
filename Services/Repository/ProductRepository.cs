using Market.Data;
using Market.Models;
using Market.Services.Repository.IRepository;

namespace Market.Services.Repository
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext db):base(db)
        {
            
        }

    }
}
