using Market.Data;
using Market.Model;
using Market.Services.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Market.Services.Repository
{
    public class CartItemRepository:BaseRepository<CartItem>,ICartItemRepository
    {
        private readonly AppDbContext _db;

        public CartItemRepository(AppDbContext db):base(db)
        {
            _db = db;
        }
        
    }
}
