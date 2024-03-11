using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
