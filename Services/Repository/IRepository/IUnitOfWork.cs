using Market.Services.Repository.Repository;

namespace Market.Services.Repository.IRepository
{
    public interface IUnitOfWork
    {
        public IProductRepository Products { get; }
        public IStoreRepository Stores { get; }

        public ICartItemRepository CartItems { get;}

        public ICartRepository Carts { get; }
        public IMerchantRepository Merchants { get; }
        public IClientRepository Clients { get; }

        void Save();
        Task<int> SaveAsync();
    }
}
