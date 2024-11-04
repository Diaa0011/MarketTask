using Market.Dtos.Product;

namespace Services.Service.IService
{
    public interface IProductService
    {
        /*Task<IEnumerable<ProductReadDto>> GetAllProducts();
        Task<ProductReadDto> GetProductById(int productId);*/
        Task<ProductReadDto> CreateProduct(ProductCreateDto productDto);
        /*Task<ProductReadDto> UpdateProduct(ProductEditDto productDto);
        Task<bool> DeleteProduct(int productId);*/
    }
}