using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoAspNetCoreWebAPI.Data;
using Microsoft.AspNetCore.Mvc;
using DemoAspNetCoreWebAPI.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

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
        public IActionResult Get()
        {
            return Ok(bookDbContext.Books);
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
