using BookApiProject.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book_GUI.ViewModels
{
    public class CountryAuthorViewModel
    {
        public CountryDto Country { get; set; }
        public IEnumerable<AuthorDto> Authors { get; set; }
    }
}
