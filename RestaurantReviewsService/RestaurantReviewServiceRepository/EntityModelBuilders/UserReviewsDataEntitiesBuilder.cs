using RestaurantReviewService.Entities;
using System.Collections.Generic;
using System.Data.SQLite;

namespace RestaurantReviewService.EntityModelBuilders
{
    public sealed class UserReviewsDataEntitiesBuilder : IEntityModelBuilder<IList<UserReview>, SQLiteDataReader>
    {
        IList<UserReview> IEntityModelBuilder<IList<UserReview>, SQLiteDataReader>.Build(SQLiteDataReader p)
        {
            IList<UserReview> userReviews = new List<UserReview>();
            const int ID = 0;
            const int USER_ID_REF = 1;
            const int REST_ID_REF = 2;
            const int TITLE = 3;
            const int COMMENTS = 4;
            const int RATINGS_REF = 6;
            const int RATINGS_LVL = 7;
            const int RATINGS_DESC = 8;
            

            if (p != null && p.HasRows)
            {
                while (p.Read())
                {
                    userReviews.Add(
                            new UserReview
                            {
                                Id = p.GetInt32(ID),
                                UserIdRef = p.GetInt32(USER_ID_REF),
                                RestaurantIdRef = p.GetInt32(REST_ID_REF),
                                Title = p.GetString(TITLE),
                                Comments = p.GetString(COMMENTS),
                                RatingsRef = p.GetInt32(RATINGS_REF),
                                RatingsLevel = p.GetInt32(RATINGS_LVL),
                                RatingsDescription = p.GetString(RATINGS_DESC)

                            }
                        ); 

                }
            }

            return userReviews;
        }
    }
}
