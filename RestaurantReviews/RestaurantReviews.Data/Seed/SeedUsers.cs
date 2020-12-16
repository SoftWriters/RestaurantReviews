using Platform.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestaurantReviews.Data.Seed
{
    public static class SeedUsers
    {
        public static User Homer { get; } = new User { Id = Guid.Parse("43757037-6429-4aa0-8c96-62867c419967"), First = "Homer", Last = "Simpson" };
        public static User Marge { get; } = new User { Id = Guid.Parse("ac6b3f45-2cdf-4a2d-8df1-764f25ca8614"), First = "Marge", Last = "Simpson" };
        public static User Bart { get; } = new User { Id = Guid.Parse("41355f76-973d-47cd-9c48-90ae826d9a0c"), First = "Bart", Last = "Simpson" };
        public static User Lisa { get; } = new User { Id = Guid.Parse("779ab260-7af2-421a-b0cf-4df17164a406"), First = "Lisa", Last = "Simpson" };
        public static User Maggie { get; } = new User { Id = Guid.Parse("292801c5-a3ca-4f35-9224-159ec9667e11"), First = "Maggie", Last = "Simpson" };

        public static IEnumerable<User> All { get; }
            = new LazyStaticProperties<User>(typeof(SeedUsers));
    }
}
