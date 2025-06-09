using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ef.Models
{
    public class ShopContext: DbContext
    {
        public static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddFilter(DbLoggerCategory.Query.Name, LogLevel.Information);
            builder.AddConsole();
        });
        public DbSet<Product> products { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<CategoryDetail> CategoryDetail { get; set; }
        private const string connectionString = "Server=DESKTOP-6FO9FCK\\SQLEXPRESS;Initial Catalog = shopdata;Trusted_Connection=yes;TrustServerCertificate=True";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLoggerFactory(loggerFactory);
            optionsBuilder.UseSqlServer(connectionString);
            //optionsBuilder.UseLazyLoadingProxies();
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // fluent API
            modelBuilder.Entity<Product>(entity =>
            {
                //Table mapping
                entity.ToTable("Sanpham");
                // Pk
                entity.HasKey(p => p.ProductId);
                // Index
                entity.HasIndex(p => p.Price).HasDatabaseName("index-sanpham-price");
                //Relative
                entity.HasOne(p => p.Category)
                .WithMany() // Category ko co Property ~ Sanpham
                .HasForeignKey("CateId") //Dat ten Fk
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Khoa_ngoai_Sanpham_Category");
                entity.HasOne(p => p.Category2)
                      .WithMany(c => c.Products) //Collect Navigation
                      .HasForeignKey("CateId2")
                      .OnDelete(DeleteBehavior.NoAction);
                entity.Property(p => p.Name)
                      .HasColumnName("Ten san pham")
                      .HasColumnType("nvarchar")
                      .HasMaxLength(60)
                      .IsRequired(false)
                      .HasDefaultValue("Ten san pham mac dinh");
            });
            //1-1
            modelBuilder.Entity<CategoryDetail>(entity =>
            {
                entity.HasOne(d => d.category)
                      .WithOne(c => c.categoryDetail)
                      .HasForeignKey<CategoryDetail>(c => c.CategoryDetailId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
