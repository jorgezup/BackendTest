using System.Collections.Generic;
using System.Threading.Tasks;
using BooksApi.Models;
using BooksAPI.Service;
using Microsoft.AspNetCore.Mvc;

namespace BooksApi.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BookController : Controller
    {
        BookService _service = new BookService();

        [HttpGet]
        public async Task<List<Book>> GetAllBooks()
        {
            return await _service.GetAllBooks();
        }

        [HttpGet("{id}")]
        public async Task<Book> GetById(int id)
        {
            return await _service.GetById(id);
        }

        [HttpGet("{id}/frete")]
        public async Task<Dictionary<string, decimal>> GetFrete(int id)
        {
            return await _service.GetFrete(id);
        }

        [HttpGet("price")]
        public async Task<List<Book>> GetOrder([FromQuery] string order)
        {
            return await _service.OrderBy(order);
        }

        [HttpGet("search")]
        public async Task<List<Book>> GetParameterSearch([FromQuery] string name, [FromQuery] string author, [FromQuery] string order)
        {
            return await _service.Search(name, author, order);
        }
    }

}
