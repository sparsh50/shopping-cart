using System.ComponentModel.DataAnnotations;

namespace ECommerceAPI.Models
{
    public class Product
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

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        [Required]
        public string CreatedOn { get; set; }
        [Required]
        public string CreatedBy { get; set; }
        [Required]
        public string UpdatedOn { get; set; }
        [Required]
        public string UpdatedBy { get; set; }
    }

}
