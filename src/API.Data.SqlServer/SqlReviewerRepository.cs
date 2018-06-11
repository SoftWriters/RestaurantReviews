/******************************************************************************
 * Name: SqlReviewerRepository.cs
 * Purpose: Reviewer Repository that uses EF to perform Repo operations
 * History:
 * ----------------------------------------------------------------------------
 * 06/10/2018   Vidya       Initial Version
 *****************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using RestaurantReviews.API.Interface;
using RestaurantReviews.API.Model.DTO;
using RestaurantReviews.API.Data.SqlServer.DataModel;

namespace RestaurantReviews.API.Data.SqlServer
{
    public class SqlReviewerRepository : IReviewerRepository
    {
        public ReviewerModelDTO GetReviewerById(int id)
        {
            ReviewerModelDTO ret = null;

            using (var db = new RestaurantReviewsContext())
            {
                var reviewer = db.TblReviewer.Find(id);

                if (reviewer != null) ret = ModelFactory.Create(reviewer);
            }
            return ret;
        }

        public bool CheckReviewerExists(ReviewerModelDTO reviewer)
        {
            using (var db = new RestaurantReviewsContext())
            {
                var reviewerRet = db.TblReviewer.Where(b => b.Name.CompareTo(reviewer.Name) == 0).FirstOrDefault();

                if (reviewerRet != null) return true;
            }

            return false;
        }

        public ReviewerModelDTO AddReviewer(ReviewerModelDTO newReviewer)
        {
            ReviewerModelDTO ret = null;

            using (var db = new RestaurantReviewsContext())
            {
                TblReviewer reviewer = ModelFactory.Create(newReviewer);
                db.TblReviewer.Add(reviewer);
                db.SaveChanges();
                ret = ModelFactory.Create(reviewer);
            }

            return ret;
        }
    }
}
