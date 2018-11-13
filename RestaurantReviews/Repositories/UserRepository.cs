using Models;
using System.Collections.Generic;
using System.Linq;

namespace Repositories
{
    public class UserRepository : IUserRepository
    {
        IEnumerable<IUserModel> _users = new List<IUserModel>();

        int _maxId = 0;

        public IEnumerable<IUserModel> AddUser(IUserModel user)
        {
            List<IUserModel> users = _users.ToList<IUserModel>();
            user.Id = ++_maxId;
            users.Add(user);
            _users = users;
            return _users;
        }

        public IUserModel GetUserById(int id)
        {
            return _users.FirstOrDefault(u => u.Id == id);
        }

        public bool HasData()
        {
            return _users.Any();
        }
        
        // TODO: Read, Update, Delete
    }
}