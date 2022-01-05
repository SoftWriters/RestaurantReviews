using RestaurantReviews.Interfaces.Models;
using RestaurantReviews.Interfaces.Repositories;
using System.Linq;

namespace RestaurantReviews.JsonData.Repositories
{
    public class RestaurantRepository : RepositoryBase<IRestaurant>, IRestaurantRepository
    {
        public RestaurantRepository(IContext context) : base(context) { }

        public override IDataSet<IRestaurant> GetDataSet()
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