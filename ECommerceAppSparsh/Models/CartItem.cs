using System.ComponentModel.DataAnnotations;

namespace ECommerceAppSparsh.Models
{
    public class CartItem
    {
        public int CartItemId { get; set; }

        [Required(ErrorMessage = "Quatity can not be empty")]
        public int Quantity { get; set; }
        public string ProductName { get; set; }
        public int ProductId { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
    }
}
