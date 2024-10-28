using Market.Data;
using Market.Model;

namespace Market.Services.Repository.Repository
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