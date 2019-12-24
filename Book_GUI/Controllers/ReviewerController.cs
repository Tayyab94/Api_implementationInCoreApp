using Book_GUI.Services;
using Book_GUI.ViewModels;
using BookApiProject.Dtos;
using BookApiProject.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Book_GUI.Controllers
{
    // Not An Reivewer APi Controller.
    public class ReviewerController :Controller
    {
        private readonly IReviewerRepositoryGUI _reviewerRepositoryGUI;
        private readonly IReviewRepositoryGUI reviewRepositoryGUI;

        public ReviewerController(IReviewerRepositoryGUI reviewerRepositoryGUI, 
            IReviewRepositoryGUI reviewRepositoryGUI)
        {
            this._reviewerRepositoryGUI = reviewerRepositoryGUI;
            this.reviewRepositoryGUI = reviewRepositoryGUI;
        }

        public IActionResult Index()
        {
            var reviewers = _reviewerRepositoryGUI.GetReviewers();

            if(reviewers.Count()<=0)
            {
                ViewBag.msg = "There was a problem retrieving reviewers from the Dtabase or the record does not exist";
            }

            return View(reviewers);
        }

        public IActionResult GetReviewerById(int reviewerid)
        {
            var reviewer = _reviewerRepositoryGUI.GetReviewerByID(reviewerid);

            if(reviewer==null)
            {
                ModelState.AddModelError(string.Empty, "Some Error Occur here in reviewer Controoler");
                ViewBag.reviewerMessage = $"There was pro. in database or reivewer with {reviewerid} does not exist";

                reviewer = new ReviewerDto();
            }

            var reviews = _reviewerRepositoryGUI.GetReviewsByReviewers(reviewerid);

            if(reviews.Count()<=0)
            {
                ViewBag.reviewMessage = $"REviewer {reviewer.FirstName} {reviewer.LastName} does not exist";
            }

            IDictionary<ReviewDto, BookDto> reviewBook = new Dictionary<ReviewDto, BookDto>();

            foreach (var review in reviews)
            {
                var book = reviewRepositoryGUI.GetBookOfAReview(review.id);

                reviewBook.Add(review, book);
            }


            var reviewerReviewsBook = new ReviewerReviewsBooksViewModel
            {
                ReviewBook = reviewBook,
                Review = reviewer
            };
            ViewBag.SuccessMessage=TempData["SuccessMessage"];
            return View(reviewerReviewsBook);
        }

        [HttpGet]
        public IActionResult CreateReviewer()
        {
            return View();
        }


        [HttpPost]
        public IActionResult CreateReviewer(Reviewer reviewer)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:60039/api/");
                var responseTask = client.PostAsJsonAsync("reviewers", reviewer);

                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var newReviewerTask = result.Content.ReadAsAsync<Reviewer>();
                    newReviewerTask.Wait();

                    var newReviewer = newReviewerTask.Result;

                    TempData["SuccessMessage"] = $"Reviewer => {newReviewer.FirstName} was Saved Successfuly!";

                    return RedirectToAction(nameof(GetReviewerById), new { reviewerid = newReviewer.Id });
                }

                if (Convert.ToInt32(result.StatusCode) == 422)
                {
                    ModelState.AddModelError("", "Reviewer already Exist");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Something went Wrong please try again");
                }
            }
            return View();
        }


        [HttpGet]
        public IActionResult UpdateReviewer(int reviewerid)
        {
            var reviewer = _reviewerRepositoryGUI.GetReviewerByID(reviewerid);

            if (reviewer == null)
            {
                ModelState.AddModelError(string.Empty, "Error For Getting Country");

                reviewer = new ReviewerDto();
            }

            return View(reviewer);
        }

        [HttpPost]
        public IActionResult UpdateReviewer(Reviewer model)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:60039/api/");
                var responseTask = client.PutAsJsonAsync($"Reviewers/{model.Id}", model);

                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = $"REviewer => {model.FirstName} was Update Successfuly!";

                    return RedirectToAction(nameof(GetReviewerById), new { reviewerid = model.Id });
                }

                if (Convert.ToInt32(result.StatusCode) == 422)
                {
                    ModelState.AddModelError("", "REviewer already Exist");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Something went Wrong please try again");
                }
            }
            var reviewerDto = _reviewerRepositoryGUI.GetReviewerByID(model.Id);
            return View(reviewerDto);
        }


        [HttpGet]
        public IActionResult DeleteReviewer(int reviewerid)
        {
            var reviewer = _reviewerRepositoryGUI.GetReviewerByID(reviewerid);

            if (reviewer == null)
            {
                ModelState.AddModelError(string.Empty, "Error for Getting Country");

                reviewer = new ReviewerDto();
            }

            return View(reviewer);
        }

        [HttpPost]
        public IActionResult DeleteReviewer(int reviewerid, string reviewerName)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:60039/api/");
                var responseTask = client.DeleteAsync($"reviewers/{reviewerid}");

                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = $"Reviewer => {reviewerName} was Deleted Successfuly!";

                    return RedirectToAction(nameof(Index));
                }

                //if (Convert.ToInt32(result.StatusCode) == 409)
                //{
                //    ModelState.AddModelError("", $"Reviewer {reviewerName} can not be deleted successfuly" +
                //        $" because it is used by at least one author");
                //}
                //else
                //{
                //    ModelState.AddModelError(string.Empty, "Something went Wrong please try again");
                //}
            }

            var reviewerDto = _reviewerRepositoryGUI.GetReviewerByID(reviewerid);
            return View(reviewerDto);
        }
    }
}
