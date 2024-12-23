using MarketTask.Infrastructure.Data;
using MarketTask.Infrastructure.Repository.IRepository;
using MarketTask.Domain.Entites;

namespace MarketTask.Infrastructure.Repository.Repository
{
    public class ClientRepository:BaseRepository<Client>,IClientRepository
    {
        private readonly AppDbContext _db;
        public ClientRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }
    }
}