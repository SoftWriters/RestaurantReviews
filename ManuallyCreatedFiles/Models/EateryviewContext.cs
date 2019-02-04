using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EateryviewApi.Models
{
    public class EateryviewContext : DbContext
    {
        public EateryviewContext(DbContextOptions<EateryviewContext> options)
            : base(options)
        {
        }

        public DbSet<Restaurant> Restaurants{ get; set; }
        public DbSet<Review> Reviews { get; set; }
    }
}
