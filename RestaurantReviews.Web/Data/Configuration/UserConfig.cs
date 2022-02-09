using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantReviews.Web.Data.Entities;

namespace RestaurantReviews.Web.Data.Configuration
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users", "dbo");
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).HasColumnName("UserId");
            builder.HasMany(u => u.Reviews);
        }
    }
}
