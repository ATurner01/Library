using Microsoft.EntityFrameworkCore;

namespace Library.src
{
    /// <summary>
    /// A class for handling queries to the attatched SQLite database
    /// </summary>
    public class ReadDatabase : IQueryData
    {

        private readonly string _dbPath;

        public ReadDatabase(string dbPath)
        {
            _dbPath = dbPath;
        }

        /// <summary>
        /// This method loads the first Account object that contains an ID that matches the input parameter "id"
        /// </summary>
        /// <param name="id">The Primary Key Identifier for the desired account</param>
        /// <returns>
        /// The matching account for the given id, or the default value if no account exists
        /// </returns>
        public Account? GetAccount(int id)
        {
            using var context = new LibraryContext(_dbPath);

            return context.Accounts.Include(b => b.Books).FirstOrDefault(a => a.Id == id);
        }

        /// <summary>
        /// Queries the Books table to find the book with the same ID as the input parameter
        /// </summary>
        /// <param name="id">The ID value of the desired book</param>
        /// <returns>
        /// The matching book if it exists, otherwise returns the default value
        /// </returns>
        public Book? GetBook(int id)
        {
            using var context = new LibraryContext(_dbPath);

            return context.Books.Find(id);
        }

        /// <summary>
        /// Returns all the entities contained in the Books table
        /// </summary>
        /// <returns>
        /// A List of Book objects
        /// </returns>
        public List<Book> GetBooks() 
        {
            using var context = new LibraryContext(_dbPath);
            var books = context.Books.ToList();

            return books;
        }

        /// <summary>
        /// Retrieves a list of books corresponding to the given ID values. If a ID does not match a book, then no entry is added to the list
        /// </summary>
        /// <param name="ids">A list of book IDs</param>
        /// <returns>
        /// A list of book object, or an empty list object if no books were found
        /// </returns>
        public List<Book> GetBooks(List<int> ids)
        {
            var books = new List<Book>();

            foreach (var id in ids)
            {
                var book = GetBook(id);
                if (book != null)
                { 
                    books.Add(book);
                }
            }

            return books;
        }


        /// <summary>
        /// Adds the provided book to the given account
        /// </summary>
        /// <param name="account">The account that is currently logged in</param>
        /// <param name="book">The book object to be added/param>
        /// <exception cref="ArgumentException">
        /// Thrown when the account already has the given book
        /// </exception>
        public void AddBook(Account account, Book book)
        {
            using var context = new LibraryContext(_dbPath);
            context.Accounts.Attach(account);
            context.Books.Attach(book);

            if (account.Books.FirstOrDefault(b => b.BookId == book.BookId) != null)
            {
                throw new ArgumentException("Account cannot have multiple of the same books");
            }

            account.Books.Add(book);
            context.SaveChanges();
        }

        /// <summary>
        /// Retrieves a collection of books that an account owns
        /// </summary>
        /// <param name="account">The account currently logged in</param>
        /// <returns>
        /// A generic ICollection object containing the retrieve books
        /// </returns>
        public ICollection<Book> GetOwnedBooks(Account account)
        {
            using var context = new LibraryContext(_dbPath);

            return account.Books;
        }

        //TODO: Do this properly. Currently removes relationship between account and book, doesn't update "stock" (need to add that too)
        public void RemoveBook(Account account, Book book)
        {
            using var context = new LibraryContext(_dbPath);
            context.Accounts.Attach(account);

            Book? bookRef = account.Books.FirstOrDefault(b => b.BookId == book.BookId) ?? throw new ArgumentException("Account does not possess this book");

            account.Books.Remove(bookRef);
            context.SaveChanges();
        }

        /// <summary>
        /// Saves all changes made to an account. This should only be called whenever the account is logged out to avoid unnecessary data I/O
        /// </summary>
        /// <param name="account">The account currently logged in</param>
        // Currently not used, but kept in case of compatibility issues
        public void SaveData(Account account)
        {
            using var context = new LibraryContext(_dbPath);
            context.Update(account);
            context.SaveChanges();
        }
    }
}
