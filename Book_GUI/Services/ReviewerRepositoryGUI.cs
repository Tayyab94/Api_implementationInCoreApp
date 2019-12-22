using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BookApiProject.Dtos;

namespace Book_GUI.Services
{
    public class ReviewerRepositoryGUI : IReviewerRepositoryGUI
    {
        public ReviewerDto GetReviewerByID(int reviewerid)
        {
            ReviewerDto reviewer = new ReviewerDto();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:60039/api/");

                var response = client.GetAsync($"Reviewers/{reviewerid}");
                response.Wait();
                var result = response.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ReviewerDto>();
                    readTask.Wait();
                    reviewer = readTask.Result;
                }
            }
            return reviewer;
        }

        public ReviewerDto GetReviewerOfAReview(int reviewid)
        {
            ReviewerDto reviewer = new ReviewerDto();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:60039/api/");

                var response = client.GetAsync($"reviewers/{reviewid}/reviewer");
                response.Wait();
                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ReviewerDto>();
                    readTask.Wait();
                    reviewer = readTask.Result;
                }
            }
            return reviewer;
        }

        public IEnumerable<ReviewerDto> GetReviewers()
        {
            IEnumerable<ReviewerDto> reviewerDtos = new List<ReviewerDto>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:60039/api/");

                var response = client.GetAsync("Reviewers");
                response.Wait();


                var result = response.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<ReviewerDto>>();

                    readTask.Wait();

                    reviewerDtos = readTask.Result;
                }

                return reviewerDtos;
            }
        }

        public IEnumerable<ReviewDto> GetReviewsByReviewers(int reviewerid)
        {
            IEnumerable<ReviewDto> reviewerDtos = new List<ReviewDto>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:60039/api/");

                var response = client.GetAsync($"Reviewers/{reviewerid}/Reviews");
                response.Wait();


                var result = response.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<ReviewDto>>();

                    readTask.Wait();

                    reviewerDtos = readTask.Result;
                }

                return reviewerDtos;
            }
        }


        //public IEnumerable<ReviewDto> GetReviewsByReviewers(int reviewerid)
        //{
        //    IEnumerable<ReviewDto> reviewerDtos = new List<ReviewDto>();

        //    using (var client = new HttpClient())
        //    {
        //        client.BaseAddress = new Uri("http://localhost:60039/api/");

        //        var response = client.GetAsync($"Reviewers/{reviewerid}/Reviewers");
        //        response.Wait();


        //        var result = response.Result;

        //        if (result.IsSuccessStatusCode)
        //        {
        //            var readTask = result.Content.ReadAsAsync<IList<ReviewDto>>();

        //            readTask.Wait();

        //            reviewerDtos = readTask.Result;
        //        }

        //        return reviewerDtos;
        //    }
        //}

    }
}
