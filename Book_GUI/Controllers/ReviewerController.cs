using Book_GUI.Services;
using Book_GUI.ViewModels;
using BookApiProject.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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

            return View(reviewerReviewsBook);
        }
    }
}
