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
                    ReturnBook();
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
                Console.WriteLine($"Stock: {book.Stock}");
                Console.WriteLine($"Available: {book.Available}");
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
            int bookID = ValidateIntInput();

            if (bookID == -1)
            {
                Console.WriteLine("Returning to menu...");
                return;
            }

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
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        private static int ValidateIntInput()
        {
            var isValid = false;
            int input = default;
            do
            {
                var value = Console.ReadLine();

                try
                {
                    input = Int32.Parse(value!);
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
            return input;
        }

        public void ReturnBook()
        {
            Console.WriteLine("Please enter the ID of the book you wish to return: ");

            int bookID = ValidateIntInput();

            if (bookID == -1)
            {
                Console.WriteLine("Returning to menu...");
                return;
            }

            var book = _bookController.GetBook(bookID);

            if (book == null)
            {
                Console.WriteLine($"Book {bookID} does not exist");
            }
            else
            {
                try
                {
                    _accController.ReturnBook(book);
                } catch (ArgumentException)
                {
                    Console.WriteLine("You do not own that book");
                }
            }


        }
    }
}
