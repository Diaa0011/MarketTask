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
        public async Task<IEnumerable<CartItem>> GetAllCartItemsAsync()
        {
            return await _db.CartItems
                                 .Include(ci => ci.product)
                                 .Include(ci => ci.cart)
                                 .ToListAsync();
        }
    }
}
