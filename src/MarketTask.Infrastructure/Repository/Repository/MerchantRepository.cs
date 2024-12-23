using MarketTask.Infrastructure.Data;
using MarketTask.Infrastructure.Repository.IRepository;
using MarketTask.Domain.Entites;

namespace MarketTask.Infrastructure.Repository.Repository
{
    public class MerchantRepository : BaseRepository<Merchant>, IMerchantRepository
    {
       private readonly AppDbContext _db;
        public MerchantRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }
    }

    
}