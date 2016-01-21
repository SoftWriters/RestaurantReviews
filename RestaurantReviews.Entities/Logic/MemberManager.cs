using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReviews.Entities.Logic
{
    /// <summary>
    /// Exposes Member business logic.
    /// </summary>
    public static class MemberManager
    {
        /// <summary>
        /// Validates that a member instance has the necessary information before persisting.  If string properties are composed of purely whitespace they will be set to null.
        /// </summary>
        /// <param name="member"></param>
        private static void ValidateMember(Member member)
        {
            if (string.IsNullOrWhiteSpace(member.UserName))
                throw (new System.ArgumentException("Member username cannot be null or whitespace."));
            else if (member.UserName.Length > 50)
                throw (new System.ArgumentException("Member username exceeds maximum length of 50 characters."));

            if (string.IsNullOrWhiteSpace(member.FirstName))
                member.FirstName = null;
            else if (member.FirstName.Length > 50)
                throw (new System.ArgumentException("Member first name exceeds maximum length of 50 characters."));

            if (string.IsNullOrWhiteSpace(member.LastName))
                member.LastName = null;
            else if (member.LastName.Length > 50)
                throw (new System.ArgumentException("Member last name exceeds maximum length of 50 characters."));

            if (string.IsNullOrWhiteSpace(member.Email))
                member.Email = null;
            else if (member.Email.Length > 50)
                throw (new System.ArgumentException("Member email exceeds maximum length of 50 characters."));
        }

        /// <summary>
        /// Persists a new member.
        /// </summary>
        /// <param name="userName">The member's username.</param>
        /// <param name="firstName">The member's first name.</param>
        /// <param name="lastName">The member's last name.</param>
        /// <param name="email">The member's email address.</param>
        /// <returns>An instance of the persisted member.</returns>
        public static Member CreateMember(string userName, string firstName, string lastName, string email)
        {
            Member member = new Member();
            member.UserName = userName;
            member.FirstName = firstName;
            member.LastName = lastName;
            member.Email = email;

            CreateMember(member);

            return member;
        }
        /// <summary>
        /// Persists a new member.
        /// </summary>
        /// <param name="member">The member instance to be persisted.</param>
        public static void CreateMember(Member member)
        {
            if (member.Id != -1)
                throw (new System.ArgumentException("Member is not a new instance."));

            ValidateMember(member);

            Data.MemberSQL.CreateMember(member);
        }
        /// <summary>
        /// Updates a member.
        /// </summary>
        /// <param name="memberId">The member id to be updated.</param>
        /// <param name="userName">The user name of the member.</param>
        /// <param name="firstName">The first name of the member.</param>
        /// <param name="lastName">The last name of the member.</param>
        /// <param name="email">The email address of the member.</param>
        /// <returns>An instance of the persisted member.</returns>
        public static Member UpdateMember(long memberId, string userName, string firstName, string lastName, string email)
        {
            Member member = new Member();
            member.Id = memberId;
            member.UserName = userName;
            member.FirstName = firstName;
            member.LastName = lastName;
            member.Email = email;

            UpdateMember(member);

            return member;
        }
        /// <summary>
        /// Updates a member.
        /// </summary>
        /// <param name="member">The member instance to be persisted.</param>
        public static void UpdateMember(Member member)
        {
            if (member.Id == -1)
                throw (new System.ArgumentException("Member is new instance and needs to be saved before updating."));

            ValidateMember(member);

            Data.MemberSQL.UpdateMember(member);
        }
        /// <summary>
        /// Deletes a member by ID.
        /// </summary>
        /// <param name="memberId">The ID of the member to be deleted.</param>
        public static void DeleteMember(long memberId)
        {
            Data.MemberSQL.DeleteMember(memberId);
        }
        /// <summary>
        /// Deletes a member instance.
        /// </summary>
        /// <param name="member">The member to be deleted.</param>
        public static void DeleteMember(Member member)
        {
            DeleteMember(member.Id);
        }
        /// <summary>
        /// Retrieves a member instance.
        /// </summary>
        /// <param name="memberId">The ID of the member to retrieve.</param>
        /// <returns>A member instance.</returns>
        public static Member GetMember(long memberId)
        {
            return Data.MemberSQL.GetMember(memberId);
        }
    }
}
