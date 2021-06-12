using System;

namespace RestaurantReviews.Core.DataTypes
{
    public class DuplicateEntityException : Exception
    {
        public DuplicateEntityException()
        {
        }

        public DuplicateEntityException(string entityType, Guid id)
            : base($"{entityType} with id {id} already exists.")
        {
        }
    }

    public class InvalidEntryException : Exception
    {
        public InvalidEntryException()
        {
        }

        public InvalidEntryException(string message)
            : base(message)
        {
        }
    }

    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException()
        {
        }

        public EntityNotFoundException(string entityType, Guid id)
            : base($"{entityType} with id {id} does not exist.")
        {
        }
    }
}
