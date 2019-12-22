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
    public class ReviewController : Controller
    {
        private readonly IReviewRepositoryGUI _reviewRepositoryGUI;
        private readonly IReviewerRepositoryGUI _reviewerRepositoryGUI;
        private readonly IBookRepositoryGUI _bookRepositoryGUI;

        public ReviewController(IReviewRepositoryGUI reviewRepositoryGUI, IReviewerRepositoryGUI reviewerRepositoryGUI,IBookRepositoryGUI bookRepositoryGUI)
        {
            this._reviewRepositoryGUI = reviewRepositoryGUI;
            this._reviewerRepositoryGUI = reviewerRepositoryGUI;
            this._bookRepositoryGUI = bookRepositoryGUI;
        }


        public IActionResult Index()
        {
            var reviewList = _reviewRepositoryGUI.GetREviews();

            if(reviewList.Count()<=0)
            {
                ViewBag.msg = "There is a problem while getting the Reivews from the database or no review exist yet";
            }

            return View(reviewList);
        }


        public IActionResult GetReviewById(int reviewid)
        {
            var review = _reviewRepositoryGUI.GetReviewByID(reviewid);

            if(review==null)
            {
                ModelState.AddModelError(string.Empty, "Some kind of error while Getting the review");

                ViewBag.reviewMsg = $"There was an error while getting the review from the database or Review of {reviewid} does not exist";

                review = new ReviewDto();
            }

            var reviewer = _reviewerRepositoryGUI.GetReviewerOfAReview(reviewid);

            if (reviewer == null)
            {
                ModelState.AddModelError(string.Empty, "Some kind of error while Getting the reviewer");

                ViewBag.reviewMsg +=$"There was an error while getting the reviewer from the database or Review of {reviewid} does not exist";

                reviewer = new ReviewerDto();
            }

            //            var book= _bookRepositoryGUI.get

            var book = _reviewRepositoryGUI.GetBookOfAReview(reviewid);

            if (book == null)
            {
                ModelState.AddModelError(string.Empty, "Some kind of error while Getting the reviewer");

                ViewBag.reviewMsg += $"There was an error while getting the reviewer from the database or Review of {reviewid} does not exist";

                book = new BookDto();
            }

            var reviewReviewerBook = new ReviewReviewerBookViewModel()
            {
                Book = book,
                Review = review,
                Reviewer = reviewer
            };

            return
                View(reviewReviewerBook);

        }

    }
}
