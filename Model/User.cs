using System.Collections.Generic;

namespace Model
{
    public class User
    {
        public int UserID { get; set; }

        public string Name { get; set; }

        public virtual List<Review> Reviews { get; set; }
    }
}