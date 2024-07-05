namespace Market.Services.Repository.IRepository
{
    public interface IUnitOfWork
    {
        public IProductRepository Products { get; }
        public IStoreRepository Stores { get; }

        public ICartItemRepository CartItems { get;}

        public ICartRepository Carts { get; }
        void Save();
        Task<int> SaveAsync();
    }
}
