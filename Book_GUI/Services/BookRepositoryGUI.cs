using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BookApiProject.Dtos;

namespace Book_GUI.Services
{
    public class BookRepositoryGUI : IBookRepositoryGUI
    {
        public BookDto GetBookByID(int bookid)
        {
            BookDto book = new BookDto();

            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:60039/api/");
                var response = client.GetAsync($"books/{bookid}");
                response.Wait();

                var result = response.Result;
                if(result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<BookDto>();
                    readTask.Wait();
                    book = readTask.Result;
                }

                return book;

            }
        }

        public BookDto GetBookByIsbn(string bookisbn)
        {
            BookDto book = new BookDto();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:60039/api/");
                var response = client.GetAsync($"books/ISBN/{bookisbn}");
                response.Wait();

                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<BookDto>();
                    readTask.Wait();
                    book = readTask.Result;
                }

                return book;

            }
        }

        public BookDto GetBookOfReview(int reviewid)
        {
            throw new NotImplementedException();
        }

        public decimal GetBookRating(int bookid)
        {
            decimal rating = 0.0m;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:60039/api/");
                var response = client.GetAsync($"books/{bookid}/rating");
                response.Wait();

                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<decimal>();
                    readTask.Wait();
                    rating = readTask.Result;
                }

                return rating;

            }
        }

        public IEnumerable<BookDto> GetBooks()
        {
           IEnumerable<BookDto> book = new List<BookDto>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:60039/api/");
                var response = client.GetAsync($"books");
                response.Wait();

                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<BookDto>>();
                    readTask.Wait();
                    book = readTask.Result;
                }

                return book;

            }
        }
    }
}
