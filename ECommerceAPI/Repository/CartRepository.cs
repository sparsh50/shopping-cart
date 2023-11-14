using ECommerceAPI.DbContext;
using ECommerceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Repository
{
    public class CartRepository : GenericRepository<CartItem>, ICartRepository
    {
        private readonly ShoppingDbContext _context;
        public CartRepository(ShoppingDbContext context) : base(context)
        {
            _context = context;
        }
        List<CartItemModel> ICartRepository.GetCartItemsByUser(string id)
        {
            List<CartItem> cartItems = _context.CartItems
                        .Where(x => x.UserId == id).Include(c => c.Product)
                        .ToList();

            List<CartItemModel> modelCartItems = new List<CartItemModel>();
            for (int i = 0; i < cartItems.Count; i++)
            {
                CartItemModel modelCartItem = new CartItemModel();
                modelCartItem.CartItemId = cartItems[i].CartItemId;
                modelCartItem.ProductName = cartItems[i].Product.ProductName;
                modelCartItem.Price = cartItems[i].Product.Price;
                modelCartItem.Description = cartItems[i].Product.Description;
                modelCartItem.Quantity = cartItems[i].Quantity;
                modelCartItem.ProductId = cartItems[i].ProductId;

                modelCartItems.Add(modelCartItem);
            }
            return modelCartItems;
        }
    }
}
