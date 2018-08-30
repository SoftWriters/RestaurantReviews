using System;

namespace Restaurant_Review
{

    public class AspNetRoles
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class AspNetUserClaims
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
    }

    public class AspNetUserLogins
    {
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }
        public string UserId { get; set; }
    }

    public class AspNetUserRoles
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }
    }

    public class AspNetUsers
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTime LockoutEndDateUtc { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string UserName { get; set; }
    }

    public class ErrorLog
    {
        public int IDErrorLog { get; set; }
        public DateTime ErrorTime { get; set; }
        public string UserName { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class Restaurant
    {
        public int IDRestaurant { get; set; }
        public string RestaurantName { get; set; }
        public string RestaurantDescription { get; set; }
        public bool IsActive { get; set; }
        public bool Deleted { get; set; }
        public DateTime DateCreated { get; set; }
        public string IDUserCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public string IDUserUpdated { get; set; }
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Line3 { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public string StateCode { get; set; }
    }

    public class Reviews
    {
        public int IDReview { get; set; }
        public string ReviewText { get; set; }
        public bool IsActive { get; set; }
        public bool Deleted { get; set; }
        public int IDRestaurant { get; set; }
        public string IDUser { get; set; }
        public DateTime DateCreated { get; set; }
    }

}
