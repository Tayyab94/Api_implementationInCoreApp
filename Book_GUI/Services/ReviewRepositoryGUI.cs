using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BookApiProject.Dtos;

namespace Book_GUI.Services
{
    public class ReviewRepositoryGUI : IReviewRepositoryGUI
    {
        public BookDto GetBookOfAReview(int reviewid)
        {
            BookDto book = new BookDto();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:60039/api/");

                var response = client.GetAsync($"reviews/{reviewid}/book");
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

        public ReviewDto GetReviewByID(int reviewid)
        {
            ReviewDto review = new ReviewDto();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:60039/api/");

                var response = client.GetAsync($"reviews/{reviewid}");
                response.Wait();
                var result = response.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ReviewDto>();
                    readTask.Wait();
                    review = readTask.Result;
                }
            }
            return review;

        }

        public IEnumerable<ReviewDto> GetREviews()
        {
            IEnumerable<ReviewDto> reviewDtos = new List<ReviewDto>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:60039/api/");

                var response = client.GetAsync("reviews");
                response.Wait();


                var result = response.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<ReviewDto>>();

                    readTask.Wait();

                    reviewDtos = readTask.Result;
                }

                return reviewDtos;
            }
        }

        public IEnumerable<ReviewDto> GetReviewsOfABook(int bookid)
        {
            IEnumerable<ReviewDto> reviewDtos = new List<ReviewDto>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:60039/api/");

                var response = client.GetAsync($"reviews/books/{bookid}");
                response.Wait();


                var result = response.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<ReviewDto>>();

                    readTask.Wait();

                    reviewDtos = readTask.Result;
                }

                return reviewDtos;
            }
        }

    }
}
