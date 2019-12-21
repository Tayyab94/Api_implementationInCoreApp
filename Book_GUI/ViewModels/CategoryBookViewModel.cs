using BookApiProject.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book_GUI.ViewModels
{
    public class CategoryBookViewModel
    {
        public CategoryDto  category { get; set; }
        public IEnumerable<BookDto> bookDtosList { get; set; }
    }
}
