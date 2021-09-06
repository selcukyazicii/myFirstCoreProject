using coreProject.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coreProject.Contexts
{
    public class YoutubeContext:IdentityDbContext<AppUser>
    {
        

        //db bağlantısı için optionsBuilder gereklidir.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=(localdb)\\mssqllocaldb; database=youtubeNetCore1; integrated security=true");
            base.OnConfiguring(optionsBuilder);
        }
        

        //çoka çok ilişki belirtilir
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasMany(x => x.ProductCategories).WithOne(x => x.Product).HasForeignKey
                 (x => x.ProductId);

            modelBuilder.Entity<Categories>().HasMany(x => x.ProductCategories).WithOne(x => x.Categories).HasForeignKey
                (x => x.CategoryId);


            //tekrarlı data girilmesinin önüne geçtik
            modelBuilder.Entity<ProductCategories>().HasIndex(x => new
            {
                x.CategoryId,
                x.ProductId
            }).IsUnique();
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<ProductCategories> ProductCategories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Categories> Categories { get; set; }
    }
}
