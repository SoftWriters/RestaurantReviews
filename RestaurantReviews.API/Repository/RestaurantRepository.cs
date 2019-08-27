using System.Linq;
using RestaurantReviews.API.Models;

namespace RestaurantReviews.API.Repository
{
    public class RestaurantRepository : RepositoryBase<Restaurant>, IRestaurantRepository
    {
        public RestaurantRepository(IContext context) : base(context) { }

        public override IDataSet<Restaurant> GetDataSet()
        {
            return context.RestaurantDataSet;
        }

        public override void Update(long id, Restaurant q)
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