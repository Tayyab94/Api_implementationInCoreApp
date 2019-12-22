using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BookApiProject.Dtos;

namespace Book_GUI.Services
{
    public class AuthorRepositoryGUI : IAuthorRepositoryGUI
    {
        public AuthorDto GetAuthorByID(int authorid)
        {
            AuthorDto author = new AuthorDto();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:60039/api/");

                var response = client.GetAsync($"authors/{authorid}");
                response.Wait();
                var result = response.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<AuthorDto>();
                    readTask.Wait();
                    author = readTask.Result;
                }
            }
            return author;
        }

        public IEnumerable<AuthorDto> GetAuthors()
        {
            IEnumerable<AuthorDto> reviewerDtos = new List<AuthorDto>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:60039/api/");

                var response = client.GetAsync("authors");
                response.Wait();


                var result = response.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<AuthorDto>>();

                    readTask.Wait();

                    reviewerDtos = readTask.Result;
                }

                return reviewerDtos;
            }
        }

        public IEnumerable<AuthorDto> GetAuthorsOfABook(int bookid)
        {
            IEnumerable<AuthorDto> authors = new List<AuthorDto>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:60039/api/");

                var response = client.GetAsync($"authors/books/{bookid}");
                response.Wait();


                var result = response.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<AuthorDto>>();

                    readTask.Wait();

                    authors = readTask.Result;
                }

                return authors;
            }
        }

        public IEnumerable<BookDto> GetBooksByAuthor(int authorid)
        {
            IEnumerable<BookDto> books = new List<BookDto>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:60039/api/");

                var response = client.GetAsync($"authors/{authorid}/books");
                response.Wait();

                var result = response.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<BookDto>>();

                    readTask.Wait();

                    books = readTask.Result;
                }

                return books;
            }
        }
    }
}
