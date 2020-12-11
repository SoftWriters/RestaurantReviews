using Microsoft.AspNetCore.Mvc.Testing;
using RestaurantReviews.Data;
using RestaurantReviews.Model.Review.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReviews.Test
{
    public class ReviewControllerTests
    {
        private readonly WebApplicationFactory<Startup> _factory = new WebApplicationFactory<Startup>();

        public void GetReviews_ReturnsAllReviews_IfNoFilterSpecified()
        {
            using (var client = _factory.CreateClient())
            {
                var response = client.PostAsJsonAsync("/review/query", new ReviewQueryRequest
                {
                });
            }
        }
    }
}
