using Market.Model;

namespace Market.Services.Repository.IRepository
{
    public interface IProductRepository:IBaseRepository<Product>
    {
        Task<Product> GetProductByIdAsync(int id);
        Task<Product> FindByNameAsync(string name);
        Task<IEnumerable<Product>> GetAllProductsAsync();
    }
}
