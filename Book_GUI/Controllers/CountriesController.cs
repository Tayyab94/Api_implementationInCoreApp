using Book_GUI.Services;
using BookApiProject.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book_GUI.Controllers
{
    public class CountriesController :Controller
    {
        private readonly ICountryRepositoryGUI _countryRepositoryGUI;

        public CountriesController(ICountryRepositoryGUI countryRepositoryGUI)
        {
            this._countryRepositoryGUI = countryRepositoryGUI;
        }

        public IActionResult Index()
        {
            var countries = _countryRepositoryGUI.GetCountries();
           
            if(countries.Count()<=0)
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

            if(country==null || country.Id==0)
            {
                ModelState.AddModelError(string.Empty, "Error Getting a Country");
                ViewBag.msg = "There is a Problem retrieving the Country " +
                    "from the database or no country exist";

                country = new CountryDto();
            }
            return View(country);
        }



    }
}
