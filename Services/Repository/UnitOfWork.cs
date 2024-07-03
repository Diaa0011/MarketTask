using Market.Data;
using Market.Services.Repository.IRepository;

namespace Market.Services.Repository
{
    public class UnitOfWork
    {
        private readonly AppDbContext _context;
        
        public IProductRepository Product { get; private set; }
        public UnitOfWork(AppDbContext context)
        {
            _context = context;

            Product = new ProductRepository(_context);

        }

    }
}
