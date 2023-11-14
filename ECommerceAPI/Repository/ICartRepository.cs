using ECommerceAPI.Models;

namespace ECommerceAPI.Repository
{
    public interface ICartRepository : IGenericRepository<CartItem>
    {
        List<CartItemModel> GetCartItemsByUser(string id);
        
    }
}
