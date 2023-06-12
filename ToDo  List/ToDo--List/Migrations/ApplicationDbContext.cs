using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using ToDo__List.Models;

namespace ToDo__List.Migrations
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<User> User { get; set; }

        public DbSet<LogIn> Login { get; set; }

    }
}

