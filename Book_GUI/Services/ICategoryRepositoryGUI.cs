using BookApiProject.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book_GUI.Services
{
    public interface ICategoryRepositoryGUI
    {
        IEnumerable<CategoryDto> GetCategories();
        CategoryDto GetCategoryByID(int categoryid);
        IEnumerable<BookDto> GetAllBooksForCategory(int categoryid);
        IEnumerable<CategoryDto> GetAllCategoriesOfABook(int bookid);
    }
}
