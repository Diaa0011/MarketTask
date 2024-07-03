using Market.Data;
using Market.Services.Repository.IRepository;

namespace Market.Services.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        
        public IProductRepository Products { get; private set; }

        public IStoreRepository Stores { get; private set; }
         
        public UnitOfWork(AppDbContext context)
        {
            _context = context;

            Products = new ProductRepository(_context);

            Stores = new StoreRepository(_context);

        }

        public void Save()
        {
            _context.SaveChangesAsync();
        }

    }
}
