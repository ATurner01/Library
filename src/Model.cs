using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Library.src
{
    public class LibraryContext : DbContext
    {
        public DbSet<Account> Accounts => Set<Account>();
        public DbSet<Book> Books => Set<Book>();

        public string DbPath { get; }

        public LibraryContext()
        {
            DbPath = Path.Join(Environment.CurrentDirectory, "library.db");
        }

        public LibraryContext(string dbPath)
        {
            DbPath = dbPath;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .HasMany(a => a.Books)
                .WithOne(a => a.Account)
                .HasForeignKey(a => a.AccountId)
                .IsRequired(false);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite($"Data Source={DbPath}");
       
    }

    public class Account
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Book> Books { get; } = new List<Book>();
    }

    public class Book
    {
        public int BookId { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        public string Author { get; set; }

        public int? AccountId { get; set; }
        public Account? Account { get; set; }
    }
}
