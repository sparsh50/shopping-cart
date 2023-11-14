using System.ComponentModel.DataAnnotations;

namespace ECommerceAPI.Models
{
    public class CartItem
    {
        public int CartItemId { get; set; }

        [Required(ErrorMessage = "Quatity can not be empty")]
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }
    }
}
