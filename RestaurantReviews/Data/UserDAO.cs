using RestaurantReviews.Api.Model;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantReviews.Data
{
    public class UserDAO
    {
        //
        // In place of a real DB, I setup this in-memory data structure to contain User data
        //
        public static ConcurrentDictionary<string, User> Users = new ConcurrentDictionary<string, User>();
        public static ConcurrentDictionary<Guid, User> UsersById = new ConcurrentDictionary<Guid, User>();

        public static User Add(User user)
        {
            if (!Users.TryAdd(user.GetKey(), user))
                return null;

            user.UserId = Guid.NewGuid();
            UsersById.TryAdd(user.UserId, user);

            return user;
        }

        public static User Add(string name, string phoneNumber)
        {
            User user = new User(name, phoneNumber);

            if (!Users.TryAdd(user.GetKey(), user))
                return null;

            user.UserId = Guid.NewGuid();
            UsersById.TryAdd(user.UserId, user);

            return user;
        }

        public static bool Delete(User user)
        {
            User deleted;
            Users.TryRemove(user.GetKey(), out deleted);

            if (deleted != null)
                UsersById.TryRemove(deleted.UserId, out deleted);

            return (deleted != null);
        }

        public static bool Delete(Guid userId)
        {
            User deleted;
            UsersById.TryRemove(userId, out deleted);

            if (deleted != null)
                Users.TryRemove(deleted.GetKey(), out deleted);

            return (deleted != null);
        }

        public static User GetUserById(Guid userId)
        {
            if (UsersById.Keys.Contains(userId))
                return UsersById[userId];
            else
                return null;
        }

        public static User GetUserByKey(string key)
        {
            if (Users.Keys.Contains(key))
                return Users[key];
            else
                return null;
        }

        static UserDAO()
        {
            Add("Glenn", "412-555-1212");
            Add("Bob", "722-555-1212");
            Add("Bill", "412-123-4567");
        }
    }
}
