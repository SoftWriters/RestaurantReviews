using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantReviews.Data.EfLibrary.Respositories;
using RestaurantReviews.Data.Framework.RepoContracts;
using RestaurantReviews.Data.Framework.UnitOfWork;

namespace RestaurantReviews.Data.EfLibrary
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RestaurantReviewsContext _context;

        public UnitOfWork(RestaurantReviewsContext context)
        {
            _context = context;
        }

        private IUserRepo _userRepo;
        public IUserRepo UserRepo { get { return _userRepo ?? (_userRepo = new UserRepo(_context)); } }

        private IStateRepo _stateRepo;
        public IStateRepo StateRepo { get { return _stateRepo ?? (_stateRepo = new StateRepo(_context)); } }

        private IRestaurantRepo _restaurantRepo;
        public IRestaurantRepo RestaurantRepo { get{return _restaurantRepo ?? (_restaurantRepo = new RestaurantRepo(_context)); } }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
