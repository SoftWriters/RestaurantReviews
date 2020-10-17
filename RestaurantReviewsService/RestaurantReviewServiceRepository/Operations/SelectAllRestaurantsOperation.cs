using RestaurantReviewService.Abstract;
using RestaurantReviewService.Concrete;
using RestaurantReviewService.Entities;
using System.Collections.Generic;

namespace RestaurantReviewService.Operations
{
    public interface ISelectAllRestaurantsOperation : IRepositoryOperation<IList<Restaurant>, RestaurantsRepository> { }    


    public sealed class SelectAllRestaurantsOperation : ISelectAllRestaurantsOperation
    {      

        public IList<Restaurant> SelectAll()
        {
            return ((IRepositoryOperation<IList<Restaurant>, RestaurantsRepository>)this).Execute(new RestaurantsRepository());
        }

        IList<Restaurant> IRepositoryOperation<IList<Restaurant>, RestaurantsRepository>.Execute(RestaurantsRepository p)
        {
            return (IList<Restaurant>)((ISqlLiteDbRepository)p).SelectAll();
        }
    }
}
