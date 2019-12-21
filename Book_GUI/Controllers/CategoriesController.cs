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
    public class CategoriesController:Controller
    {
        ICategoryRepositoryGUI _categoryRepositoryGUI;

        public CategoriesController(ICategoryRepositoryGUI categoryRepositoryGUI)
        {
            this._categoryRepositoryGUI = categoryRepositoryGUI;
        }

        public IActionResult Index()
        {
            var categorirs = _categoryRepositoryGUI.GetCategories();

            if(categorirs.Count()<=0)
            {
                ViewBag.msg = "There was a problem of receiving categories form Database or Category does not exist ";


            }

            return View(categorirs);
        }  //Show All Categoris

        [HttpGet]   // Get CategorybyY id, categoryId is Paremeter here
        public IActionResult GetCategoryById(int categoryid)
        {
            var category = this._categoryRepositoryGUI.GetCategoryByID(categoryid);

            if(category==null)
            {
                ModelState.AddModelError(string.Empty, "Error Getting Cagtegories");
                ViewBag.msg = "This is a problem While getting Category from the Database or Category does not exit yet.";
                category = new CategoryDto();
            }


            var booksOfCategory = this._categoryRepositoryGUI.GetAllBooksForCategory(categoryid);
            if(booksOfCategory.Count()<=0)
            {
                ViewBag.msgBook = String.Format("{0} Has No Exist", category.Name);
            }

            CategoryBookViewModel categoryBookViewModel = new CategoryBookViewModel
            {
                bookDtosList = booksOfCategory,
                category = category
            };
            return View(categoryBookViewModel);
        }
    }
}
