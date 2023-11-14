using System.ComponentModel.DataAnnotations;

namespace ECommerceAPI.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        [Required]
        public string CategoryName { get; set; }
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
