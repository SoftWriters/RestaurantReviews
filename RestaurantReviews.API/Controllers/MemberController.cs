using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RestaurantReviews.Entities;
using RestaurantReviews.Entities.Logic;
using RestaurantReviews.API.Models;

namespace RestaurantReviews.API.Controllers
{
    /// <summary>
    /// Member Controller
    /// </summary>
    public class MemberController : ApiController
    {
        readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Retrieves the Member instance matching the memberId parameter.
        /// </summary>
        /// <param name="memberId">The ID of the Member to retrieve.</param>
        /// <returns>The Member who is associated with the ID.</returns>
        [HttpGet]
        [Route("members/{memberId}")]
        public IHttpActionResult GetMember(long memberId)
        {
            try
            {
                return Ok(MemberManager.GetMember(memberId));
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return InternalServerError();
            }
        }

        /// <summary>
        /// Creates a new Member instance.
        /// </summary>
        /// <param name="member">Contains the information used to create a Member instance.</param>
        /// <returns>The newly created Member.</returns>
        [HttpPost]
        [Route("members")]
        public IHttpActionResult CreateMember(MemberModel member)
        {
            try
            {
                return Ok(MemberManager.CreateMember(member.UserName, member.FirstName, member.LastName, member.Email));
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return InternalServerError();
            }
        }

        /// <summary>
        /// Updates a Member.
        /// </summary>
        /// <param name="memberId">The ID of the Member to update.</param>
        /// <param name="member">Contains the information used to udpate a Member instance.</param>
        /// <returns>An updated Member instance.</returns>
        [HttpPut]
        [Route("members/{memberId}")]
        public IHttpActionResult UpdateMember(long memberId, MemberModel member)
        {
            try
            {
                return Ok(MemberManager.UpdateMember(memberId, member.UserName, member.FirstName, member.LastName, member.Email));
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return InternalServerError();
            }
        }

        /// <summary>
        /// Deletes a member instance.
        /// </summary>
        /// <param name="memberId">The ID of the Member to delete.</param>
        /// <returns>200 OK if successful.</returns>
        [HttpDelete]
        [Route("members/{memberId}")]
        public IHttpActionResult DeleteMember(long memberId)
        {
            try
            {
                MemberManager.DeleteMember(memberId);
                return Ok();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return InternalServerError();
            }
        }
    }
}
