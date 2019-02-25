using RestaurantReviews.Data.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantReviews.Data.Entities
{
    [Table("Users", Schema = "DomainData")]
    public class User : IEntity
    {
        [Key]
        [Column("UserId")]
        public Guid Id { get; set; }

        [Column("DateCreated")]
        [Required(ErrorMessage = "DateCreated is required")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;

        [Column("FirstName")]
        [StringLength(50, ErrorMessage = "FirstName can't be longer than 50 characters")]
        public string FirstName { get; set; }

        [Column("MiddleName")]
        [StringLength(50, ErrorMessage = "MiddleName can't be longer than 50 characters")]
        public string MiddleName { get; set; }
    
        [Column("LastName")]
        [StringLength(50, ErrorMessage = "LastName can't be longer than 50 characters")]
        public string LastName { get; set; }

        [Column("EmailAddress")]
        [Required(ErrorMessage = "EmailAddress is required")]
        [StringLength(150, ErrorMessage = "EmailAddress can't be longer than 150 characters")]
        public string EmailAddress { get; set; }

        [Column("IsActive")]
        public bool IsActive { get; set; } = true;
    }
}
