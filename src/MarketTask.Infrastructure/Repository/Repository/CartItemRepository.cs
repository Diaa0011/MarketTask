using MarketTask.Infrastructure.Data;
using MarketTask.Domain.Entites;
using MarketTask.Infrastructure.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace MarketTask.Infrastructure.Repository.Repository
{
    public class CartItemRepository:BaseRepository<CartItem>,ICartItemRepository
    {
        private readonly AppDbContext _db;

        public CartItemRepository(AppDbContext db):base(db)
        {
            _db = db;
        }
        public async Task<IEnumerable<CartItem>> GetAllCartItemsAsync()
        {
            return await _db.CartItems
                                 .Include(ci => ci.Product)
                                 .Include(ci => ci.cart)
                                 .ToListAsync();
        }
    }
}
