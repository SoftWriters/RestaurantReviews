using RestaurantReviews.Data.Contracts.Repositories;
using RestaurantReviews.Data.Contracts.Repositories.Entities;
using RestaurantReviews.Data.Repositories.Entities;

namespace RestaurantReviews.Data.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        #region Private Variables

        private RestaurantReviewsContext _repoContext;

        private IRestaurantRepository _restaurant;
        private IReviewRepository _review;
        private IUserRepository _user;

        #endregion Private Variables

        #region Constructors

        public RepositoryWrapper()
        {
        }
    
        #endregion Constructors

        #region Repository Properties

        public IRestaurantRepository Restaurant
        {
            get
            {
                if (_restaurant == null)
                {
                    _restaurant = new RestaurantRepository(_repoContext);
                }
                return _restaurant;
            }
            set { _restaurant = value; }
        }

        public IReviewRepository Review
        {
            get
            {
                if (_review == null)
                {
                    _review = new ReviewRepository(_repoContext);
                }
                return _review;
            }
            set { _review = value; }
        }

        public IUserRepository User
        {
            get
            {
                if (_user == null)
                {
                    _user = new UserRepository(_repoContext);
                }
                return _user;
            }
            set { _user = value; }
        }

        #endregion Repository Properties

        #region Constructors

        public RepositoryWrapper(RestaurantReviewsContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }

        #endregion Constructors
    }
}
