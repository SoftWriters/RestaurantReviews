using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReviewsAPI
{
    class User
    {
        public int userID { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public bool disposed { get; set; }
        
        public User(string userName, string password, string email)
        {
            // userID would be incremented in the database
            this.userName = userName;
            this.password = password;
            this.email = email;
            this.disposed = false;
        }

        // This is a placeholder for the unknown data source of the larger system
        List<User> users = new List<User>();

        // Add a new User to the database
        public string AddUser(string userName, string password, string email)
        {
            string response = "";
            if (!userExists(userName))
            {
                User user = new User(userName, password, email);
                users.Add(user);
                response = "Account created successfully.";
            }
            else
            {
                response = "An account with that username already exists in our database.";
            }
            return response;
        }

        // Check if User with the same name and address already exists
        public bool userExists(string userName)
        {
            bool exists = false;
            foreach (User user in users)
            {
                if (user.userName == userName)
                {
                    exists = true;
                }
            }
            return exists;
        }

        // Process a login from the mobile app
        public bool UserLogin(string userName, string password)
        {
            bool valid = false;
            foreach (User user in users)
            {
                if (user.userName == userName && user.password == password)
                {
                    valid = true;
                }
            }
            return valid;
        }
    }
}
