using BookApiProject.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book_GUI.ViewModels
{
    public class AuthoCountryBooksCategoriesViewModel
    {

        public AuthorDto author { get; set; }

        public CountryDto country { get; set; }

        public IDictionary<BookDto, IEnumerable<CategoryDto>> BookCategories { get; set; }
    }
}
