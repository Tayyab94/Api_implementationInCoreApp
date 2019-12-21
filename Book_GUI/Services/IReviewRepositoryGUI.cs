using BookApiProject.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book_GUI.Services
{
   public interface IReviewRepositoryGUI
    {
        IEnumerable<ReviewDto> GetREviews();
        ReviewDto GetReviewByID(int reviewid);
        IEnumerable<ReviewDto> GetReviewsOfABook(int bookid);


        BookDto GetBookOfAReview(int reviewid);

    }
}
