using BookApiProject.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book_GUI.Services
{
    public interface ICountryRepositoryGUI
    {
        IEnumerable<CountryDto> GetCountries();

        CountryDto GetCountryByID(int countryid);
        CountryDto GetCountryOfAuthor(int authorid);
        IEnumerable<AuthorDto> GetAuthorsFromCountry(int countryid);
    }
}
