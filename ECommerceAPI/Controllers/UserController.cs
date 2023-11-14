using ECommerceAPI.Models;
using ECommerceAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ECommerceAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "User")]
    public class UserController : ControllerBase
    {
        private readonly ICartRepository _cartItemsRepository;
        private readonly IProductRepository _productRepository;

        public UserController(ICartRepository cartItemsRepository, IProductRepository productRepository)
        {
            _cartItemsRepository = cartItemsRepository;

            _productRepository = productRepository;

        }
        [HttpGet]
        public List<Product> Index()
        {

            return _productRepository.GetAll().ToList();
        }

        [HttpPost]
        public async Task<IActionResult> AddProductToCart(ProductViewModel productModel)
        {

            try
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                CartItem cartItem = new CartItem()
                {
                    ProductId = productModel.ProductId,
                    Quantity = productModel.Quantity,
                    UserId = userId
                };
                _cartItemsRepository.Insert(cartItem);
                _cartItemsRepository.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound();

            }
            return Ok();
        }

        [HttpGet]
        public List<CartItemModel> GetCartItems()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return _cartItemsRepository.GetCartItemsByUser(userId);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveCartItem(int id)
        {
            try
            {
                _cartItemsRepository.Delete(id);
                _cartItemsRepository.Save();
            }
            catch
            {
                return BadRequest();
            }
            return Ok();

        }
        [HttpGet]
        public async Task<IActionResult> CheckOut()
        {
            try
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                List<CartItemModel> cartItems = _cartItemsRepository.GetCartItemsByUser(userId).ToList();
                foreach (CartItemModel item in cartItems)
                {
                    _cartItemsRepository.Delete(item.CartItemId);
                    _cartItemsRepository.Save();
                }
            }
            catch
            {
                return BadRequest();
            }
            return Ok();

        }
    }
}
