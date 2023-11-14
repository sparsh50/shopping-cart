using System.ComponentModel.DataAnnotations;

namespace ECommerceAPI.Models
{
    public class UserRegistrationModel
    {
        [Required(ErrorMessage = "Name is requird")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email is requird")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is requird")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password is not matching")]
        public string ConfirmPassword { get; set; }
    }
}
