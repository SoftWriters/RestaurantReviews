using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReviewUnitTests
{
    public abstract class UnitTestBase
    {
        private TestSetup _testSetup;

        public UnitTestBase()
        {
            _testSetup = new TestSetup();
            _testSetup.Initialize();
        }

   
    }
}
