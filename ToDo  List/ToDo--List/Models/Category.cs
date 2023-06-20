using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDo__List.Models
{
	public class Category
	{
		public int ID { get; set; }

		public string Categories { get; set; }

        public string Tasks { get; set; }

        public string Status { get; set; }

        [DisplayName("Due Date")]
        public string EndDate { get; set; }

        public string Priority { get; set; }

        // Foreign key for the user who owns the task
        public string UserId { get; set; }
        //[NotMapped]
        //public string EncryptedId { get; set; }

        public IdentityUser? User { get; set; }
    }
}

