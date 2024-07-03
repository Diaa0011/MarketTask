using Market.Data;
using Market.Models;
using Market.Services.Repository.IRepository;

namespace Market.Services.Repository
{
    public class StoreRepository : BaseRepository<Store>, IStoreRepository
    {
        public StoreRepository(AppDbContext db):base(db)
        {
            
        }

    }
}
