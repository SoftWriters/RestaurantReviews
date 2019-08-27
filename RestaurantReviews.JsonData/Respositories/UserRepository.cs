using RestaurantReviews.Interfaces.Models;
using RestaurantReviews.Interfaces.Repository;
using System.Linq;

namespace RestaurantReviews.JsonData.Repositories
{
    public class UserRepository : RepositoryBase<IUser>, IUserRepository
    {
        internal UserRepository(Context context) : base(context) { }

        internal override DataSet<IUser> GetDataSet()
        {
            return context.UserDataSet;
        }

        public override void Update(long id, IUser q)
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