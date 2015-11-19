namespace RestaurantReview.DAL.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Review : Base.EntityBase.Entity<Review>, Interface.IEntityBase<Review>
    {
        public int RestaurantID { get; set; }

        public int UserID { get; set; }

        public int Rating { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string Comments { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateCreated { get; set; }

        public virtual Restaurant Restaurant { get; set; }

        public virtual User User { get; set; }
    }
}
