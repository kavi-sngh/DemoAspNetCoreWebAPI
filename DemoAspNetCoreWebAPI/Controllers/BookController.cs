using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoAspNetCoreWebAPI.Data;
using Microsoft.AspNetCore.Mvc;
using DemoAspNetCoreWebAPI.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Internal;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DemoAspNetCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        BookDbContext bookDbContext;

        public BookController(BookDbContext bookDbContext)
        {
            this.bookDbContext = bookDbContext;
        }

        // GET: api/<BookController>
        [HttpGet]
        [ResponseCache(Duration = 20, Location = ResponseCacheLocation.Client)]
        public IActionResult Get(string sort = "asc")
        {
            IQueryable<Book> bookCollection;
            switch (sort)
            {
                case "asc":
                    bookCollection = bookDbContext.Books.OrderBy(o => o.PublishedDate);
                    break;
                case "desc":
                    bookCollection = bookDbContext.Books.OrderByDescending(o => o.PublishedDate);
                    break;
                default:
                    bookCollection = bookDbContext.Books;
                    break;
            }

            return Ok(bookCollection);
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult GetBooksByPage(int? pageNum, int? pageSize)
        {
            var collection = bookDbContext.Books;
            var currentPageNum = pageNum ?? 1;
            var currentPageSize = pageSize ?? 5;
            //Wont work with SQL 2008 as Fetch and Offset not supported
            return Ok(collection.Skip((currentPageNum - 1)*currentPageSize).Take(currentPageSize));
        }

        // GET api/<BookController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var book = bookDbContext.Books.Find(id);
            if(book == null)
            {
                return NotFound("No record found!");
            }
            return Ok(book);
        }

        [HttpGet("[action]")]
        public IActionResult SearchByAuthor(string author)
        {
            var books = bookDbContext.Books.Where(book => book.Author.StartsWith(author));
            return Ok(books);
        }

        // POST api/<BookController>
        [HttpPost]
        public IActionResult Post([FromBody] Book book)
        {
            bookDbContext.Books.Add(book);
            bookDbContext.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT api/<BookController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Book book)
        {
            var entity = bookDbContext.Books.Find(id);
            if(entity == null)
            {
                return NotFound("No record found!");
            }
            entity.Author = book.Author;
            entity.Description = book.Description;
            entity.Title = book.Title;
            entity.PublishedDate = book.PublishedDate;
            bookDbContext.SaveChanges();
            return Ok("Record Updated!");
        }

        // DELETE api/<BookController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var entity = bookDbContext.Books.Find(id);
            if(entity == null)
            {
                return NotFound("No Record found!");
            }
            bookDbContext.Books.Remove(entity);
            bookDbContext.SaveChanges();
            return Ok("Record Deleted!");
        }
    }
}
