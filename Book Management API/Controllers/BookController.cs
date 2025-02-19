using Book_Management_API.Data;
using Book_Management_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Book_Management_API.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _repository;

        public BookController(IBookRepository bookRepository)
        {
            _repository = bookRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetBooksAsync(int pageNumber, int pageSize)
        {
            var books = await _repository.GetBooksAsync(pageNumber, pageSize);
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetBook(int id)
        {
            var book = await _repository.GetBookAsync(id);
            if (book == null || book.IsDeleted)
            {
                return NotFound();
            }

            int currentYear = DateTime.Now.Year;
            int yearsSincePublished = currentYear - book.PublicationYear;
            double popularityScore = book.ViewsCount * 0.5 + yearsSincePublished * 2;

            var result = new
            {
                book.Id,
                book.Title,
                book.PublicationYear,
                book.AuthorName,
                book.ViewsCount,
                PopularityScore = popularityScore
            };

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(Book book)
        {
            if (_repository.BookTitleExists(book.Title))
            {
                return Conflict("A book with the same title already exists.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _repository.AddBookAsync(book);
            return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
        }

        [HttpPost("bulk")]
        public async Task<ActionResult> PostBooksBulk(List<Book> books)
        {
            var existingTitles = books.Where(book => _repository.BookTitleExists(book.Title)).Select(book => book.Title).ToList();

            if (existingTitles.Any())
            {
                return Conflict($"Books with the following titles already exist: {string.Join(", ", existingTitles)}");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _repository.AddBooksBulkAsync(books);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, Book book)
        {
            if (id != book.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _repository.UpdateBookAsync(book);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_repository.BookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var bookExists = _repository.BookExists(id);
            if (!bookExists)
            {
                return NotFound();
            }
            await _repository.SoftDeleteBookAsync(id);
            return NoContent();
        }

        [HttpDelete("bulk")]
        public async Task<IActionResult> DeleteBooksBulk(List<int> ids)
        {
            await _repository.SoftDeleteBooksBulkAsync(ids);
            return NoContent();
        }
    }
}
