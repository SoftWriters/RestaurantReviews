using System.Linq;
using RestaurantReviews.Interfaces.Models;
using RestaurantReviews.Interfaces.Repository;

namespace RestaurantReviews.JsonData.Repositories
{
    public class RestaurantRepository : RepositoryBase<IRestaurant>, IRestaurantRepository
    {
        internal RestaurantRepository(Context context) : base(context) { }

        internal override DataSet<IRestaurant> GetDataSet()
        {
            return context.RestaurantDataSet;
        }

        public override void Update(long id, IRestaurant q)
        {
            var contents = GetAll();
            var found = contents.FirstOrDefault(x => x.Id == id);
            if (found == null)
                return;
            found.City = q.City;
            context.RestaurantDataSet.Save(contents);
        }
    }
}