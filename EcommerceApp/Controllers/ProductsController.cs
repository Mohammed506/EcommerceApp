using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Ecommerce.Domain.Shared.Interfaces;
using Ecommerce.Domain.Shared.Entites;
using Ecommerce.Domain.Shared.DTO;

namespace EcommerceApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
         
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        /// <summary>
        /// This end point returns all of the product in the DataBase . 
        /// </summary>
        /// <returns></returns>
        // GET: /Products
        [HttpGet]
        public async Task<ActionResult<List<Product>>> Get()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }
        /// <summary>
        /// This end point returns a specific product by providing it's Id .
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: /Products/{id}
        [HttpGet("{id:length(24)}", Name = "GetProduct")]
        public async Task<ActionResult<Product>> Get(string id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        /// <summary>
        /// This end point creates new product .
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        // POST: /Products
        [HttpPost]
        public async Task<ActionResult<Product>> Create(CreateProductRequest productRequest)
        {
          var product =   await _productService.CreateProductAsync(productRequest);
            return Ok(product);
        }
        /// <summary>
        /// This end point update a product , by providing it's Id . 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="productIn"></param>
        /// <returns></returns>
        // PUT: /Products/{id}
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Product productIn)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            await _productService.UpdateProductAsync(id, productIn);
            return NoContent();
        }
        /// <summary>
        /// This end point deletes a product , by providing it's Id . 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: /Products/{id}
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _productService.DeleteProductAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
        /// <summary>
        /// this end point simulate the purchase of a single product by reducing the quantity . 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // POST: /Products/{id}/Purchase
        [HttpPost("{id:length(24)}/purchase")]
        [Authorize]
        public async Task<IActionResult> Purchase(string id)
        {
            var success = await _productService.PurchaseProductAsync(id);
            if (!success)
            {
                return BadRequest("Product is out of stock or does not exist.");
            }

            return Ok(new { message = "Purchase simulated successfully.", productId = id });
        }
    }
}
