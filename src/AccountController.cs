﻿namespace Library.src
{
    public class AccountController
    {

        private readonly ReadDatabase _database;
        private readonly Account _account;

        public AccountController(Account account, string path) 
        {
            _account = account;
            _database = new ReadDatabase(path);
        }
        public ICollection<Book> ViewBooks()
        {
            var books = _database.GetOwnedBooks(_account);
            return books;
        }

        public void WithdrawBook(Book book)
        {
            try
            {
                _database.AddBook(_account, book);
            } catch (Exception)
            {
                throw;
            }
        }

        public void ReturnBook()
        {

        }
    }
}
