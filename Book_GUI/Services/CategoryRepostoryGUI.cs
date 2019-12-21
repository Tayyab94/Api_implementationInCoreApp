using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BookApiProject.Dtos;

namespace Book_GUI.Services
{
    public class CategoryRepostoryGUI : ICategoryRepositoryGUI
    {
        public IEnumerable<BookDto> GetAllBooksForCategory(int categoryid)
        {
            IEnumerable<BookDto> books = new List<BookDto>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:60039/api/");

                var response = client.GetAsync($"categories/{categoryid}/books");
                response.Wait();

                var result = response.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<BookDto>>();
                    readTask.Wait();

                    books = readTask.Result;
                }
            }

            return books;
        }

        public IEnumerable<CategoryDto> GetAllCategoriesOfABook(int bookid)
        {
            IEnumerable<CategoryDto> categories = new List<CategoryDto>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:60039/api/");

                var response = client.GetAsync($"categories/books/{bookid}");
                response.Wait();

                var result = response.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<CategoryDto>>();
                    readTask.Wait();

                    categories = readTask.Result;
                }
            }

            return categories;
        }

        public IEnumerable<CategoryDto> GetCategories()
        {
            IEnumerable<CategoryDto> categories = new List<CategoryDto>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:60039/api/");

                var response = client.GetAsync("categories");
                response.Wait();

                var result = response.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<CategoryDto>>();
                    readTask.Wait();

                    categories = readTask.Result;
                }
            }

            return categories;
        }

        public CategoryDto GetCategoryByID(int categoryid)
        {
            CategoryDto categories = new CategoryDto();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:60039/api/");

                var response = client.GetAsync($"categories/{categoryid}");
                response.Wait();

                var result = response.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<CategoryDto>();
                    readTask.Wait();

                    categories = readTask.Result;
                }
            }

            return categories;
        }
    }
}
