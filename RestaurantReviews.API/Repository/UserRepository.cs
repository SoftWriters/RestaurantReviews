using RestaurantReviews.API.Models;
using System.Linq;

namespace RestaurantReviews.API.Repository
{
    public class UserRepository : RepositoryBase<User>
    {
        public UserRepository(IContext context) : base(context) { }

        public override IDataSet<User> GetDataSet()
        {
            return context.UserDataSet;
        }

        public override void Update(long id, User q)
        {
            var contents = GetAll();
            var found = contents.FirstOrDefault(x => x.Id == id);
            if (found == null)
                return;
            // no properties to update
            context.UserDataSet.Save(contents);
        }
    }
}