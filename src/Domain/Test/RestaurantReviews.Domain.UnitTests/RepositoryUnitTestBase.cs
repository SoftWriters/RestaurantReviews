using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity;

namespace RestaurantReviews.Domain.UnitTests
{
    [TestClass]
    public class RepositoryUnitTestBase : IDisposable
    {
        IUnityContainer Container;

        public void Dispose()
        {
            if(Container!= null)
            {
                Container.Dispose();
            }
        }

        [TestInitialize]
        public virtual void TestInitialize()
        {
            Container = new UnityContainer();
        }
    }
}
