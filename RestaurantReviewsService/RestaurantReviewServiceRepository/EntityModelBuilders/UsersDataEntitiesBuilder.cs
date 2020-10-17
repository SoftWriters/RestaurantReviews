using RestaurantReviewService.Entities;
using System.Collections.Generic;
using System.Data.SQLite;

namespace RestaurantReviewService.EntityModelBuilders
{
    public sealed class UsersDataEntitiesBuilder : IEntityModelBuilder<IList<User>, SQLiteDataReader>
    {
        IList<User> IEntityModelBuilder<IList<User>, SQLiteDataReader>.Build(SQLiteDataReader p)
        {
            IList<User> users = new List<User>();
            const int ID = 0;
            const int FIRST_NAME = 1;
            const int LAST_NAME = 2;
            const int ALIAS = 3;
            const int CITY = 4;
            const int STATE_ID = 5;
            const int STATE = 6;

            if (p != null && p.HasRows)
            {
                while (p.Read())
                {
                    users.Add(
                            new User
                            {
                                Id = p.GetInt32(ID),
                                FirstName = p.GetString(FIRST_NAME),
                                LastName = p.GetString(LAST_NAME),
                                Alias = p.GetString(ALIAS),
                                City = p.GetString(CITY),
                                StateIdRef = p.GetInt32(STATE_ID),
                                State = p.GetString(STATE)
                            }
                        );

                }
            }

            return users;
        }
    }
}
