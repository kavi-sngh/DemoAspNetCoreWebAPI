using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoAspNetCoreWebAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoAspNetCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookOldController : ControllerBase
    {
        static List<Book> books = new List<Book>
        {
            new Book {Id = 1, Author = "Paulo Coelho" , Title = "The Alchemist", Description = "Self-help"},
            new Book {Id = 2, Author = "Dan Brown" , Title = "The Da Vinci Code", Description = "Thriller"}
        };

        [HttpGet]
        public IEnumerable<Book> GetBooks()
        {
            return books;
        }

        [HttpPost]
        public void CreateBook([FromBody] Book book)
        {
            books.Add(book);
        }

        [HttpPut("{id}")]
        public void UpdateBook(int id, [FromBody]Book book)
        {
            books[id] = book;
        }

        [HttpDelete("{id}")]
        public void DeleteBook(int id)
        {
            books.RemoveAt(id);
        }
    }
}
