namespace Library.src
{
    public class CLI : IUserInterface
    {

        private readonly AccountController _accController;
        private readonly BookController _bookController;

        public CLI(Account account, string path) 
        {
            _accController = new(account, path);
            _bookController = new(path);
        } 

        public void LogIn()
        {
            
        }

        public void LogOut()
        {
            
        }

        public void DisplayUI()
        {
            Console.WriteLine("Library System");
            Console.WriteLine();
            Console.WriteLine("Please select one of the following options: ");
            Console.WriteLine("1. View all books");
            Console.WriteLine("2. View borrowed books");
            Console.WriteLine("3. Withdraw book");
            Console.WriteLine("4. Return books");
            Console.WriteLine("5. Logout");
            Console.WriteLine("6. Exit program");
        }

        public int UpdateUI()
        {
            var choice = Console.ReadLine();
            switch(choice)
            {
                case "1":
                    GetAllBooks();
                    break;
                case "2":
                    GetRentedBooks();
                    break;
                case "3":
                    WithdrawBook();
                    break;
                case "4":
                    Console.WriteLine("temp");
                    break;
                case "5":
                    Console.WriteLine("temp");
                    break;
                case "6":
                    Console.WriteLine("Exiting...");
                    return -1;
                default:
                    Console.WriteLine("Please enter a correct value");
                    break;
            }

            return 0;
        }

        private void GetAllBooks()
        {
            var books = _bookController.GetBooks();

            foreach (var book in books)
            {
                Console.WriteLine($"ID: {book.BookId}");
                Console.WriteLine($"Title: {book.Title}");
                Console.WriteLine($"Author: {book.Author}");
                Console.WriteLine();
            }
        }

        private void GetRentedBooks()
        {
            var ownedBooks = _accController.ViewBooks();

            if (ownedBooks.Count == 0)
            {
                Console.WriteLine("No books currently rented.");
            }
            else
            {
                foreach (var book in ownedBooks)
                {
                    Console.WriteLine(book.Title);
                    Console.WriteLine(book.Author);
                }
            }
        }

        private void WithdrawBook()
        {
            Console.WriteLine("Please enter the book ID: ");
            var isValid = false;
            int bookID = default;
            do
            {
                var value = Console.ReadLine();

                try
                {
                    bookID = Int32.Parse(value!);
                    isValid = true;
                }
                catch (ArgumentNullException)
                {
                    Console.WriteLine("Please enter a value");
                }
                catch (FormatException)
                {
                    Console.WriteLine("Please enter a valid whole number");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            } while (!isValid);

            var book = _bookController.GetBook(bookID);

            if (book == null)
            {
                Console.WriteLine($"Book {bookID} does not exist");
            }
            else
            {
                try
                {
                    _accController.WithdrawBook(book);
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("You already own this book");
                }
            }
        }
    }
}
