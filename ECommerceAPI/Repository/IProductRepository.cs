using ECommerceAPI.Models;

namespace ECommerceAPI.Repository
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        public Tuple<List<Product>, int> GetProductsByFilteration( string searchString, int pageNo);
    }
   
}
