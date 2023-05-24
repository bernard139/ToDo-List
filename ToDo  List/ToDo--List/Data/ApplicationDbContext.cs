using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ToDo__List.Models;

namespace ToDo__List.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
    }
}

