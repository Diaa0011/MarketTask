using Market.Data;
using Market.Services.Repository.IRepository;
using Model.Client;

namespace Market.Services.Repository.Repository
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