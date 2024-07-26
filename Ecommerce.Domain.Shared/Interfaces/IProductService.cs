using Ecommerce.Domain.Shared.DTO;
using Ecommerce.Domain.Shared.Entites;

namespace Ecommerce.Domain.Shared.Interfaces
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(string id);
        Task<Product> CreateProductAsync(CreateProductRequest product);
        Task UpdateProductAsync(string id, Product productIn);
        Task<bool> DeleteProductAsync(string id);
        Task<bool> PurchaseProductAsync(string id);
    }
}
