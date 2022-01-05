using RestaurantReviews.Interfaces.Business;
using RestaurantReviews.Interfaces.Models;
using RestaurantReviews.Interfaces.Repositories;
using System;
using System.Collections.Generic;

namespace RestaurantReviews.Business.Managers
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepository _userRepository;

        public UserManager(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public ICollection<IUser> GetAll()
        {
            return _userRepository.GetAll();
        }

        public IUser GetById(long id)
        {
            return _userRepository.GetById(id);
        }        

        public void Create(IUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            _userRepository.Create(user);
        }
    }
}
