namespace Market.Services.Repository.IRepository
{
    public interface IUnitOfWork
    {
        public IProductRepository Products { get; }
        public IStoreRepository Stores { get; }
        void Save();
    }
}
