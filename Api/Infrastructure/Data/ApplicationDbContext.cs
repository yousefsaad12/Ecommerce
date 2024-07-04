using Api.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Api.Infrastructer.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser, IdentityRole , string>
    {

      public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}
      
      
      public DbSet<AppUser> AppUser { get; set; }
      public DbSet<User> User { get; set; }
      public DbSet<Admin> Admin { get; set; }
      public DbSet<Product> Products { get; set; }
      public DbSet<Category> Categories { get; set; }
      public DbSet<Order> Orders { get; set; }

      public DbSet<OrderItem> OrderItems { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {   
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AppUser>().ToTable("AppUser");
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Admin>().ToTable("Admin");


            modelBuilder.Entity<OrderItem>()
                        .HasKey(oi => new {oi.OrderId, oi.ProductId});
            
            modelBuilder.Entity<OrderItem>()
                        .HasOne(oi => oi.Order)
                        .WithMany(o => o.orderItems)
                        .HasForeignKey(o => o.OrderId);

            modelBuilder.Entity<OrderItem>()
                        .HasOne(oi => oi.Product)
                        .WithMany(p => p.orderItems)
                        .HasForeignKey(o => o.ProductId);
  

        }
    }
}