using RestaurantReviewService.Entities;
using System.Collections.Generic;
using System.Data.SQLite;

namespace RestaurantReviewService.EntityModelBuilders
{
    public sealed class RestaurantsDataEntitiesBuilder : IEntityModelBuilder<IList<Restaurant>, SQLiteDataReader>
    {
        IList<Restaurant> IEntityModelBuilder<IList<Restaurant>, SQLiteDataReader>.Build(SQLiteDataReader p)
        {
            IList<Restaurant> restaurants = new List<Restaurant>();
            const int ID = 0;
            const int NAME = 1;
            const int STREET_ADDR = 2;
            const int OVERVIEW = 3;
            const int CITY = 4;
            const int STATE_ID = 5;
            const int STATE = 6;

            if (p != null && p.HasRows)
            {
                while (p.Read())
                {
                    restaurants.Add(
                            new Restaurant
                            {
                                Id = p.GetInt32(ID),
                                Name = p.GetString(NAME),
                                StreetAddress = p.GetString(STREET_ADDR),
                                City = p.GetString(CITY),
                                Overview = p.GetString(OVERVIEW),
                                StateIdRef = p.GetInt32(STATE_ID),
                                State = p.GetString(STATE)
                            }
                        );
                   
                }
            }

            return restaurants;
        }
    }
}
