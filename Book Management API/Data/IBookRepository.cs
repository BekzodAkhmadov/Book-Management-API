using Book_Management_API.Models;

namespace Book_Management_API.Data
{
    public interface IBookRepository
    {
        Task<IEnumerable<string>> GetBooksAsync(int pageNumber, int pageSize);
        Task<Book> GetBookAsync(int id);
        Task AddBookAsync(Book book);
        Task AddBooksBulkAsync(IEnumerable<Book> books);
        Task UpdateBookAsync(Book book);
        Task SoftDeleteBookAsync(int id);
        Task SoftDeleteBooksBulkAsync(IEnumerable<int> ids);
        bool BookExists(int id);
        bool BookTitleExists(string title);
    }
}