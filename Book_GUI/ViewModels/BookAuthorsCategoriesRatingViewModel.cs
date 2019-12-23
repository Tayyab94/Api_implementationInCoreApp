using BookApiProject.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book_GUI.ViewModels
{
    public class BookAuthorsCategoriesRatingViewModel
    {
        public BookDto Book { get; set; }

        public IEnumerable<CategoryDto> Categories { get; set; }

        public IEnumerable<AuthorDto> Authors { get; set; }

        public decimal Rating { get; set; }
    }
}
