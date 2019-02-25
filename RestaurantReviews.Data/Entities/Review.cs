using RestaurantReviews.Data.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantReviews.Data.Entities
{
    [Table("Reviews", Schema = "DomainData")]
    public class Review : IEntity
    {
        [Key]
        [Column("ReviewId")]
        public Guid Id { get; set; }

        [Column("Comment")]
        [StringLength(1000, ErrorMessage = "Comment can't be longer than 1000 characters")]
        public string Comment { get; set; }

        [Column("Rating")]
        [Required(ErrorMessage = "Rating is required")]
        public int Rating { get; set; }

        [Column("RestaurauntId")]
        [Required(ErrorMessage = "RestaurauntId is required")]
        public Guid RestaurauntId { get; set; }

        [Column("SubmissionDate")]
        [Required(ErrorMessage = "Submission Date is required")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime SubmissionDate { get; set; }

        [Column("UserId")]
        [Required(ErrorMessage = "UserId is required")]
        public Guid UserId { get; set; }
    }
}
