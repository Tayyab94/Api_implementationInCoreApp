using BookApiProject.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book_GUI.Services
{
   public interface IAuthorRepositoryGUI
    {
        IEnumerable<AuthorDto> GetAuthors();
        AuthorDto GetAuthorByID(int authorid);
        IEnumerable<BookDto> GetBooksByAuthor(int authorid);
        IEnumerable<AuthorDto> GetAuthorsOfABook(int bookid);
    }
}
