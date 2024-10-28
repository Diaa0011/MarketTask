using Market.Data;
using Market.Dtos.Product;
using Market.Model;
using Market.Services.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Market.Services.Repository
{
    public class CartRepository : BaseRepository<Cart>, ICartRepository
    {
        private readonly AppDbContext _db;
        public CartRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task<IEnumerable<Cart>> GetAllCartsAsync()
        {
            return await _db.Carts
                .Include(c => c.CartItems)
                .ToListAsync();
        }

        public async Task<Cart> GetCartByIdAsync(int cartId)
        {
            return await _db.Carts.Include(c => c.CartItems)
                                  .FirstOrDefaultAsync(c => c.CartId == cartId);
        }

        public async Task UpdateCartAsync(Cart cart)
        {
            _db.Carts.Update(cart);
            await _db.SaveChangesAsync();
        }
        public bool HasCartItems(int cartId)
        {
            return _db.CartItems.Any(ci => ci.cart.CartId == cartId);
        }
    }
}
