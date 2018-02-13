using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantReviews.Data.EfLibrary.Context;
using RestaurantReviews.Data.Framework.RepoContracts;
using RestaurantReviews.Data.Models;

namespace RestaurantReviews.Data.EfLibrary.Respositories
{
    public class StateRepo : IStateRepo
    {
        private readonly RestaurantReviewsContext _context;

        public StateRepo(RestaurantReviewsContext context)
        {
            _context = context;
        }

        public async Task<bool> Exists(string code = null, string name = null)
        {
            var query = _context
                .States
                .AsQueryable();

            if (!string.IsNullOrEmpty(code))
                query = query.Where(state => state.Code == code);

            if (!string.IsNullOrEmpty(name))
                query = query.Where(state => state.Name == name);

            return await query
                .AnyAsync();
        }
    }
}
