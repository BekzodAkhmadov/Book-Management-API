using Book_Management_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Book_Management_API.Data
{
    public class BookRepository : IBookRepository
    {
        private readonly BookDbContext _context;

        public BookRepository(BookDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<string>> GetBooksAsync(int pageNumber, int pageSize)
        {
            return await _context.Books
                .Where(b => !b.IsDeleted)
                .OrderByDescending(b => b.ViewsCount)
                .Select(b => b.Title)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Book> GetBookAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null && !book.IsDeleted)
            {
                book.ViewsCount++;
                await _context.SaveChangesAsync();
            }
            return book;
        }

        public async Task AddBookAsync(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
        }

        public async Task AddBooksBulkAsync(IEnumerable<Book> books)
        {
            _context.Books.AddRange(books);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBookAsync(Book book)
        {
            _context.Entry(book).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task SoftDeleteBookAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                book.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
        }
        public async Task SoftDeleteBooksBulkAsync(IEnumerable<int> ids)
        {
            var books = await _context.Books.Where(b => ids.Contains(b.Id)).ToListAsync();
            foreach (var book in books)
            {
                book.IsDeleted = true;
            }
            await _context.SaveChangesAsync();
        }

        public bool BookExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }
        public bool BookTitleExists(string title)
        {
            return _context.Books.Any(b => b.Title == title && !b.IsDeleted);
        }


    }
}
