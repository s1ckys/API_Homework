using System;
using Class03_Homework.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Class03_Homework.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<Book>> GetAll()
        {
            try
            {
                return Ok(StaticDb.Books);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            
        }
        [HttpGet("queryIndex")]
        public ActionResult<Book> GetBook(int index)
        {
            try
            {
                if (index == null)
                {
                    return BadRequest("User index cannot be null");
                }
                if (index < 0)
                {
                    return BadRequest("User index cannot be negative number");
                }
                if (index >= StaticDb.Books.Count)
                {
                    return NotFound($"There is no user with index {index}");
                }
                return Ok(StaticDb.Books[index]);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            
        }
        [HttpGet("queryFilter")]
        public ActionResult<Book> FilterBooks(string? author, string? title)
        {
            try
            {
                if(string.IsNullOrEmpty(author) && string.IsNullOrEmpty(title))
                {
                    return BadRequest("You must fill at least one option to the filter");
                }
                if (string.IsNullOrEmpty(title))
                {
                    var book = StaticDb.Books.FirstOrDefault(x=> x.Author.ToLower().Contains(author.ToLower()));
                    if(book == null)
                    {
                        return NotFound($"There is not book with author {author}");
                    }
                    return Ok(book);
                }
                if (string.IsNullOrEmpty(author)){
                    var book = StaticDb.Books.FirstOrDefault(x => x.Title.ToLower().Contains(title.ToLower()));
                    if (book == null)
                    {
                        return NotFound($"There is no book with title {title}");
                    }
                    return Ok(book);
                }
                var bookWithAllParams = StaticDb.Books.FirstOrDefault( x => x.Author.ToLower().Contains(author.ToLower()) && x.Title.ToLower().Contains(title.ToLower()));
                if( bookWithAllParams == null)
                {
                    return NotFound($"There is no book with title {title} and author {author}");
                }
                return Ok(bookWithAllParams);
                
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPost]
        public IActionResult AddBook([FromBody] Book book)
        {
            try
            {
                if (book == null)
                {
                    return BadRequest("The book you want to add cannot be null");
                }
                if (string.IsNullOrEmpty(book.Title))
                {
                    return BadRequest("The title cannot be null");
                }
                if (string.IsNullOrEmpty(book.Author))
                {
                    return BadRequest("The author cannot be null");
                }
                StaticDb.Books.Add(book);
                return StatusCode(StatusCodes.Status201Created, "The book was successfuly created");
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPost("listOfBooks")]
        public ActionResult ListOfBooks([FromBody] List<Book> books)
        {
            try
            {
                if(books == null)
                {
                    return BadRequest("The list of books cannot be null");
                }
                for(int i = 0; i < books.Count; i++)
                {
                    var book = books[i];
                    if (book == null)
                    {
                        return BadRequest($"The book number{i + 1 } you want to add cannot be null");
                    }
                    if (string.IsNullOrEmpty(book.Title))
                    {
                        return BadRequest($"The title on the book number {i + 1} cannot be null");
                    }
                    if (string.IsNullOrEmpty(book.Author))
                    {
                        return BadRequest($"The author on the book number {i + 1} cannot be null");
                    }
                }
                var titles = books.Select(x=> x.Title).ToList();
                return Ok(titles);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
