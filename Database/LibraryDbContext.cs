using Microsoft.EntityFrameworkCore;
using LibraryManagementAPI.Database.Entities;

namespace LibraryManagementAPI.Database
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .HasMany(b => b.Transactions)
                .WithOne(t => t.Book)
                .HasForeignKey(t => t.BookId);

            // Sample data seeding
            modelBuilder.Entity<Book>().HasData(
                new Book { Id = 1, Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", ISBN = "978-0743273565", Publisher = "Scribner", PublicationYear = 1925, CopiesAvailable = 5, Price = 9.99m, Category = "Fiction" },
                new Book { Id = 2, Title = "To Kill a Mockingbird", Author = "Harper Lee", ISBN = "978-0446310789", Publisher = "Grand Central", PublicationYear = 1960, CopiesAvailable = 3, Price = 12.99m, Category = "Fiction" }
            );
        }
    }
}