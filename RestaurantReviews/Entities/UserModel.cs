using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class UserModel : IUserModel
    {
        public UserModel(int id, string name, bool isAdmin)
        {
            Id = id;
            Name = name;
            IsAdmin = isAdmin;
        }

        public int Id
        { get; set; }

        public string Name
        { get; set; }

        public bool IsAdmin
        { get; private set; }
    }
}