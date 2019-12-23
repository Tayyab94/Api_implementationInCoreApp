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


    public class HomeController :Controller
    {
        private readonly IBookRepositoryGUI bookRepositoryGUI;
        private readonly IAuthorRepositoryGUI authorRepositoryGUI;
        private readonly ICountryRepositoryGUI countryRepositoryGUI;
        private readonly IReviewerRepositoryGUI reviewerRepositoryGUI;
        private readonly IReviewRepositoryGUI reviewRepositoryGUI;
        private readonly ICategoryRepositoryGUI categoryRepositoryGUI;

        public HomeController(IBookRepositoryGUI bookRepositoryGUI, IAuthorRepositoryGUI authorRepositoryGUI, 
            ICountryRepositoryGUI countryRepositoryGUI,IReviewerRepositoryGUI reviewerRepositoryGUI,
            IReviewRepositoryGUI reviewRepositoryGUI,ICategoryRepositoryGUI categoryRepositoryGUI)
        {
            this.bookRepositoryGUI = bookRepositoryGUI;
            this.authorRepositoryGUI = authorRepositoryGUI;
            this.countryRepositoryGUI = countryRepositoryGUI;
            this.reviewerRepositoryGUI = reviewerRepositoryGUI;
            this.reviewRepositoryGUI = reviewRepositoryGUI;
            this.categoryRepositoryGUI = categoryRepositoryGUI;
        }

        public IActionResult Index()
        {
            var books = bookRepositoryGUI.GetBooks();

            if(books.Count()<=0)
            {
                ViewBag.msg = "there was a problem while Getting the books from teh darabase or books does not exist yet.";

            }

            var bookAuthorsCategoriesRatingViewModel = new List<BookAuthorsCategoriesRatingViewModel>();

            foreach (var item in books)
            {
                var categories = categoryRepositoryGUI.GetAllCategoriesOfABook(item.Id).ToList();

                if (categories.Count() <= 0)
                    ModelState.AddModelError(string.Empty, "Some Kind of error while Getting authors");

                var authors = authorRepositoryGUI.GetAuthorsOfABook(item.Id).ToList();

                if (authors.Count() <= 0)
                    ModelState.AddModelError(string.Empty, "Some kind of error while Getting Authors");

                var rating = bookRepositoryGUI.GetBookRating(item.Id);

                bookAuthorsCategoriesRatingViewModel.Add(new BookAuthorsCategoriesRatingViewModel()
                {
                    Authors = authors,
                    Book = item,
                    Categories = categories,
                    Rating = rating
                });

            }

            return View(bookAuthorsCategoriesRatingViewModel);
        }

        public IActionResult GetBookById(int bookid)
        {
            var CompleteBookViewModel = new CompleteBookViewModel()
            {
                ReviewsReviewers = new Dictionary<ReviewDto, ReviewerDto>(),
                AuthorsCountries = new Dictionary<AuthorDto, CountryDto>()
            };

            var book = bookRepositoryGUI.GetBookByID(bookid);

            if(book==null)
            {
                ModelState.AddModelError(string.Empty, "Some kind of error Getting Books");
                book = new BookDto();
            }

            var categories = categoryRepositoryGUI.GetAllCategoriesOfABook(bookid);
            if(categories.Count()<=0)
            {
                ModelState.AddModelError(string.Empty, "Some kind of Error Getting Categories");
                
            }

            var rating = bookRepositoryGUI.GetBookRating(bookid);

            CompleteBookViewModel.Book = book;
            CompleteBookViewModel.Categories = categories;
            CompleteBookViewModel.Rating = rating;

            var authors = authorRepositoryGUI.GetAuthorsOfABook(bookid);

            if (authors.Count() <= 0)
                ModelState.AddModelError(string.Empty, "Some kind of error Getting Authors");

            foreach (var author in authors)
            {
                var country = countryRepositoryGUI.GetCountryOfAuthor(author.Id);

                CompleteBookViewModel.AuthorsCountries.Add(author, country);
            }

            var reviews = reviewRepositoryGUI.GetReviewsOfABook(bookid);

            if (reviews.Count() <= 0)
                ModelState.AddModelError(string.Empty, "Some kind of error Getting reviews");

            foreach (var review in reviews)
            {
                var reviewer = reviewerRepositoryGUI.GetReviewerOfAReview(review.id);

                CompleteBookViewModel.ReviewsReviewers.Add(review, reviewer);
            }


            return View(CompleteBookViewModel);
        }
    }
}
