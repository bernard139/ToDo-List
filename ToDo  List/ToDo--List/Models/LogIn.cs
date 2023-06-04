using System;
using System.ComponentModel.DataAnnotations;

namespace ToDo__List.Models
{
	public class LogIn
	{
        [Key] // Add this attribute to specify the primary key
        public int UserId { get; set; } // Example primary key property

        [Required]
        [UsernameOrEmail]
        public string UsernameOrEmail { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}

