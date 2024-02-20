using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Models;
using EcommerceApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApi.Data
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
      public DbSet<Wishlist> Wishlist { get; set; }
      public DbSet<OrderItem> OrderItems { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {   
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AppUser>().ToTable("AppUser");
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Admin>().ToTable("Admin");
          

            modelBuilder.Entity<OrderItem>(oi => oi.HasKey(oi => new{oi.OrderId, oi.ProductId}));  

            modelBuilder.Entity<OrderItem>()
                        .HasOne(oi => oi.Order)
                        .WithMany(o => o.orderItems)
                        .HasForeignKey(oi => oi.OrderId);

            modelBuilder.Entity<OrderItem>()
                        .HasOne(oi => oi.Product)
                        .WithMany(p => p.OrderItems)
                        .HasForeignKey(oi => oi.ProductId);


            modelBuilder.Entity<Wishlist>(wl => wl.HasKey(wl => new{wl.UserId, wl.ProductId}));  

            modelBuilder.Entity<Wishlist>()
                        .HasOne(wl => wl.User)
                        .WithMany(us => us.Wishlist)
                        .HasForeignKey(wl => wl.UserId);

            modelBuilder.Entity<Wishlist>()
                        .HasOne(wl => wl.Product)
                        .WithMany(p => p.Wishlists)
                        .HasForeignKey(wl => wl.ProductId);

        }
    }
}