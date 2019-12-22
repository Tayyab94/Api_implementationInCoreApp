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
    public class AuthorsController :Controller
    {
        private readonly IAuthorRepositoryGUI _authorRepositoryGUI;
        private readonly ICountryRepositoryGUI _countryRepositoryGUI;
        private readonly ICategoryRepositoryGUI _categoryRepositoryGUI;

        public AuthorsController(IAuthorRepositoryGUI authorRepositoryGUI,
            ICountryRepositoryGUI countryRepositoryGUI,
            ICategoryRepositoryGUI categoryRepositoryGUI)
        {
            this._authorRepositoryGUI = authorRepositoryGUI;
            this._countryRepositoryGUI = countryRepositoryGUI;
            this._categoryRepositoryGUI = categoryRepositoryGUI;
        }


        public IActionResult Index ()
        {
            var authoers = _authorRepositoryGUI.GetAuthors();

            if(authoers.Count()<=0)
            {
                ViewBag.msg = "There is some Problem while getting the authors from the database or Authors  does not exist yet";
            }

            return View(authoers);
        }


        public IActionResult GetAuthorById(int authorid)
        {
            var author = _authorRepositoryGUI.GetAuthorByID(authorid);

            if (author == null)
            {
                ModelState.AddModelError(string.Empty, "Some kind of error while Getting the Author");

                ViewBag.reviewMsg += $"There was an error while getting the Author from the database or Review of {authorid} does not exist";

                author = new AuthorDto();
            }

            var country = _countryRepositoryGUI.GetCountryOfAuthor(authorid);

            if (country == null)
            {
                ModelState.AddModelError(string.Empty, "Some kind of error while Getting the country");

                ViewBag.reviewMsg += $"There was an error while getting the country from the database or Review of {authorid} does not exist";

                country = new CountryDto();
            }
            var bookCategories = new Dictionary<BookDto, IEnumerable<CategoryDto>>();

            var books = _authorRepositoryGUI.GetBooksByAuthor(authorid);

            foreach (var item in books)
            {
                var categories = _categoryRepositoryGUI.GetAllCategoriesOfABook(item.Id);
                bookCategories.Add(item, categories);
            }

            var mainModel = new AuthoCountryBooksCategoriesViewModel
            {
                author = author,
                BookCategories = bookCategories,
                country = country
            };

            return
                View(mainModel);

        }
    }
}
