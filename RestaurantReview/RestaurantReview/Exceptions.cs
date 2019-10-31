using System;

namespace RestaurantReview
{
    public class IsTooShort : Exception
    {
        public IsTooShort(string message) : base(message)
        {
        }
    }
}