using System.ComponentModel.DataAnnotations;

namespace ECommerceAppSparsh.Models
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Name is Required")]
        [MinLength(3, ErrorMessage = "Minimum 3 characters are required")]

        [Display(Name = "Product Name")]
        public string ProductName { get; set; }
        [Required(ErrorMessage = "Description is Required")]
        [MinLength(6,ErrorMessage ="Minimum 6 characters are required")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Image Url is Required")]
        [Display(Name = "Image Url")]
        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        public int Quantity { get; set; }
        [Required(ErrorMessage = "Price is Required")]
        public int Price { get; set; }

        [Required(ErrorMessage = "Rating is Required")]
        public int Rating { get; set; }

        [Display(Name = "Category")]
        [Required(ErrorMessage = "Category is Required")]
        public int CategoryId { get; set; }
    }
}
