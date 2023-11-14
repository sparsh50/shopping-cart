using ECommerceAPI.Models;
using ECommerceAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,User")]
    public class AdminController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        public AdminController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        [HttpGet]

        public Tuple<List<Product>, int> Index([FromQuery] int pageNo, [FromQuery] string? searchString = "")
        {

            return _productRepository.GetProductsByFilteration( searchString, pageNo);
        }

        [HttpPost]
        public async Task<ActionResult> Create(ProductViewModel productModel)
        {
            Product product = new Product()
            {
                ProductName = productModel.ProductName,
                Description = productModel.Description,
                Price = productModel.Price,
                ImageUrl = productModel.ImageUrl,
                Quantity = productModel.Quantity,
                Rating = productModel.Rating,
                CategoryId = productModel.CategoryId,
                CreatedOn = DateTime.Now.ToString("yyyy-MM-dd"),
                CreatedBy = "Admin",
                UpdatedOn = DateTime.Now.ToString("yyyy-MM-dd"),
                UpdatedBy = "Admin"
            };
            _productRepository.Insert(product);
            _productRepository.Save();
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<Product> GetById(int id)
        {

            return _productRepository.GetById(id);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Edit(int id, ProductViewModel productModel)
        {
            Product product = new Product()
            {
                ProductId = productModel.ProductId,
                ProductName = productModel.ProductName,
                Description = productModel.Description,
                Price = productModel.Price,
                ImageUrl = productModel.ImageUrl,
                Quantity = productModel.Quantity,
                Rating = productModel.Rating,
                CategoryId = productModel.CategoryId,
                CreatedOn = DateTime.Now.ToString("yyyy-MM-dd"),
                CreatedBy = "Admin",
                UpdatedOn = DateTime.Now.ToString("yyyy-MM-dd"),
                UpdatedBy = "Admin"
            };

            if (id != product.ProductId)
            {
                return BadRequest();
            }
            _productRepository.Update(product);
            _productRepository.Save();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {

            Product product = _productRepository.GetById(id);
            if (product != null)
            {
                _productRepository.Delete(id);
                _productRepository.Save();
                return Ok();
            }
            return NotFound();
        }
    }
}
