using Infosys.CodeFirstCore.DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infosys.CodeFirstCore.DataAccessLayer
{
    public class QuickKartDbCoreContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<PurchaseDetail> PurchaseDetails { get; set; }
        public DbSet<CardDetail> CardDetails { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source = (localdb)\\MSSQLLocalDB;Initial Catalog=QuickKartDBCore;Integrated Security=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure entity and its properties using Fluent API
            modelBuilder.Entity<Category>()
                .HasAlternateKey("CategoryName")
                .HasName("uq_CategoryName");

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasAlternateKey(e => e.ProductName).HasName("uq_ProductName");

                //entity.Property(e => e.ProductId)
                //.HasColumnType("Char(4)");

                entity.HasOne(c => c.Category)
                .WithMany(p => p.Products)
                .HasForeignKey(c => c.CategoryId)
                .HasConstraintName("fk_CategoryId");
            });

            modelBuilder.Entity<PurchaseDetail>(entity =>
            {
                entity.Property(e => e.DateOfPurchase)
                .HasDefaultValueSql("GETDATE()");

                entity.HasOne(e => e.Email)
                .WithMany(u => u.PurchaseDetail)
                .HasForeignKey(e => e.EmailId);

                entity.HasOne(e => e.Product)
                .WithMany(p => p.PurchaseDetails)
                .HasForeignKey(e => e.ProductId);
            });
        }
    }
}
