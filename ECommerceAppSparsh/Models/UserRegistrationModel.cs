using System.ComponentModel.DataAnnotations;

namespace ECommerceAppSparsh.Models
{
    public class UserRegistrationModel
    {
        [Required(ErrorMessage = "Name is requird")]
        [MinLength(3,ErrorMessage ="Minimum three chars required")]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-Za-z]+))$",
            ErrorMessage = "Invalid Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email is requird")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is requird")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{6,}",
        ErrorMessage = "Password must be Minimum 6 characters and contain at least one digit, one uppercase letter, one lower case letter and Special Character")]

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password is not matching")]
        public string ConfirmPassword { get; set; }

    }
}
