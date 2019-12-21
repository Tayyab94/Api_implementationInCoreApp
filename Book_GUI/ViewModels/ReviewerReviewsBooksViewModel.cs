using BookApiProject.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book_GUI.ViewModels
{
    public class ReviewerReviewsBooksViewModel
    {
        public ReviewerDto Review { get; set; }
        public IDictionary<ReviewDto,BookDto> ReviewBook { get; set; }
    }
}
