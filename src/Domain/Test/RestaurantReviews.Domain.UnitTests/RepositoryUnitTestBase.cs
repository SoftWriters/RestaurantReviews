using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity;

namespace RestaurantReviews.Domain.UnitTests
{
    [TestClass]
    public class RepositoryUnitTestBase
    {
        IUnityContainer Container;
        [TestInitialize]
        public virtual void TestInitialize()
        {
            Container = new UnityContainer();
        }
    }
}
