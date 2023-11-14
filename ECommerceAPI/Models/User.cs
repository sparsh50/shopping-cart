using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ECommerceAPI.Models
{
    public class User : IdentityUser
    {
        [Required(ErrorMessage = "User Name is required")]
        public string Name { get; set; }

    }
}

