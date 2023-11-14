﻿using System.ComponentModel.DataAnnotations;

namespace ECommerceAppSparsh.Models
{
    public class UserLoginModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
