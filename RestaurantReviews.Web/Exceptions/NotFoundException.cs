using System;

namespace RestaurantReviews.Web
{
    public abstract class NotFoundException<Entity> : Exception
    {
        protected string _message = $"{ typeof(Entity).Name } not found";

        public override string Message => _message;
    }
}
