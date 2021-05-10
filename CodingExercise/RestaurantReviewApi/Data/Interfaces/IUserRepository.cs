using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public interface IUserRepository
    {
        User ReadUser(int id);
        IList<User> ReadAllUsers();
    }
}
