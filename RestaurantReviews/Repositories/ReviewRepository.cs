using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;

namespace Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        IEnumerable<IReviewModel> _reviews = new List<IReviewModel>();

        public bool HasData()
        {
            return _reviews.Count() > 0;
        }
    }
}