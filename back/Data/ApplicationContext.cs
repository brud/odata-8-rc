using System;
using back.Entities;
using Microsoft.EntityFrameworkCore;
 
namespace back.Data
{
    public sealed class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
 
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(
                "server=localhost;user=root;password=root;database=odata;", 
                new MySqlServerVersion(new Version(8, 0, 11))
            );
        }
    }
}