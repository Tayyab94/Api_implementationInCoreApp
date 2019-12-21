using BookApiProject.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book_GUI.Services
{
   public interface IReviewerRepositoryGUI
    {
        IEnumerable<ReviewerDto> GetReviewers();
        ReviewerDto GetReviewerByID(int reviewerid);
        IEnumerable<ReviewDto> GetReviewsByReviewers(int reviewerid);

        ReviewerDto GetReviewerOfAReview(int reviewid);


      //  IEnumerable<ReviewerDto> GetReviewsByReviewers(int reviewerid);


    }
}
