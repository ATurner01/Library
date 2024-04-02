namespace Library.src
{
    public class BookController(string path)
    {
        private readonly ReadDatabase _database = new(path);

        public Book? GetBook(int id)
        {
            return _database.GetBook(id);
        }

        public List<Book> GetBooks()
        {
            return _database.GetBooks();
        }
    }
}
