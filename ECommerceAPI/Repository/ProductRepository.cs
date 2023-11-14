using ECommerceAPI.DbContext;
using ECommerceAPI.Models;

namespace ECommerceAPI.Repository
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private ShoppingDbContext _context = null;
        public ProductRepository(ShoppingDbContext context) : base(context)
        {
            _context = context;
        }

        Tuple<List<Product>, int> IProductRepository.GetProductsByFilteration( string searchString, int pageNo)
        {
            int pageSize = 4;

            IQueryable<Product> products = null;

            int totalProducts; _context.Products.Where(s => s.ProductName.Contains(searchString)
                                      || s.Description.Contains(searchString)).Count();

            if (!String.IsNullOrEmpty(searchString))
            {
                totalProducts = _context.Products.Where(s => s.ProductName.Contains(searchString)
                                      || s.Description.Contains(searchString)).Count();
                products = _context.Products.Where(s => s.ProductName.Contains(searchString)
                                      || s.Description.Contains(searchString)).Skip((pageNo - 1) * pageSize).Take(pageSize);
            }
            else
            {
                totalProducts = _context.Products.Count();
                products = _context.Products.Skip((pageNo - 1) * pageSize).Take(pageSize);
            }
            Tuple<List<Product>, int> tuple = new Tuple<List<Product>, int>(products.ToList(), totalProducts);

            return tuple;
        }
    }
}
