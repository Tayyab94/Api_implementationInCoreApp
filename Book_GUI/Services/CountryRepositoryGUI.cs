using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BookApiProject.Dtos;

namespace Book_GUI.Services
{
    public class CountryRepositoryGUI : ICountryRepositoryGUI
    {
        public IEnumerable<AuthorDto> GetAuthorsFromCountry(int countryid)
        {


            IEnumerable<AuthorDto> authors = new List<AuthorDto>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:60039/api/");

                var response = client.GetAsync($"countries/{countryid}/authors");
                response.Wait();

                var result = response.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<AuthorDto>>();
                    readTask.Wait();

                    authors = readTask.Result;
                }
            }

            return authors;
        }
        public IEnumerable<CountryDto> GetCountries()
        {
            IEnumerable<CountryDto> countries = new List<CountryDto>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:60039/api/");

                var response = client.GetAsync("countries");
                response.Wait();

                var result = response.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<CountryDto>>();
                    readTask.Wait();

                    countries = readTask.Result;
                }
            }

            return countries;
        }
        public IEnumerable<CountryDto> GetCountries4()
        {
            IEnumerable<CountryDto> countries = new List<CountryDto>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:60039/api/");

                var response = client.GetAsync("countries");
                response.Wait();

                var result = response.Result;

                if (result.IsSuccessStatusCode)
                {

                    var readTask = result.Content.ReadAsAsync<IList<CountryDto>>();
                    readTask.Wait();

                    //    countries =(IEnumerable<CountryDto>)readTask.Result.ToList();
                    //countries = (IList<CountryDto>)readTask.Result;

                    countries = readTask.Result;
                }
            }


            return countries;
            // throw new NotImplementedException();
        }

        public CountryDto GetCountryByID(int countryid)
        {
            CountryDto country = new CountryDto();

            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("http://localhost:60039/api/");

                var response = client.GetAsync($"countries/{countryid}");

                response.Wait();

                var result = response.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<CountryDto>();
                    readTask.Wait();
                    country = readTask.Result;
                }
            }

            return country;
        }

        public CountryDto GetCountryOfAuthor(int authorid)
        {
            CountryDto countryDto = new CountryDto();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:60039/api/");
                var response = client.GetAsync($"countries/authors/{authorid}");
                response.Wait();

                var result = response.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readtask = result.Content.ReadAsAsync<CountryDto>();
                    readtask.Wait();

                    countryDto = readtask.Result;
                }
            }

            return countryDto;
        }
    }
}
