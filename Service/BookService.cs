using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BooksApi.Models;

namespace BooksAPI.Service
{
    public class BookService
    {
        HttpClientHandler _clientHandler = new HttpClientHandler();
        List<Book> _oBooks = new List<Book>();
        string _apiResponse = string.Empty;

        public async Task<string> ReadJson()
        {
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.GetAsync("https://raw.githubusercontent.com/timeiagro/BackendTest/master/books.json"))
                {

                    _apiResponse = await response.Content.ReadAsStringAsync();
                }
            }
            return _apiResponse;
        }

        public async Task<List<Book>> Deserialize()
        {
            _apiResponse = await ReadJson();

            _oBooks = JsonConvert.DeserializeObject<List<Book>>(_apiResponse);

            return _oBooks;
        }

        public async Task<List<Book>>GetAllBooks()
        {
            return await Deserialize();
        }

        public async Task<Book> GetById(int id)
        {
            List<Book> listBook = await GetAllBooks();

            return listBook.FirstOrDefault(book => book.id == id);
        }

        public async Task<Dictionary<string, decimal>> GetFrete(int id)
        {
            List<Book> listBook = await GetAllBooks();

            Book foundedBook = listBook.FirstOrDefault(book => book.id == id);

            var Book = new Dictionary<string, decimal> 
            {
                { "id", foundedBook.id },
                { "frete", foundedBook.GetFrete(foundedBook.price)}
            };

            return Book;
        }

        public async Task<List<Book>> OrderBy(string order="all")
        {
            List<Book> listBook = await GetAllBooks();

            switch (order.ToLower())
            {
                case "all":
                    return listBook;
                case "desc":
                    return listBook.OrderByDescending(book => book.price).ToList();
                case "asc":
                    return listBook.OrderBy(book => book.price).ToList();
                default:
                    return new List<Book>();
            }  
        }

        public async Task<List<Book>> Search(string name, string author, string order)
        {
            List<Book> listBook;
            
            if (order == null)
            {
                listBook = await GetAllBooks();
            }
            else
            {
                listBook = await OrderBy(order);
            }

            if (name != null)
            {
                return listBook.Where(book => book.name.ToLower().Contains(name.ToLower())).ToList();
            }
            else if (author != null)
            {
                return listBook.Where(book => book.Specifications.author.ToLower().Contains(author.ToLower())).ToList();
            }
            else
            {
                return new List<Book>();
            }


        }

    }
}