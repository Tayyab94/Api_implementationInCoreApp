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
    public class CountriesController : Controller
    {
        private readonly ICountryRepositoryGUI _countryRepositoryGUI;

        public CountriesController(ICountryRepositoryGUI countryRepositoryGUI)
        {
            this._countryRepositoryGUI = countryRepositoryGUI;
        }

        public IActionResult Index()
        {
            var countries = _countryRepositoryGUI.GetCountries();

            if (countries.Count() <= 0)
            {
                ViewBag.msg = "There was a problem retrieving the countries from " +
                    "the database or no counry exist";
            }

            return View(countries);
        }

        [HttpGet]
        public IActionResult GetCountryById(int countryId)
        {
            var country = _countryRepositoryGUI.GetCountryByID(countryId);

            if (country == null || country.Id == 0)
            {
                ModelState.AddModelError(string.Empty, "Error Getting a Country");
                ViewBag.msg = "There is a Problem retrieving the Country " +
                    "from the database or no country exist";

                country = new CountryDto();
            }

            var authors = _countryRepositoryGUI.GetAuthorsFromCountry(countryId);

            if (authors.Count() <= 0)
            {
                ViewBag.AuthorMsg = $"There is No Author from the Country with id => {countryId}";
            }

            var countryAuthorModel = new CountryAuthorViewModel
            {
                Authors = authors,
                Country = country
            };
            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            return View(countryAuthorModel);
        }

        [HttpGet]
        public IActionResult CreateCountry()
        {
            return View();
        }


        [HttpPost]
        public IActionResult CreateCountry(Country model)
        {
            using(var client= new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:60039/api/");
                var responseTask = client.PostAsJsonAsync("countries", model);

                responseTask.Wait();

                var result = responseTask.Result;

                if(result.IsSuccessStatusCode)
                {
                    var newCountryTask = result.Content.ReadAsAsync<Country>();
                    newCountryTask.Wait();

                    var newCountry = newCountryTask.Result;

                    TempData["SuccessMessage"] = $"Country => {newCountry.Name} was Saved Successfuly!";

                    return RedirectToAction(nameof(GetCountryById), new { countryId = newCountry.Id });
                }

                if(Convert.ToInt32(result.StatusCode)==422)
                {
                    ModelState.AddModelError("", "Country already Exist");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Something went Wrong please try again");
                }
            }
            return View();
        }


        [HttpGet]
        public IActionResult DeleteCountry(int countryid)
        {
            var country = _countryRepositoryGUI.GetCountryByID(countryid);

            if(country==null)
            {
                ModelState.AddModelError(string.Empty, "Error for Getting Country");

                country = new CountryDto();
            }

            return View(country);
        }

        [HttpPost]
        public IActionResult DeleteCountry(int countryid,string countryName)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:60039/api/");
                var responseTask = client.DeleteAsync($"countries/{countryid}");

                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = $"Country => {countryName} was Deleted Successfuly!";

                    return RedirectToAction(nameof(Index));
                }

                if (Convert.ToInt32(result.StatusCode) == 409)
                {
                    ModelState.AddModelError("", $"Country {countryName} can not be deleted successfuly" +
                        $" because it is used by at least one author");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Something went Wrong please try again");
                }
            }

            var countryDto = _countryRepositoryGUI.GetCountryByID(countryid);
            return View(countryDto);
        }
    }
}
