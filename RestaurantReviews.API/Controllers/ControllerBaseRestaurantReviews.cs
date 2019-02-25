using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestaurantReviews.Data.Contracts.Logging;
using RestaurantReviews.Data.Contracts.Repositories;

namespace RestaurantReviews.API.Controllers
{
    public class ControllerBaseRestaurantReviews : ControllerBase
    {
        #region Private Variables

        protected ILoggerManager _loggerManager;
        protected IMapper _mapper;
        protected IRepositoryWrapper _repositoryWrapper;

        #endregion Private Variables

        #region Constructors

        public ControllerBaseRestaurantReviews(ILoggerManager loggerManager, IMapper mapper, IRepositoryWrapper repositoryWrapper)
        {
            _loggerManager = loggerManager;
            _mapper = mapper;
            _repositoryWrapper = repositoryWrapper;
        }

        #endregion Constructors
    }
}
