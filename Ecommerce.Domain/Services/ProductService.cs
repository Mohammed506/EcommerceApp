using MongoDB.Driver;
using Ecommerce.Domain.Shared.Interfaces;
using Ecommerce.Domain.Shared.Entites;
using Ecommerce.Domain.Shared.DTO;
using Ecommerce.DB.Data;

namespace EcommerceApp.Domain.Services
{
    

    public class ProductService:IProductService
    {
        
        private readonly MongoDBContext _context;

        public ProductService(MongoDBContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _context.Products.Find(product => true).ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(string id)
        {
            return await _context.Products.Find<Product>(product => product.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Product> CreateProductAsync(CreateProductRequest productRequest)
        {
            var product = new Product{
                Description = productRequest.Description , 
                Price = productRequest.Price , 
                Quantity = productRequest.Quantity ,
                Name = productRequest.Name 
            } ; 

            await _context.Products.InsertOneAsync(product);
            return product ; 
        }

        public async Task UpdateProductAsync(string id, Product productIn)
        {
            await _context.Products.ReplaceOneAsync(product => product.Id == id, productIn);
        }

        public async Task<bool> DeleteProductAsync(string id)
        {
            var result = await _context.Products.DeleteOneAsync(product => product.Id == id);
            return result.DeletedCount > 0;
        }

        public async Task<bool> PurchaseProductAsync(string id)
        {
            var product = await GetProductByIdAsync(id);
            if (product == null || product.Quantity <= 0)
            {
                return false;
            }

            var update = Builders<Product>.Update.Inc(p => p.Quantity, -1);
            await _context.Products.UpdateOneAsync(p => p.Id == id, update);
            return true;
        }
    }
}
