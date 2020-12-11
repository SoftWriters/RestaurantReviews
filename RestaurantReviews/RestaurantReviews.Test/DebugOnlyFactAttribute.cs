using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xunit;

namespace RestaurantReviews.Test
{
    public class DebugOnlyFactAttribute : FactAttribute
    {
        public DebugOnlyFactAttribute()
        {
            if (!Debugger.IsAttached)
            {
                Skip = "Test Skipped.  You must have a debugger attached to run this test";
            }
        }
    }
}
