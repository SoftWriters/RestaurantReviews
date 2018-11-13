using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;

namespace Repositories
{
    public class UserRepository : IUserRepository
    {
        IEnumerable<IUserModel> _users = new List<IUserModel>();

        public bool HasData()
        {
            return _users.Count() > 0;
        }

        public IEnumerable<IUserModel> AddUser(IUserModel user)
        {
            List<IUserModel> users = _users.ToList();
            users.Add(user);
            _users = users;
            return _users;
        }

        // TODO: Read, Update, Delete
    }
}