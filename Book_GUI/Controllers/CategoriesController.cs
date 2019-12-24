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
         ViewBag.SuccessMessage =TempData["SuccessMessage"];
            CategoryBookViewModel categoryBookViewModel = new CategoryBookViewModel
            {
                bookDtosList = booksOfCategory,
                category = category
            };
            return View(categoryBookViewModel);
        }

        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }


        [HttpPost]
        public IActionResult CreateCategory(Category category)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:60039/api/");
                var responseTask = client.PostAsJsonAsync("categories", category);

                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var newCategoryTask = result.Content.ReadAsAsync<Category>();
                    newCategoryTask.Wait();

                    var newCategory = newCategoryTask.Result;

                    TempData["SuccessMessage"] = $"CAtegory => {newCategory.Name} was Saved Successfuly!";

                    return RedirectToAction(nameof(GetCategoryById), new { countryId = newCategory.Id });
                }

                if (Convert.ToInt32(result.StatusCode) == 422)
                {
                    ModelState.AddModelError("", "CAtegory already Exist");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Something went Wrong please try again");
                }
            }
            return View();
        }


        [HttpGet]
        public IActionResult UpdateCategory(int categoryid)
        {
            var Category = _categoryRepositoryGUI.GetCategoryByID(categoryid);

            if (Category == null)
            {
                ModelState.AddModelError(string.Empty, "Error For Getting Country");

                Category = new CategoryDto();
            }

            return View(Category);
        }

        [HttpPost]
        public IActionResult UpdateCategory(Category model)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:60039/api/");
                var responseTask = client.PutAsJsonAsync($"categories/{model.Id}", model);

                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = $"Country => {model.Name} was Update Successfuly!";

                    return RedirectToAction(nameof(GetCategoryById), new { categoryid = model.Id });
                }

                if (Convert.ToInt32(result.StatusCode) == 422)
                {
                    ModelState.AddModelError("", "Country already Exist");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Something went Wrong please try again");
                }
            }
            var CategoryDto = _categoryRepositoryGUI.GetCategoryByID(model.Id);
            return View(CategoryDto);
        }

        [HttpGet]
        public IActionResult DeleteCategory(int categoryid)
        {
            var category = _categoryRepositoryGUI.GetCategoryByID(categoryid);

            if (category == null)
            {
                ModelState.AddModelError(string.Empty, "Error for Getting Country");

                category = new CategoryDto();
            }

            return View(category);
        }

        [HttpPost]
        public IActionResult DeleteCategory(int categoryid, string categoryName)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:60039/api/");
                var responseTask = client.DeleteAsync($"categories/{categoryid}");

                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = $"Category => {categoryName} was Deleted Successfuly!";

                    return RedirectToAction(nameof(Index));
                }

                if (Convert.ToInt32(result.StatusCode) == 409)
                {
                    ModelState.AddModelError("", $"Category {categoryName} can not be deleted successfuly" +
                        $" because it is used by at least one author");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Something went Wrong please try again");
                }
            }

            var categoryDto = _categoryRepositoryGUI.GetCategoryByID(categoryid);
            return View(categoryDto);
        }
    }
}
