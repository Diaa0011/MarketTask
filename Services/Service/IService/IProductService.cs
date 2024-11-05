using Market.Dtos.Product;
using Market.Dtos.Product;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Services.Service.IService
{
    public interface IProductService
    {
        Task<IEnumerable<ProductWithStoreReadDto>> GetAllProducts();
        Task<ProductWithStoreReadDto> GetProductById(int productId);
        Task<ProductReadDto> CreateProduct(ProductCreateDto productDto);
        Task<ProductReadDto> UpdateProduct(int id,[FromBody]JsonPatchDocument<ProductEditDto> patchDoc);
        Task<bool> DeleteProduct(int productId);
    }
}