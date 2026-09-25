using Microsoft.EntityFrameworkCore;
using P06Shop.Shared;
using P07Shop.DataSeeder;

namespace P05Shop.API.Models
{
    public class DataContext : DbContext
    {

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }
       

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    // Configure the database connection string here
        //    optionsBuilder.UseSqlServer("Data Source=(localdb)\\mssqllocaldb;Initial Catalog=MySuperShop;Integrated Security=True");

        //}

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .Property(p=>p.Title)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Product>()
                .Property(p => p.Barcode)
                .IsRequired()
                .HasMaxLength(12);

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(8,2)");

            modelBuilder.Entity<Product>()
                .HasData(ProductDataSeeder.GenerateProductData());

            modelBuilder.Entity<Category>()
                .Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Category>()
                .HasIndex(c => c.Name)
                .IsUnique();

            modelBuilder.Entity<Product>()
                .HasOne<Category>()
                .WithMany()
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Category>()
                .HasData(
                    new Category { Id = 1, Name = "Elektronika" },
                    new Category { Id = 2, Name = "Dom i ogród" },
                    new Category { Id = 3, Name = "Sport" },
                    new Category { Id = 4, Name = "Odzież" },
                    new Category { Id = 5, Name = "Zabawki" });

        }
    }
}