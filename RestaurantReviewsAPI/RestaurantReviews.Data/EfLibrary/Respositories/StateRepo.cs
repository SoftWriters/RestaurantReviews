using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public async Task<List<State>> Query(string code, string name)
        {
            var query = _context
                .States
                .AsQueryable();

            if (!string.IsNullOrEmpty(code))
                query = query.Where(state => state.Code == code);

            if (!string.IsNullOrEmpty(name))
                query = query.Where(state => state.Name == name);

            var results = await query
                .ToListAsync();

            return results
                .Select(state => new State
                {
                    Id = state.Id,
                    Name = state.Name,
                    Code = state.Code
                })
                .ToList();
        }
    }
}
