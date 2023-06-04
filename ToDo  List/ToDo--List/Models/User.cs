using System;
using System.ComponentModel.DataAnnotations;

namespace ToDo__List.Models
{
	public class User
	{
        [Key] // Add this attribute to specify the primary key
        public int UserId { get; set; } // Example primary key property

        [Required]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password",
            ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
    }
}

