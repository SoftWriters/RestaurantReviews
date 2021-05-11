using Microsoft.EntityFrameworkCore;
using ORMLayer.TableDefinitions;

namespace ORMLayer
{
  public class DatabaseContext : DbContext
  {
    private readonly string _connectionString;
    public DatabaseContext() {
      /*
       * If you want to create an empty database using entity framework 
       * set your sql server connection string in this constructor like below:
       * 
       * _connectionString = @"Data Source=.\SQLExpress;Initial Catalog=RestaurantReviews;Integrated Security=SSPI;";
       * 
       * Then set this project to the start up project, and run Update-Database in the Package Manager Console;
       */
    }
    public DatabaseContext(DbContextOptions<DatabaseContext> options): base(options) { }
    public DatabaseContext(string connectionString) {
      _connectionString = connectionString;
    }

    public DbSet<City> Cities { get; set; }
    public DbSet<Restaurant> Restaurants { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Review> Reviews { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
      if(!string.IsNullOrWhiteSpace(_connectionString))
        optionsBuilder.UseSqlServer(_connectionString);
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
      //Add Unique Constraint on Restaurant for Name and Address
      builder.Entity<Restaurant>()
          .HasIndex(r => new { r.Name, r.Address })
          .IsUnique(true);

      //Add Unique Constraint on Review for User and Restaurant, so user cannot review bomb a restaurant 
      builder.Entity<Review>()
          .HasIndex(r => new { r.UserId, r.RestaurantId })
          .IsUnique(true);

    }
  }
}
