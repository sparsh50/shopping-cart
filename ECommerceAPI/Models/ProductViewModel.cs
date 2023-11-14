using System.ComponentModel.DataAnnotations;

namespace ECommerceAPI.Models
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Name is Required")]
        public string ProductName { get; set; }
        [Required(ErrorMessage = "Description is Required")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Image Url is Required")]
        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        public int Quantity { get; set; }
        [Required(ErrorMessage = "Price is Required")]
        public int Price { get; set; }

        [Required(ErrorMessage = "Rating is Required")]
        public int Rating { get; set; }

        [Required(ErrorMessage = "Category is Required")]
        public int CategoryId { get; set; }
    }
}
