using Market.Data;
using Market.Model;
using Market.Services.Repository.IRepository;
using Market.Services.Repository.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace Market.Services.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        
        public IProductRepository Products { get; private set; }

        public IStoreRepository Stores { get; private set; }

        public ICartItemRepository CartItems { get; private set; }

        public ICartRepository Carts { get; private set; }

        public IMerchantRepository Merchants { get; private set; }
        public IClientRepository Clients { get; private set; }

         
        public UnitOfWork(AppDbContext context)
        {
            _context = context;

            Products = new ProductRepository(_context);

            Stores = new StoreRepository(_context);

            CartItems = new CartItemRepository(_context);

            Carts = new CartRepository(_context);

            Merchants = new MerchantRepository(_context);

            Clients = new ClientRepository(_context);


        }

        public void Save()
        {
            _context.SaveChangesAsync();
        }

        public async Task<int> SaveAsync()
        {
             return await _context.SaveChangesAsync();
        }

    }
}
