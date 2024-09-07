using Microsoft.EntityFrameworkCore;

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
                .WithMany(a => a.Accounts)
                .UsingEntity("BorrowedBooks");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite($"Data Source={DbPath}");
       
    }

    public class Account
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Book> Books { get; } = [];
    }

    public class Book
    {
        public int BookId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Author { get; set; }

        public bool Available { get; set; }

        public int Stock { get; set; }

        public ICollection<Account> Accounts { get; } = [];
    }
}
