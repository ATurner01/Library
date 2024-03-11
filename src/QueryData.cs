namespace Library.src
{
    public interface IQueryData
    {
        public abstract Account? GetAccount(int index);
        public abstract Book? GetBook(int index);
        public abstract List<Book> GetBooks();
        public abstract List<Book> GetBooks(List<int> ids);

        public abstract void AddBook(Account account, Book book);
        public abstract ICollection<Book> GetOwnedBooks(Account account);

    }
}
