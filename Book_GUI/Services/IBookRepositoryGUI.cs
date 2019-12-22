using BookApiProject.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book_GUI.Services
{
   public interface IBookRepositoryGUI
    {
        IEnumerable<BookDto> GetBooks();
        BookDto GetBookByID(int bookid);
        BookDto GetBookByIsbn(string bookisbn);
        decimal GetBookRating(int bookid);

        BookDto GetBookOfReview(int reviewid);
    }
}
