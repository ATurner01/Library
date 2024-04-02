using Library.src;

namespace Library {

    internal class DbSetup
    {

        private readonly string _path;
        public DbSetup(string path)
        {
            _path = path;
            //InitialSetup();
            //AddBooks(5);
        }

        private void InitialSetup()
        {
            using var db = new LibraryContext(_path);

            Console.WriteLine($"Database Path = {db.DbPath}");
            Console.WriteLine("Inserting new account");
            db.Add(new Account { Name = "Account1" });
            db.SaveChanges();

            Console.WriteLine("Adding a book");
            db.Add(new Book { Title = "Book 1", Author = "Author 1", Description = "This is book 1" });
            db.SaveChanges();

            Console.WriteLine("Withdrawing a book");
            var account = db.Accounts
                .OrderBy(a => a.Id)
                .First();

            var book = db.Books
                .OrderBy(b => b.BookId)
                .First();
            Console.WriteLine(book.Title);

            account.Books.Add(book);
            db.SaveChanges();
        }

        private void AddBooks(int numOfBooks)
        {
            using var db = new LibraryContext(_path);
            var random = new Random();

            for (int i = 0; i < numOfBooks; i++)
            {
                var bookId = random.Next(1000);
                db.Add(new Book
                {
                    Title = $"Book {bookId}",
                    Author = $"Author {bookId}",
                    Description = $"This is book {bookId}"
                });
            }

            try
            {
                db.SaveChanges();
            } catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            
        }
    }
}