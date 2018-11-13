using Models;
using System.Collections.Generic;

namespace Repositories
{
    public interface IUserRepository
    {
        IEnumerable<IUserModel> AddUser(IUserModel user);

        IUserModel GetUserById(int id);

        bool HasData();
    }
}
