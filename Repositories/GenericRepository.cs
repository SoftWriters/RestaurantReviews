
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestaurantReviews
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected ReviewContext _context;

        public GenericRepository(ReviewContext context)
        {
            _context = context;
        }

        public abstract Task<List<T>> ListAllAsync();
        public abstract Task<T> CreateAsync(Object dto);
        public abstract Task<T> ReadAsync(long id);
        public abstract Task<T> UpdateAsync();
        public abstract Task<T> DeleteAsync(long id);
    }
}
