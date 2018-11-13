namespace Models
{
    public class UserModel : IUserModel
    {
        public UserModel(string name, bool isAdmin)
        {
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