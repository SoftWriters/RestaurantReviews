using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestaurantReviews.API.Helpers;
using RestaurantReviews.API.Tests.Mocks;
using RestaurantReviews.Data.Contracts.Logging;
using RestaurantReviews.Data.Contracts.Repositories;
using RestaurantReviews.Data.Contracts.Repositories.Entities;
using RestaurantReviews.Data.Repositories;

namespace RestaurantReviews.API.Tests.Base
{
    public class BaseControllerUnitTests
    {
        #region Protected Variables

        protected ILoggerManager _loggerManager = new MockLoggerManager();
        protected IMapper _mapper;
        protected MapperConfiguration _mapperConfiguration;
        protected IRepositoryWrapper _repositoryWrapper;

        #endregion Protected Variables

        #region Initialization

        [TestInitialize]
        public void TestInitialization()
        {
            _mapperConfiguration = new MapperConfiguration(cfg => { cfg.AddProfile<AutoMapperProfile>(); });
            _mapper = _mapperConfiguration.CreateMapper();
            _mapper = new Mapper(_mapperConfiguration);
            _repositoryWrapper = Mock.Of<RepositoryWrapper>();
            _repositoryWrapper.Restaurant = Mock.Of<IRestaurantRepository>();
            _repositoryWrapper.Review = Mock.Of<IReviewRepository>();
            _repositoryWrapper.User = Mock.Of<IUserRepository>();
        }

        #endregion Initialization
    }
}
