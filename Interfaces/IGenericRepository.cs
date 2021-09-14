
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestaurantReviews
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> ListAllAsync();
        Task<T> CreateAsync(Object dto);
        Task<T> ReadAsync(long id);
        Task<T> UpdateAsync();
        Task<T> DeleteAsync(long id);
    }
}
